using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x02000208 RID: 520
	internal class ServerImpact
	{
		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001D8A RID: 7562 RVA: 0x000C8C67 File Offset: 0x000C6E67
		// (set) Token: 0x06001D8B RID: 7563 RVA: 0x000C8C6F File Offset: 0x000C6E6F
		public bool IsFullModel
		{
			get
			{
				return this.isFullModel;
			}
			internal set
			{
				this.isFullModel = value;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001D8C RID: 7564 RVA: 0x000C8C78 File Offset: 0x000C6E78
		// (set) Token: 0x06001D8D RID: 7565 RVA: 0x000C8C80 File Offset: 0x000C6E80
		public long ModelVersion
		{
			get
			{
				return this.modelVersion;
			}
			internal set
			{
				this.modelVersion = value;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x000C8C89 File Offset: 0x000C6E89
		public IEnumerable<ObjectId> DeletedObjects
		{
			get
			{
				return this.deletedObjects;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001D8F RID: 7567 RVA: 0x000C8C91 File Offset: 0x000C6E91
		public IEnumerable<MetadataObject> AffectedObjects
		{
			get
			{
				return this.affectedObjects;
			}
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x000C8C99 File Offset: 0x000C6E99
		internal void AddDeletedObject(ObjectId id)
		{
			this.deletedObjects.Add(id);
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x000C8CA7 File Offset: 0x000C6EA7
		internal void AddAffectedObject(MetadataObject @object)
		{
			this.affectedObjects.Add(@object);
		}

		// Token: 0x040006CC RID: 1740
		private List<ObjectId> deletedObjects = new List<ObjectId>();

		// Token: 0x040006CD RID: 1741
		private List<MetadataObject> affectedObjects = new List<MetadataObject>();

		// Token: 0x040006CE RID: 1742
		private bool isFullModel;

		// Token: 0x040006CF RID: 1743
		private long modelVersion;
	}
}
