using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003D2 RID: 978
	internal class RefColumnMap : ColumnMap
	{
		// Token: 0x06002EAF RID: 11951 RVA: 0x00094CF6 File Offset: 0x00092EF6
		internal RefColumnMap(TypeUsage type, string name, EntityIdentity entityIdentity)
			: base(type, name)
		{
			this.m_entityIdentity = entityIdentity;
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06002EB0 RID: 11952 RVA: 0x00094D07 File Offset: 0x00092F07
		internal EntityIdentity EntityIdentity
		{
			get
			{
				return this.m_entityIdentity;
			}
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x00094D0F File Offset: 0x00092F0F
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x00094D19 File Offset: 0x00092F19
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x04000FBE RID: 4030
		private readonly EntityIdentity m_entityIdentity;
	}
}
