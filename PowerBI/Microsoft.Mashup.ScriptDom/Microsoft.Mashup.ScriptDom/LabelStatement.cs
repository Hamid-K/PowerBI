using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000233 RID: 563
	[Serializable]
	internal class LabelStatement : TSqlStatement
	{
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06002542 RID: 9538 RVA: 0x00162B90 File Offset: 0x00160D90
		// (set) Token: 0x06002543 RID: 9539 RVA: 0x00162B98 File Offset: 0x00160D98
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x00162BA1 File Offset: 0x00160DA1
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x00162BAD File Offset: 0x00160DAD
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001AFA RID: 6906
		private string _value;
	}
}
