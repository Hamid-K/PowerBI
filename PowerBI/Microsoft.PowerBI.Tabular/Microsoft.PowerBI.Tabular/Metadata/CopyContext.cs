using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001E2 RID: 482
	internal class CopyContext
	{
		// Token: 0x06001C44 RID: 7236 RVA: 0x000C5352 File Offset: 0x000C3552
		public CopyContext(CopyFlags flags, IDictionary<ObjectId, MetadataObject> incrementalUpdateOriginalObjectMap = null)
		{
			this.Flags = flags;
			this.OriginalToCloneObjectMap = new Dictionary<MetadataObject, MetadataObject>();
			this.CopiedObjects = new List<MetadataObject>();
			this.IncrementalUpdateOriginalObjectMap = incrementalUpdateOriginalObjectMap;
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001C45 RID: 7237 RVA: 0x000C537E File Offset: 0x000C357E
		public CopyFlags Flags { get; }

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001C46 RID: 7238 RVA: 0x000C5386 File Offset: 0x000C3586
		public IDictionary<MetadataObject, MetadataObject> OriginalToCloneObjectMap { get; }

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001C47 RID: 7239 RVA: 0x000C538E File Offset: 0x000C358E
		public IList<MetadataObject> CopiedObjects { get; }

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001C48 RID: 7240 RVA: 0x000C5396 File Offset: 0x000C3596
		public IDictionary<ObjectId, MetadataObject> IncrementalUpdateOriginalObjectMap { get; }
	}
}
