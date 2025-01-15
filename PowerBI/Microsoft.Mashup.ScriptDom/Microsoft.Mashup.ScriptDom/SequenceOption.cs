using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200026C RID: 620
	[Serializable]
	internal class SequenceOption : TSqlFragment
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060026A5 RID: 9893 RVA: 0x001643C9 File Offset: 0x001625C9
		// (set) Token: 0x060026A6 RID: 9894 RVA: 0x001643D1 File Offset: 0x001625D1
		public SequenceOptionKind OptionKind
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

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060026A7 RID: 9895 RVA: 0x001643DA File Offset: 0x001625DA
		// (set) Token: 0x060026A8 RID: 9896 RVA: 0x001643E2 File Offset: 0x001625E2
		public bool NoValue
		{
			get
			{
				return this._noValue;
			}
			set
			{
				this._noValue = value;
			}
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x001643EB File Offset: 0x001625EB
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x001643F7 File Offset: 0x001625F7
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B62 RID: 7010
		private SequenceOptionKind _optionKind;

		// Token: 0x04001B63 RID: 7011
		private bool _noValue;
	}
}
