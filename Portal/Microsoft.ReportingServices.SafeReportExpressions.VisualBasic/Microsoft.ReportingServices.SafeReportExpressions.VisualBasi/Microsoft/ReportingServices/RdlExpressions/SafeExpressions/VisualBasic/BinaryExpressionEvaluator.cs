using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions.VisualBasic
{
	// Token: 0x02000006 RID: 6
	internal class BinaryExpressionEvaluator : ArithmeticEvaluator, IBinaryExpressionEvaluator
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002C0C File Offset: 0x00000E0C
		public object Evaluate(object leftValue, object rightValue, SyntaxKind expressionType)
		{
			object objectValue = RuntimeHelpers.GetObjectValue(base.CastToStrongType(RuntimeHelpers.GetObjectValue(leftValue)));
			object objectValue2 = RuntimeHelpers.GetObjectValue(base.CastToStrongType(RuntimeHelpers.GetObjectValue(rightValue)));
			switch (expressionType)
			{
			case 307:
				return Operators.AddObject(objectValue, objectValue2);
			case 308:
				return Operators.SubtractObject(objectValue, objectValue2);
			case 309:
				return Operators.MultiplyObject(objectValue, objectValue2);
			case 310:
				return Operators.DivideObject(objectValue, objectValue2);
			case 311:
				return Operators.IntDivideObject(objectValue, objectValue2);
			case 314:
				return Operators.ExponentObject(objectValue, objectValue2);
			case 315:
				return Operators.LeftShiftObject(objectValue, objectValue2);
			case 316:
				return Operators.RightShiftObject(objectValue, objectValue2);
			case 317:
				return Operators.ConcatenateObject(objectValue, objectValue2);
			case 318:
				return Operators.ModObject(objectValue, objectValue2);
			case 319:
				return Operators.CompareObjectEqual(objectValue, objectValue2, false);
			case 320:
				return Operators.CompareObjectNotEqual(objectValue, objectValue2, false);
			case 321:
				return Operators.CompareObjectLess(objectValue, objectValue2, false);
			case 322:
				return Operators.CompareObjectLessEqual(objectValue, objectValue2, false);
			case 323:
				return Operators.CompareObjectGreaterEqual(objectValue, objectValue2, false);
			case 324:
				return Operators.CompareObjectGreater(objectValue, objectValue2, false);
			case 327:
				return LikeOperator.LikeObject(objectValue, objectValue2, CompareMethod.Binary);
			case 328:
				return Operators.OrObject(objectValue, objectValue2);
			case 329:
				return Operators.XorObject(objectValue, objectValue2);
			case 330:
				return Operators.AndObject(objectValue, objectValue2);
			case 331:
				return Conversions.ToBoolean(objectValue) || Conversions.ToBoolean(objectValue2);
			case 332:
				return Conversions.ToBoolean(objectValue) && Conversions.ToBoolean(objectValue2);
			}
			throw new NotSupportedException(string.Format("Binary Expression Evaluator: Syntax kind <{0}> is not supported yet.", expressionType));
		}
	}
}
