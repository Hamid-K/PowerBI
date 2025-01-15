using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x0200000E RID: 14
	public abstract class MultipartStreamProvider
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000029CB File Offset: 0x00000BCB
		public Collection<HttpContent> Contents
		{
			get
			{
				return this._contents;
			}
		}

		// Token: 0x06000040 RID: 64
		public abstract Stream GetStream(HttpContent parent, HttpContentHeaders headers);

		// Token: 0x06000041 RID: 65 RVA: 0x000029D3 File Offset: 0x00000BD3
		public virtual Task ExecutePostProcessingAsync()
		{
			return TaskHelpers.Completed();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000029DA File Offset: 0x00000BDA
		public virtual Task ExecutePostProcessingAsync(CancellationToken cancellationToken)
		{
			return this.ExecutePostProcessingAsync();
		}

		// Token: 0x0400001C RID: 28
		private Collection<HttpContent> _contents = new Collection<HttpContent>();
	}
}
