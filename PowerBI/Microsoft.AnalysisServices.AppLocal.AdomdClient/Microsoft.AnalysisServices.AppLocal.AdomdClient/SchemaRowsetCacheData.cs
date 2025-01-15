using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000EE RID: 238
	internal sealed class SchemaRowsetCacheData
	{
		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0002FC37 File Offset: 0x0002DE37
		internal string RequestType
		{
			get
			{
				return this.requestType;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0002FC3F File Offset: 0x0002DE3F
		internal string UniqueNameColumnName
		{
			get
			{
				return this.uniqueNameColumnName;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x0002FC47 File Offset: 0x0002DE47
		internal string RelationColumnName
		{
			get
			{
				return this.relationColumnName;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0002FC4F File Offset: 0x0002DE4F
		internal string[] PrimaryKeyColumns
		{
			get
			{
				return this.primaryKeyColumns;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0002FC57 File Offset: 0x0002DE57
		internal InternalObjectType ObjectType
		{
			get
			{
				return this.objectType;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x0002FC5F File Offset: 0x0002DE5F
		internal KeyValuePair<string, string>[] AdditionalStaticRestrictions
		{
			get
			{
				return this.additionalStaticRestrictions;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0002FC67 File Offset: 0x0002DE67
		internal bool HasAdditionalRestrictions
		{
			get
			{
				return this.additionalStaticRestrictions != null;
			}
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0002FC72 File Offset: 0x0002DE72
		internal SchemaRowsetCacheData(InternalObjectType objectType, string requestType, string relationColumnName, string[] primaryKeyColumns, string uniqueNameColumnName)
			: this(objectType, requestType, relationColumnName, primaryKeyColumns, uniqueNameColumnName, null)
		{
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0002FC82 File Offset: 0x0002DE82
		internal SchemaRowsetCacheData(InternalObjectType objectType, string requestType, string relationColumnName, string[] primaryKeyColumns, string uniqueNameColumnName, KeyValuePair<string, string>[] additionalStaticRestrictions)
		{
			this.objectType = objectType;
			this.requestType = requestType;
			this.relationColumnName = relationColumnName;
			this.primaryKeyColumns = primaryKeyColumns;
			this.uniqueNameColumnName = uniqueNameColumnName;
			this.additionalStaticRestrictions = additionalStaticRestrictions;
		}

		// Token: 0x0400083E RID: 2110
		private string requestType;

		// Token: 0x0400083F RID: 2111
		private string relationColumnName;

		// Token: 0x04000840 RID: 2112
		private string uniqueNameColumnName;

		// Token: 0x04000841 RID: 2113
		private string[] primaryKeyColumns;

		// Token: 0x04000842 RID: 2114
		private InternalObjectType objectType;

		// Token: 0x04000843 RID: 2115
		private KeyValuePair<string, string>[] additionalStaticRestrictions;
	}
}
