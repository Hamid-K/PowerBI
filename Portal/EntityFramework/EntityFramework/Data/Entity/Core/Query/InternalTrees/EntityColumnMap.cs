using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003A0 RID: 928
	internal class EntityColumnMap : TypedColumnMap
	{
		// Token: 0x06002D2F RID: 11567 RVA: 0x000919B4 File Offset: 0x0008FBB4
		internal EntityColumnMap(TypeUsage type, string name, ColumnMap[] properties, EntityIdentity entityIdentity)
			: base(type, name, properties)
		{
			this.m_entityIdentity = entityIdentity;
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06002D30 RID: 11568 RVA: 0x000919C7 File Offset: 0x0008FBC7
		internal EntityIdentity EntityIdentity
		{
			get
			{
				return this.m_entityIdentity;
			}
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x000919CF File Offset: 0x0008FBCF
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x000919D9 File Offset: 0x0008FBD9
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x000919E3 File Offset: 0x0008FBE3
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "E{0}", new object[] { base.ToString() });
		}

		// Token: 0x04000F1F RID: 3871
		private readonly EntityIdentity m_entityIdentity;
	}
}
