using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200000F RID: 15
	internal sealed class BuiltInFunctionSignature : FunctionSignature
	{
		// Token: 0x0600003A RID: 58 RVA: 0x0000336A File Offset: 0x0000156A
		internal BuiltInFunctionSignature(Func<Expression[], Expression> buildExpression, IEdmTypeReference returnType, params IEdmTypeReference[] argumentTypes)
			: base(argumentTypes)
		{
			this.returnType = returnType;
			this.buildExpression = buildExpression;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003381 File Offset: 0x00001581
		private BuiltInFunctionSignature(IEdmTypeReference returnType, params IEdmTypeReference[] argumentTypes)
			: base(argumentTypes)
		{
			this.returnType = returnType;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003391 File Offset: 0x00001591
		internal IEdmTypeReference ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000033B4 File Offset: 0x000015B4
		internal static BuiltInFunctionSignature CreateFromInstanceMethodCall(string methodName, IEdmPrimitiveTypeReference returnType, IEdmModel model, params IEdmPrimitiveTypeReference[] argumentTypes)
		{
			MethodInfo method = argumentTypes[0].GetInstanceType(model).GetMethod(methodName, 20, null, Enumerable.ToArray<Type>(Enumerable.Select<IEdmPrimitiveTypeReference, Type>(Enumerable.Skip<IEdmPrimitiveTypeReference>(argumentTypes, 1), (IEdmPrimitiveTypeReference argumentType) => TypeUtils.GetNonNullableType(argumentType.GetInstanceType(model)))), null);
			BuiltInFunctionSignature builtInFunctionSignature = new BuiltInFunctionSignature(returnType, argumentTypes);
			builtInFunctionSignature.memberInfo = method;
			builtInFunctionSignature.buildExpression = new Func<Expression[], Expression>(builtInFunctionSignature.BuildInstanceMethodCallExpression);
			return builtInFunctionSignature;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003444 File Offset: 0x00001644
		internal static BuiltInFunctionSignature CreateFromStaticMethodCall(Type targetType, string methodName, IEdmPrimitiveTypeReference returnType, IEdmModel model, params IEdmPrimitiveTypeReference[] argumentTypes)
		{
			MethodInfo method = targetType.GetMethod(methodName, 24, null, Enumerable.ToArray<Type>(Enumerable.Select<IEdmPrimitiveTypeReference, Type>(argumentTypes, (IEdmPrimitiveTypeReference argumentType) => TypeUtils.GetNonNullableType(argumentType.GetInstanceType(model)))), null);
			BuiltInFunctionSignature builtInFunctionSignature = new BuiltInFunctionSignature(returnType, argumentTypes);
			builtInFunctionSignature.memberInfo = method;
			builtInFunctionSignature.buildExpression = new Func<Expression[], Expression>(builtInFunctionSignature.BuildStaticMethodCallExpression);
			return builtInFunctionSignature;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000034A8 File Offset: 0x000016A8
		internal static BuiltInFunctionSignature CreateFromPropertyAccess(string propertyName, IEdmPrimitiveTypeReference returnType, IEdmModel model, params IEdmPrimitiveTypeReference[] argumentTypes)
		{
			PropertyInfo property = TypeUtils.GetNonNullableType(argumentTypes[0].GetInstanceType(model)).GetProperty(propertyName, 20);
			BuiltInFunctionSignature builtInFunctionSignature = new BuiltInFunctionSignature(returnType, argumentTypes);
			builtInFunctionSignature.memberInfo = property;
			builtInFunctionSignature.buildExpression = new Func<Expression[], Expression>(builtInFunctionSignature.BuildPropertyAccessExpression);
			return builtInFunctionSignature;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000034EE File Offset: 0x000016EE
		internal Expression BuildExpression(Expression[] argumentExpressions)
		{
			return this.buildExpression.Invoke(argumentExpressions);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000034FC File Offset: 0x000016FC
		private Expression BuildInstanceMethodCallExpression(Expression[] argumentExpressions)
		{
			Expression[] array = new Expression[argumentExpressions.Length - 1];
			Array.Copy(argumentExpressions, 1, array, 0, argumentExpressions.Length - 1);
			return Expression.Call(argumentExpressions[0], (MethodInfo)this.memberInfo, array);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003536 File Offset: 0x00001736
		private Expression BuildStaticMethodCallExpression(Expression[] argumentExpressions)
		{
			return Expression.Call(null, (MethodInfo)this.memberInfo, argumentExpressions);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000354A File Offset: 0x0000174A
		private Expression BuildPropertyAccessExpression(Expression[] argumentExpressions)
		{
			return Expression.Property(argumentExpressions[0], (PropertyInfo)this.memberInfo);
		}

		// Token: 0x0400003E RID: 62
		private IEdmTypeReference returnType;

		// Token: 0x0400003F RID: 63
		private Func<Expression[], Expression> buildExpression;

		// Token: 0x04000040 RID: 64
		private MemberInfo memberInfo;
	}
}
