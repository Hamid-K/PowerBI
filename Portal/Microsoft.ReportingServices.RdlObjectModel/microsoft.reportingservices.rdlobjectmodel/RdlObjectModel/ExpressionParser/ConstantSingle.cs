using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200026F RID: 623
	[Serializable]
	internal sealed class ConstantSingle : Constant
	{
		// Token: 0x060013E9 RID: 5097 RVA: 0x0002F66D File Offset: 0x0002D86D
		public ConstantSingle(string value)
			: base(float.Parse(value, RDLUtil.GetFormatProvider(false)))
		{
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0002F686 File Offset: 0x0002D886
		public ConstantSingle(float value)
			: base(value)
		{
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0002F694 File Offset: 0x0002D894
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Single;
		}
	}
}
