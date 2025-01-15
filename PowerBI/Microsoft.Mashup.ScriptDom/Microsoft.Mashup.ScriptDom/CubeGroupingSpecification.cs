using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003B8 RID: 952
	[Serializable]
	internal class CubeGroupingSpecification : GroupingSpecification
	{
		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06002EA1 RID: 11937 RVA: 0x0016C78D File Offset: 0x0016A98D
		public IList<GroupingSpecification> Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x0016C795 File Offset: 0x0016A995
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x0016C7A4 File Offset: 0x0016A9A4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Arguments.Count;
			while (i < count)
			{
				this.Arguments[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DAF RID: 7599
		private List<GroupingSpecification> _arguments = new List<GroupingSpecification>();
	}
}
