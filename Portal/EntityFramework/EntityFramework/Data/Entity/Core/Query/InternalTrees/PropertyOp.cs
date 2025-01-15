using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003D0 RID: 976
	internal sealed class PropertyOp : ScalarOp
	{
		// Token: 0x06002EA3 RID: 11939 RVA: 0x00094C4A File Offset: 0x00092E4A
		internal PropertyOp(TypeUsage type, EdmMember property)
			: base(OpType.Property, type)
		{
			this.m_property = property;
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x00094C5C File Offset: 0x00092E5C
		private PropertyOp()
			: base(OpType.Property)
		{
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06002EA5 RID: 11941 RVA: 0x00094C66 File Offset: 0x00092E66
		internal override int Arity
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06002EA6 RID: 11942 RVA: 0x00094C69 File Offset: 0x00092E69
		internal EdmMember PropertyInfo
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x00094C71 File Offset: 0x00092E71
		[DebuggerNonUserCode]
		internal override void Accept(BasicOpVisitor v, Node n)
		{
			v.Visit(this, n);
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x00094C7B File Offset: 0x00092E7B
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType>(BasicOpVisitorOfT<TResultType> v, Node n)
		{
			return v.Visit(this, n);
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x00094C88 File Offset: 0x00092E88
		internal override bool IsEquivalent(Op other)
		{
			PropertyOp propertyOp = other as PropertyOp;
			return propertyOp != null && propertyOp.PropertyInfo.EdmEquals(this.PropertyInfo) && base.IsEquivalent(other);
		}

		// Token: 0x04000FBB RID: 4027
		private readonly EdmMember m_property;

		// Token: 0x04000FBC RID: 4028
		internal static readonly PropertyOp Pattern = new PropertyOp();
	}
}
