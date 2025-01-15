using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000020 RID: 32
	public class MultipartFileStreamProvider : MultipartStreamProvider
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00004719 File Offset: 0x00002919
		public MultipartFileStreamProvider(string rootPath)
			: this(rootPath, 4096)
		{
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004728 File Offset: 0x00002928
		public MultipartFileStreamProvider(string rootPath, int bufferSize)
		{
			if (rootPath == null)
			{
				throw Error.ArgumentNull("rootPath");
			}
			if (bufferSize < 1)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("bufferSize", bufferSize, 1);
			}
			this._rootPath = Path.GetFullPath(rootPath);
			this._bufferSize = bufferSize;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000478D File Offset: 0x0000298D
		public Collection<MultipartFileData> FileData
		{
			get
			{
				return this._fileData;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004795 File Offset: 0x00002995
		protected string RootPath
		{
			get
			{
				return this._rootPath;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000479D File Offset: 0x0000299D
		protected int BufferSize
		{
			get
			{
				return this._bufferSize;
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000047A8 File Offset: 0x000029A8
		public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
		{
			if (parent == null)
			{
				throw Error.ArgumentNull("parent");
			}
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			string text;
			try
			{
				string localFileName = this.GetLocalFileName(headers);
				text = Path.Combine(this._rootPath, Path.GetFileName(localFileName));
			}
			catch (Exception ex)
			{
				throw Error.InvalidOperation(ex, Resources.MultipartStreamProviderInvalidLocalFileName, new object[0]);
			}
			MultipartFileData multipartFileData = new MultipartFileData(headers, text);
			this._fileData.Add(multipartFileData);
			return File.Create(text, this._bufferSize, FileOptions.Asynchronous);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004834 File Offset: 0x00002A34
		public virtual string GetLocalFileName(HttpContentHeaders headers)
		{
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			return string.Format(CultureInfo.InvariantCulture, "BodyPart_{0}", new object[] { Guid.NewGuid() });
		}

		// Token: 0x04000059 RID: 89
		private const int MinBufferSize = 1;

		// Token: 0x0400005A RID: 90
		private const int DefaultBufferSize = 4096;

		// Token: 0x0400005B RID: 91
		private string _rootPath;

		// Token: 0x0400005C RID: 92
		private int _bufferSize = 4096;

		// Token: 0x0400005D RID: 93
		private Collection<MultipartFileData> _fileData = new Collection<MultipartFileData>();
	}
}
