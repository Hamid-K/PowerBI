using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200040D RID: 1037
	public sealed class DataRegionInstance : ScopeInstance, IMemberHierarchy
	{
		// Token: 0x06002C75 RID: 11381 RVA: 0x000CCCC9 File Offset: 0x000CAEC9
		private DataRegionInstance(DataRegion dataRegionDef, int dataSetIndex)
		{
			this.m_dataRegionDef = dataRegionDef;
			this.m_dataSetIndexInCollection = dataSetIndex;
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x000CCCE6 File Offset: 0x000CAEE6
		internal DataRegionInstance()
		{
		}

		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x06002C77 RID: 11383 RVA: 0x000CCCF5 File Offset: 0x000CAEF5
		internal override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegionInstance;
			}
		}

		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x06002C78 RID: 11384 RVA: 0x000CCCFC File Offset: 0x000CAEFC
		internal override IRIFReportScope RIFReportScope
		{
			get
			{
				return this.m_dataRegionDef;
			}
		}

		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x06002C79 RID: 11385 RVA: 0x000CCD04 File Offset: 0x000CAF04
		internal DataRegion DataRegionDef
		{
			get
			{
				return this.m_dataRegionDef;
			}
		}

		// Token: 0x17001576 RID: 5494
		// (get) Token: 0x06002C7A RID: 11386 RVA: 0x000CCD0C File Offset: 0x000CAF0C
		internal bool NoRows
		{
			get
			{
				return this.m_firstRowOffset <= 0L;
			}
		}

		// Token: 0x17001577 RID: 5495
		// (get) Token: 0x06002C7B RID: 11387 RVA: 0x000CCD1B File Offset: 0x000CAF1B
		internal int DataSetIndexInCollection
		{
			get
			{
				return this.m_dataSetIndexInCollection;
			}
		}

		// Token: 0x17001578 RID: 5496
		// (get) Token: 0x06002C7C RID: 11388 RVA: 0x000CCD23 File Offset: 0x000CAF23
		internal List<ScalableList<DataRegionMemberInstance>> TopLevelRowMembers
		{
			get
			{
				return this.m_rowMembers;
			}
		}

		// Token: 0x17001579 RID: 5497
		// (get) Token: 0x06002C7D RID: 11389 RVA: 0x000CCD2B File Offset: 0x000CAF2B
		internal List<ScalableList<DataRegionMemberInstance>> TopLevelColumnMembers
		{
			get
			{
				return this.m_columnMembers;
			}
		}

		// Token: 0x1700157A RID: 5498
		// (get) Token: 0x06002C7E RID: 11390 RVA: 0x000CCD33 File Offset: 0x000CAF33
		internal ScalableList<DataCellInstanceList> Cells
		{
			get
			{
				return this.m_cells;
			}
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x000CCD3C File Offset: 0x000CAF3C
		internal static IReference<DataRegionInstance> CreateInstance(ScopeInstance parentInstance, OnDemandMetadata odpMetadata, DataRegion dataRegionDef, int dataSetIndex)
		{
			DataRegionInstance dataRegionInstance = new DataRegionInstance(dataRegionDef, dataSetIndex);
			GroupTreeScalabilityCache groupTreeScalabilityCache = odpMetadata.GroupTreeScalabilityCache;
			IReference<DataRegionInstance> reference;
			if (parentInstance.ObjectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportInstance)
			{
				reference = ((ReportInstance)parentInstance).GetTopLevelDataRegionReference(dataRegionDef.IndexInCollection);
				groupTreeScalabilityCache.SetTreePartitionContentsAndPin<DataRegionInstance>(reference, dataRegionInstance);
			}
			else
			{
				reference = groupTreeScalabilityCache.AllocateAndPin<DataRegionInstance>(dataRegionInstance, 0);
				parentInstance.AddChildScope((IReference<ScopeInstance>)reference, dataRegionDef.IndexInCollection);
			}
			dataRegionInstance.m_cleanupRef = (IDisposable)reference;
			return reference;
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x000CCDA9 File Offset: 0x000CAFA9
		internal override void InstanceComplete()
		{
			if (this.m_cells != null)
			{
				this.m_cells.UnPinAll();
			}
			base.UnPinList<DataRegionMemberInstance>(this.m_rowMembers);
			base.UnPinList<DataRegionMemberInstance>(this.m_columnMembers);
			base.InstanceComplete();
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x000CCDDC File Offset: 0x000CAFDC
		IDisposable IMemberHierarchy.AddMemberInstance(DataRegionMemberInstance instance, int indexInCollection, IScalabilityCache cache, out int instanceIndex)
		{
			List<ScalableList<DataRegionMemberInstance>> list = (instance.MemberDef.IsColumn ? this.m_columnMembers : this.m_rowMembers);
			bool flag = false;
			if (list == null)
			{
				flag = true;
				list = new List<ScalableList<DataRegionMemberInstance>>();
				if (instance.MemberDef.IsColumn)
				{
					this.m_columnMembers = list;
				}
				else
				{
					this.m_rowMembers = list;
				}
			}
			ListUtils.AdjustLength<ScalableList<DataRegionMemberInstance>>(list, indexInCollection);
			ScalableList<DataRegionMemberInstance> scalableList = list[indexInCollection];
			if (flag || scalableList == null)
			{
				scalableList = new ScalableList<DataRegionMemberInstance>(0, cache, 100, 5);
				list[indexInCollection] = scalableList;
			}
			instanceIndex = scalableList.Count;
			return scalableList.AddAndPin(instance);
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x000CCE67 File Offset: 0x000CB067
		IDisposable IMemberHierarchy.AddCellInstance(int columnMemberSequenceId, int cellIndexInCollection, DataCellInstance cellInstance, IScalabilityCache cache)
		{
			if (this.m_cells == null)
			{
				this.m_cells = new ScalableList<DataCellInstanceList>(0, cache, 100, 5, true);
			}
			return DataRegionInstance.AddCellInstance(this.m_cells, columnMemberSequenceId, cellIndexInCollection, cellInstance, cache);
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x000CCE94 File Offset: 0x000CB094
		internal static IDisposable AddCellInstance(ScalableList<DataCellInstanceList> cells, int columnMemberSequenceId, int cellIndexInCollection, DataCellInstance cellInstance, IScalabilityCache cache)
		{
			ScopeInstance.AdjustLength<DataCellInstanceList>(cells, columnMemberSequenceId);
			DataCellInstanceList dataCellInstanceList;
			IDisposable andPin = cells.GetAndPin(columnMemberSequenceId, out dataCellInstanceList);
			if (dataCellInstanceList == null)
			{
				dataCellInstanceList = new DataCellInstanceList();
				cells[columnMemberSequenceId] = dataCellInstanceList;
			}
			ListUtils.AdjustLength<DataCellInstance>(dataCellInstanceList, cellIndexInCollection);
			dataCellInstanceList[cellIndexInCollection] = cellInstance;
			return andPin;
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x000CCED4 File Offset: 0x000CB0D4
		internal void SetupEnvironment(OnDemandProcessingContext odpContext)
		{
			base.SetupFields(odpContext, this.m_dataSetIndexInCollection);
			int num = 0;
			base.SetupAggregates<DataAggregateInfo>(odpContext, this.m_dataRegionDef.Aggregates, ref num);
			base.SetupAggregates<DataAggregateInfo>(odpContext, this.m_dataRegionDef.PostSortAggregates, ref num);
			base.SetupAggregates<RunningValueInfo>(odpContext, this.m_dataRegionDef.RunningValues, ref num);
			if (this.m_dataRegionDef.DataScopeInfo != null)
			{
				DataScopeInfo dataScopeInfo = this.m_dataRegionDef.DataScopeInfo;
				base.SetupAggregates<DataAggregateInfo>(odpContext, dataScopeInfo.AggregatesOfAggregates, ref num);
				base.SetupAggregates<DataAggregateInfo>(odpContext, dataScopeInfo.PostSortAggregatesOfAggregates, ref num);
				base.SetupAggregates<RunningValueInfo>(odpContext, dataScopeInfo.RunningValuesOfAggregates, ref num);
			}
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x000CCF72 File Offset: 0x000CB172
		IList<DataRegionMemberInstance> IMemberHierarchy.GetChildMemberInstances(bool isRowMember, int memberIndexInCollection)
		{
			return ScopeInstance.GetChildMemberInstances(isRowMember ? this.m_rowMembers : this.m_columnMembers, memberIndexInCollection);
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x000CCF8C File Offset: 0x000CB18C
		IList<DataCellInstance> IMemberHierarchy.GetCellInstances(int columnMemberSequenceId)
		{
			if (this.m_cells != null && columnMemberSequenceId < this.m_cells.Count)
			{
				return this.m_cells[columnMemberSequenceId];
			}
			if (this.m_upgradedSnapshotCells != null && columnMemberSequenceId < this.m_upgradedSnapshotCells.Count)
			{
				return this.m_upgradedSnapshotCells[columnMemberSequenceId];
			}
			return null;
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x000CCFE0 File Offset: 0x000CB1E0
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegionInstance, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScopeInstance, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ID, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegion, Token.GlobalReference),
				new MemberInfo(MemberName.DataSetIndexInCollection, Token.Int32),
				new MemberInfo(MemberName.RowMembers, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList),
				new MemberInfo(MemberName.ColumnMembers, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList),
				new ReadOnlyMemberInfo(MemberName.Cells, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList),
				new MemberInfo(MemberName.Cells2, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataCellInstanceList)
			});
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x000CD06C File Offset: 0x000CB26C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(DataRegionInstance.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.ID)
				{
					switch (memberName)
					{
					case MemberName.DataSetIndexInCollection:
						writer.Write7BitEncodedInt(this.m_dataSetIndexInCollection);
						break;
					case MemberName.RowMembers:
						writer.Write<ScalableList<DataRegionMemberInstance>>(this.m_rowMembers);
						break;
					case MemberName.ColumnMembers:
						writer.Write<ScalableList<DataRegionMemberInstance>>(this.m_columnMembers);
						break;
					default:
						if (memberName != MemberName.Cells2)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_cells);
						}
						break;
					}
				}
				else
				{
					Global.Tracer.Assert(this.m_dataRegionDef != null, "(null != m_dataRegionDef)");
					writer.WriteGlobalReference(this.m_dataRegionDef);
				}
			}
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x000CD13C File Offset: 0x000CB33C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(DataRegionInstance.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.ID)
				{
					switch (memberName)
					{
					case MemberName.DataSetIndexInCollection:
						this.m_dataSetIndexInCollection = reader.Read7BitEncodedInt();
						break;
					case MemberName.RowMembers:
						this.m_rowMembers = reader.ReadGenericListOfRIFObjectsUsingNew<ScalableList<DataRegionMemberInstance>>();
						base.SetReadOnlyList<DataRegionMemberInstance>(this.m_rowMembers);
						break;
					case MemberName.ColumnMembers:
						this.m_columnMembers = reader.ReadGenericListOfRIFObjectsUsingNew<ScalableList<DataRegionMemberInstance>>();
						base.SetReadOnlyList<DataRegionMemberInstance>(this.m_columnMembers);
						break;
					case MemberName.Cells:
						this.m_upgradedSnapshotCells = reader.ReadGenericListOfRIFObjectsUsingNew<ScalableList<DataCellInstance>>();
						base.SetReadOnlyList<DataCellInstance>(this.m_upgradedSnapshotCells);
						break;
					default:
						if (memberName != MemberName.Cells2)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_cells = reader.ReadRIFObject<ScalableList<DataCellInstanceList>>();
						}
						break;
					}
				}
				else
				{
					this.m_dataRegionDef = reader.ReadGlobalReference<DataRegion>();
				}
			}
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x000CD229 File Offset: 0x000CB429
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x000CD236 File Offset: 0x000CB436
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegionInstance;
		}

		// Token: 0x1700157B RID: 5499
		// (get) Token: 0x06002C8C RID: 11404 RVA: 0x000CD23D File Offset: 0x000CB43D
		public override int Size
		{
			get
			{
				return base.Size + 4 + ItemSizes.SizeOf<ScalableList<DataRegionMemberInstance>>(this.m_rowMembers) + ItemSizes.SizeOf<ScalableList<DataRegionMemberInstance>>(this.m_columnMembers) + ItemSizes.SizeOf<DataCellInstanceList>(this.m_cells) + ItemSizes.SizeOf<ScalableList<DataCellInstance>>(this.m_upgradedSnapshotCells) + ItemSizes.ReferenceSize;
			}
		}

		// Token: 0x040017E7 RID: 6119
		private int m_dataSetIndexInCollection = -1;

		// Token: 0x040017E8 RID: 6120
		private List<ScalableList<DataRegionMemberInstance>> m_rowMembers;

		// Token: 0x040017E9 RID: 6121
		private List<ScalableList<DataRegionMemberInstance>> m_columnMembers;

		// Token: 0x040017EA RID: 6122
		private ScalableList<DataCellInstanceList> m_cells;

		// Token: 0x040017EB RID: 6123
		[NonSerialized]
		private List<ScalableList<DataCellInstance>> m_upgradedSnapshotCells;

		// Token: 0x040017EC RID: 6124
		[Reference]
		private DataRegion m_dataRegionDef;

		// Token: 0x040017ED RID: 6125
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = DataRegionInstance.GetDeclaration();
	}
}
