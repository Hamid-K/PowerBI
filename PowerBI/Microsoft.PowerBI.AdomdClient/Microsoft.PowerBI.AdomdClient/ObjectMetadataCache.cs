using System;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000DE RID: 222
	internal class ObjectMetadataCache : IObjectCache
	{
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x0002E46B File Offset: 0x0002C66B
		internal bool IsInitialized
		{
			get
			{
				return this.isInitialized;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0002E473 File Offset: 0x0002C673
		internal bool IsPopulated
		{
			get
			{
				return this.cacheState > MetadataCacheState.Empty;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x0002E47E File Offset: 0x0002C67E
		internal string RelationColumn
		{
			get
			{
				return this.relationColumn;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x0002E486 File Offset: 0x0002C686
		// (set) Token: 0x06000C48 RID: 3144 RVA: 0x0002E48E File Offset: 0x0002C68E
		internal DataRelation Relation
		{
			get
			{
				return this.relation;
			}
			set
			{
				this.relation = value;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0002E497 File Offset: 0x0002C697
		internal DataTable CacheTable
		{
			get
			{
				return this.cacheTable;
			}
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0002E49F File Offset: 0x0002C69F
		internal ObjectMetadataCache(AdomdConnection connection, InternalObjectType objectType, string requestType, ListDictionary restrictions)
			: this(connection, objectType, requestType, restrictions, false)
		{
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0002E4AD File Offset: 0x0002C6AD
		internal ObjectMetadataCache(AdomdConnection connection, InternalObjectType objectType, string requestType, ListDictionary restrictions, bool isNestedSchema)
			: this(null, connection, objectType, requestType, restrictions, null)
		{
			this.isNestedSchema = isNestedSchema;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0002E4C4 File Offset: 0x0002C6C4
		internal ObjectMetadataCache(IMetadataCache metadataCache, AdomdConnection connection, SchemaRowsetCacheData rowsetData, ListDictionary restrictions)
			: this(metadataCache, connection, (rowsetData == null) ? InternalObjectType.InternalTypeMemberProperty : rowsetData.ObjectType, (rowsetData == null) ? null : rowsetData.RequestType, restrictions, (rowsetData == null) ? null : rowsetData.RelationColumnName)
		{
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0002E4F8 File Offset: 0x0002C6F8
		internal ObjectMetadataCache(IMetadataCache metadataCache, AdomdConnection connection, InternalObjectType objectType, string requestType, ListDictionary restrictions, string relationColumn)
		{
			this.msgDelegate = new AdomdUtils.GetInvalidatedMessageDelegate(this.GetMessageForCacheExpiration);
			this.isInitialized = false;
			this.cacheState = MetadataCacheState.Empty;
			this.metadataCache = metadataCache;
			this.connection = connection;
			this.objectType = objectType;
			this.requestType = requestType;
			this.restrictions = restrictions;
			this.relationColumn = relationColumn;
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0002E558 File Offset: 0x0002C758
		internal void PopulateSelf()
		{
			if (this.connection == null)
			{
				throw new NotSupportedException(SR.NotSupportedWhenConnectionMissing);
			}
			AdomdUtils.CheckConnectionOpened(this.connection);
			if (!this.IsInitialized)
			{
				this.cacheTable = new DataTable();
				this.cacheTable.Locale = CultureInfo.InvariantCulture;
				this.cacheTable.Columns.Add("ObjectColumn_ADOMDInternal$$", typeof(object));
			}
			if (this.isNestedSchema)
			{
				ObjectMetadataCache.DiscoverNested(this.connection, this.requestType, this.restrictions, out this.cacheDataSet);
				this.cacheTable.Columns.Add("__RowIndex__", typeof(int));
				object[] array = new object[2];
				for (int i = 0; i < this.cacheDataSet.Tables[0].Rows.Count; i++)
				{
					array[0] = null;
					array[1] = i;
					this.cacheTable.Rows.Add(array);
				}
			}
			else
			{
				ObjectMetadataCache.Discover(this.connection, this.requestType, this.restrictions, this.cacheTable, !this.isInitialized);
			}
			this.cacheState = MetadataCacheState.UpToDate;
			this.isInitialized = true;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0002E690 File Offset: 0x0002C890
		internal void RefreshSelf()
		{
			if (this.cacheState != MetadataCacheState.Empty)
			{
				if (this.cacheTable != null)
				{
					DataTable dataTable = this.cacheTable.Clone();
					this.cacheTable = dataTable;
				}
				this.cacheState = MetadataCacheState.Empty;
			}
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0002E6C7 File Offset: 0x0002C8C7
		internal void MarkAbandonedSelf()
		{
			this.cacheState = MetadataCacheState.Abandoned;
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0002E6D0 File Offset: 0x0002C8D0
		internal static void Discover(AdomdConnection connection, string requestType, ListDictionary restrictions, DataTable destinationTable, bool doCreate)
		{
			if (!doCreate)
			{
				connection.IDiscoverProvider.DiscoverData(requestType, restrictions, destinationTable);
				return;
			}
			connection.IDiscoverProvider.Discover(requestType, restrictions, destinationTable);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002E6F4 File Offset: 0x0002C8F4
		internal static void DiscoverNested(AdomdConnection connection, string requestType, ListDictionary restrictions, out DataSet destinationDS)
		{
			RowsetFormatter rowsetFormatter = connection.IDiscoverProvider.Discover(requestType, restrictions);
			destinationDS = rowsetFormatter.RowsetDataset;
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x0002E717 File Offset: 0x0002C917
		bool IObjectCache.IsPopulated
		{
			get
			{
				return this.IsPopulated;
			}
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0002E71F File Offset: 0x0002C91F
		void IObjectCache.Populate()
		{
			if (!this.IsPopulated)
			{
				if (this.metadataCache != null)
				{
					this.metadataCache.Populate(this.objectType);
					return;
				}
				this.EnsureNotAbandoned();
				this.EnsureValid();
				this.PopulateSelf();
			}
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0002E755 File Offset: 0x0002C955
		void IObjectCache.Refresh()
		{
			if (this.IsPopulated)
			{
				if (this.metadataCache != null)
				{
					this.metadataCache.Refresh(this.objectType);
					return;
				}
				this.EnsureNotAbandoned();
				this.EnsureValid();
				this.RefreshSelf();
			}
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0002E78B File Offset: 0x0002C98B
		DataRowCollection IObjectCache.GetNonFilteredRows()
		{
			return this.cacheTable.Rows;
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0002E798 File Offset: 0x0002C998
		DataRow[] IObjectCache.GetFilteredRows(DataRow parentRow, string filter)
		{
			if (filter == null && this.Relation != null && parentRow != null)
			{
				return parentRow.GetChildRows(this.Relation);
			}
			return this.GetRows(parentRow, filter);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0002E7C0 File Offset: 0x0002C9C0
		void IObjectCache.CheckCacheIsValid()
		{
			if (this.IsPopulated)
			{
				if (this.metadataCache != null)
				{
					this.metadataCache.CheckCacheIsValid();
					return;
				}
				this.EnsureNotAbandoned();
				this.EnsureValid();
				if (this.NeedCheckForRefresh())
				{
					bool flag = true;
					if (this.cacheValidationTable == null)
					{
						this.cacheValidationTable = this.cacheTable.Clone();
						this.cacheValidationColumnNames = ObjectMetadataCache.GetColumnNamesToCompareForUpdate(this.objectType);
					}
					else
					{
						this.cacheValidationTable.Clear();
					}
					this.Discover(this.cacheValidationTable, false);
					if (this.cacheTable.Rows.Count != this.cacheValidationTable.Rows.Count)
					{
						flag = false;
					}
					else if (this.cacheValidationColumnNames != null)
					{
						for (int i = 0; i < this.cacheValidationTable.Rows.Count; i++)
						{
							DataRow dataRow = this.cacheTable.Rows[i];
							foreach (string text in this.cacheValidationColumnNames)
							{
								if (!object.Equals(dataRow[text], this.cacheValidationTable.Rows[i][text]))
								{
									flag = false;
									break;
								}
							}
							if (!flag)
							{
								break;
							}
						}
					}
					this.lastCacheValidationTime = DateTime.Now;
					if (!flag)
					{
						this.cacheState = MetadataCacheState.Invalid;
					}
					else
					{
						this.cacheState = MetadataCacheState.UpToDate;
					}
					this.EnsureValid();
				}
			}
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0002E918 File Offset: 0x0002CB18
		void IObjectCache.MarkNeedCheckForValidness()
		{
			if (this.metadataCache != null)
			{
				this.metadataCache.MarkNeedCheckForValidness();
				return;
			}
			if (this.cacheState == MetadataCacheState.UpToDate)
			{
				this.cacheState = MetadataCacheState.NeedsValidnessCheck;
			}
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002E93E File Offset: 0x0002CB3E
		void IObjectCache.MarkAbandoned()
		{
			if (this.metadataCache != null)
			{
				this.metadataCache.MarkAbandoned();
				return;
			}
			this.cacheState = MetadataCacheState.Abandoned;
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x0002E95B File Offset: 0x0002CB5B
		DataSet IObjectCache.CacheDataSet
		{
			get
			{
				return this.cacheDataSet;
			}
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0002E963 File Offset: 0x0002CB63
		private void EnsureValid()
		{
			AdomdUtils.EnsureCacheNotInvalid(this.cacheState, this.msgDelegate);
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0002E976 File Offset: 0x0002CB76
		private void EnsureNotAbandoned()
		{
			AdomdUtils.EnsureCacheNotAbandoned(this.cacheState);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0002E983 File Offset: 0x0002CB83
		private void Discover(DataTable destinationTable, bool doCreate)
		{
			ObjectMetadataCache.Discover(this.connection, this.requestType, this.restrictions, destinationTable, doCreate);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0002E9A0 File Offset: 0x0002CBA0
		private DataRow[] GetRows(DataRow parentRow, string filter)
		{
			string text;
			if (parentRow != null)
			{
				text = AdomdUtils.GetDataTableFilter(this.relationColumn, parentRow[this.relationColumn].ToString());
				if (filter != null)
				{
					text = string.Concat(new string[] { "(", text, " and ", filter, ")" });
				}
			}
			else
			{
				text = filter;
			}
			return this.cacheTable.Select(text);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0002EA0C File Offset: 0x0002CC0C
		private bool NeedCheckForRefresh()
		{
			return this.cacheState == MetadataCacheState.NeedsValidnessCheck || this.connection.HasAutoSyncTimeElapsed(this.lastCacheValidationTime, DateTime.Now);
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0002EA2F File Offset: 0x0002CC2F
		private static string[] GetColumnNamesToCompareForUpdate(InternalObjectType objectType)
		{
			if (objectType == InternalObjectType.InternalTypeCube)
			{
				return ObjectMetadataCache.columnNamesForCubeValidation;
			}
			return null;
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002EA40 File Offset: 0x0002CC40
		private string GetMessageForCacheExpiration()
		{
			InternalObjectType internalObjectType = this.objectType;
			if (internalObjectType == InternalObjectType.InternalTypeMiningService)
			{
				return SR.Metadata_MiningServicesCollectionHasbeenUpdated;
			}
			if (internalObjectType == InternalObjectType.InternalTypeCube)
			{
				return SR.Metadata_CubesCollectionHasbeenUpdated;
			}
			return null;
		}

		// Token: 0x040007E1 RID: 2017
		private DataTable cacheTable;

		// Token: 0x040007E2 RID: 2018
		private DateTime lastCacheValidationTime;

		// Token: 0x040007E3 RID: 2019
		private DataTable cacheValidationTable;

		// Token: 0x040007E4 RID: 2020
		private string[] cacheValidationColumnNames;

		// Token: 0x040007E5 RID: 2021
		private static readonly string[] columnNamesForCubeValidation = new string[]
		{
			CubeDef.cubeNameColumn,
			CubeDef.lastSchemaUpdateColumn
		};

		// Token: 0x040007E6 RID: 2022
		private const string objectColumn = "ObjectColumn_ADOMDInternal$$";

		// Token: 0x040007E7 RID: 2023
		private bool isInitialized;

		// Token: 0x040007E8 RID: 2024
		private MetadataCacheState cacheState;

		// Token: 0x040007E9 RID: 2025
		private string requestType;

		// Token: 0x040007EA RID: 2026
		private IMetadataCache metadataCache;

		// Token: 0x040007EB RID: 2027
		private AdomdConnection connection;

		// Token: 0x040007EC RID: 2028
		private ListDictionary restrictions;

		// Token: 0x040007ED RID: 2029
		private string relationColumn;

		// Token: 0x040007EE RID: 2030
		private DataRelation relation;

		// Token: 0x040007EF RID: 2031
		private InternalObjectType objectType;

		// Token: 0x040007F0 RID: 2032
		private bool isNestedSchema;

		// Token: 0x040007F1 RID: 2033
		private DataSet cacheDataSet;

		// Token: 0x040007F2 RID: 2034
		private AdomdUtils.GetInvalidatedMessageDelegate msgDelegate;
	}
}
