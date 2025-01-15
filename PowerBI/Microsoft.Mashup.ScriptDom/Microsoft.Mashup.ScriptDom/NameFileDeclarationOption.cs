using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000314 RID: 788
	[Serializable]
	internal class NameFileDeclarationOption : FileDeclarationOption
	{
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06002A5C RID: 10844 RVA: 0x001680B7 File Offset: 0x001662B7
		// (set) Token: 0x06002A5D RID: 10845 RVA: 0x001680BF File Offset: 0x001662BF
		public IdentifierOrValueExpression LogicalFileName
		{
			get
			{
				return this._logicalFileName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._logicalFileName = value;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06002A5E RID: 10846 RVA: 0x001680CF File Offset: 0x001662CF
		// (set) Token: 0x06002A5F RID: 10847 RVA: 0x001680D7 File Offset: 0x001662D7
		public bool IsNewName
		{
			get
			{
				return this._isNewName;
			}
			set
			{
				this._isNewName = value;
			}
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x001680E0 File Offset: 0x001662E0
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x001680EC File Offset: 0x001662EC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.LogicalFileName != null)
			{
				this.LogicalFileName.Accept(visitor);
			}
		}

		// Token: 0x04001C61 RID: 7265
		private IdentifierOrValueExpression _logicalFileName;

		// Token: 0x04001C62 RID: 7266
		private bool _isNewName;
	}
}
