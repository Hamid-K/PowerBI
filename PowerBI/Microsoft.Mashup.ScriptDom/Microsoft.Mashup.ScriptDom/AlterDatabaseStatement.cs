using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200031A RID: 794
	[Serializable]
	internal abstract class AlterDatabaseStatement : TSqlStatement
	{
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06002A89 RID: 10889 RVA: 0x0016832D File Offset: 0x0016652D
		// (set) Token: 0x06002A8A RID: 10890 RVA: 0x00168335 File Offset: 0x00166535
		public Identifier DatabaseName
		{
			get
			{
				return this._databaseName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._databaseName = value;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06002A8B RID: 10891 RVA: 0x00168345 File Offset: 0x00166545
		// (set) Token: 0x06002A8C RID: 10892 RVA: 0x0016834D File Offset: 0x0016654D
		public bool UseCurrent
		{
			get
			{
				return this._useCurrent;
			}
			set
			{
				this._useCurrent = value;
			}
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x00168356 File Offset: 0x00166556
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.DatabaseName != null)
			{
				this.DatabaseName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C6F RID: 7279
		private Identifier _databaseName;

		// Token: 0x04001C70 RID: 7280
		private bool _useCurrent;
	}
}
