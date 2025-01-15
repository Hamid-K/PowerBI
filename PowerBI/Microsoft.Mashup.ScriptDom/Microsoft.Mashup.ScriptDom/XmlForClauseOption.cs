using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001F4 RID: 500
	[Serializable]
	internal class XmlForClauseOption : ForClause
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x00160CFD File Offset: 0x0015EEFD
		// (set) Token: 0x060023AB RID: 9131 RVA: 0x00160D05 File Offset: 0x0015EF05
		public XmlForClauseOptions OptionKind
		{
			get
			{
				return this._optionKind;
			}
			set
			{
				this._optionKind = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x00160D0E File Offset: 0x0015EF0E
		// (set) Token: 0x060023AD RID: 9133 RVA: 0x00160D16 File Offset: 0x0015EF16
		public Literal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._value = value;
			}
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x00160D26 File Offset: 0x0015EF26
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x00160D32 File Offset: 0x0015EF32
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A81 RID: 6785
		private XmlForClauseOptions _optionKind;

		// Token: 0x04001A82 RID: 6786
		private Literal _value;
	}
}
