using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200026E RID: 622
	[Serializable]
	internal sealed class ConstantShort : Constant
	{
		// Token: 0x060013E7 RID: 5095 RVA: 0x0002F651 File Offset: 0x0002D851
		public ConstantShort(string value)
			: base(short.Parse(value, RDLUtil.GetFormatProvider(false)))
		{
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0002F66A File Offset: 0x0002D86A
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Int16;
		}
	}
}
