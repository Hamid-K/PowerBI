using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200046E RID: 1134
	[Serializable]
	internal class CreateCryptographicProviderStatement : TSqlStatement
	{
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06003299 RID: 12953 RVA: 0x001704B9 File Offset: 0x0016E6B9
		// (set) Token: 0x0600329A RID: 12954 RVA: 0x001704C1 File Offset: 0x0016E6C1
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

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x0600329B RID: 12955 RVA: 0x001704D1 File Offset: 0x0016E6D1
		// (set) Token: 0x0600329C RID: 12956 RVA: 0x001704D9 File Offset: 0x0016E6D9
		public Literal File
		{
			get
			{
				return this._file;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._file = value;
			}
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x001704E9 File Offset: 0x0016E6E9
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x001704F5 File Offset: 0x0016E6F5
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.File != null)
			{
				this.File.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EB6 RID: 7862
		private Identifier _name;

		// Token: 0x04001EB7 RID: 7863
		private Literal _file;
	}
}
