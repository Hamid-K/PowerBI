using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Batch
{
	// Token: 0x020001CF RID: 463
	public abstract class ODataBatchResponseItem : IDisposable
	{
		// Token: 0x06000F48 RID: 3912 RVA: 0x0003ED76 File Offset: 0x0003CF76
		public static Task WriteMessageAsync(ODataBatchWriter writer, HttpResponseMessage response)
		{
			return ODataBatchResponseItem.WriteMessageAsync(writer, response, CancellationToken.None);
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0003ED84 File Offset: 0x0003CF84
		public static async Task WriteMessageAsync(ODataBatchWriter writer, HttpResponseMessage response, CancellationToken cancellationToken)
		{
			if (writer == null)
			{
				throw Error.ArgumentNull("writer");
			}
			if (response == null)
			{
				throw Error.ArgumentNull("response");
			}
			HttpRequestMessage requestMessage = response.RequestMessage;
			string text = ((requestMessage != null) ? requestMessage.GetODataContentId() : string.Empty);
			ODataBatchOperationResponseMessage odataBatchOperationResponseMessage = writer.CreateOperationResponseMessage(text);
			odataBatchOperationResponseMessage.StatusCode = (int)response.StatusCode;
			foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in response.Headers)
			{
				odataBatchOperationResponseMessage.SetHeader(keyValuePair.Key, string.Join(",", keyValuePair.Value));
			}
			if (response.Content != null)
			{
				foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair2 in response.Content.Headers)
				{
					odataBatchOperationResponseMessage.SetHeader(keyValuePair2.Key, string.Join(",", keyValuePair2.Value));
				}
				using (Stream stream = odataBatchOperationResponseMessage.GetStream())
				{
					cancellationToken.ThrowIfCancellationRequested();
					await response.Content.CopyToAsync(stream);
				}
				Stream stream = null;
			}
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0003EDD9 File Offset: 0x0003CFD9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000F4B RID: 3915
		public abstract Task WriteResponseAsync(ODataBatchWriter writer, CancellationToken cancellationToken);

		// Token: 0x06000F4C RID: 3916 RVA: 0x0003EDE8 File Offset: 0x0003CFE8
		internal Task WriteResponseAsync(ODataBatchWriter writer)
		{
			return this.WriteResponseAsync(writer, CancellationToken.None);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x000102A1 File Offset: 0x0000E4A1
		internal virtual bool IsResponseSuccessful()
		{
			return false;
		}

		// Token: 0x06000F4E RID: 3918
		protected abstract void Dispose(bool disposing);
	}
}
