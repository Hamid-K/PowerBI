using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003A8 RID: 936
	internal sealed class FunctionOp : ScalarOp
	{
		// Token: 0x06002D5E RID: 11614 RVA: 0x00091D5B File Offset: 0x0008FF5B
		internal FunctionOp(EdmFunction function)
			: base(OpType.Function, function.ReturnParameter.TypeUsage)
		{
			this.m_function = function;
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x00091D77 File Offset: 0x0008FF77
		private FunctionOp()
			: base(OpType.Function)
		{
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002D60 RID: 11616 RVA: 0x00091D81 File Offset: 0x0008FF81
		internal EdmFunction Function
		{
			get
			{
				return this.m_function;
			}
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x00091D8C File Offset: 0x0008FF8C
		internal override bool IsEquivalent(Op other)
		{
			FunctionOp functionOp = other as FunctionOp;
			return functionOp != null && functionOp.Function.EdmEquals(this.Function);
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x00091DB6 File Offset: 0x0008FFB6
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x00091DC0 File Offset: 0x0008FFC0
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x04000F31 RID: 3889
		private readonly EdmFunction m_function;

		// Token: 0x04000F32 RID: 3890
		internal static readonly FunctionOp Pattern = new FunctionOp();
	}
}
