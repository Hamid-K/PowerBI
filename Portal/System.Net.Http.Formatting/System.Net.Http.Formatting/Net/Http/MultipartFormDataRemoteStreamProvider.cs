using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http.Formatting.Internal;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000005 RID: 5
	public abstract class MultipartFormDataRemoteStreamProvider : MultipartStreamProvider
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000024CD File Offset: 0x000006CD
		protected MultipartFormDataRemoteStreamProvider()
		{
			this.FormData = HttpValueCollection.Create();
			this.FileData = new Collection<MultipartRemoteFileData>();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000024F6 File Offset: 0x000006F6
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000024FE File Offset: 0x000006FE
		public Collection<MultipartRemoteFileData> FileData { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002507 File Offset: 0x00000707
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000250F File Offset: 0x0000070F
		public NameValueCollection FormData { get; private set; }

		// Token: 0x06000012 RID: 18
		public abstract RemoteStreamInfo GetRemoteStream(HttpContent parent, HttpContentHeaders headers);

		// Token: 0x06000013 RID: 19 RVA: 0x00002518 File Offset: 0x00000718
		public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
		{
			if (!MultipartFormDataStreamProviderHelper.IsFileContent(parent, headers))
			{
				return new MemoryStream();
			}
			RemoteStreamInfo remoteStream = this.GetRemoteStream(parent, headers);
			if (remoteStream == null)
			{
				throw Error.InvalidOperation(Resources.RemoteStreamInfoCannotBeNull, new object[]
				{
					"GetRemoteStream",
					base.GetType().Name
				});
			}
			this.FileData.Add(new MultipartRemoteFileData(headers, remoteStream.Location, remoteStream.FileName));
			return remoteStream.RemoteStream;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000258A File Offset: 0x0000078A
		public override Task ExecutePostProcessingAsync()
		{
			return MultipartFormDataStreamProviderHelper.ReadFormDataAsync(base.Contents, this.FormData, this._cancellationToken);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025A3 File Offset: 0x000007A3
		public override Task ExecutePostProcessingAsync(CancellationToken cancellationToken)
		{
			this._cancellationToken = cancellationToken;
			return this.ExecutePostProcessingAsync();
		}

		// Token: 0x0400000A RID: 10
		private CancellationToken _cancellationToken = CancellationToken.None;
	}
}
