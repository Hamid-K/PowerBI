using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200033C RID: 828
	[Serializable]
	internal class ColumnDefinition : ColumnDefinitionBase
	{
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06002B5E RID: 11102 RVA: 0x00168EEC File Offset: 0x001670EC
		// (set) Token: 0x06002B5F RID: 11103 RVA: 0x00168EF4 File Offset: 0x001670F4
		public ScalarExpression ComputedColumnExpression
		{
			get
			{
				return this._computedColumnExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._computedColumnExpression = value;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06002B60 RID: 11104 RVA: 0x00168F04 File Offset: 0x00167104
		// (set) Token: 0x06002B61 RID: 11105 RVA: 0x00168F0C File Offset: 0x0016710C
		public bool IsPersisted
		{
			get
			{
				return this._isPersisted;
			}
			set
			{
				this._isPersisted = value;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06002B62 RID: 11106 RVA: 0x00168F15 File Offset: 0x00167115
		// (set) Token: 0x06002B63 RID: 11107 RVA: 0x00168F1D File Offset: 0x0016711D
		public DefaultConstraintDefinition DefaultConstraint
		{
			get
			{
				return this._defaultConstraint;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._defaultConstraint = value;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06002B64 RID: 11108 RVA: 0x00168F2D File Offset: 0x0016712D
		// (set) Token: 0x06002B65 RID: 11109 RVA: 0x00168F35 File Offset: 0x00167135
		public IdentityOptions IdentityOptions
		{
			get
			{
				return this._identityOptions;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._identityOptions = value;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06002B66 RID: 11110 RVA: 0x00168F45 File Offset: 0x00167145
		// (set) Token: 0x06002B67 RID: 11111 RVA: 0x00168F4D File Offset: 0x0016714D
		public bool IsRowGuidCol
		{
			get
			{
				return this._isRowGuidCol;
			}
			set
			{
				this._isRowGuidCol = value;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06002B68 RID: 11112 RVA: 0x00168F56 File Offset: 0x00167156
		public IList<ConstraintDefinition> Constraints
		{
			get
			{
				return this._constraints;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06002B69 RID: 11113 RVA: 0x00168F5E File Offset: 0x0016715E
		// (set) Token: 0x06002B6A RID: 11114 RVA: 0x00168F66 File Offset: 0x00167166
		public ColumnStorageOptions StorageOptions
		{
			get
			{
				return this._storageOptions;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._storageOptions = value;
			}
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x00168F76 File Offset: 0x00167176
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x00168F84 File Offset: 0x00167184
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.ComputedColumnExpression != null)
			{
				this.ComputedColumnExpression.Accept(visitor);
			}
			if (this.DefaultConstraint != null)
			{
				this.DefaultConstraint.Accept(visitor);
			}
			if (this.IdentityOptions != null)
			{
				this.IdentityOptions.Accept(visitor);
			}
			int i = 0;
			int count = this.Constraints.Count;
			while (i < count)
			{
				this.Constraints[i].Accept(visitor);
				i++;
			}
			if (this.StorageOptions != null)
			{
				this.StorageOptions.Accept(visitor);
			}
		}

		// Token: 0x04001CA9 RID: 7337
		private ScalarExpression _computedColumnExpression;

		// Token: 0x04001CAA RID: 7338
		private bool _isPersisted;

		// Token: 0x04001CAB RID: 7339
		private DefaultConstraintDefinition _defaultConstraint;

		// Token: 0x04001CAC RID: 7340
		private IdentityOptions _identityOptions;

		// Token: 0x04001CAD RID: 7341
		private bool _isRowGuidCol;

		// Token: 0x04001CAE RID: 7342
		private List<ConstraintDefinition> _constraints = new List<ConstraintDefinition>();

		// Token: 0x04001CAF RID: 7343
		private ColumnStorageOptions _storageOptions;
	}
}
