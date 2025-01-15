using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000269 RID: 617
	[Serializable]
	internal sealed class ConstantDecimal : Constant
	{
		// Token: 0x060013DC RID: 5084 RVA: 0x0002F5B8 File Offset: 0x0002D7B8
		public ConstantDecimal(string value)
			: base(decimal.Parse(value, RDLUtil.GetFormatProvider(false)))
		{
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0002F5D1 File Offset: 0x0002D7D1
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Decimal;
		}
	}
}
