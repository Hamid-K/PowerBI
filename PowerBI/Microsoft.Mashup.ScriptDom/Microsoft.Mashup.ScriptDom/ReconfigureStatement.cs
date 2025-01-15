using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000300 RID: 768
	[Serializable]
	internal class ReconfigureStatement : TSqlStatement
	{
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060029EB RID: 10731 RVA: 0x00167A43 File Offset: 0x00165C43
		// (set) Token: 0x060029EC RID: 10732 RVA: 0x00167A4B File Offset: 0x00165C4B
		public bool WithOverride
		{
			get
			{
				return this._withOverride;
			}
			set
			{
				this._withOverride = value;
			}
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x00167A54 File Offset: 0x00165C54
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x00167A60 File Offset: 0x00165C60
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C43 RID: 7235
		private bool _withOverride;
	}
}
