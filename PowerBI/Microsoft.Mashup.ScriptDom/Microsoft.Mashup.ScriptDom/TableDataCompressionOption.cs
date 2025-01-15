using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000342 RID: 834
	[Serializable]
	internal class TableDataCompressionOption : TableOption
	{
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06002B9B RID: 11163 RVA: 0x0016931A File Offset: 0x0016751A
		// (set) Token: 0x06002B9C RID: 11164 RVA: 0x00169322 File Offset: 0x00167522
		public DataCompressionOption DataCompressionOption
		{
			get
			{
				return this._dataCompressionOption;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._dataCompressionOption = value;
			}
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x00169332 File Offset: 0x00167532
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x0016933E File Offset: 0x0016753E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.DataCompressionOption != null)
			{
				this.DataCompressionOption.Accept(visitor);
			}
		}

		// Token: 0x04001CC0 RID: 7360
		private DataCompressionOption _dataCompressionOption;
	}
}
