using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001D8 RID: 472
	[Serializable]
	internal class ScalarFunctionReturnType : FunctionReturnType
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600230A RID: 8970 RVA: 0x0016017A File Offset: 0x0015E37A
		// (set) Token: 0x0600230B RID: 8971 RVA: 0x00160182 File Offset: 0x0015E382
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

		// Token: 0x0600230C RID: 8972 RVA: 0x00160192 File Offset: 0x0015E392
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x0016019E File Offset: 0x0015E39E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.DataType != null)
			{
				this.DataType.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A52 RID: 6738
		private DataTypeReference _dataType;
	}
}
