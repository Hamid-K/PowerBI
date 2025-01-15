using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000173 RID: 371
	internal class SocketProtocolMagicBytesManager
	{
		// Token: 0x06000BB2 RID: 2994 RVA: 0x000276F0 File Offset: 0x000258F0
		protected SocketProtocolMagicBytesManager()
		{
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00027703 File Offset: 0x00025903
		protected void AddProtocol<T>(byte magicByte) where T : IServerSocketProtocol, new()
		{
			this._protocols.Add(magicByte, new Func<IServerSocketProtocol>(SocketProtocolMagicBytesManager.CreateObject<T>));
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00027720 File Offset: 0x00025920
		protected void AddProtocol<T>(IEnumerable<byte> possibleBytes) where T : IServerSocketProtocol, new()
		{
			foreach (byte b in possibleBytes)
			{
				this.AddProtocol<T>(b);
			}
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00027768 File Offset: 0x00025968
		private static IServerSocketProtocol CreateObject<T>() where T : IServerSocketProtocol, new()
		{
			return (default(T) == null) ? new T() : default(T);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002779C File Offset: 0x0002599C
		protected IServerSocketProtocol GetProtocol(byte firstByte)
		{
			Func<IServerSocketProtocol> func;
			if (this._protocols.TryGetValue(firstByte, out func))
			{
				return func();
			}
			return null;
		}

		// Token: 0x0400086E RID: 2158
		private IDictionary<byte, Func<IServerSocketProtocol>> _protocols = new Dictionary<byte, Func<IServerSocketProtocol>>();
	}
}
