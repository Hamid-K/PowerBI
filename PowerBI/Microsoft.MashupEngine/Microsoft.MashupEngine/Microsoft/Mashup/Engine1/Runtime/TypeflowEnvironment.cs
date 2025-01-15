using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200133C RID: 4924
	internal static class TypeflowEnvironment
	{
		// Token: 0x060081DF RID: 33247 RVA: 0x001B9154 File Offset: 0x001B7354
		public static TypeValue GetReturnType(this ITypeflowEnvironment environment, FunctionValue function, IList<IExpression> arguments)
		{
			FunctionTypeValue asFunctionType = function.Type.AsFunctionType;
			if (asFunctionType.Abstract || arguments.Count < asFunctionType.Min)
			{
				return TypeValue.Any;
			}
			return function.GetTypeflowReturnType(arguments, environment);
		}
	}
}
