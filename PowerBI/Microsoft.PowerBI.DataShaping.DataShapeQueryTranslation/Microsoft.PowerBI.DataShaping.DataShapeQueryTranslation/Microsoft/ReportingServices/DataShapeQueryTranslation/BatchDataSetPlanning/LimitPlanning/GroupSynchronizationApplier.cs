using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.LimitPlanning
{
	// Token: 0x0200023E RID: 574
	internal sealed class GroupSynchronizationApplier
	{
		// Token: 0x0600139B RID: 5019 RVA: 0x0004C47C File Offset: 0x0004A67C
		public GroupSynchronizationApplier(ICommonPlanningContext context, SynchronizationInputTablesCollector collector = null)
		{
			this.m_context = context;
			this.CalculationsWithSharedValues = new CalculationsWithSharedValues();
			this.m_collector = collector ?? new SynchronizationInputTablesCollector();
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x0004C4A6 File Offset: 0x0004A6A6
		public CalculationsWithSharedValues CalculationsWithSharedValues { get; }

		// Token: 0x0600139D RID: 5021 RVA: 0x0004C4B0 File Offset: 0x0004A6B0
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "SyncTables", "IndexedInputTable" })]
		public global::System.ValueTuple<List<PlanNamedTableContext>, PlanOperationContext> ProcessSyncDataShapes(DataShapeContext dsContext, PlanDeclarationCollection declarations, ExpressionTable expressionTable, PlanOperationContext bodyTable, bool isPrimary = true)
		{
			IReadOnlyList<Calculation> hierarchySyncCalcs = this.m_context.Annotations.GetHierarchySyncCalcs(dsContext.DataShape, isPrimary);
			if (hierarchySyncCalcs.IsNullOrEmpty<Calculation>())
			{
				return new global::System.ValueTuple<List<PlanNamedTableContext>, PlanOperationContext>(null, bodyTable);
			}
			List<PlanNamedTableContext> list = new List<PlanNamedTableContext>(hierarchySyncCalcs.Count);
			foreach (Calculation calculation in hierarchySyncCalcs)
			{
				DataShapeContext synchronizationDataShapeContext = dsContext.GetSynchronizationDataShapeContext(calculation);
				PlanOperationContext planOperationContext = this.BuildSynchronizationSourceTable(synchronizationDataShapeContext.PrimaryDynamics);
				PlanNamedTableContext planNamedTableContext = new PlanNamedTableContext(PlanNames.SyncTable(synchronizationDataShapeContext.Id), planOperationContext, false, false, false);
				list.Add(planNamedTableContext);
				bodyTable = bodyTable.DeclareIfNotDeclared(PlanNames.Synchronized(dsContext.Id, synchronizationDataShapeContext.Id), declarations, false, null, false);
			}
			return new global::System.ValueTuple<List<PlanNamedTableContext>, PlanOperationContext>(list, bodyTable);
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0004C588 File Offset: 0x0004A788
		public PlanOperationContext AddSyncIndex(DataShapeContext dsContext, PlanDeclarationCollection declarations, ExpressionTable expressionTable, IReadOnlyList<DataMember> scopeMembers, PlanOperationContext syncInput)
		{
			Calculation calculation = this.FindSyncCalc(scopeMembers);
			if (calculation == null)
			{
				return syncInput;
			}
			DataShapeContext synchronizationDataShapeContext = dsContext.GetSynchronizationDataShapeContext(calculation);
			PlanOperationContext syncTable = this.GetSyncTable(synchronizationDataShapeContext, expressionTable, syncInput);
			this.m_collector.RegisterScopedLimitedTable(dsContext, syncTable);
			return this.IndexInputTable(calculation, scopeMembers, syncInput, syncInput.Table).DeclareIfNotDeclared(PlanNames.SyncTable(synchronizationDataShapeContext.Id), declarations, false, null, true);
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0004C5EA File Offset: 0x0004A7EA
		private Calculation FindSyncCalc(IReadOnlyList<DataMember> scopeMembers)
		{
			return scopeMembers.Last<DataMember>().Calculations.FirstOrDefault(new Func<Calculation, bool>(this.m_context.Annotations.IsSynchronizationIndex));
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0004C614 File Offset: 0x0004A814
		private PlanOperationContext GetSyncTable(DataShapeContext syncDsContext, ExpressionTable expressionTable, PlanOperationContext inputTable)
		{
			IReadOnlyList<DataMember> primaryDynamics = syncDsContext.PrimaryDynamics;
			PlanOperationContext planOperationContext = this.ProjectSyncMembers(primaryDynamics, inputTable, expressionTable);
			return GroupSynchronizationApplier.SortSyncMembers(this.m_context, primaryDynamics, planOperationContext);
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0004C640 File Offset: 0x0004A840
		private PlanOperationContext BuildSynchronizationSourceTable(IReadOnlyList<DataMember> syncDsMembers)
		{
			PlanOperationContext planOperationContext;
			if (!this.m_collector.TryGetTable(syncDsMembers, out planOperationContext))
			{
				throw new InvalidOperationException("GroupSynchronization currently depends on scoped limits.");
			}
			return planOperationContext;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0004C66C File Offset: 0x0004A86C
		private HashSet<IScope> GetReferencedScopes(IReadOnlyList<DataMember> syncDsMembers)
		{
			HashSet<IScope> hashSet = new HashSet<IScope>();
			foreach (DataMember dataMember in syncDsMembers)
			{
				GroupSynchronizationApplier.AddReferencedScopes(this.m_context, dataMember, hashSet);
			}
			return hashSet;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0004C6C4 File Offset: 0x0004A8C4
		private PlanOperationContext IndexInputTable(Calculation synchronizationIndexCalculation, IEnumerable<DataMember> dsMembers, PlanOperationContext inputTableWithClonedColumns, PlanOperation syncSourceTableWithRenamedColumns)
		{
			IEnumerable<PlanSortItem> enumerable = dsMembers.ToSortItems(this.m_context.Annotations, true);
			return new PlanOperationContext(inputTableWithClonedColumns.Table.AddJoinIndex(synchronizationIndexCalculation, syncSourceTableWithRenamedColumns, enumerable, this.m_context.OutputExpressionTable), inputTableWithClonedColumns.RowScopes, inputTableWithClonedColumns.Calculations.Concat(new Calculation[] { synchronizationIndexCalculation }).ToReadOnlyList<Calculation>(), inputTableWithClonedColumns.ShowAll, inputTableWithClonedColumns.FilteringMetadata);
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0004C730 File Offset: 0x0004A930
		private PlanOperationContext ProjectSyncMembers(IReadOnlyList<DataMember> syncDsMembers, PlanOperationContext reference, ExpressionTable expressionTable)
		{
			List<PlanProjectItem> list = new List<PlanProjectItem>(syncDsMembers.Count + 1);
			list.Add(PlanPreserveAllColumnsProjectItem.Instance);
			List<Calculation> list2 = new List<Calculation>(reference.Calculations.Count);
			foreach (DataMember dataMember in syncDsMembers)
			{
				list.Add(dataMember.ToDataMemberProjectItem(true));
				if (dataMember.Calculations != null)
				{
					list2.AddRange(dataMember.Calculations);
					foreach (Calculation calculation in dataMember.Calculations)
					{
						list.Add(calculation.ToNewColumnProjectItem());
						ResolvedCalculationReferenceExpressionNode resolvedCalculationReferenceExpressionNode = expressionTable.GetNode(calculation.Value.ExpressionId.Value) as ResolvedCalculationReferenceExpressionNode;
						if (resolvedCalculationReferenceExpressionNode != null)
						{
							this.CalculationsWithSharedValues.CalculationsList.Add(new List<string>
							{
								calculation.Id.Value,
								resolvedCalculationReferenceExpressionNode.Calculation.Id.Value
							});
						}
					}
				}
			}
			return new PlanOperationContext(reference.Table.Project(list, false), syncDsMembers, list2, reference.ShowAll, reference.FilteringMetadata);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0004C898 File Offset: 0x0004AA98
		private static PlanOperationContext SortSyncMembers(ICommonPlanningContext context, IReadOnlyList<DataMember> syncDsMembers, PlanOperationContext reference)
		{
			IEnumerable<PlanSortItem> enumerable = syncDsMembers.ToSortItems(context.Annotations, true);
			return reference.SortBy(enumerable);
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0004C8BC File Offset: 0x0004AABC
		private static void AddReferencedScopes(ICommonPlanningContext context, DataMember syncMember, HashSet<IScope> allReferencedScopes)
		{
			foreach (Calculation calculation in syncMember.Calculations)
			{
				IReadOnlyList<IScope> referencedScopes = context.Annotations.GetReferencedScopes(calculation);
				allReferencedScopes.UnionWith(referencedScopes);
			}
		}

		// Token: 0x040008A9 RID: 2217
		private readonly ICommonPlanningContext m_context;

		// Token: 0x040008AA RID: 2218
		private readonly SynchronizationInputTablesCollector m_collector;
	}
}
