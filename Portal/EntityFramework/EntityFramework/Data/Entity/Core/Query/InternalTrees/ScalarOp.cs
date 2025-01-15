using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003DE RID: 990
	internal abstract class ScalarOp : Op
	{
		// Token: 0x06002EE7 RID: 12007 RVA: 0x0009529A File Offset: 0x0009349A
		internal ScalarOp(OpType opType, TypeUsage type)
			: this(opType)
		{
			this.m_type = type;
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x000952AA File Offset: 0x000934AA
		protected ScalarOp(OpType opType)
			: base(opType)
		{
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06002EE9 RID: 12009 RVA: 0x000952B3 File Offset: 0x000934B3
		internal override bool IsScalarOp
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x000952B6 File Offset: 0x000934B6
		internal override bool IsEquivalent(Op other)
		{
			return other.OpType == base.OpType && TypeSemantics.IsStructurallyEqual(this.Type, other.Type);
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06002EEB RID: 12011 RVA: 0x000952D9 File Offset: 0x000934D9
		// (set) Token: 0x06002EEC RID: 12012 RVA: 0x000952E1 File Offset: 0x000934E1
		internal override TypeUsage Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06002EED RID: 12013 RVA: 0x000952EA File Offset: 0x000934EA
		internal virtual bool IsAggregateOp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000FD2 RID: 4050
		private TypeUsage m_type;
	}
}
