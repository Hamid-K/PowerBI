using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000016 RID: 22
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ColumnDecryptionProperties : IDisposable
	{
		// Token: 0x06000058 RID: 88 RVA: 0x000033B4 File Offset: 0x000015B4
		internal ColumnDecryptionProperties(IntPtr handle)
		{
			this.Handle = new ParquetHandle(handle, new Action<IntPtr>(ColumnDecryptionProperties.ColumnDecryptionProperties_Free));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000033D4 File Offset: 0x000015D4
		public void Dispose()
		{
			this.Handle.Dispose();
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000033E4 File Offset: 0x000015E4
		public string ColumnPath
		{
			get
			{
				return ExceptionInfo.ReturnString(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(ColumnDecryptionProperties.ColumnDecryptionProperties_Column_Path), new Action<IntPtr>(ColumnDecryptionProperties.ColumnDecryptionProperties_Column_Path_Free));
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000340C File Offset: 0x0000160C
		public byte[] Key
		{
			get
			{
				return ExceptionInfo.Return<AesKey>(this.Handle, new ExceptionInfo.GetFunction<AesKey>(ColumnDecryptionProperties.ColumnDecryptionProperties_Key)).ToBytes();
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000343C File Offset: 0x0000163C
		public ColumnDecryptionProperties DeepClone()
		{
			return new ColumnDecryptionProperties(ExceptionInfo.Return<IntPtr>(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(ColumnDecryptionProperties.ColumnDecryptionProperties_Deep_Clone)));
		}

		// Token: 0x0600005D RID: 93
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnDecryptionProperties_Deep_Clone(IntPtr properties, out IntPtr clone);

		// Token: 0x0600005E RID: 94
		[DllImport("ParquetSharpNative")]
		private static extern void ColumnDecryptionProperties_Free(IntPtr properties);

		// Token: 0x0600005F RID: 95
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnDecryptionProperties_Column_Path(IntPtr properties, out IntPtr columnPath);

		// Token: 0x06000060 RID: 96
		[DllImport("ParquetSharpNative")]
		private static extern void ColumnDecryptionProperties_Column_Path_Free(IntPtr columnPath);

		// Token: 0x06000061 RID: 97
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr ColumnDecryptionProperties_Key(IntPtr properties, out AesKey key);

		// Token: 0x0400002D RID: 45
		internal readonly ParquetHandle Handle;
	}
}
