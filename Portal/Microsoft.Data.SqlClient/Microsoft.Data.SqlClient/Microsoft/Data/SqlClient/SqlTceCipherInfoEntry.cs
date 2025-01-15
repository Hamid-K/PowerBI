using System;
using System.Collections.Generic;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000103 RID: 259
	internal class SqlTceCipherInfoEntry
	{
		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x0005E25C File Offset: 0x0005C45C
		internal int Ordinal
		{
			get
			{
				return this._ordinal;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x0005E264 File Offset: 0x0005C464
		internal int DatabaseId
		{
			get
			{
				return this._databaseId;
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x0005E26C File Offset: 0x0005C46C
		internal int CekId
		{
			get
			{
				return this._cekId;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06001560 RID: 5472 RVA: 0x0005E274 File Offset: 0x0005C474
		internal int CekVersion
		{
			get
			{
				return this._cekVersion;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x0005E27C File Offset: 0x0005C47C
		internal byte[] CekMdVersion
		{
			get
			{
				return this._cekMdVersion;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x0005E284 File Offset: 0x0005C484
		internal List<SqlEncryptionKeyInfo> ColumnEncryptionKeyValues
		{
			get
			{
				return this._columnEncryptionKeyValues;
			}
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0005E28C File Offset: 0x0005C48C
		internal void Add(byte[] encryptedKey, int databaseId, int cekId, int cekVersion, byte[] cekMdVersion, string keyPath, string keyStoreName, string algorithmName)
		{
			SqlEncryptionKeyInfo sqlEncryptionKeyInfo = new SqlEncryptionKeyInfo();
			sqlEncryptionKeyInfo.encryptedKey = encryptedKey;
			sqlEncryptionKeyInfo.databaseId = databaseId;
			sqlEncryptionKeyInfo.cekId = cekId;
			sqlEncryptionKeyInfo.cekVersion = cekVersion;
			sqlEncryptionKeyInfo.cekMdVersion = cekMdVersion;
			sqlEncryptionKeyInfo.keyPath = keyPath;
			sqlEncryptionKeyInfo.keyStoreName = keyStoreName;
			sqlEncryptionKeyInfo.algorithmName = algorithmName;
			this._columnEncryptionKeyValues.Add(sqlEncryptionKeyInfo);
			if (this._databaseId == 0)
			{
				this._databaseId = databaseId;
				this._cekId = cekId;
				this._cekVersion = cekVersion;
				this._cekMdVersion = cekMdVersion;
			}
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x0005E30E File Offset: 0x0005C50E
		internal SqlTceCipherInfoEntry(int ordinal = 0)
		{
			this._ordinal = ordinal;
			this._databaseId = 0;
			this._cekId = 0;
			this._cekVersion = 0;
			this._cekMdVersion = null;
			this._columnEncryptionKeyValues = new List<SqlEncryptionKeyInfo>();
		}

		// Token: 0x04000858 RID: 2136
		private readonly List<SqlEncryptionKeyInfo> _columnEncryptionKeyValues;

		// Token: 0x04000859 RID: 2137
		private readonly int _ordinal;

		// Token: 0x0400085A RID: 2138
		private int _databaseId;

		// Token: 0x0400085B RID: 2139
		private int _cekId;

		// Token: 0x0400085C RID: 2140
		private int _cekVersion;

		// Token: 0x0400085D RID: 2141
		private byte[] _cekMdVersion;
	}
}
