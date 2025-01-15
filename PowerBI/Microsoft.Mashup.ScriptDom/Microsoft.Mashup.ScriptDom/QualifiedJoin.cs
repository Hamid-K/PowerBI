using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003C2 RID: 962
	[Serializable]
	internal class QualifiedJoin : JoinTableReference
	{
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06002ED2 RID: 11986 RVA: 0x0016CBC1 File Offset: 0x0016ADC1
		// (set) Token: 0x06002ED3 RID: 11987 RVA: 0x0016CBC9 File Offset: 0x0016ADC9
		public BooleanExpression SearchCondition
		{
			get
			{
				return this._searchCondition;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._searchCondition = value;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06002ED4 RID: 11988 RVA: 0x0016CBD9 File Offset: 0x0016ADD9
		// (set) Token: 0x06002ED5 RID: 11989 RVA: 0x0016CBE1 File Offset: 0x0016ADE1
		public QualifiedJoinType QualifiedJoinType
		{
			get
			{
				return this._qualifiedJoinType;
			}
			set
			{
				this._qualifiedJoinType = value;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06002ED6 RID: 11990 RVA: 0x0016CBEA File Offset: 0x0016ADEA
		// (set) Token: 0x06002ED7 RID: 11991 RVA: 0x0016CBF2 File Offset: 0x0016ADF2
		public JoinHint JoinHint
		{
			get
			{
				return this._joinHint;
			}
			set
			{
				this._joinHint = value;
			}
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x0016CBFB File Offset: 0x0016ADFB
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x0016CC07 File Offset: 0x0016AE07
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.SearchCondition != null)
			{
				this.SearchCondition.Accept(visitor);
			}
		}

		// Token: 0x04001DBC RID: 7612
		private BooleanExpression _searchCondition;

		// Token: 0x04001DBD RID: 7613
		private QualifiedJoinType _qualifiedJoinType;

		// Token: 0x04001DBE RID: 7614
		private JoinHint _joinHint;
	}
}
