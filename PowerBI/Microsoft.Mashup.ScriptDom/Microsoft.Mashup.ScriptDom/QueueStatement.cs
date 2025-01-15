using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002A3 RID: 675
	[Serializable]
	internal abstract class QueueStatement : TSqlStatement
	{
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060027D8 RID: 10200 RVA: 0x0016578F File Offset: 0x0016398F
		// (set) Token: 0x060027D9 RID: 10201 RVA: 0x00165797 File Offset: 0x00163997
		public SchemaObjectName Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060027DA RID: 10202 RVA: 0x001657A7 File Offset: 0x001639A7
		public IList<QueueOption> QueueOptions
		{
			get
			{
				return this._queueOptions;
			}
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x001657B0 File Offset: 0x001639B0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			int i = 0;
			int count = this.QueueOptions.Count;
			while (i < count)
			{
				this.QueueOptions[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BB3 RID: 7091
		private SchemaObjectName _name;

		// Token: 0x04001BB4 RID: 7092
		private List<QueueOption> _queueOptions = new List<QueueOption>();
	}
}
