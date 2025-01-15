using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000411 RID: 1041
	public class ReportInstance : ScopeInstance
	{
		// Token: 0x06002CEB RID: 11499 RVA: 0x000CE4CC File Offset: 0x000CC6CC
		internal ReportInstance(OnDemandProcessingContext odpContext, Report reportDef, ParameterInfoCollection parameters)
		{
			int count = reportDef.MappingNameToDataSet.Count;
			this.m_dataSetInstances = new DataSetInstance[count];
			List<DataRegion> topLevelDataRegions = reportDef.TopLevelDataRegions;
			if (topLevelDataRegions != null)
			{
				GroupTreeScalabilityCache groupTreeScalabilityCache = odpContext.OdpMetadata.GroupTreeScalabilityCache;
				int count2 = topLevelDataRegions.Count;
				this.m_dataRegionInstances = new List<IReference<DataRegionInstance>>(count2);
				for (int i = 0; i < count2; i++)
				{
					this.m_dataRegionInstances.Add(groupTreeScalabilityCache.AllocateEmptyTreePartition<DataRegionInstance>(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegionInstanceReference));
				}
			}
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x000CE543 File Offset: 0x000CC743
		internal ReportInstance()
		{
		}

		// Token: 0x1700159C RID: 5532
		// (get) Token: 0x06002CED RID: 11501 RVA: 0x000CE54B File Offset: 0x000CC74B
		internal override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportInstance;
			}
		}

		// Token: 0x1700159D RID: 5533
		// (get) Token: 0x06002CEE RID: 11502 RVA: 0x000CE552 File Offset: 0x000CC752
		// (set) Token: 0x06002CEF RID: 11503 RVA: 0x000CE55A File Offset: 0x000CC75A
		internal bool NoRows
		{
			get
			{
				return this.m_noRows;
			}
			set
			{
				this.m_noRows = value;
			}
		}

		// Token: 0x1700159E RID: 5534
		// (get) Token: 0x06002CF0 RID: 11504 RVA: 0x000CE563 File Offset: 0x000CC763
		// (set) Token: 0x06002CF1 RID: 11505 RVA: 0x000CE56B File Offset: 0x000CC76B
		internal string Language
		{
			get
			{
				return this.m_language;
			}
			set
			{
				if (!base.IsReadOnly)
				{
					this.m_language = value;
				}
			}
		}

		// Token: 0x1700159F RID: 5535
		// (get) Token: 0x06002CF2 RID: 11506 RVA: 0x000CE57C File Offset: 0x000CC77C
		internal object[] VariableValues
		{
			get
			{
				return this.m_variables;
			}
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x000CE584 File Offset: 0x000CC784
		internal bool IsMissingExpectedDataChunk(OnDemandProcessingContext odpContext)
		{
			List<DataSet> mappingDataSetIndexToDataSet = odpContext.ReportDefinition.MappingDataSetIndexToDataSet;
			for (int i = 0; i < mappingDataSetIndexToDataSet.Count; i++)
			{
				DataSet dataSet = mappingDataSetIndexToDataSet[i];
				if (!dataSet.UsedOnlyInParameters && this.GetDataSetInstance(dataSet, odpContext) == null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x000CE5CC File Offset: 0x000CC7CC
		internal DataSetInstance GetDataSetInstance(DataSet dataSet, OnDemandProcessingContext odpContext)
		{
			if (this.m_dataSetInstances == null)
			{
				this.InitDataSetInstances(odpContext);
			}
			int indexInCollection = dataSet.IndexInCollection;
			if (this.m_dataSetInstances[indexInCollection] == null)
			{
				this.m_dataSetInstances[indexInCollection] = odpContext.GetDataSetInstance(dataSet);
			}
			return this.m_dataSetInstances[indexInCollection];
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000CE610 File Offset: 0x000CC810
		internal DataSetInstance GetDataSetInstance(int dataSetIndexInCollection, OnDemandProcessingContext odpContext)
		{
			if (this.m_dataSetInstances == null)
			{
				this.InitDataSetInstances(odpContext);
			}
			if (this.m_dataSetInstances[dataSetIndexInCollection] == null)
			{
				DataSet dataSet = odpContext.ReportDefinition.MappingDataSetIndexToDataSet[dataSetIndexInCollection];
				this.m_dataSetInstances[dataSetIndexInCollection] = odpContext.GetDataSetInstance(dataSet);
			}
			return this.m_dataSetInstances[dataSetIndexInCollection];
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x000CE65F File Offset: 0x000CC85F
		internal void SetDataSetInstance(DataSetInstance dataSetInstance)
		{
			this.m_dataSetInstances[dataSetInstance.DataSetDef.IndexInCollection] = dataSetInstance;
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x000CE674 File Offset: 0x000CC874
		private void InitDataSetInstances(OnDemandProcessingContext odpContext)
		{
			this.m_dataSetInstances = new DataSetInstance[odpContext.ReportDefinition.MappingDataSetIndexToDataSet.Count];
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000CE691 File Offset: 0x000CC891
		internal IEnumerator GetCachedDataSetInstances()
		{
			return this.m_dataSetInstances.GetEnumerator();
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000CE6A0 File Offset: 0x000CC8A0
		internal void InitializeFromSnapshot(OnDemandProcessingContext odpContext)
		{
			if (!odpContext.ReprocessSnapshot)
			{
				int num = 0;
				if (this.m_dataSetInstances == null && odpContext.ReportDefinition.MappingNameToDataSet != null)
				{
					num = odpContext.ReportDefinition.MappingNameToDataSet.Count;
				}
				this.m_dataSetInstances = new DataSetInstance[num];
			}
			Report reportDefinition = odpContext.ReportDefinition;
			this.m_noRows = reportDefinition.DataSetsNotOnlyUsedInParameters > 0;
			List<DataSource> dataSources = reportDefinition.DataSources;
			for (int i = 0; i < dataSources.Count; i++)
			{
				List<DataSet> dataSets = dataSources[i].DataSets;
				if (dataSets != null)
				{
					for (int j = 0; j < dataSets.Count; j++)
					{
						DataSet dataSet = dataSets[j];
						if (dataSet != null)
						{
							DataSetInstance dataSetInstance = this.GetDataSetInstance(dataSet, odpContext);
							if (dataSetInstance != null && this.m_noRows && !dataSetInstance.NoRows)
							{
								this.m_noRows = false;
							}
						}
					}
				}
			}
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000CE775 File Offset: 0x000CC975
		internal override void AddChildScope(IReference<ScopeInstance> child, int indexInCollection)
		{
			base.AddChildScope(child, indexInCollection);
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x000CE77F File Offset: 0x000CC97F
		internal IReference<DataRegionInstance> GetTopLevelDataRegionReference(int indexInCollection)
		{
			return this.m_dataRegionInstances[indexInCollection];
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x000CE790 File Offset: 0x000CC990
		internal void SetupEnvironment(OnDemandProcessingContext odpContext)
		{
			if (this.m_dataSetInstances == null)
			{
				this.InitDataSetInstances(odpContext);
			}
			for (int i = 0; i < this.m_dataSetInstances.Length; i++)
			{
				DataSetInstance dataSetInstance = this.GetDataSetInstance(i, odpContext);
				if (dataSetInstance != null)
				{
					dataSetInstance.SetupDataSetLevelAggregates(odpContext);
				}
			}
			if (this.m_variables != null)
			{
				ScopeInstance.SetupVariables(odpContext, odpContext.ReportDefinition.Variables, this.m_variables);
			}
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x000CE7F1 File Offset: 0x000CC9F1
		internal void CalculateAndStoreReportVariables(OnDemandProcessingContext odpContext)
		{
			if (odpContext.ReportDefinition.Variables != null && this.m_variables == null)
			{
				ScopeInstance.CalculateVariables(odpContext, odpContext.ReportDefinition.Variables, out this.m_variables);
			}
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x000CE81F File Offset: 0x000CCA1F
		internal void ResetReportVariables(OnDemandProcessingContext odpContext)
		{
			if (odpContext.ReportDefinition.Variables != null)
			{
				ScopeInstance.ResetVariables(odpContext, odpContext.ReportDefinition.Variables);
			}
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x000CE840 File Offset: 0x000CCA40
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportInstance, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScopeInstance, new List<MemberInfo>
			{
				new MemberInfo(MemberName.NoRows, Token.Boolean),
				new MemberInfo(MemberName.Language, Token.String),
				new ReadOnlyMemberInfo(MemberName.Variables, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveArray, Token.Object),
				new MemberInfo(MemberName.SerializableVariables, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SerializableArray, Token.Serializable)
			});
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x000CE8B0 File Offset: 0x000CCAB0
		internal static IReference<ReportInstance> CreateInstance(IReportInstanceContainer reportInstanceContainer, OnDemandProcessingContext odpContext, Report reportDef, ParameterInfoCollection parameters)
		{
			ReportInstance reportInstance = new ReportInstance(odpContext, reportDef, parameters);
			IReference<ReportInstance> reference = reportInstanceContainer.SetReportInstance(reportInstance, odpContext.OdpMetadata);
			reportInstance.m_cleanupRef = (IDisposable)reference;
			return reference;
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x000CE8E4 File Offset: 0x000CCAE4
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ReportInstance.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.NoRows)
				{
					if (memberName != MemberName.Language)
					{
						if (memberName != MemberName.SerializableVariables)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.WriteSerializableArray(this.m_variables);
						}
					}
					else
					{
						writer.Write(this.m_language);
					}
				}
				else
				{
					writer.Write(this.m_noRows);
				}
			}
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x000CE968 File Offset: 0x000CCB68
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ReportInstance.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.NoRows)
				{
					if (memberName == MemberName.Variables)
					{
						this.m_variables = reader.ReadVariantArray();
						continue;
					}
					if (memberName == MemberName.NoRows)
					{
						this.m_noRows = reader.ReadBoolean();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Language)
					{
						this.m_language = reader.ReadString();
						continue;
					}
					if (memberName == MemberName.SerializableVariables)
					{
						this.m_variables = reader.ReadSerializableArray();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x000CEA07 File Offset: 0x000CCC07
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x000CEA14 File Offset: 0x000CCC14
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportInstance;
		}

		// Token: 0x04001810 RID: 6160
		private bool m_noRows;

		// Token: 0x04001811 RID: 6161
		private string m_language;

		// Token: 0x04001812 RID: 6162
		private object[] m_variables;

		// Token: 0x04001813 RID: 6163
		[NonSerialized]
		private DataSetInstance[] m_dataSetInstances;

		// Token: 0x04001814 RID: 6164
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ReportInstance.GetDeclaration();
	}
}
