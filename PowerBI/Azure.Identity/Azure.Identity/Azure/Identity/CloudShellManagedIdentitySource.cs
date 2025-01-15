using System;
using System.Text;
using Azure.Core;

namespace Azure.Identity
{
	// Token: 0x0200002A RID: 42
	internal class CloudShellManagedIdentitySource : ManagedIdentitySource
	{
		// Token: 0x060000FE RID: 254 RVA: 0x00004B04 File Offset: 0x00002D04
		public static ManagedIdentitySource TryCreate(ManagedIdentityClientOptions options)
		{
			string msiEndpoint = EnvironmentVariables.MsiEndpoint;
			if (string.IsNullOrEmpty(msiEndpoint))
			{
				return null;
			}
			Uri uri;
			try
			{
				uri = new Uri(msiEndpoint);
			}
			catch (FormatException ex)
			{
				throw new AuthenticationFailedException("The environment variable MSI_ENDPOINT contains an invalid Uri.", ex);
			}
			return new CloudShellManagedIdentitySource(uri, options);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004B50 File Offset: 0x00002D50
		public CloudShellManagedIdentitySource(Uri endpoint, ManagedIdentityClientOptions options)
			: base(options.Pipeline)
		{
			this._endpoint = endpoint;
			if (!string.IsNullOrEmpty(options.ClientId) || null != options.ResourceIdentifier)
			{
				AzureIdentityEventSource.Singleton.UserAssignedManagedIdentityNotSupported("Cloud Shell");
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004B90 File Offset: 0x00002D90
		protected override Request CreateRequest(string[] scopes)
		{
			string text = ScopeUtilities.ScopesToResource(scopes);
			Request request = base.Pipeline.HttpPipeline.CreateRequest();
			request.Method = RequestMethod.Post;
			request.Headers.Add(HttpHeader.Common.FormUrlEncodedContentType);
			request.Uri.Reset(this._endpoint);
			request.Headers.Add("Metadata", "true");
			string text2 = "resource=" + Uri.EscapeDataString(text);
			ReadOnlyMemory<byte> readOnlyMemory = MemoryExtensions.AsMemory<byte>(Encoding.UTF8.GetBytes(text2));
			request.Content = RequestContent.Create(readOnlyMemory);
			return request;
		}

		// Token: 0x0400009D RID: 157
		private readonly Uri _endpoint;

		// Token: 0x0400009E RID: 158
		private const string MsiEndpointInvalidUriError = "The environment variable MSI_ENDPOINT contains an invalid Uri.";
	}
}
