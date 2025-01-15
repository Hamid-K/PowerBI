using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002E7 RID: 743
	[Serializable]
	internal class DropDatabaseStatement : TSqlStatement
	{
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600296D RID: 10605 RVA: 0x0016726E File Offset: 0x0016546E
		public IList<Identifier> Databases
		{
			get
			{
				return this._databases;
			}
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x00167276 File Offset: 0x00165476
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x00167284 File Offset: 0x00165484
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Databases.Count;
			while (i < count)
			{
				this.Databases[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C26 RID: 7206
		private List<Identifier> _databases = new List<Identifier>();
	}
}
