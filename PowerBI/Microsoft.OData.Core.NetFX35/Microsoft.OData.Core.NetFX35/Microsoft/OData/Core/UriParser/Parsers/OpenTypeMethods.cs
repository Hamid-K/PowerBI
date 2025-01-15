using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x0200020A RID: 522
	internal static class OpenTypeMethods
	{
		// Token: 0x060012E2 RID: 4834 RVA: 0x00045550 File Offset: 0x00043750
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object GetValue(object value, string propertyName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00045557 File Offset: 0x00043757
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static IEnumerable<object> GetCollectionValue(object value, string propertyName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0004555E File Offset: 0x0004375E
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Add(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00045565 File Offset: 0x00043765
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object AndAlso(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x0004556C File Offset: 0x0004376C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Divide(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00045573 File Offset: 0x00043773
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Equal(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x0004557A File Offset: 0x0004377A
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object GreaterThan(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00045581 File Offset: 0x00043781
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object GreaterThanOrEqual(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00045588 File Offset: 0x00043788
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object LessThan(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x0004558F File Offset: 0x0004378F
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object LessThanOrEqual(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00045596 File Offset: 0x00043796
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Modulo(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0004559D File Offset: 0x0004379D
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Multiply(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x000455A4 File Offset: 0x000437A4
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object NotEqual(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x000455AB File Offset: 0x000437AB
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object OrElse(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x000455B2 File Offset: 0x000437B2
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Subtract(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x000455B9 File Offset: 0x000437B9
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Negate(object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x000455C0 File Offset: 0x000437C0
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Not(object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x000455C7 File Offset: 0x000437C7
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Convert(object value, IEdmTypeReference typeReference)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x000455CE File Offset: 0x000437CE
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object TypeIs(object value, IEdmTypeReference typeReference)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x000455D5 File Offset: 0x000437D5
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Concat(object first, object second)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x000455DC File Offset: 0x000437DC
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object EndsWith(object targetString, object substring)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x000455E3 File Offset: 0x000437E3
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object IndexOf(object targetString, object substring)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x000455EA File Offset: 0x000437EA
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Length(object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000455F1 File Offset: 0x000437F1
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Replace(object targetString, object substring, object newString)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x000455F8 File Offset: 0x000437F8
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object StartsWith(object targetString, object substring)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x000455FF File Offset: 0x000437FF
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Substring(object targetString, object startIndex)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00045606 File Offset: 0x00043806
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Substring(object targetString, object startIndex, object length)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0004560D File Offset: 0x0004380D
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Contains(object targetString, object substring)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00045614 File Offset: 0x00043814
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		[SuppressMessage("Microsoft.Globalization", "CA1308", Justification = "Need to support ToLower function")]
		public static object ToLower(object targetString)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0004561B File Offset: 0x0004381B
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object ToUpper(object targetString)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00045622 File Offset: 0x00043822
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Trim(object targetString)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00045629 File Offset: 0x00043829
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Year(object dateTime)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00045630 File Offset: 0x00043830
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Month(object dateTime)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00045637 File Offset: 0x00043837
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Day(object dateTime)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0004563E File Offset: 0x0004383E
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Hour(object dateTime)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00045645 File Offset: 0x00043845
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Minute(object dateTime)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0004564C File Offset: 0x0004384C
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Second(object dateTime)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00045653 File Offset: 0x00043853
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Ceiling(object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0004565A File Offset: 0x0004385A
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Floor(object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00045661 File Offset: 0x00043861
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", Justification = "Parameters will be used in the actual impl")]
		public static object Round(object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00045668 File Offset: 0x00043868
		internal static Expression AddExpression(Expression left, Expression right)
		{
			return Expression.Add(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), OpenTypeMethods.AddMethodInfo);
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00045680 File Offset: 0x00043880
		internal static Expression AndAlsoExpression(Expression left, Expression right)
		{
			return Expression.Call(OpenTypeMethods.AndAlsoMethodInfo, new Expression[]
			{
				OpenTypeMethods.ExpressionAsObject(left),
				OpenTypeMethods.ExpressionAsObject(right)
			});
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x000456B1 File Offset: 0x000438B1
		internal static Expression DivideExpression(Expression left, Expression right)
		{
			return Expression.Divide(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), OpenTypeMethods.DivideMethodInfo);
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x000456C9 File Offset: 0x000438C9
		internal static Expression EqualExpression(Expression left, Expression right)
		{
			return Expression.Equal(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), false, OpenTypeMethods.EqualMethodInfo);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x000456E2 File Offset: 0x000438E2
		internal static Expression GreaterThanExpression(Expression left, Expression right)
		{
			return Expression.GreaterThan(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), false, OpenTypeMethods.GreaterThanMethodInfo);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x000456FB File Offset: 0x000438FB
		internal static Expression GreaterThanOrEqualExpression(Expression left, Expression right)
		{
			return Expression.GreaterThanOrEqual(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), false, OpenTypeMethods.GreaterThanOrEqualMethodInfo);
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00045714 File Offset: 0x00043914
		internal static Expression LessThanExpression(Expression left, Expression right)
		{
			return Expression.LessThan(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), false, OpenTypeMethods.LessThanMethodInfo);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x0004572D File Offset: 0x0004392D
		internal static Expression LessThanOrEqualExpression(Expression left, Expression right)
		{
			return Expression.LessThanOrEqual(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), false, OpenTypeMethods.LessThanOrEqualMethodInfo);
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x00045746 File Offset: 0x00043946
		internal static Expression ModuloExpression(Expression left, Expression right)
		{
			return Expression.Modulo(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), OpenTypeMethods.ModuloMethodInfo);
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0004575E File Offset: 0x0004395E
		internal static Expression MultiplyExpression(Expression left, Expression right)
		{
			return Expression.Multiply(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), OpenTypeMethods.MultiplyMethodInfo);
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00045778 File Offset: 0x00043978
		internal static Expression OrElseExpression(Expression left, Expression right)
		{
			return Expression.Call(OpenTypeMethods.OrElseMethodInfo, new Expression[]
			{
				OpenTypeMethods.ExpressionAsObject(left),
				OpenTypeMethods.ExpressionAsObject(right)
			});
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x000457A9 File Offset: 0x000439A9
		internal static Expression NotEqualExpression(Expression left, Expression right)
		{
			return Expression.NotEqual(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), false, OpenTypeMethods.NotEqualMethodInfo);
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x000457C2 File Offset: 0x000439C2
		internal static Expression SubtractExpression(Expression left, Expression right)
		{
			return Expression.Subtract(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), OpenTypeMethods.SubtractMethodInfo);
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x000457DA File Offset: 0x000439DA
		internal static Expression NegateExpression(Expression expression)
		{
			return Expression.Negate(OpenTypeMethods.ExpressionAsObject(expression), OpenTypeMethods.NegateMethodInfo);
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x000457EC File Offset: 0x000439EC
		internal static Expression NotExpression(Expression expression)
		{
			return Expression.Not(OpenTypeMethods.ExpressionAsObject(expression), OpenTypeMethods.NotMethodInfo);
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x000457FE File Offset: 0x000439FE
		private static Expression ExpressionAsObject(Expression expression)
		{
			if (!expression.Type.IsValueType())
			{
				return expression;
			}
			return Expression.Convert(expression, typeof(object));
		}

		// Token: 0x04000819 RID: 2073
		internal static readonly MethodInfo AddMethodInfo = typeof(OpenTypeMethods).GetMethod("Add", 24);

		// Token: 0x0400081A RID: 2074
		internal static readonly MethodInfo AndAlsoMethodInfo = typeof(OpenTypeMethods).GetMethod("AndAlso", 24);

		// Token: 0x0400081B RID: 2075
		internal static readonly MethodInfo ConvertMethodInfo = typeof(OpenTypeMethods).GetMethod("Convert", 24);

		// Token: 0x0400081C RID: 2076
		internal static readonly MethodInfo DivideMethodInfo = typeof(OpenTypeMethods).GetMethod("Divide", 24);

		// Token: 0x0400081D RID: 2077
		internal static readonly MethodInfo EqualMethodInfo = typeof(OpenTypeMethods).GetMethod("Equal", 24);

		// Token: 0x0400081E RID: 2078
		internal static readonly MethodInfo GreaterThanMethodInfo = typeof(OpenTypeMethods).GetMethod("GreaterThan", 24);

		// Token: 0x0400081F RID: 2079
		internal static readonly MethodInfo GreaterThanOrEqualMethodInfo = typeof(OpenTypeMethods).GetMethod("GreaterThanOrEqual", 24);

		// Token: 0x04000820 RID: 2080
		internal static readonly MethodInfo LessThanMethodInfo = typeof(OpenTypeMethods).GetMethod("LessThan", 24);

		// Token: 0x04000821 RID: 2081
		internal static readonly MethodInfo LessThanOrEqualMethodInfo = typeof(OpenTypeMethods).GetMethod("LessThanOrEqual", 24);

		// Token: 0x04000822 RID: 2082
		internal static readonly MethodInfo ModuloMethodInfo = typeof(OpenTypeMethods).GetMethod("Modulo", 24);

		// Token: 0x04000823 RID: 2083
		internal static readonly MethodInfo MultiplyMethodInfo = typeof(OpenTypeMethods).GetMethod("Multiply", 24);

		// Token: 0x04000824 RID: 2084
		internal static readonly MethodInfo NegateMethodInfo = typeof(OpenTypeMethods).GetMethod("Negate", 24);

		// Token: 0x04000825 RID: 2085
		internal static readonly MethodInfo NotMethodInfo = typeof(OpenTypeMethods).GetMethod("Not", 24);

		// Token: 0x04000826 RID: 2086
		internal static readonly MethodInfo NotEqualMethodInfo = typeof(OpenTypeMethods).GetMethod("NotEqual", 24);

		// Token: 0x04000827 RID: 2087
		internal static readonly MethodInfo OrElseMethodInfo = typeof(OpenTypeMethods).GetMethod("OrElse", 24);

		// Token: 0x04000828 RID: 2088
		internal static readonly MethodInfo SubtractMethodInfo = typeof(OpenTypeMethods).GetMethod("Subtract", 24);

		// Token: 0x04000829 RID: 2089
		internal static readonly MethodInfo TypeIsMethodInfo = typeof(OpenTypeMethods).GetMethod("TypeIs", 24);

		// Token: 0x0400082A RID: 2090
		internal static readonly MethodInfo GetValueOpenPropertyMethodInfo = typeof(OpenTypeMethods).GetMethod("GetValue", new Type[]
		{
			typeof(object),
			typeof(string)
		}, true, true);

		// Token: 0x0400082B RID: 2091
		internal static readonly MethodInfo GetCollectionValueOpenPropertyMethodInfo = typeof(OpenTypeMethods).GetMethod("GetCollectionValue", new Type[]
		{
			typeof(object),
			typeof(string)
		}, true, true);
	}
}
