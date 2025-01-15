using System;
using System.IO;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000844 RID: 2116
	internal sealed class IndexTablePage
	{
		// Token: 0x0600763E RID: 30270 RVA: 0x001EA14F File Offset: 0x001E834F
		public IndexTablePage(int size)
		{
			this.Buffer = new byte[size];
			this.Dirty = false;
			this.PreviousPage = null;
			this.NextPage = null;
		}

		// Token: 0x0600763F RID: 30271 RVA: 0x001EA178 File Offset: 0x001E8378
		public void Read(Stream stream)
		{
			int num = stream.Read(this.Buffer, 0, this.Buffer.Length);
			if (num == 0)
			{
				for (int i = 0; i < this.Buffer.Length; i++)
				{
					this.Buffer[i] = 0;
				}
			}
			else if (num < this.Buffer.Length)
			{
				Global.Tracer.Assert(false);
			}
			this.Dirty = false;
		}

		// Token: 0x06007640 RID: 30272 RVA: 0x001EA1D9 File Offset: 0x001E83D9
		public void Write(Stream stream)
		{
			stream.Write(this.Buffer, 0, this.Buffer.Length);
			this.Dirty = false;
		}

		// Token: 0x04003BC5 RID: 15301
		internal byte[] Buffer;

		// Token: 0x04003BC6 RID: 15302
		internal bool Dirty;

		// Token: 0x04003BC7 RID: 15303
		internal int PageNumber;

		// Token: 0x04003BC8 RID: 15304
		internal IndexTablePage PreviousPage;

		// Token: 0x04003BC9 RID: 15305
		internal IndexTablePage NextPage;
	}
}
