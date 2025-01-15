using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000072 RID: 114
	internal sealed class QueryDesignGroup
	{
		// Token: 0x06000241 RID: 577 RVA: 0x0000BA68 File Offset: 0x00009C68
		internal QueryDesignGroup(IRdmQueryExpression key, string name)
		{
			this._key = key;
			this._name = name;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000BA7E File Offset: 0x00009C7E
		public IRdmQueryExpression Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000BA86 File Offset: 0x00009C86
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x04000186 RID: 390
		private readonly IRdmQueryExpression _key;

		// Token: 0x04000187 RID: 391
		private readonly string _name;
	}
}
