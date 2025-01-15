using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002BD RID: 701
	[Serializable]
	internal class AlterMasterKeyStatement : MasterKeyStatement
	{
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600288A RID: 10378 RVA: 0x0016640D File Offset: 0x0016460D
		// (set) Token: 0x0600288B RID: 10379 RVA: 0x00166415 File Offset: 0x00164615
		public AlterMasterKeyOption Option
		{
			get
			{
				return this._option;
			}
			set
			{
				this._option = value;
			}
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x0016641E File Offset: 0x0016461E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x0016642A File Offset: 0x0016462A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BEC RID: 7148
		private AlterMasterKeyOption _option;
	}
}
