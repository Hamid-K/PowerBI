using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Http;

namespace Microsoft.Internal
{
	// Token: 0x020001C8 RID: 456
	internal class RetryStream : ForwardReadOnlyStream
	{
		// Token: 0x060008D0 RID: 2256 RVA: 0x000116E0 File Offset: 0x0000F8E0
		public RetryStream(IEngineHost host, IResource resource, RetryPolicy retryPolicy, long baseOffset, Func<long, Stream> getStreamAtOffset)
		{
			this.host = host;
			this.resource = resource;
			this.retryPolicy = retryPolicy;
			this.baseOffset = baseOffset;
			this.getStreamAtOffset = getStreamAtOffset;
			this.oneByteBuffer = new byte[1];
			this.tracer = new Tracer(this.host, "RetryStream/", this.resource, null, null);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00011742 File Offset: 0x0000F942
		public override int ReadByte()
		{
			if (this.Read(this.oneByteBuffer, 0, 1) > 0)
			{
				return (int)this.oneByteBuffer[0];
			}
			return -1;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00011760 File Offset: 0x0000F960
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.retryPolicy.Execute<int>(this.host, this.resource, delegate
			{
				if (this.stream == null)
				{
					this.stream = this.getStreamAtOffset(this.offset);
				}
				int num2;
				try
				{
					int num = this.stream.Read(buffer, offset, count);
					this.offset += (long)num;
					num2 = num;
				}
				catch (Exception ex)
				{
					using (IHostTrace hostTrace = this.tracer.CreateTrace("Read", TraceEventType.Information))
					{
						hostTrace.Add("Position", this.baseOffset + this.offset, false);
						if (!SafeExceptions.TraceIsSafeException(hostTrace, ex))
						{
							throw;
						}
						this.stream.Dispose();
						this.stream = null;
						throw;
					}
				}
				return num2;
			}, this.tracer);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x000117B8 File Offset: 0x0000F9B8
		public override void Close()
		{
			if (this.stream != null)
			{
				this.stream.Dispose();
			}
		}

		// Token: 0x04000514 RID: 1300
		private readonly IEngineHost host;

		// Token: 0x04000515 RID: 1301
		private readonly IResource resource;

		// Token: 0x04000516 RID: 1302
		private readonly RetryPolicy retryPolicy;

		// Token: 0x04000517 RID: 1303
		private readonly Func<long, Stream> getStreamAtOffset;

		// Token: 0x04000518 RID: 1304
		private readonly byte[] oneByteBuffer;

		// Token: 0x04000519 RID: 1305
		private readonly Tracer tracer;

		// Token: 0x0400051A RID: 1306
		private readonly long baseOffset;

		// Token: 0x0400051B RID: 1307
		private long offset;

		// Token: 0x0400051C RID: 1308
		private Stream stream;
	}
}
