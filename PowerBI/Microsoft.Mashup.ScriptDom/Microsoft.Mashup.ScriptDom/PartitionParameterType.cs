using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000363 RID: 867
	[Serializable]
	internal class PartitionParameterType : TSqlFragment, ICollationSetter
	{
		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06002C82 RID: 11394 RVA: 0x0016A399 File Offset: 0x00168599
		// (set) Token: 0x06002C83 RID: 11395 RVA: 0x0016A3A1 File Offset: 0x001685A1
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

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06002C84 RID: 11396 RVA: 0x0016A3B1 File Offset: 0x001685B1
		// (set) Token: 0x06002C85 RID: 11397 RVA: 0x0016A3B9 File Offset: 0x001685B9
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

		// Token: 0x06002C86 RID: 11398 RVA: 0x0016A3C9 File Offset: 0x001685C9
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x0016A3D5 File Offset: 0x001685D5
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
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

		// Token: 0x04001D0D RID: 7437
		private DataTypeReference _dataType;

		// Token: 0x04001D0E RID: 7438
		private Identifier _collation;
	}
}
