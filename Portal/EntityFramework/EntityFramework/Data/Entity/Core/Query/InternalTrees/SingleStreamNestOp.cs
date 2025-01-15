using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003EB RID: 1003
	internal class SingleStreamNestOp : NestBaseOp
	{
		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06002F1C RID: 12060 RVA: 0x00095645 File Offset: 0x00093845
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002F1D RID: 12061 RVA: 0x00095648 File Offset: 0x00093848
		internal Var Discriminator
		{
			get
			{
				return this.m_discriminator;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002F1E RID: 12062 RVA: 0x00095650 File Offset: 0x00093850
		internal List<SortKey> PostfixSortKeys
		{
			get
			{
				return this.m_postfixSortKeys;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06002F1F RID: 12063 RVA: 0x00095658 File Offset: 0x00093858
		internal VarVec Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x00095660 File Offset: 0x00093860
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x0009566A File Offset: 0x0009386A
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x00095674 File Offset: 0x00093874
		internal SingleStreamNestOp(VarVec keys, List<SortKey> prefixSortKeys, List<SortKey> postfixSortKeys, VarVec outputVars, List<CollectionInfo> collectionInfoList, Var discriminatorVar)
			: base(OpType.SingleStreamNest, prefixSortKeys, outputVars, collectionInfoList)
		{
			this.m_keys = keys;
			this.m_postfixSortKeys = postfixSortKeys;
			this.m_discriminator = discriminatorVar;
		}

		// Token: 0x04000FDF RID: 4063
		private readonly VarVec m_keys;

		// Token: 0x04000FE0 RID: 4064
		private readonly Var m_discriminator;

		// Token: 0x04000FE1 RID: 4065
		private readonly List<SortKey> m_postfixSortKeys;
	}
}
