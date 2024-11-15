﻿using System.Linq.Expressions;
using Z.Core.Extensions;

namespace Z.Core.Validations;

public class Validator<T> : IValidator<T>
{
    private T? _current;

    private Dictionary<string, object> _evals = [];

    private Dictionary<string, string> _errors { get; } = [];

    public void AddMessage(string field, string message)
    {
        if (!Util.IsEmpty(message))
            _errors.Add(field, message);
    }

    public IEvaluator<T, TFld> For<TFld>(Expression<Func<T, TFld>> selector)
    {
        var name = selector.GetMemberName();
        if (_evals.TryGetValue(name, out var obj) && obj is IEvaluator<T, TFld> cval)
        {
            cval.Restart(_current);
            return cval;
        }
        
        var eval = new Evaluator<T, TFld>(this, selector.Compile(), _current, name);
        _evals[name] = eval;

        return eval;
    }

    public virtual bool Validate(T? value)
    {
        _errors.Clear();
        _current = value;
        if (Util.IsEmpty(_current))
            AddMessage("", "Validated fail with null or empty current value");

        return _errors.Count == 0;
    }
}