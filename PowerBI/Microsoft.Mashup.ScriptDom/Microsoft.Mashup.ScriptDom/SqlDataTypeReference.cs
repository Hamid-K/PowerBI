using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001D5 RID: 469
	[Serializable]
	internal class SqlDataTypeReference : ParameterizedDataTypeReference
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060022FB RID: 8955 RVA: 0x001600D5 File Offset: 0x0015E2D5
		// (set) Token: 0x060022FC RID: 8956 RVA: 0x001600DD File Offset: 0x0015E2DD
		public SqlDataTypeOption SqlDataTypeOption
		{
			get
			{
				return this._sqlDataTypeOption;
			}
			set
			{
				this._sqlDataTypeOption = value;
			}
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x001600E6 File Offset: 0x0015E2E6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x001600F2 File Offset: 0x0015E2F2
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A4F RID: 6735
		private SqlDataTypeOption _sqlDataTypeOption;
	}
}
