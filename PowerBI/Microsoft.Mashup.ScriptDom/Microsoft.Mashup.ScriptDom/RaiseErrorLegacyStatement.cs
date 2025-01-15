using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002F8 RID: 760
	[Serializable]
	internal class RaiseErrorLegacyStatement : TSqlStatement
	{
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060029B2 RID: 10674 RVA: 0x00167696 File Offset: 0x00165896
		// (set) Token: 0x060029B3 RID: 10675 RVA: 0x0016769E File Offset: 0x0016589E
		public ScalarExpression FirstParameter
		{
			get
			{
				return this._firstParameter;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._firstParameter = value;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060029B4 RID: 10676 RVA: 0x001676AE File Offset: 0x001658AE
		// (set) Token: 0x060029B5 RID: 10677 RVA: 0x001676B6 File Offset: 0x001658B6
		public ValueExpression SecondParameter
		{
			get
			{
				return this._secondParameter;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._secondParameter = value;
			}
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x001676C6 File Offset: 0x001658C6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x001676D2 File Offset: 0x001658D2
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.FirstParameter != null)
			{
				this.FirstParameter.Accept(visitor);
			}
			if (this.SecondParameter != null)
			{
				this.SecondParameter.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C32 RID: 7218
		private ScalarExpression _firstParameter;

		// Token: 0x04001C33 RID: 7219
		private ValueExpression _secondParameter;
	}
}
