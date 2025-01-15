using System;
using System.Text;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200079B RID: 1947
	public class EbcdicManager : ICcsidManager
	{
		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06003EC4 RID: 16068 RVA: 0x000D26B4 File Offset: 0x000D08B4
		public static ICcsidManager Instance
		{
			get
			{
				return EbcdicManager._manager;
			}
		}

		// Token: 0x06003EC6 RID: 16070 RVA: 0x000D26BB File Offset: 0x000D08BB
		public byte[] GetBytes(string s)
		{
			return Encoding.GetEncoding(500).GetBytes(s);
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x000D26D0 File Offset: 0x000D08D0
		public int GetBytes(string s, byte[] abyte0, int i)
		{
			byte[] bytes = Encoding.GetEncoding(500).GetBytes(s);
			Buffer.BlockCopy(bytes, 0, abyte0, i, bytes.Length);
			return i + bytes.Length;
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x000D26FF File Offset: 0x000D08FF
		public string GetString(byte[] abyte0)
		{
			return Encoding.GetEncoding(500).GetString(abyte0);
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x000D2714 File Offset: 0x000D0914
		public string GetString(byte[] abyte0, int i, int j)
		{
			byte[] array = new byte[j];
			Buffer.BlockCopy(abyte0, i, array, 0, j);
			return Encoding.GetEncoding(500).GetString(array);
		}

		// Token: 0x0400254D RID: 9549
		private static ICcsidManager _manager = new EbcdicManager();
	}
}
