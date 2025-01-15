using System;
using System.IO;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002A2 RID: 674
	internal abstract class BufferedWriteStreamBase : BufferedReadWriteStream, ReportProcessing.IErasable
	{
		// Token: 0x060018A2 RID: 6306 RVA: 0x000634E3 File Offset: 0x000616E3
		public BufferedWriteStreamBase(Stream store, int bufferSize)
			: base(store, bufferSize)
		{
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x00005BEF File Offset: 0x00003DEF
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}
	}
}
