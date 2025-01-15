using System;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000D8 RID: 216
	internal class GenericActionMetadata : IActionMetadata, IXEObjectMetadata
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0001CB48 File Offset: 0x0001CB48
		// (set) Token: 0x060002DB RID: 731 RVA: 0x0001CB5C File Offset: 0x0001CB5C
		public string Name { get; internal set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0001CB70 File Offset: 0x0001CB70
		// (set) Token: 0x060002DD RID: 733 RVA: 0x0001CB84 File Offset: 0x0001CB84
		public IPackage Package { get; internal set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0001CB98 File Offset: 0x0001CB98
		// (set) Token: 0x060002DF RID: 735 RVA: 0x0001CBAC File Offset: 0x0001CBAC
		public Type Type { get; internal set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0001CBC0 File Offset: 0x0001CBC0
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x0001CBD4 File Offset: 0x0001CBD4
		public string Description { get; internal set; }
	}
}
