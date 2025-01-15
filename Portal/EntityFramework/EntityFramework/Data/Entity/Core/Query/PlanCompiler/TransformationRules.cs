using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200036E RID: 878
	internal static class TransformationRules
	{
		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06002A93 RID: 10899 RVA: 0x0008BC98 File Offset: 0x00089E98
		private static List<Rule> AllRules
		{
			get
			{
				if (TransformationRules.allRules == null)
				{
					TransformationRules.allRules = new List<Rule>();
					TransformationRules.allRules.AddRange(ScalarOpRules.Rules);
					TransformationRules.allRules.AddRange(FilterOpRules.Rules);
					TransformationRules.allRules.AddRange(ProjectOpRules.Rules);
					TransformationRules.allRules.AddRange(ApplyOpRules.Rules);
					TransformationRules.allRules.AddRange(JoinOpRules.Rules);
					TransformationRules.allRules.AddRange(SingleRowOpRules.Rules);
					TransformationRules.allRules.AddRange(SetOpRules.Rules);
					TransformationRules.allRules.AddRange(GroupByOpRules.Rules);
					TransformationRules.allRules.AddRange(SortOpRules.Rules);
					TransformationRules.allRules.AddRange(ConstrainedSortOpRules.Rules);
					TransformationRules.allRules.AddRange(DistinctOpRules.Rules);
				}
				return TransformationRules.allRules;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06002A94 RID: 10900 RVA: 0x0008BD64 File Offset: 0x00089F64
		private static List<Rule> PostJoinEliminationRules
		{
			get
			{
				if (TransformationRules.postJoinEliminationRules == null)
				{
					TransformationRules.postJoinEliminationRules = new List<Rule>();
					TransformationRules.postJoinEliminationRules.AddRange(ProjectOpRules.Rules);
					TransformationRules.postJoinEliminationRules.AddRange(DistinctOpRules.Rules);
					TransformationRules.postJoinEliminationRules.AddRange(FilterOpRules.Rules);
					TransformationRules.postJoinEliminationRules.AddRange(ApplyOpRules.Rules);
					TransformationRules.postJoinEliminationRules.AddRange(JoinOpRules.Rules);
					TransformationRules.postJoinEliminationRules.AddRange(TransformationRules.NullabilityRules);
				}
				return TransformationRules.postJoinEliminationRules;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06002A95 RID: 10901 RVA: 0x0008BDE4 File Offset: 0x00089FE4
		private static List<Rule> NullabilityRules
		{
			get
			{
				if (TransformationRules.nullabilityRules == null)
				{
					TransformationRules.nullabilityRules = new List<Rule>();
					TransformationRules.nullabilityRules.Add(ScalarOpRules.Rule_IsNullOverVarRef);
					TransformationRules.nullabilityRules.Add(ScalarOpRules.Rule_AndOverConstantPred1);
					TransformationRules.nullabilityRules.Add(ScalarOpRules.Rule_AndOverConstantPred2);
					TransformationRules.nullabilityRules.Add(ScalarOpRules.Rule_SimplifyCase);
					TransformationRules.nullabilityRules.Add(ScalarOpRules.Rule_NotOverConstantPred);
				}
				return TransformationRules.nullabilityRules;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06002A96 RID: 10902 RVA: 0x0008BE54 File Offset: 0x0008A054
		private static List<Rule> NullSemanticsRules
		{
			get
			{
				if (TransformationRules.nullSemanticsRules == null)
				{
					TransformationRules.nullSemanticsRules = new List<Rule>();
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_IsNullOverAnything);
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_NullCast);
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_EqualsOverConstant);
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_AndOverConstantPred1);
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_AndOverConstantPred2);
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_OrOverConstantPred1);
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_OrOverConstantPred2);
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_NotOverConstantPred);
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_LikeOverConstants);
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_SimplifyCase);
					TransformationRules.nullSemanticsRules.Add(ScalarOpRules.Rule_FlattenCase);
				}
				return TransformationRules.nullSemanticsRules;
			}
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x0008BF20 File Offset: 0x0008A120
		private static ReadOnlyCollection<ReadOnlyCollection<Rule>> BuildLookupTableForRules(IEnumerable<Rule> rules)
		{
			ReadOnlyCollection<Rule> readOnlyCollection = new ReadOnlyCollection<Rule>(new Rule[0]);
			List<Rule>[] array = new List<Rule>[73];
			foreach (Rule rule in rules)
			{
				List<Rule> list = array[(int)rule.RuleOpType];
				if (list == null)
				{
					list = new List<Rule>();
					array[(int)rule.RuleOpType] = list;
				}
				list.Add(rule);
			}
			ReadOnlyCollection<Rule>[] array2 = new ReadOnlyCollection<Rule>[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					array2[i] = new ReadOnlyCollection<Rule>(array[i].ToArray());
				}
				else
				{
					array2[i] = readOnlyCollection;
				}
			}
			return new ReadOnlyCollection<ReadOnlyCollection<Rule>>(array2);
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x0008BFE0 File Offset: 0x0008A1E0
		private static HashSet<Rule> InitializeRulesRequiringProjectionPruning()
		{
			return new HashSet<Rule>
			{
				ApplyOpRules.Rule_OuterApplyOverProject,
				JoinOpRules.Rule_CrossJoinOverProject1,
				JoinOpRules.Rule_CrossJoinOverProject2,
				JoinOpRules.Rule_InnerJoinOverProject1,
				JoinOpRules.Rule_InnerJoinOverProject2,
				JoinOpRules.Rule_OuterJoinOverProject2,
				ProjectOpRules.Rule_ProjectWithNoLocalDefs,
				FilterOpRules.Rule_FilterOverProject,
				FilterOpRules.Rule_FilterWithConstantPredicate,
				GroupByOpRules.Rule_GroupByOverProject,
				GroupByOpRules.Rule_GroupByOpWithSimpleVarRedefinitions
			};
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x0008C076 File Offset: 0x0008A276
		private static HashSet<Rule> InitializeRulesRequiringNullabilityRulesToBeReapplied()
		{
			return new HashSet<Rule> { FilterOpRules.Rule_FilterOverLeftOuterJoin };
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x0008C08C File Offset: 0x0008A28C
		internal static bool Process(PlanCompiler compilerState, TransformationRulesGroup rulesGroup)
		{
			ReadOnlyCollection<ReadOnlyCollection<Rule>> readOnlyCollection = null;
			switch (rulesGroup)
			{
			case TransformationRulesGroup.All:
				readOnlyCollection = TransformationRules.AllRulesTable;
				break;
			case TransformationRulesGroup.Project:
				readOnlyCollection = TransformationRules.ProjectRulesTable;
				break;
			case TransformationRulesGroup.PostJoinElimination:
				readOnlyCollection = TransformationRules.PostJoinEliminationRulesTable;
				break;
			case TransformationRulesGroup.NullSemantics:
				readOnlyCollection = TransformationRules.NullSemanticsRulesTable;
				break;
			}
			bool flag;
			if (TransformationRules.Process(compilerState, readOnlyCollection, out flag))
			{
				bool flag2;
				TransformationRules.Process(compilerState, TransformationRules.NullabilityRulesTable, out flag2);
				flag = flag || flag2;
			}
			return flag;
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x0008C0F0 File Offset: 0x0008A2F0
		private static bool Process(PlanCompiler compilerState, ReadOnlyCollection<ReadOnlyCollection<Rule>> rulesTable, out bool projectionPruningRequired)
		{
			RuleProcessor ruleProcessor = new RuleProcessor();
			TransformationRulesContext transformationRulesContext = new TransformationRulesContext(compilerState);
			compilerState.Command.Root = ruleProcessor.ApplyRulesToSubtree(transformationRulesContext, rulesTable, compilerState.Command.Root);
			projectionPruningRequired = transformationRulesContext.ProjectionPruningRequired;
			return transformationRulesContext.ReapplyNullabilityRules;
		}

		// Token: 0x04000EAD RID: 3757
		internal static readonly ReadOnlyCollection<ReadOnlyCollection<Rule>> AllRulesTable = TransformationRules.BuildLookupTableForRules(TransformationRules.AllRules);

		// Token: 0x04000EAE RID: 3758
		internal static readonly ReadOnlyCollection<ReadOnlyCollection<Rule>> ProjectRulesTable = TransformationRules.BuildLookupTableForRules(ProjectOpRules.Rules);

		// Token: 0x04000EAF RID: 3759
		internal static readonly ReadOnlyCollection<ReadOnlyCollection<Rule>> PostJoinEliminationRulesTable = TransformationRules.BuildLookupTableForRules(TransformationRules.PostJoinEliminationRules);

		// Token: 0x04000EB0 RID: 3760
		internal static readonly ReadOnlyCollection<ReadOnlyCollection<Rule>> NullabilityRulesTable = TransformationRules.BuildLookupTableForRules(TransformationRules.NullabilityRules);

		// Token: 0x04000EB1 RID: 3761
		internal static readonly HashSet<Rule> RulesRequiringProjectionPruning = TransformationRules.InitializeRulesRequiringProjectionPruning();

		// Token: 0x04000EB2 RID: 3762
		internal static readonly HashSet<Rule> RulesRequiringNullabilityRulesToBeReapplied = TransformationRules.InitializeRulesRequiringNullabilityRulesToBeReapplied();

		// Token: 0x04000EB3 RID: 3763
		internal static readonly ReadOnlyCollection<ReadOnlyCollection<Rule>> NullSemanticsRulesTable = TransformationRules.BuildLookupTableForRules(TransformationRules.NullSemanticsRules);

		// Token: 0x04000EB4 RID: 3764
		private static List<Rule> allRules;

		// Token: 0x04000EB5 RID: 3765
		private static List<Rule> postJoinEliminationRules;

		// Token: 0x04000EB6 RID: 3766
		private static List<Rule> nullabilityRules;

		// Token: 0x04000EB7 RID: 3767
		private static List<Rule> nullSemanticsRules;
	}
}
