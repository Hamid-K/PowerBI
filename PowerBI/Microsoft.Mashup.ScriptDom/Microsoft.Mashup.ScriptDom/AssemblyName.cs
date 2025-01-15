using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200027D RID: 637
	[Serializable]
	internal class AssemblyName : TSqlFragment
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060026FD RID: 9981 RVA: 0x001649AC File Offset: 0x00162BAC
		// (set) Token: 0x060026FE RID: 9982 RVA: 0x001649B4 File Offset: 0x00162BB4
		public Identifier Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060026FF RID: 9983 RVA: 0x001649C4 File Offset: 0x00162BC4
		// (set) Token: 0x06002700 RID: 9984 RVA: 0x001649CC File Offset: 0x00162BCC
		public Identifier ClassName
		{
			get
			{
				return this._className;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._className = value;
			}
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x001649DC File Offset: 0x00162BDC
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x001649E8 File Offset: 0x00162BE8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.ClassName != null)
			{
				this.ClassName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B78 RID: 7032
		private Identifier _name;

		// Token: 0x04001B79 RID: 7033
		private Identifier _className;
	}
}
