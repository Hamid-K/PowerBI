using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002BF RID: 703
	[Serializable]
	internal abstract class ApplicationRoleStatement : TSqlStatement
	{
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06002896 RID: 10390 RVA: 0x00166495 File Offset: 0x00164695
		// (set) Token: 0x06002897 RID: 10391 RVA: 0x0016649D File Offset: 0x0016469D
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

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06002898 RID: 10392 RVA: 0x001664AD File Offset: 0x001646AD
		public IList<ApplicationRoleOption> ApplicationRoleOptions
		{
			get
			{
				return this._applicationRoleOptions;
			}
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x001664B8 File Offset: 0x001646B8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			int i = 0;
			int count = this.ApplicationRoleOptions.Count;
			while (i < count)
			{
				this.ApplicationRoleOptions[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BEF RID: 7151
		private Identifier _name;

		// Token: 0x04001BF0 RID: 7152
		private List<ApplicationRoleOption> _applicationRoleOptions = new List<ApplicationRoleOption>();
	}
}
