using System;

namespace System.Web.Http.Filters
{
	// Token: 0x020000CA RID: 202
	public sealed class FilterInfo
	{
		// Token: 0x0600056E RID: 1390 RVA: 0x0000E031 File Offset: 0x0000C231
		public FilterInfo(IFilter instance, FilterScope scope)
		{
			if (instance == null)
			{
				throw Error.ArgumentNull("instance");
			}
			this.Instance = instance;
			this.Scope = scope;
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0000E055 File Offset: 0x0000C255
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x0000E05D File Offset: 0x0000C25D
		public IFilter Instance { get; private set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0000E066 File Offset: 0x0000C266
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x0000E06E File Offset: 0x0000C26E
		public FilterScope Scope { get; private set; }
	}
}
