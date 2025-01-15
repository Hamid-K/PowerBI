using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000207 RID: 519
	[Serializable]
	internal class OpenXmlTableReference : TableReferenceWithAlias
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06002428 RID: 9256 RVA: 0x00161665 File Offset: 0x0015F865
		// (set) Token: 0x06002429 RID: 9257 RVA: 0x0016166D File Offset: 0x0015F86D
		public VariableReference Variable
		{
			get
			{
				return this._variable;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._variable = value;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600242A RID: 9258 RVA: 0x0016167D File Offset: 0x0015F87D
		// (set) Token: 0x0600242B RID: 9259 RVA: 0x00161685 File Offset: 0x0015F885
		public ValueExpression RowPattern
		{
			get
			{
				return this._rowPattern;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._rowPattern = value;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600242C RID: 9260 RVA: 0x00161695 File Offset: 0x0015F895
		// (set) Token: 0x0600242D RID: 9261 RVA: 0x0016169D File Offset: 0x0015F89D
		public ValueExpression Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._flags = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600242E RID: 9262 RVA: 0x001616AD File Offset: 0x0015F8AD
		public IList<SchemaDeclarationItem> SchemaDeclarationItems
		{
			get
			{
				return this._schemaDeclarationItems;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600242F RID: 9263 RVA: 0x001616B5 File Offset: 0x0015F8B5
		// (set) Token: 0x06002430 RID: 9264 RVA: 0x001616BD File Offset: 0x0015F8BD
		public SchemaObjectName TableName
		{
			get
			{
				return this._tableName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._tableName = value;
			}
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x001616CD File Offset: 0x0015F8CD
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x001616DC File Offset: 0x0015F8DC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Variable != null)
			{
				this.Variable.Accept(visitor);
			}
			if (this.RowPattern != null)
			{
				this.RowPattern.Accept(visitor);
			}
			if (this.Flags != null)
			{
				this.Flags.Accept(visitor);
			}
			int i = 0;
			int count = this.SchemaDeclarationItems.Count;
			while (i < count)
			{
				this.SchemaDeclarationItems[i].Accept(visitor);
				i++;
			}
			if (this.TableName != null)
			{
				this.TableName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001AA9 RID: 6825
		private VariableReference _variable;

		// Token: 0x04001AAA RID: 6826
		private ValueExpression _rowPattern;

		// Token: 0x04001AAB RID: 6827
		private ValueExpression _flags;

		// Token: 0x04001AAC RID: 6828
		private List<SchemaDeclarationItem> _schemaDeclarationItems = new List<SchemaDeclarationItem>();

		// Token: 0x04001AAD RID: 6829
		private SchemaObjectName _tableName;
	}
}
