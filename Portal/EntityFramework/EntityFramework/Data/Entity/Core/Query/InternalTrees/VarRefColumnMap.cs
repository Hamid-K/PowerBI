using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Globalization;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003FD RID: 1021
	internal class VarRefColumnMap : SimpleColumnMap
	{
		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06002F7B RID: 12155 RVA: 0x00095EE0 File Offset: 0x000940E0
		internal Var Var
		{
			get
			{
				return this.m_var;
			}
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x00095EE8 File Offset: 0x000940E8
		internal VarRefColumnMap(TypeUsage type, string name, Var v)
			: base(type, name)
		{
			this.m_var = v;
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x00095EF9 File Offset: 0x000940F9
		internal VarRefColumnMap(Var v)
			: this(v.Type, null, v)
		{
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x00095F09 File Offset: 0x00094109
		[DebuggerNonUserCode]
		internal override void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg)
		{
			visitor.Visit(this, arg);
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x00095F13 File Offset: 0x00094113
		[DebuggerNonUserCode]
		internal override TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg)
		{
			return visitor.Visit(this, arg);
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x00095F1D File Offset: 0x0009411D
		public override string ToString()
		{
			if (!base.IsNamed)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}", new object[] { this.m_var.Id });
			}
			return base.Name;
		}

		// Token: 0x04001005 RID: 4101
		private readonly Var m_var;
	}
}
