using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003A6 RID: 934
	[Serializable]
	internal class BuiltInFunctionTableReference : TableReferenceWithAlias
	{
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06002E27 RID: 11815 RVA: 0x0016BF24 File Offset: 0x0016A124
		// (set) Token: 0x06002E28 RID: 11816 RVA: 0x0016BF2C File Offset: 0x0016A12C
		public Identifier Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06002E29 RID: 11817 RVA: 0x0016BF3C File Offset: 0x0016A13C
		public IList<ScalarExpression> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x0016BF44 File Offset: 0x0016A144
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x0016BF50 File Offset: 0x0016A150
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			int i = 0;
			int count = this.Parameters.Count;
			while (i < count)
			{
				this.Parameters[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001D88 RID: 7560
		private Identifier _name;

		// Token: 0x04001D89 RID: 7561
		private List<ScalarExpression> _parameters = new List<ScalarExpression>();
	}
}
