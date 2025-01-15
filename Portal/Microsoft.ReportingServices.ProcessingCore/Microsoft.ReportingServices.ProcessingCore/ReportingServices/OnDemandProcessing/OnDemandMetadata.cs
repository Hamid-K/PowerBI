using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000826 RID: 2086
	public class OnDemandMetadata : IReportInstanceContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060074F4 RID: 29940 RVA: 0x001E490B File Offset: 0x001E2B0B
		internal OnDemandMetadata()
		{
			this.m_isInitialProcessingRequest = false;
			this.m_metaDataChanged = false;
		}

		// Token: 0x060074F5 RID: 29941 RVA: 0x001E494C File Offset: 0x001E2B4C
		internal OnDemandMetadata(Microsoft.ReportingServices.ReportIntermediateFormat.Report report)
		{
			this.m_report = report;
			this.m_odpChunkManager = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.OnDemandProcessingManager();
			this.m_isInitialProcessingRequest = true;
			this.m_metaDataChanged = true;
			this.m_tablixProcessingComplete = new Dictionary<string, bool[]>();
		}

		// Token: 0x060074F6 RID: 29942 RVA: 0x001E49B4 File Offset: 0x001E2BB4
		internal OnDemandMetadata(OnDemandMetadata metadataFromOldSnapshot, Microsoft.ReportingServices.ReportIntermediateFormat.Report report)
		{
			this.m_isInitialProcessingRequest = true;
			this.m_metaDataChanged = true;
			this.m_report = report;
			this.m_odpChunkManager = metadataFromOldSnapshot.m_odpChunkManager;
			this.m_subReportInfoMap = metadataFromOldSnapshot.m_subReportInfoMap;
			this.m_commonSubReportInfoMap = metadataFromOldSnapshot.m_commonSubReportInfoMap;
			this.m_dataChunkMap = metadataFromOldSnapshot.m_dataChunkMap;
			this.m_lastAssignedGlobalID = metadataFromOldSnapshot.m_lastAssignedGlobalID;
			this.CommonPrepareForReprocessing();
		}

		// Token: 0x1700277E RID: 10110
		// (get) Token: 0x060074F7 RID: 29943 RVA: 0x001E4A46 File Offset: 0x001E2C46
		internal bool IsInitialProcessingRequest
		{
			get
			{
				return this.m_isInitialProcessingRequest;
			}
		}

		// Token: 0x1700277F RID: 10111
		// (get) Token: 0x060074F8 RID: 29944 RVA: 0x001E4A4E File Offset: 0x001E2C4E
		// (set) Token: 0x060074F9 RID: 29945 RVA: 0x001E4A65 File Offset: 0x001E2C65
		internal bool GroupTreeHasChanged
		{
			get
			{
				return this.m_groupTreePartitions != null && this.m_groupTreePartitions.TreeHasChanged;
			}
			set
			{
				this.GroupTreePartitionManager.TreeHasChanged = value;
			}
		}

		// Token: 0x17002780 RID: 10112
		// (get) Token: 0x060074FA RID: 29946 RVA: 0x001E4A73 File Offset: 0x001E2C73
		// (set) Token: 0x060074FB RID: 29947 RVA: 0x001E4A8A File Offset: 0x001E2C8A
		internal bool LookupInfoHasChanged
		{
			get
			{
				return this.m_lookupPartitions != null && this.m_lookupPartitions.TreeHasChanged;
			}
			set
			{
				this.LookupPartitionManager.TreeHasChanged = value;
			}
		}

		// Token: 0x17002781 RID: 10113
		// (get) Token: 0x060074FC RID: 29948 RVA: 0x001E4A98 File Offset: 0x001E2C98
		internal bool SnapshotHasChanged
		{
			get
			{
				return this.GroupTreeHasChanged || this.LookupInfoHasChanged || this.m_metaDataChanged || this.m_reportSnapshot.CachedDataChanged;
			}
		}

		// Token: 0x17002782 RID: 10114
		// (get) Token: 0x060074FD RID: 29949 RVA: 0x001E4ABF File Offset: 0x001E2CBF
		// (set) Token: 0x060074FE RID: 29950 RVA: 0x001E4AC7 File Offset: 0x001E2CC7
		internal bool MetadataHasChanged
		{
			get
			{
				return this.m_metaDataChanged;
			}
			set
			{
				this.m_metaDataChanged = value;
			}
		}

		// Token: 0x17002783 RID: 10115
		// (get) Token: 0x060074FF RID: 29951 RVA: 0x001E4AD0 File Offset: 0x001E2CD0
		internal TreePartitionManager GroupTreePartitionManager
		{
			get
			{
				if (this.m_groupTreePartitions == null)
				{
					this.m_groupTreePartitions = new TreePartitionManager();
					this.m_groupTreePartitions.TreeHasChanged = true;
				}
				return this.m_groupTreePartitions;
			}
		}

		// Token: 0x17002784 RID: 10116
		// (get) Token: 0x06007500 RID: 29952 RVA: 0x001E4AF7 File Offset: 0x001E2CF7
		internal TreePartitionManager LookupPartitionManager
		{
			get
			{
				if (this.m_lookupPartitions == null)
				{
					this.m_lookupPartitions = new TreePartitionManager();
					this.m_lookupPartitions.TreeHasChanged = true;
				}
				return this.m_lookupPartitions;
			}
		}

		// Token: 0x17002785 RID: 10117
		// (get) Token: 0x06007501 RID: 29953 RVA: 0x001E4B1E File Offset: 0x001E2D1E
		// (set) Token: 0x06007502 RID: 29954 RVA: 0x001E4B26 File Offset: 0x001E2D26
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Report Report
		{
			get
			{
				return this.m_report;
			}
			set
			{
				this.m_report = value;
			}
		}

		// Token: 0x17002786 RID: 10118
		// (get) Token: 0x06007503 RID: 29955 RVA: 0x001E4B2F File Offset: 0x001E2D2F
		// (set) Token: 0x06007504 RID: 29956 RVA: 0x001E4B37 File Offset: 0x001E2D37
		public IReference<Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance> ReportInstance
		{
			get
			{
				return this.m_reportInstance;
			}
			set
			{
				this.m_reportInstance = value;
			}
		}

		// Token: 0x06007505 RID: 29957 RVA: 0x001E4B40 File Offset: 0x001E2D40
		public IReference<Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance> SetReportInstance(Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance reportInstance, OnDemandMetadata odpMetadata)
		{
			this.m_reportInstance = this.m_groupTreeScalabilityCache.AllocateAndPin<Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance>(reportInstance, 0);
			return this.m_reportInstance;
		}

		// Token: 0x17002787 RID: 10119
		// (get) Token: 0x06007506 RID: 29958 RVA: 0x001E4B5B File Offset: 0x001E2D5B
		// (set) Token: 0x06007507 RID: 29959 RVA: 0x001E4B63 File Offset: 0x001E2D63
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ReportSnapshot ReportSnapshot
		{
			get
			{
				return this.m_reportSnapshot;
			}
			set
			{
				this.m_reportSnapshot = value;
				this.m_metaDataChanged = true;
			}
		}

		// Token: 0x17002788 RID: 10120
		// (get) Token: 0x06007508 RID: 29960 RVA: 0x001E4B73 File Offset: 0x001E2D73
		internal Dictionary<string, DataSetInstance> DataChunkMap
		{
			get
			{
				return this.m_dataChunkMap;
			}
		}

		// Token: 0x17002789 RID: 10121
		// (get) Token: 0x06007509 RID: 29961 RVA: 0x001E4B7B File Offset: 0x001E2D7B
		// (set) Token: 0x0600750A RID: 29962 RVA: 0x001E4B83 File Offset: 0x001E2D83
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.OnDemandProcessingManager OdpChunkManager
		{
			get
			{
				return this.m_odpChunkManager;
			}
			set
			{
				this.m_odpChunkManager = value;
			}
		}

		// Token: 0x1700278A RID: 10122
		// (get) Token: 0x0600750B RID: 29963 RVA: 0x001E4B8C File Offset: 0x001E2D8C
		internal List<OnDemandProcessingContext> OdpContexts
		{
			get
			{
				return this.m_odpContexts;
			}
		}

		// Token: 0x1700278B RID: 10123
		// (get) Token: 0x0600750C RID: 29964 RVA: 0x001E4B94 File Offset: 0x001E2D94
		// (set) Token: 0x0600750D RID: 29965 RVA: 0x001E4B9C File Offset: 0x001E2D9C
		internal string TransparentImageChunkName
		{
			get
			{
				return this.m_transparentImageChunkName;
			}
			set
			{
				this.m_transparentImageChunkName = value;
				this.m_metaDataChanged = true;
			}
		}

		// Token: 0x1700278C RID: 10124
		// (get) Token: 0x0600750E RID: 29966 RVA: 0x001E4BAC File Offset: 0x001E2DAC
		// (set) Token: 0x0600750F RID: 29967 RVA: 0x001E4BB4 File Offset: 0x001E2DB4
		internal GroupTreeScalabilityCache GroupTreeScalabilityCache
		{
			get
			{
				return this.m_groupTreeScalabilityCache;
			}
			set
			{
				this.m_groupTreeScalabilityCache = value;
			}
		}

		// Token: 0x1700278D RID: 10125
		// (get) Token: 0x06007510 RID: 29968 RVA: 0x001E4BBD File Offset: 0x001E2DBD
		// (set) Token: 0x06007511 RID: 29969 RVA: 0x001E4BC5 File Offset: 0x001E2DC5
		internal LookupScalabilityCache LookupScalabilityCache
		{
			get
			{
				return this.m_lookupScalabilityCache;
			}
			set
			{
				this.m_lookupScalabilityCache = value;
			}
		}

		// Token: 0x1700278E RID: 10126
		// (get) Token: 0x06007512 RID: 29970 RVA: 0x001E4BCE File Offset: 0x001E2DCE
		// (set) Token: 0x06007513 RID: 29971 RVA: 0x001E4BD6 File Offset: 0x001E2DD6
		internal GlobalIDOwnerCollection GlobalIDOwnerCollection
		{
			get
			{
				return this.m_globalIDOwnerCollection;
			}
			set
			{
				this.m_globalIDOwnerCollection = value;
			}
		}

		// Token: 0x1700278F RID: 10127
		// (get) Token: 0x06007514 RID: 29972 RVA: 0x001E4BDF File Offset: 0x001E2DDF
		// (set) Token: 0x06007515 RID: 29973 RVA: 0x001E4BE7 File Offset: 0x001E2DE7
		internal long GroupTreeRootOffset
		{
			get
			{
				return this.m_groupTreeRootOffset;
			}
			set
			{
				this.m_groupTreeRootOffset = value;
				this.m_metaDataChanged = true;
			}
		}

		// Token: 0x17002790 RID: 10128
		// (get) Token: 0x06007516 RID: 29974 RVA: 0x001E4BF7 File Offset: 0x001E2DF7
		internal int LastAssignedGlobalID
		{
			get
			{
				return this.m_lastAssignedGlobalID;
			}
		}

		// Token: 0x06007517 RID: 29975 RVA: 0x001E4C00 File Offset: 0x001E2E00
		internal void ResetUserSortFilterContexts()
		{
			foreach (OnDemandProcessingContext onDemandProcessingContext in this.m_odpContexts)
			{
				onDemandProcessingContext.ResetUserSortFilterContext();
			}
		}

		// Token: 0x06007518 RID: 29976 RVA: 0x001E4C50 File Offset: 0x001E2E50
		internal void UpdateLastAssignedGlobalID()
		{
			if (this.m_globalIDOwnerCollection != null)
			{
				int lastAssignedID = this.m_globalIDOwnerCollection.LastAssignedID;
				if (lastAssignedID > this.m_lastAssignedGlobalID)
				{
					this.m_lastAssignedGlobalID = lastAssignedID;
					this.m_metaDataChanged = true;
				}
			}
		}

		// Token: 0x06007519 RID: 29977 RVA: 0x001E4C88 File Offset: 0x001E2E88
		private void CommonPrepareForReprocessing()
		{
			this.m_tablixProcessingComplete = new Dictionary<string, bool[]>();
			if (this.m_dataChunkMap != null)
			{
				foreach (DataSetInstance dataSetInstance in this.m_dataChunkMap.Values)
				{
					dataSetInstance.InitializeForReprocessing();
				}
			}
		}

		// Token: 0x0600751A RID: 29978 RVA: 0x001E4CF0 File Offset: 0x001E2EF0
		internal void LogMetrics()
		{
			if (this.m_odpContexts != null)
			{
				foreach (OnDemandProcessingContext onDemandProcessingContext in this.m_odpContexts)
				{
					if (onDemandProcessingContext.ReportRuntime != null)
					{
						onDemandProcessingContext.ReportRuntime.LogMetrics();
					}
				}
			}
		}

		// Token: 0x0600751B RID: 29979 RVA: 0x001E4D58 File Offset: 0x001E2F58
		internal void PrepareForCachedDataProcessing(OnDemandMetadata odpMetadata)
		{
			this.m_subReportInfoMap = odpMetadata.m_subReportInfoMap;
			this.m_commonSubReportInfoMap = odpMetadata.m_commonSubReportInfoMap;
			this.m_dataChunkMap = odpMetadata.m_dataChunkMap;
			this.CommonPrepareForReprocessing();
		}

		// Token: 0x0600751C RID: 29980 RVA: 0x001E4D84 File Offset: 0x001E2F84
		internal bool IsTablixProcessingComplete(OnDemandProcessingContext odpContext, int dataSetIndexInCollection)
		{
			if (this.m_tablixProcessingComplete == null)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = odpContext.ReportDefinition.MappingDataSetIndexToDataSet[dataSetIndexInCollection];
				DataSetInstance dataSetInstance = odpContext.GetDataSetInstance(dataSet);
				return dataSetInstance != null && dataSetInstance.OldSnapshotTablixProcessingComplete;
			}
			bool[] array;
			return this.m_tablixProcessingComplete.TryGetValue(this.GetUniqueIdFromContext(odpContext), out array) && array[dataSetIndexInCollection];
		}

		// Token: 0x0600751D RID: 29981 RVA: 0x001E4DDC File Offset: 0x001E2FDC
		internal void SetTablixProcessingComplete(OnDemandProcessingContext odpContext, int dataSetIndexInCollection)
		{
			if (this.m_tablixProcessingComplete == null)
			{
				this.m_tablixProcessingComplete = new Dictionary<string, bool[]>();
			}
			string uniqueIdFromContext = this.GetUniqueIdFromContext(odpContext);
			bool[] array;
			if (!this.m_tablixProcessingComplete.TryGetValue(uniqueIdFromContext, out array))
			{
				array = new bool[odpContext.ReportDefinition.DataSetCount];
				this.m_tablixProcessingComplete[uniqueIdFromContext] = array;
			}
			array[dataSetIndexInCollection] = true;
			this.m_metaDataChanged = true;
		}

		// Token: 0x0600751E RID: 29982 RVA: 0x001E4E40 File Offset: 0x001E3040
		private string GetUniqueIdFromContext(OnDemandProcessingContext odpContext)
		{
			if (odpContext.InSubreport)
			{
				string processingAbortItemUniqueIdentifier = odpContext.ProcessingAbortItemUniqueIdentifier;
				Global.Tracer.Assert(!string.IsNullOrEmpty(processingAbortItemUniqueIdentifier), "Subreport ID must not be null or empty");
				return processingAbortItemUniqueIdentifier;
			}
			return string.Empty;
		}

		// Token: 0x0600751F RID: 29983 RVA: 0x001E4E7B File Offset: 0x001E307B
		internal void DisposePersistedTreeScalability()
		{
			if (this.m_groupTreeScalabilityCache != null)
			{
				this.m_groupTreeScalabilityCache.Dispose();
				this.m_groupTreeScalabilityCache = null;
			}
			if (this.m_lookupScalabilityCache != null)
			{
				this.m_lookupScalabilityCache.Dispose();
				this.m_lookupScalabilityCache = null;
			}
		}

		// Token: 0x06007520 RID: 29984 RVA: 0x001E4EB4 File Offset: 0x001E30B4
		internal void EnsureLookupScalabilitySetup(IChunkFactory chunkFactory, int rifCompatVersion, bool prohibitSerializableValues)
		{
			if (this.m_lookupScalabilityCache == null)
			{
				bool flag = this.m_lookupPartitions != null;
				Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.OnDemandProcessingManager.EnsureLookupStorageSetup(this, chunkFactory, flag, rifCompatVersion, prohibitSerializableValues);
			}
		}

		// Token: 0x06007521 RID: 29985 RVA: 0x001E4EE0 File Offset: 0x001E30E0
		internal SubReportInfo AddSubReportInfo(bool isTopLevelSubreport, string definitionPath, string reportPath, string originalCatalogReportPath)
		{
			this.m_metaDataChanged = true;
			if (this.m_subReportInfoMap == null)
			{
				this.m_subReportInfoMap = new Dictionary<string, SubReportInfo>(EqualityComparers.StringComparerInstance);
			}
			Global.Tracer.Assert(!this.m_subReportInfoMap.ContainsKey(definitionPath), "(!m_subReportInfoMap.ContainsKey(definitionPath))");
			SubReportInfo subReportInfo = new SubReportInfo(Guid.NewGuid());
			this.m_subReportInfoMap.Add(definitionPath, subReportInfo);
			string text = (isTopLevelSubreport ? reportPath : (definitionPath + "_" + reportPath));
			bool flag;
			subReportInfo.CommonSubReportInfo = this.GetOrCreateCommonSubReportInfo(text, out flag);
			if (flag)
			{
				subReportInfo.CommonSubReportInfo.DefinitionUniqueName = subReportInfo.UniqueName;
				subReportInfo.CommonSubReportInfo.OriginalCatalogPath = originalCatalogReportPath;
			}
			return subReportInfo;
		}

		// Token: 0x06007522 RID: 29986 RVA: 0x001E4F88 File Offset: 0x001E3188
		private CommonSubReportInfo GetOrCreateCommonSubReportInfo(string reportPath, out bool created)
		{
			created = false;
			if (this.m_commonSubReportInfoMap == null)
			{
				this.m_commonSubReportInfoMap = new Dictionary<string, CommonSubReportInfo>(EqualityComparers.StringComparerInstance);
			}
			CommonSubReportInfo commonSubReportInfo;
			if (!this.m_commonSubReportInfoMap.TryGetValue(reportPath, out commonSubReportInfo))
			{
				created = true;
				commonSubReportInfo = new CommonSubReportInfo();
				commonSubReportInfo.ReportPath = reportPath;
				this.m_commonSubReportInfoMap.Add(reportPath, commonSubReportInfo);
			}
			return commonSubReportInfo;
		}

		// Token: 0x06007523 RID: 29987 RVA: 0x001E4FE0 File Offset: 0x001E31E0
		internal bool TryGetSubReportInfo(bool isTopLevelSubreport, string definitionPath, string reportPath, out SubReportInfo subReportInfo)
		{
			subReportInfo = null;
			if (this.m_subReportInfoMap != null && this.m_subReportInfoMap.TryGetValue(definitionPath, out subReportInfo))
			{
				if (subReportInfo.CommonSubReportInfo == null)
				{
					string text = (isTopLevelSubreport ? reportPath : (definitionPath + "_" + reportPath));
					if (this.m_commonSubReportInfoMap != null)
					{
						CommonSubReportInfo commonSubReportInfo;
						if (this.m_commonSubReportInfoMap.TryGetValue(text, out commonSubReportInfo))
						{
							subReportInfo.CommonSubReportInfo = commonSubReportInfo;
							return true;
						}
						int length = reportPath.Length;
						using (Dictionary<string, CommonSubReportInfo>.KeyCollection.Enumerator enumerator = this.m_commonSubReportInfoMap.Keys.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								string text2 = enumerator.Current;
								if (text2.Length >= length)
								{
									int num = text2.LastIndexOf(reportPath, StringComparison.OrdinalIgnoreCase);
									if (num >= 0 && num + length == text2.Length)
									{
										subReportInfo.CommonSubReportInfo = this.m_commonSubReportInfoMap[text2];
										return true;
									}
								}
							}
							return true;
						}
					}
					subReportInfo = null;
					return false;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06007524 RID: 29988 RVA: 0x001E50F0 File Offset: 0x001E32F0
		internal SubReportInfo GetSubReportInfo(bool isTopLevelSubreport, string definitionPath, string reportPath)
		{
			SubReportInfo subReportInfo = null;
			bool flag = this.TryGetSubReportInfo(isTopLevelSubreport, definitionPath, reportPath, out subReportInfo);
			Global.Tracer.Assert(flag, "Missing expected SubReportInfo: {0}_{1}", new object[] { definitionPath, reportPath });
			return subReportInfo;
		}

		// Token: 0x06007525 RID: 29989 RVA: 0x001E512C File Offset: 0x001E332C
		internal void AddDataChunk(string dataSetChunkName, DataSetInstance dataSetInstance)
		{
			this.m_metaDataChanged = true;
			dataSetInstance.DataChunkName = dataSetChunkName;
			Dictionary<string, DataSetInstance> dataChunkMap = this.m_dataChunkMap;
			lock (dataChunkMap)
			{
				this.m_dataChunkMap.Add(dataSetChunkName, dataSetInstance);
			}
		}

		// Token: 0x06007526 RID: 29990 RVA: 0x001E5184 File Offset: 0x001E3384
		internal void DeleteDataChunk(string dataSetChunkName)
		{
			this.m_metaDataChanged = true;
			Dictionary<string, DataSetInstance> dataChunkMap = this.m_dataChunkMap;
			lock (dataChunkMap)
			{
				this.m_dataChunkMap.Remove(dataSetChunkName);
			}
		}

		// Token: 0x06007527 RID: 29991 RVA: 0x001E51D4 File Offset: 0x001E33D4
		internal void AddExternalImage(string value, Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo imageInfo)
		{
			this.m_metaDataChanged = true;
			if (this.m_cachedExternalImages == null)
			{
				this.m_cachedExternalImages = new Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo>(EqualityComparers.StringComparerInstance);
			}
			this.m_cachedExternalImages.Add(value, imageInfo);
		}

		// Token: 0x06007528 RID: 29992 RVA: 0x001E5202 File Offset: 0x001E3402
		internal bool TryGetExternalImage(string value, out Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo imageInfo)
		{
			if (this.m_cachedExternalImages != null)
			{
				return this.m_cachedExternalImages.TryGetValue(value, out imageInfo);
			}
			imageInfo = null;
			return false;
		}

		// Token: 0x06007529 RID: 29993 RVA: 0x001E521E File Offset: 0x001E341E
		internal void AddShapefile(string value, ShapefileInfo shapefileInfo)
		{
			this.m_metaDataChanged = true;
			if (this.m_cachedShapefiles == null)
			{
				this.m_cachedShapefiles = new Dictionary<string, ShapefileInfo>(EqualityComparers.StringComparerInstance);
			}
			this.m_cachedShapefiles.Add(value, shapefileInfo);
		}

		// Token: 0x0600752A RID: 29994 RVA: 0x001E524C File Offset: 0x001E344C
		internal bool TryGetShapefile(string value, out ShapefileInfo shapefileInfo)
		{
			if (this.m_cachedShapefiles != null)
			{
				return this.m_cachedShapefiles.TryGetValue(value, out shapefileInfo);
			}
			shapefileInfo = null;
			return false;
		}

		// Token: 0x0600752B RID: 29995 RVA: 0x001E5268 File Offset: 0x001E3468
		internal bool StoreUpdatedVariableValue(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance reportInstance, int index, object value)
		{
			this.m_metaDataChanged = true;
			if (this.m_updatedVariableValues == null)
			{
				this.m_updatedVariableValues = new Dictionary<string, UpdatedVariableValues>();
			}
			string text = odpContext.SubReportUniqueName ?? "Report";
			UpdatedVariableValues updatedVariableValues;
			Dictionary<int, object> dictionary;
			if (this.m_updatedVariableValues.TryGetValue(text, out updatedVariableValues))
			{
				dictionary = updatedVariableValues.VariableValues;
			}
			else
			{
				dictionary = new Dictionary<int, object>();
				updatedVariableValues = new UpdatedVariableValues();
				updatedVariableValues.VariableValues = dictionary;
				this.m_updatedVariableValues.Add(text, updatedVariableValues);
			}
			if (reportInstance != null && reportInstance.VariableValues != null)
			{
				reportInstance.VariableValues[index] = value;
			}
			dictionary[index] = value;
			return true;
		}

		// Token: 0x0600752C RID: 29996 RVA: 0x001E52F8 File Offset: 0x001E34F8
		internal void SetUpdatedVariableValues(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance reportInstance)
		{
			if (this.m_updatedVariableValues == null)
			{
				return;
			}
			string text = odpContext.SubReportUniqueName ?? "Report";
			UpdatedVariableValues updatedVariableValues;
			if (this.m_updatedVariableValues.TryGetValue(text, out updatedVariableValues))
			{
				Dictionary<int, object> variableValues = updatedVariableValues.VariableValues;
				List<Variable> variables = odpContext.ReportDefinition.Variables;
				foreach (KeyValuePair<int, object> keyValuePair in variableValues)
				{
					reportInstance.VariableValues[keyValuePair.Key] = keyValuePair.Value;
					variables[keyValuePair.Key].GetCachedVariableObj(odpContext).SetValue(keyValuePair.Value, true);
				}
			}
		}

		// Token: 0x0600752D RID: 29997 RVA: 0x001E53B0 File Offset: 0x001E35B0
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.OnDemandMetadata, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.CommonSubReportInfos, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StringRIFObjectDictionary, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CommonSubReportInfo),
				new MemberInfo(MemberName.SubReportInfos, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StringRIFObjectDictionary, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SubReportInfo),
				new MemberInfo(MemberName.ReportSnapshot, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportSnapshot),
				new ReadOnlyMemberInfo(MemberName.GroupTreePartitionOffsets, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Int64),
				new MemberInfo(MemberName.DataChunkMap, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StringRIFObjectDictionary, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSetInstance),
				new MemberInfo(MemberName.CachedExternalImages, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StringRIFObjectDictionary, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ImageInfo),
				new MemberInfo(MemberName.TransparentImageChunkName, Token.String),
				new MemberInfo(MemberName.GroupTreeRootOffset, Token.Int64),
				new MemberInfo(MemberName.TablixProcessingComplete, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StringBoolArrayDictionary, Token.Boolean),
				new MemberInfo(MemberName.GroupTreePartitions, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TreePartitionManager),
				new MemberInfo(MemberName.LookupPartitions, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TreePartitionManager),
				new MemberInfo(MemberName.LastAssignedGlobalID, Token.Int32),
				new MemberInfo(MemberName.CachedShapefiles, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StringRIFObjectDictionary, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ShapefileInfo),
				new MemberInfo(MemberName.UpdatedVariableValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StringRIFObjectDictionary, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.UpdatedVariableValues)
			});
		}

		// Token: 0x0600752E RID: 29998 RVA: 0x001E5500 File Offset: 0x001E3700
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(OnDemandMetadata.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.GroupTreeRootOffset)
				{
					if (memberName <= MemberName.ReportSnapshot)
					{
						if (memberName == MemberName.TablixProcessingComplete)
						{
							writer.WriteStringBoolArrayDictionary(this.m_tablixProcessingComplete);
							continue;
						}
						if (memberName == MemberName.ReportSnapshot)
						{
							writer.Write(this.m_reportSnapshot);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.SubReportInfos)
						{
							writer.WriteStringRIFObjectDictionary<SubReportInfo>(this.m_subReportInfoMap);
							continue;
						}
						switch (memberName)
						{
						case MemberName.CachedExternalImages:
							writer.WriteStringRIFObjectDictionary<Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo>(this.m_cachedExternalImages);
							continue;
						case MemberName.TransparentImageChunkName:
							writer.Write(this.m_transparentImageChunkName);
							continue;
						case MemberName.DataChunkName:
							break;
						case MemberName.DataChunkMap:
							writer.WriteStringRIFObjectDictionary<DataSetInstance>(this.m_dataChunkMap);
							continue;
						default:
							if (memberName == MemberName.GroupTreeRootOffset)
							{
								writer.Write(this.m_groupTreeRootOffset);
								continue;
							}
							break;
						}
					}
				}
				else if (memberName <= MemberName.LookupPartitions)
				{
					if (memberName == MemberName.CommonSubReportInfos)
					{
						writer.WriteStringRIFObjectDictionary<CommonSubReportInfo>(this.m_commonSubReportInfoMap);
						continue;
					}
					if (memberName == MemberName.GroupTreePartitions)
					{
						writer.Write(this.m_groupTreePartitions);
						continue;
					}
					if (memberName == MemberName.LookupPartitions)
					{
						writer.Write(this.m_lookupPartitions);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.LastAssignedGlobalID)
					{
						writer.Write(this.m_lastAssignedGlobalID);
						continue;
					}
					if (memberName == MemberName.CachedShapefiles)
					{
						writer.WriteStringRIFObjectDictionary<ShapefileInfo>(this.m_cachedShapefiles);
						continue;
					}
					if (memberName == MemberName.UpdatedVariableValues)
					{
						writer.WriteStringRIFObjectDictionary<UpdatedVariableValues>(this.m_updatedVariableValues);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600752F RID: 29999 RVA: 0x001E56C4 File Offset: 0x001E38C4
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(OnDemandMetadata.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.GroupTreeRootOffset)
				{
					if (memberName <= MemberName.GroupTreePartitionOffsets)
					{
						if (memberName == MemberName.TablixProcessingComplete)
						{
							this.m_tablixProcessingComplete = reader.ReadStringBoolArrayDictionary();
							continue;
						}
						if (memberName == MemberName.ReportSnapshot)
						{
							this.m_reportSnapshot = (Microsoft.ReportingServices.ReportIntermediateFormat.ReportSnapshot)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.GroupTreePartitionOffsets)
						{
							List<long> list = reader.ReadListOfPrimitives<long>();
							if (list != null)
							{
								this.m_groupTreePartitions = new TreePartitionManager(list);
								continue;
							}
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.SubReportInfos)
						{
							this.m_subReportInfoMap = reader.ReadStringRIFObjectDictionary<SubReportInfo>();
							continue;
						}
						switch (memberName)
						{
						case MemberName.CachedExternalImages:
							this.m_cachedExternalImages = reader.ReadStringRIFObjectDictionary<Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo>();
							continue;
						case MemberName.TransparentImageChunkName:
							this.m_transparentImageChunkName = reader.ReadString();
							continue;
						case MemberName.DataChunkName:
							break;
						case MemberName.DataChunkMap:
							this.m_dataChunkMap = reader.ReadStringRIFObjectDictionary<DataSetInstance>();
							continue;
						default:
							if (memberName == MemberName.GroupTreeRootOffset)
							{
								this.m_groupTreeRootOffset = reader.ReadInt64();
								continue;
							}
							break;
						}
					}
				}
				else if (memberName <= MemberName.LookupPartitions)
				{
					if (memberName == MemberName.CommonSubReportInfos)
					{
						this.m_commonSubReportInfoMap = reader.ReadStringRIFObjectDictionary<CommonSubReportInfo>();
						continue;
					}
					if (memberName == MemberName.GroupTreePartitions)
					{
						this.m_groupTreePartitions = (TreePartitionManager)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.LookupPartitions)
					{
						this.m_lookupPartitions = (TreePartitionManager)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.LastAssignedGlobalID)
					{
						this.m_lastAssignedGlobalID = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.CachedShapefiles)
					{
						this.m_cachedShapefiles = reader.ReadStringRIFObjectDictionary<ShapefileInfo>();
						continue;
					}
					if (memberName == MemberName.UpdatedVariableValues)
					{
						this.m_updatedVariableValues = reader.ReadStringRIFObjectDictionary<UpdatedVariableValues>();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007530 RID: 30000 RVA: 0x001E58C1 File Offset: 0x001E3AC1
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007531 RID: 30001 RVA: 0x001E58CE File Offset: 0x001E3ACE
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.OnDemandMetadata;
		}

		// Token: 0x04003B6A RID: 15210
		private Dictionary<string, SubReportInfo> m_subReportInfoMap;

		// Token: 0x04003B6B RID: 15211
		private Dictionary<string, CommonSubReportInfo> m_commonSubReportInfoMap;

		// Token: 0x04003B6C RID: 15212
		private Microsoft.ReportingServices.ReportIntermediateFormat.ReportSnapshot m_reportSnapshot;

		// Token: 0x04003B6D RID: 15213
		private Dictionary<string, DataSetInstance> m_dataChunkMap = new Dictionary<string, DataSetInstance>();

		// Token: 0x04003B6E RID: 15214
		private Dictionary<string, bool[]> m_tablixProcessingComplete;

		// Token: 0x04003B6F RID: 15215
		private Dictionary<string, Microsoft.ReportingServices.ReportIntermediateFormat.ImageInfo> m_cachedExternalImages;

		// Token: 0x04003B70 RID: 15216
		private Dictionary<string, ShapefileInfo> m_cachedShapefiles;

		// Token: 0x04003B71 RID: 15217
		private string m_transparentImageChunkName;

		// Token: 0x04003B72 RID: 15218
		private long m_groupTreeRootOffset = TreePartitionManager.EmptyTreePartitionOffset;

		// Token: 0x04003B73 RID: 15219
		private TreePartitionManager m_groupTreePartitions;

		// Token: 0x04003B74 RID: 15220
		private TreePartitionManager m_lookupPartitions;

		// Token: 0x04003B75 RID: 15221
		private int m_lastAssignedGlobalID = -1;

		// Token: 0x04003B76 RID: 15222
		private Dictionary<string, UpdatedVariableValues> m_updatedVariableValues;

		// Token: 0x04003B77 RID: 15223
		[NonSerialized]
		private IReference<Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance> m_reportInstance;

		// Token: 0x04003B78 RID: 15224
		[NonSerialized]
		private Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.OnDemandProcessingManager m_odpChunkManager;

		// Token: 0x04003B79 RID: 15225
		[NonSerialized]
		private Microsoft.ReportingServices.ReportIntermediateFormat.Report m_report;

		// Token: 0x04003B7A RID: 15226
		[NonSerialized]
		private bool m_isInitialProcessingRequest;

		// Token: 0x04003B7B RID: 15227
		[NonSerialized]
		private bool m_metaDataChanged;

		// Token: 0x04003B7C RID: 15228
		[NonSerialized]
		private List<OnDemandProcessingContext> m_odpContexts = new List<OnDemandProcessingContext>();

		// Token: 0x04003B7D RID: 15229
		[NonSerialized]
		private GroupTreeScalabilityCache m_groupTreeScalabilityCache;

		// Token: 0x04003B7E RID: 15230
		[NonSerialized]
		private LookupScalabilityCache m_lookupScalabilityCache;

		// Token: 0x04003B7F RID: 15231
		[NonSerialized]
		private GlobalIDOwnerCollection m_globalIDOwnerCollection;

		// Token: 0x04003B80 RID: 15232
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = OnDemandMetadata.GetDeclaration();
	}
}
