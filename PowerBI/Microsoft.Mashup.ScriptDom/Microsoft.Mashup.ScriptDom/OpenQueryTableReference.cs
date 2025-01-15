using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200020B RID: 523
	[Serializable]
	internal class OpenQueryTableReference : TableReferenceWithAlias
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06002451 RID: 9297 RVA: 0x00161A01 File Offset: 0x0015FC01
		// (set) Token: 0x06002452 RID: 9298 RVA: 0x00161A09 File Offset: 0x0015FC09
		public Identifier LinkedServer
		{
			get
			{
				return this._linkedServer;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._linkedServer = value;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06002453 RID: 9299 RVA: 0x00161A19 File Offset: 0x0015FC19
		// (set) Token: 0x06002454 RID: 9300 RVA: 0x00161A21 File Offset: 0x0015FC21
		public StringLiteral Query
		{
			get
			{
				return this._query;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._query = value;
			}
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x00161A31 File Offset: 0x0015FC31
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x00161A3D File Offset: 0x0015FC3D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.LinkedServer != null)
			{
				this.LinkedServer.Accept(visitor);
			}
			if (this.Query != null)
			{
				this.Query.Accept(visitor);
			}
		}

		// Token: 0x04001AB9 RID: 6841
		private Identifier _linkedServer;

		// Token: 0x04001ABA RID: 6842
		private StringLiteral _query;
	}
}
