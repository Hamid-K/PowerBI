using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000244 RID: 580
	[Serializable]
	internal class WriteTextStatement : TextModificationStatement
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060025BD RID: 9661 RVA: 0x001633B0 File Offset: 0x001615B0
		// (set) Token: 0x060025BE RID: 9662 RVA: 0x001633B8 File Offset: 0x001615B8
		public ValueExpression SourceParameter
		{
			get
			{
				return this._sourceParameter;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._sourceParameter = value;
			}
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x001633C8 File Offset: 0x001615C8
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x001633D4 File Offset: 0x001615D4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.SourceParameter != null)
			{
				this.SourceParameter.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B21 RID: 6945
		private ValueExpression _sourceParameter;
	}
}
