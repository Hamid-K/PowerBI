using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200002B RID: 43
	public static class OpenTypeMethods
	{
		// Token: 0x060000BF RID: 191 RVA: 0x00005866 File Offset: 0x00003A66
		public static object GetValue(object value, string propertyName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000586D File Offset: 0x00003A6D
		public static object Add(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005874 File Offset: 0x00003A74
		public static object AndAlso(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000587B File Offset: 0x00003A7B
		public static object Divide(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005882 File Offset: 0x00003A82
		public static object Equal(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005889 File Offset: 0x00003A89
		public static object GreaterThan(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005890 File Offset: 0x00003A90
		public static object GreaterThanOrEqual(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005897 File Offset: 0x00003A97
		public static object LessThan(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000589E File Offset: 0x00003A9E
		public static object LessThanOrEqual(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000058A5 File Offset: 0x00003AA5
		public static object Modulo(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000058AC File Offset: 0x00003AAC
		public static object Multiply(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000058B3 File Offset: 0x00003AB3
		public static object NotEqual(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000058BA File Offset: 0x00003ABA
		public static object OrElse(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000058C1 File Offset: 0x00003AC1
		public static object Subtract(object left, object right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000058C8 File Offset: 0x00003AC8
		public static object Negate(object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000058CF File Offset: 0x00003ACF
		public static object Not(object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000058D6 File Offset: 0x00003AD6
		public static object Convert(object value, IEdmTypeReference typeReference)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000058DD File Offset: 0x00003ADD
		public static object TypeIs(object value, IEdmTypeReference typeReference)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000058E4 File Offset: 0x00003AE4
		public static object Concat(object first, object second)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000058EB File Offset: 0x00003AEB
		public static object EndsWith(object targetString, object substring)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000058F2 File Offset: 0x00003AF2
		public static object IndexOf(object targetString, object substring)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000058F9 File Offset: 0x00003AF9
		public static object Length(object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005900 File Offset: 0x00003B00
		public static object Replace(object targetString, object substring, object newString)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005907 File Offset: 0x00003B07
		public static object StartsWith(object targetString, object substring)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000590E File Offset: 0x00003B0E
		public static object Substring(object targetString, object startIndex)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005915 File Offset: 0x00003B15
		public static object Substring(object targetString, object startIndex, object length)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000591C File Offset: 0x00003B1C
		public static object SubstringOf(object substring, object targetString)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005923 File Offset: 0x00003B23
		public static object ToLower(object targetString)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000592A File Offset: 0x00003B2A
		public static object ToUpper(object targetString)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005931 File Offset: 0x00003B31
		public static object Trim(object targetString)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005938 File Offset: 0x00003B38
		public static object Year(object dateTime)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000593F File Offset: 0x00003B3F
		public static object Month(object dateTime)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005946 File Offset: 0x00003B46
		public static object Day(object dateTime)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000594D File Offset: 0x00003B4D
		public static object Hour(object dateTime)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005954 File Offset: 0x00003B54
		public static object Minute(object dateTime)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000595B File Offset: 0x00003B5B
		public static object Second(object dateTime)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005962 File Offset: 0x00003B62
		public static object Ceiling(object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005969 File Offset: 0x00003B69
		public static object Floor(object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005970 File Offset: 0x00003B70
		public static object Round(object value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005977 File Offset: 0x00003B77
		internal static Expression AddExpression(Expression left, Expression right)
		{
			return Expression.Add(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), OpenTypeMethods.AddMethodInfo);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005990 File Offset: 0x00003B90
		internal static Expression AndAlsoExpression(Expression left, Expression right)
		{
			return Expression.Call(OpenTypeMethods.AndAlsoMethodInfo, new Expression[]
			{
				OpenTypeMethods.ExpressionAsObject(left),
				OpenTypeMethods.ExpressionAsObject(right)
			});
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000059C1 File Offset: 0x00003BC1
		internal static Expression DivideExpression(Expression left, Expression right)
		{
			return Expression.Divide(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), OpenTypeMethods.DivideMethodInfo);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000059D9 File Offset: 0x00003BD9
		internal static Expression EqualExpression(Expression left, Expression right)
		{
			return Expression.Equal(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), false, OpenTypeMethods.EqualMethodInfo);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000059F2 File Offset: 0x00003BF2
		internal static Expression GreaterThanExpression(Expression left, Expression right)
		{
			return Expression.GreaterThan(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), false, OpenTypeMethods.GreaterThanMethodInfo);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005A0B File Offset: 0x00003C0B
		internal static Expression GreaterThanOrEqualExpression(Expression left, Expression right)
		{
			return Expression.GreaterThanOrEqual(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), false, OpenTypeMethods.GreaterThanOrEqualMethodInfo);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005A24 File Offset: 0x00003C24
		internal static Expression LessThanExpression(Expression left, Expression right)
		{
			return Expression.LessThan(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), false, OpenTypeMethods.LessThanMethodInfo);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005A3D File Offset: 0x00003C3D
		internal static Expression LessThanOrEqualExpression(Expression left, Expression right)
		{
			return Expression.LessThanOrEqual(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), false, OpenTypeMethods.LessThanOrEqualMethodInfo);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005A56 File Offset: 0x00003C56
		internal static Expression ModuloExpression(Expression left, Expression right)
		{
			return Expression.Modulo(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), OpenTypeMethods.ModuloMethodInfo);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005A6E File Offset: 0x00003C6E
		internal static Expression MultiplyExpression(Expression left, Expression right)
		{
			return Expression.Multiply(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), OpenTypeMethods.MultiplyMethodInfo);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005A88 File Offset: 0x00003C88
		internal static Expression OrElseExpression(Expression left, Expression right)
		{
			return Expression.Call(OpenTypeMethods.OrElseMethodInfo, new Expression[]
			{
				OpenTypeMethods.ExpressionAsObject(left),
				OpenTypeMethods.ExpressionAsObject(right)
			});
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005AB9 File Offset: 0x00003CB9
		internal static Expression NotEqualExpression(Expression left, Expression right)
		{
			return Expression.NotEqual(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), false, OpenTypeMethods.NotEqualMethodInfo);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005AD2 File Offset: 0x00003CD2
		internal static Expression SubtractExpression(Expression left, Expression right)
		{
			return Expression.Subtract(OpenTypeMethods.ExpressionAsObject(left), OpenTypeMethods.ExpressionAsObject(right), OpenTypeMethods.SubtractMethodInfo);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005AEA File Offset: 0x00003CEA
		internal static Expression NegateExpression(Expression expression)
		{
			return Expression.Negate(OpenTypeMethods.ExpressionAsObject(expression), OpenTypeMethods.NegateMethodInfo);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005AFC File Offset: 0x00003CFC
		internal static Expression NotExpression(Expression expression)
		{
			return Expression.Not(OpenTypeMethods.ExpressionAsObject(expression), OpenTypeMethods.NotMethodInfo);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005B0E File Offset: 0x00003D0E
		private static Expression ExpressionAsObject(Expression expression)
		{
			if (!expression.Type.IsValueType)
			{
				return expression;
			}
			return Expression.Convert(expression, typeof(object));
		}

		// Token: 0x04000142 RID: 322
		internal static readonly MethodInfo AddMethodInfo = typeof(OpenTypeMethods).GetMethod("Add", 24);

		// Token: 0x04000143 RID: 323
		internal static readonly MethodInfo AndAlsoMethodInfo = typeof(OpenTypeMethods).GetMethod("AndAlso", 24);

		// Token: 0x04000144 RID: 324
		internal static readonly MethodInfo ConvertMethodInfo = typeof(OpenTypeMethods).GetMethod("Convert", 24);

		// Token: 0x04000145 RID: 325
		internal static readonly MethodInfo DivideMethodInfo = typeof(OpenTypeMethods).GetMethod("Divide", 24);

		// Token: 0x04000146 RID: 326
		internal static readonly MethodInfo EqualMethodInfo = typeof(OpenTypeMethods).GetMethod("Equal", 24);

		// Token: 0x04000147 RID: 327
		internal static readonly MethodInfo GreaterThanMethodInfo = typeof(OpenTypeMethods).GetMethod("GreaterThan", 24);

		// Token: 0x04000148 RID: 328
		internal static readonly MethodInfo GreaterThanOrEqualMethodInfo = typeof(OpenTypeMethods).GetMethod("GreaterThanOrEqual", 24);

		// Token: 0x04000149 RID: 329
		internal static readonly MethodInfo LessThanMethodInfo = typeof(OpenTypeMethods).GetMethod("LessThan", 24);

		// Token: 0x0400014A RID: 330
		internal static readonly MethodInfo LessThanOrEqualMethodInfo = typeof(OpenTypeMethods).GetMethod("LessThanOrEqual", 24);

		// Token: 0x0400014B RID: 331
		internal static readonly MethodInfo ModuloMethodInfo = typeof(OpenTypeMethods).GetMethod("Modulo", 24);

		// Token: 0x0400014C RID: 332
		internal static readonly MethodInfo MultiplyMethodInfo = typeof(OpenTypeMethods).GetMethod("Multiply", 24);

		// Token: 0x0400014D RID: 333
		internal static readonly MethodInfo NegateMethodInfo = typeof(OpenTypeMethods).GetMethod("Negate", 24);

		// Token: 0x0400014E RID: 334
		internal static readonly MethodInfo NotMethodInfo = typeof(OpenTypeMethods).GetMethod("Not", 24);

		// Token: 0x0400014F RID: 335
		internal static readonly MethodInfo NotEqualMethodInfo = typeof(OpenTypeMethods).GetMethod("NotEqual", 24);

		// Token: 0x04000150 RID: 336
		internal static readonly MethodInfo OrElseMethodInfo = typeof(OpenTypeMethods).GetMethod("OrElse", 24);

		// Token: 0x04000151 RID: 337
		internal static readonly MethodInfo SubtractMethodInfo = typeof(OpenTypeMethods).GetMethod("Subtract", 24);

		// Token: 0x04000152 RID: 338
		internal static readonly MethodInfo TypeIsMethodInfo = typeof(OpenTypeMethods).GetMethod("TypeIs", 24);

		// Token: 0x04000153 RID: 339
		internal static readonly MethodInfo GetValueOpenPropertyMethodInfo = typeof(OpenTypeMethods).GetMethod("GetValue", 24, null, new Type[]
		{
			typeof(object),
			typeof(string)
		}, null);
	}
}
