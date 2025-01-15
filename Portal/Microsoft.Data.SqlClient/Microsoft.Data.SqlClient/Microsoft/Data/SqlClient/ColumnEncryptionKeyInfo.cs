using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000026 RID: 38
	internal class ColumnEncryptionKeyInfo
	{
		// Token: 0x060006AD RID: 1709 RVA: 0x0000D404 File Offset: 0x0000B604
		internal ColumnEncryptionKeyInfo(byte[] decryptedKey, int databaseId, byte[] keyMetadataVersion, int keyid)
		{
			if (decryptedKey == null)
			{
				throw SQL.NullArgumentInConstructorInternal(ColumnEncryptionKeyInfo._decryptedKeyName, ColumnEncryptionKeyInfo._className);
			}
			if (decryptedKey.Length == 0)
			{
				throw SQL.EmptyArgumentInConstructorInternal(ColumnEncryptionKeyInfo._decryptedKeyName, ColumnEncryptionKeyInfo._className);
			}
			if (keyMetadataVersion == null)
			{
				throw SQL.NullArgumentInConstructorInternal(ColumnEncryptionKeyInfo._keyMetadataVersionName, ColumnEncryptionKeyInfo._className);
			}
			if (keyMetadataVersion.Length == 0)
			{
				throw SQL.EmptyArgumentInConstructorInternal(ColumnEncryptionKeyInfo._keyMetadataVersionName, ColumnEncryptionKeyInfo._className);
			}
			this.KeyId = keyid;
			this.DatabaseId = databaseId;
			this.DecryptedKeyBytes = decryptedKey;
			this.KeyMetadataVersionBytes = keyMetadataVersion;
			ushort num;
			try
			{
				num = (ushort)keyid;
			}
			catch (Exception ex)
			{
				throw SQL.InvalidKeyIdUnableToCastToUnsignedShort(keyid, ex);
			}
			this.KeyIdBytes = BitConverter.GetBytes(num);
			try
			{
			}
			catch (Exception ex2)
			{
				throw SQL.InvalidDatabaseIdUnableToCastToUnsignedInt(databaseId, ex2);
			}
			this.DatabaseIdBytes = BitConverter.GetBytes((uint)databaseId);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0000D4D4 File Offset: 0x0000B6D4
		internal int GetLengthForSerialization()
		{
			int num = 0;
			num += this.DecryptedKeyBytes.Length;
			num += this.KeyIdBytes.Length;
			num += this.DatabaseIdBytes.Length;
			return num + this.KeyMetadataVersionBytes.Length;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0000D510 File Offset: 0x0000B710
		internal int SerializeToBuffer(byte[] bytePackage, int startOffset)
		{
			if (bytePackage == null)
			{
				throw SQL.NullArgumentInternal(ColumnEncryptionKeyInfo._bytePackageName, ColumnEncryptionKeyInfo._className, ColumnEncryptionKeyInfo._serializeToBufferMethodName);
			}
			if (bytePackage.Length == 0)
			{
				throw SQL.EmptyArgumentInternal(ColumnEncryptionKeyInfo._bytePackageName, ColumnEncryptionKeyInfo._className, ColumnEncryptionKeyInfo._serializeToBufferMethodName);
			}
			if (startOffset >= bytePackage.Length)
			{
				throw SQL.OffsetOutOfBounds(ColumnEncryptionKeyInfo._startOffsetName, ColumnEncryptionKeyInfo._className, ColumnEncryptionKeyInfo._serializeToBufferMethodName);
			}
			if (bytePackage.Length - startOffset < this.GetLengthForSerialization())
			{
				throw SQL.InsufficientBuffer(ColumnEncryptionKeyInfo._bytePackageName, ColumnEncryptionKeyInfo._className, ColumnEncryptionKeyInfo._serializeToBufferMethodName);
			}
			Buffer.BlockCopy(this.DatabaseIdBytes, 0, bytePackage, startOffset, this.DatabaseIdBytes.Length);
			startOffset += this.DatabaseIdBytes.Length;
			Buffer.BlockCopy(this.KeyMetadataVersionBytes, 0, bytePackage, startOffset, this.KeyMetadataVersionBytes.Length);
			startOffset += this.KeyMetadataVersionBytes.Length;
			Buffer.BlockCopy(this.KeyIdBytes, 0, bytePackage, startOffset, this.KeyIdBytes.Length);
			startOffset += this.KeyIdBytes.Length;
			Buffer.BlockCopy(this.DecryptedKeyBytes, 0, bytePackage, startOffset, this.DecryptedKeyBytes.Length);
			startOffset += this.DecryptedKeyBytes.Length;
			return startOffset;
		}

		// Token: 0x0400007B RID: 123
		internal readonly int KeyId;

		// Token: 0x0400007C RID: 124
		internal readonly int DatabaseId;

		// Token: 0x0400007D RID: 125
		internal readonly byte[] DecryptedKeyBytes;

		// Token: 0x0400007E RID: 126
		internal readonly byte[] KeyIdBytes;

		// Token: 0x0400007F RID: 127
		internal readonly byte[] DatabaseIdBytes;

		// Token: 0x04000080 RID: 128
		internal readonly byte[] KeyMetadataVersionBytes;

		// Token: 0x04000081 RID: 129
		private static readonly string _decryptedKeyName = "DecryptedKey";

		// Token: 0x04000082 RID: 130
		private static readonly string _keyMetadataVersionName = "KeyMetadataVersion";

		// Token: 0x04000083 RID: 131
		private static readonly string _className = "ColumnEncryptionKeyInfo";

		// Token: 0x04000084 RID: 132
		private static readonly string _bytePackageName = "BytePackage";

		// Token: 0x04000085 RID: 133
		private static readonly string _serializeToBufferMethodName = "SerializeToBuffer";

		// Token: 0x04000086 RID: 134
		private static readonly string _startOffsetName = "StartOffset";
	}
}
