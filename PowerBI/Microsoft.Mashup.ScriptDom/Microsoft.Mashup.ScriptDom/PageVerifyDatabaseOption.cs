using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200032E RID: 814
	[Serializable]
	internal class PageVerifyDatabaseOption : DatabaseOption
	{
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06002B02 RID: 11010 RVA: 0x001689B4 File Offset: 0x00166BB4
		// (set) Token: 0x06002B03 RID: 11011 RVA: 0x001689BC File Offset: 0x00166BBC
		public PageVerifyDatabaseOptionKind Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x001689C5 File Offset: 0x00166BC5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x001689D1 File Offset: 0x00166BD1
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C8F RID: 7311
		private PageVerifyDatabaseOptionKind _value;
	}
}
