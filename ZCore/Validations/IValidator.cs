using System.Linq.Expressions;

namespace Z.Core.Validations;

public interface IValidator<T>
{
    void AddMessage(string field, string message);

    IEvaluator<T, TFld> For<TFld>(Expression<Func<T, TFld>> selector);

    bool Validate(T? value);
}