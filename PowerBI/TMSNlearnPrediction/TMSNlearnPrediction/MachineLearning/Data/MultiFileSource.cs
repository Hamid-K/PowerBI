using System;
using System.IO;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000133 RID: 307
	public sealed class MultiFileSource : IMultiStreamSource
	{
		// Token: 0x06000638 RID: 1592 RVA: 0x0002192C File Offset: 0x0001FB2C
		public MultiFileSource(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				this._paths = new string[0];
				return;
			}
			this._paths = StreamUtils.ExpandWildCards(path);
			if (this._paths.Length == 0)
			{
				throw Contracts.ExceptIO("Could not find file '{0}'", new object[] { path });
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00021981 File Offset: 0x0001FB81
		public int Count
		{
			get
			{
				return this._paths.Length;
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0002198B File Offset: 0x0001FB8B
		public string GetPathOrNull(int index)
		{
			Contracts.CheckParam(0 <= index && index < this.Count, "index");
			return this._paths[index];
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x000219B0 File Offset: 0x0001FBB0
		public Stream Open(int index)
		{
			Contracts.CheckParam(0 <= index && index < this.Count, "index");
			string text = this._paths[index];
			Stream stream;
			try
			{
				stream = StreamUtils.OpenInStream(text);
			}
			catch (Exception ex)
			{
				throw Contracts.ExceptIO(ex, "Could not open file '{0}'. Error is: {1}", new object[] { text, ex.Message });
			}
			return stream;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00021A1C File Offset: 0x0001FC1C
		public TextReader OpenTextReader(int index)
		{
			return new StreamReader(this.Open(index));
		}

		// Token: 0x0400032D RID: 813
		private readonly string[] _paths;
	}
}
