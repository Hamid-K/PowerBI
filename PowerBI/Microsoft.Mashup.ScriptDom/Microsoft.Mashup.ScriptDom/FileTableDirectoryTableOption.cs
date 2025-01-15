using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000286 RID: 646
	[Serializable]
	internal class FileTableDirectoryTableOption : TableOption
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600272C RID: 10028 RVA: 0x00164C93 File Offset: 0x00162E93
		// (set) Token: 0x0600272D RID: 10029 RVA: 0x00164C9B File Offset: 0x00162E9B
		public Literal Value
		{
			get
			{
				return this._value;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._value = value;
			}
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x00164CAB File Offset: 0x00162EAB
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x00164CB7 File Offset: 0x00162EB7
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
		}

		// Token: 0x04001B84 RID: 7044
		private Literal _value;
	}
}
