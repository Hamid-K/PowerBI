using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FCB RID: 4043
	internal class ActiveDirectoryEnvironment
	{
		// Token: 0x06006A1D RID: 27165 RVA: 0x0016D772 File Offset: 0x0016B972
		public ActiveDirectoryEnvironment(IEngineHost host, ActiveDirectoryServiceAccessor service)
		{
			this.service = service;
			this.typeCatalog = new ActiveDirectoryTypeCatalog(host, this.service);
			this.valueBuilder = new ActiveDirectoryValueBuilder(this.service, this.typeCatalog);
		}

		// Token: 0x17001E78 RID: 7800
		// (get) Token: 0x06006A1E RID: 27166 RVA: 0x0016D7AA File Offset: 0x0016B9AA
		public ActiveDirectoryTypeCatalog TypeCatalog
		{
			get
			{
				return this.typeCatalog;
			}
		}

		// Token: 0x17001E79 RID: 7801
		// (get) Token: 0x06006A1F RID: 27167 RVA: 0x0016D7B2 File Offset: 0x0016B9B2
		public ActiveDirectoryValueBuilder ValueBuilder
		{
			get
			{
				return this.valueBuilder;
			}
		}

		// Token: 0x17001E7A RID: 7802
		// (get) Token: 0x06006A20 RID: 27168 RVA: 0x0016D7BA File Offset: 0x0016B9BA
		public ActiveDirectoryServiceAccessor Service
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x17001E7B RID: 7803
		// (get) Token: 0x06006A21 RID: 27169 RVA: 0x0016D7C2 File Offset: 0x0016B9C2
		public IEngineHost EngineHost
		{
			get
			{
				return this.service.Host;
			}
		}

		// Token: 0x04003AE8 RID: 15080
		private readonly ActiveDirectoryTypeCatalog typeCatalog;

		// Token: 0x04003AE9 RID: 15081
		private readonly ActiveDirectoryValueBuilder valueBuilder;

		// Token: 0x04003AEA RID: 15082
		private readonly ActiveDirectoryServiceAccessor service;
	}
}
