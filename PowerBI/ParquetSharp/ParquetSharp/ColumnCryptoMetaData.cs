using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ParquetSharp.Schema;

namespace ParquetSharp
{
	// Token: 0x02000015 RID: 21
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ColumnCryptoMetaData : IDisposable
	{
		// Token: 0x0600004F RID: 79 RVA: 0x0000332C File Offset: 0x0000152C
		internal ColumnCryptoMetaData(IntPtr handle)
		{
			this._handle = new ParquetHandle(handle, new Action<IntPtr>(ColumnCryptoMetaData.ColumnCryptoMetaData_Free));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000334C File Offset: 0x0000154C
		public void Dispose()
		{
			this._handle.Dispose();
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000335C File Offset: 0x0000155C
		public ColumnPath ColumnPath
		{
			get
			{
				return new ColumnPath(ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(ColumnCryptoMetaData.ColumnCryptoMetaData_Path_In_Schema)));
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000052 RID: 82 RVA: 0x0000337C File Offset: 0x0000157C
		public bool EncryptedWithFooterKey
		{
			get
			{
				return ExceptionInfo.Return<bool>(this._handle, new ExceptionInfo.GetFunction<bool>(ColumnCryptoMetaData.ColumnCryptoMetaData_Encrypted_With_Footer_Key));
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003398 File Offset: 0x00001598
		public string KeyMetadata
		{
			get
			{
				return ExceptionInfo.ReturnString(this._handle, new ExceptionInfo.GetFunction<IntPtr>(ColumnCryptoMetaData.ColumnCryptoMetaData_Key_Metadata), null);
			}
		}

		// Token: 0x06000054 RID: 84
		[DllImport("ParquetSharpNative")]
		private static extern void ColumnCryptoMetaData_Free(IntPtr columnCryptoMetaData);

		// Token: 0x06000055 RID: 85
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnCryptoMetaData_Path_In_Schema(IntPtr columnCryptoMetaData, out IntPtr columnPath);

		// Token: 0x06000056 RID: 86
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnCryptoMetaData_Encrypted_With_Footer_Key(IntPtr columnCryptoMetaData, [MarshalAs(UnmanagedType.I1)] out bool encryptedWithFooterKey);

		// Token: 0x06000057 RID: 87
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnCryptoMetaData_Key_Metadata(IntPtr columnCryptoMetaData, out IntPtr keyMetadata);

		// Token: 0x0400002C RID: 44
		private readonly ParquetHandle _handle;
	}
}
