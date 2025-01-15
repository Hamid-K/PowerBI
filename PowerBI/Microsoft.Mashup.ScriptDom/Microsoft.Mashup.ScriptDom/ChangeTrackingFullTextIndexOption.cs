using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002B2 RID: 690
	[Serializable]
	internal class ChangeTrackingFullTextIndexOption : FullTextIndexOption
	{
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06002846 RID: 10310 RVA: 0x0016605C File Offset: 0x0016425C
		// (set) Token: 0x06002847 RID: 10311 RVA: 0x00166064 File Offset: 0x00164264
		public ChangeTrackingOption Value
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

		// Token: 0x06002848 RID: 10312 RVA: 0x0016606D File Offset: 0x0016426D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x00166079 File Offset: 0x00164279
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BD9 RID: 7129
		private ChangeTrackingOption _value;
	}
}
