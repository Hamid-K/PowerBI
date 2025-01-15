using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Mashup.Security;

namespace Microsoft.Mashup.Client.Packaging
{
	// Token: 0x0200000B RID: 11
	public class PackageComponents
	{
		// Token: 0x06000012 RID: 18 RVA: 0x0000252C File Offset: 0x0000072C
		public PackageComponents(byte[] partsBytes, byte[] permissionBytes, byte[] metadataBytes)
		{
			this.partsBytes = partsBytes;
			this.permissionBytes = permissionBytes;
			this.metadataBytes = metadataBytes;
			this.permissionBinding = PackageComponents.CreatePermissionBinding(partsBytes, permissionBytes);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002556 File Offset: 0x00000756
		public PackageComponents(byte[] partsBytes, byte[] permissionBytes, byte[] metadataBytes, byte[] permissionBinding)
		{
			this.partsBytes = partsBytes;
			this.permissionBytes = permissionBytes;
			this.metadataBytes = metadataBytes;
			this.permissionBinding = permissionBinding;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000257B File Offset: 0x0000077B
		public byte[] PartsBytes
		{
			get
			{
				return this.partsBytes;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002583 File Offset: 0x00000783
		public byte[] PermissionBytes
		{
			get
			{
				if (!PackageComponents.VerifyPermissionBinding(this.partsBytes, this.permissionBytes, this.permissionBinding))
				{
					return new byte[0];
				}
				return this.permissionBytes;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000025AB File Offset: 0x000007AB
		public byte[] MetadataBytes
		{
			get
			{
				return this.metadataBytes;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000025B3 File Offset: 0x000007B3
		public byte[] UnverifiedPermissionBytes
		{
			get
			{
				return this.permissionBytes;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000025BB File Offset: 0x000007BB
		public byte[] PermissionBinding
		{
			get
			{
				return this.permissionBinding;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025C4 File Offset: 0x000007C4
		public static bool IsDelegationSupported()
		{
			byte[] array = new byte[1];
			try
			{
				UserProtectedDataServices.Protect(array, array);
			}
			catch (CryptographicException ex)
			{
				if (ex.GetHResult() == -2146892987)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000260C File Offset: 0x0000080C
		public static bool TryDeserialize(byte[] bytes, out PackageComponents packageComponents)
		{
			bool flag;
			try
			{
				packageComponents = PackageComponents.Deserialize(bytes);
				flag = true;
			}
			catch (Exception ex)
			{
				if (!(ex is FormatException) && !(ex is IOException))
				{
					throw;
				}
				packageComponents = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002650 File Offset: 0x00000850
		public static PackageComponents Deserialize(byte[] bytes)
		{
			MemoryStream memoryStream = new MemoryStream(bytes);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			int num = binaryReader.ReadInt32();
			if (num != 0)
			{
				throw new FormatException();
			}
			byte[] array = PackageComponents.ReadBytes(binaryReader);
			byte[] array2 = PackageComponents.ReadBytes(binaryReader);
			byte[] array3 = PackageComponents.ReadBytes(binaryReader);
			byte[] array4 = PackageComponents.ReadBytes(binaryReader);
			return new PackageComponents(array, array2, array3, array4);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000026A8 File Offset: 0x000008A8
		public byte[] Serialize()
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(0);
			PackageComponents.WriteBytes(binaryWriter, this.partsBytes);
			PackageComponents.WriteBytes(binaryWriter, this.permissionBytes);
			PackageComponents.WriteBytes(binaryWriter, this.metadataBytes);
			PackageComponents.WriteBytes(binaryWriter, this.permissionBinding);
			binaryWriter.Flush();
			return memoryStream.ToArray();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002708 File Offset: 0x00000908
		public static byte[] HashBytes(byte[] bytes)
		{
			byte[] array;
			using (SHA256 sha = SHA256CryptoProvider.Create())
			{
				array = sha.ComputeHash(bytes);
			}
			return array;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002740 File Offset: 0x00000940
		public static byte[] EncryptBytes(byte[] bytes)
		{
			return UserProtectedDataServices.Protect(bytes, PackageComponents.GetAdditionalEntropy());
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000274D File Offset: 0x0000094D
		public static byte[] DecryptBytes(byte[] permissionBytes)
		{
			return UserProtectedDataServices.Unprotect(permissionBytes, PackageComponents.GetAdditionalEntropy());
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000275C File Offset: 0x0000095C
		private static byte[] CreatePermissionBinding(byte[] partsBytes, byte[] permissionBytes)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			byte[] array = PackageComponents.HashBytes(partsBytes);
			byte[] array2 = PackageComponents.HashBytes(permissionBytes);
			PackageComponents.WriteBytes(binaryWriter, array);
			PackageComponents.WriteBytes(binaryWriter, array2);
			binaryWriter.Flush();
			byte[] array3 = memoryStream.ToArray();
			return PackageComponents.EncryptBytes(array3);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000027A8 File Offset: 0x000009A8
		private static bool VerifyPermissionBinding(byte[] partsBytes, byte[] permissionBytes, byte[] permissionBinding)
		{
			byte[] array2;
			byte[] array3;
			try
			{
				byte[] array = PackageComponents.DecryptBytes(permissionBinding);
				MemoryStream memoryStream = new MemoryStream(array);
				BinaryReader binaryReader = new BinaryReader(memoryStream);
				array2 = PackageComponents.ReadBytes(binaryReader);
				array3 = PackageComponents.ReadBytes(binaryReader);
			}
			catch (CryptographicException)
			{
				return false;
			}
			catch (FormatException)
			{
				return false;
			}
			catch (IOException)
			{
				return false;
			}
			byte[] array4 = PackageComponents.HashBytes(partsBytes);
			byte[] array5 = PackageComponents.HashBytes(permissionBytes);
			return PackageComponents.AreBytesEqual(array2, array4) && PackageComponents.AreBytesEqual(array3, array5);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002844 File Offset: 0x00000A44
		private static byte[] GetAdditionalEntropy()
		{
			return Encoding.UTF8.GetBytes(PackageComponents.additionalEntropy);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002855 File Offset: 0x00000A55
		private static void WriteBytes(BinaryWriter writer, byte[] bytes)
		{
			writer.Write(bytes.Length);
			writer.Write(bytes);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002868 File Offset: 0x00000A68
		private static byte[] ReadBytes(BinaryReader reader)
		{
			int num = reader.ReadInt32();
			if (num < 0)
			{
				throw new FormatException("Invalid size");
			}
			return reader.ReadBytes(num);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002894 File Offset: 0x00000A94
		private static bool AreBytesEqual(byte[] bytes1, byte[] bytes2)
		{
			if (bytes1.Length != bytes2.Length)
			{
				return false;
			}
			for (int i = 0; i < bytes1.Length; i++)
			{
				if (bytes1[i] != bytes2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400003D RID: 61
		private const int delegationRequiredHResult = -2146892987;

		// Token: 0x0400003E RID: 62
		private const int currentVersion = 0;

		// Token: 0x0400003F RID: 63
		private byte[] partsBytes;

		// Token: 0x04000040 RID: 64
		private byte[] permissionBytes;

		// Token: 0x04000041 RID: 65
		private byte[] metadataBytes;

		// Token: 0x04000042 RID: 66
		private byte[] permissionBinding;

		// Token: 0x04000043 RID: 67
		private static readonly string additionalEntropy = "DataExplorer Package Components";
	}
}
