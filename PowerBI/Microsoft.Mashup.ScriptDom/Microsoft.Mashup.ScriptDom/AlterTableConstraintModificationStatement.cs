using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200028A RID: 650
	[Serializable]
	internal class AlterTableConstraintModificationStatement : AlterTableStatement
	{
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06002742 RID: 10050 RVA: 0x00164DD5 File Offset: 0x00162FD5
		// (set) Token: 0x06002743 RID: 10051 RVA: 0x00164DDD File Offset: 0x00162FDD
		public ConstraintEnforcement ExistingRowsCheckEnforcement
		{
			get
			{
				return this._existingRowsCheckEnforcement;
			}
			set
			{
				this._existingRowsCheckEnforcement = value;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06002744 RID: 10052 RVA: 0x00164DE6 File Offset: 0x00162FE6
		// (set) Token: 0x06002745 RID: 10053 RVA: 0x00164DEE File Offset: 0x00162FEE
		public ConstraintEnforcement ConstraintEnforcement
		{
			get
			{
				return this._constraintEnforcement;
			}
			set
			{
				this._constraintEnforcement = value;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06002746 RID: 10054 RVA: 0x00164DF7 File Offset: 0x00162FF7
		// (set) Token: 0x06002747 RID: 10055 RVA: 0x00164DFF File Offset: 0x00162FFF
		public bool All
		{
			get
			{
				return this._all;
			}
			set
			{
				this._all = value;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06002748 RID: 10056 RVA: 0x00164E08 File Offset: 0x00163008
		public IList<Identifier> ConstraintNames
		{
			get
			{
				return this._constraintNames;
			}
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x00164E10 File Offset: 0x00163010
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x00164E1C File Offset: 0x0016301C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.SchemaObjectName != null)
			{
				base.SchemaObjectName.Accept(visitor);
			}
			int i = 0;
			int count = this.ConstraintNames.Count;
			while (i < count)
			{
				this.ConstraintNames[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001B89 RID: 7049
		private ConstraintEnforcement _existingRowsCheckEnforcement;

		// Token: 0x04001B8A RID: 7050
		private ConstraintEnforcement _constraintEnforcement;

		// Token: 0x04001B8B RID: 7051
		private bool _all;

		// Token: 0x04001B8C RID: 7052
		private List<Identifier> _constraintNames = new List<Identifier>();
	}
}
