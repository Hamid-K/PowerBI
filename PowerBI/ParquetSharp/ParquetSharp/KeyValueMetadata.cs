using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000045 RID: 69
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class KeyValueMetadata : IDisposable
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x000065F8 File Offset: 0x000047F8
		public KeyValueMetadata()
			: this(KeyValueMetadata.MakeEmpty())
		{
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006608 File Offset: 0x00004808
		public KeyValueMetadata(IntPtr handle)
		{
			this.Handle = new ParquetHandle(handle, new Action<IntPtr>(KeyValueMetadata.KeyValueMetadata_Free));
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006628 File Offset: 0x00004828
		public void Dispose()
		{
			this.Handle.Dispose();
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00006638 File Offset: 0x00004838
		private long Size
		{
			get
			{
				return ExceptionInfo.Return<long>(this.Handle, new ExceptionInfo.GetFunction<long>(KeyValueMetadata.KeyValueMetadata_Size));
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006654 File Offset: 0x00004854
		public void SetData(IReadOnlyDictionary<string, string> keyValueMetadata)
		{
			using (ByteBuffer byteBuffer = new ByteBuffer(1024, 0))
			{
				foreach (KeyValuePair<string, string> keyValuePair in keyValueMetadata)
				{
					IntPtr intPtr = StringUtil.ToCStringUtf8(keyValuePair.Key, byteBuffer);
					IntPtr intPtr2 = StringUtil.ToCStringUtf8(keyValuePair.Value, byteBuffer);
					ExceptionInfo.Check(KeyValueMetadata.KeyValueMetadata_Append(this.Handle.IntPtr, intPtr, intPtr2));
				}
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000066F8 File Offset: 0x000048F8
		public unsafe IReadOnlyDictionary<string, string> ToDictionary()
		{
			IntPtr intPtr;
			IntPtr intPtr2;
			ExceptionInfo.Check(KeyValueMetadata.KeyValueMetadata_Get_Entries(this.Handle.IntPtr, out intPtr, out intPtr2));
			IReadOnlyDictionary<string, string> readOnlyDictionary;
			try
			{
				int num = checked((int)this.Size);
				Dictionary<string, string> dictionary = new Dictionary<string, string>(num);
				for (int num2 = 0; num2 != num; num2++)
				{
					string text = StringUtil.PtrToStringUtf8(*(IntPtr*)((byte*)(void*)intPtr + (IntPtr)num2 * (IntPtr)sizeof(IntPtr)));
					string text2 = StringUtil.PtrToStringUtf8(*(IntPtr*)((byte*)(void*)intPtr2 + (IntPtr)num2 * (IntPtr)sizeof(IntPtr)));
					dictionary.Add(text, text2);
				}
				readOnlyDictionary = dictionary;
			}
			finally
			{
				KeyValueMetadata.KeyValueMetadata_Free_Entries(this.Handle.IntPtr, intPtr, intPtr2);
				GC.KeepAlive(this.Handle);
			}
			return readOnlyDictionary;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000067BC File Offset: 0x000049BC
		private static IntPtr MakeEmpty()
		{
			IntPtr intPtr;
			ExceptionInfo.Check(KeyValueMetadata.KeyValueMetadata_MakeEmpty(out intPtr));
			return intPtr;
		}

		// Token: 0x060001FA RID: 506
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr KeyValueMetadata_MakeEmpty(out IntPtr keyValueMetadata);

		// Token: 0x060001FB RID: 507
		[DllImport("ParquetSharpNative")]
		private static extern void KeyValueMetadata_Free(IntPtr keyValueMetadata);

		// Token: 0x060001FC RID: 508
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr KeyValueMetadata_Size(IntPtr keyValueMetadata, out long size);

		// Token: 0x060001FD RID: 509
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr KeyValueMetadata_Append(IntPtr keyValueMetadata, IntPtr key, IntPtr value);

		// Token: 0x060001FE RID: 510
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr KeyValueMetadata_Get_Entries(IntPtr keyValueMetadata, out IntPtr keys, out IntPtr values);

		// Token: 0x060001FF RID: 511
		[DllImport("ParquetSharpNative")]
		private static extern void KeyValueMetadata_Free_Entries(IntPtr keyValueMetadata, IntPtr keys, IntPtr values);

		// Token: 0x04000078 RID: 120
		internal readonly ParquetHandle Handle;
	}
}
