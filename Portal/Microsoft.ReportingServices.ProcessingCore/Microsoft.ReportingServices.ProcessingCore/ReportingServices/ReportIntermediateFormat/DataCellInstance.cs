using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200040A RID: 1034
	internal sealed class DataCellInstance : ScopeInstance
	{
		// Token: 0x06002C5A RID: 11354 RVA: 0x000CC878 File Offset: 0x000CAA78
		private DataCellInstance(OnDemandProcessingContext odpContext, Cell cellDef, DataAggregateObjResult[] runningValueValues, DataAggregateObjResult[] runningValueOfAggregateValues, long firstRowOffset)
			: base(firstRowOffset)
		{
			this.m_cellDef = cellDef;
			DataRegion dataRegionDef = this.m_cellDef.DataRegionDef;
			if (cellDef.AggregateIndexes != null)
			{
				base.StoreAggregates<DataAggregateInfo>(odpContext, dataRegionDef.CellAggregates, cellDef.AggregateIndexes);
			}
			if (cellDef.PostSortAggregateIndexes != null)
			{
				base.StoreAggregates<DataAggregateInfo>(odpContext, dataRegionDef.CellPostSortAggregates, cellDef.PostSortAggregateIndexes);
			}
			if (runningValueValues == null)
			{
				if (cellDef.RunningValueIndexes != null)
				{
					base.StoreAggregates<RunningValueInfo>(odpContext, dataRegionDef.CellRunningValues, cellDef.RunningValueIndexes);
				}
			}
			else if (runningValueValues != null)
			{
				base.StoreAggregates(runningValueValues);
			}
			if (cellDef.DataScopeInfo != null)
			{
				DataScopeInfo dataScopeInfo = cellDef.DataScopeInfo;
				if (dataScopeInfo.AggregatesOfAggregates != null)
				{
					base.StoreAggregates<DataAggregateInfo>(odpContext, dataScopeInfo.AggregatesOfAggregates);
				}
				if (dataScopeInfo.PostSortAggregatesOfAggregates != null)
				{
					base.StoreAggregates<DataAggregateInfo>(odpContext, dataScopeInfo.PostSortAggregatesOfAggregates);
				}
				if (runningValueOfAggregateValues != null)
				{
					base.StoreAggregates(runningValueOfAggregateValues);
				}
			}
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x000CC945 File Offset: 0x000CAB45
		internal DataCellInstance()
		{
		}

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x06002C5C RID: 11356 RVA: 0x000CC94D File Offset: 0x000CAB4D
		internal override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataCellInstance;
			}
		}

		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x06002C5D RID: 11357 RVA: 0x000CC954 File Offset: 0x000CAB54
		internal override IRIFReportScope RIFReportScope
		{
			get
			{
				return this.m_cellDef;
			}
		}

		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x06002C5E RID: 11358 RVA: 0x000CC95C File Offset: 0x000CAB5C
		internal Cell CellDef
		{
			get
			{
				return this.m_cellDef;
			}
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x000CC964 File Offset: 0x000CAB64
		internal static DataCellInstance CreateInstance(IMemberHierarchy dataRegionOrRowMemberInstance, OnDemandProcessingContext odpContext, Cell cellDef, long firstRowOffset, int columnMemberSequenceId)
		{
			return DataCellInstance.CreateInstance(dataRegionOrRowMemberInstance, odpContext, cellDef, null, null, firstRowOffset, columnMemberSequenceId);
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x000CC974 File Offset: 0x000CAB74
		internal static DataCellInstance CreateInstance(IMemberHierarchy dataRegionOrRowMemberInstance, OnDemandProcessingContext odpContext, Cell cellDef, DataAggregateObjResult[] runningValueValues, DataAggregateObjResult[] runningValueOfAggregateValues, long firstRowOffset, int columnMemberSequenceId)
		{
			DataCellInstance dataCellInstance = new DataCellInstance(odpContext, cellDef, runningValueValues, runningValueOfAggregateValues, firstRowOffset);
			dataCellInstance.m_cleanupRef = dataRegionOrRowMemberInstance.AddCellInstance(columnMemberSequenceId, cellDef.IndexInCollection, dataCellInstance, odpContext.OdpMetadata.GroupTreeScalabilityCache);
			return dataCellInstance;
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x000CC9B0 File Offset: 0x000CABB0
		internal void SetupEnvironment(OnDemandProcessingContext odpContext, int dataSetIndex)
		{
			base.SetupFields(odpContext, dataSetIndex);
			DataRegion dataRegionDef = this.m_cellDef.DataRegionDef;
			int num = 0;
			if (this.m_cellDef.AggregateIndexes != null)
			{
				base.SetupAggregates<DataAggregateInfo>(odpContext, dataRegionDef.CellAggregates, this.m_cellDef.AggregateIndexes, ref num);
			}
			if (this.m_cellDef.PostSortAggregateIndexes != null)
			{
				base.SetupAggregates<DataAggregateInfo>(odpContext, dataRegionDef.CellPostSortAggregates, this.m_cellDef.PostSortAggregateIndexes, ref num);
			}
			if (this.m_cellDef.RunningValueIndexes != null)
			{
				base.SetupAggregates<RunningValueInfo>(odpContext, dataRegionDef.CellRunningValues, this.m_cellDef.RunningValueIndexes, ref num);
			}
			if (this.m_cellDef.DataScopeInfo != null)
			{
				DataScopeInfo dataScopeInfo = this.m_cellDef.DataScopeInfo;
				if (dataScopeInfo.AggregatesOfAggregates != null)
				{
					base.SetupAggregates<DataAggregateInfo>(odpContext, dataScopeInfo.AggregatesOfAggregates, ref num);
				}
				if (dataScopeInfo.PostSortAggregatesOfAggregates != null)
				{
					base.SetupAggregates<DataAggregateInfo>(odpContext, dataScopeInfo.PostSortAggregatesOfAggregates, ref num);
				}
				if (dataScopeInfo.RunningValuesOfAggregates != null)
				{
					base.SetupAggregates<RunningValueInfo>(odpContext, dataScopeInfo.RunningValuesOfAggregates, ref num);
				}
			}
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x000CCAA8 File Offset: 0x000CACA8
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataCellInstance, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScopeInstance, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ID, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Cell, Token.GlobalReference)
			});
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x000CCAE0 File Offset: 0x000CACE0
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(DataCellInstance.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.ID)
				{
					Global.Tracer.Assert(this.m_cellDef != null, "(null != m_cellDef)");
					writer.WriteGlobalReference(this.m_cellDef);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x000CCB4C File Offset: 0x000CAD4C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(DataCellInstance.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.ID)
				{
					this.m_cellDef = reader.ReadGlobalReference<Cell>();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x000CCB9F File Offset: 0x000CAD9F
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x000CCBAC File Offset: 0x000CADAC
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataCellInstance;
		}

		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x06002C67 RID: 11367 RVA: 0x000CCBB3 File Offset: 0x000CADB3
		public override int Size
		{
			get
			{
				return base.Size + ItemSizes.ReferenceSize;
			}
		}

		// Token: 0x040017E4 RID: 6116
		[NonSerialized]
		private Cell m_cellDef;

		// Token: 0x040017E5 RID: 6117
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = DataCellInstance.GetDeclaration();
	}
}
