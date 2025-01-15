using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000277 RID: 631
	[Serializable]
	internal class OnOffAssemblyOption : AssemblyOption
	{
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060026D9 RID: 9945 RVA: 0x001647A8 File Offset: 0x001629A8
		// (set) Token: 0x060026DA RID: 9946 RVA: 0x001647B0 File Offset: 0x001629B0
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

		// Token: 0x060026DB RID: 9947 RVA: 0x001647B9 File Offset: 0x001629B9
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x001647C5 File Offset: 0x001629C5
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B6F RID: 7023
		private OptionState _optionState;
	}
}
