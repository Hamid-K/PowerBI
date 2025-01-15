using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001CC RID: 460
	[Serializable]
	internal abstract class XmlNamespacesElement : TSqlFragment
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x0015FDC5 File Offset: 0x0015DFC5
		// (set) Token: 0x060022D2 RID: 8914 RVA: 0x0015FDCD File Offset: 0x0015DFCD
		public StringLiteral String
		{
			get
			{
				return this._string;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._string = value;
			}
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x0015FDDD File Offset: 0x0015DFDD
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.String != null)
			{
				this.String.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A44 RID: 6724
		private StringLiteral _string;
	}
}
