using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200049C RID: 1180
	[Serializable]
	internal class CreateFederationStatement : TSqlStatement
	{
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x0600339B RID: 13211 RVA: 0x00171524 File Offset: 0x0016F724
		// (set) Token: 0x0600339C RID: 13212 RVA: 0x0017152C File Offset: 0x0016F72C
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

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x0600339D RID: 13213 RVA: 0x0017153C File Offset: 0x0016F73C
		// (set) Token: 0x0600339E RID: 13214 RVA: 0x00171544 File Offset: 0x0016F744
		public Identifier DistributionName
		{
			get
			{
				return this._distributionName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._distributionName = value;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600339F RID: 13215 RVA: 0x00171554 File Offset: 0x0016F754
		// (set) Token: 0x060033A0 RID: 13216 RVA: 0x0017155C File Offset: 0x0016F75C
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

		// Token: 0x060033A1 RID: 13217 RVA: 0x0017156C File Offset: 0x0016F76C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x00171578 File Offset: 0x0016F778
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			if (this.DistributionName != null)
			{
				this.DistributionName.Accept(visitor);
			}
			if (this.DataType != null)
			{
				this.DataType.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EFD RID: 7933
		private Identifier _name;

		// Token: 0x04001EFE RID: 7934
		private Identifier _distributionName;

		// Token: 0x04001EFF RID: 7935
		private DataTypeReference _dataType;
	}
}
