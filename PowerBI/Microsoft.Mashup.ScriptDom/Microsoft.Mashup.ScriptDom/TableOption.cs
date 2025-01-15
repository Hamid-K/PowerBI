using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000283 RID: 643
	[Serializable]
	internal abstract class TableOption : TSqlFragment
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600271E RID: 10014 RVA: 0x00164BFA File Offset: 0x00162DFA
		// (set) Token: 0x0600271F RID: 10015 RVA: 0x00164C02 File Offset: 0x00162E02
		public TableOptionKind OptionKind
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

		// Token: 0x06002720 RID: 10016 RVA: 0x00164C0B File Offset: 0x00162E0B
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B81 RID: 7041
		private TableOptionKind _optionKind;
	}
}
