using System;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http.Formatting.Internal;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x02000021 RID: 33
	public class MultipartFormDataStreamProvider : MultipartFileStreamProvider
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00004866 File Offset: 0x00002A66
		public MultipartFormDataStreamProvider(string rootPath)
			: base(rootPath)
		{
			this.FormData = HttpValueCollection.Create();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000487A File Offset: 0x00002A7A
		public MultipartFormDataStreamProvider(string rootPath, int bufferSize)
			: base(rootPath, bufferSize)
		{
			this.FormData = HttpValueCollection.Create();
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000488F File Offset: 0x00002A8F
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00004897 File Offset: 0x00002A97
		public NameValueCollection FormData { get; private set; }

		// Token: 0x060000F8 RID: 248 RVA: 0x000048A0 File Offset: 0x00002AA0
		public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
		{
			if (MultipartFormDataStreamProviderHelper.IsFileContent(parent, headers))
			{
				return base.GetStream(parent, headers);
			}
			return new MemoryStream();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000048B9 File Offset: 0x00002AB9
		public override Task ExecutePostProcessingAsync()
		{
			return MultipartFormDataStreamProviderHelper.ReadFormDataAsync(base.Contents, this.FormData, this._cancellationToken);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000048D2 File Offset: 0x00002AD2
		public override Task ExecutePostProcessingAsync(CancellationToken cancellationToken)
		{
			this._cancellationToken = cancellationToken;
			return this.ExecutePostProcessingAsync();
		}

		// Token: 0x0400005E RID: 94
		private CancellationToken _cancellationToken;
	}
}
