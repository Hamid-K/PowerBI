using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000366 RID: 870
	[Serializable]
	internal abstract class RemoteServiceBindingOption : TSqlFragment
	{
		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06002C98 RID: 11416 RVA: 0x0016A565 File Offset: 0x00168765
		// (set) Token: 0x06002C99 RID: 11417 RVA: 0x0016A56D File Offset: 0x0016876D
		public RemoteServiceBindingOptionKind OptionKind
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

		// Token: 0x06002C9A RID: 11418 RVA: 0x0016A576 File Offset: 0x00168776
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D15 RID: 7445
		private RemoteServiceBindingOptionKind _optionKind;
	}
}
