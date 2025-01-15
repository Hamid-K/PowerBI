using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000333 RID: 819
	[Serializable]
	internal class IdentifierDatabaseOption : DatabaseOption
	{
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06002B21 RID: 11041 RVA: 0x00168B39 File Offset: 0x00166D39
		// (set) Token: 0x06002B22 RID: 11042 RVA: 0x00168B41 File Offset: 0x00166D41
		public Identifier Value
		{
			get
			{
				return this._value;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._value = value;
			}
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x00168B51 File Offset: 0x00166D51
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x00168B5D File Offset: 0x00166D5D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
		}

		// Token: 0x04001C97 RID: 7319
		private Identifier _value;
	}
}
