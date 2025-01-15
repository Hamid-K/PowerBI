using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000019 RID: 25
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ColumnEncryptionProperties : IDisposable
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00003A18 File Offset: 0x00001C18
		internal ColumnEncryptionProperties(IntPtr handle)
		{
			this.Handle = new ParquetHandle(handle, new Action<IntPtr>(ColumnEncryptionProperties.ColumnEncryptionProperties_Free));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003A38 File Offset: 0x00001C38
		public void Dispose()
		{
			this.Handle.Dispose();
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003A48 File Offset: 0x00001C48
		public string ColumnPath
		{
			get
			{
				return ExceptionInfo.ReturnString(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(ColumnEncryptionProperties.ColumnEncryptionProperties_Column_Path), new Action<IntPtr>(ColumnEncryptionProperties.ColumnEncryptionProperties_Column_Path_Free));
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003A70 File Offset: 0x00001C70
		public bool IsEncrypted
		{
			get
			{
				return ExceptionInfo.Return<bool>(this.Handle, new ExceptionInfo.GetFunction<bool>(ColumnEncryptionProperties.ColumnEncryptionProperties_Is_Encrypted));
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003A8C File Offset: 0x00001C8C
		public bool IsEncryptedWithFooterKey
		{
			get
			{
				return ExceptionInfo.Return<bool>(this.Handle, new ExceptionInfo.GetFunction<bool>(ColumnEncryptionProperties.ColumnEncryptionProperties_Is_Encrypted_With_Footer_Key));
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003AA8 File Offset: 0x00001CA8
		public byte[] Key
		{
			get
			{
				return ExceptionInfo.Return<AesKey>(this.Handle, new ExceptionInfo.GetFunction<AesKey>(ColumnEncryptionProperties.ColumnEncryptionProperties_Key)).ToBytes();
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003AD8 File Offset: 0x00001CD8
		public string KeyMetadata
		{
			get
			{
				return ExceptionInfo.ReturnString(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(ColumnEncryptionProperties.ColumnEncryptionProperties_Key_Metadata), new Action<IntPtr>(ColumnEncryptionProperties.ColumnEncryptionProperties_Key_Metadata_Free));
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003B00 File Offset: 0x00001D00
		public ColumnEncryptionProperties DeepClone()
		{
			return new ColumnEncryptionProperties(ExceptionInfo.Return<IntPtr>(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(ColumnEncryptionProperties.ColumnEncryptionProperties_Deep_Clone)));
		}

		// Token: 0x06000097 RID: 151
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnEncryptionProperties_Deep_Clone(IntPtr properties, out IntPtr clone);

		// Token: 0x06000098 RID: 152
		[DllImport("ParquetSharpNative")]
		private static extern void ColumnEncryptionProperties_Free(IntPtr properties);

		// Token: 0x06000099 RID: 153
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnEncryptionProperties_Column_Path(IntPtr properties, out IntPtr columnPath);

		// Token: 0x0600009A RID: 154
		[DllImport("ParquetSharpNative")]
		private static extern void ColumnEncryptionProperties_Column_Path_Free(IntPtr columnPath);

		// Token: 0x0600009B RID: 155
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnEncryptionProperties_Is_Encrypted(IntPtr properties, [MarshalAs(UnmanagedType.I1)] out bool isEncrypted);

		// Token: 0x0600009C RID: 156
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnEncryptionProperties_Is_Encrypted_With_Footer_Key(IntPtr properties, [MarshalAs(UnmanagedType.I1)] out bool isEncryptedWithFooterKey);

		// Token: 0x0600009D RID: 157
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnEncryptionProperties_Key(IntPtr properties, out AesKey key);

		// Token: 0x0600009E RID: 158
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnEncryptionProperties_Key_Metadata(IntPtr properties, out IntPtr keyMetadata);

		// Token: 0x0600009F RID: 159
		[DllImport("ParquetSharpNative")]
		private static extern void ColumnEncryptionProperties_Key_Metadata_Free(IntPtr keyMetadata);

		// Token: 0x04000031 RID: 49
		internal readonly ParquetHandle Handle;
	}
}
