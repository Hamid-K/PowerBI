using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200024C RID: 588
	[Serializable]
	internal class SecurityTargetObject : TSqlFragment
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060025EE RID: 9710 RVA: 0x0016387A File Offset: 0x00161A7A
		// (set) Token: 0x060025EF RID: 9711 RVA: 0x00163882 File Offset: 0x00161A82
		public SecurityObjectKind ObjectKind
		{
			get
			{
				return this._objectKind;
			}
			set
			{
				this._objectKind = value;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060025F0 RID: 9712 RVA: 0x0016388B File Offset: 0x00161A8B
		// (set) Token: 0x060025F1 RID: 9713 RVA: 0x00163893 File Offset: 0x00161A93
		public SecurityTargetObjectName ObjectName
		{
			get
			{
				return this._objectName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._objectName = value;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060025F2 RID: 9714 RVA: 0x001638A3 File Offset: 0x00161AA3
		public IList<Identifier> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x001638AB File Offset: 0x00161AAB
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x001638B8 File Offset: 0x00161AB8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.ObjectName != null)
			{
				this.ObjectName.Accept(visitor);
			}
			int i = 0;
			int count = this.Columns.Count;
			while (i < count)
			{
				this.Columns[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B30 RID: 6960
		private SecurityObjectKind _objectKind;

		// Token: 0x04001B31 RID: 6961
		private SecurityTargetObjectName _objectName;

		// Token: 0x04001B32 RID: 6962
		private List<Identifier> _columns = new List<Identifier>();
	}
}
