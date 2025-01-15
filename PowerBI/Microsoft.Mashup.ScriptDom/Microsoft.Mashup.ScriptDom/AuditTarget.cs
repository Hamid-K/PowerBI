using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000449 RID: 1097
	[Serializable]
	internal class AuditTarget : TSqlFragment
	{
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060031D3 RID: 12755 RVA: 0x0016F9F9 File Offset: 0x0016DBF9
		// (set) Token: 0x060031D4 RID: 12756 RVA: 0x0016FA01 File Offset: 0x0016DC01
		public AuditTargetKind TargetKind
		{
			get
			{
				return this._targetKind;
			}
			set
			{
				this._targetKind = value;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060031D5 RID: 12757 RVA: 0x0016FA0A File Offset: 0x0016DC0A
		public IList<AuditTargetOption> TargetOptions
		{
			get
			{
				return this._targetOptions;
			}
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x0016FA12 File Offset: 0x0016DC12
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x0016FA20 File Offset: 0x0016DC20
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.TargetOptions.Count;
			while (i < count)
			{
				this.TargetOptions[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E85 RID: 7813
		private AuditTargetKind _targetKind;

		// Token: 0x04001E86 RID: 7814
		private List<AuditTargetOption> _targetOptions = new List<AuditTargetOption>();
	}
}
