using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Internal
{
	// Token: 0x020001BD RID: 445
	internal class ParallelReadAheadStream : ForwardReadOnlyStream
	{
		// Token: 0x06000892 RID: 2194 RVA: 0x00010D5F File Offset: 0x0000EF5F
		public static Stream NewNonBuffering(IEngineHost engineHost, IResource resource, int readers, Func<int, Stream> getBufferedPage)
		{
			return new ParallelReadAheadStream(new ParallelEnumerator<Stream>(engineHost, resource, readers, getBufferedPage, delegate(Stream page)
			{
				page.Dispose();
			}));
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00010D8E File Offset: 0x0000EF8E
		private ParallelReadAheadStream(IEnumerator<Stream> streams)
		{
			this.oneByteBuffer = new byte[1];
			this.streams = streams;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00010DA9 File Offset: 0x0000EFA9
		public override int ReadByte()
		{
			if (this.Read(this.oneByteBuffer, 0, 1) > 0)
			{
				return (int)this.oneByteBuffer[0];
			}
			return -1;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00010DC8 File Offset: 0x0000EFC8
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			while (count > 0)
			{
				if (this.currentStream == null)
				{
					if (!this.streams.MoveNext())
					{
						break;
					}
					this.currentStream = this.streams.Current;
				}
				int num2 = this.currentStream.Read(buffer, offset, count);
				offset += num2;
				count -= num2;
				num += num2;
				if (num2 == 0)
				{
					this.currentStream.Dispose();
					this.currentStream = null;
				}
			}
			return num;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00010E36 File Offset: 0x0000F036
		public override void Close()
		{
			if (this.currentStream != null)
			{
				this.currentStream.Dispose();
				this.currentStream = null;
			}
			this.streams.Dispose();
		}

		// Token: 0x040004F6 RID: 1270
		private readonly byte[] oneByteBuffer;

		// Token: 0x040004F7 RID: 1271
		private IEnumerator<Stream> streams;

		// Token: 0x040004F8 RID: 1272
		private Stream currentStream;
	}
}
