using System;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000266 RID: 614
	[Serializable]
	internal sealed class ConstantBoolean : Constant
	{
		// Token: 0x060013D1 RID: 5073 RVA: 0x0002F505 File Offset: 0x0002D705
		public ConstantBoolean(string value)
			: base(Convert.ToBoolean(value, CultureInfo.InvariantCulture))
		{
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0002F51D File Offset: 0x0002D71D
		public ConstantBoolean(bool value)
			: base(value)
		{
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0002F52B File Offset: 0x0002D72B
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Boolean;
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0002F52E File Offset: 0x0002D72E
		public override string WriteSource(NameChanges nameChanges)
		{
			if (!(bool)base.Evaluate())
			{
				return "False";
			}
			return "True";
		}
	}
}
