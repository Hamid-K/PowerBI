using System;
using System.Collections;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003D3 RID: 979
	public class EnumPropertyDef : PropertyDef
	{
		// Token: 0x06001F56 RID: 8022 RVA: 0x0007E413 File Offset: 0x0007C613
		public EnumPropertyDef(string name, string defaultValue, IList values)
			: base(name)
		{
			this.Default = defaultValue;
			this.Values = values;
		}

		// Token: 0x04000DB3 RID: 3507
		public readonly string Default;

		// Token: 0x04000DB4 RID: 3508
		public readonly IList Values;
	}
}
