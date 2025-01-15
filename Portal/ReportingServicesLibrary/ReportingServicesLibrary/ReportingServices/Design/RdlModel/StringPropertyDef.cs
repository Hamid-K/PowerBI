using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003D2 RID: 978
	public class StringPropertyDef : PropertyDef
	{
		// Token: 0x06001F55 RID: 8021 RVA: 0x0007E403 File Offset: 0x0007C603
		public StringPropertyDef(string name, string defaultValue)
			: base(name)
		{
			this.Default = defaultValue;
		}

		// Token: 0x04000DB2 RID: 3506
		public readonly string Default;
	}
}
