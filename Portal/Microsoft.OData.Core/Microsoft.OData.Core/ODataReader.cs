using System;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x020000B3 RID: 179
	public abstract class ODataReader
	{
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060007C4 RID: 1988
		public abstract ODataReaderState State { get; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060007C5 RID: 1989
		public abstract ODataItem Item { get; }

		// Token: 0x060007C6 RID: 1990
		public abstract bool Read();

		// Token: 0x060007C7 RID: 1991 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual Stream CreateReadStream()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual TextReader CreateTextReader()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060007C9 RID: 1993
		public abstract Task<bool> ReadAsync();

		// Token: 0x060007CA RID: 1994 RVA: 0x00012BF1 File Offset: 0x00010DF1
		public virtual Task<Stream> CreateReadStreamAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<Stream>(() => this.CreateReadStream());
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00012C04 File Offset: 0x00010E04
		public virtual Task<TextReader> CreateTextReaderAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<TextReader>(() => this.CreateTextReader());
		}
	}
}
