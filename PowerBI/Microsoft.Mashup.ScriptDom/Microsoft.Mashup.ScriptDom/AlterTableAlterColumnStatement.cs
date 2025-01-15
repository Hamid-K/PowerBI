using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200033A RID: 826
	[Serializable]
	internal class AlterTableAlterColumnStatement : AlterTableStatement, ICollationSetter
	{
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06002B48 RID: 11080 RVA: 0x00168D46 File Offset: 0x00166F46
		// (set) Token: 0x06002B49 RID: 11081 RVA: 0x00168D4E File Offset: 0x00166F4E
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

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06002B4A RID: 11082 RVA: 0x00168D5E File Offset: 0x00166F5E
		// (set) Token: 0x06002B4B RID: 11083 RVA: 0x00168D66 File Offset: 0x00166F66
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

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06002B4C RID: 11084 RVA: 0x00168D76 File Offset: 0x00166F76
		// (set) Token: 0x06002B4D RID: 11085 RVA: 0x00168D7E File Offset: 0x00166F7E
		public AlterTableAlterColumnOption AlterTableAlterColumnOption
		{
			get
			{
				return this._alterTableAlterColumnOption;
			}
			set
			{
				this._alterTableAlterColumnOption = value;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06002B4E RID: 11086 RVA: 0x00168D87 File Offset: 0x00166F87
		// (set) Token: 0x06002B4F RID: 11087 RVA: 0x00168D8F File Offset: 0x00166F8F
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

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06002B50 RID: 11088 RVA: 0x00168D9F File Offset: 0x00166F9F
		// (set) Token: 0x06002B51 RID: 11089 RVA: 0x00168DA7 File Offset: 0x00166FA7
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

		// Token: 0x06002B52 RID: 11090 RVA: 0x00168DB7 File Offset: 0x00166FB7
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x00168DC4 File Offset: 0x00166FC4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.SchemaObjectName != null)
			{
				base.SchemaObjectName.Accept(visitor);
			}
			if (this.ColumnIdentifier != null)
			{
				this.ColumnIdentifier.Accept(visitor);
			}
			if (this.DataType != null)
			{
				this.DataType.Accept(visitor);
			}
			if (this.StorageOptions != null)
			{
				this.StorageOptions.Accept(visitor);
			}
			if (this.Collation != null)
			{
				this.Collation.Accept(visitor);
			}
		}

		// Token: 0x04001CA1 RID: 7329
		private Identifier _columnIdentifier;

		// Token: 0x04001CA2 RID: 7330
		private DataTypeReference _dataType;

		// Token: 0x04001CA3 RID: 7331
		private AlterTableAlterColumnOption _alterTableAlterColumnOption;

		// Token: 0x04001CA4 RID: 7332
		private ColumnStorageOptions _storageOptions;

		// Token: 0x04001CA5 RID: 7333
		private Identifier _collation;
	}
}
