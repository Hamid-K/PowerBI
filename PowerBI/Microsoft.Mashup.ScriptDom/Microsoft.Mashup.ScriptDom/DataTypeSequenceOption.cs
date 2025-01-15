using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200026D RID: 621
	[Serializable]
	internal class DataTypeSequenceOption : SequenceOption
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060026AC RID: 9900 RVA: 0x00164408 File Offset: 0x00162608
		// (set) Token: 0x060026AD RID: 9901 RVA: 0x00164410 File Offset: 0x00162610
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

		// Token: 0x060026AE RID: 9902 RVA: 0x00164420 File Offset: 0x00162620
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x0016442C File Offset: 0x0016262C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.DataType != null)
			{
				this.DataType.Accept(visitor);
			}
		}

		// Token: 0x04001B64 RID: 7012
		private DataTypeReference _dataType;
	}
}
