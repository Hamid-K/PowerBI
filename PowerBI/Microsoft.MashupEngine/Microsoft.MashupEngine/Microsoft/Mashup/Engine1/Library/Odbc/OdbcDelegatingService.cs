using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005EC RID: 1516
	internal abstract class OdbcDelegatingService : IOdbcService
	{
		// Token: 0x06002FC4 RID: 12228 RVA: 0x000907EF File Offset: 0x0008E9EF
		public OdbcDelegatingService(IOdbcService service)
		{
			this.service = service;
		}

		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x06002FC5 RID: 12229 RVA: 0x000907FE File Offset: 0x0008E9FE
		public virtual int PageSize
		{
			get
			{
				return this.service.PageSize;
			}
		}

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x06002FC6 RID: 12230 RVA: 0x0009080B File Offset: 0x0008EA0B
		public IOdbcService InnerService
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x00090813 File Offset: 0x0008EA13
		public virtual IOdbcConnection CreateConnection(OdbcConnectionProperties args)
		{
			return this.service.CreateConnection(args);
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x00090821 File Offset: 0x0008EA21
		public virtual IList<string> GetInstalledDrivers()
		{
			return this.service.GetInstalledDrivers();
		}

		// Token: 0x04001512 RID: 5394
		private readonly IOdbcService service;
	}
}
