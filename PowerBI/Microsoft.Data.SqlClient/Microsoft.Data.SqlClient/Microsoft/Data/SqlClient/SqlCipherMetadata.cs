using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200010D RID: 269
	internal class SqlCipherMetadata
	{
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x0005E96C File Offset: 0x0005CB6C
		// (set) Token: 0x0600159C RID: 5532 RVA: 0x0005E974 File Offset: 0x0005CB74
		internal SqlTceCipherInfoEntry EncryptionInfo
		{
			get
			{
				return this._sqlTceCipherInfoEntry;
			}
			set
			{
				this._sqlTceCipherInfoEntry = value;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x0005E97D File Offset: 0x0005CB7D
		internal byte CipherAlgorithmId
		{
			get
			{
				return this._cipherAlgorithmId;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x0005E985 File Offset: 0x0005CB85
		internal string CipherAlgorithmName
		{
			get
			{
				return this._cipherAlgorithmName;
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x0005E98D File Offset: 0x0005CB8D
		internal byte EncryptionType
		{
			get
			{
				return this._encryptionType;
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x0005E995 File Offset: 0x0005CB95
		internal byte NormalizationRuleVersion
		{
			get
			{
				return this._normalizationRuleVersion;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x0005E99D File Offset: 0x0005CB9D
		// (set) Token: 0x060015A2 RID: 5538 RVA: 0x0005E9A5 File Offset: 0x0005CBA5
		internal SqlClientEncryptionAlgorithm CipherAlgorithm
		{
			get
			{
				return this._sqlClientEncryptionAlgorithm;
			}
			set
			{
				this._sqlClientEncryptionAlgorithm = value;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x0005E9AE File Offset: 0x0005CBAE
		// (set) Token: 0x060015A4 RID: 5540 RVA: 0x0005E9B6 File Offset: 0x0005CBB6
		internal SqlEncryptionKeyInfo EncryptionKeyInfo
		{
			get
			{
				return this._sqlEncryptionKeyInfo;
			}
			set
			{
				this._sqlEncryptionKeyInfo = value;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x0005E9BF File Offset: 0x0005CBBF
		internal ushort CekTableOrdinal
		{
			get
			{
				return this._ordinal;
			}
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x0005E9C7 File Offset: 0x0005CBC7
		internal SqlCipherMetadata(SqlTceCipherInfoEntry sqlTceCipherInfoEntry, ushort ordinal, byte cipherAlgorithmId, string cipherAlgorithmName, byte encryptionType, byte normalizationRuleVersion)
		{
			this._sqlTceCipherInfoEntry = sqlTceCipherInfoEntry;
			this._ordinal = ordinal;
			this._cipherAlgorithmId = cipherAlgorithmId;
			this._cipherAlgorithmName = cipherAlgorithmName;
			this._encryptionType = encryptionType;
			this._normalizationRuleVersion = normalizationRuleVersion;
			this._sqlEncryptionKeyInfo = null;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0005EA03 File Offset: 0x0005CC03
		internal bool IsAlgorithmInitialized()
		{
			return this._sqlClientEncryptionAlgorithm != null;
		}

		// Token: 0x0400088F RID: 2191
		private SqlTceCipherInfoEntry _sqlTceCipherInfoEntry;

		// Token: 0x04000890 RID: 2192
		private readonly byte _cipherAlgorithmId;

		// Token: 0x04000891 RID: 2193
		private readonly string _cipherAlgorithmName;

		// Token: 0x04000892 RID: 2194
		private readonly byte _encryptionType;

		// Token: 0x04000893 RID: 2195
		private readonly byte _normalizationRuleVersion;

		// Token: 0x04000894 RID: 2196
		private SqlClientEncryptionAlgorithm _sqlClientEncryptionAlgorithm;

		// Token: 0x04000895 RID: 2197
		private SqlEncryptionKeyInfo _sqlEncryptionKeyInfo;

		// Token: 0x04000896 RID: 2198
		private readonly ushort _ordinal;
	}
}
