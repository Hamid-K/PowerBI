using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions.VisualBasic
{
	// Token: 0x02000009 RID: 9
	internal class ExecutionFlowFunctionEvaluator : ArithmeticEvaluator, IFunctionEvaluator
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00003068 File Offset: 0x00001268
		public object Evaluate(string functionName, List<object> arguments)
		{
			base.ValidateStrongType(arguments);
			string text = functionName.ToUpperInvariant();
			object obj;
			if (Operators.CompareString(text, "IIF", false) != 0)
			{
				if (Operators.CompareString(text, "SWITCH", false) != 0)
				{
					throw new NotSupportedException(string.Format("Function <{0}> is not supported yet.", functionName));
				}
				obj = Interaction.Switch(arguments.ToArray());
			}
			else
			{
				obj = Interaction.IIf(Conversions.ToBoolean(arguments[0]), RuntimeHelpers.GetObjectValue(arguments[1]), RuntimeHelpers.GetObjectValue(arguments[2]));
			}
			return obj;
		}
	}
}
