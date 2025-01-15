using System;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000DE RID: 222
	internal class ObjectMetadataCache : IObjectCache
	{
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x0002E79B File Offset: 0x0002C99B
		internal bool IsInitialized
		{
			get
			{
				return this.isInitialized;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x0002E7A3 File Offset: 0x0002C9A3
		internal bool IsPopulated
		{
			get
			{
				return this.cacheState > MetadataCacheState.Empty;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x0002E7AE File Offset: 0x0002C9AE
		internal string RelationColumn
		{
			get
			{
				return this.relationColumn;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x0002E7B6 File Offset: 0x0002C9B6
		// (set) Token: 0x06000C55 RID: 3157 RVA: 0x0002E7BE File Offset: 0x0002C9BE
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

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x0002E7C7 File Offset: 0x0002C9C7
		internal DataTable CacheTable
		{
			get
			{
				return this.cacheTable;
			}
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0002E7CF File Offset: 0x0002C9CF
		internal ObjectMetadataCache(AdomdConnection connection, InternalObjectType objectType, string requestType, ListDictionary restrictions)
			: this(connection, objectType, requestType, restrictions, false)
		{
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0002E7DD File Offset: 0x0002C9DD
		internal ObjectMetadataCache(AdomdConnection connection, InternalObjectType objectType, string requestType, ListDictionary restrictions, bool isNestedSchema)
			: this(null, connection, objectType, requestType, restrictions, null)
		{
			this.isNestedSchema = isNestedSchema;
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0002E7F4 File Offset: 0x0002C9F4
		internal ObjectMetadataCache(IMetadataCache metadataCache, AdomdConnection connection, SchemaRowsetCacheData rowsetData, ListDictionary restrictions)
			: this(metadataCache, connection, (rowsetData == null) ? InternalObjectType.InternalTypeMemberProperty : rowsetData.ObjectType, (rowsetData == null) ? null : rowsetData.RequestType, restrictions, (rowsetData == null) ? null : rowsetData.RelationColumnName)
		{
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002E828 File Offset: 0x0002CA28
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

		// Token: 0x06000C5B RID: 3163 RVA: 0x0002E888 File Offset: 0x0002CA88
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

		// Token: 0x06000C5C RID: 3164 RVA: 0x0002E9C0 File Offset: 0x0002CBC0
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

		// Token: 0x06000C5D RID: 3165 RVA: 0x0002E9F7 File Offset: 0x0002CBF7
		internal void MarkAbandonedSelf()
		{
			this.cacheState = MetadataCacheState.Abandoned;
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0002EA00 File Offset: 0x0002CC00
		internal static void Discover(AdomdConnection connection, string requestType, ListDictionary restrictions, DataTable destinationTable, bool doCreate)
		{
			if (!doCreate)
			{
				connection.IDiscoverProvider.DiscoverData(requestType, restrictions, destinationTable);
				return;
			}
			connection.IDiscoverProvider.Discover(requestType, restrictions, destinationTable);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0002EA24 File Offset: 0x0002CC24
		internal static void DiscoverNested(AdomdConnection connection, string requestType, ListDictionary restrictions, out DataSet destinationDS)
		{
			RowsetFormatter rowsetFormatter = connection.IDiscoverProvider.Discover(requestType, restrictions);
			destinationDS = rowsetFormatter.RowsetDataset;
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0002EA47 File Offset: 0x0002CC47
		bool IObjectCache.IsPopulated
		{
			get
			{
				return this.IsPopulated;
			}
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0002EA4F File Offset: 0x0002CC4F
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

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002EA85 File Offset: 0x0002CC85
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

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002EABB File Offset: 0x0002CCBB
		DataRowCollection IObjectCache.GetNonFilteredRows()
		{
			return this.cacheTable.Rows;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0002EAC8 File Offset: 0x0002CCC8
		DataRow[] IObjectCache.GetFilteredRows(DataRow parentRow, string filter)
		{
			if (filter == null && this.Relation != null && parentRow != null)
			{
				return parentRow.GetChildRows(this.Relation);
			}
			return this.GetRows(parentRow, filter);
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002EAF0 File Offset: 0x0002CCF0
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

		// Token: 0x06000C66 RID: 3174 RVA: 0x0002EC48 File Offset: 0x0002CE48
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

		// Token: 0x06000C67 RID: 3175 RVA: 0x0002EC6E File Offset: 0x0002CE6E
		void IObjectCache.MarkAbandoned()
		{
			if (this.metadataCache != null)
			{
				this.metadataCache.MarkAbandoned();
				return;
			}
			this.cacheState = MetadataCacheState.Abandoned;
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x0002EC8B File Offset: 0x0002CE8B
		DataSet IObjectCache.CacheDataSet
		{
			get
			{
				return this.cacheDataSet;
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0002EC93 File Offset: 0x0002CE93
		private void EnsureValid()
		{
			AdomdUtils.EnsureCacheNotInvalid(this.cacheState, this.msgDelegate);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002ECA6 File Offset: 0x0002CEA6
		private void EnsureNotAbandoned()
		{
			AdomdUtils.EnsureCacheNotAbandoned(this.cacheState);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0002ECB3 File Offset: 0x0002CEB3
		private void Discover(DataTable destinationTable, bool doCreate)
		{
			ObjectMetadataCache.Discover(this.connection, this.requestType, this.restrictions, destinationTable, doCreate);
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0002ECD0 File Offset: 0x0002CED0
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

		// Token: 0x06000C6D RID: 3181 RVA: 0x0002ED3C File Offset: 0x0002CF3C
		private bool NeedCheckForRefresh()
		{
			return this.cacheState == MetadataCacheState.NeedsValidnessCheck || this.connection.HasAutoSyncTimeElapsed(this.lastCacheValidationTime, DateTime.Now);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0002ED5F File Offset: 0x0002CF5F
		private static string[] GetColumnNamesToCompareForUpdate(InternalObjectType objectType)
		{
			if (objectType == InternalObjectType.InternalTypeCube)
			{
				return ObjectMetadataCache.columnNamesForCubeValidation;
			}
			return null;
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0002ED70 File Offset: 0x0002CF70
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

		// Token: 0x040007EE RID: 2030
		private DataTable cacheTable;

		// Token: 0x040007EF RID: 2031
		private DateTime lastCacheValidationTime;

		// Token: 0x040007F0 RID: 2032
		private DataTable cacheValidationTable;

		// Token: 0x040007F1 RID: 2033
		private string[] cacheValidationColumnNames;

		// Token: 0x040007F2 RID: 2034
		private static readonly string[] columnNamesForCubeValidation = new string[]
		{
			CubeDef.cubeNameColumn,
			CubeDef.lastSchemaUpdateColumn
		};

		// Token: 0x040007F3 RID: 2035
		private const string objectColumn = "ObjectColumn_ADOMDInternal$$";

		// Token: 0x040007F4 RID: 2036
		private bool isInitialized;

		// Token: 0x040007F5 RID: 2037
		private MetadataCacheState cacheState;

		// Token: 0x040007F6 RID: 2038
		private string requestType;

		// Token: 0x040007F7 RID: 2039
		private IMetadataCache metadataCache;

		// Token: 0x040007F8 RID: 2040
		private AdomdConnection connection;

		// Token: 0x040007F9 RID: 2041
		private ListDictionary restrictions;

		// Token: 0x040007FA RID: 2042
		private string relationColumn;

		// Token: 0x040007FB RID: 2043
		private DataRelation relation;

		// Token: 0x040007FC RID: 2044
		private InternalObjectType objectType;

		// Token: 0x040007FD RID: 2045
		private bool isNestedSchema;

		// Token: 0x040007FE RID: 2046
		private DataSet cacheDataSet;

		// Token: 0x040007FF RID: 2047
		private AdomdUtils.GetInvalidatedMessageDelegate msgDelegate;
	}
}
