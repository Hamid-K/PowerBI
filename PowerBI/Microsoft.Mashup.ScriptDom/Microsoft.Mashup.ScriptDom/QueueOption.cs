using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200029A RID: 666
	[Serializable]
	internal class QueueOption : TSqlFragment
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060027AB RID: 10155 RVA: 0x001654CE File Offset: 0x001636CE
		// (set) Token: 0x060027AC RID: 10156 RVA: 0x001654D6 File Offset: 0x001636D6
		public QueueOptionKind OptionKind
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

		// Token: 0x060027AD RID: 10157 RVA: 0x001654DF File Offset: 0x001636DF
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x001654EB File Offset: 0x001636EB
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BA9 RID: 7081
		private QueueOptionKind _optionKind;
	}
}
