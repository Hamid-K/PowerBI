using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200041E RID: 1054
	[Serializable]
	internal abstract class DialogOption : TSqlFragment
	{
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06003101 RID: 12545 RVA: 0x0016EE0D File Offset: 0x0016D00D
		// (set) Token: 0x06003102 RID: 12546 RVA: 0x0016EE15 File Offset: 0x0016D015
		public DialogOptionKind OptionKind
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

		// Token: 0x06003103 RID: 12547 RVA: 0x0016EE1E File Offset: 0x0016D01E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E55 RID: 7765
		private DialogOptionKind _optionKind;
	}
}
