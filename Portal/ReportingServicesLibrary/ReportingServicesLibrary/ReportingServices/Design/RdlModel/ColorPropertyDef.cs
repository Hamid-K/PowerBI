using System;
using System.Drawing;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003D4 RID: 980
	public class ColorPropertyDef : PropertyDef
	{
		// Token: 0x06001F57 RID: 8023 RVA: 0x0007E42A File Offset: 0x0007C62A
		public ColorPropertyDef(string name, Color defaultValue)
			: base(name)
		{
			this.Default = defaultValue;
		}

		// Token: 0x04000DB5 RID: 3509
		public readonly Color Default;
	}
}
