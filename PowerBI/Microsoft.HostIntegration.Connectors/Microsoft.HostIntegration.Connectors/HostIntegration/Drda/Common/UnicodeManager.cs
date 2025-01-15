using System;
using System.Text;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200079C RID: 1948
	public class UnicodeManager : ICcsidManager
	{
		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06003ECB RID: 16075 RVA: 0x000D274E File Offset: 0x000D094E
		public static ICcsidManager Instance
		{
			get
			{
				return UnicodeManager._manager;
			}
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x000D2755 File Offset: 0x000D0955
		public byte[] GetBytes(string s)
		{
			return Encoding.UTF8.GetBytes(s);
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x000D2764 File Offset: 0x000D0964
		public int GetBytes(string s, byte[] abyte0, int i)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			Buffer.BlockCopy(bytes, 0, abyte0, i, bytes.Length);
			return i + bytes.Length;
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x000D278E File Offset: 0x000D098E
		public string GetString(byte[] abyte0)
		{
			return Encoding.UTF8.GetString(abyte0);
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x000D279C File Offset: 0x000D099C
		public string GetString(byte[] abyte0, int i, int j)
		{
			byte[] array = new byte[j];
			Buffer.BlockCopy(abyte0, i, array, 0, j);
			return Encoding.UTF8.GetString(array);
		}

		// Token: 0x0400254E RID: 9550
		private static ICcsidManager _manager = new UnicodeManager();
	}
}
