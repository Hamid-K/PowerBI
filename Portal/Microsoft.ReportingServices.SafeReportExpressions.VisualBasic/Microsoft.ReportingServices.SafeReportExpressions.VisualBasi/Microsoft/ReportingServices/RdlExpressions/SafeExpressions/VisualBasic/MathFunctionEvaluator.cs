using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;
using Microsoft.VisualBasic.CompilerServices;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions.VisualBasic
{
	// Token: 0x0200000B RID: 11
	internal class MathFunctionEvaluator : ArithmeticEvaluator, IFunctionEvaluator
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000031CC File Offset: 0x000013CC
		public object Evaluate(string functionName, List<object> arguments)
		{
			base.ValidateStrongType(arguments);
			string text = functionName.ToUpperInvariant();
			if (Operators.CompareString(text, "ROUND", false) == 0)
			{
				object obj2;
				if (arguments.Count == 1)
				{
					object[] array;
					bool[] array2;
					object obj = NewLateBinding.LateGet(null, typeof(Math), "Round", array = new object[] { arguments[0] }, null, null, array2 = new bool[] { true });
					if (array2[0])
					{
						arguments[0] = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
					}
					obj2 = obj;
				}
				else if (arguments.Count == 2)
				{
					object[] array;
					bool[] array2;
					object obj3 = NewLateBinding.LateGet(null, typeof(Math), "Round", array = new object[]
					{
						arguments[0],
						arguments[1]
					}, null, null, array2 = new bool[] { true, true });
					if (array2[0])
					{
						arguments[0] = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
					}
					if (array2[1])
					{
						arguments[1] = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[1]));
					}
					obj2 = obj3;
				}
				else
				{
					object[] array;
					bool[] array2;
					object obj4 = NewLateBinding.LateGet(null, typeof(Math), "Round", array = new object[]
					{
						arguments[0],
						arguments[1],
						arguments[2]
					}, null, null, array2 = new bool[] { true, true, true });
					if (array2[0])
					{
						arguments[0] = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[0]));
					}
					if (array2[1])
					{
						arguments[1] = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[1]));
					}
					if (array2[2])
					{
						arguments[2] = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(array[2]));
					}
					obj2 = obj4;
				}
				return obj2;
			}
			throw new NotSupportedException(string.Format("Function <{0}> is not supported yet.", functionName));
		}
	}
}
