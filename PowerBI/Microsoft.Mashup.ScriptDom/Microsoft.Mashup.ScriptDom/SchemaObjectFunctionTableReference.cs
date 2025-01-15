using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001E1 RID: 481
	[Serializable]
	internal class SchemaObjectFunctionTableReference : TableReferenceWithAliasAndColumns
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06002338 RID: 9016 RVA: 0x001604E5 File Offset: 0x0015E6E5
		// (set) Token: 0x06002339 RID: 9017 RVA: 0x001604ED File Offset: 0x0015E6ED
		public SchemaObjectName SchemaObject
		{
			get
			{
				return this._schemaObject;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._schemaObject = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600233A RID: 9018 RVA: 0x001604FD File Offset: 0x0015E6FD
		public IList<ScalarExpression> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x00160505 File Offset: 0x0015E705
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x00160514 File Offset: 0x0015E714
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.SchemaObject != null)
			{
				this.SchemaObject.Accept(visitor);
			}
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.Parameters.Count;
			while (i < count)
			{
				this.Parameters[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001A5F RID: 6751
		private SchemaObjectName _schemaObject;

		// Token: 0x04001A60 RID: 6752
		private List<ScalarExpression> _parameters = new List<ScalarExpression>();
	}
}
