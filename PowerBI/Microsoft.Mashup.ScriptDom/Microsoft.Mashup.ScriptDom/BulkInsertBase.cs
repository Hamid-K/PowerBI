using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000357 RID: 855
	[Serializable]
	internal abstract class BulkInsertBase : TSqlStatement
	{
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06002C2D RID: 11309 RVA: 0x00169DA9 File Offset: 0x00167FA9
		// (set) Token: 0x06002C2E RID: 11310 RVA: 0x00169DB1 File Offset: 0x00167FB1
		public SchemaObjectName To
		{
			get
			{
				return this._to;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._to = value;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06002C2F RID: 11311 RVA: 0x00169DC1 File Offset: 0x00167FC1
		public IList<BulkInsertOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x00169DCC File Offset: 0x00167FCC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.To != null)
			{
				this.To.Accept(visitor);
			}
			int i = 0;
			int count = this.Options.Count;
			while (i < count)
			{
				this.Options[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001CF1 RID: 7409
		private SchemaObjectName _to;

		// Token: 0x04001CF2 RID: 7410
		private List<BulkInsertOption> _options = new List<BulkInsertOption>();
	}
}
