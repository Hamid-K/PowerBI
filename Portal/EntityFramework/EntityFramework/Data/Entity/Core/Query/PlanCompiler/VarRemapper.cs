using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200037A RID: 890
	internal class VarRemapper : BasicOpVisitor
	{
		// Token: 0x06002AEE RID: 10990 RVA: 0x0008D10C File Offset: 0x0008B30C
		internal VarRemapper(Command command)
			: this(command, new Dictionary<Var, Var>())
		{
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x0008D11A File Offset: 0x0008B31A
		internal VarRemapper(Command command, Dictionary<Var, Var> varMap)
		{
			this.m_command = command;
			this.m_varMap = varMap;
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x0008D130 File Offset: 0x0008B330
		internal void AddMapping(Var oldVar, Var newVar)
		{
			this.m_varMap[oldVar] = newVar;
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x0008D13F File Offset: 0x0008B33F
		internal virtual void RemapNode(Node node)
		{
			if (this.m_varMap.Count == 0)
			{
				return;
			}
			this.VisitNode(node);
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x0008D158 File Offset: 0x0008B358
		internal virtual void RemapSubtree(Node subTree)
		{
			if (this.m_varMap.Count == 0)
			{
				return;
			}
			foreach (Node node in subTree.Children)
			{
				this.RemapSubtree(node);
			}
			this.RemapNode(subTree);
			this.m_command.RecomputeNodeInfo(subTree);
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x0008D1CC File Offset: 0x0008B3CC
		internal VarList RemapVarList(VarList varList)
		{
			return Command.CreateVarList(this.MapVars(varList));
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x0008D1DA File Offset: 0x0008B3DA
		internal static VarList RemapVarList(Command command, Dictionary<Var, Var> varMap, VarList varList)
		{
			return new VarRemapper(command, varMap).RemapVarList(varList);
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x0008D1EC File Offset: 0x0008B3EC
		private Var Map(Var v)
		{
			Var var;
			while (this.m_varMap.TryGetValue(v, out var))
			{
				v = var;
			}
			return v;
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x0008D20F File Offset: 0x0008B40F
		private IEnumerable<Var> MapVars(IEnumerable<Var> vars)
		{
			foreach (Var var in vars)
			{
				yield return this.Map(var);
			}
			IEnumerator<Var> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x0008D228 File Offset: 0x0008B428
		private void Map(VarVec vec)
		{
			VarVec varVec = this.m_command.CreateVarVec(this.MapVars(vec));
			vec.InitFrom(varVec);
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x0008D250 File Offset: 0x0008B450
		private void Map(VarList varList)
		{
			VarList varList2 = Command.CreateVarList(this.MapVars(varList));
			varList.Clear();
			varList.AddRange(varList2);
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x0008D278 File Offset: 0x0008B478
		private void Map(VarMap varMap)
		{
			VarMap varMap2 = new VarMap();
			foreach (KeyValuePair<Var, Var> keyValuePair in varMap)
			{
				Var var = this.Map(keyValuePair.Value);
				varMap2.Add(keyValuePair.Key, var);
			}
			varMap.Clear();
			foreach (KeyValuePair<Var, Var> keyValuePair2 in varMap2)
			{
				varMap.Add(keyValuePair2.Key, keyValuePair2.Value);
			}
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x0008D334 File Offset: 0x0008B534
		private void Map(List<SortKey> sortKeys)
		{
			VarVec varVec = this.m_command.CreateVarVec();
			bool flag = false;
			foreach (SortKey sortKey in sortKeys)
			{
				sortKey.Var = this.Map(sortKey.Var);
				if (varVec.IsSet(sortKey.Var))
				{
					flag = true;
				}
				varVec.Set(sortKey.Var);
			}
			if (flag)
			{
				List<SortKey> list = new List<SortKey>(sortKeys);
				sortKeys.Clear();
				varVec.Clear();
				foreach (SortKey sortKey2 in list)
				{
					if (!varVec.IsSet(sortKey2.Var))
					{
						sortKeys.Add(sortKey2);
					}
					varVec.Set(sortKey2.Var);
				}
			}
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x0008D428 File Offset: 0x0008B628
		protected override void VisitDefault(Node n)
		{
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x0008D42C File Offset: 0x0008B62C
		public override void Visit(VarRefOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			Var var = this.Map(op.Var);
			if (var != op.Var)
			{
				n.Op = this.m_command.CreateVarRefOp(var);
			}
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x0008D469 File Offset: 0x0008B669
		protected override void VisitNestOp(NestBaseOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x0008D470 File Offset: 0x0008B670
		public override void Visit(PhysicalProjectOp op, Node n)
		{
			this.VisitPhysicalOpDefault(op, n);
			this.Map(op.Outputs);
			SimpleCollectionColumnMap simpleCollectionColumnMap = (SimpleCollectionColumnMap)ColumnMapTranslator.Translate(op.ColumnMap, this.m_varMap);
			n.Op = this.m_command.CreatePhysicalProjectOp(op.Outputs, simpleCollectionColumnMap);
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x0008D4C0 File Offset: 0x0008B6C0
		protected override void VisitGroupByOp(GroupByBaseOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
			this.Map(op.Outputs);
			this.Map(op.Keys);
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x0008D4E2 File Offset: 0x0008B6E2
		public override void Visit(GroupByIntoOp op, Node n)
		{
			this.VisitGroupByOp(op, n);
			this.Map(op.Inputs);
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x0008D4F8 File Offset: 0x0008B6F8
		public override void Visit(DistinctOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
			this.Map(op.Keys);
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x0008D50E File Offset: 0x0008B70E
		public override void Visit(ProjectOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
			this.Map(op.Outputs);
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x0008D524 File Offset: 0x0008B724
		public override void Visit(UnnestOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
			Var var = this.Map(op.Var);
			if (var != op.Var)
			{
				n.Op = this.m_command.CreateUnnestOp(var, op.Table);
			}
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x0008D567 File Offset: 0x0008B767
		protected override void VisitSetOp(SetOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
			this.Map(op.VarMap[0]);
			this.Map(op.VarMap[1]);
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x0008D58D File Offset: 0x0008B78D
		protected override void VisitSortOp(SortBaseOp op, Node n)
		{
			this.VisitRelOpDefault(op, n);
			this.Map(op.Keys);
		}

		// Token: 0x04000ED6 RID: 3798
		private readonly Dictionary<Var, Var> m_varMap;

		// Token: 0x04000ED7 RID: 3799
		protected readonly Command m_command;
	}
}
