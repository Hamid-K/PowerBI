using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Linq;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200036F RID: 879
	internal class TransformationRulesContext : RuleProcessingContext
	{
		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002A9D RID: 10909 RVA: 0x0008C1A4 File Offset: 0x0008A3A4
		internal PlanCompiler PlanCompiler
		{
			get
			{
				return this.m_compilerState;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06002A9E RID: 10910 RVA: 0x0008C1AC File Offset: 0x0008A3AC
		internal bool ProjectionPruningRequired
		{
			get
			{
				return this.m_projectionPruningRequired;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06002A9F RID: 10911 RVA: 0x0008C1B4 File Offset: 0x0008A3B4
		internal bool ReapplyNullabilityRules
		{
			get
			{
				return this.m_reapplyNullabilityRules;
			}
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x0008C1BC File Offset: 0x0008A3BC
		internal void RemapSubtree(Node subTree)
		{
			this.m_remapper.RemapSubtree(subTree);
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x0008C1CA File Offset: 0x0008A3CA
		internal void AddVarMapping(Var oldVar, Var newVar)
		{
			this.m_remapper.AddMapping(oldVar, newVar);
			this.m_remappedVars.Set(oldVar);
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x0008C1E8 File Offset: 0x0008A3E8
		internal Node ReMap(Node node, Dictionary<Var, Node> varMap)
		{
			PlanCompiler.Assert(node.Op.IsScalarOp, "Expected a scalarOp: Found " + Dump.AutoString.ToString(node.Op.OpType));
			if (node.Op.OpType != OpType.VarRef)
			{
				for (int i = 0; i < node.Children.Count; i++)
				{
					node.Children[i] = this.ReMap(node.Children[i], varMap);
				}
				base.Command.RecomputeNodeInfo(node);
				return node;
			}
			VarRefOp varRefOp = node.Op as VarRefOp;
			Node node2 = null;
			if (varMap.TryGetValue(varRefOp.Var, out node2))
			{
				node2 = this.Copy(node2);
				return node2;
			}
			return node;
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x0008C29C File Offset: 0x0008A49C
		internal Node Copy(Node node)
		{
			if (node.Op.OpType == OpType.VarRef)
			{
				VarRefOp varRefOp = node.Op as VarRefOp;
				return base.Command.CreateNode(base.Command.CreateVarRefOp(varRefOp.Var));
			}
			return OpCopier.Copy(base.Command, node);
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x0008C2EC File Offset: 0x0008A4EC
		internal bool IsScalarOpTree(Node node)
		{
			int num = 0;
			return this.IsScalarOpTree(node, null, ref num);
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x0008C308 File Offset: 0x0008A508
		internal bool IsNonNullable(Var variable)
		{
			if (variable.VarType == VarType.Parameter && !TypeSemantics.IsNullable(variable.Type))
			{
				return true;
			}
			foreach (Node node in this.m_relOpAncestors)
			{
				base.Command.RecomputeNodeInfo(node);
				ExtendedNodeInfo extendedNodeInfo = base.Command.GetExtendedNodeInfo(node);
				if (extendedNodeInfo.NonNullableVisibleDefinitions.IsSet(variable))
				{
					return true;
				}
				if (extendedNodeInfo.LocalDefinitions.IsSet(variable))
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06002AA6 RID: 10918 RVA: 0x0008C3AC File Offset: 0x0008A5AC
		internal bool CanChangeNullSentinelValue
		{
			get
			{
				if (this.m_compilerState.HasSortingOnNullSentinels)
				{
					return false;
				}
				if (this.m_relOpAncestors.Any((Node a) => TransformationRulesContext.IsOpNotSafeForNullSentinelValueChange(a.Op.OpType)))
				{
					return false;
				}
				foreach (Node node in this.m_relOpAncestors.Where((Node a) => a.Op.OpType == OpType.CrossApply || a.Op.OpType == OpType.OuterApply))
				{
					if (!this.m_relOpAncestors.Contains(node.Child1) && TransformationRulesContext.HasOpNotSafeForNullSentinelValueChange(node.Child1))
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x0008C47C File Offset: 0x0008A67C
		internal static bool IsOpNotSafeForNullSentinelValueChange(OpType optype)
		{
			return optype == OpType.Distinct || optype == OpType.GroupBy || optype == OpType.Intersect || optype == OpType.Except;
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x0008C494 File Offset: 0x0008A694
		internal static bool HasOpNotSafeForNullSentinelValueChange(Node n)
		{
			if (TransformationRulesContext.IsOpNotSafeForNullSentinelValueChange(n.Op.OpType))
			{
				return true;
			}
			using (List<Node>.Enumerator enumerator = n.Children.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (TransformationRulesContext.HasOpNotSafeForNullSentinelValueChange(enumerator.Current))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x0008C504 File Offset: 0x0008A704
		internal bool IsScalarOpTree(Node node, Dictionary<Var, int> varRefMap)
		{
			PlanCompiler.Assert(varRefMap != null, "Null varRef map");
			int num = 0;
			return this.IsScalarOpTree(node, varRefMap, ref num);
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x0008C52C File Offset: 0x0008A72C
		internal bool IncludeCustomFunctionOp(Node node, Dictionary<Var, Node> varMap)
		{
			if (!this.m_compilerState.DisableFilterOverProjectionSimplificationForCustomFunctions)
			{
				return false;
			}
			PlanCompiler.Assert(varMap != null, "Null varRef map");
			if (node.Op.OpType == OpType.VarRef)
			{
				VarRefOp varRefOp = (VarRefOp)node.Op;
				Node node2;
				if (varMap.TryGetValue(varRefOp.Var, out node2))
				{
					return this.IncludeCustomFunctionOp(node2, varMap);
				}
			}
			if (node.Op.OpType == OpType.Function && !(node.Op as FunctionOp).Function.BuiltInAttribute)
			{
				return true;
			}
			for (int i = 0; i < node.Children.Count; i++)
			{
				if (this.IncludeCustomFunctionOp(node.Children[i], varMap))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x0008C5E0 File Offset: 0x0008A7E0
		internal Dictionary<Var, Node> GetVarMap(Node varDefListNode, Dictionary<Var, int> varRefMap)
		{
			VarDefListOp varDefListOp = (VarDefListOp)varDefListNode.Op;
			Dictionary<Var, Node> dictionary = new Dictionary<Var, Node>();
			foreach (Node node in varDefListNode.Children)
			{
				VarDefOp varDefOp = (VarDefOp)node.Op;
				int num = 0;
				int num2 = 0;
				if (!this.IsScalarOpTree(node.Child0, null, ref num))
				{
					return null;
				}
				if (num > 100 && varRefMap != null && varRefMap.TryGetValue(varDefOp.Var, out num2) && num2 > 2)
				{
					return null;
				}
				Node node2;
				if (dictionary.TryGetValue(varDefOp.Var, out node2))
				{
					PlanCompiler.Assert(node2 == node.Child0, "reusing varDef for different Node?");
				}
				else
				{
					dictionary.Add(varDefOp.Var, node.Child0);
				}
			}
			return dictionary;
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x0008C6CC File Offset: 0x0008A8CC
		internal Node BuildNullIfExpression(Var conditionVar, Node expr)
		{
			VarRefOp varRefOp = base.Command.CreateVarRefOp(conditionVar);
			Node node = base.Command.CreateNode(varRefOp);
			Node node2 = base.Command.CreateNode(base.Command.CreateConditionalOp(OpType.IsNull), node);
			Node node3 = base.Command.CreateNode(base.Command.CreateNullOp(expr.Op.Type));
			return base.Command.CreateNode(base.Command.CreateCaseOp(expr.Op.Type), node2, node3, expr);
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x0008C757 File Offset: 0x0008A957
		internal void SuppressFilterPushdown(Node n)
		{
			this.m_suppressions[n] = n;
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x0008C766 File Offset: 0x0008A966
		internal bool IsFilterPushdownSuppressed(Node n)
		{
			return this.m_suppressions.ContainsKey(n);
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x0008C774 File Offset: 0x0008A974
		internal static bool TryGetInt32Var(IEnumerable<Var> varList, out Var int32Var)
		{
			foreach (Var var in varList)
			{
				PrimitiveTypeKind primitiveTypeKind;
				if (TypeHelpers.TryGetPrimitiveTypeKind(var.Type, out primitiveTypeKind) && primitiveTypeKind == PrimitiveTypeKind.Int32)
				{
					int32Var = var;
					return true;
				}
			}
			int32Var = null;
			return false;
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x0008C7D8 File Offset: 0x0008A9D8
		internal TransformationRulesContext(PlanCompiler compilerState)
			: base(compilerState.Command)
		{
			this.m_compilerState = compilerState;
			this.m_remapper = new VarRemapper(compilerState.Command);
			this.m_suppressions = new Dictionary<Node, Node>();
			this.m_remappedVars = compilerState.Command.CreateVarVec();
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x0008C830 File Offset: 0x0008AA30
		internal override void PreProcess(Node n)
		{
			this.m_remapper.RemapNode(n);
			base.Command.RecomputeNodeInfo(n);
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x0008C84C File Offset: 0x0008AA4C
		internal override void PreProcessSubTree(Node subTree)
		{
			if (subTree.Op.IsRelOp)
			{
				this.m_relOpAncestors.Push(subTree);
			}
			if (this.m_remappedVars.IsEmpty)
			{
				return;
			}
			foreach (Var var in base.Command.GetNodeInfo(subTree).ExternalReferences)
			{
				if (this.m_remappedVars.IsSet(var))
				{
					this.m_remapper.RemapSubtree(subTree);
					break;
				}
			}
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x0008C8E0 File Offset: 0x0008AAE0
		internal override void PostProcessSubTree(Node subtree)
		{
			if (subtree.Op.IsRelOp)
			{
				PlanCompiler.Assert(this.m_relOpAncestors.Count != 0, "The RelOp ancestors stack is empty when post processing a RelOp subtree");
				Node node = this.m_relOpAncestors.Pop();
				PlanCompiler.Assert(subtree == node, "The popped ancestor is not equal to the root of the subtree being post processed");
			}
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x0008C92C File Offset: 0x0008AB2C
		internal override void PostProcess(Node n, Rule rule)
		{
			if (rule != null)
			{
				if (!this.m_projectionPruningRequired && TransformationRules.RulesRequiringProjectionPruning.Contains(rule))
				{
					this.m_projectionPruningRequired = true;
				}
				if (!this.m_reapplyNullabilityRules && TransformationRules.RulesRequiringNullabilityRulesToBeReapplied.Contains(rule))
				{
					this.m_reapplyNullabilityRules = true;
				}
				base.Command.RecomputeNodeInfo(n);
			}
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x0008C980 File Offset: 0x0008AB80
		internal override int GetHashCode(Node node)
		{
			return base.Command.GetNodeInfo(node).HashValue;
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x0008C994 File Offset: 0x0008AB94
		private bool IsScalarOpTree(Node node, Dictionary<Var, int> varRefMap, ref int nonLeafNodeCount)
		{
			if (!node.Op.IsScalarOp)
			{
				return false;
			}
			if (node.HasChild0)
			{
				nonLeafNodeCount++;
			}
			if (varRefMap != null && node.Op.OpType == OpType.VarRef)
			{
				VarRefOp varRefOp = (VarRefOp)node.Op;
				int num;
				if (!varRefMap.TryGetValue(varRefOp.Var, out num))
				{
					num = 1;
				}
				else
				{
					num++;
				}
				varRefMap[varRefOp.Var] = num;
			}
			foreach (Node node2 in node.Children)
			{
				if (!this.IsScalarOpTree(node2, varRefMap, ref nonLeafNodeCount))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000EB8 RID: 3768
		private readonly PlanCompiler m_compilerState;

		// Token: 0x04000EB9 RID: 3769
		private readonly VarRemapper m_remapper;

		// Token: 0x04000EBA RID: 3770
		private readonly Dictionary<Node, Node> m_suppressions;

		// Token: 0x04000EBB RID: 3771
		private readonly VarVec m_remappedVars;

		// Token: 0x04000EBC RID: 3772
		private bool m_projectionPruningRequired;

		// Token: 0x04000EBD RID: 3773
		private bool m_reapplyNullabilityRules;

		// Token: 0x04000EBE RID: 3774
		private readonly Stack<Node> m_relOpAncestors = new Stack<Node>();
	}
}
