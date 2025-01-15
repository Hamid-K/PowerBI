using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200044F RID: 1103
	[Serializable]
	internal abstract class AuditTargetOption : TSqlFragment
	{
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060031F1 RID: 12785 RVA: 0x0016FB81 File Offset: 0x0016DD81
		// (set) Token: 0x060031F2 RID: 12786 RVA: 0x0016FB89 File Offset: 0x0016DD89
		public AuditTargetOptionKind OptionKind
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

		// Token: 0x060031F3 RID: 12787 RVA: 0x0016FB92 File Offset: 0x0016DD92
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E8C RID: 7820
		private AuditTargetOptionKind _optionKind;
	}
}
