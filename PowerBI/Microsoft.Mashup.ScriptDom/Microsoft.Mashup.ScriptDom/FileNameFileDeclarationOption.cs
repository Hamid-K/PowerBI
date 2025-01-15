using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000315 RID: 789
	[Serializable]
	internal class FileNameFileDeclarationOption : FileDeclarationOption
	{
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06002A63 RID: 10851 RVA: 0x00168111 File Offset: 0x00166311
		// (set) Token: 0x06002A64 RID: 10852 RVA: 0x00168119 File Offset: 0x00166319
		public Literal OSFileName
		{
			get
			{
				return this._oSFileName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._oSFileName = value;
			}
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x00168129 File Offset: 0x00166329
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x00168135 File Offset: 0x00166335
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.OSFileName != null)
			{
				this.OSFileName.Accept(visitor);
			}
		}

		// Token: 0x04001C63 RID: 7267
		private Literal _oSFileName;
	}
}
