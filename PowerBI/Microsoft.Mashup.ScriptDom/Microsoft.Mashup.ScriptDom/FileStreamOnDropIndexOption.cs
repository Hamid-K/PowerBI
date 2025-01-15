using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002EE RID: 750
	[Serializable]
	internal class FileStreamOnDropIndexOption : IndexOption, IFileStreamSpecifier
	{
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600298C RID: 10636 RVA: 0x001674FA File Offset: 0x001656FA
		// (set) Token: 0x0600298D RID: 10637 RVA: 0x00167502 File Offset: 0x00165702
		public IdentifierOrValueExpression FileStreamOn
		{
			get
			{
				return this._fileStreamOn;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._fileStreamOn = value;
			}
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x00167512 File Offset: 0x00165712
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x0016751E File Offset: 0x0016571E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.FileStreamOn != null)
			{
				this.FileStreamOn.Accept(visitor);
			}
		}

		// Token: 0x04001C2E RID: 7214
		private IdentifierOrValueExpression _fileStreamOn;
	}
}
