using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000485 RID: 1157
	[Serializable]
	internal class BoundingBoxSpatialIndexOption : SpatialIndexOption
	{
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06003327 RID: 13095 RVA: 0x00170E33 File Offset: 0x0016F033
		public IList<BoundingBoxParameter> BoundingBoxParameters
		{
			get
			{
				return this._boundingBoxParameters;
			}
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x00170E3B File Offset: 0x0016F03B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x00170E48 File Offset: 0x0016F048
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.BoundingBoxParameters.Count;
			while (i < count)
			{
				this.BoundingBoxParameters[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EE0 RID: 7904
		private List<BoundingBoxParameter> _boundingBoxParameters = new List<BoundingBoxParameter>();
	}
}
