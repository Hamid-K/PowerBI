using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000305 RID: 773
	[Serializable]
	internal class PredicateSetStatement : SetOnOffStatement
	{
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06002A05 RID: 10757 RVA: 0x00167B64 File Offset: 0x00165D64
		// (set) Token: 0x06002A06 RID: 10758 RVA: 0x00167B6C File Offset: 0x00165D6C
		public SetOptions Options
		{
			get
			{
				return this._options;
			}
			set
			{
				this._options = value;
			}
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x00167B75 File Offset: 0x00165D75
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x00167B81 File Offset: 0x00165D81
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C49 RID: 7241
		private SetOptions _options;
	}
}
