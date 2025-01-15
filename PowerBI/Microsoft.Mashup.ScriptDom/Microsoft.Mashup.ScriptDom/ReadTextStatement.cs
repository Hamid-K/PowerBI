using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000241 RID: 577
	[Serializable]
	internal class ReadTextStatement : TSqlStatement
	{
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06002599 RID: 9625 RVA: 0x00163128 File Offset: 0x00161328
		// (set) Token: 0x0600259A RID: 9626 RVA: 0x00163130 File Offset: 0x00161330
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

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600259B RID: 9627 RVA: 0x00163140 File Offset: 0x00161340
		// (set) Token: 0x0600259C RID: 9628 RVA: 0x00163148 File Offset: 0x00161348
		public ValueExpression TextPointer
		{
			get
			{
				return this._textPointer;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._textPointer = value;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600259D RID: 9629 RVA: 0x00163158 File Offset: 0x00161358
		// (set) Token: 0x0600259E RID: 9630 RVA: 0x00163160 File Offset: 0x00161360
		public ValueExpression Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._offset = value;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600259F RID: 9631 RVA: 0x00163170 File Offset: 0x00161370
		// (set) Token: 0x060025A0 RID: 9632 RVA: 0x00163178 File Offset: 0x00161378
		public ValueExpression Size
		{
			get
			{
				return this._size;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._size = value;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060025A1 RID: 9633 RVA: 0x00163188 File Offset: 0x00161388
		// (set) Token: 0x060025A2 RID: 9634 RVA: 0x00163190 File Offset: 0x00161390
		public bool HoldLock
		{
			get
			{
				return this._holdLock;
			}
			set
			{
				this._holdLock = value;
			}
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x00163199 File Offset: 0x00161399
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x001631A8 File Offset: 0x001613A8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Column != null)
			{
				this.Column.Accept(visitor);
			}
			if (this.TextPointer != null)
			{
				this.TextPointer.Accept(visitor);
			}
			if (this.Offset != null)
			{
				this.Offset.Accept(visitor);
			}
			if (this.Size != null)
			{
				this.Size.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B13 RID: 6931
		private ColumnReferenceExpression _column;

		// Token: 0x04001B14 RID: 6932
		private ValueExpression _textPointer;

		// Token: 0x04001B15 RID: 6933
		private ValueExpression _offset;

		// Token: 0x04001B16 RID: 6934
		private ValueExpression _size;

		// Token: 0x04001B17 RID: 6935
		private bool _holdLock;
	}
}
