using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000412 RID: 1042
	public abstract class ScopeInstance : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002D06 RID: 11526 RVA: 0x000CEA27 File Offset: 0x000CCC27
		protected ScopeInstance()
		{
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x000CEA41 File Offset: 0x000CCC41
		protected ScopeInstance(long firstRowOffset)
		{
			this.m_firstRowOffset = firstRowOffset;
		}

		// Token: 0x170015A0 RID: 5536
		// (get) Token: 0x06002D08 RID: 11528
		internal abstract Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType ObjectType { get; }

		// Token: 0x170015A1 RID: 5537
		// (get) Token: 0x06002D09 RID: 11529 RVA: 0x000CEA62 File Offset: 0x000CCC62
		internal virtual IRIFReportScope RIFReportScope
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170015A2 RID: 5538
		// (get) Token: 0x06002D0A RID: 11530 RVA: 0x000CEA65 File Offset: 0x000CCC65
		// (set) Token: 0x06002D0B RID: 11531 RVA: 0x000CEA6D File Offset: 0x000CCC6D
		internal long FirstRowOffset
		{
			get
			{
				return this.m_firstRowOffset;
			}
			set
			{
				this.m_firstRowOffset = value;
			}
		}

		// Token: 0x170015A3 RID: 5539
		// (get) Token: 0x06002D0C RID: 11532 RVA: 0x000CEA76 File Offset: 0x000CCC76
		internal List<IReference<DataRegionInstance>> DataRegionInstances
		{
			get
			{
				return this.m_dataRegionInstances;
			}
		}

		// Token: 0x170015A4 RID: 5540
		// (get) Token: 0x06002D0D RID: 11533 RVA: 0x000CEA7E File Offset: 0x000CCC7E
		internal List<IReference<SubReportInstance>> SubreportInstances
		{
			get
			{
				return this.m_subReportInstances;
			}
		}

		// Token: 0x170015A5 RID: 5541
		// (get) Token: 0x06002D0E RID: 11534 RVA: 0x000CEA86 File Offset: 0x000CCC86
		internal List<DataAggregateObjResult> AggregateValues
		{
			get
			{
				return this.m_aggregateValues;
			}
		}

		// Token: 0x170015A6 RID: 5542
		// (get) Token: 0x06002D0F RID: 11535 RVA: 0x000CEA8E File Offset: 0x000CCC8E
		internal bool IsReadOnly
		{
			get
			{
				return this.m_cleanupRef == null;
			}
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x000CEA99 File Offset: 0x000CCC99
		internal virtual void InstanceComplete()
		{
			this.m_cleanupRef.Dispose();
			this.m_cleanupRef = null;
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000CEAB0 File Offset: 0x000CCCB0
		protected void UnPinList<T>(List<ScalableList<T>> listOfLists)
		{
			if (listOfLists != null)
			{
				int count = listOfLists.Count;
				for (int i = 0; i < count; i++)
				{
					ScalableList<T> scalableList = listOfLists[i];
					if (scalableList != null)
					{
						scalableList.UnPinAll();
					}
				}
			}
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x000CEAE4 File Offset: 0x000CCCE4
		protected void SetReadOnlyList<T>(List<ScalableList<T>> listOfLists)
		{
			if (listOfLists != null)
			{
				int count = listOfLists.Count;
				for (int i = 0; i < count; i++)
				{
					ScalableList<T> scalableList = listOfLists[i];
					if (scalableList != null)
					{
						scalableList.SetReadOnly();
					}
				}
			}
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x000CEB18 File Offset: 0x000CCD18
		protected static void AdjustLength<T>(ScalableList<T> instances, int indexInCollection) where T : class
		{
			for (int i = instances.Count; i <= indexInCollection; i++)
			{
				instances.Add(default(T));
			}
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x000CEB48 File Offset: 0x000CCD48
		protected static IDisposable AddAndPinItemAt<T>(ScalableList<T> list, int index, T item) where T : class
		{
			for (int i = list.Count; i < index; i++)
			{
				list.Add(default(T));
			}
			return list.AddAndPin(item);
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x000CEB7C File Offset: 0x000CCD7C
		internal virtual void AddChildScope(IReference<ScopeInstance> childRef, int indexInCollection)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType objectType = childRef.Value().ObjectType;
			if (objectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SubReportInstance)
			{
				if (this.m_subReportInstances == null)
				{
					this.m_subReportInstances = new List<IReference<SubReportInstance>>();
				}
				ListUtils.AdjustLength<IReference<SubReportInstance>>(this.m_subReportInstances, indexInCollection);
				Global.Tracer.Assert(this.m_subReportInstances[indexInCollection] == null, "(null == m_subReportInstances[indexInCollection])");
				this.m_subReportInstances[indexInCollection] = childRef as IReference<SubReportInstance>;
				return;
			}
			if (objectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegionInstance)
			{
				if (this.m_dataRegionInstances == null)
				{
					this.m_dataRegionInstances = new List<IReference<DataRegionInstance>>();
				}
				ListUtils.AdjustLength<IReference<DataRegionInstance>>(this.m_dataRegionInstances, indexInCollection);
				Global.Tracer.Assert(this.m_dataRegionInstances[indexInCollection] == null, "(null == m_dataRegionInstances[indexInCollection])");
				this.m_dataRegionInstances[indexInCollection] = childRef as IReference<DataRegionInstance>;
				return;
			}
			Global.Tracer.Assert(false, childRef.Value().ToString());
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000CEC60 File Offset: 0x000CCE60
		internal void StoreAggregates<AggregateType>(OnDemandProcessingContext odpContext, List<AggregateType> aggregateDefs) where AggregateType : DataAggregateInfo
		{
			if (aggregateDefs == null)
			{
				return;
			}
			int count = aggregateDefs.Count;
			if (this.m_aggregateValues == null)
			{
				this.m_aggregateValues = new List<DataAggregateObjResult>();
			}
			for (int i = 0; i < count; i++)
			{
				ScopeInstance.StoreAggregate<AggregateType>(odpContext, aggregateDefs[i], ref this.m_aggregateValues);
			}
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x000CECAC File Offset: 0x000CCEAC
		internal void StoreAggregates<AggregateType>(OnDemandProcessingContext odpContext, BucketedAggregatesCollection<AggregateType> aggregateDefs) where AggregateType : DataAggregateInfo
		{
			if (aggregateDefs == null || aggregateDefs.IsEmpty)
			{
				return;
			}
			if (this.m_aggregateValues == null)
			{
				this.m_aggregateValues = new List<DataAggregateObjResult>();
			}
			foreach (AggregateType aggregateType in aggregateDefs)
			{
				ScopeInstance.StoreAggregate<AggregateType>(odpContext, aggregateType, ref this.m_aggregateValues);
			}
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x000CED1C File Offset: 0x000CCF1C
		internal void StoreAggregates(DataAggregateObjResult[] aggregateObjResults)
		{
			if (aggregateObjResults == null)
			{
				return;
			}
			int num = aggregateObjResults.Length;
			if (this.m_aggregateValues == null)
			{
				this.m_aggregateValues = new List<DataAggregateObjResult>();
			}
			for (int i = 0; i < num; i++)
			{
				this.m_aggregateValues.Add(aggregateObjResults[i]);
			}
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x000CED60 File Offset: 0x000CCF60
		internal void StoreAggregates<AggregateType>(OnDemandProcessingContext odpContext, List<AggregateType> aggregateDefs, List<int> aggregateIndexes) where AggregateType : DataAggregateInfo
		{
			if (aggregateIndexes == null)
			{
				return;
			}
			int count = aggregateIndexes.Count;
			if (this.m_aggregateValues == null)
			{
				this.m_aggregateValues = new List<DataAggregateObjResult>();
			}
			for (int i = 0; i < count; i++)
			{
				int num = aggregateIndexes[i];
				ScopeInstance.StoreAggregate<AggregateType>(odpContext, aggregateDefs[num], ref this.m_aggregateValues);
			}
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x000CEDB4 File Offset: 0x000CCFB4
		private static void StoreAggregate<AggregateType>(OnDemandProcessingContext odpContext, AggregateType aggregateDef, ref List<DataAggregateObjResult> aggregateValues) where AggregateType : DataAggregateInfo
		{
			DataAggregateObjResult dataAggregateObjResult = odpContext.ReportObjectModel.AggregatesImpl.GetAggregateObj(aggregateDef.Name).AggregateResult();
			aggregateValues.Add(dataAggregateObjResult);
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x000CEDEA File Offset: 0x000CCFEA
		protected static IList<DataRegionMemberInstance> GetChildMemberInstances(List<ScalableList<DataRegionMemberInstance>> members, int memberIndexInCollection)
		{
			if (members == null || members.Count <= memberIndexInCollection)
			{
				return null;
			}
			return members[memberIndexInCollection];
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x000CEE04 File Offset: 0x000CD004
		internal void SetupFields(OnDemandProcessingContext odpContext, int dataSetIndex)
		{
			DataSetInstance dataSetInstance = odpContext.CurrentReportInstance.GetDataSetInstance(dataSetIndex, odpContext);
			this.SetupFields(odpContext, dataSetInstance);
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x000CEE28 File Offset: 0x000CD028
		internal void SetupFields(OnDemandProcessingContext odpContext, DataSetInstance dataSetInstance)
		{
			if (!dataSetInstance.NoRows)
			{
				if (0L < this.m_firstRowOffset)
				{
					odpContext.ReportObjectModel.RegisterOnDemandFieldValueUpdate(this.m_firstRowOffset, dataSetInstance, odpContext.GetDataChunkReader(dataSetInstance.DataSetDef.IndexInCollection));
					return;
				}
				odpContext.ReportObjectModel.ResetFieldValues();
			}
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x000CEE78 File Offset: 0x000CD078
		internal void SetupAggregates<AggregateType>(OnDemandProcessingContext odpContext, List<AggregateType> aggregateDefs, ref int aggregateValueOffset) where AggregateType : DataAggregateInfo
		{
			if (this.m_aggregateValues != null && aggregateDefs != null)
			{
				int count = aggregateDefs.Count;
				for (int i = 0; i < count; i++)
				{
					ScopeInstance.SetupAggregate<AggregateType>(odpContext, aggregateDefs[i], this.m_aggregateValues[aggregateValueOffset]);
					aggregateValueOffset++;
				}
			}
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x000CEEC4 File Offset: 0x000CD0C4
		internal void SetupAggregates<AggregateType>(OnDemandProcessingContext odpContext, BucketedAggregatesCollection<AggregateType> aggregateDefs, ref int aggregateValueOffset) where AggregateType : DataAggregateInfo
		{
			if (this.m_aggregateValues != null && aggregateDefs != null)
			{
				foreach (AggregateType aggregateType in aggregateDefs)
				{
					ScopeInstance.SetupAggregate<AggregateType>(odpContext, aggregateType, this.m_aggregateValues[aggregateValueOffset]);
					aggregateValueOffset++;
				}
			}
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x000CEF2C File Offset: 0x000CD12C
		internal void SetupAggregates<AggregateType>(OnDemandProcessingContext odpContext, List<AggregateType> aggregateDefs, List<int> aggregateIndexes, ref int aggregateValueOffset) where AggregateType : DataAggregateInfo
		{
			int num = ((aggregateIndexes == null) ? 0 : aggregateIndexes.Count);
			for (int i = 0; i < num; i++)
			{
				int num2 = aggregateIndexes[i];
				ScopeInstance.SetupAggregate<AggregateType>(odpContext, aggregateDefs[num2], this.m_aggregateValues[aggregateValueOffset]);
				aggregateValueOffset++;
			}
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x000CEF7D File Offset: 0x000CD17D
		private static void SetupAggregate<AggregateType>(OnDemandProcessingContext odpContext, AggregateType aggregateDef, DataAggregateObjResult aggregateObj) where AggregateType : DataAggregateInfo
		{
			odpContext.ReportObjectModel.AggregatesImpl.Set(aggregateDef.Name, aggregateDef, aggregateDef.DuplicateNames, aggregateObj);
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x000CEFAC File Offset: 0x000CD1AC
		internal static void CalculateVariables(OnDemandProcessingContext odpContext, List<Variable> variableDefs, out object[] variableValues)
		{
			variableValues = null;
			if (variableDefs != null && variableDefs.Count != 0)
			{
				int count = variableDefs.Count;
				variableValues = new object[count];
				for (int i = 0; i < count; i++)
				{
					VariableImpl cachedVariableObj = variableDefs[i].GetCachedVariableObj(odpContext);
					variableValues[i] = cachedVariableObj.GetResult();
				}
			}
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x000CEFFC File Offset: 0x000CD1FC
		internal static void ResetVariables(OnDemandProcessingContext odpContext, List<Variable> variableDefs)
		{
			if (variableDefs == null)
			{
				return;
			}
			for (int i = 0; i < variableDefs.Count; i++)
			{
				variableDefs[i].GetCachedVariableObj(odpContext).Reset();
			}
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x000CF030 File Offset: 0x000CD230
		internal static void SetupVariables(OnDemandProcessingContext odpContext, List<Variable> variableDefs, object[] variableValues)
		{
			if (variableDefs == null)
			{
				return;
			}
			for (int i = 0; i < variableValues.Length; i++)
			{
				variableDefs[i].GetCachedVariableObj(odpContext).SetValue(variableValues[i], true);
			}
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x000CF068 File Offset: 0x000CD268
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScopeInstance, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.FirstRowIndex, Token.Int64),
				new MemberInfo(MemberName.DataRegionInstances, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegionInstanceReference),
				new MemberInfo(MemberName.SubReportInstances, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SubReportInstanceReference),
				new MemberInfo(MemberName.AggregateValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObjResult)
			});
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x000CF0CC File Offset: 0x000CD2CC
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ScopeInstance.m_Declaration);
			while (writer.NextMember())
			{
				switch (writer.CurrentMember.MemberName)
				{
				case MemberName.FirstRowIndex:
					writer.Write(this.m_firstRowOffset);
					break;
				case MemberName.DataRegionInstances:
					if (this.m_serializationDataRegionIndexInCollection < 0 || this.m_dataRegionInstances == null)
					{
						writer.Write<IReference<DataRegionInstance>>(this.m_dataRegionInstances);
					}
					else
					{
						List<IReference<DataRegionInstance>> list = new List<IReference<DataRegionInstance>>(this.m_dataRegionInstances.Count);
						ListUtils.AdjustLength<IReference<DataRegionInstance>>(list, this.m_dataRegionInstances.Count - 1);
						list[this.m_serializationDataRegionIndexInCollection] = this.m_dataRegionInstances[this.m_serializationDataRegionIndexInCollection];
						writer.Write<IReference<DataRegionInstance>>(list);
					}
					break;
				case MemberName.SubReportInstances:
					writer.Write<IReference<SubReportInstance>>(this.m_subReportInstances);
					break;
				case MemberName.AggregateValues:
					writer.Write<DataAggregateObjResult>(this.m_aggregateValues);
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x000CF1C8 File Offset: 0x000CD3C8
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ScopeInstance.m_Declaration);
			while (reader.NextMember())
			{
				switch (reader.CurrentMember.MemberName)
				{
				case MemberName.FirstRowIndex:
					this.m_firstRowOffset = reader.ReadInt64();
					break;
				case MemberName.DataRegionInstances:
					this.m_dataRegionInstances = reader.ReadGenericListOfRIFObjects<IReference<DataRegionInstance>>();
					break;
				case MemberName.SubReportInstances:
					this.m_subReportInstances = reader.ReadGenericListOfRIFObjects<IReference<SubReportInstance>>();
					break;
				case MemberName.AggregateValues:
					this.m_aggregateValues = reader.ReadGenericListOfRIFObjects<DataAggregateObjResult>();
					break;
				default:
					Global.Tracer.Assert(false);
					break;
				}
			}
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x000CF25B File Offset: 0x000CD45B
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x000CF268 File Offset: 0x000CD468
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScopeInstance;
		}

		// Token: 0x170015A7 RID: 5543
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x000CF26F File Offset: 0x000CD46F
		public virtual int Size
		{
			get
			{
				return 8 + ItemSizes.SizeOf<IReference<DataRegionInstance>>(this.m_dataRegionInstances) + ItemSizes.SizeOf<IReference<SubReportInstance>>(this.m_subReportInstances) + ItemSizes.SizeOf<DataAggregateObjResult>(this.m_aggregateValues) + 4;
			}
		}

		// Token: 0x04001815 RID: 6165
		protected long m_firstRowOffset = DataFieldRow.UnInitializedStreamOffset;

		// Token: 0x04001816 RID: 6166
		protected List<IReference<DataRegionInstance>> m_dataRegionInstances;

		// Token: 0x04001817 RID: 6167
		protected List<IReference<SubReportInstance>> m_subReportInstances;

		// Token: 0x04001818 RID: 6168
		protected List<DataAggregateObjResult> m_aggregateValues;

		// Token: 0x04001819 RID: 6169
		[NonSerialized]
		private int m_serializationDataRegionIndexInCollection = -1;

		// Token: 0x0400181A RID: 6170
		[NonSerialized]
		protected IDisposable m_cleanupRef;

		// Token: 0x0400181B RID: 6171
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ScopeInstance.GetDeclaration();
	}
}
