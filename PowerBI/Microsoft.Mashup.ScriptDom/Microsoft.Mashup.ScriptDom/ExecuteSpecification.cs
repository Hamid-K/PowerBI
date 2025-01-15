using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001AD RID: 429
	[Serializable]
	internal class ExecuteSpecification : TSqlFragment
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06002210 RID: 8720 RVA: 0x0015EF9F File Offset: 0x0015D19F
		// (set) Token: 0x06002211 RID: 8721 RVA: 0x0015EFA7 File Offset: 0x0015D1A7
		public VariableReference Variable
		{
			get
			{
				return this._variable;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._variable = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06002212 RID: 8722 RVA: 0x0015EFB7 File Offset: 0x0015D1B7
		// (set) Token: 0x06002213 RID: 8723 RVA: 0x0015EFBF File Offset: 0x0015D1BF
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

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x0015EFCF File Offset: 0x0015D1CF
		// (set) Token: 0x06002215 RID: 8725 RVA: 0x0015EFD7 File Offset: 0x0015D1D7
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

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06002216 RID: 8726 RVA: 0x0015EFE7 File Offset: 0x0015D1E7
		// (set) Token: 0x06002217 RID: 8727 RVA: 0x0015EFEF File Offset: 0x0015D1EF
		public ExecutableEntity ExecutableEntity
		{
			get
			{
				return this._executableEntity;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._executableEntity = value;
			}
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x0015EFFF File Offset: 0x0015D1FF
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x0015F00C File Offset: 0x0015D20C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Variable != null)
			{
				this.Variable.Accept(visitor);
			}
			if (this.LinkedServer != null)
			{
				this.LinkedServer.Accept(visitor);
			}
			if (this.ExecuteContext != null)
			{
				this.ExecuteContext.Accept(visitor);
			}
			if (this.ExecutableEntity != null)
			{
				this.ExecutableEntity.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A0A RID: 6666
		private VariableReference _variable;

		// Token: 0x04001A0B RID: 6667
		private Identifier _linkedServer;

		// Token: 0x04001A0C RID: 6668
		private ExecuteContext _executeContext;

		// Token: 0x04001A0D RID: 6669
		private ExecutableEntity _executableEntity;
	}
}
