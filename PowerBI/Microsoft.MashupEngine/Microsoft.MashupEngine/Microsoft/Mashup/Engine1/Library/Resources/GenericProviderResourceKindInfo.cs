using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Library.Resources
{
	// Token: 0x0200050B RID: 1291
	internal class GenericProviderResourceKindInfo : ResourceKindInfo
	{
		// Token: 0x060029E8 RID: 10728 RVA: 0x0007D5A8 File Offset: 0x0007B7A8
		public GenericProviderResourceKindInfo(string kind, string label, ConnectionStringHandler connectionStringHandler, AuthenticationInfo[] authenticationInfo, IEnumerable<IDataSourceLocationFactory> dslFactories = null)
			: base(kind, label, false, true, false, false, true, true, authenticationInfo, null, null, null, dslFactories)
		{
			this.connectionStringHandler = connectionStringHandler;
		}

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x060029E9 RID: 10729 RVA: 0x0007D5D1 File Offset: 0x0007B7D1
		public ConnectionStringHandler ConnectionStringHandler
		{
			get
			{
				return this.connectionStringHandler;
			}
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x0007D5DC File Offset: 0x0007B7DC
		public override bool Validate(string resourcePath, out IResource resource, out string errorMessage)
		{
			string text;
			if (this.TryNormalizeConnectionString(resourcePath, out text, out errorMessage))
			{
				resource = new Resource(base.Kind, text, resourcePath);
				errorMessage = null;
				return true;
			}
			resource = null;
			return false;
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x0007D60D File Offset: 0x0007B80D
		public override bool TryGetHostName(string resourcePath, out string hostName)
		{
			return this.connectionStringHandler.TryGetHostName(resourcePath, out hostName);
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x0007D61C File Offset: 0x0007B81C
		protected bool TryNormalizeConnectionString(string connectionString, out string normalizedConnectionString, out string errorMessage)
		{
			bool flag;
			try
			{
				normalizedConnectionString = this.connectionStringHandler.ResourcePathNormalize(connectionString);
				errorMessage = null;
				flag = true;
			}
			catch (FormatException ex)
			{
				normalizedConnectionString = null;
				errorMessage = Strings.GenericProviders_InvalidConnectionString(ex.Message);
				flag = false;
			}
			return flag;
		}

		// Token: 0x04001238 RID: 4664
		private readonly ConnectionStringHandler connectionStringHandler;
	}
}
