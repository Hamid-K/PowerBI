using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200026A RID: 618
	[Serializable]
	internal sealed class ConstantDouble : Constant
	{
		// Token: 0x060013DE RID: 5086 RVA: 0x0002F5D5 File Offset: 0x0002D7D5
		public ConstantDouble(string value)
			: base(double.Parse(value, RDLUtil.GetFormatProvider(false)))
		{
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0002F5EE File Offset: 0x0002D7EE
		public ConstantDouble(double value)
			: base(value)
		{
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0002F5FC File Offset: 0x0002D7FC
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Double;
		}
	}
}
