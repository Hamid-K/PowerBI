using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002DB RID: 731
	[Serializable]
	internal abstract class CursorStatement : TSqlStatement
	{
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600292D RID: 10541 RVA: 0x00166EBF File Offset: 0x001650BF
		// (set) Token: 0x0600292E RID: 10542 RVA: 0x00166EC7 File Offset: 0x001650C7
		public CursorId Cursor
		{
			get
			{
				return this._cursor;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._cursor = value;
			}
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x00166ED7 File Offset: 0x001650D7
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Cursor != null)
			{
				this.Cursor.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C17 RID: 7191
		private CursorId _cursor;
	}
}
