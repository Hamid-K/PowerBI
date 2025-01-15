using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000412 RID: 1042
	[Serializable]
	internal class ExecuteAsStatement : TSqlStatement
	{
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06003091 RID: 12433 RVA: 0x0016E62D File Offset: 0x0016C82D
		// (set) Token: 0x06003092 RID: 12434 RVA: 0x0016E635 File Offset: 0x0016C835
		public bool WithNoRevert
		{
			get
			{
				return this._withNoRevert;
			}
			set
			{
				this._withNoRevert = value;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06003093 RID: 12435 RVA: 0x0016E63E File Offset: 0x0016C83E
		// (set) Token: 0x06003094 RID: 12436 RVA: 0x0016E646 File Offset: 0x0016C846
		public VariableReference Cookie
		{
			get
			{
				return this._cookie;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._cookie = value;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06003095 RID: 12437 RVA: 0x0016E656 File Offset: 0x0016C856
		// (set) Token: 0x06003096 RID: 12438 RVA: 0x0016E65E File Offset: 0x0016C85E
		public ExecuteContext ExecuteContext
		{
			get
			{
				return this._executeContext;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._executeContext = value;
			}
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x0016E66E File Offset: 0x0016C86E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x0016E67A File Offset: 0x0016C87A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Cookie != null)
			{
				this.Cookie.Accept(visitor);
			}
			if (this.ExecuteContext != null)
			{
				this.ExecuteContext.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E2D RID: 7725
		private bool _withNoRevert;

		// Token: 0x04001E2E RID: 7726
		private VariableReference _cookie;

		// Token: 0x04001E2F RID: 7727
		private ExecuteContext _executeContext;
	}
}
