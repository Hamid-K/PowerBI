using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000360 RID: 864
	[Serializable]
	internal class DbccNamedLiteral : TSqlFragment
	{
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06002C64 RID: 11364 RVA: 0x0016A184 File Offset: 0x00168384
		// (set) Token: 0x06002C65 RID: 11365 RVA: 0x0016A18C File Offset: 0x0016838C
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06002C66 RID: 11366 RVA: 0x0016A195 File Offset: 0x00168395
		// (set) Token: 0x06002C67 RID: 11367 RVA: 0x0016A19D File Offset: 0x0016839D
		public ScalarExpression Value
		{
			get
			{
				return this._value;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._value = value;
			}
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x0016A1AD File Offset: 0x001683AD
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x0016A1B9 File Offset: 0x001683B9
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001D02 RID: 7426
		private string _name;

		// Token: 0x04001D03 RID: 7427
		private ScalarExpression _value;
	}
}
