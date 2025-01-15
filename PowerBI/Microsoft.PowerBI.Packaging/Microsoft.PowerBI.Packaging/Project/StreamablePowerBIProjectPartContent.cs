using System;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000077 RID: 119
	public sealed class StreamablePowerBIProjectPartContent : IStreamablePowerBIProjectPartContent, IFromPBIProjectFile
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000A18D File Offset: 0x0000838D
		// (set) Token: 0x06000379 RID: 889 RVA: 0x0000A19E File Offset: 0x0000839E
		public string FileName
		{
			get
			{
				return this.fileName ?? string.Empty;
			}
			set
			{
				this.fileName = value;
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000A1A7 File Offset: 0x000083A7
		public StreamablePowerBIProjectPartContent(string content)
			: this((content == null) ? null : PBIProjectConstants.SafeUtf8NoBom.GetBytes(content))
		{
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000A1C0 File Offset: 0x000083C0
		public StreamablePowerBIProjectPartContent(byte[] content)
			: this(delegate
			{
				if (content != null)
				{
					return new MemoryStream(content, false);
				}
				return null;
			})
		{
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000A1EC File Offset: 0x000083EC
		public StreamablePowerBIProjectPartContent(Func<Stream> getPowerBiProjectPartContentReadonlyStream)
		{
			this.getPowerBiProjectPartContentReadonlyStream = getPowerBiProjectPartContentReadonlyStream;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000A1FC File Offset: 0x000083FC
		public Task<Stream> GetStreamAsync()
		{
			Stream stream = this.getPowerBiProjectPartContentReadonlyStream();
			if (stream != null)
			{
				MemoryStream memoryStream = stream as MemoryStream;
			}
			return Task.FromResult<Stream>(stream);
		}

		// Token: 0x040001D3 RID: 467
		private readonly Func<Stream> getPowerBiProjectPartContentReadonlyStream;

		// Token: 0x040001D4 RID: 468
		private string fileName;
	}
}
