using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000484 RID: 1156
	[Serializable]
	internal class SpatialIndexRegularOption : SpatialIndexOption
	{
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06003322 RID: 13090 RVA: 0x00170DEA File Offset: 0x0016EFEA
		// (set) Token: 0x06003323 RID: 13091 RVA: 0x00170DF2 File Offset: 0x0016EFF2
		public IndexOption Option
		{
			get
			{
				return this._option;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._option = value;
			}
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x00170E02 File Offset: 0x0016F002
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x00170E0E File Offset: 0x0016F00E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Option != null)
			{
				this.Option.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EDF RID: 7903
		private IndexOption _option;
	}
}
