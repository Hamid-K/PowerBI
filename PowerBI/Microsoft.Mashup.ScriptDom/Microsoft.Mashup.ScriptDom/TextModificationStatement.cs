using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000242 RID: 578
	[Serializable]
	internal abstract class TextModificationStatement : TSqlStatement
	{
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060025A6 RID: 9638 RVA: 0x00163214 File Offset: 0x00161414
		// (set) Token: 0x060025A7 RID: 9639 RVA: 0x0016321C File Offset: 0x0016141C
		public bool Bulk
		{
			get
			{
				return this._bulk;
			}
			set
			{
				this._bulk = value;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060025A8 RID: 9640 RVA: 0x00163225 File Offset: 0x00161425
		// (set) Token: 0x060025A9 RID: 9641 RVA: 0x0016322D File Offset: 0x0016142D
		public ColumnReferenceExpression Column
		{
			get
			{
				return this._column;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._column = value;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060025AA RID: 9642 RVA: 0x0016323D File Offset: 0x0016143D
		// (set) Token: 0x060025AB RID: 9643 RVA: 0x00163245 File Offset: 0x00161445
		public ValueExpression TextId
		{
			get
			{
				return this._textId;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._textId = value;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060025AC RID: 9644 RVA: 0x00163255 File Offset: 0x00161455
		// (set) Token: 0x060025AD RID: 9645 RVA: 0x0016325D File Offset: 0x0016145D
		public Literal Timestamp
		{
			get
			{
				return this._timestamp;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._timestamp = value;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060025AE RID: 9646 RVA: 0x0016326D File Offset: 0x0016146D
		// (set) Token: 0x060025AF RID: 9647 RVA: 0x00163275 File Offset: 0x00161475
		public bool WithLog
		{
			get
			{
				return this._withLog;
			}
			set
			{
				this._withLog = value;
			}
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x00163280 File Offset: 0x00161480
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Column != null)
			{
				this.Column.Accept(visitor);
			}
			if (this.TextId != null)
			{
				this.TextId.Accept(visitor);
			}
			if (this.Timestamp != null)
			{
				this.Timestamp.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B18 RID: 6936
		private bool _bulk;

		// Token: 0x04001B19 RID: 6937
		private ColumnReferenceExpression _column;

		// Token: 0x04001B1A RID: 6938
		private ValueExpression _textId;

		// Token: 0x04001B1B RID: 6939
		private Literal _timestamp;

		// Token: 0x04001B1C RID: 6940
		private bool _withLog;
	}
}
