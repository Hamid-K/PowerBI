using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200026B RID: 619
	[Serializable]
	internal sealed class ConstantInteger : Constant
	{
		// Token: 0x060013E1 RID: 5089 RVA: 0x0002F600 File Offset: 0x0002D800
		public ConstantInteger(string value)
			: base(int.Parse(value, RDLUtil.GetFormatProvider(false)))
		{
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0002F619 File Offset: 0x0002D819
		public ConstantInteger(int value)
			: base(value)
		{
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0002F627 File Offset: 0x0002D827
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Int32;
		}
	}
}
