using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200024A RID: 586
	[Serializable]
	internal class AlterAuthorizationStatement : TSqlStatement
	{
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060025E0 RID: 9696 RVA: 0x00163751 File Offset: 0x00161951
		// (set) Token: 0x060025E1 RID: 9697 RVA: 0x00163759 File Offset: 0x00161959
		public SecurityTargetObject SecurityTargetObject
		{
			get
			{
				return this._securityTargetObject;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._securityTargetObject = value;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060025E2 RID: 9698 RVA: 0x00163769 File Offset: 0x00161969
		// (set) Token: 0x060025E3 RID: 9699 RVA: 0x00163771 File Offset: 0x00161971
		public bool ToSchemaOwner
		{
			get
			{
				return this._toSchemaOwner;
			}
			set
			{
				this._toSchemaOwner = value;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060025E4 RID: 9700 RVA: 0x0016377A File Offset: 0x0016197A
		// (set) Token: 0x060025E5 RID: 9701 RVA: 0x00163782 File Offset: 0x00161982
		public Identifier PrincipalName
		{
			get
			{
				return this._principalName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._principalName = value;
			}
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x00163792 File Offset: 0x00161992
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x0016379E File Offset: 0x0016199E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.SecurityTargetObject != null)
			{
				this.SecurityTargetObject.Accept(visitor);
			}
			if (this.PrincipalName != null)
			{
				this.PrincipalName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B2B RID: 6955
		private SecurityTargetObject _securityTargetObject;

		// Token: 0x04001B2C RID: 6956
		private bool _toSchemaOwner;

		// Token: 0x04001B2D RID: 6957
		private Identifier _principalName;
	}
}
