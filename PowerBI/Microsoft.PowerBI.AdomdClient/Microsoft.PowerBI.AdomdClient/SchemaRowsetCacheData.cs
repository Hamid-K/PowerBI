using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000EE RID: 238
	internal sealed class SchemaRowsetCacheData
	{
		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0002F907 File Offset: 0x0002DB07
		internal string RequestType
		{
			get
			{
				return this.requestType;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0002F90F File Offset: 0x0002DB0F
		internal string UniqueNameColumnName
		{
			get
			{
				return this.uniqueNameColumnName;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0002F917 File Offset: 0x0002DB17
		internal string RelationColumnName
		{
			get
			{
				return this.relationColumnName;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0002F91F File Offset: 0x0002DB1F
		internal string[] PrimaryKeyColumns
		{
			get
			{
				return this.primaryKeyColumns;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0002F927 File Offset: 0x0002DB27
		internal InternalObjectType ObjectType
		{
			get
			{
				return this.objectType;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0002F92F File Offset: 0x0002DB2F
		internal KeyValuePair<string, string>[] AdditionalStaticRestrictions
		{
			get
			{
				return this.additionalStaticRestrictions;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0002F937 File Offset: 0x0002DB37
		internal bool HasAdditionalRestrictions
		{
			get
			{
				return this.additionalStaticRestrictions != null;
			}
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0002F942 File Offset: 0x0002DB42
		internal SchemaRowsetCacheData(InternalObjectType objectType, string requestType, string relationColumnName, string[] primaryKeyColumns, string uniqueNameColumnName)
			: this(objectType, requestType, relationColumnName, primaryKeyColumns, uniqueNameColumnName, null)
		{
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0002F952 File Offset: 0x0002DB52
		internal SchemaRowsetCacheData(InternalObjectType objectType, string requestType, string relationColumnName, string[] primaryKeyColumns, string uniqueNameColumnName, KeyValuePair<string, string>[] additionalStaticRestrictions)
		{
			this.objectType = objectType;
			this.requestType = requestType;
			this.relationColumnName = relationColumnName;
			this.primaryKeyColumns = primaryKeyColumns;
			this.uniqueNameColumnName = uniqueNameColumnName;
			this.additionalStaticRestrictions = additionalStaticRestrictions;
		}

		// Token: 0x04000831 RID: 2097
		private string requestType;

		// Token: 0x04000832 RID: 2098
		private string relationColumnName;

		// Token: 0x04000833 RID: 2099
		private string uniqueNameColumnName;

		// Token: 0x04000834 RID: 2100
		private string[] primaryKeyColumns;

		// Token: 0x04000835 RID: 2101
		private InternalObjectType objectType;

		// Token: 0x04000836 RID: 2102
		private KeyValuePair<string, string>[] additionalStaticRestrictions;
	}
}
