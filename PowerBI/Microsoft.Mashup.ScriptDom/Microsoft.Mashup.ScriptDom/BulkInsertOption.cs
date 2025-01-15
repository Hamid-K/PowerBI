using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200035A RID: 858
	[Serializable]
	internal class BulkInsertOption : TSqlFragment
	{
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06002C3B RID: 11323 RVA: 0x00169F25 File Offset: 0x00168125
		// (set) Token: 0x06002C3C RID: 11324 RVA: 0x00169F2D File Offset: 0x0016812D
		public BulkInsertOptionKind OptionKind
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

		// Token: 0x06002C3D RID: 11325 RVA: 0x00169F36 File Offset: 0x00168136
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x00169F42 File Offset: 0x00168142
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001CF5 RID: 7413
		private BulkInsertOptionKind _optionKind;
	}
}
