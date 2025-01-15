using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200044A RID: 1098
	[Serializable]
	internal abstract class AuditOption : TSqlFragment
	{
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060031D9 RID: 12761 RVA: 0x0016FA71 File Offset: 0x0016DC71
		// (set) Token: 0x060031DA RID: 12762 RVA: 0x0016FA79 File Offset: 0x0016DC79
		public AuditOptionKind OptionKind
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

		// Token: 0x060031DB RID: 12763 RVA: 0x0016FA82 File Offset: 0x0016DC82
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E87 RID: 7815
		private AuditOptionKind _optionKind;
	}
}
