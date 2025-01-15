using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000355 RID: 853
	internal class OpCopierTrackingCollectionVars : OpCopier
	{
		// Token: 0x06002950 RID: 10576 RVA: 0x00084358 File Offset: 0x00082558
		private OpCopierTrackingCollectionVars(Command cmd)
			: base(cmd)
		{
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x0008436C File Offset: 0x0008256C
		internal static Node Copy(Command cmd, Node n, out VarMap varMap, out Dictionary<Var, Node> newCollectionVarDefinitions)
		{
			OpCopierTrackingCollectionVars opCopierTrackingCollectionVars = new OpCopierTrackingCollectionVars(cmd);
			Node node = opCopierTrackingCollectionVars.CopyNode(n);
			varMap = opCopierTrackingCollectionVars.m_varMap;
			newCollectionVarDefinitions = opCopierTrackingCollectionVars.m_newCollectionVarDefinitions;
			return node;
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x00084398 File Offset: 0x00082598
		public override Node Visit(MultiStreamNestOp op, Node n)
		{
			Node node = base.Visit(op, n);
			MultiStreamNestOp multiStreamNestOp = (MultiStreamNestOp)node.Op;
			for (int i = 0; i < multiStreamNestOp.CollectionInfo.Count; i++)
			{
				this.m_newCollectionVarDefinitions.Add(multiStreamNestOp.CollectionInfo[i].CollectionVar, node.Children[i + 1]);
			}
			return node;
		}

		// Token: 0x04000E3D RID: 3645
		private readonly Dictionary<Var, Node> m_newCollectionVarDefinitions = new Dictionary<Var, Node>();
	}
}
