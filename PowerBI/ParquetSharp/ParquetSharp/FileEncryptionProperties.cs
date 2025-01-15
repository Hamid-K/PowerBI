using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x0200003B RID: 59
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FileEncryptionProperties : IDisposable
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x00005F30 File Offset: 0x00004130
		internal FileEncryptionProperties(IntPtr handle)
		{
			this.Handle = new ParquetHandle(handle, new Action<IntPtr>(FileEncryptionProperties.FileEncryptionProperties_Free));
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00005F50 File Offset: 0x00004150
		public void Dispose()
		{
			this.Handle.Dispose();
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00005F60 File Offset: 0x00004160
		public bool EncryptedFooter
		{
			get
			{
				return ExceptionInfo.Return<bool>(this.Handle, new ExceptionInfo.GetFunction<bool>(FileEncryptionProperties.FileEncryptionProperties_Encrypted_Footer));
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00005F7C File Offset: 0x0000417C
		public byte[] FooterKey
		{
			get
			{
				return ExceptionInfo.Return<AesKey>(this.Handle, new ExceptionInfo.GetFunction<AesKey>(FileEncryptionProperties.FileEncryptionProperties_Footer_Key)).ToBytes();
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00005FAC File Offset: 0x000041AC
		public string FooterKeyMetadata
		{
			get
			{
				return ExceptionInfo.ReturnString(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(FileEncryptionProperties.FileEncryptionProperties_Footer_Key_Metadata), new Action<IntPtr>(FileEncryptionProperties.FileEncryptionProperties_Footer_Key_Metadata_Free));
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00005FD4 File Offset: 0x000041D4
		public string FileAad
		{
			get
			{
				return ExceptionInfo.ReturnString(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(FileEncryptionProperties.FileEncryptionProperties_File_Aad), new Action<IntPtr>(FileEncryptionProperties.FileEncryptionProperties_File_Aad_Free));
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00005FFC File Offset: 0x000041FC
		public ColumnEncryptionProperties ColumnEncryptionProperties(string columnPath)
		{
			ColumnEncryptionProperties columnEncryptionProperties;
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				columnEncryptionProperties = new ColumnEncryptionProperties(ExceptionInfo.Return<IntPtr, IntPtr>(this.Handle, StringUtil.ToCStringUtf8(columnPath, byteBuffer), new ExceptionInfo.GetFunction<IntPtr, IntPtr>(FileEncryptionProperties.FileEncryptionProperties_Column_Encryption_Properties)));
			}
			return columnEncryptionProperties;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00006058 File Offset: 0x00004258
		public FileEncryptionProperties DeepClone()
		{
			return new FileEncryptionProperties(ExceptionInfo.Return<IntPtr>(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(FileEncryptionProperties.FileEncryptionProperties_Deep_Clone)));
		}

		// Token: 0x060001AF RID: 431
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileEncryptionProperties_Deep_Clone(IntPtr properties, out IntPtr clone);

		// Token: 0x060001B0 RID: 432
		[DllImport("ParquetSharpNative")]
		private static extern void FileEncryptionProperties_Free(IntPtr properties);

		// Token: 0x060001B1 RID: 433
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileEncryptionProperties_Encrypted_Footer(IntPtr properties, [MarshalAs(UnmanagedType.I1)] out bool encryptedFooter);

		// Token: 0x060001B2 RID: 434
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileEncryptionProperties_Algorithm(IntPtr properties, IntPtr algorithm);

		// Token: 0x060001B3 RID: 435
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileEncryptionProperties_Footer_Key(IntPtr properties, out AesKey footerKey);

		// Token: 0x060001B4 RID: 436
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileEncryptionProperties_Footer_Key_Metadata(IntPtr properties, out IntPtr footerKeyMetadata);

		// Token: 0x060001B5 RID: 437
		[DllImport("ParquetSharpNative")]
		private static extern void FileEncryptionProperties_Footer_Key_Metadata_Free(IntPtr footerKeyMetadata);

		// Token: 0x060001B6 RID: 438
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileEncryptionProperties_File_Aad(IntPtr properties, out IntPtr fileAad);

		// Token: 0x060001B7 RID: 439
		[DllImport("ParquetSharpNative")]
		private static extern void FileEncryptionProperties_File_Aad_Free(IntPtr fileAad);

		// Token: 0x060001B8 RID: 440
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileEncryptionProperties_Column_Encryption_Properties(IntPtr properties, IntPtr columnPath, out IntPtr columnEncryptionProperties);

		// Token: 0x04000070 RID: 112
		internal readonly ParquetHandle Handle;
	}
}
