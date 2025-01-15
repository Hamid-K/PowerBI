using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008EC RID: 2284
	[PersistedWithinRequestOnly]
	public class RuntimeHierarchyObj : RuntimeDataRegionObj, IHierarchyObj, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007D7F RID: 32127 RVA: 0x002066A5 File Offset: 0x002048A5
		internal RuntimeHierarchyObj()
		{
		}

		// Token: 0x06007D80 RID: 32128 RVA: 0x002066AD File Offset: 0x002048AD
		protected RuntimeHierarchyObj(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, int level)
			: base(odpContext, objectType, level)
		{
		}

		// Token: 0x06007D81 RID: 32129 RVA: 0x002066B8 File Offset: 0x002048B8
		internal RuntimeHierarchyObj(RuntimeHierarchyObj outerHierarchy, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, int level)
			: base(outerHierarchy.OdpContext, objectType, level)
		{
			if (outerHierarchy.m_expression != null)
			{
				this.ConstructorHelper(outerHierarchy.m_expression.ExpressionIndex + 1, outerHierarchy.m_hierarchyRoot);
				return;
			}
			this.ConstructorHelper(-1, outerHierarchy.m_hierarchyRoot);
		}

		// Token: 0x170028E7 RID: 10471
		// (get) Token: 0x06007D82 RID: 32130 RVA: 0x002066F7 File Offset: 0x002048F7
		internal List<IReference<RuntimeHierarchyObj>> HierarchyObjs
		{
			get
			{
				return this.m_hierarchyObjs;
			}
		}

		// Token: 0x170028E8 RID: 10472
		// (get) Token: 0x06007D83 RID: 32131 RVA: 0x002066FF File Offset: 0x002048FF
		protected override IReference<IScope> OuterScope
		{
			get
			{
				Global.Tracer.Assert(false);
				return null;
			}
		}

		// Token: 0x170028E9 RID: 10473
		// (get) Token: 0x06007D84 RID: 32132 RVA: 0x0020670D File Offset: 0x0020490D
		protected virtual IReference<IHierarchyObj> HierarchyRoot
		{
			get
			{
				return this.m_hierarchyRoot;
			}
		}

		// Token: 0x170028EA RID: 10474
		// (get) Token: 0x06007D85 RID: 32133 RVA: 0x00206715 File Offset: 0x00204915
		internal RuntimeGroupingObj Grouping
		{
			get
			{
				return this.m_grouping;
			}
		}

		// Token: 0x170028EB RID: 10475
		// (get) Token: 0x06007D86 RID: 32134 RVA: 0x0020671D File Offset: 0x0020491D
		protected virtual BTree SortTree
		{
			get
			{
				return this.m_grouping.Tree;
			}
		}

		// Token: 0x170028EC RID: 10476
		// (get) Token: 0x06007D87 RID: 32135 RVA: 0x0020672A File Offset: 0x0020492A
		protected virtual int ExpressionIndex
		{
			get
			{
				if (this.m_expression != null)
				{
					return this.m_expression.ExpressionIndex;
				}
				return 0;
			}
		}

		// Token: 0x170028ED RID: 10477
		// (get) Token: 0x06007D88 RID: 32136 RVA: 0x00206741 File Offset: 0x00204941
		protected virtual List<DataFieldRow> SortDataRows
		{
			get
			{
				Global.Tracer.Assert(false);
				return null;
			}
		}

		// Token: 0x170028EE RID: 10478
		// (get) Token: 0x06007D89 RID: 32137 RVA: 0x0020674F File Offset: 0x0020494F
		protected virtual List<int> SortFilterInfoIndices
		{
			get
			{
				Global.Tracer.Assert(false);
				return null;
			}
		}

		// Token: 0x170028EF RID: 10479
		// (get) Token: 0x06007D8A RID: 32138 RVA: 0x0020675D File Offset: 0x0020495D
		protected virtual bool IsDetail
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170028F0 RID: 10480
		// (get) Token: 0x06007D8B RID: 32139 RVA: 0x00206760 File Offset: 0x00204960
		IReference<IHierarchyObj> IHierarchyObj.HierarchyRoot
		{
			get
			{
				return this.HierarchyRoot;
			}
		}

		// Token: 0x170028F1 RID: 10481
		// (get) Token: 0x06007D8C RID: 32140 RVA: 0x00206768 File Offset: 0x00204968
		OnDemandProcessingContext IHierarchyObj.OdpContext
		{
			get
			{
				return this.m_odpContext;
			}
		}

		// Token: 0x170028F2 RID: 10482
		// (get) Token: 0x06007D8D RID: 32141 RVA: 0x00206770 File Offset: 0x00204970
		BTree IHierarchyObj.SortTree
		{
			get
			{
				return this.SortTree;
			}
		}

		// Token: 0x170028F3 RID: 10483
		// (get) Token: 0x06007D8E RID: 32142 RVA: 0x00206778 File Offset: 0x00204978
		int IHierarchyObj.ExpressionIndex
		{
			get
			{
				return this.ExpressionIndex;
			}
		}

		// Token: 0x170028F4 RID: 10484
		// (get) Token: 0x06007D8F RID: 32143 RVA: 0x00206780 File Offset: 0x00204980
		List<int> IHierarchyObj.SortFilterInfoIndices
		{
			get
			{
				return this.SortFilterInfoIndices;
			}
		}

		// Token: 0x170028F5 RID: 10485
		// (get) Token: 0x06007D90 RID: 32144 RVA: 0x00206788 File Offset: 0x00204988
		bool IHierarchyObj.IsDetail
		{
			get
			{
				return this.IsDetail;
			}
		}

		// Token: 0x170028F6 RID: 10486
		// (get) Token: 0x06007D91 RID: 32145 RVA: 0x00206790 File Offset: 0x00204990
		bool IHierarchyObj.InDataRowSortPhase
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007D92 RID: 32146 RVA: 0x00206793 File Offset: 0x00204993
		ProcessingMessageList IHierarchyObj.RegisterComparisonError(string propertyName)
		{
			return this.RegisterComparisonError(propertyName);
		}

		// Token: 0x06007D93 RID: 32147 RVA: 0x0020679C File Offset: 0x0020499C
		void IHierarchyObj.NextRow(IHierarchyObj owner)
		{
			this.NextRow();
		}

		// Token: 0x06007D94 RID: 32148 RVA: 0x002067A4 File Offset: 0x002049A4
		void IHierarchyObj.Traverse(ProcessingStages operation, ITraversalContext traversalContext)
		{
			switch (operation)
			{
			case ProcessingStages.SortAndFilter:
				this.SortAndFilter((AggregateUpdateContext)traversalContext);
				return;
			case ProcessingStages.RunningValues:
				this.CalculateRunningValues((AggregateUpdateContext)traversalContext);
				return;
			case ProcessingStages.UpdateAggregates:
				this.UpdateAggregates((AggregateUpdateContext)traversalContext);
				return;
			case ProcessingStages.CreateGroupTree:
				this.CreateInstances((CreateInstancesTraversalContext)traversalContext);
				return;
			}
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007D95 RID: 32149 RVA: 0x00206813 File Offset: 0x00204A13
		void IHierarchyObj.ReadRow()
		{
			this.ReadRow(DataActions.UserSort, null);
		}

		// Token: 0x06007D96 RID: 32150 RVA: 0x0020681D File Offset: 0x00204A1D
		void IHierarchyObj.ProcessUserSort()
		{
			this.ProcessUserSort();
		}

		// Token: 0x06007D97 RID: 32151 RVA: 0x00206825 File Offset: 0x00204A25
		void IHierarchyObj.MarkSortInfoProcessed(List<IReference<RuntimeSortFilterEventInfo>> runtimeSortFilterInfo)
		{
			this.MarkSortInfoProcessed(runtimeSortFilterInfo);
		}

		// Token: 0x06007D98 RID: 32152 RVA: 0x0020682E File Offset: 0x00204A2E
		void IHierarchyObj.AddSortInfoIndex(int sortInfoIndex, IReference<RuntimeSortFilterEventInfo> sortInfo)
		{
			this.AddSortInfoIndex(sortInfoIndex, sortInfo);
		}

		// Token: 0x06007D99 RID: 32153 RVA: 0x00206838 File Offset: 0x00204A38
		private void ConstructorHelper(int exprIndex, RuntimeHierarchyObjReference hierarchyRoot)
		{
			this.m_hierarchyRoot = hierarchyRoot;
			using (this.m_hierarchyRoot.PinValue())
			{
				RuntimeGroupRootObj runtimeGroupRootObj = this.m_hierarchyRoot.Value() as RuntimeGroupRootObj;
				Global.Tracer.Assert(runtimeGroupRootObj != null, "(null != groupRoot)");
				List<Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo> list;
				IndexedExprHost indexedExprHost;
				List<bool> list2;
				if (ProcessingStages.Grouping == runtimeGroupRootObj.ProcessingStage)
				{
					list = runtimeGroupRootObj.GroupExpressions;
					indexedExprHost = runtimeGroupRootObj.GroupExpressionHost;
					list2 = runtimeGroupRootObj.GroupDirections;
				}
				else
				{
					Global.Tracer.Assert(ProcessingStages.SortAndFilter == runtimeGroupRootObj.ProcessingStage, "(ProcessingStages.SortAndFilter == groupRoot.ProcessingStage)");
					list = runtimeGroupRootObj.SortExpressions;
					indexedExprHost = runtimeGroupRootObj.SortExpressionHost;
					list2 = runtimeGroupRootObj.SortDirections;
				}
				if (exprIndex == -1 || exprIndex >= list.Count)
				{
					this.m_hierarchyObjs = new List<IReference<RuntimeHierarchyObj>>();
					RuntimeGroupLeafObjReference runtimeGroupLeafObjReference = null;
					IScalabilityCache tablixProcessingScalabilityCache = this.m_odpContext.TablixProcessingScalabilityCache;
					if (ProcessingStages.Grouping == runtimeGroupRootObj.ProcessingStage)
					{
						runtimeGroupLeafObjReference = runtimeGroupRootObj.CreateGroupLeaf();
						if (!runtimeGroupRootObj.HasParent)
						{
							runtimeGroupRootObj.AddChildWithNoParent(runtimeGroupLeafObjReference);
						}
					}
					if (null != runtimeGroupLeafObjReference)
					{
						this.m_hierarchyObjs.Add(runtimeGroupLeafObjReference);
					}
				}
				else
				{
					this.m_expression = new RuntimeExpressionInfo(list, indexedExprHost, list2, exprIndex);
					this.m_grouping = RuntimeGroupingObj.CreateGroupingObj(runtimeGroupRootObj.GroupingType, this, this.m_objectType);
				}
			}
		}

		// Token: 0x06007D9A RID: 32154 RVA: 0x00206988 File Offset: 0x00204B88
		internal ProcessingMessageList RegisterComparisonError(string propertyName)
		{
			return this.RegisterComparisonError(propertyName, null);
		}

		// Token: 0x06007D9B RID: 32155 RVA: 0x00206994 File Offset: 0x00204B94
		internal ProcessingMessageList RegisterComparisonError(string propertyName, ReportProcessingException_ComparisonError e)
		{
			Microsoft.ReportingServices.ReportProcessing.ObjectType objectType;
			string name;
			using (this.m_hierarchyRoot.PinValue())
			{
				RuntimeGroupRootObj runtimeGroupRootObj = (RuntimeGroupRootObj)this.m_hierarchyRoot.Value();
				objectType = runtimeGroupRootObj.HierarchyDef.DataRegionDef.ObjectType;
				name = runtimeGroupRootObj.HierarchyDef.DataRegionDef.Name;
			}
			return this.m_odpContext.RegisterComparisonError(e, objectType, name, propertyName);
		}

		// Token: 0x06007D9C RID: 32156 RVA: 0x00206A0C File Offset: 0x00204C0C
		internal ProcessingMessageList RegisterSpatialTypeComparisonError(string type)
		{
			Microsoft.ReportingServices.ReportProcessing.ObjectType objectType;
			string name;
			using (this.m_hierarchyRoot.PinValue())
			{
				RuntimeGroupRootObj runtimeGroupRootObj = (RuntimeGroupRootObj)this.m_hierarchyRoot.Value();
				objectType = runtimeGroupRootObj.HierarchyDef.DataRegionDef.ObjectType;
				name = runtimeGroupRootObj.HierarchyDef.DataRegionDef.Name;
			}
			return this.m_odpContext.RegisterSpatialTypeComparisonError(objectType, name, type);
		}

		// Token: 0x06007D9D RID: 32157 RVA: 0x00206A80 File Offset: 0x00204C80
		internal override void NextRow()
		{
			bool flag = true;
			RuntimeGroupRootObj runtimeGroupRootObj = null;
			using (this.m_hierarchyRoot.PinValue())
			{
				if (this.m_hierarchyRoot is RuntimeGroupRootObjReference)
				{
					runtimeGroupRootObj = (RuntimeGroupRootObj)this.m_hierarchyRoot.Value();
					if (ProcessingStages.SortAndFilter == runtimeGroupRootObj.ProcessingStage)
					{
						flag = false;
					}
				}
				if (this.m_hierarchyObjs != null)
				{
					if (flag)
					{
						IReference<RuntimeHierarchyObj> reference = this.m_hierarchyObjs[0];
						Global.Tracer.Assert(reference != null, "(null != hierarchyObj)");
						using (reference.PinValue())
						{
							reference.Value().NextRow();
							return;
						}
					}
					if (runtimeGroupRootObj != null)
					{
						RuntimeGroupLeafObjReference lastChild = runtimeGroupRootObj.LastChild;
						Global.Tracer.Assert(null != lastChild, "(null != groupLastChild)");
						this.m_hierarchyObjs.Add(lastChild);
					}
				}
				else if (this.m_grouping != null)
				{
					Microsoft.ReportingServices.ReportProcessing.ObjectType objectType = runtimeGroupRootObj.HierarchyDef.DataRegionDef.ObjectType;
					string name = runtimeGroupRootObj.HierarchyDef.DataRegionDef.Name;
					string text = "GroupExpression";
					DomainScopeContext domainScopeContext = base.OdpContext.DomainScopeContext;
					DomainScopeContext.DomainScopeInfo domainScopeInfo = null;
					if (domainScopeContext != null)
					{
						domainScopeInfo = domainScopeContext.CurrentDomainScope;
					}
					object obj;
					if (domainScopeInfo != null)
					{
						domainScopeInfo.MoveNext();
						obj = domainScopeInfo.CurrentKey;
					}
					else if (this.m_expression == null)
					{
						obj = this.m_odpContext.ReportObjectModel.FieldsImpl.GetRowIndex();
					}
					else
					{
						obj = this.m_odpContext.ReportRuntime.EvaluateRuntimeExpression(this.m_expression, objectType, name, text);
					}
					if (runtimeGroupRootObj != null && flag)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = runtimeGroupRootObj.HierarchyDef.Grouping;
						if (runtimeGroupRootObj.SaveGroupExprValues)
						{
							grouping.CurrentGroupExpressionValues.Add(obj);
						}
						this.MatchSortFilterScope(runtimeGroupRootObj.SelfReference, grouping, obj, this.m_expression.ExpressionIndex);
					}
					this.m_grouping.NextRow(obj);
					if (domainScopeInfo != null)
					{
						domainScopeInfo.MovePrevious();
					}
				}
			}
		}

		// Token: 0x06007D9E RID: 32158 RVA: 0x00206C98 File Offset: 0x00204E98
		internal override bool SortAndFilter(AggregateUpdateContext aggContext)
		{
			this.Traverse(ProcessingStages.SortAndFilter, aggContext);
			return true;
		}

		// Token: 0x06007D9F RID: 32159 RVA: 0x00206CA3 File Offset: 0x00204EA3
		public override void UpdateAggregates(AggregateUpdateContext aggContext)
		{
			this.Traverse(ProcessingStages.UpdateAggregates, aggContext);
		}

		// Token: 0x06007DA0 RID: 32160 RVA: 0x00206CB0 File Offset: 0x00204EB0
		private void Traverse(ProcessingStages operation, AggregateUpdateContext aggContext)
		{
			if (this.m_grouping != null)
			{
				this.m_grouping.Traverse(ProcessingStages.SortAndFilter, true, aggContext);
			}
			if (this.m_hierarchyObjs != null)
			{
				for (int i = 0; i < this.m_hierarchyObjs.Count; i++)
				{
					IReference<RuntimeHierarchyObj> reference = this.m_hierarchyObjs[i];
					using (reference.PinValue())
					{
						if (operation != ProcessingStages.SortAndFilter)
						{
							if (operation == ProcessingStages.UpdateAggregates)
							{
								reference.Value().UpdateAggregates(aggContext);
							}
						}
						else
						{
							reference.Value().SortAndFilter(aggContext);
						}
					}
				}
			}
		}

		// Token: 0x06007DA1 RID: 32161 RVA: 0x00206D48 File Offset: 0x00204F48
		internal virtual void CalculateRunningValues(AggregateUpdateContext aggContext)
		{
			if (this.m_grouping != null)
			{
				this.m_grouping.Traverse(ProcessingStages.RunningValues, this.m_expression == null || this.m_expression.Direction, aggContext);
			}
			if (this.m_hierarchyObjs != null)
			{
				bool flag = true;
				for (int i = 0; i < this.m_hierarchyObjs.Count; i++)
				{
					IReference<RuntimeHierarchyObj> reference = this.m_hierarchyObjs[i];
					using (reference.PinValue())
					{
						RuntimeHierarchyObj runtimeHierarchyObj = reference.Value();
						if (!flag || runtimeHierarchyObj is RuntimeGroupLeafObj)
						{
							((RuntimeGroupLeafObj)runtimeHierarchyObj).TraverseAllLeafNodes(ProcessingStages.RunningValues, aggContext);
							flag = false;
						}
					}
				}
			}
		}

		// Token: 0x06007DA2 RID: 32162 RVA: 0x00206DF4 File Offset: 0x00204FF4
		internal override void CalculateRunningValues(Dictionary<string, IReference<RuntimeGroupRootObj>> groupCol, IReference<RuntimeGroupRootObj> lastGroup, AggregateUpdateContext aggContext)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007DA3 RID: 32163 RVA: 0x00206E01 File Offset: 0x00205001
		internal override void CalculatePreviousAggregates()
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007DA4 RID: 32164 RVA: 0x00206E10 File Offset: 0x00205010
		internal void CreateInstances(CreateInstancesTraversalContext traversalContext)
		{
			if (this.m_grouping != null)
			{
				this.m_grouping.Traverse(ProcessingStages.CreateGroupTree, this.m_expression == null || this.m_expression.Direction, traversalContext);
			}
			if (this.m_hierarchyObjs != null)
			{
				bool flag = true;
				for (int i = 0; i < this.m_hierarchyObjs.Count; i++)
				{
					IReference<RuntimeHierarchyObj> reference = this.m_hierarchyObjs[i];
					using (reference.PinValue())
					{
						RuntimeHierarchyObj runtimeHierarchyObj = reference.Value();
						if (!flag || runtimeHierarchyObj is RuntimeGroupLeafObj)
						{
							((RuntimeGroupLeafObj)runtimeHierarchyObj).TraverseAllLeafNodes(ProcessingStages.CreateGroupTree, traversalContext);
							flag = false;
						}
						else
						{
							((RuntimeDetailObj)runtimeHierarchyObj).CreateInstance(traversalContext);
						}
					}
				}
			}
		}

		// Token: 0x06007DA5 RID: 32165 RVA: 0x00206ECC File Offset: 0x002050CC
		internal virtual void CreateInstance(CreateInstancesTraversalContext traversalContext)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007DA6 RID: 32166 RVA: 0x00206ED9 File Offset: 0x002050D9
		public override void SetupEnvironment()
		{
		}

		// Token: 0x06007DA7 RID: 32167 RVA: 0x00206EDB File Offset: 0x002050DB
		public override void ReadRow(DataActions dataAction, ITraversalContext context)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007DA8 RID: 32168 RVA: 0x00206EE8 File Offset: 0x002050E8
		internal override bool InScope(string scope)
		{
			Global.Tracer.Assert(false);
			return false;
		}

		// Token: 0x06007DA9 RID: 32169 RVA: 0x00206EF6 File Offset: 0x002050F6
		public virtual IHierarchyObj CreateHierarchyObjForSortTree()
		{
			return new RuntimeHierarchyObj(this, this.m_objectType, this.m_depth + 1);
		}

		// Token: 0x06007DAA RID: 32170 RVA: 0x00206F0C File Offset: 0x0020510C
		protected void MatchSortFilterScope(IReference<IScope> outerScope, Microsoft.ReportingServices.ReportIntermediateFormat.Grouping groupDef, object groupExprValue, int groupExprIndex)
		{
			if (this.m_odpContext.RuntimeSortFilterInfo == null || groupDef.SortFilterScopeInfo == null)
			{
				return;
			}
			List<IReference<RuntimeSortFilterEventInfo>> runtimeSortFilterInfo = this.m_odpContext.RuntimeSortFilterInfo;
			for (int i = 0; i < runtimeSortFilterInfo.Count; i++)
			{
				List<object> list = groupDef.SortFilterScopeInfo[i];
				if (list != null && outerScope.Value().TargetScopeMatched(i, false))
				{
					if (this.m_odpContext.ProcessingComparer.Compare(list[groupExprIndex], groupExprValue) != 0)
					{
						groupDef.SortFilterScopeMatched[i] = false;
					}
				}
				else
				{
					groupDef.SortFilterScopeMatched[i] = false;
				}
			}
		}

		// Token: 0x06007DAB RID: 32171 RVA: 0x00206F96 File Offset: 0x00205196
		protected virtual void ProcessUserSort()
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007DAC RID: 32172 RVA: 0x00206FA3 File Offset: 0x002051A3
		protected virtual void MarkSortInfoProcessed(List<IReference<RuntimeSortFilterEventInfo>> runtimeSortFilterInfo)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007DAD RID: 32173 RVA: 0x00206FB0 File Offset: 0x002051B0
		protected virtual void AddSortInfoIndex(int sortInfoIndex, IReference<RuntimeSortFilterEventInfo> sortInfo)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007DAE RID: 32174 RVA: 0x00206FC0 File Offset: 0x002051C0
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeHierarchyObj.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Grouping)
				{
					if (memberName == MemberName.HierarchyRoot)
					{
						writer.Write(this.m_hierarchyRoot);
						continue;
					}
					if (memberName == MemberName.Grouping)
					{
						writer.Write(this.m_grouping);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Expression)
					{
						writer.Write(this.m_expression);
						continue;
					}
					if (memberName == MemberName.HierarchyObjs)
					{
						writer.Write<IReference<RuntimeHierarchyObj>>(this.m_hierarchyObjs);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007DAF RID: 32175 RVA: 0x0020706C File Offset: 0x0020526C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeHierarchyObj.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Grouping)
				{
					if (memberName == MemberName.HierarchyRoot)
					{
						this.m_hierarchyRoot = (RuntimeHierarchyObjReference)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Grouping)
					{
						this.m_grouping = (RuntimeGroupingObj)reader.ReadRIFObject();
						if (this.m_grouping != null)
						{
							this.m_grouping.SetOwner(this);
							continue;
						}
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Expression)
					{
						this.m_expression = (RuntimeExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.HierarchyObjs)
					{
						this.m_hierarchyObjs = reader.ReadListOfRIFObjects<List<IReference<RuntimeHierarchyObj>>>();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007DB0 RID: 32176 RVA: 0x0020713D File Offset: 0x0020533D
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007DB1 RID: 32177 RVA: 0x0020713F File Offset: 0x0020533F
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeHierarchyObj;
		}

		// Token: 0x06007DB2 RID: 32178 RVA: 0x00207144 File Offset: 0x00205344
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeHierarchyObj.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeHierarchyObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataRegionObj, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Grouping, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObj),
					new MemberInfo(MemberName.Expression, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeExpressionInfo),
					new MemberInfo(MemberName.HierarchyRoot, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeHierarchyObjReference),
					new MemberInfo(MemberName.HierarchyObjs, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeHierarchyObjReference)
				});
			}
			return RuntimeHierarchyObj.m_declaration;
		}

		// Token: 0x170028F7 RID: 10487
		// (get) Token: 0x06007DB3 RID: 32179 RVA: 0x002071B7 File Offset: 0x002053B7
		public override int Size
		{
			get
			{
				return base.Size + ItemSizes.SizeOf(this.m_grouping) + ItemSizes.SizeOf(this.m_expression) + ItemSizes.SizeOf(this.m_hierarchyRoot) + ItemSizes.SizeOf<IReference<RuntimeHierarchyObj>>(this.m_hierarchyObjs);
			}
		}

		// Token: 0x04003DFC RID: 15868
		protected RuntimeGroupingObj m_grouping;

		// Token: 0x04003DFD RID: 15869
		protected RuntimeExpressionInfo m_expression;

		// Token: 0x04003DFE RID: 15870
		protected RuntimeHierarchyObjReference m_hierarchyRoot;

		// Token: 0x04003DFF RID: 15871
		protected List<IReference<RuntimeHierarchyObj>> m_hierarchyObjs;

		// Token: 0x04003E00 RID: 15872
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeHierarchyObj.GetDeclaration();
	}
}
