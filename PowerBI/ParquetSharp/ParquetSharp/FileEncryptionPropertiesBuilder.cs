using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x0200003C RID: 60
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FileEncryptionPropertiesBuilder : IDisposable
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x00006078 File Offset: 0x00004278
		public FileEncryptionPropertiesBuilder(byte[] footerKey)
		{
			AesKey aesKey = new AesKey(footerKey);
			IntPtr intPtr;
			ExceptionInfo.Check(FileEncryptionPropertiesBuilder.FileEncryptionPropertiesBuilder_Create(in aesKey, out intPtr));
			this._handle = new ParquetHandle(intPtr, new Action<IntPtr>(FileEncryptionPropertiesBuilder.FileEncryptionPropertiesBuilder_Free));
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000060C0 File Offset: 0x000042C0
		public void Dispose()
		{
			this._handle.Dispose();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000060D0 File Offset: 0x000042D0
		public FileEncryptionPropertiesBuilder SetPlaintextFooter()
		{
			ExceptionInfo.Check(FileEncryptionPropertiesBuilder.FileEncryptionPropertiesBuilder_Set_Plaintext_Footer(this._handle.IntPtr));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000060F4 File Offset: 0x000042F4
		public FileEncryptionPropertiesBuilder Algorithm(ParquetCipher parquetCipher)
		{
			ExceptionInfo.Check(FileEncryptionPropertiesBuilder.FileEncryptionPropertiesBuilder_Algorithm(this._handle.IntPtr, parquetCipher));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00006118 File Offset: 0x00004318
		public FileEncryptionPropertiesBuilder FooterKeyId(string footerKeyId)
		{
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				ExceptionInfo.Check(FileEncryptionPropertiesBuilder.FileEncryptionPropertiesBuilder_Footer_Key_Id(this._handle.IntPtr, StringUtil.ToCStringUtf8(footerKeyId, byteBuffer)));
				GC.KeepAlive(this._handle);
			}
			return this;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000617C File Offset: 0x0000437C
		public FileEncryptionPropertiesBuilder FooterKeyMetadata(string footerKeyMetadata)
		{
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				ExceptionInfo.Check(FileEncryptionPropertiesBuilder.FileEncryptionPropertiesBuilder_Footer_Key_Metadata(this._handle.IntPtr, StringUtil.ToCStringUtf8(footerKeyMetadata, byteBuffer)));
				GC.KeepAlive(this._handle);
			}
			return this;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000061E0 File Offset: 0x000043E0
		public FileEncryptionPropertiesBuilder AadPrefix(string aadPrefix)
		{
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				ExceptionInfo.Check(FileEncryptionPropertiesBuilder.FileEncryptionPropertiesBuilder_Aad_Prefix(this._handle.IntPtr, StringUtil.ToCStringUtf8(aadPrefix, byteBuffer)));
				GC.KeepAlive(this._handle);
			}
			return this;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00006244 File Offset: 0x00004444
		public FileEncryptionPropertiesBuilder DisableAadPrefixStorage()
		{
			ExceptionInfo.Check(FileEncryptionPropertiesBuilder.FileEncryptionPropertiesBuilder_Disable_Aad_Prefix_Storage(this._handle.IntPtr));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00006268 File Offset: 0x00004468
		public FileEncryptionPropertiesBuilder EncryptedColumns(ColumnEncryptionProperties[] columnEncryptionProperties)
		{
			IntPtr[] array = columnEncryptionProperties.Select((ColumnEncryptionProperties p) => p.Handle.IntPtr).ToArray<IntPtr>();
			ExceptionInfo.Check(FileEncryptionPropertiesBuilder.FileEncryptionPropertiesBuilder_Encrypted_Columns(this._handle.IntPtr, array, array.Length));
			GC.KeepAlive(this._handle);
			GC.KeepAlive(columnEncryptionProperties);
			return this;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000062D4 File Offset: 0x000044D4
		public FileEncryptionProperties Build()
		{
			return new FileEncryptionProperties(ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(FileEncryptionPropertiesBuilder.FileEncryptionPropertiesBuilder_Build)));
		}

		// Token: 0x060001C3 RID: 451
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileEncryptionPropertiesBuilder_Create(in AesKey footerKey, out IntPtr builder);

		// Token: 0x060001C4 RID: 452
		[DllImport("ParquetSharpNative")]
		private static extern void FileEncryptionPropertiesBuilder_Free(IntPtr builder);

		// Token: 0x060001C5 RID: 453
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileEncryptionPropertiesBuilder_Set_Plaintext_Footer(IntPtr builder);

		// Token: 0x060001C6 RID: 454
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileEncryptionPropertiesBuilder_Algorithm(IntPtr builder, ParquetCipher parquetCipher);

		// Token: 0x060001C7 RID: 455
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileEncryptionPropertiesBuilder_Footer_Key_Id(IntPtr builder, IntPtr footerKeyId);

		// Token: 0x060001C8 RID: 456
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileEncryptionPropertiesBuilder_Footer_Key_Metadata(IntPtr builder, IntPtr footerKeyMetadata);

		// Token: 0x060001C9 RID: 457
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileEncryptionPropertiesBuilder_Aad_Prefix(IntPtr builder, IntPtr aadPrefix);

		// Token: 0x060001CA RID: 458
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileEncryptionPropertiesBuilder_Disable_Aad_Prefix_Storage(IntPtr builder);

		// Token: 0x060001CB RID: 459
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileEncryptionPropertiesBuilder_Encrypted_Columns(IntPtr builder, IntPtr[] columnEncryptionProperties, int numProperties);

		// Token: 0x060001CC RID: 460
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileEncryptionPropertiesBuilder_Build(IntPtr builder, out IntPtr properties);

		// Token: 0x04000071 RID: 113
		private readonly ParquetHandle _handle;
	}
}
