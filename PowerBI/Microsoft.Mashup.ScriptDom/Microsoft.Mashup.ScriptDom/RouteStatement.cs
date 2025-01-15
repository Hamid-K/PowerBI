using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002A0 RID: 672
	[Serializable]
	internal abstract class RouteStatement : TSqlStatement
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060027CB RID: 10187 RVA: 0x0016565F File Offset: 0x0016385F
		// (set) Token: 0x060027CC RID: 10188 RVA: 0x00165667 File Offset: 0x00163867
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

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060027CD RID: 10189 RVA: 0x00165677 File Offset: 0x00163877
		public IList<RouteOption> RouteOptions
		{
			get
			{
				return this._routeOptions;
			}
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x00165680 File Offset: 0x00163880
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			int i = 0;
			int count = this.RouteOptions.Count;
			while (i < count)
			{
				this.RouteOptions[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BB0 RID: 7088
		private Identifier _name;

		// Token: 0x04001BB1 RID: 7089
		private List<RouteOption> _routeOptions = new List<RouteOption>();
	}
}
