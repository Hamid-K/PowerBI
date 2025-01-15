using System;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Json
{
	// Token: 0x020001B6 RID: 438
	internal sealed class JsonObjectTreeSchemaWriterSettings
	{
		// Token: 0x06001AE2 RID: 6882 RVA: 0x000B4DB2 File Offset: 0x000B2FB2
		internal JsonObjectTreeSchemaWriterSettings(JsonObjectTreeSchemaWriterSettings.WriteObjectPropertiesMethod writeMethod, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			this.writeMethod = writeMethod;
			this.mode = mode;
			this.dbCompatibilityLevel = dbCompatibilityLevel;
			this.options = options;
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x000B4DD7 File Offset: 0x000B2FD7
		// (set) Token: 0x06001AE4 RID: 6884 RVA: 0x000B4DDF File Offset: 0x000B2FDF
		public Predicate<ObjectType> WriteObjectFilter { get; set; }

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x000B4DE8 File Offset: 0x000B2FE8
		// (set) Token: 0x06001AE6 RID: 6886 RVA: 0x000B4DF0 File Offset: 0x000B2FF0
		public Predicate<ObjectType> WriteCollectionFilter { get; set; }

		// Token: 0x06001AE7 RID: 6887 RVA: 0x000B4DF9 File Offset: 0x000B2FF9
		internal bool ShouldWriteObject(ObjectType type)
		{
			return ObjectTreeHelper.IsObjectComplientWithCompatibilityRestriction(type, this.mode, this.dbCompatibilityLevel) && (this.WriteObjectFilter == null || this.WriteObjectFilter(type));
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x000B4E27 File Offset: 0x000B3027
		internal bool ShouldWriteCollection(ObjectType type)
		{
			return ObjectTreeHelper.IsObjectComplientWithCompatibilityRestriction(type, this.mode, this.dbCompatibilityLevel) && (this.WriteCollectionFilter == null || this.WriteCollectionFilter(type));
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x000B4E55 File Offset: 0x000B3055
		internal void WriteObjectProperties(JsonWriter writer, ObjectType type)
		{
			this.writeMethod(writer, type, this.options, this.mode, this.dbCompatibilityLevel);
		}

		// Token: 0x04000528 RID: 1320
		private readonly JsonObjectTreeSchemaWriterSettings.WriteObjectPropertiesMethod writeMethod;

		// Token: 0x04000529 RID: 1321
		private readonly CompatibilityMode mode;

		// Token: 0x0400052A RID: 1322
		private readonly int dbCompatibilityLevel;

		// Token: 0x0400052B RID: 1323
		private readonly SerializeOptions options;

		// Token: 0x020003FB RID: 1019
		// (Invoke) Token: 0x06002757 RID: 10071
		public delegate void WriteObjectPropertiesMethod(JsonWriter writer, ObjectType type, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel);
	}
}
