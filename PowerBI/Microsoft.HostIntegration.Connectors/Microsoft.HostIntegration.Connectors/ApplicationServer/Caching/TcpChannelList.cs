using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002D4 RID: 724
	internal sealed class TcpChannelList
	{
		// Token: 0x06001ADE RID: 6878 RVA: 0x000517C0 File Offset: 0x0004F9C0
		internal TcpChannelList(TcpClientChannel[] channels)
		{
			this._channels = channels;
			this._safeCounter = new OptimizedSafeCounter(channels.Length);
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x000517DD File Offset: 0x0004F9DD
		internal TcpClientChannel GetChannel()
		{
			return this._channels[this._safeCounter.Next()];
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x000517F4 File Offset: 0x0004F9F4
		public void Close()
		{
			for (int i = 0; i < this._channels.Length; i++)
			{
				this._channels[i].Dispose();
			}
		}

		// Token: 0x04000E3D RID: 3645
		internal OptimizedSafeCounter _safeCounter;

		// Token: 0x04000E3E RID: 3646
		private TcpClientChannel[] _channels;
	}
}
