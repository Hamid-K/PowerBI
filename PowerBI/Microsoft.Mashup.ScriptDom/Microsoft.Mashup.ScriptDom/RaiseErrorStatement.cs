using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002F9 RID: 761
	[Serializable]
	internal class RaiseErrorStatement : TSqlStatement
	{
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060029B9 RID: 10681 RVA: 0x0016770B File Offset: 0x0016590B
		// (set) Token: 0x060029BA RID: 10682 RVA: 0x00167713 File Offset: 0x00165913
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

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060029BB RID: 10683 RVA: 0x00167723 File Offset: 0x00165923
		// (set) Token: 0x060029BC RID: 10684 RVA: 0x0016772B File Offset: 0x0016592B
		public ScalarExpression SecondParameter
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

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060029BD RID: 10685 RVA: 0x0016773B File Offset: 0x0016593B
		// (set) Token: 0x060029BE RID: 10686 RVA: 0x00167743 File Offset: 0x00165943
		public ScalarExpression ThirdParameter
		{
			get
			{
				return this._thirdParameter;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._thirdParameter = value;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060029BF RID: 10687 RVA: 0x00167753 File Offset: 0x00165953
		public IList<ScalarExpression> OptionalParameters
		{
			get
			{
				return this._optionalParameters;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060029C0 RID: 10688 RVA: 0x0016775B File Offset: 0x0016595B
		// (set) Token: 0x060029C1 RID: 10689 RVA: 0x00167763 File Offset: 0x00165963
		public RaiseErrorOptions RaiseErrorOptions
		{
			get
			{
				return this._raiseErrorOptions;
			}
			set
			{
				this._raiseErrorOptions = value;
			}
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x0016776C File Offset: 0x0016596C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x00167778 File Offset: 0x00165978
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
			if (this.ThirdParameter != null)
			{
				this.ThirdParameter.Accept(visitor);
			}
			int i = 0;
			int count = this.OptionalParameters.Count;
			while (i < count)
			{
				this.OptionalParameters[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C34 RID: 7220
		private ScalarExpression _firstParameter;

		// Token: 0x04001C35 RID: 7221
		private ScalarExpression _secondParameter;

		// Token: 0x04001C36 RID: 7222
		private ScalarExpression _thirdParameter;

		// Token: 0x04001C37 RID: 7223
		private List<ScalarExpression> _optionalParameters = new List<ScalarExpression>();

		// Token: 0x04001C38 RID: 7224
		private RaiseErrorOptions _raiseErrorOptions;
	}
}
