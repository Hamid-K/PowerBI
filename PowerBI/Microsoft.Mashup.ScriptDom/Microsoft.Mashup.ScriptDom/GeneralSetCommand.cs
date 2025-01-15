using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200030A RID: 778
	[Serializable]
	internal class GeneralSetCommand : SetCommand
	{
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06002A1B RID: 10779 RVA: 0x00167C48 File Offset: 0x00165E48
		// (set) Token: 0x06002A1C RID: 10780 RVA: 0x00167C50 File Offset: 0x00165E50
		public GeneralSetCommandType CommandType
		{
			get
			{
				return this._commandType;
			}
			set
			{
				this._commandType = value;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06002A1D RID: 10781 RVA: 0x00167C59 File Offset: 0x00165E59
		// (set) Token: 0x06002A1E RID: 10782 RVA: 0x00167C61 File Offset: 0x00165E61
		public ScalarExpression Parameter
		{
			get
			{
				return this._parameter;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._parameter = value;
			}
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x00167C71 File Offset: 0x00165E71
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x00167C7D File Offset: 0x00165E7D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Parameter != null)
			{
				this.Parameter.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C4D RID: 7245
		private GeneralSetCommandType _commandType;

		// Token: 0x04001C4E RID: 7246
		private ScalarExpression _parameter;
	}
}
