using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions.VisualBasic
{
	// Token: 0x02000008 RID: 8
	internal class DateTimeFunctionEvaluator : ArithmeticEvaluator, IFunctionEvaluator
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002E78 File Offset: 0x00001078
		public object Evaluate(string functionName, List<object> arguments)
		{
			base.ValidateStrongType(arguments);
			string text = functionName.ToUpperInvariant();
			object obj2;
			if (Operators.CompareString(text, "NOW", false) != 0)
			{
				if (Operators.CompareString(text, "TODAY", false) != 0)
				{
					if (Operators.CompareString(text, "TIMEOFDAY", false) != 0)
					{
						if (Operators.CompareString(text, "DATEADD", false) != 0)
						{
							throw new NotSupportedException(string.Format("Function <{0}> is not supported yet.", functionName));
						}
						if (arguments[0] != null)
						{
							Type type = arguments[0].GetType();
							if (type == typeof(DateInterval))
							{
								return DateAndTime.DateAdd((DateInterval)arguments[0], Conversions.ToDouble(arguments[1]), Conversions.ToDate(arguments[2]));
							}
							if (type == typeof(string))
							{
								return DateAndTime.DateAdd((string)arguments[0], Conversions.ToDouble(arguments[1]), RuntimeHelpers.GetObjectValue(arguments[2]));
							}
						}
						object[] array;
						bool[] array2;
						object obj = NewLateBinding.LateGet(null, typeof(DateAndTime), "DateAdd", array = new object[]
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
						obj2 = obj;
					}
					else
					{
						obj2 = DateAndTime.TimeOfDay;
					}
				}
				else
				{
					obj2 = DateAndTime.Today;
				}
			}
			else
			{
				obj2 = DateAndTime.Now;
			}
			return obj2;
		}
	}
}
