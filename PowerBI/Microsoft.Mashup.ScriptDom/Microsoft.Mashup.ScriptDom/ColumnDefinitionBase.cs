using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200033B RID: 827
	[Serializable]
	internal class ColumnDefinitionBase : TSqlFragment, ICollationSetter
	{
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06002B55 RID: 11093 RVA: 0x00168E3D File Offset: 0x0016703D
		// (set) Token: 0x06002B56 RID: 11094 RVA: 0x00168E45 File Offset: 0x00167045
		public Identifier ColumnIdentifier
		{
			get
			{
				return this._columnIdentifier;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._columnIdentifier = value;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06002B57 RID: 11095 RVA: 0x00168E55 File Offset: 0x00167055
		// (set) Token: 0x06002B58 RID: 11096 RVA: 0x00168E5D File Offset: 0x0016705D
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

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06002B59 RID: 11097 RVA: 0x00168E6D File Offset: 0x0016706D
		// (set) Token: 0x06002B5A RID: 11098 RVA: 0x00168E75 File Offset: 0x00167075
		public Identifier Collation
		{
			get
			{
				return this._collation;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._collation = value;
			}
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x00168E85 File Offset: 0x00167085
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x00168E94 File Offset: 0x00167094
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.ColumnIdentifier != null)
			{
				this.ColumnIdentifier.Accept(visitor);
			}
			if (this.DataType != null)
			{
				this.DataType.Accept(visitor);
			}
			if (this.Collation != null)
			{
				this.Collation.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001CA6 RID: 7334
		private Identifier _columnIdentifier;

		// Token: 0x04001CA7 RID: 7335
		private DataTypeReference _dataType;

		// Token: 0x04001CA8 RID: 7336
		private Identifier _collation;
	}
}
