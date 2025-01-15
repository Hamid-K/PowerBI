using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ParquetSharp.Schema;

namespace ParquetSharp
{
	// Token: 0x02000017 RID: 23
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ColumnDecryptionPropertiesBuilder : IDisposable
	{
		// Token: 0x06000062 RID: 98 RVA: 0x0000345C File Offset: 0x0000165C
		public ColumnDecryptionPropertiesBuilder(string columnName)
			: this(ColumnDecryptionPropertiesBuilder.Make(columnName))
		{
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000346C File Offset: 0x0000166C
		public ColumnDecryptionPropertiesBuilder(ColumnPath columnPath)
			: this(ColumnDecryptionPropertiesBuilder.Make(columnPath))
		{
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000347C File Offset: 0x0000167C
		internal ColumnDecryptionPropertiesBuilder(IntPtr handle)
		{
			this._handle = new ParquetHandle(handle, new Action<IntPtr>(ColumnDecryptionPropertiesBuilder.ColumnDecryptionPropertiesBuilder_Free));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000349C File Offset: 0x0000169C
		public void Dispose()
		{
			this._handle.Dispose();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000034AC File Offset: 0x000016AC
		public ColumnDecryptionPropertiesBuilder Key(byte[] key)
		{
			AesKey aesKey = new AesKey(key);
			ExceptionInfo.Check(ColumnDecryptionPropertiesBuilder.ColumnDecryptionPropertiesBuilder_Key(this._handle.IntPtr, in aesKey));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000034E8 File Offset: 0x000016E8
		public ColumnDecryptionProperties Build()
		{
			return new ColumnDecryptionProperties(ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(ColumnDecryptionPropertiesBuilder.ColumnDecryptionPropertiesBuilder_Build)));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003508 File Offset: 0x00001708
		private static IntPtr Make(string columnName)
		{
			IntPtr intPtr2;
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				IntPtr intPtr;
				ExceptionInfo.Check(ColumnDecryptionPropertiesBuilder.ColumnDecryptionPropertiesBuilder_Create(StringUtil.ToCStringUtf8(columnName, byteBuffer), out intPtr));
				intPtr2 = intPtr;
			}
			return intPtr2;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003558 File Offset: 0x00001758
		private static IntPtr Make(ColumnPath columnPath)
		{
			IntPtr intPtr;
			ExceptionInfo.Check(ColumnDecryptionPropertiesBuilder.ColumnDecryptionPropertiesBuilder_Create_From_Column_Path(columnPath.Handle.IntPtr, out intPtr));
			GC.KeepAlive(columnPath);
			return intPtr;
		}

		// Token: 0x0600006A RID: 106
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnDecryptionPropertiesBuilder_Create(IntPtr name, out IntPtr builder);

		// Token: 0x0600006B RID: 107
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnDecryptionPropertiesBuilder_Create_From_Column_Path(IntPtr path, out IntPtr builder);

		// Token: 0x0600006C RID: 108
		[DllImport("ParquetSharpNative")]
		private static extern void ColumnDecryptionPropertiesBuilder_Free(IntPtr builder);

		// Token: 0x0600006D RID: 109
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnDecryptionPropertiesBuilder_Key(IntPtr builder, in AesKey key);

		// Token: 0x0600006E RID: 110
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnDecryptionPropertiesBuilder_Build(IntPtr builder, out IntPtr properties);

		// Token: 0x0400002E RID: 46
		private readonly ParquetHandle _handle;
	}
}
