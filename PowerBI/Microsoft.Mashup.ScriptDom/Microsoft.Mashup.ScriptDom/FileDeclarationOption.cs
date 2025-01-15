using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000313 RID: 787
	[Serializable]
	internal class FileDeclarationOption : TSqlFragment
	{
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06002A57 RID: 10839 RVA: 0x00168089 File Offset: 0x00166289
		// (set) Token: 0x06002A58 RID: 10840 RVA: 0x00168091 File Offset: 0x00166291
		public FileDeclarationOptionKind OptionKind
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

		// Token: 0x06002A59 RID: 10841 RVA: 0x0016809A File Offset: 0x0016629A
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x001680A6 File Offset: 0x001662A6
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C60 RID: 7264
		private FileDeclarationOptionKind _optionKind;
	}
}
