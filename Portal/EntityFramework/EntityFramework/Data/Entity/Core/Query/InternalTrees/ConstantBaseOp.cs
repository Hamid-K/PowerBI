using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000393 RID: 915
	internal abstract class ConstantBaseOp : ScalarOp
	{
		// Token: 0x06002CBC RID: 11452 RVA: 0x0009002B File Offset: 0x0008E22B
		protected ConstantBaseOp(OpType opType, TypeUsage type, object value)
			: base(opType, type)
		{
			this.m_value = value;
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x0009003C File Offset: 0x0008E23C
		protected ConstantBaseOp(OpType opType)
			: base(opType)
		{
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06002CBE RID: 11454 RVA: 0x00090045 File Offset: 0x0008E245
		internal virtual object Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06002CBF RID: 11455 RVA: 0x0009004D File Offset: 0x0008E24D
		internal override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x00090050 File Offset: 0x0008E250
		internal override bool IsEquivalent(Op other)
		{
			ConstantBaseOp constantBaseOp = other as ConstantBaseOp;
			return constantBaseOp != null && base.OpType == other.OpType && constantBaseOp.Type.EdmEquals(this.Type) && ((constantBaseOp.Value == null && this.Value == null) || constantBaseOp.Value.Equals(this.Value));
		}

		// Token: 0x04000F0A RID: 3850
		private readonly object m_value;
	}
}
