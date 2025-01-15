using System;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000013 RID: 19
	public sealed class AtomGeneratorMetadata
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002E13 File Offset: 0x00001013
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00002E1B File Offset: 0x0000101B
		public string Name { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002E24 File Offset: 0x00001024
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00002E2C File Offset: 0x0000102C
		public Uri Uri { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002E35 File Offset: 0x00001035
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00002E3D File Offset: 0x0000103D
		public string Version { get; set; }
	}
}
