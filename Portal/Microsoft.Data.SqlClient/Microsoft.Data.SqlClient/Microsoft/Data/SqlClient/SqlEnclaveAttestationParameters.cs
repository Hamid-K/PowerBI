using System;
using System.Security.Cryptography;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000077 RID: 119
	internal class SqlEnclaveAttestationParameters
	{
		// Token: 0x06000A9E RID: 2718 RVA: 0x0001DD80 File Offset: 0x0001BF80
		internal SqlEnclaveAttestationParameters(int protocol, byte[] input, ECDiffieHellman clientDiffieHellmanKey)
		{
			if (input == null)
			{
				throw SQL.NullArgumentInConstructorInternal("input", "SqlEnclaveAttestationParameters");
			}
			if (clientDiffieHellmanKey == null)
			{
				throw SQL.NullArgumentInConstructorInternal("clientDiffieHellmanKey", "SqlEnclaveAttestationParameters");
			}
			this._input = input;
			this.Protocol = protocol;
			this.ClientDiffieHellmanKey = clientDiffieHellmanKey;
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0001DDCE File Offset: 0x0001BFCE
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x0001DDD6 File Offset: 0x0001BFD6
		internal int Protocol { get; private set; }

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0001DDDF File Offset: 0x0001BFDF
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x0001DDE7 File Offset: 0x0001BFE7
		internal ECDiffieHellman ClientDiffieHellmanKey { get; private set; }

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0001DDF0 File Offset: 0x0001BFF0
		internal byte[] GetInput()
		{
			if (this._input == null)
			{
				return null;
			}
			byte[] array = new byte[this._input.Length];
			Buffer.BlockCopy(this._input, 0, array, 0, this._input.Length);
			return array;
		}

		// Token: 0x0400023D RID: 573
		private readonly byte[] _input;
	}
}
