using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ArquitetaWeb.Common.Infra.MvcHtmlExtensions
{
    public static class ExpressionExtensions
    {
        private static readonly PropertyInfo BodyInfo = (((Expression<Func<Expression<object>, Expression>>)(e => e.Body)).Body as MemberExpression).Member as PropertyInfo;

        internal static TResultExpression HandleUnary<TResultExpression>(this Expression expression) where TResultExpression : Expression
        {
            var unary = expression as UnaryExpression;
            return (unary != null ? unary.Operand : expression) as TResultExpression;
        }

        public static TInfo GetMemberInfo<TInfo>(this Expression expression) where TInfo : MemberInfo
        {
            if (expression == null) throw new ArgumentNullException("expression");
            return expression.GetMemberInfo() as TInfo;
        }

        public static MemberInfo GetMemberInfo(this Expression expression)
        {
            if (expression.GetType().IsGenericType) expression = BodyInfo.GetValue(expression, null) as Expression;
            var memberExpression = HandleUnary<MemberExpression>(expression);
            if (memberExpression != null) return memberExpression.Expression.Type.GetMember(memberExpression.Member.Name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).First();
            var methodExpression = HandleUnary<MethodCallExpression>(expression);
            return methodExpression == null ? null : methodExpression.Method;
        }

        public static PropertyInfo GetPropertyInfo<TObject, TProperty>(this Expression<Func<TObject, TProperty>> expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");
            return expression.Body.GetMemberInfo<PropertyInfo>();
        }

        public static MethodInfo GetMethodInfo<TDelegate>(this Expression<TDelegate> expression)
        {
            var methodCallExpression = HandleUnary<MethodCallExpression>(expression.Body);
            if (methodCallExpression == null) return null;
            return methodCallExpression.Method;
        }

    }
}
