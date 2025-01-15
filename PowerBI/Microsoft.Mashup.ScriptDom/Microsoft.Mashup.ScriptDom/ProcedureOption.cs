using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001C7 RID: 455
	[Serializable]
	internal class ProcedureOption : TSqlFragment
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060022B9 RID: 8889 RVA: 0x0015FC71 File Offset: 0x0015DE71
		// (set) Token: 0x060022BA RID: 8890 RVA: 0x0015FC79 File Offset: 0x0015DE79
		public ProcedureOptionKind OptionKind
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

		// Token: 0x060022BB RID: 8891 RVA: 0x0015FC82 File Offset: 0x0015DE82
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x0015FC8E File Offset: 0x0015DE8E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A3F RID: 6719
		private ProcedureOptionKind _optionKind;
	}
}
