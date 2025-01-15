using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000345 RID: 837
	[Serializable]
	internal class CheckConstraintDefinition : ConstraintDefinition
	{
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06002BAD RID: 11181 RVA: 0x0016944E File Offset: 0x0016764E
		// (set) Token: 0x06002BAE RID: 11182 RVA: 0x00169456 File Offset: 0x00167656
		public BooleanExpression CheckCondition
		{
			get
			{
				return this._checkCondition;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._checkCondition = value;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06002BAF RID: 11183 RVA: 0x00169466 File Offset: 0x00167666
		// (set) Token: 0x06002BB0 RID: 11184 RVA: 0x0016946E File Offset: 0x0016766E
		public bool NotForReplication
		{
			get
			{
				return this._notForReplication;
			}
			set
			{
				this._notForReplication = value;
			}
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x00169477 File Offset: 0x00167677
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x00169483 File Offset: 0x00167683
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.CheckCondition != null)
			{
				this.CheckCondition.Accept(visitor);
			}
		}

		// Token: 0x04001CC5 RID: 7365
		private BooleanExpression _checkCondition;

		// Token: 0x04001CC6 RID: 7366
		private bool _notForReplication;
	}
}
