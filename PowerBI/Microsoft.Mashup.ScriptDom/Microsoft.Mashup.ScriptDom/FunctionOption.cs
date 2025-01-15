using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001C9 RID: 457
	[Serializable]
	internal class FunctionOption : TSqlFragment
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x0015FCE8 File Offset: 0x0015DEE8
		// (set) Token: 0x060022C4 RID: 8900 RVA: 0x0015FCF0 File Offset: 0x0015DEF0
		public FunctionOptionKind OptionKind
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

		// Token: 0x060022C5 RID: 8901 RVA: 0x0015FCF9 File Offset: 0x0015DEF9
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x0015FD05 File Offset: 0x0015DF05
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A41 RID: 6721
		private FunctionOptionKind _optionKind;
	}
}
