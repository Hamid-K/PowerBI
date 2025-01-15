using System;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Json
{
	// Token: 0x020001B8 RID: 440
	internal sealed class JsonObjectTreeWriterSettings
	{
		// Token: 0x06001B18 RID: 6936 RVA: 0x000B8152 File Offset: 0x000B6352
		internal JsonObjectTreeWriterSettings(JsonObjectTreeWriterSettings.WriteObjectMethod writeMethod, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			this.writeMethod = writeMethod;
			this.mode = mode;
			this.dbCompatibilityLevel = dbCompatibilityLevel;
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x000B816F File Offset: 0x000B636F
		// (set) Token: 0x06001B1A RID: 6938 RVA: 0x000B8177 File Offset: 0x000B6377
		public Predicate<MetadataObject> WriteObjectFilter { get; set; }

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001B1B RID: 6939 RVA: 0x000B8180 File Offset: 0x000B6380
		// (set) Token: 0x06001B1C RID: 6940 RVA: 0x000B8188 File Offset: 0x000B6388
		public Predicate<IMetadataObjectCollection> WriteCollectionFilter { get; set; }

		// Token: 0x06001B1D RID: 6941 RVA: 0x000B8191 File Offset: 0x000B6391
		internal bool ShouldWriteObject(MetadataObject obj)
		{
			return ObjectTreeHelper.IsObjectComplientWithCompatibilityRestriction(obj.ObjectType, this.mode, this.dbCompatibilityLevel) && (this.WriteObjectFilter == null || this.WriteObjectFilter(obj));
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x000B81C4 File Offset: 0x000B63C4
		internal bool ShouldWriteCollection(IMetadataObjectCollection collection)
		{
			return ObjectTreeHelper.IsObjectComplientWithCompatibilityRestriction(collection.ItemType, this.mode, this.dbCompatibilityLevel) && (this.WriteCollectionFilter == null || this.WriteCollectionFilter(collection));
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x000B81F7 File Offset: 0x000B63F7
		internal void WriteObject(MetadataObject obj, JsonObject jsonObj)
		{
			this.writeMethod(obj, jsonObj, this.mode, this.dbCompatibilityLevel);
		}

		// Token: 0x0400052E RID: 1326
		private readonly JsonObjectTreeWriterSettings.WriteObjectMethod writeMethod;

		// Token: 0x0400052F RID: 1327
		private readonly CompatibilityMode mode;

		// Token: 0x04000530 RID: 1328
		private readonly int dbCompatibilityLevel;

		// Token: 0x0200041F RID: 1055
		// (Invoke) Token: 0x0600284C RID: 10316
		public delegate void WriteObjectMethod(MetadataObject obj, JsonObject jsonObj, CompatibilityMode mode, int dbCompatibilityLevel);
	}
}
