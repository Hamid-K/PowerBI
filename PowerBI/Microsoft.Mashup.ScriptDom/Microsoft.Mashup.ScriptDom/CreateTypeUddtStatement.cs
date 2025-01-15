using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000297 RID: 663
	[Serializable]
	internal class CreateTypeUddtStatement : CreateTypeStatement
	{
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06002796 RID: 10134 RVA: 0x0016537D File Offset: 0x0016357D
		// (set) Token: 0x06002797 RID: 10135 RVA: 0x00165385 File Offset: 0x00163585
		public DataTypeReference DataType
		{
			get
			{
				return this._dataType;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._dataType = value;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06002798 RID: 10136 RVA: 0x00165395 File Offset: 0x00163595
		// (set) Token: 0x06002799 RID: 10137 RVA: 0x0016539D File Offset: 0x0016359D
		public NullableConstraintDefinition NullableConstraint
		{
			get
			{
				return this._nullableConstraint;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._nullableConstraint = value;
			}
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x001653AD File Offset: 0x001635AD
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x001653B9 File Offset: 0x001635B9
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.Name != null)
			{
				base.Name.Accept(visitor);
			}
			if (this.DataType != null)
			{
				this.DataType.Accept(visitor);
			}
			if (this.NullableConstraint != null)
			{
				this.NullableConstraint.Accept(visitor);
			}
		}

		// Token: 0x04001BA3 RID: 7075
		private DataTypeReference _dataType;

		// Token: 0x04001BA4 RID: 7076
		private NullableConstraintDefinition _nullableConstraint;
	}
}
