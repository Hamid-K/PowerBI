using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000268 RID: 616
	[Serializable]
	internal sealed class ConstantDateTime : Constant
	{
		// Token: 0x060013D9 RID: 5081 RVA: 0x0002F58E File Offset: 0x0002D78E
		public ConstantDateTime(string value)
			: base(Convert.ToDateTime(value, CultureInfo.InvariantCulture))
		{
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0002F5A6 File Offset: 0x0002D7A6
		public ConstantDateTime(DateTime value)
			: base(value)
		{
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0002F5B4 File Offset: 0x0002D7B4
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.DateTime;
		}
	}
}
