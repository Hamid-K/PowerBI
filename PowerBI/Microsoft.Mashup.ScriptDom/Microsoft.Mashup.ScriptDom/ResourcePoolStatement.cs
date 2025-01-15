using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000458 RID: 1112
	[Serializable]
	internal class ResourcePoolStatement : TSqlStatement
	{
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06003220 RID: 12832 RVA: 0x0016FD95 File Offset: 0x0016DF95
		// (set) Token: 0x06003221 RID: 12833 RVA: 0x0016FD9D File Offset: 0x0016DF9D
		public Identifier Name
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

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06003222 RID: 12834 RVA: 0x0016FDAD File Offset: 0x0016DFAD
		public IList<ResourcePoolParameter> ResourcePoolParameters
		{
			get
			{
				return this._resourcePoolParameters;
			}
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x0016FDB5 File Offset: 0x0016DFB5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x0016FDC4 File Offset: 0x0016DFC4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			int i = 0;
			int count = this.ResourcePoolParameters.Count;
			while (i < count)
			{
				this.ResourcePoolParameters[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E97 RID: 7831
		private Identifier _name;

		// Token: 0x04001E98 RID: 7832
		private List<ResourcePoolParameter> _resourcePoolParameters = new List<ResourcePoolParameter>();
	}
}
