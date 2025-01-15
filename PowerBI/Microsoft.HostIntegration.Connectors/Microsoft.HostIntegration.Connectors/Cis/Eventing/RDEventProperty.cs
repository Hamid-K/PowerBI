using System;

namespace Microsoft.Cis.Eventing
{
	// Token: 0x0200048C RID: 1164
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class RDEventProperty : Attribute
	{
		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x0600287A RID: 10362 RVA: 0x0007A083 File Offset: 0x00078283
		// (set) Token: 0x0600287B RID: 10363 RVA: 0x0007A08B File Offset: 0x0007828B
		public bool ExcludeFromConsole { get; set; }
	}
}
