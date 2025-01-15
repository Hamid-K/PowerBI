using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000356 RID: 854
	internal class PlanCompiler
	{
		// Token: 0x06002953 RID: 10579 RVA: 0x000843FB File Offset: 0x000825FB
		private PlanCompiler(DbCommandTree ctree)
		{
			this.m_ctree = ctree;
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x0008440A File Offset: 0x0008260A
		internal static void Assert(bool condition, string message)
		{
			if (!condition)
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.AssertionFailed, 0, message);
			}
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x0008441C File Offset: 0x0008261C
		internal static void Compile(DbCommandTree ctree, out List<ProviderCommandInfo> providerCommands, out ColumnMap resultColumnMap, out int columnCount, out Set<EntitySet> entitySets)
		{
			PlanCompiler.Assert(ctree != null, "Expected a valid, non-null Command Tree input");
			new PlanCompiler(ctree).Compile(out providerCommands, out resultColumnMap, out columnCount, out entitySets);
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06002956 RID: 10582 RVA: 0x0008443C File Offset: 0x0008263C
		internal Command Command
		{
			get
			{
				return this.m_command;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06002957 RID: 10583 RVA: 0x00084444 File Offset: 0x00082644
		// (set) Token: 0x06002958 RID: 10584 RVA: 0x0008444C File Offset: 0x0008264C
		internal bool HasSortingOnNullSentinels { get; set; }

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06002959 RID: 10585 RVA: 0x00084455 File Offset: 0x00082655
		internal ConstraintManager ConstraintManager
		{
			get
			{
				if (this.m_constraintManager == null)
				{
					this.m_constraintManager = new ConstraintManager();
				}
				return this.m_constraintManager;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x0600295A RID: 10586 RVA: 0x00084470 File Offset: 0x00082670
		internal bool DisableFilterOverProjectionSimplificationForCustomFunctions
		{
			get
			{
				return this.m_ctree.DisableFilterOverProjectionSimplificationForCustomFunctions;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x0600295B RID: 10587 RVA: 0x0008447D File Offset: 0x0008267D
		internal MetadataWorkspace MetadataWorkspace
		{
			get
			{
				return this.m_ctree.MetadataWorkspace;
			}
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x0008448A File Offset: 0x0008268A
		internal bool IsPhaseNeeded(PlanCompilerPhase phase)
		{
			return (this.m_neededPhases & (1 << (int)phase)) != 0;
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x0008449C File Offset: 0x0008269C
		internal void MarkPhaseAsNeeded(PlanCompilerPhase phase)
		{
			this.m_neededPhases |= 1 << (int)phase;
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x000844B1 File Offset: 0x000826B1
		internal bool IsAfterPhase(PlanCompilerPhase phase)
		{
			return (this._precedingPhases & (1 << (int)phase)) != 0;
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x000844C4 File Offset: 0x000826C4
		private void Compile(out List<ProviderCommandInfo> providerCommands, out ColumnMap resultColumnMap, out int columnCount, out Set<EntitySet> entitySets)
		{
			this.Initialize();
			string empty = string.Empty;
			string empty2 = string.Empty;
			string empty3 = string.Empty;
			string empty4 = string.Empty;
			string empty5 = string.Empty;
			string empty6 = string.Empty;
			string empty7 = string.Empty;
			string empty8 = string.Empty;
			string empty9 = string.Empty;
			string empty10 = string.Empty;
			string empty11 = string.Empty;
			string empty12 = string.Empty;
			string empty13 = string.Empty;
			string empty14 = string.Empty;
			string empty15 = string.Empty;
			this.m_neededPhases = 593;
			this.SwitchToPhase(PlanCompilerPhase.PreProcessor);
			StructuredTypeInfo structuredTypeInfo;
			Dictionary<EdmFunction, EdmProperty[]> dictionary;
			PreProcessor.Process(this, out structuredTypeInfo, out dictionary);
			entitySets = structuredTypeInfo.GetEntitySets();
			if (this.IsPhaseNeeded(PlanCompilerPhase.AggregatePushdown))
			{
				this.SwitchToPhase(PlanCompilerPhase.AggregatePushdown);
				AggregatePushdown.Process(this);
			}
			if (this.IsPhaseNeeded(PlanCompilerPhase.Normalization))
			{
				this.SwitchToPhase(PlanCompilerPhase.Normalization);
				Normalizer.Process(this);
			}
			if (this.IsPhaseNeeded(PlanCompilerPhase.NTE))
			{
				this.SwitchToPhase(PlanCompilerPhase.NTE);
				NominalTypeEliminator.Process(this, structuredTypeInfo, dictionary);
			}
			if (this.IsPhaseNeeded(PlanCompilerPhase.ProjectionPruning))
			{
				this.SwitchToPhase(PlanCompilerPhase.ProjectionPruning);
				ProjectionPruner.Process(this);
			}
			if (this.IsPhaseNeeded(PlanCompilerPhase.NestPullup))
			{
				this.SwitchToPhase(PlanCompilerPhase.NestPullup);
				NestPullup.Process(this);
				this.SwitchToPhase(PlanCompilerPhase.ProjectionPruning);
				ProjectionPruner.Process(this);
			}
			if (this.IsPhaseNeeded(PlanCompilerPhase.Transformations) && this.ApplyTransformations(ref empty8, TransformationRulesGroup.All))
			{
				this.SwitchToPhase(PlanCompilerPhase.ProjectionPruning);
				ProjectionPruner.Process(this);
				this.ApplyTransformations(ref empty10, TransformationRulesGroup.Project);
			}
			if (this.IsPhaseNeeded(PlanCompilerPhase.NullSemantics))
			{
				this.SwitchToPhase(PlanCompilerPhase.NullSemantics);
				if (!this.m_ctree.UseDatabaseNullSemantics && NullSemantics.Process(this.Command))
				{
					this.ApplyTransformations(ref empty12, TransformationRulesGroup.NullSemantics);
				}
			}
			if (this.IsPhaseNeeded(PlanCompilerPhase.JoinElimination))
			{
				for (int i = 0; i < 10; i++)
				{
					this.SwitchToPhase(PlanCompilerPhase.JoinElimination);
					if (!JoinElimination.Process(this) && !this.TransformationsDeferred)
					{
						break;
					}
					this.TransformationsDeferred = false;
					this.ApplyTransformations(ref empty14, TransformationRulesGroup.PostJoinElimination);
				}
			}
			this.SwitchToPhase(PlanCompilerPhase.CodeGen);
			CodeGen.Process(this, out providerCommands, out resultColumnMap, out columnCount);
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x00084693 File Offset: 0x00082893
		private bool ApplyTransformations(ref string dumpString, TransformationRulesGroup rulesGroup)
		{
			if (this.MayApplyTransformationRules)
			{
				dumpString = this.SwitchToPhase(PlanCompilerPhase.Transformations);
				return TransformationRules.Process(this, rulesGroup);
			}
			return false;
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x000846AF File Offset: 0x000828AF
		private string SwitchToPhase(PlanCompilerPhase newPhase)
		{
			string empty = string.Empty;
			if (newPhase != this.m_phase)
			{
				this._precedingPhases |= 1 << (int)this.m_phase;
			}
			this.m_phase = newPhase;
			return empty;
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06002962 RID: 10594 RVA: 0x000846DE File Offset: 0x000828DE
		private bool MayApplyTransformationRules
		{
			get
			{
				if (this.m_mayApplyTransformationRules == null)
				{
					this.m_mayApplyTransformationRules = new bool?(this.ComputeMayApplyTransformations());
				}
				return this.m_mayApplyTransformationRules.Value;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002963 RID: 10595 RVA: 0x00084709 File Offset: 0x00082909
		// (set) Token: 0x06002964 RID: 10596 RVA: 0x00084711 File Offset: 0x00082911
		internal bool TransformationsDeferred { get; set; }

		// Token: 0x06002965 RID: 10597 RVA: 0x0008471A File Offset: 0x0008291A
		private bool ComputeMayApplyTransformations()
		{
			return PlanCompiler._applyTransformationsRegardlessOfSize.Enabled || this.m_command.NextNodeId < 10000 || NodeCounter.Count(this.m_command.Root) < 10000;
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x00084754 File Offset: 0x00082954
		private void Initialize()
		{
			DbQueryCommandTree dbQueryCommandTree = this.m_ctree as DbQueryCommandTree;
			PlanCompiler.Assert(dbQueryCommandTree != null, "Unexpected command tree kind. Only query command tree is supported.");
			this.m_command = ITreeGenerator.Generate(dbQueryCommandTree);
			PlanCompiler.Assert(this.m_command != null, "Unable to generate internal tree from Command Tree");
		}

		// Token: 0x04000E3E RID: 3646
		private static readonly BooleanSwitch _applyTransformationsRegardlessOfSize = new BooleanSwitch("System.Data.Entity.Core.EntityClient.IgnoreOptimizationLimit", "The Entity Framework should try to optimize the query regardless of its size");

		// Token: 0x04000E3F RID: 3647
		private const int MaxNodeCountForTransformations = 10000;

		// Token: 0x04000E40 RID: 3648
		private readonly DbCommandTree m_ctree;

		// Token: 0x04000E41 RID: 3649
		private Command m_command;

		// Token: 0x04000E42 RID: 3650
		private PlanCompilerPhase m_phase;

		// Token: 0x04000E43 RID: 3651
		private int _precedingPhases;

		// Token: 0x04000E44 RID: 3652
		private int m_neededPhases;

		// Token: 0x04000E45 RID: 3653
		private ConstraintManager m_constraintManager;

		// Token: 0x04000E46 RID: 3654
		private bool? m_mayApplyTransformationRules;
	}
}
