using System;
using Microsoft.Data.SqlClient.Server;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000111 RID: 273
	internal sealed class SqlColumnEncryptionInputParameterInfo
	{
		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x0005EBF0 File Offset: 0x0005CDF0
		internal SmiParameterMetaData ParameterMetadata
		{
			get
			{
				return this._smiParameterMetadata;
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x0005EBF8 File Offset: 0x0005CDF8
		internal byte[] SerializedWireFormat
		{
			get
			{
				return this._serializedWireFormat;
			}
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x0005EC00 File Offset: 0x0005CE00
		internal SqlColumnEncryptionInputParameterInfo(SmiParameterMetaData smiParameterMetadata, SqlCipherMetadata cipherMetadata)
		{
			this._smiParameterMetadata = smiParameterMetadata;
			this._cipherMetadata = cipherMetadata;
			this._serializedWireFormat = this.SerializeToWriteFormat();
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0005EC24 File Offset: 0x0005CE24
		private byte[] SerializeToWriteFormat()
		{
			int num = 0;
			num++;
			num++;
			num += 4;
			num += 4;
			num += 4;
			num += this._cipherMetadata.EncryptionKeyInfo.cekMdVersion.Length;
			num++;
			byte[] array = new byte[num];
			int num2 = 0;
			array[num2++] = this._cipherMetadata.CipherAlgorithmId;
			array[num2++] = this._cipherMetadata.EncryptionType;
			this.SerializeIntIntoBuffer(this._cipherMetadata.EncryptionKeyInfo.databaseId, array, ref num2);
			this.SerializeIntIntoBuffer(this._cipherMetadata.EncryptionKeyInfo.cekId, array, ref num2);
			this.SerializeIntIntoBuffer(this._cipherMetadata.EncryptionKeyInfo.cekVersion, array, ref num2);
			Buffer.BlockCopy(this._cipherMetadata.EncryptionKeyInfo.cekMdVersion, 0, array, num2, this._cipherMetadata.EncryptionKeyInfo.cekMdVersion.Length);
			num2 += this._cipherMetadata.EncryptionKeyInfo.cekMdVersion.Length;
			array[num2++] = this._cipherMetadata.NormalizationRuleVersion;
			return array;
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x0005ED2C File Offset: 0x0005CF2C
		private void SerializeIntIntoBuffer(int value, byte[] buffer, ref int offset)
		{
			int num = offset;
			offset = num + 1;
			buffer[num] = (byte)(value & 255);
			num = offset;
			offset = num + 1;
			buffer[num] = (byte)((value >> 8) & 255);
			num = offset;
			offset = num + 1;
			buffer[num] = (byte)((value >> 16) & 255);
			num = offset;
			offset = num + 1;
			buffer[num] = (byte)((value >> 24) & 255);
		}

		// Token: 0x040008AE RID: 2222
		private readonly SmiParameterMetaData _smiParameterMetadata;

		// Token: 0x040008AF RID: 2223
		private readonly SqlCipherMetadata _cipherMetadata;

		// Token: 0x040008B0 RID: 2224
		private readonly byte[] _serializedWireFormat;
	}
}
