using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000EB RID: 235
	internal sealed class DataSetPlanGenerator : DataShapeVisitor
	{
		// Token: 0x0600097D RID: 2429 RVA: 0x00023F20 File Offset: 0x00022120
		private DataSetPlanGenerator(DataShapeAnnotations annotations, ScopeTree scopeTree, TranslationErrorContext errorContext, ExpressionTable expressionTable, ContextGraph contextGraph, ContextWeights contextWeights, DataShape rootDataShape)
		{
			this.m_annotations = annotations;
			this.m_scopeTree = scopeTree;
			this.m_errorContext = errorContext;
			this.m_expressionTable = expressionTable;
			this.m_contextGraph = contextGraph;
			this.m_contextWeights = contextWeights;
			this.m_rootDataShape = rootDataShape;
			ContextMerger contextMerger = new ContextMerger(this.m_contextWeights, annotations);
			this.m_planAffinityBlock = new DataSetPlanGenerator.PlanAffinityBlock(contextMerger);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00023F81 File Offset: 0x00022181
		public static DataSetPlanGeneratorResult Generate(DataShapeAnnotations annotations, ScopeTree scopeTree, TranslationErrorContext errorContext, ExpressionTable expressionTable, ContextGraph contextGraph, ContextWeights contextWeights, DataShape dataShape)
		{
			DataSetPlanGenerator dataSetPlanGenerator = new DataSetPlanGenerator(annotations, scopeTree, errorContext, expressionTable, contextGraph, contextWeights, dataShape);
			dataSetPlanGenerator.Visit(dataShape);
			dataSetPlanGenerator.PopulateDataShapesForDelayedOutputBinding();
			return new DataSetPlanGeneratorResult(dataSetPlanGenerator.m_planAffinityBlock.Plans);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00023FB0 File Offset: 0x000221B0
		private void PopulateDataShapesForDelayedOutputBinding()
		{
			if (this.m_dataShapesForDelayedOutputBinding == null || this.m_errorContext.HasError)
			{
				return;
			}
			for (int i = this.m_dataShapesForDelayedOutputBinding.Count - 1; i >= 0; i--)
			{
				DataShape dataShape = this.m_dataShapesForDelayedOutputBinding[i];
				IScope parentScope = this.m_scopeTree.GetParentScope(dataShape);
				DataSetPlanInfo dataSetPlanInfo;
				if (parentScope == null)
				{
					dataSetPlanInfo = this.GetOutputPlanFromNestedDataShape(dataShape);
					if (dataSetPlanInfo == null)
					{
						this.m_errorContext.Register(TranslationMessages.InvalidDataShapeNoOutputData(EngineMessageSeverity.Error, dataShape.ObjectType, dataShape.Id, "Calculations"));
						return;
					}
				}
				else
				{
					dataSetPlanInfo = this.GetOutputPlanForItem(parentScope);
				}
				dataSetPlanInfo.AddOutputItem(dataShape);
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00024048 File Offset: 0x00022248
		private DataSetPlanInfo GetOutputPlanFromNestedDataShape(DataShape dataShape)
		{
			IList<DataShape> list = this.m_scopeTree.GetChildScopes(dataShape).OfType<DataShape>().Evaluate<DataShape>();
			foreach (DataShape dataShape2 in list)
			{
				if (this.HasOutputPlan(dataShape2))
				{
					return this.GetOutputPlanForItem(dataShape2);
				}
			}
			foreach (DataShape dataShape3 in list)
			{
				DataSetPlanInfo outputPlanFromNestedDataShape = this.GetOutputPlanFromNestedDataShape(dataShape3);
				if (outputPlanFromNestedDataShape != null)
				{
					return outputPlanFromNestedDataShape;
				}
			}
			return null;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x000240FC File Offset: 0x000222FC
		protected override void Visit(DataShape dataShape)
		{
			if (dataShape.ContextOnly.GetValueOrDefault<bool>() && dataShape != this.m_rootDataShape)
			{
				return;
			}
			this.Enter(dataShape);
			base.Visit<DataShape>(dataShape.DataShapes, new Action<DataShape>(this.Visit));
			this.BeginPlanAffinityBlock();
			base.Visit(dataShape.SecondaryHierarchy);
			base.Visit<Calculation>(dataShape.Calculations, new Action<Calculation>(this.Visit));
			if (dataShape.PrimaryHierarchy != null)
			{
				this.BeginPlanAffinityBlock();
				this.TraversePrimaryHierarchyAndDataRows(dataShape, dataShape.PrimaryHierarchy.DataMembers, false);
				this.TraversePrimaryHierarchyAndDataRows(dataShape, dataShape.PrimaryHierarchy.DataMembers, true);
				this.EndPlanAffinityBlock();
			}
			this.EndPlanAffinityBlock();
			this.Exit(dataShape);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x000241B4 File Offset: 0x000223B4
		protected override void Exit(DataShape dataShape)
		{
			DataSetPlanInfo dataSetPlanInfo = this.DetermineOutputDataSetForDataShape(dataShape);
			if (dataSetPlanInfo == null)
			{
				if (this.m_dataShapesForDelayedOutputBinding == null)
				{
					this.m_dataShapesForDelayedOutputBinding = new List<DataShape>();
				}
				this.m_dataShapesForDelayedOutputBinding.Add(dataShape);
				return;
			}
			dataSetPlanInfo.AddOutputItem(dataShape);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x000241F4 File Offset: 0x000223F4
		private DataSetPlanInfo DetermineOutputDataSetForDataShape(DataShape dataShape)
		{
			DataMember dataMember = dataShape.SecondaryHierarchy.GetAllDynamicMembers().FirstOrDefault<DataMember>();
			if (dataMember != null)
			{
				return this.GetOutputPlanForItem(dataMember);
			}
			DataMember dataMember2 = dataShape.PrimaryHierarchy.GetAllDynamicMembers().FirstOrDefault<DataMember>();
			if (dataMember2 != null)
			{
				return this.GetOutputPlanForItem(dataMember2);
			}
			Calculation calculation = this.m_scopeTree.GetItems<Calculation>(dataShape.Id).FirstOrDefault<Calculation>();
			if (calculation != null)
			{
				return this.GetOutputPlanForItem(calculation);
			}
			return null;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0002425C File Offset: 0x0002245C
		private DataSetPlanInfo GetOutputPlanForItem(IContextItem item)
		{
			return this.m_planAffinityBlock.Plans.First((DataSetPlanInfo p) => p.OutputItems.Contains(item));
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00024294 File Offset: 0x00022494
		private bool HasOutputPlan(IContextItem item)
		{
			return this.m_planAffinityBlock.Plans.Any((DataSetPlanInfo p) => p.OutputItems.Contains(item));
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x000242CC File Offset: 0x000224CC
		private void TraversePrimaryHierarchyAndDataRows(DataShape dataShape, List<DataMember> primaryMembers, bool dynamics)
		{
			if (primaryMembers == null)
			{
				return;
			}
			foreach (DataMember dataMember in primaryMembers)
			{
				this.TraversePrimaryHierarchyAndDataRows(dataShape, dataMember, dynamics);
			}
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00024320 File Offset: 0x00022520
		private void TraversePrimaryHierarchyAndDataRows(DataShape dataShape, DataMember member, bool dynamics)
		{
			if (!dynamics && member.IsDynamic)
			{
				return;
			}
			bool flag = dynamics == member.IsDynamic && !member.ContextOnly;
			if (flag)
			{
				if (member.IsDynamic)
				{
					this.BeginPlanAffinityBlock();
				}
				this.Enter(member);
				base.Visit<Calculation>(member.Calculations, new Action<Calculation>(this.Visit));
				if (this.m_annotations.IsLeaf(member) && dataShape.DataRows != null)
				{
					base.Visit(dataShape.DataRows[this.m_annotations.GetLeafIndex(member)]);
				}
				base.Visit<DataShape>(member.DataShapes, new Action<DataShape>(this.Visit));
			}
			if (member.IsDynamic)
			{
				this.TraversePrimaryHierarchyAndDataRows(dataShape, member.DataMembers, false);
				this.TraversePrimaryHierarchyAndDataRows(dataShape, member.DataMembers, true);
			}
			else
			{
				this.TraversePrimaryHierarchyAndDataRows(dataShape, member.DataMembers, dynamics);
			}
			if (flag)
			{
				this.Exit(member);
				if (member.IsDynamic)
				{
					this.EndPlanAffinityBlock();
				}
			}
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00024418 File Offset: 0x00022618
		protected override void Enter(DataMember dataMember)
		{
			if (dataMember.ContextOnly)
			{
				return;
			}
			if (dataMember.IsDynamic)
			{
				if (dataMember.Group != null && dataMember.Group.DetailGroupIdentity != null)
				{
					IConceptualEntity conceptualEntity;
					dataMember.Group.DetailGroupIdentity.TryGetDetailGroupIdentityEntity(this.m_expressionTable, out conceptualEntity);
					if (!ConceptualEntityExtensions.HasStableKeys(conceptualEntity))
					{
						this.m_errorContext.Register(TranslationMessages.NoUniqueKeyForDetailTable(EngineMessageSeverity.Error, dataMember.ObjectType, dataMember.Id, "DetailGroupIdentity"));
					}
				}
				this.MergeItemIntoPlans(dataMember);
			}
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00024498 File Offset: 0x00022698
		protected override void Exit(DataIntersection dataIntersection)
		{
			if (this.m_scopeTree.HasScope(dataIntersection.Id))
			{
				DataSetPlanInfo dataSetPlanInfo;
				if ((dataIntersection.Calculations == null || dataIntersection.Calculations.Count == 0) && (dataIntersection.DataShapes == null || !dataIntersection.DataShapes.Any((DataShape d) => this.HasOutputPlan(d))) && this.IsInnermostProjectedScope(dataIntersection))
				{
					EvaluationContext evaluationContext = EvaluationContextBuilder.BuildContext(this.m_contextGraph, this.m_contextWeights, this.m_scopeTree, dataIntersection, this.m_annotations, EvaluationContextBuilderOptions.Default);
					dataSetPlanInfo = this.m_planAffinityBlock.MergeItem(dataIntersection, evaluationContext);
				}
				else
				{
					IScope primaryParentScope = this.m_scopeTree.GetPrimaryParentScope(dataIntersection);
					dataSetPlanInfo = this.GetOutputPlanForItem(primaryParentScope);
				}
				dataSetPlanInfo.AddOutputItem(dataIntersection);
			}
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0002454C File Offset: 0x0002274C
		private bool IsInnermostProjectedScope(DataIntersection dataIntersection)
		{
			IScope primaryParentScope = this.m_scopeTree.GetPrimaryParentScope(dataIntersection);
			IScope secondaryParentScope = this.m_scopeTree.GetSecondaryParentScope(dataIntersection);
			DataShape parentDataShape = this.m_scopeTree.GetParentDataShape((DataMember)primaryParentScope);
			DataMember innermostProjectedMember = DataSetPlanGenerator.GetInnermostProjectedMember(parentDataShape.PrimaryHierarchy);
			DataMember innermostProjectedMember2 = DataSetPlanGenerator.GetInnermostProjectedMember(parentDataShape.SecondaryHierarchy);
			return this.m_scopeTree.AreSameScope(primaryParentScope, innermostProjectedMember) && this.m_scopeTree.AreSameScope(secondaryParentScope, innermostProjectedMember2);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x000245B9 File Offset: 0x000227B9
		private static DataMember GetInnermostProjectedMember(DataHierarchy hierarchy)
		{
			return (from m in hierarchy.GetAllDynamicMembers()
				where !m.ContextOnly
				select m).Last<DataMember>();
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x000245EA File Offset: 0x000227EA
		protected override void Visit(Calculation calculation)
		{
			this.MergeItemIntoPlans(calculation);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x000245F4 File Offset: 0x000227F4
		private void MergeItemIntoPlans(IContextItem item)
		{
			EvaluationContext evaluationContext = EvaluationContextBuilder.BuildContext(this.m_contextGraph, this.m_contextWeights, this.m_scopeTree, item, this.m_annotations, EvaluationContextBuilderOptions.Default);
			this.m_planAffinityBlock.MergeItem(item, evaluationContext).AddOutputItem(item);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00024638 File Offset: 0x00022838
		private void BeginPlanAffinityBlock()
		{
			this.m_planAffinityBlock = this.m_planAffinityBlock.StartNewBlock();
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0002464B File Offset: 0x0002284B
		private void EndPlanAffinityBlock()
		{
			this.m_planAffinityBlock = this.m_planAffinityBlock.EndBlock();
		}

		// Token: 0x04000477 RID: 1143
		private readonly ScopeTree m_scopeTree;

		// Token: 0x04000478 RID: 1144
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x04000479 RID: 1145
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x0400047A RID: 1146
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x0400047B RID: 1147
		private readonly ContextGraph m_contextGraph;

		// Token: 0x0400047C RID: 1148
		private readonly ContextWeights m_contextWeights;

		// Token: 0x0400047D RID: 1149
		private readonly DataShape m_rootDataShape;

		// Token: 0x0400047E RID: 1150
		private DataSetPlanGenerator.PlanAffinityBlock m_planAffinityBlock;

		// Token: 0x0400047F RID: 1151
		private List<DataShape> m_dataShapesForDelayedOutputBinding;

		// Token: 0x020002B8 RID: 696
		private sealed class PlanAffinityBlock
		{
			// Token: 0x0600160F RID: 5647 RVA: 0x00050DDA File Offset: 0x0004EFDA
			internal PlanAffinityBlock(ContextMerger merger)
			{
				this.m_merger = merger;
				this.m_parentBlock = null;
				this.m_plans = new List<DataSetPlanInfo>();
				this.m_open = true;
			}

			// Token: 0x06001610 RID: 5648 RVA: 0x00050E02 File Offset: 0x0004F002
			private PlanAffinityBlock(ContextMerger merger, DataSetPlanGenerator.PlanAffinityBlock parentBlock)
				: this(merger)
			{
				this.m_parentBlock = parentBlock;
			}

			// Token: 0x170003ED RID: 1005
			// (get) Token: 0x06001611 RID: 5649 RVA: 0x00050E12 File Offset: 0x0004F012
			public List<DataSetPlanInfo> Plans
			{
				get
				{
					return this.m_plans;
				}
			}

			// Token: 0x06001612 RID: 5650 RVA: 0x00050E1A File Offset: 0x0004F01A
			public DataSetPlanGenerator.PlanAffinityBlock StartNewBlock()
			{
				return new DataSetPlanGenerator.PlanAffinityBlock(this.m_merger, this);
			}

			// Token: 0x06001613 RID: 5651 RVA: 0x00050E28 File Offset: 0x0004F028
			public DataSetPlanGenerator.PlanAffinityBlock EndBlock()
			{
				this.m_parentBlock.MergeChildBlock(this);
				this.m_open = false;
				return this.m_parentBlock;
			}

			// Token: 0x06001614 RID: 5652 RVA: 0x00050E44 File Offset: 0x0004F044
			public DataSetPlanInfo MergeItem(IContextItem item, EvaluationContext context)
			{
				foreach (DataSetPlanInfo dataSetPlanInfo in this.m_plans)
				{
					List<ContextElement> list;
					if (this.m_merger.MergeContexts(dataSetPlanInfo.Elements, context.Elements, out list))
					{
						dataSetPlanInfo.Elements = list;
						return dataSetPlanInfo;
					}
				}
				DataSetPlanInfo dataSetPlanInfo2 = new DataSetPlanInfo(item.Id.Value, context.Elements);
				this.m_plans.Add(dataSetPlanInfo2);
				return dataSetPlanInfo2;
			}

			// Token: 0x06001615 RID: 5653 RVA: 0x00050EE0 File Offset: 0x0004F0E0
			private void MergeChildBlock(DataSetPlanGenerator.PlanAffinityBlock childBlock)
			{
				List<DataSetPlanInfo> plans = childBlock.Plans;
				List<DataSetPlanInfo> list = new List<DataSetPlanInfo>();
				foreach (DataSetPlanInfo dataSetPlanInfo in this.m_plans)
				{
					bool flag = false;
					int num = 0;
					while (!flag && num < plans.Count)
					{
						DataSetPlanInfo dataSetPlanInfo2 = plans[num];
						List<ContextElement> list2;
						if (this.m_merger.MergeContexts(dataSetPlanInfo.Elements, dataSetPlanInfo2.Elements, out list2))
						{
							flag = true;
							DataSetPlanInfo dataSetPlanInfo3 = new DataSetPlanInfo(dataSetPlanInfo.PlanName, list2);
							dataSetPlanInfo3.AddOutputItems(dataSetPlanInfo.OutputItems);
							dataSetPlanInfo3.AddOutputItems(dataSetPlanInfo2.OutputItems);
							list.Add(dataSetPlanInfo3);
							plans.RemoveAt(num);
						}
						num++;
					}
					if (!flag)
					{
						list.Add(dataSetPlanInfo);
					}
				}
				list.AddRange(plans);
				this.m_plans = list;
			}

			// Token: 0x04000A5F RID: 2655
			private readonly ContextMerger m_merger;

			// Token: 0x04000A60 RID: 2656
			private readonly DataSetPlanGenerator.PlanAffinityBlock m_parentBlock;

			// Token: 0x04000A61 RID: 2657
			private List<DataSetPlanInfo> m_plans;

			// Token: 0x04000A62 RID: 2658
			private bool m_open;
		}
	}
}
