using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions.VisualBasic
{
	// Token: 0x0200000A RID: 10
	internal class InspectionFunctionEvaluator : ArithmeticEvaluator, IFunctionEvaluator
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000030F8 File Offset: 0x000012F8
		public object Evaluate(string functionName, List<object> arguments)
		{
			base.ValidateStrongType(arguments);
			string text = functionName.ToUpperInvariant();
			object obj;
			if (Operators.CompareString(text, "ISARRAY", false) != 0)
			{
				if (Operators.CompareString(text, "ISDATE", false) != 0)
				{
					if (Operators.CompareString(text, "ISNOTHING", false) != 0)
					{
						if (Operators.CompareString(text, "ISNUMERIC", false) != 0)
						{
							throw new NotSupportedException(string.Format("Function <{0}> is not supported yet.", functionName));
						}
						obj = Versioned.IsNumeric(RuntimeHelpers.GetObjectValue(arguments[0]));
					}
					else
					{
						obj = Information.IsNothing(RuntimeHelpers.GetObjectValue(arguments[0]));
					}
				}
				else
				{
					obj = Information.IsDate(RuntimeHelpers.GetObjectValue(arguments[0]));
				}
			}
			else
			{
				obj = Information.IsArray(RuntimeHelpers.GetObjectValue(arguments[0]));
			}
			return obj;
		}
	}
}
