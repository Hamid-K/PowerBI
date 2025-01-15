using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000334 RID: 820
	[Serializable]
	internal class ChangeTrackingDatabaseOption : DatabaseOption
	{
		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06002B26 RID: 11046 RVA: 0x00168B82 File Offset: 0x00166D82
		// (set) Token: 0x06002B27 RID: 11047 RVA: 0x00168B8A File Offset: 0x00166D8A
		public OptionState OptionState
		{
			get
			{
				return this._optionState;
			}
			set
			{
				this._optionState = value;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06002B28 RID: 11048 RVA: 0x00168B93 File Offset: 0x00166D93
		public IList<ChangeTrackingOptionDetail> Details
		{
			get
			{
				return this._details;
			}
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x00168B9B File Offset: 0x00166D9B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x00168BA8 File Offset: 0x00166DA8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.Details.Count;
			while (i < count)
			{
				this.Details[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001C98 RID: 7320
		private OptionState _optionState;

		// Token: 0x04001C99 RID: 7321
		private List<ChangeTrackingOptionDetail> _details = new List<ChangeTrackingOptionDetail>();
	}
}
