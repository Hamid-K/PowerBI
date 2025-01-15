using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002E8 RID: 744
	[Serializable]
	internal abstract class DropChildObjectsStatement : TSqlStatement
	{
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06002971 RID: 10609 RVA: 0x001672D5 File Offset: 0x001654D5
		public IList<ChildObjectName> Objects
		{
			get
			{
				return this._objects;
			}
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x001672E0 File Offset: 0x001654E0
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

		// Token: 0x04001C27 RID: 7207
		private List<ChildObjectName> _objects = new List<ChildObjectName>();
	}
}
