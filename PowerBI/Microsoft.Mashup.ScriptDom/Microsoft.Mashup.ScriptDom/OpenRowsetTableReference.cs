using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000208 RID: 520
	[Serializable]
	internal class OpenRowsetTableReference : TableReferenceWithAlias
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06002434 RID: 9268 RVA: 0x0016177D File Offset: 0x0015F97D
		// (set) Token: 0x06002435 RID: 9269 RVA: 0x00161785 File Offset: 0x0015F985
		public StringLiteral ProviderName
		{
			get
			{
				return this._providerName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._providerName = value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06002436 RID: 9270 RVA: 0x00161795 File Offset: 0x0015F995
		// (set) Token: 0x06002437 RID: 9271 RVA: 0x0016179D File Offset: 0x0015F99D
		public StringLiteral DataSource
		{
			get
			{
				return this._dataSource;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._dataSource = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06002438 RID: 9272 RVA: 0x001617AD File Offset: 0x0015F9AD
		// (set) Token: 0x06002439 RID: 9273 RVA: 0x001617B5 File Offset: 0x0015F9B5
		public StringLiteral UserId
		{
			get
			{
				return this._userId;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._userId = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600243A RID: 9274 RVA: 0x001617C5 File Offset: 0x0015F9C5
		// (set) Token: 0x0600243B RID: 9275 RVA: 0x001617CD File Offset: 0x0015F9CD
		public StringLiteral Password
		{
			get
			{
				return this._password;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._password = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600243C RID: 9276 RVA: 0x001617DD File Offset: 0x0015F9DD
		// (set) Token: 0x0600243D RID: 9277 RVA: 0x001617E5 File Offset: 0x0015F9E5
		public StringLiteral ProviderString
		{
			get
			{
				return this._providerString;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._providerString = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600243E RID: 9278 RVA: 0x001617F5 File Offset: 0x0015F9F5
		// (set) Token: 0x0600243F RID: 9279 RVA: 0x001617FD File Offset: 0x0015F9FD
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

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06002440 RID: 9280 RVA: 0x0016180D File Offset: 0x0015FA0D
		// (set) Token: 0x06002441 RID: 9281 RVA: 0x00161815 File Offset: 0x0015FA15
		public SchemaObjectName Object
		{
			get
			{
				return this._object;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._object = value;
			}
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x00161825 File Offset: 0x0015FA25
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x00161834 File Offset: 0x0015FA34
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.ProviderName != null)
			{
				this.ProviderName.Accept(visitor);
			}
			if (this.DataSource != null)
			{
				this.DataSource.Accept(visitor);
			}
			if (this.UserId != null)
			{
				this.UserId.Accept(visitor);
			}
			if (this.Password != null)
			{
				this.Password.Accept(visitor);
			}
			if (this.ProviderString != null)
			{
				this.ProviderString.Accept(visitor);
			}
			if (this.Query != null)
			{
				this.Query.Accept(visitor);
			}
			if (this.Object != null)
			{
				this.Object.Accept(visitor);
			}
		}

		// Token: 0x04001AAE RID: 6830
		private StringLiteral _providerName;

		// Token: 0x04001AAF RID: 6831
		private StringLiteral _dataSource;

		// Token: 0x04001AB0 RID: 6832
		private StringLiteral _userId;

		// Token: 0x04001AB1 RID: 6833
		private StringLiteral _password;

		// Token: 0x04001AB2 RID: 6834
		private StringLiteral _providerString;

		// Token: 0x04001AB3 RID: 6835
		private StringLiteral _query;

		// Token: 0x04001AB4 RID: 6836
		private SchemaObjectName _object;
	}
}
