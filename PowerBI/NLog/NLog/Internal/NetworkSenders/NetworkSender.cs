using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using NLog.Common;

namespace NLog.Internal.NetworkSenders
{
	// Token: 0x02000157 RID: 343
	internal abstract class NetworkSender : IDisposable
	{
		// Token: 0x06001026 RID: 4134 RVA: 0x00029C1A File Offset: 0x00027E1A
		protected NetworkSender(string url)
		{
			this.Address = url;
			this.LastSendTime = Interlocked.Increment(ref NetworkSender.currentSendTime);
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x00029C39 File Offset: 0x00027E39
		// (set) Token: 0x06001028 RID: 4136 RVA: 0x00029C41 File Offset: 0x00027E41
		public string Address { get; private set; }

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x00029C4A File Offset: 0x00027E4A
		// (set) Token: 0x0600102A RID: 4138 RVA: 0x00029C52 File Offset: 0x00027E52
		public int LastSendTime { get; private set; }

		// Token: 0x0600102B RID: 4139 RVA: 0x00029C5B File Offset: 0x00027E5B
		public void Initialize()
		{
			this.DoInitialize();
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00029C63 File Offset: 0x00027E63
		public void Close(AsyncContinuation continuation)
		{
			this.DoClose(continuation);
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x00029C6C File Offset: 0x00027E6C
		public void FlushAsync(AsyncContinuation continuation)
		{
			this.DoFlush(continuation);
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00029C75 File Offset: 0x00027E75
		public void Send(byte[] bytes, int offset, int length, AsyncContinuation asyncContinuation)
		{
			this.LastSendTime = Interlocked.Increment(ref NetworkSender.currentSendTime);
			this.DoSend(bytes, offset, length, asyncContinuation);
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00029C92 File Offset: 0x00027E92
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00029CA1 File Offset: 0x00027EA1
		protected virtual void DoInitialize()
		{
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00029CA3 File Offset: 0x00027EA3
		protected virtual void DoClose(AsyncContinuation continuation)
		{
			continuation(null);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00029CAC File Offset: 0x00027EAC
		protected virtual void DoFlush(AsyncContinuation continuation)
		{
			continuation(null);
		}

		// Token: 0x06001033 RID: 4147
		protected abstract void DoSend(byte[] bytes, int offset, int length, AsyncContinuation asyncContinuation);

		// Token: 0x06001034 RID: 4148 RVA: 0x00029CB8 File Offset: 0x00027EB8
		protected virtual EndPoint ParseEndpointAddress(Uri uri, AddressFamily addressFamily)
		{
			UriHostNameType hostNameType = uri.HostNameType;
			if (hostNameType - UriHostNameType.IPv4 <= 1)
			{
				return new IPEndPoint(IPAddress.Parse(uri.Host), uri.Port);
			}
			foreach (IPAddress ipaddress in Dns.GetHostEntry(uri.Host).AddressList)
			{
				if (ipaddress.AddressFamily == addressFamily || addressFamily == AddressFamily.Unspecified)
				{
					return new IPEndPoint(ipaddress, uri.Port);
				}
			}
			throw new IOException(string.Format("Cannot resolve '{0}' to an address in '{1}'", uri.Host, addressFamily));
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x00029D40 File Offset: 0x00027F40
		public virtual void CheckSocket()
		{
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00029D42 File Offset: 0x00027F42
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close(delegate(Exception ex)
				{
				});
			}
		}

		// Token: 0x04000456 RID: 1110
		private static int currentSendTime;
	}
}
