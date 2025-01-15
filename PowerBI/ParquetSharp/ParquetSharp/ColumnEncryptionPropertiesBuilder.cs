using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ParquetSharp.Schema;

namespace ParquetSharp
{
	// Token: 0x0200001A RID: 26
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ColumnEncryptionPropertiesBuilder : IDisposable
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00003B20 File Offset: 0x00001D20
		public ColumnEncryptionPropertiesBuilder(string columnName)
			: this(ColumnEncryptionPropertiesBuilder.Make(columnName))
		{
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003B30 File Offset: 0x00001D30
		public ColumnEncryptionPropertiesBuilder(ColumnPath columnPath)
			: this(ColumnEncryptionPropertiesBuilder.Make(columnPath))
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003B40 File Offset: 0x00001D40
		internal ColumnEncryptionPropertiesBuilder(IntPtr handle)
		{
			this._handle = new ParquetHandle(handle, new Action<IntPtr>(ColumnEncryptionPropertiesBuilder.ColumnEncryptionPropertiesBuilder_Free));
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003B60 File Offset: 0x00001D60
		public void Dispose()
		{
			this._handle.Dispose();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003B70 File Offset: 0x00001D70
		public ColumnEncryptionPropertiesBuilder Key(byte[] key)
		{
			AesKey aesKey = new AesKey(key);
			ExceptionInfo.Check(ColumnEncryptionPropertiesBuilder.ColumnEncryptionPropertiesBuilder_Key(this._handle.IntPtr, in aesKey));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003BAC File Offset: 0x00001DAC
		public ColumnEncryptionPropertiesBuilder KeyMetadata(string keyMetadata)
		{
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				ExceptionInfo.Check(ColumnEncryptionPropertiesBuilder.ColumnEncryptionPropertiesBuilder_Key_Metadata(this._handle.IntPtr, StringUtil.ToCStringUtf8(keyMetadata, byteBuffer)));
				GC.KeepAlive(this._handle);
			}
			return this;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003C10 File Offset: 0x00001E10
		public ColumnEncryptionPropertiesBuilder KeyId(string keyId)
		{
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				ExceptionInfo.Check(ColumnEncryptionPropertiesBuilder.ColumnEncryptionPropertiesBuilder_Key_Id(this._handle.IntPtr, StringUtil.ToCStringUtf8(keyId, byteBuffer)));
				GC.KeepAlive(this._handle);
			}
			return this;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003C74 File Offset: 0x00001E74
		public ColumnEncryptionProperties Build()
		{
			return new ColumnEncryptionProperties(ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(ColumnEncryptionPropertiesBuilder.ColumnEncryptionPropertiesBuilder_Build)));
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003C94 File Offset: 0x00001E94
		private static IntPtr Make(string columnName)
		{
			IntPtr intPtr2;
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				IntPtr intPtr;
				ExceptionInfo.Check(ColumnEncryptionPropertiesBuilder.ColumnEncryptionPropertiesBuilder_Create(StringUtil.ToCStringUtf8(columnName, byteBuffer), out intPtr));
				intPtr2 = intPtr;
			}
			return intPtr2;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003CE4 File Offset: 0x00001EE4
		private static IntPtr Make(ColumnPath columnPath)
		{
			IntPtr intPtr;
			ExceptionInfo.Check(ColumnEncryptionPropertiesBuilder.ColumnEncryptionPropertiesBuilder_Create_From_Column_Path(columnPath.Handle.IntPtr, out intPtr));
			GC.KeepAlive(columnPath);
			return intPtr;
		}

		// Token: 0x060000AA RID: 170
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnEncryptionPropertiesBuilder_Create(IntPtr name, out IntPtr builder);

		// Token: 0x060000AB RID: 171
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnEncryptionPropertiesBuilder_Create_From_Column_Path(IntPtr path, out IntPtr builder);

		// Token: 0x060000AC RID: 172
		[DllImport("ParquetSharpNative")]
		private static extern void ColumnEncryptionPropertiesBuilder_Free(IntPtr builder);

		// Token: 0x060000AD RID: 173
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnEncryptionPropertiesBuilder_Key(IntPtr builder, in AesKey key);

		// Token: 0x060000AE RID: 174
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnEncryptionPropertiesBuilder_Key_Metadata(IntPtr builder, IntPtr keyMetadata);

		// Token: 0x060000AF RID: 175
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnEncryptionPropertiesBuilder_Key_Id(IntPtr builder, IntPtr keyId);

		// Token: 0x060000B0 RID: 176
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnEncryptionPropertiesBuilder_Build(IntPtr builder, out IntPtr properties);

		// Token: 0x04000032 RID: 50
		private readonly ParquetHandle _handle;
	}
}
