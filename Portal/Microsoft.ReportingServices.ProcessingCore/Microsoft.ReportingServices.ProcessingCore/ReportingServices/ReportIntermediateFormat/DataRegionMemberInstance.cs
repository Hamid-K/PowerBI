using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200040E RID: 1038
	public sealed class DataRegionMemberInstance : ScopeInstance, IMemberHierarchy
	{
		// Token: 0x06002C8E RID: 11406 RVA: 0x000CD28C File Offset: 0x000CB48C
		private DataRegionMemberInstance(OnDemandProcessingContext odpContext, ReportHierarchyNode memberDef, long firstRowOffset, int memberInstanceIndexWithinScopeLevel, int recursiveLevel, List<object> groupExpressionValues, object[] groupVariableValues)
			: base(firstRowOffset)
		{
			this.m_memberDef = memberDef;
			this.m_memberInstanceIndexWithinScopeLevel = memberInstanceIndexWithinScopeLevel;
			this.m_recursiveLevel = recursiveLevel;
			if (groupExpressionValues != null && groupExpressionValues.Count != 0)
			{
				this.m_groupExprValues = new object[groupExpressionValues.Count];
				for (int i = 0; i < this.m_groupExprValues.Length; i++)
				{
					object obj = groupExpressionValues[i];
					if (obj == DBNull.Value)
					{
						obj = null;
					}
					this.m_groupExprValues[i] = obj;
				}
			}
			base.StoreAggregates<DataAggregateInfo>(odpContext, memberDef.Grouping.Aggregates);
			base.StoreAggregates<DataAggregateInfo>(odpContext, memberDef.Grouping.RecursiveAggregates);
			base.StoreAggregates<DataAggregateInfo>(odpContext, memberDef.Grouping.PostSortAggregates);
			base.StoreAggregates<RunningValueInfo>(odpContext, memberDef.RunningValues);
			if (memberDef.DataScopeInfo != null)
			{
				DataScopeInfo dataScopeInfo = memberDef.DataScopeInfo;
				base.StoreAggregates<DataAggregateInfo>(odpContext, dataScopeInfo.AggregatesOfAggregates);
				base.StoreAggregates<DataAggregateInfo>(odpContext, dataScopeInfo.PostSortAggregatesOfAggregates);
				base.StoreAggregates<RunningValueInfo>(odpContext, dataScopeInfo.RunningValuesOfAggregates);
			}
			this.m_variables = groupVariableValues;
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000CD394 File Offset: 0x000CB594
		internal DataRegionMemberInstance()
		{
		}

		// Token: 0x1700157C RID: 5500
		// (get) Token: 0x06002C90 RID: 11408 RVA: 0x000CD3AA File Offset: 0x000CB5AA
		internal override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegionMemberInstance;
			}
		}

		// Token: 0x1700157D RID: 5501
		// (get) Token: 0x06002C91 RID: 11409 RVA: 0x000CD3B1 File Offset: 0x000CB5B1
		internal override IRIFReportScope RIFReportScope
		{
			get
			{
				return this.m_memberDef;
			}
		}

		// Token: 0x1700157E RID: 5502
		// (get) Token: 0x06002C92 RID: 11410 RVA: 0x000CD3B9 File Offset: 0x000CB5B9
		internal int MemberInstanceIndexWithinScopeLevel
		{
			get
			{
				return this.m_memberInstanceIndexWithinScopeLevel;
			}
		}

		// Token: 0x1700157F RID: 5503
		// (get) Token: 0x06002C93 RID: 11411 RVA: 0x000CD3C1 File Offset: 0x000CB5C1
		internal int RecursiveLevel
		{
			get
			{
				return this.m_recursiveLevel;
			}
		}

		// Token: 0x17001580 RID: 5504
		// (get) Token: 0x06002C94 RID: 11412 RVA: 0x000CD3C9 File Offset: 0x000CB5C9
		internal object[] GroupVariables
		{
			get
			{
				return this.m_variables;
			}
		}

		// Token: 0x17001581 RID: 5505
		// (get) Token: 0x06002C95 RID: 11413 RVA: 0x000CD3D1 File Offset: 0x000CB5D1
		internal object[] GroupExprValues
		{
			get
			{
				return this.m_groupExprValues;
			}
		}

		// Token: 0x17001582 RID: 5506
		// (get) Token: 0x06002C96 RID: 11414 RVA: 0x000CD3D9 File Offset: 0x000CB5D9
		internal List<ScalableList<DataRegionMemberInstance>> Children
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x17001583 RID: 5507
		// (get) Token: 0x06002C97 RID: 11415 RVA: 0x000CD3E1 File Offset: 0x000CB5E1
		internal ScalableList<DataCellInstanceList> Cells
		{
			get
			{
				return this.m_cells;
			}
		}

		// Token: 0x17001584 RID: 5508
		// (get) Token: 0x06002C98 RID: 11416 RVA: 0x000CD3E9 File Offset: 0x000CB5E9
		internal ReportHierarchyNode MemberDef
		{
			get
			{
				return this.m_memberDef;
			}
		}

		// Token: 0x17001585 RID: 5509
		// (get) Token: 0x06002C99 RID: 11417 RVA: 0x000CD3F1 File Offset: 0x000CB5F1
		// (set) Token: 0x06002C9A RID: 11418 RVA: 0x000CD3F9 File Offset: 0x000CB5F9
		internal int RecursiveParentIndex
		{
			get
			{
				return this.m_parentInstanceIndex;
			}
			set
			{
				this.m_parentInstanceIndex = value;
			}
		}

		// Token: 0x17001586 RID: 5510
		// (get) Token: 0x06002C9B RID: 11419 RVA: 0x000CD402 File Offset: 0x000CB602
		// (set) Token: 0x06002C9C RID: 11420 RVA: 0x000CD40A File Offset: 0x000CB60A
		internal bool? HasRecursiveChildren
		{
			get
			{
				return this.m_hasRecursiveChildren;
			}
			set
			{
				this.m_hasRecursiveChildren = value;
			}
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x000CD414 File Offset: 0x000CB614
		internal static DataRegionMemberInstance CreateInstance(IMemberHierarchy parentInstance, OnDemandProcessingContext odpContext, ReportHierarchyNode memberDef, long firstRowOffset, int memberInstanceIndexWithinScopeLevel, int recursiveLevel, List<object> groupExpressionValues, object[] groupVariableValues, out int instanceIndex)
		{
			DataRegionMemberInstance dataRegionMemberInstance = new DataRegionMemberInstance(odpContext, memberDef, firstRowOffset, memberInstanceIndexWithinScopeLevel, recursiveLevel, groupExpressionValues, groupVariableValues);
			dataRegionMemberInstance.m_cleanupRef = parentInstance.AddMemberInstance(dataRegionMemberInstance, memberDef.IndexInCollection, odpContext.OdpMetadata.GroupTreeScalabilityCache, out instanceIndex);
			return dataRegionMemberInstance;
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x000CD453 File Offset: 0x000CB653
		internal override void InstanceComplete()
		{
			if (this.m_cells != null)
			{
				this.m_cells.UnPinAll();
			}
			base.UnPinList<DataRegionMemberInstance>(this.m_children);
			base.InstanceComplete();
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x000CD47C File Offset: 0x000CB67C
		IDisposable IMemberHierarchy.AddMemberInstance(DataRegionMemberInstance instance, int indexInCollection, IScalabilityCache cache, out int instanceIndex)
		{
			bool flag = false;
			if (this.m_children == null)
			{
				this.m_children = new List<ScalableList<DataRegionMemberInstance>>();
				flag = true;
			}
			ListUtils.AdjustLength<ScalableList<DataRegionMemberInstance>>(this.m_children, indexInCollection);
			ScalableList<DataRegionMemberInstance> scalableList = this.m_children[indexInCollection];
			if (flag || scalableList == null)
			{
				scalableList = new ScalableList<DataRegionMemberInstance>(0, cache, 100, 5);
				this.m_children[indexInCollection] = scalableList;
			}
			instanceIndex = scalableList.Count;
			return scalableList.AddAndPin(instance);
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000CD4E7 File Offset: 0x000CB6E7
		IDisposable IMemberHierarchy.AddCellInstance(int columnMemberSequenceId, int cellIndexInCollection, DataCellInstance cellInstance, IScalabilityCache cache)
		{
			if (this.m_cells == null)
			{
				this.m_cells = new ScalableList<DataCellInstanceList>(0, cache, 100, 5, true);
			}
			return DataRegionInstance.AddCellInstance(this.m_cells, columnMemberSequenceId, cellIndexInCollection, cellInstance, cache);
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000CD514 File Offset: 0x000CB714
		internal void SetupEnvironment(OnDemandProcessingContext odpContext, int dataSetIndex)
		{
			base.SetupFields(odpContext, dataSetIndex);
			int num = 0;
			base.SetupAggregates<DataAggregateInfo>(odpContext, this.m_memberDef.Grouping.Aggregates, ref num);
			base.SetupAggregates<DataAggregateInfo>(odpContext, this.m_memberDef.Grouping.RecursiveAggregates, ref num);
			base.SetupAggregates<DataAggregateInfo>(odpContext, this.m_memberDef.Grouping.PostSortAggregates, ref num);
			base.SetupAggregates<RunningValueInfo>(odpContext, this.m_memberDef.RunningValues, ref num);
			if (this.m_memberDef.DataScopeInfo != null)
			{
				DataScopeInfo dataScopeInfo = this.m_memberDef.DataScopeInfo;
				base.SetupAggregates<DataAggregateInfo>(odpContext, dataScopeInfo.AggregatesOfAggregates, ref num);
				base.SetupAggregates<DataAggregateInfo>(odpContext, dataScopeInfo.PostSortAggregatesOfAggregates, ref num);
				base.SetupAggregates<RunningValueInfo>(odpContext, dataScopeInfo.RunningValuesOfAggregates, ref num);
			}
			if (this.m_variables != null)
			{
				ScopeInstance.SetupVariables(odpContext, this.m_memberDef.Grouping.Variables, this.m_variables);
			}
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x000CD5F4 File Offset: 0x000CB7F4
		IList<DataRegionMemberInstance> IMemberHierarchy.GetChildMemberInstances(bool isRowMember, int memberIndexInCollection)
		{
			return ScopeInstance.GetChildMemberInstances(this.m_children, memberIndexInCollection);
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000CD604 File Offset: 0x000CB804
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

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000CD658 File Offset: 0x000CB858
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegionMemberInstance, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScopeInstance, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ID, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportHierarchyNode, Token.GlobalReference),
				new MemberInfo(MemberName.MemberInstanceIndexWithinScopeLevel, Token.Int32),
				new MemberInfo(MemberName.Children, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList),
				new ReadOnlyMemberInfo(MemberName.Cells, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList),
				new ReadOnlyMemberInfo(MemberName.Variables, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveArray, Token.Object),
				new MemberInfo(MemberName.SerializableVariables, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SerializableArray, Token.Serializable),
				new MemberInfo(MemberName.RecursiveLevel, Token.Int32),
				new MemberInfo(MemberName.GroupExpressionValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveArray, Token.Object),
				new MemberInfo(MemberName.ParentInstanceIndex, Token.Int32),
				new MemberInfo(MemberName.HasRecursiveChildren, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Nullable, Token.Boolean),
				new MemberInfo(MemberName.Cells2, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataCellInstanceList)
			});
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000CD748 File Offset: 0x000CB948
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(DataRegionMemberInstance.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ParentInstanceIndex)
				{
					if (memberName == MemberName.ID)
					{
						Global.Tracer.Assert(this.m_memberDef != null, "(null != m_memberDef)");
						writer.WriteGlobalReference(this.m_memberDef);
						continue;
					}
					switch (memberName)
					{
					case MemberName.MemberInstanceIndexWithinScopeLevel:
						writer.Write7BitEncodedInt(this.m_memberInstanceIndexWithinScopeLevel);
						continue;
					case MemberName.Children:
						writer.Write<ScalableList<DataRegionMemberInstance>>(this.m_children);
						continue;
					case MemberName.Variable:
					case MemberName.Variables:
						break;
					case MemberName.RecursiveLevel:
						writer.Write7BitEncodedInt(this.m_recursiveLevel);
						continue;
					case MemberName.GroupExpressionValues:
						writer.Write(this.m_groupExprValues);
						continue;
					default:
						if (memberName == MemberName.ParentInstanceIndex)
						{
							writer.Write(this.m_parentInstanceIndex);
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.HasRecursiveChildren)
					{
						writer.Write(this.m_hasRecursiveChildren);
						continue;
					}
					if (memberName == MemberName.SerializableVariables)
					{
						writer.WriteSerializableArray(this.m_variables);
						continue;
					}
					if (memberName == MemberName.Cells2)
					{
						writer.Write(this.m_cells);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000CD890 File Offset: 0x000CBA90
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(DataRegionMemberInstance.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ParentInstanceIndex)
				{
					if (memberName == MemberName.ID)
					{
						this.m_memberDef = reader.ReadGlobalReference<ReportHierarchyNode>();
						continue;
					}
					switch (memberName)
					{
					case MemberName.Cells:
						this.m_upgradedSnapshotCells = reader.ReadGenericListOfRIFObjectsUsingNew<ScalableList<DataCellInstance>>();
						base.SetReadOnlyList<DataCellInstance>(this.m_upgradedSnapshotCells);
						continue;
					case MemberName.MemberInstanceIndexWithinScopeLevel:
						this.m_memberInstanceIndexWithinScopeLevel = reader.Read7BitEncodedInt();
						continue;
					case MemberName.Children:
						this.m_children = reader.ReadGenericListOfRIFObjectsUsingNew<ScalableList<DataRegionMemberInstance>>();
						base.SetReadOnlyList<DataRegionMemberInstance>(this.m_children);
						continue;
					case MemberName.Variable:
						break;
					case MemberName.Variables:
						this.m_variables = reader.ReadVariantArray();
						continue;
					case MemberName.RecursiveLevel:
						this.m_recursiveLevel = reader.Read7BitEncodedInt();
						continue;
					case MemberName.GroupExpressionValues:
						this.m_groupExprValues = reader.ReadVariantArray();
						continue;
					default:
						if (memberName == MemberName.ParentInstanceIndex)
						{
							this.m_parentInstanceIndex = reader.ReadInt32();
							continue;
						}
						break;
					}
				}
				else if (memberName != MemberName.HasRecursiveChildren)
				{
					if (memberName == MemberName.SerializableVariables)
					{
						this.m_variables = reader.ReadSerializableArray();
						continue;
					}
					if (memberName == MemberName.Cells2)
					{
						this.m_cells = reader.ReadRIFObject<ScalableList<DataCellInstanceList>>();
						continue;
					}
				}
				else
				{
					object obj = reader.ReadVariant();
					if (obj != null)
					{
						this.m_hasRecursiveChildren = new bool?((bool)obj);
						continue;
					}
					continue;
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x000CDA0E File Offset: 0x000CBC0E
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x000CDA1B File Offset: 0x000CBC1B
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegionMemberInstance;
		}

		// Token: 0x17001587 RID: 5511
		// (get) Token: 0x06002CA9 RID: 11433 RVA: 0x000CDA24 File Offset: 0x000CBC24
		public override int Size
		{
			get
			{
				return base.Size + ItemSizes.SizeOf<ScalableList<DataRegionMemberInstance>>(this.m_children) + ItemSizes.SizeOf<DataCellInstanceList>(this.m_cells) + ItemSizes.SizeOf<ScalableList<DataCellInstance>>(this.m_upgradedSnapshotCells) + ItemSizes.SizeOf(this.m_variables) + ItemSizes.SizeOf(this.m_recursiveLevel) + ItemSizes.SizeOf(this.m_groupExprValues) + 4 + ItemSizes.NullableBoolSize + ItemSizes.ReferenceSize;
			}
		}

		// Token: 0x040017EE RID: 6126
		private int m_memberInstanceIndexWithinScopeLevel = -1;

		// Token: 0x040017EF RID: 6127
		private List<ScalableList<DataRegionMemberInstance>> m_children;

		// Token: 0x040017F0 RID: 6128
		private ScalableList<DataCellInstanceList> m_cells;

		// Token: 0x040017F1 RID: 6129
		[NonSerialized]
		private List<ScalableList<DataCellInstance>> m_upgradedSnapshotCells;

		// Token: 0x040017F2 RID: 6130
		private object[] m_variables;

		// Token: 0x040017F3 RID: 6131
		private int m_recursiveLevel;

		// Token: 0x040017F4 RID: 6132
		private object[] m_groupExprValues;

		// Token: 0x040017F5 RID: 6133
		private int m_parentInstanceIndex = -1;

		// Token: 0x040017F6 RID: 6134
		private bool? m_hasRecursiveChildren;

		// Token: 0x040017F7 RID: 6135
		[Reference]
		private ReportHierarchyNode m_memberDef;

		// Token: 0x040017F8 RID: 6136
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = DataRegionMemberInstance.GetDeclaration();
	}
}
