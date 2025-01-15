using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Cloud.Platform.Communication;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.CommunicationFramework.Routers
{
	// Token: 0x02000474 RID: 1140
	public class FormattedUriRouter : Router
	{
		// Token: 0x0600237F RID: 9087 RVA: 0x000806FF File Offset: 0x0007E8FF
		public FormattedUriRouter(string uriToFormat)
			: base(true, new RoundRobinRetryPolicy(new List<Type>()))
		{
			this.m_uriToFormat = uriToFormat;
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x0008071C File Offset: 0x0007E91C
		public override IAsyncResult BeginGetEndpoints(object[] keys, AsyncCallback callback, object state)
		{
			ExtendedDiagnostics.EnsureOperation(keys != null, "keys can't be null");
			Uri uri = new Uri(string.Format(CultureInfo.InvariantCulture, this.m_uriToFormat, keys));
			return new CompletedAsyncResult<IEnumerable<Uri>>(callback, state, new List<Uri> { uri });
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x00080761 File Offset: 0x0007E961
		public override IEnumerable<Uri> EndGetEndpoints(IAsyncResult result)
		{
			return CompletedAsyncResult<IEnumerable<Uri>>.End(result);
		}

		// Token: 0x04000C62 RID: 3170
		private string m_uriToFormat;
	}
}
