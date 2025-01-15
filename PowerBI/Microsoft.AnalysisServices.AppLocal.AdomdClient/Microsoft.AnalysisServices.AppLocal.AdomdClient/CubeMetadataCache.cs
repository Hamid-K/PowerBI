using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000080 RID: 128
	internal class CubeMetadataCache : IMetadataCache
	{
		// Token: 0x06000800 RID: 2048 RVA: 0x000264C0 File Offset: 0x000246C0
		static CubeMetadataCache()
		{
			CubeMetadataCache.internalTypeMap[InternalObjectType.InternalTypeDimension] = 0;
			CubeMetadataCache.internalTypeMap[InternalObjectType.InternalTypeHierarchy] = 1;
			CubeMetadataCache.internalTypeMap[InternalObjectType.InternalTypeLevel] = 2;
			CubeMetadataCache.internalTypeMap[InternalObjectType.InternalTypeMember] = 3;
			CubeMetadataCache.internalTypeMap[InternalObjectType.InternalTypeMeasure] = 4;
			CubeMetadataCache.internalTypeMap[InternalObjectType.InternalTypeNamedSet] = 5;
			CubeMetadataCache.internalTypeMap[InternalObjectType.InternalTypeLevelProperty] = 6;
			CubeMetadataCache.internalTypeMap[InternalObjectType.InternalTypeKpi] = 7;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x000266A8 File Offset: 0x000248A8
		private static int GetIndexForInternalType(InternalObjectType objectType)
		{
			return CubeMetadataCache.internalTypeMap[objectType];
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x000266B8 File Offset: 0x000248B8
		internal CubeMetadataCache(AdomdConnection connection, CubeDef parentCube)
		{
			this.msgDelegate = new AdomdUtils.GetInvalidatedMessageDelegate(this.GetCubesUpdatedMessage);
			this.connection = connection;
			this.parentCube = parentCube;
			this.cacheState = MetadataCacheState.UpToDate;
			this.cacheDataset = new DataSet();
			this.cacheDataset.Locale = CultureInfo.InvariantCulture;
			this.restrictions = new ListDictionary();
			this.restrictions.Add(CubeCollectionInternal.cubeNameRest, parentCube.Name);
			AdomdUtils.AddCubeSourceRestrictionIfApplicable(this.connection, this.restrictions);
			int count = CubeMetadataCache.internalTypeMap.Count;
			this.objectMetadataCaches = new ObjectMetadataCache[count];
			bool flag = AdomdUtils.ShouldAddObjectVisibilityRestriction(this.connection);
			for (int i = 0; i < count; i++)
			{
				ListDictionary listDictionary;
				if (flag || CubeMetadataCache.SchemaRowsetsData[i].HasAdditionalRestrictions)
				{
					listDictionary = new ListDictionary();
					AdomdUtils.CopyRestrictions(this.restrictions, listDictionary);
					if (CubeMetadataCache.SchemaRowsetsData[i].HasAdditionalRestrictions)
					{
						CubeMetadataCache.AddSpecificRestrictions(CubeMetadataCache.SchemaRowsetsData[i], listDictionary);
					}
					if (flag)
					{
						AdomdUtils.AddObjectVisibilityRestrictionIfApplicable(this.connection, CubeMetadataCache.SchemaRowsetsData[i].RequestType, listDictionary);
					}
				}
				else
				{
					listDictionary = this.restrictions;
				}
				this.objectMetadataCaches[i] = new ObjectMetadataCache(this, connection, CubeMetadataCache.SchemaRowsetsData[i], listDictionary);
			}
			this.lastCacheValidationTime = parentCube.PopulatedTime;
			this.cubeInfoTable = new DataTable();
			this.cubeInfoTable.Locale = CultureInfo.InvariantCulture;
			this.cubeInfoTable = ((DataRow)((IAdomdBaseObject)parentCube).MetadataData).Table.Clone();
			this.originalTimeColumn = ((DataRow)((IAdomdBaseObject)parentCube).MetadataData).Table.Columns[CubeDef.lastSchemaUpdateColumn];
			this.currentTimeColumn = this.cubeInfoTable.Columns[CubeDef.lastSchemaUpdateColumn];
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00026873 File Offset: 0x00024A73
		IObjectCache IMetadataCache.GetObjectCache(InternalObjectType objectType)
		{
			return this.GetObjectCache(objectType);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0002687C File Offset: 0x00024A7C
		void IMetadataCache.Populate(InternalObjectType objectType)
		{
			this.EnsureNotAbandoned();
			this.EnsureValid();
			this.Populate(objectType);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00026891 File Offset: 0x00024A91
		void IMetadataCache.Refresh(InternalObjectType objectType)
		{
			this.EnsureNotAbandoned();
			this.EnsureValid();
			this.Refresh(objectType);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x000268A6 File Offset: 0x00024AA6
		void IMetadataCache.CheckCacheIsValid()
		{
			this.EnsureNotAbandoned();
			this.EnsureValid();
			this.CheckCacheIsValid();
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x000268BA File Offset: 0x00024ABA
		void IMetadataCache.MarkNeedCheckForValidness()
		{
			if (this.cacheState == MetadataCacheState.UpToDate)
			{
				this.cacheState = MetadataCacheState.NeedsValidnessCheck;
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x000268CC File Offset: 0x00024ACC
		void IMetadataCache.MarkAbandoned()
		{
			this.cacheState = MetadataCacheState.Abandoned;
			ObjectMetadataCache[] array = this.objectMetadataCaches;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].MarkAbandonedSelf();
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00026900 File Offset: 0x00024B00
		DataRow IMetadataCache.FindObjectByUniqueName(SchemaObjectType objectType, string nameUnique)
		{
			this.Populate((InternalObjectType)objectType);
			IObjectCache objectCache = this.GetObjectCache((InternalObjectType)objectType);
			string dataTableFilter = AdomdUtils.GetDataTableFilter(this.GetUniqueNameColumn((InternalObjectType)objectType), nameUnique);
			DataRow[] filteredRows = objectCache.GetFilteredRows(null, dataTableFilter);
			if (filteredRows.Length == 0)
			{
				return null;
			}
			return filteredRows[0];
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0002693A File Offset: 0x00024B3A
		private void EnsureValid()
		{
			AdomdUtils.EnsureCacheNotInvalid(this.cacheState, this.msgDelegate);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0002694D File Offset: 0x00024B4D
		private string GetCubesUpdatedMessage()
		{
			return SR.Metadata_CubeHasbeenUpdated(this.parentCube.Name);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0002695F File Offset: 0x00024B5F
		private void EnsureNotAbandoned()
		{
			AdomdUtils.EnsureCacheNotAbandoned(this.cacheState);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0002696C File Offset: 0x00024B6C
		private void Populate(InternalObjectType objectType)
		{
			ObjectMetadataCache objectCache = this.GetObjectCache(objectType);
			bool isInitialized = objectCache.IsInitialized;
			bool isPopulated = objectCache.IsPopulated;
			if (!isInitialized || !isPopulated)
			{
				objectCache.PopulateSelf();
				if (!isInitialized)
				{
					this.CreatePrimaryKeys(objectCache.CacheTable, objectType);
					this.cacheDataset.Tables.Add(objectCache.CacheTable);
				}
				DataTable objectsParentTable = this.GetObjectsParentTable(objectType);
				if (objectsParentTable != null)
				{
					objectCache.Relation = this.CreateRelation(objectsParentTable, objectCache.CacheTable, objectCache.RelationColumn);
				}
				ObjectMetadataCache objectsChildObjectCache = this.GetObjectsChildObjectCache(objectType);
				if (this.GetObjectTable(objectsChildObjectCache) != null)
				{
					objectsChildObjectCache.Relation = this.CreateRelation(objectCache.CacheTable, objectsChildObjectCache.CacheTable, objectsChildObjectCache.RelationColumn);
				}
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00026A1E File Offset: 0x00024C1E
		private void Refresh(InternalObjectType objectType)
		{
			this.GetObjectCache(objectType).RefreshSelf();
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00026A2C File Offset: 0x00024C2C
		private void CheckCacheIsValid()
		{
			if (this.NeedCheckForRefresh())
			{
				this.CheckCacheValid();
			}
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00026A3C File Offset: 0x00024C3C
		private bool NeedCheckForRefresh()
		{
			return this.cacheState == MetadataCacheState.NeedsValidnessCheck || this.connection.HasAutoSyncTimeElapsed(this.lastCacheValidationTime, DateTime.Now);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00026A60 File Offset: 0x00024C60
		private void CheckCacheValid()
		{
			this.cubeInfoTable.Clear();
			ObjectMetadataCache.Discover(this.connection, CubeCollectionInternal.schemaName, this.restrictions, this.cubeInfoTable, false);
			bool flag = this.cubeInfoTable.Rows.Count > 0 && this.cubeInfoTable.Rows[0][this.currentTimeColumn].Equals(((DataRow)((IAdomdBaseObject)this.parentCube).MetadataData)[this.originalTimeColumn]);
			this.lastCacheValidationTime = DateTime.Now;
			if (flag)
			{
				this.cacheState = MetadataCacheState.UpToDate;
			}
			else
			{
				this.cacheState = MetadataCacheState.Invalid;
			}
			this.EnsureValid();
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00026B10 File Offset: 0x00024D10
		private DataRelation CreateRelation(DataTable parentTable, DataTable childTable, string relationColumn)
		{
			DataColumn dataColumn = parentTable.Columns[relationColumn];
			DataColumn dataColumn2 = childTable.Columns[relationColumn];
			DataRelation dataRelation = new DataRelation(relationColumn, dataColumn, dataColumn2);
			this.cacheDataset.Relations.Add(dataRelation);
			return dataRelation;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00026B54 File Offset: 0x00024D54
		private void CreatePrimaryKeys(DataTable table, InternalObjectType objectType)
		{
			SchemaRowsetCacheData schemaRowsetCacheData = CubeMetadataCache.SchemaRowsetsData[CubeMetadataCache.GetIndexForInternalType(objectType)];
			DataColumn[] array = new DataColumn[schemaRowsetCacheData.PrimaryKeyColumns.Length];
			for (int i = 0; i < schemaRowsetCacheData.PrimaryKeyColumns.Length; i++)
			{
				array[i] = table.Columns[schemaRowsetCacheData.PrimaryKeyColumns[i]];
			}
			table.PrimaryKey = array;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00026BAC File Offset: 0x00024DAC
		private DataTable GetObjectTable(ObjectMetadataCache cache)
		{
			if (cache != null && cache.IsInitialized)
			{
				return cache.CacheTable;
			}
			return null;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00026BC4 File Offset: 0x00024DC4
		private ObjectMetadataCache GetObjectsParentObjectCache(InternalObjectType objectType)
		{
			switch (objectType)
			{
			case InternalObjectType.InternalTypeDimension:
			case InternalObjectType.InternalTypeMeasure:
			case InternalObjectType.InternalTypeKpi:
				break;
			case InternalObjectType.InternalTypeHierarchy:
				return this.GetObjectCache(InternalObjectType.InternalTypeDimension);
			case InternalObjectType.InternalTypeLevel:
				return this.GetObjectCache(InternalObjectType.InternalTypeHierarchy);
			case InternalObjectType.InternalTypeMember:
				return null;
			case (InternalObjectType)5:
				goto IL_004C;
			default:
				if (objectType != InternalObjectType.InternalTypeNamedSet)
				{
					if (objectType != InternalObjectType.InternalTypeLevelProperty)
					{
						goto IL_004C;
					}
					return this.GetObjectCache(InternalObjectType.InternalTypeLevel);
				}
				break;
			}
			return null;
			IL_004C:
			return null;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00026C20 File Offset: 0x00024E20
		private ObjectMetadataCache GetObjectsChildObjectCache(InternalObjectType objectType)
		{
			switch (objectType)
			{
			case InternalObjectType.InternalTypeDimension:
				return this.GetObjectCache(InternalObjectType.InternalTypeHierarchy);
			case InternalObjectType.InternalTypeHierarchy:
				return this.GetObjectCache(InternalObjectType.InternalTypeLevel);
			case InternalObjectType.InternalTypeLevel:
				return this.GetObjectCache(InternalObjectType.InternalTypeLevelProperty);
			case InternalObjectType.InternalTypeMember:
				return null;
			case (InternalObjectType)5:
				return null;
			case InternalObjectType.InternalTypeMeasure:
			case InternalObjectType.InternalTypeKpi:
				break;
			default:
			{
				int num = objectType - InternalObjectType.InternalTypeNamedSet;
				break;
			}
			}
			return null;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00026C76 File Offset: 0x00024E76
		private DataTable GetObjectsParentTable(InternalObjectType objectType)
		{
			return this.GetObjectTable(this.GetObjectsParentObjectCache(objectType));
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00026C85 File Offset: 0x00024E85
		private string GetUniqueNameColumn(InternalObjectType objectType)
		{
			return CubeMetadataCache.SchemaRowsetsData[CubeMetadataCache.GetIndexForInternalType(objectType)].UniqueNameColumnName;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00026C98 File Offset: 0x00024E98
		private ObjectMetadataCache GetObjectCache(InternalObjectType objectType)
		{
			return this.objectMetadataCaches[CubeMetadataCache.GetIndexForInternalType(objectType)];
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00026CA7 File Offset: 0x00024EA7
		private static bool IsSupportedInternalType(InternalObjectType objectType)
		{
			return CubeMetadataCache.internalTypeMap.ContainsKey(objectType);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00026CB4 File Offset: 0x00024EB4
		private static void AddSpecificRestrictions(SchemaRowsetCacheData data, ListDictionary restrictions)
		{
			if (data.AdditionalStaticRestrictions != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in data.AdditionalStaticRestrictions)
				{
					restrictions[keyValuePair.Key] = keyValuePair.Value;
				}
			}
		}

		// Token: 0x0400056F RID: 1391
		private static readonly Dictionary<InternalObjectType, int> internalTypeMap = new Dictionary<InternalObjectType, int>(8);

		// Token: 0x04000570 RID: 1392
		private static readonly SchemaRowsetCacheData[] SchemaRowsetsData = new SchemaRowsetCacheData[]
		{
			new SchemaRowsetCacheData(InternalObjectType.InternalTypeDimension, DimensionCollectionInternal.schemaName, null, new string[]
			{
				Dimension.dimensionNameColumn,
				Dimension.uniqueNameColumn
			}, Dimension.uniqueNameColumn),
			new SchemaRowsetCacheData(InternalObjectType.InternalTypeHierarchy, HierarchyCollectionInternal.schemaName, DimensionCollectionInternal.dimUNameRest, new string[]
			{
				Hierarchy.uniqueNameColumn,
				Hierarchy.isAttribHierColumn
			}, Hierarchy.uniqueNameColumn),
			new SchemaRowsetCacheData(InternalObjectType.InternalTypeLevel, LevelCollectionInternal.schemaName, HierarchyCollectionInternal.hierUNameRest, new string[]
			{
				Level.levelNameColumn,
				Level.uniqueNameColumn
			}, Level.uniqueNameColumn),
			new SchemaRowsetCacheData(InternalObjectType.InternalTypeMember, "MDSCHEMA_MEMBERS", null, new string[0], "MEMBER_UNIQUE_NAME"),
			new SchemaRowsetCacheData(InternalObjectType.InternalTypeMeasure, MeasureCollectionInternal.schemaName, null, new string[]
			{
				Measure.measureNameColumn,
				Measure.uniqueNameColumn
			}, Measure.uniqueNameColumn),
			new SchemaRowsetCacheData(InternalObjectType.InternalTypeNamedSet, NamedSetCollectionInternal.schemaName, null, new string[] { "SET_NAME" }, "SET_NAME"),
			new SchemaRowsetCacheData(InternalObjectType.InternalTypeLevelProperty, "MDSCHEMA_PROPERTIES", LevelCollectionInternal.levelUNameRest, new string[]
			{
				Level.uniqueNameColumn,
				LevelProperty.levelPropNameColumn
			}, LevelProperty.levelPropNameColumn, new KeyValuePair<string, string>[]
			{
				new KeyValuePair<string, string>("PROPERTY_TYPE", 1.ToString(CultureInfo.InvariantCulture))
			}),
			new SchemaRowsetCacheData(InternalObjectType.InternalTypeKpi, KpiCollectionInternal.schemaName, null, new string[] { Kpi.kpiNameColumn }, Kpi.kpiNameColumn)
		};

		// Token: 0x04000571 RID: 1393
		private DataSet cacheDataset;

		// Token: 0x04000572 RID: 1394
		private AdomdConnection connection;

		// Token: 0x04000573 RID: 1395
		private ObjectMetadataCache[] objectMetadataCaches;

		// Token: 0x04000574 RID: 1396
		private ListDictionary restrictions;

		// Token: 0x04000575 RID: 1397
		private CubeDef parentCube;

		// Token: 0x04000576 RID: 1398
		private DateTime lastCacheValidationTime;

		// Token: 0x04000577 RID: 1399
		private DataTable cubeInfoTable;

		// Token: 0x04000578 RID: 1400
		private DataColumn originalTimeColumn;

		// Token: 0x04000579 RID: 1401
		private DataColumn currentTimeColumn;

		// Token: 0x0400057A RID: 1402
		private MetadataCacheState cacheState = MetadataCacheState.UpToDate;

		// Token: 0x0400057B RID: 1403
		private AdomdUtils.GetInvalidatedMessageDelegate msgDelegate;
	}
}
