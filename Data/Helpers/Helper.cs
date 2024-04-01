using System.Linq.Expressions;

namespace Data.Helpers;

public static class Helper
{
    public const string BaseUrl = "http://localhost:5027";
    public static string GetPropertyName(Expression expression)
    {
        if (expression is UnaryExpression unary)
        {
            return GetPropertyName(unary.Operand);
        }
        else if (expression is MemberExpression member)
        {
            return member.Member.Name;
        }
        else if (expression is ParameterExpression parameter)
        {
            return parameter.Name;
        }
        else
        {
            return string.Empty;
        }
    }
}
