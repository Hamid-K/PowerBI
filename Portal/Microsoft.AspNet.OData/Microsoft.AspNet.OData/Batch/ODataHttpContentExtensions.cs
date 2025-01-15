using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Batch
{
	// Token: 0x020001D6 RID: 470
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ODataHttpContentExtensions
	{
		// Token: 0x06000F77 RID: 3959 RVA: 0x0003F3F0 File Offset: 0x0003D5F0
		public static Task<ODataMessageReader> GetODataMessageReaderAsync(this HttpContent content, IServiceProvider requestContainer)
		{
			return content.GetODataMessageReaderAsync(requestContainer, CancellationToken.None);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0003F400 File Offset: 0x0003D600
		public static async Task<ODataMessageReader> GetODataMessageReaderAsync(this HttpContent content, IServiceProvider requestContainer, CancellationToken cancellationToken)
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			cancellationToken.ThrowIfCancellationRequested();
			IODataRequestMessage iodataRequestMessage = ODataMessageWrapperHelper.Create(await content.ReadAsStreamAsync(), content.Headers, requestContainer);
			ODataMessageReaderSettings requiredService = ServiceProviderServiceExtensions.GetRequiredService<ODataMessageReaderSettings>(requestContainer);
			return new ODataMessageReader(iodataRequestMessage, requiredService);
		}
	}
}
