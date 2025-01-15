using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200020C RID: 524
	[Serializable]
	internal class AdHocTableReference : TableReferenceWithAlias
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06002458 RID: 9304 RVA: 0x00161A76 File Offset: 0x0015FC76
		// (set) Token: 0x06002459 RID: 9305 RVA: 0x00161A7E File Offset: 0x0015FC7E
		public AdHocDataSource DataSource
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

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600245A RID: 9306 RVA: 0x00161A8E File Offset: 0x0015FC8E
		// (set) Token: 0x0600245B RID: 9307 RVA: 0x00161A96 File Offset: 0x0015FC96
		public SchemaObjectNameOrValueExpression Object
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

		// Token: 0x0600245C RID: 9308 RVA: 0x00161AA6 File Offset: 0x0015FCA6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x00161AB2 File Offset: 0x0015FCB2
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.DataSource != null)
			{
				this.DataSource.Accept(visitor);
			}
			if (this.Object != null)
			{
				this.Object.Accept(visitor);
			}
		}

		// Token: 0x04001ABB RID: 6843
		private AdHocDataSource _dataSource;

		// Token: 0x04001ABC RID: 6844
		private SchemaObjectNameOrValueExpression _object;
	}
}
