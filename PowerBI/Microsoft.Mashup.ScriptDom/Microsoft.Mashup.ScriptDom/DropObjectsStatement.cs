using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000271 RID: 625
	[Serializable]
	internal abstract class DropObjectsStatement : TSqlStatement
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060026BC RID: 9916 RVA: 0x001644D4 File Offset: 0x001626D4
		public IList<SchemaObjectName> Objects
		{
			get
			{
				return this._objects;
			}
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x001644DC File Offset: 0x001626DC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				this.Objects[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B66 RID: 7014
		private List<SchemaObjectName> _objects = new List<SchemaObjectName>();
	}
}
