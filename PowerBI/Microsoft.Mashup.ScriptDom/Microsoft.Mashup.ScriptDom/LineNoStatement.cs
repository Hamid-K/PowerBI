using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000245 RID: 581
	[Serializable]
	internal class LineNoStatement : TSqlStatement
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060025C2 RID: 9666 RVA: 0x001633F9 File Offset: 0x001615F9
		// (set) Token: 0x060025C3 RID: 9667 RVA: 0x00163401 File Offset: 0x00161601
		public IntegerLiteral LineNo
		{
			get
			{
				return this._lineNo;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._lineNo = value;
			}
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x00163411 File Offset: 0x00161611
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x0016341D File Offset: 0x0016161D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.LineNo != null)
			{
				this.LineNo.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B22 RID: 6946
		private IntegerLiteral _lineNo;
	}
}
