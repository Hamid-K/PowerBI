using System;
using System.Text;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200004B RID: 75
	internal class SqlAeadAes256CbcHmac256EncryptionKey : SqlClientSymmetricKey
	{
		// Token: 0x060007A8 RID: 1960 RVA: 0x00010DA0 File Offset: 0x0000EFA0
		internal SqlAeadAes256CbcHmac256EncryptionKey(byte[] rootKey, string algorithmName)
			: base(rootKey)
		{
			this._algorithmName = algorithmName;
			int num = 32;
			if (rootKey.Length != num)
			{
				throw SQL.InvalidKeySize(this._algorithmName, rootKey.Length, num);
			}
			string text = string.Format("Microsoft SQL Server cell encryption key with encryption algorithm:{0} and key length:{1}", this._algorithmName, 256);
			byte[] array = new byte[num];
			SqlSecurityUtility.GetHMACWithSHA256(Encoding.Unicode.GetBytes(text), this.RootKey, array);
			this._encryptionKey = new SqlClientSymmetricKey(array);
			string text2 = string.Format("Microsoft SQL Server cell MAC key with encryption algorithm:{0} and key length:{1}", this._algorithmName, 256);
			byte[] array2 = new byte[num];
			SqlSecurityUtility.GetHMACWithSHA256(Encoding.Unicode.GetBytes(text2), this.RootKey, array2);
			this._macKey = new SqlClientSymmetricKey(array2);
			string text3 = string.Format("Microsoft SQL Server cell IV key with encryption algorithm:{0} and key length:{1}", this._algorithmName, 256);
			byte[] array3 = new byte[num];
			SqlSecurityUtility.GetHMACWithSHA256(Encoding.Unicode.GetBytes(text3), this.RootKey, array3);
			this._ivKey = new SqlClientSymmetricKey(array3);
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x00010EAB File Offset: 0x0000F0AB
		internal byte[] EncryptionKey
		{
			get
			{
				return this._encryptionKey.RootKey;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x00010EB8 File Offset: 0x0000F0B8
		internal byte[] MACKey
		{
			get
			{
				return this._macKey.RootKey;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00010EC5 File Offset: 0x0000F0C5
		internal byte[] IVKey
		{
			get
			{
				return this._ivKey.RootKey;
			}
		}

		// Token: 0x04000105 RID: 261
		internal const int KeySize = 256;

		// Token: 0x04000106 RID: 262
		private const string _encryptionKeySaltFormat = "Microsoft SQL Server cell encryption key with encryption algorithm:{0} and key length:{1}";

		// Token: 0x04000107 RID: 263
		private const string _macKeySaltFormat = "Microsoft SQL Server cell MAC key with encryption algorithm:{0} and key length:{1}";

		// Token: 0x04000108 RID: 264
		private const string _ivKeySaltFormat = "Microsoft SQL Server cell IV key with encryption algorithm:{0} and key length:{1}";

		// Token: 0x04000109 RID: 265
		private readonly SqlClientSymmetricKey _encryptionKey;

		// Token: 0x0400010A RID: 266
		private readonly SqlClientSymmetricKey _macKey;

		// Token: 0x0400010B RID: 267
		private readonly SqlClientSymmetricKey _ivKey;

		// Token: 0x0400010C RID: 268
		private readonly string _algorithmName;
	}
}
