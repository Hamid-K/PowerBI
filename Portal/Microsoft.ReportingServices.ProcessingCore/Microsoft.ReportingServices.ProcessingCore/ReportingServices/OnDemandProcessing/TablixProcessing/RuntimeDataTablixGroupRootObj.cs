using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008EB RID: 2283
	[PersistedWithinRequestOnly]
	public class RuntimeDataTablixGroupRootObj : RuntimeGroupRootObj
	{
		// Token: 0x06007D5D RID: 32093 RVA: 0x00205865 File Offset: 0x00203A65
		internal RuntimeDataTablixGroupRootObj()
		{
		}

		// Token: 0x06007D5E RID: 32094 RVA: 0x00205874 File Offset: 0x00203A74
		internal RuntimeDataTablixGroupRootObj(IReference<IScope> outerScope, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode dynamicMember, ref DataActions dataAction, OnDemandProcessingContext odpContext, IReference<RuntimeMemberObj>[] innerGroupings, bool outermostStatics, int headingLevel, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
			: base(outerScope, dynamicMember, dataAction, odpContext, objectType)
		{
			this.m_innerGroupings = innerGroupings;
			this.m_headingLevel = headingLevel;
			this.m_outermostStatics = outermostStatics;
			this.m_hasLeafCells = false;
			HierarchyNodeList innerStaticMembersInSameScope = dynamicMember.InnerStaticMembersInSameScope;
			this.m_hasLeafCells = !dynamicMember.HasInnerDynamic || (innerStaticMembersInSameScope != null && innerStaticMembersInSameScope.LeafCellIndexes != null);
			if (((innerGroupings == null && innerStaticMembersInSameScope != null && innerStaticMembersInSameScope.LeafCellIndexes != null) || (innerGroupings != null && outermostStatics)) && this.m_hasLeafCells)
			{
				this.m_processStaticCellsForRVs = true;
			}
			if (this.m_hasLeafCells && outermostStatics)
			{
				this.m_processOutermostStaticCells = true;
			}
			this.NeedProcessDataActions(dynamicMember);
			this.NeedProcessDataActions(dynamicMember.InnerStaticMembersInSameScope);
			if (dynamicMember.Grouping.Filters == null)
			{
				dataAction = DataActions.None;
			}
			if ((this.m_processOutermostStaticCells || this.m_processStaticCellsForRVs) && (dynamicMember.DataRegionDef.CellPostSortAggregates != null || dynamicMember.DataRegionDef.CellRunningValues != null))
			{
				this.m_dataAction |= DataActions.PostSortAggregates;
			}
		}

		// Token: 0x170028DE RID: 10462
		// (get) Token: 0x06007D5F RID: 32095 RVA: 0x00205971 File Offset: 0x00203B71
		internal IReference<RuntimeMemberObj>[] InnerGroupings
		{
			get
			{
				return this.m_innerGroupings;
			}
		}

		// Token: 0x170028DF RID: 10463
		// (get) Token: 0x06007D60 RID: 32096 RVA: 0x00205979 File Offset: 0x00203B79
		internal int HeadingLevel
		{
			get
			{
				return this.m_headingLevel;
			}
		}

		// Token: 0x170028E0 RID: 10464
		// (get) Token: 0x06007D61 RID: 32097 RVA: 0x00205981 File Offset: 0x00203B81
		internal bool OutermostStatics
		{
			get
			{
				return this.m_outermostStatics;
			}
		}

		// Token: 0x170028E1 RID: 10465
		// (get) Token: 0x06007D62 RID: 32098 RVA: 0x00205989 File Offset: 0x00203B89
		internal bool ProcessOutermostStaticCells
		{
			get
			{
				return this.m_processOutermostStaticCells;
			}
		}

		// Token: 0x170028E2 RID: 10466
		// (get) Token: 0x06007D63 RID: 32099 RVA: 0x00205991 File Offset: 0x00203B91
		internal bool HasLeafCells
		{
			get
			{
				return this.m_hasLeafCells;
			}
		}

		// Token: 0x170028E3 RID: 10467
		// (get) Token: 0x06007D64 RID: 32100 RVA: 0x00205999 File Offset: 0x00203B99
		internal object CurrentGroupExpressionValue
		{
			get
			{
				return this.m_currentGroupExprValue;
			}
		}

		// Token: 0x170028E4 RID: 10468
		// (get) Token: 0x06007D65 RID: 32101 RVA: 0x002059A1 File Offset: 0x00203BA1
		// (set) Token: 0x06007D66 RID: 32102 RVA: 0x002059A9 File Offset: 0x00203BA9
		internal int CurrentMemberIndexWithinScopeLevel
		{
			get
			{
				return this.m_currentMemberIndexWithinScopeLevel;
			}
			set
			{
				this.m_currentMemberIndexWithinScopeLevel = value;
			}
		}

		// Token: 0x170028E5 RID: 10469
		// (get) Token: 0x06007D67 RID: 32103 RVA: 0x002059B2 File Offset: 0x00203BB2
		// (set) Token: 0x06007D68 RID: 32104 RVA: 0x002059BA File Offset: 0x00203BBA
		internal DataRegionMemberInstance CurrentMemberInstance
		{
			get
			{
				return this.m_currentMemberInstance;
			}
			set
			{
				this.m_currentMemberInstance = value;
			}
		}

		// Token: 0x06007D69 RID: 32105 RVA: 0x002059C4 File Offset: 0x00203BC4
		private void NeedProcessDataActions(HierarchyNodeList members)
		{
			if (members != null)
			{
				for (int i = 0; i < members.Count; i++)
				{
					this.NeedProcessDataActions(members[i]);
				}
			}
		}

		// Token: 0x06007D6A RID: 32106 RVA: 0x002059F2 File Offset: 0x00203BF2
		private void NeedProcessDataActions(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode memberDefinition)
		{
			if (memberDefinition != null)
			{
				this.NeedProcessDataActions(memberDefinition.RunningValues);
			}
		}

		// Token: 0x06007D6B RID: 32107 RVA: 0x00205A03 File Offset: 0x00203C03
		private void NeedProcessDataActions(List<Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo> runningValues)
		{
			if ((this.m_dataAction & DataActions.PostSortAggregates) != DataActions.None)
			{
				return;
			}
			if (runningValues != null && 0 < runningValues.Count)
			{
				this.m_dataAction |= DataActions.PostSortAggregates;
			}
		}

		// Token: 0x06007D6C RID: 32108 RVA: 0x00205A2A File Offset: 0x00203C2A
		protected override void UpdateDataRegionGroupRootInfo()
		{
			if (this.m_innerGroupings != null)
			{
				base.HierarchyDef.DataRegionDef.CurrentOuterGroupRootObjs[this.m_hierarchyDef.HierarchyDynamicIndex] = (RuntimeDataTablixGroupRootObjReference)base.SelfReference;
			}
		}

		// Token: 0x06007D6D RID: 32109 RVA: 0x00205A5B File Offset: 0x00203C5B
		internal virtual void PrepareCalculateRunningValues()
		{
			base.TraverseGroupOrSortTree(ProcessingStages.PreparePeerGroupRunningValues, null);
		}

		// Token: 0x06007D6E RID: 32110 RVA: 0x00205A68 File Offset: 0x00203C68
		internal override void CalculateRunningValues(Dictionary<string, IReference<RuntimeGroupRootObj>> groupCol, IReference<RuntimeGroupRootObj> lastGroup, AggregateUpdateContext aggContext)
		{
			base.CalculateRunningValues(groupCol, lastGroup, aggContext);
			if (this.m_processStaticCellsForRVs || this.m_processOutermostStaticCells)
			{
				this.m_hierarchyDef.DataRegionDef.ProcessOutermostStaticCellRunningValues = true;
				if (this.m_innerGroupings != null)
				{
					this.m_hierarchyDef.DataRegionDef.CurrentOuterGroupRoot = (RuntimeDataTablixGroupRootObjReference)base.SelfReference;
				}
				this.AddCellRunningValues(groupCol, ref this.m_staticCellRVs, ref this.m_staticCellPreviousValues, true);
				this.m_hierarchyDef.DataRegionDef.ProcessOutermostStaticCellRunningValues = false;
			}
			if (this.m_innerGroupings == null)
			{
				IReference<RuntimeDataTablixGroupRootObj> currentOuterGroupRoot = this.m_hierarchyDef.DataRegionDef.CurrentOuterGroupRoot;
				if (currentOuterGroupRoot != null)
				{
					this.m_hierarchyDef.DataRegionDef.ProcessCellRunningValues = true;
					this.m_cellRVs = null;
					this.m_cellPreviousValues = null;
					this.AddCellRunningValues(groupCol, ref this.m_cellRVs, ref this.m_cellPreviousValues, false);
					this.m_hierarchyDef.DataRegionDef.ProcessCellRunningValues = false;
				}
			}
			base.AddRunningValues(this.m_hierarchyDef.RunningValues);
			base.AddRunningValuesOfAggregates();
			base.TraverseGroupOrSortTree(ProcessingStages.RunningValues, aggContext);
			if (this.m_hierarchyDef.Grouping.Name != null)
			{
				groupCol.Remove(this.m_hierarchyDef.Grouping.Name);
			}
		}

		// Token: 0x06007D6F RID: 32111 RVA: 0x00205B90 File Offset: 0x00203D90
		private void AddCellRunningValues(Dictionary<string, IReference<RuntimeGroupRootObj>> groupCol, ref List<string> runningValues, ref List<string> previousValues, bool outermostStatics)
		{
			if (this.m_hierarchyDef.DataRegionDef.CellRunningValues != null && 0 < this.m_hierarchyDef.DataRegionDef.CellRunningValues.Count && base.AddRunningValues(this.m_hierarchyDef.DataRegionDef.CellRunningValues, ref runningValues, ref previousValues, groupCol, true, outermostStatics))
			{
				this.m_dataAction |= DataActions.PostSortAggregates;
			}
		}

		// Token: 0x06007D70 RID: 32112 RVA: 0x00205BF4 File Offset: 0x00203DF4
		internal override void CalculatePreviousAggregates()
		{
			if (FlagUtils.HasFlag(this.m_dataAction, DataActions.PostSortAggregates))
			{
				AggregatesImpl aggregatesImpl = this.m_odpContext.ReportObjectModel.AggregatesImpl;
				if (this.m_hierarchyDef.DataRegionDef.ProcessCellRunningValues)
				{
					if (this.m_cellPreviousValues != null)
					{
						for (int i = 0; i < this.m_cellPreviousValues.Count; i++)
						{
							string text = this.m_cellPreviousValues[i];
							Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj aggregateObj = aggregatesImpl.GetAggregateObj(text);
							Global.Tracer.Assert(aggregateObj != null, "Missing expected previous aggregate: {0}", new object[] { text });
							aggregateObj.Update();
						}
					}
					if (this.m_outerScope == null || (this.m_outerDataAction & DataActions.PostSortAggregates) == DataActions.None)
					{
						return;
					}
					using (this.m_outerScope.PinValue())
					{
						this.m_outerScope.Value().CalculatePreviousAggregates();
						return;
					}
				}
				if (this.m_staticCellPreviousValues != null)
				{
					for (int j = 0; j < this.m_staticCellPreviousValues.Count; j++)
					{
						string text2 = this.m_staticCellPreviousValues[j];
						Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj aggregateObj2 = aggregatesImpl.GetAggregateObj(text2);
						Global.Tracer.Assert(aggregateObj2 != null, "Missing expected previous aggregate: {0}", new object[] { text2 });
						aggregateObj2.Update();
					}
				}
				base.CalculatePreviousAggregates();
			}
		}

		// Token: 0x06007D71 RID: 32113 RVA: 0x00205D48 File Offset: 0x00203F48
		public override void ReadRow(DataActions dataAction, ITraversalContext context)
		{
			if (FlagUtils.HasFlag(dataAction, DataActions.PostSortAggregates) && FlagUtils.HasFlag(this.m_dataAction, DataActions.PostSortAggregates))
			{
				AggregatesImpl aggregatesImpl = this.m_odpContext.ReportObjectModel.AggregatesImpl;
				if (this.m_hierarchyDef.DataRegionDef.ProcessCellRunningValues)
				{
					if (this.m_cellRVs != null)
					{
						for (int i = 0; i < this.m_cellRVs.Count; i++)
						{
							string text = this.m_cellRVs[i];
							Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj aggregateObj = aggregatesImpl.GetAggregateObj(text);
							Global.Tracer.Assert(aggregateObj != null, "Missing expected running value: {0}", new object[] { text });
							aggregateObj.Update();
						}
					}
					if (this.m_outerScope == null || this.m_hierarchyDef.DataRegionDef.CellPostSortAggregates == null)
					{
						return;
					}
					using (this.m_outerScope.PinValue())
					{
						this.m_outerScope.Value().ReadRow(dataAction, context);
						return;
					}
				}
				if (this.m_staticCellRVs != null)
				{
					for (int j = 0; j < this.m_staticCellRVs.Count; j++)
					{
						string text2 = this.m_staticCellRVs[j];
						Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj aggregateObj2 = aggregatesImpl.GetAggregateObj(text2);
						Global.Tracer.Assert(aggregateObj2 != null, "Missing expected running value: {0}", new object[] { text2 });
						aggregateObj2.Update();
					}
				}
				base.ReadRow(dataAction, context);
			}
		}

		// Token: 0x06007D72 RID: 32114 RVA: 0x00205EB4 File Offset: 0x002040B4
		internal void DoneReadingRows(ref Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[] runningValueValues, ref Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[] runningValueOfAggregateValues, ref Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[] cellRunningValueValues)
		{
			AggregatesImpl aggregatesImpl = this.m_odpContext.ReportObjectModel.AggregatesImpl;
			RuntimeRICollection.StoreRunningValues(aggregatesImpl, this.m_hierarchyDef.RunningValues, ref runningValueValues);
			if (this.m_hierarchyDef.DataScopeInfo != null)
			{
				RuntimeRICollection.StoreRunningValues(aggregatesImpl, this.m_hierarchyDef.DataScopeInfo.RunningValuesOfAggregates, ref runningValueOfAggregateValues);
			}
			int num = ((this.m_staticCellPreviousValues != null) ? this.m_staticCellPreviousValues.Count : 0);
			int num2 = ((this.m_staticCellRVs != null) ? this.m_staticCellRVs.Count : 0);
			if (num2 > 0)
			{
				cellRunningValueValues = new Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[num2 + num];
				for (int i = 0; i < num2; i++)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj aggregateObj = aggregatesImpl.GetAggregateObj(this.m_staticCellRVs[i]);
					cellRunningValueValues[i] = aggregateObj.AggregateResult();
				}
			}
			if (num > 0)
			{
				if (cellRunningValueValues == null)
				{
					cellRunningValueValues = new Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[num];
				}
				for (int j = 0; j < num; j++)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj aggregateObj2 = aggregatesImpl.GetAggregateObj(this.m_staticCellPreviousValues[j]);
					cellRunningValueValues[num2 + j] = aggregateObj2.AggregateResult();
				}
			}
		}

		// Token: 0x06007D73 RID: 32115 RVA: 0x00205FB4 File Offset: 0x002041B4
		internal void SetupCellRunningValues(Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[] cellRunningValueValues)
		{
			if (cellRunningValueValues != null)
			{
				AggregatesImpl aggregatesImpl = this.m_odpContext.ReportObjectModel.AggregatesImpl;
				int num = ((this.m_staticCellPreviousValues != null) ? this.m_staticCellPreviousValues.Count : 0);
				int num2 = ((this.m_staticCellRVs != null) ? this.m_staticCellRVs.Count : 0);
				if (num2 > 0)
				{
					for (int i = 0; i < num2; i++)
					{
						string text = this.m_staticCellRVs[i];
						Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj aggregateObj = aggregatesImpl.GetAggregateObj(text);
						Global.Tracer.Assert(aggregateObj != null, "Missing expected running value: {0}", new object[] { text });
						aggregateObj.Set(cellRunningValueValues[i]);
					}
				}
				if (num > 0)
				{
					for (int j = 0; j < num; j++)
					{
						string text2 = this.m_staticCellPreviousValues[j];
						Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj aggregateObj2 = aggregatesImpl.GetAggregateObj(text2);
						Global.Tracer.Assert(aggregateObj2 != null, "Missing expected running value: {0}", new object[] { text2 });
						aggregateObj2.Set(cellRunningValueValues[num2 + j]);
					}
				}
			}
		}

		// Token: 0x06007D74 RID: 32116 RVA: 0x002060B0 File Offset: 0x002042B0
		internal bool GetCellTargetForNonDetailSort()
		{
			bool flag;
			using (this.m_outerScope.PinValue())
			{
				IScope scope = this.m_outerScope.Value();
				if (scope is RuntimeTablixObj)
				{
					flag = scope.TargetForNonDetailSort;
				}
				else
				{
					Global.Tracer.Assert(scope is RuntimeTablixGroupLeafObj, "(outerScopeObj is RuntimeTablixGroupLeafObj)");
					flag = ((RuntimeTablixGroupLeafObj)scope).GetCellTargetForNonDetailSort();
				}
			}
			return flag;
		}

		// Token: 0x06007D75 RID: 32117 RVA: 0x00206128 File Offset: 0x00204328
		internal bool GetCellTargetForSort(int index, bool detailSort)
		{
			bool flag;
			using (this.m_outerScope.PinValue())
			{
				IScope scope = this.m_outerScope.Value();
				if (scope is RuntimeTablixObj)
				{
					flag = scope.IsTargetForSort(index, detailSort);
				}
				else
				{
					Global.Tracer.Assert(scope is RuntimeTablixGroupLeafObj, "(outerScopeObj is RuntimeTablixGroupLeafObj)");
					flag = ((RuntimeTablixGroupLeafObj)scope).GetCellTargetForSort(index, detailSort);
				}
			}
			return flag;
		}

		// Token: 0x06007D76 RID: 32118 RVA: 0x002061A4 File Offset: 0x002043A4
		internal int GetRecursiveParentIndex(int recursiveLevel)
		{
			return this.m_recursiveParentIndexes[recursiveLevel];
		}

		// Token: 0x06007D77 RID: 32119 RVA: 0x002061B2 File Offset: 0x002043B2
		internal void SetRecursiveParentIndex(int instanceIndex, int recursiveLevel)
		{
			if (this.m_recursiveParentIndexes == null)
			{
				this.m_recursiveParentIndexes = new List<int>();
			}
			while (recursiveLevel >= this.m_recursiveParentIndexes.Count)
			{
				this.m_recursiveParentIndexes.Add(-1);
			}
			this.m_recursiveParentIndexes[recursiveLevel] = instanceIndex;
		}

		// Token: 0x06007D78 RID: 32120 RVA: 0x002061F0 File Offset: 0x002043F0
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeDataTablixGroupRootObj.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.CurrentMemberIndexWithinScopeLevel)
				{
					if (memberName == MemberName.CellRunningValues)
					{
						writer.WriteListOfPrimitives<string>(this.m_cellRVs);
						continue;
					}
					switch (memberName)
					{
					case MemberName.InnerGroupings:
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[] innerGroupings = this.m_innerGroupings;
						writer.Write(innerGroupings);
						continue;
					}
					case MemberName.HeadingLevel:
						writer.Write(this.m_headingLevel);
						continue;
					case MemberName.OutermostStatics:
						writer.Write(this.m_outermostStatics);
						continue;
					case MemberName.HasLeafCells:
						writer.Write(this.m_hasLeafCells);
						continue;
					case MemberName.ProcessOutermostStaticCells:
						writer.Write(this.m_processOutermostStaticCells);
						continue;
					case MemberName.CurrentMemberIndexWithinScopeLevel:
						writer.Write(this.m_currentMemberIndexWithinScopeLevel);
						continue;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.StaticCellRunningValues:
						writer.WriteListOfPrimitives<string>(this.m_staticCellRVs);
						continue;
					case MemberName.CellPreviousValues:
						writer.WriteListOfPrimitives<string>(this.m_cellPreviousValues);
						continue;
					case MemberName.StaticCellPreviousValues:
						writer.WriteListOfPrimitives<string>(this.m_staticCellPreviousValues);
						continue;
					case MemberName.DetailRowIndex:
					case MemberName.DetailUserSortTargetInfo:
					case MemberName.InstanceIndex:
						break;
					case MemberName.RecursiveParentIndexes:
						writer.WriteListOfPrimitives<int>(this.m_recursiveParentIndexes);
						continue;
					default:
						if (memberName == MemberName.ProcessStaticCellsForRVs)
						{
							writer.Write(this.m_processStaticCellsForRVs);
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007D79 RID: 32121 RVA: 0x00206370 File Offset: 0x00204570
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeDataTablixGroupRootObj.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.CurrentMemberIndexWithinScopeLevel)
				{
					if (memberName == MemberName.CellRunningValues)
					{
						this.m_cellRVs = reader.ReadListOfPrimitives<string>();
						continue;
					}
					switch (memberName)
					{
					case MemberName.InnerGroupings:
						this.m_innerGroupings = reader.ReadArrayOfRIFObjects<IReference<RuntimeMemberObj>>();
						continue;
					case MemberName.HeadingLevel:
						this.m_headingLevel = reader.ReadInt32();
						continue;
					case MemberName.OutermostStatics:
						this.m_outermostStatics = reader.ReadBoolean();
						continue;
					case MemberName.HasLeafCells:
						this.m_hasLeafCells = reader.ReadBoolean();
						continue;
					case MemberName.ProcessOutermostStaticCells:
						this.m_processOutermostStaticCells = reader.ReadBoolean();
						continue;
					case MemberName.CurrentMemberIndexWithinScopeLevel:
						this.m_currentMemberIndexWithinScopeLevel = reader.ReadInt32();
						continue;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.StaticCellRunningValues:
						this.m_staticCellRVs = reader.ReadListOfPrimitives<string>();
						continue;
					case MemberName.CellPreviousValues:
						this.m_cellPreviousValues = reader.ReadListOfPrimitives<string>();
						continue;
					case MemberName.StaticCellPreviousValues:
						this.m_staticCellPreviousValues = reader.ReadListOfPrimitives<string>();
						continue;
					case MemberName.DetailRowIndex:
					case MemberName.DetailUserSortTargetInfo:
					case MemberName.InstanceIndex:
						break;
					case MemberName.RecursiveParentIndexes:
						this.m_recursiveParentIndexes = reader.ReadListOfPrimitives<int>();
						continue;
					default:
						if (memberName == MemberName.ProcessStaticCellsForRVs)
						{
							this.m_processStaticCellsForRVs = reader.ReadBoolean();
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007D7A RID: 32122 RVA: 0x002064EB File Offset: 0x002046EB
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06007D7B RID: 32123 RVA: 0x002064F5 File Offset: 0x002046F5
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixGroupRootObj;
		}

		// Token: 0x06007D7C RID: 32124 RVA: 0x002064FC File Offset: 0x002046FC
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeDataTablixGroupRootObj.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixGroupRootObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupRootObj, new List<MemberInfo>
				{
					new MemberInfo(MemberName.InnerGroupings, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeMemberObjReference),
					new MemberInfo(MemberName.CellRunningValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.String),
					new MemberInfo(MemberName.StaticCellRunningValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.String),
					new MemberInfo(MemberName.CellPreviousValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.String),
					new MemberInfo(MemberName.StaticCellPreviousValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.String),
					new MemberInfo(MemberName.HeadingLevel, Token.Int32),
					new MemberInfo(MemberName.OutermostStatics, Token.Boolean),
					new MemberInfo(MemberName.HasLeafCells, Token.Boolean),
					new MemberInfo(MemberName.ProcessOutermostStaticCells, Token.Boolean),
					new MemberInfo(MemberName.CurrentMemberIndexWithinScopeLevel, Token.Int32),
					new MemberInfo(MemberName.RecursiveParentIndexes, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Int32),
					new MemberInfo(MemberName.ProcessStaticCellsForRVs, Token.Boolean)
				});
			}
			return RuntimeDataTablixGroupRootObj.m_declaration;
		}

		// Token: 0x170028E6 RID: 10470
		// (get) Token: 0x06007D7D RID: 32125 RVA: 0x0020662C File Offset: 0x0020482C
		public override int Size
		{
			get
			{
				return base.Size + ItemSizes.SizeOf<IReference<RuntimeMemberObj>>(this.m_innerGroupings) + ItemSizes.SizeOf(this.m_cellRVs) + ItemSizes.SizeOf(this.m_staticCellRVs) + ItemSizes.SizeOf(this.m_cellPreviousValues) + ItemSizes.SizeOf(this.m_staticCellPreviousValues) + ItemSizes.SizeOf(this.m_recursiveParentIndexes) + 4 + 1 + 1 + 1 + 4 + 1 + ItemSizes.ReferenceSize;
			}
		}

		// Token: 0x04003DEE RID: 15854
		private List<int> m_recursiveParentIndexes;

		// Token: 0x04003DEF RID: 15855
		private IReference<RuntimeMemberObj>[] m_innerGroupings;

		// Token: 0x04003DF0 RID: 15856
		private List<string> m_cellRVs;

		// Token: 0x04003DF1 RID: 15857
		private List<string> m_staticCellRVs;

		// Token: 0x04003DF2 RID: 15858
		private List<string> m_cellPreviousValues;

		// Token: 0x04003DF3 RID: 15859
		private List<string> m_staticCellPreviousValues;

		// Token: 0x04003DF4 RID: 15860
		private int m_headingLevel;

		// Token: 0x04003DF5 RID: 15861
		private bool m_outermostStatics;

		// Token: 0x04003DF6 RID: 15862
		private bool m_hasLeafCells;

		// Token: 0x04003DF7 RID: 15863
		private bool m_processOutermostStaticCells;

		// Token: 0x04003DF8 RID: 15864
		private bool m_processStaticCellsForRVs;

		// Token: 0x04003DF9 RID: 15865
		private int m_currentMemberIndexWithinScopeLevel = -1;

		// Token: 0x04003DFA RID: 15866
		[NonSerialized]
		private DataRegionMemberInstance m_currentMemberInstance;

		// Token: 0x04003DFB RID: 15867
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeDataTablixGroupRootObj.GetDeclaration();
	}
}
