using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000247 RID: 583
	[Serializable]
	internal class GrantStatement : SecurityStatement
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060025CF RID: 9679 RVA: 0x00163532 File Offset: 0x00161732
		// (set) Token: 0x060025D0 RID: 9680 RVA: 0x0016353A File Offset: 0x0016173A
		public bool WithGrantOption
		{
			get
			{
				return this._withGrantOption;
			}
			set
			{
				this._withGrantOption = value;
			}
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x00163543 File Offset: 0x00161743
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x00163550 File Offset: 0x00161750
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

		// Token: 0x04001B27 RID: 6951
		private bool _withGrantOption;
	}
}
