using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Batch
{
	// Token: 0x020001D2 RID: 466
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ODataBatchReaderExtensions
	{
		// Token: 0x06000F5D RID: 3933 RVA: 0x0003EEF8 File Offset: 0x0003D0F8
		public static Task<IList<HttpRequestMessage>> ReadChangeSetRequestAsync(this ODataBatchReader reader, Guid batchId)
		{
			return reader.ReadChangeSetRequestAsync(batchId, CancellationToken.None);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0003EF08 File Offset: 0x0003D108
		public static async Task<IList<HttpRequestMessage>> ReadChangeSetRequestAsync(this ODataBatchReader reader, Guid batchId, CancellationToken cancellationToken)
		{
			if (reader == null)
			{
				throw Error.ArgumentNull("reader");
			}
			if (reader.State != ODataBatchReaderState.ChangesetStart)
			{
				throw Error.InvalidOperation(SRResources.InvalidBatchReaderState, new object[]
				{
					reader.State.ToString(),
					ODataBatchReaderState.ChangesetStart.ToString()
				});
			}
			Guid changeSetId = Guid.NewGuid();
			List<HttpRequestMessage> requests = new List<HttpRequestMessage>();
			while (reader.Read() && reader.State != ODataBatchReaderState.ChangesetEnd)
			{
				if (reader.State == ODataBatchReaderState.Operation)
				{
					List<HttpRequestMessage> list = requests;
					HttpRequestMessage httpRequestMessage = await ODataBatchReaderExtensions.ReadOperationInternalAsync(reader, batchId, new Guid?(changeSetId), cancellationToken, true);
					list.Add(httpRequestMessage);
					list = null;
				}
			}
			return requests;
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0003EF5D File Offset: 0x0003D15D
		public static Task<HttpRequestMessage> ReadOperationRequestAsync(this ODataBatchReader reader, Guid batchId, bool bufferContentStream)
		{
			return reader.ReadOperationRequestAsync(batchId, bufferContentStream, CancellationToken.None);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0003EF6C File Offset: 0x0003D16C
		public static Task<HttpRequestMessage> ReadOperationRequestAsync(this ODataBatchReader reader, Guid batchId, bool bufferContentStream, CancellationToken cancellationToken)
		{
			if (reader == null)
			{
				throw Error.ArgumentNull("reader");
			}
			if (reader.State != ODataBatchReaderState.Operation)
			{
				throw Error.InvalidOperation(SRResources.InvalidBatchReaderState, new object[]
				{
					reader.State.ToString(),
					ODataBatchReaderState.Operation.ToString()
				});
			}
			return ODataBatchReaderExtensions.ReadOperationInternalAsync(reader, batchId, null, cancellationToken, bufferContentStream);
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0003EFDC File Offset: 0x0003D1DC
		public static Task<HttpRequestMessage> ReadChangeSetOperationRequestAsync(this ODataBatchReader reader, Guid batchId, Guid changeSetId, bool bufferContentStream)
		{
			return reader.ReadChangeSetOperationRequestAsync(batchId, changeSetId, bufferContentStream, CancellationToken.None);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0003EFEC File Offset: 0x0003D1EC
		public static Task<HttpRequestMessage> ReadChangeSetOperationRequestAsync(this ODataBatchReader reader, Guid batchId, Guid changeSetId, bool bufferContentStream, CancellationToken cancellationToken)
		{
			if (reader == null)
			{
				throw Error.ArgumentNull("reader");
			}
			if (reader.State != ODataBatchReaderState.Operation)
			{
				throw Error.InvalidOperation(SRResources.InvalidBatchReaderState, new object[]
				{
					reader.State.ToString(),
					ODataBatchReaderState.Operation.ToString()
				});
			}
			return ODataBatchReaderExtensions.ReadOperationInternalAsync(reader, batchId, new Guid?(changeSetId), cancellationToken, bufferContentStream);
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0003F05C File Offset: 0x0003D25C
		private static async Task<HttpRequestMessage> ReadOperationInternalAsync(ODataBatchReader reader, Guid batchId, Guid? changeSetId, CancellationToken cancellationToken, bool bufferContentStream = true)
		{
			ODataBatchOperationRequestMessage batchRequest = reader.CreateOperationRequestMessage();
			HttpRequestMessage request = new HttpRequestMessage();
			request.Method = new HttpMethod(batchRequest.Method);
			request.RequestUri = batchRequest.Url;
			if (bufferContentStream)
			{
				using (Stream stream = batchRequest.GetStream())
				{
					MemoryStream bufferedStream = new MemoryStream();
					await stream.CopyToAsync(bufferedStream, 81920, cancellationToken);
					bufferedStream.Position = 0L;
					request.Content = new StreamContent(bufferedStream);
					bufferedStream = null;
				}
				Stream stream = null;
			}
			else
			{
				request.Content = new LazyStreamContent(() => batchRequest.GetStream());
			}
			foreach (KeyValuePair<string, string> keyValuePair in batchRequest.Headers)
			{
				string key = keyValuePair.Key;
				string value = keyValuePair.Value;
				if (!request.Headers.TryAddWithoutValidation(key, value))
				{
					request.Content.Headers.TryAddWithoutValidation(key, value);
				}
			}
			request.SetODataBatchId(batchId);
			request.SetODataContentId(batchRequest.ContentId);
			if (changeSetId != null && changeSetId != null)
			{
				request.SetODataChangeSetId(changeSetId.Value);
			}
			return request;
		}
	}
}
