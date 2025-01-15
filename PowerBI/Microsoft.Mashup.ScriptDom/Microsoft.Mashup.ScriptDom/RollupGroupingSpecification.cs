using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003B9 RID: 953
	[Serializable]
	internal class RollupGroupingSpecification : GroupingSpecification
	{
		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06002EA5 RID: 11941 RVA: 0x0016C7F5 File Offset: 0x0016A9F5
		public IList<GroupingSpecification> Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x0016C7FD File Offset: 0x0016A9FD
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x0016C80C File Offset: 0x0016AA0C
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

		// Token: 0x04001DB0 RID: 7600
		private List<GroupingSpecification> _arguments = new List<GroupingSpecification>();
	}
}
