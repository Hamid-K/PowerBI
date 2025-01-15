using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003B8 RID: 952
	internal class MultiStreamNestOp : NestBaseOp
	{
		// Token: 0x06002DBA RID: 11706 RVA: 0x00092304 File Offset: 0x00090504
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x0009230E File Offset: 0x0009050E
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x00092318 File Offset: 0x00090518
		internal MultiStreamNestOp(List<SortKey> prefixSortKeys, VarVec outputVars, List<CollectionInfo> collectionInfoList)
			: base(OpType.MultiStreamNest, prefixSortKeys, outputVars, collectionInfoList)
		{
		}
	}
}
