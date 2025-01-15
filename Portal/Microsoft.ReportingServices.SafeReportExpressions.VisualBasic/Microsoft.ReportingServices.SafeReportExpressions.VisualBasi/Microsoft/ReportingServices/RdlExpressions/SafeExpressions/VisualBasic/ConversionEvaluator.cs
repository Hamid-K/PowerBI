using System;
using System.Runtime.CompilerServices;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;
using Microsoft.VisualBasic.CompilerServices;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions.VisualBasic
{
	// Token: 0x02000007 RID: 7
	internal class ConversionEvaluator : ArithmeticEvaluator, IConversionEvaluator
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002E00 File Offset: 0x00001000
		public object Evaluate(string conversionName, object conversionValue)
		{
			object objectValue = RuntimeHelpers.GetObjectValue(base.CastToStrongType(RuntimeHelpers.GetObjectValue(conversionValue)));
			string text = conversionName.ToUpperInvariant();
			object obj;
			if (Operators.CompareString(text, "CSTR", false) != 0)
			{
				if (Operators.CompareString(text, "CDATE", false) != 0)
				{
					throw new NotSupportedException(string.Format("Conversion <{0}> is not supported yet.", conversionName));
				}
				obj = Conversions.ToDate(objectValue);
			}
			else
			{
				obj = Conversions.ToString(objectValue);
			}
			return obj;
		}
	}
}
