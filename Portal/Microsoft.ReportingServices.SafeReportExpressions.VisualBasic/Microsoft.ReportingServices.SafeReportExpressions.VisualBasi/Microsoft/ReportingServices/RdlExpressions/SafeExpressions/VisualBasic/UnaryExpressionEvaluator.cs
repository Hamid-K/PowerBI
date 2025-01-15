using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;
using Microsoft.VisualBasic.CompilerServices;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions.VisualBasic
{
	// Token: 0x0200000D RID: 13
	internal class UnaryExpressionEvaluator : ArithmeticEvaluator, IUnaryExpressionEvaluator
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00003BE4 File Offset: 0x00001DE4
		public object Evaluate(object operandValue, SyntaxKind expressionType)
		{
			object objectValue = RuntimeHelpers.GetObjectValue(base.CastToStrongType(RuntimeHelpers.GetObjectValue(operandValue)));
			object obj;
			switch (expressionType)
			{
			case 333:
				obj = Operators.PlusObject(objectValue);
				break;
			case 334:
				obj = Operators.NegateObject(objectValue);
				break;
			case 335:
				obj = Operators.NotObject(objectValue);
				break;
			default:
				throw new NotSupportedException(string.Format("Unary Expression Evaluator: Syntax kind <{0}> is not supported yet.", expressionType));
			}
			return obj;
		}
	}
}
