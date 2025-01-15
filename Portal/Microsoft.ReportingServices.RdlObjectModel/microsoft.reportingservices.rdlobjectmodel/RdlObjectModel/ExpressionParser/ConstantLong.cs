using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200026C RID: 620
	[Serializable]
	internal sealed class ConstantLong : Constant
	{
		// Token: 0x060013E4 RID: 5092 RVA: 0x0002F62B File Offset: 0x0002D82B
		public ConstantLong(string value)
			: base(long.Parse(value, RDLUtil.GetFormatProvider(false)))
		{
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0002F644 File Offset: 0x0002D844
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Int64;
		}
	}
}
