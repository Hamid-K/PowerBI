using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002AC RID: 684
	[Serializable]
	internal abstract class IndexOption : TSqlFragment
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x0600281E RID: 10270 RVA: 0x00165DBC File Offset: 0x00163FBC
		// (set) Token: 0x0600281F RID: 10271 RVA: 0x00165DC4 File Offset: 0x00163FC4
		public IndexOptionKind OptionKind
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

		// Token: 0x06002820 RID: 10272 RVA: 0x00165DCD File Offset: 0x00163FCD
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BCC RID: 7116
		private IndexOptionKind _optionKind;
	}
}
