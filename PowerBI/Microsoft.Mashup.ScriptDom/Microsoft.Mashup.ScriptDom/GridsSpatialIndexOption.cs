using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000487 RID: 1159
	[Serializable]
	internal class GridsSpatialIndexOption : SpatialIndexOption
	{
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06003332 RID: 13106 RVA: 0x00170EF3 File Offset: 0x0016F0F3
		public IList<GridParameter> GridParameters
		{
			get
			{
				return this._gridParameters;
			}
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x00170EFB File Offset: 0x0016F0FB
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x00170F08 File Offset: 0x0016F108
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.GridParameters.Count;
			while (i < count)
			{
				this.GridParameters[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EE3 RID: 7907
		private List<GridParameter> _gridParameters = new List<GridParameter>();
	}
}
