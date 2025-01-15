using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003BC RID: 956
	internal sealed class NewEntityOp : NewEntityBaseOp
	{
		// Token: 0x06002DD0 RID: 11728 RVA: 0x0009240C File Offset: 0x0009060C
		private NewEntityOp()
			: base(OpType.NewEntity)
		{
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x00092416 File Offset: 0x00090616
		internal NewEntityOp(TypeUsage type, List<RelProperty> relProperties, bool scoped, EntitySet entitySet)
			: base(OpType.NewEntity, type, scoped, entitySet, relProperties)
		{
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x00092425 File Offset: 0x00090625
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x0009242F File Offset: 0x0009062F
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F53 RID: 3923
		internal static readonly NewEntityOp Pattern = new NewEntityOp();
	}
}
