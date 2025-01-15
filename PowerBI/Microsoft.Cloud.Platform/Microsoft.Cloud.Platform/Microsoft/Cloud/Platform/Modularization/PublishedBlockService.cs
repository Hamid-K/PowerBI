using System;
using System.Globalization;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000B5 RID: 181
	internal class PublishedBlockService
	{
		// Token: 0x0600053B RID: 1339 RVA: 0x000134BC File Offset: 0x000116BC
		internal PublishedBlockService(string name, object service, Type serviceType, BlockServiceProviderIdentity serviceIdentity, IBlock serviceProvider)
		{
			this.m_name = name;
			this.m_service = service;
			this.m_serviceType = serviceType;
			this.m_serviceIdentity = serviceIdentity;
			this.m_serviceProvider = serviceProvider;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000134E9 File Offset: 0x000116E9
		internal PublishedBlockService(object service, Type serviceType, BlockServiceProviderIdentity serviceIdentity, IBlock serviceProvider)
			: this(null, service, serviceType, serviceIdentity, serviceProvider)
		{
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000134F8 File Offset: 0x000116F8
		internal bool Matches(RequestedBlockService request)
		{
			return ((this.m_name != null) ? this.m_name.Equals(request.Name, StringComparison.OrdinalIgnoreCase) : (request.Name == null)) && this.m_serviceType == request.ServiceType && this.m_serviceIdentity == request.ServiceIdentity;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00013550 File Offset: 0x00011750
		internal bool PartiallyMatches(RequestedBlockService request)
		{
			return ((this.m_name != null) ? this.m_name.Equals(request.Name, StringComparison.OrdinalIgnoreCase) : (request.Name == null)) && this.m_serviceType == request.ServiceType && this.m_serviceIdentity == BlockServiceProviderIdentity.Implementation;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000135A4 File Offset: 0x000117A4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "<Name: '{0}', Service: '{1}', Type: '{2}', Level: '{3}', Provider: '{4}'>", new object[]
			{
				this.m_name,
				this.m_service,
				this.m_serviceType,
				this.m_serviceIdentity,
				this.m_serviceProvider.Name
			});
		}

		// Token: 0x040001CF RID: 463
		internal string m_name;

		// Token: 0x040001D0 RID: 464
		internal object m_service;

		// Token: 0x040001D1 RID: 465
		internal Type m_serviceType;

		// Token: 0x040001D2 RID: 466
		internal BlockServiceProviderIdentity m_serviceIdentity;

		// Token: 0x040001D3 RID: 467
		internal IBlock m_serviceProvider;
	}
}
