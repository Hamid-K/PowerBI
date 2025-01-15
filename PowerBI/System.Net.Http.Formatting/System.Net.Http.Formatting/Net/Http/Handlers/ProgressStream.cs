using System;
using System.IO;
using System.Net.Http.Internal;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.Handlers
{
	// Token: 0x0200002B RID: 43
	internal class ProgressStream : DelegatingStream
	{
		// Token: 0x0600019A RID: 410 RVA: 0x00005CC8 File Offset: 0x00003EC8
		public ProgressStream(Stream innerStream, ProgressMessageHandler handler, HttpRequestMessage request, HttpResponseMessage response)
			: base(innerStream)
		{
			if (request.Content != null)
			{
				this._totalBytesToSend = request.Content.Headers.ContentLength;
			}
			if (response != null && response.Content != null)
			{
				this._totalBytesToReceive = response.Content.Headers.ContentLength;
			}
			this._handler = handler;
			this._request = request;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005D2C File Offset: 0x00003F2C
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = base.InnerStream.Read(buffer, offset, count);
			this.ReportBytesReceived(num, null);
			return num;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005D54 File Offset: 0x00003F54
		public override int ReadByte()
		{
			int num = base.InnerStream.ReadByte();
			this.ReportBytesReceived((num == -1) ? 0 : 1, null);
			return num;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00005D80 File Offset: 0x00003F80
		public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			int num = await base.InnerStream.ReadAsync(buffer, offset, count, cancellationToken);
			this.ReportBytesReceived(num, null);
			return num;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00005DE6 File Offset: 0x00003FE6
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return base.InnerStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00005DFC File Offset: 0x00003FFC
		public override int EndRead(IAsyncResult asyncResult)
		{
			int num = base.InnerStream.EndRead(asyncResult);
			this.ReportBytesReceived(num, asyncResult.AsyncState);
			return num;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00005E24 File Offset: 0x00004024
		public override void Write(byte[] buffer, int offset, int count)
		{
			base.InnerStream.Write(buffer, offset, count);
			this.ReportBytesSent(count, null);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00005E3C File Offset: 0x0000403C
		public override void WriteByte(byte value)
		{
			base.InnerStream.WriteByte(value);
			this.ReportBytesSent(1, null);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00005E54 File Offset: 0x00004054
		public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			await base.InnerStream.WriteAsync(buffer, offset, count, cancellationToken);
			this.ReportBytesSent(count, null);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00005EBA File Offset: 0x000040BA
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return new ProgressWriteAsyncResult(base.InnerStream, this, buffer, offset, count, callback, state);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00005ECF File Offset: 0x000040CF
		public override void EndWrite(IAsyncResult asyncResult)
		{
			ProgressWriteAsyncResult.End(asyncResult);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00005ED8 File Offset: 0x000040D8
		internal void ReportBytesSent(int bytesSent, object userState)
		{
			if (bytesSent > 0)
			{
				this._bytesSent += (long)bytesSent;
				int num = 0;
				if (this._totalBytesToSend != null)
				{
					long? totalBytesToSend = this._totalBytesToSend;
					long num2 = 0L;
					if (!((totalBytesToSend.GetValueOrDefault() == num2) & (totalBytesToSend != null)))
					{
						num = (int)(100L * this._bytesSent / this._totalBytesToSend).Value;
					}
				}
				this._handler.OnHttpRequestProgress(this._request, new HttpProgressEventArgs(num, userState, this._bytesSent, this._totalBytesToSend));
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00005F8C File Offset: 0x0000418C
		private void ReportBytesReceived(int bytesReceived, object userState)
		{
			if (bytesReceived > 0)
			{
				this._bytesReceived += (long)bytesReceived;
				int num = 0;
				if (this._totalBytesToReceive != null)
				{
					long? totalBytesToReceive = this._totalBytesToReceive;
					long num2 = 0L;
					if (!((totalBytesToReceive.GetValueOrDefault() == num2) & (totalBytesToReceive != null)))
					{
						num = (int)(100L * this._bytesReceived / this._totalBytesToReceive).Value;
					}
				}
				this._handler.OnHttpResponseProgress(this._request, new HttpProgressEventArgs(num, userState, this._bytesReceived, this._totalBytesToReceive));
			}
		}

		// Token: 0x0400007E RID: 126
		private readonly ProgressMessageHandler _handler;

		// Token: 0x0400007F RID: 127
		private readonly HttpRequestMessage _request;

		// Token: 0x04000080 RID: 128
		private long _bytesReceived;

		// Token: 0x04000081 RID: 129
		private long? _totalBytesToReceive;

		// Token: 0x04000082 RID: 130
		private long _bytesSent;

		// Token: 0x04000083 RID: 131
		private long? _totalBytesToSend;
	}
}
