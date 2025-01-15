using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000248 RID: 584
	[Serializable]
	internal class DenyStatement : SecurityStatement
	{
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060025D4 RID: 9684 RVA: 0x001635E1 File Offset: 0x001617E1
		// (set) Token: 0x060025D5 RID: 9685 RVA: 0x001635E9 File Offset: 0x001617E9
		public bool CascadeOption
		{
			get
			{
				return this._cascadeOption;
			}
			set
			{
				this._cascadeOption = value;
			}
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x001635F2 File Offset: 0x001617F2
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x00163600 File Offset: 0x00161800
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = base.Permissions.Count;
			while (i < count)
			{
				base.Permissions[i].Accept(visitor);
				i++;
			}
			if (base.SecurityTargetObject != null)
			{
				base.SecurityTargetObject.Accept(visitor);
			}
			int j = 0;
			int count2 = base.Principals.Count;
			while (j < count2)
			{
				base.Principals[j].Accept(visitor);
				j++;
			}
			if (base.AsClause != null)
			{
				base.AsClause.Accept(visitor);
			}
		}

		// Token: 0x04001B28 RID: 6952
		private bool _cascadeOption;
	}
}
