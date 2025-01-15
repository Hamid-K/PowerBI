using System;
using System.Collections.ObjectModel;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000D7 RID: 215
	internal class GenericEventMetadata : IEventMetadata, IXEObjectMetadata
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0001CA44 File Offset: 0x0001CA44
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0001CA58 File Offset: 0x0001CA58
		public string Name { get; internal set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0001CA6C File Offset: 0x0001CA6C
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0001CA80 File Offset: 0x0001CA80
		public Guid UUID { get; internal set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0001CA94 File Offset: 0x0001CA94
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0001CAA8 File Offset: 0x0001CAA8
		public int Version { get; internal set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0001CABC File Offset: 0x0001CABC
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0001CAD0 File Offset: 0x0001CAD0
		public ReadOnlyCollection<IEventFieldMetadata> Fields { get; internal set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0001CAE4 File Offset: 0x0001CAE4
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0001CAF8 File Offset: 0x0001CAF8
		public IPackage Package { get; internal set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0001CB0C File Offset: 0x0001CB0C
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0001CB20 File Offset: 0x0001CB20
		public string Description { get; internal set; }
	}
}
