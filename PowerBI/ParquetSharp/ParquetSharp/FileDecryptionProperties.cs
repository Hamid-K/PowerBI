using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000039 RID: 57
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FileDecryptionProperties : IDisposable
	{
		// Token: 0x0600017F RID: 383 RVA: 0x00005AF0 File Offset: 0x00003CF0
		internal FileDecryptionProperties(IntPtr handle)
		{
			this.Handle = new ParquetHandle(handle, new Action<IntPtr>(FileDecryptionProperties.FileDecryptionProperties_Free));
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005B10 File Offset: 0x00003D10
		public void Dispose()
		{
			this.Handle.Dispose();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005B20 File Offset: 0x00003D20
		public byte[] ColumnKey(string columPath)
		{
			byte[] array;
			using (ByteBuffer byteBuffer = new ByteBuffer(0, 0))
			{
				array = ExceptionInfo.Return<IntPtr, AesKey>(this.Handle, StringUtil.ToCStringUtf8(columPath, byteBuffer), new ExceptionInfo.GetFunction<IntPtr, AesKey>(FileDecryptionProperties.FileDecryptionProperties_Column_Key)).ToBytes();
			}
			return array;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00005B80 File Offset: 0x00003D80
		public byte[] FooterKey
		{
			get
			{
				return ExceptionInfo.Return<AesKey>(this.Handle, new ExceptionInfo.GetFunction<AesKey>(FileDecryptionProperties.FileDecryptionProperties_Footer_Key)).ToBytes();
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00005BB0 File Offset: 0x00003DB0
		public string AadPrefix
		{
			get
			{
				return ExceptionInfo.ReturnString(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(FileDecryptionProperties.FileDecryptionProperties_Aad_Prefix), new Action<IntPtr>(FileDecryptionProperties.FileDecryptionProperties_Aad_Prefix_Free));
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00005BD8 File Offset: 0x00003DD8
		public bool CheckPlaintextFooterIntegrity
		{
			get
			{
				return ExceptionInfo.Return<bool>(this.Handle, new ExceptionInfo.GetFunction<bool>(FileDecryptionProperties.FileDecryptionProperties_Check_Plaintext_Footer_Integrity));
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00005BF4 File Offset: 0x00003DF4
		public bool PlaintextFilesAllowed
		{
			get
			{
				return ExceptionInfo.Return<bool>(this.Handle, new ExceptionInfo.GetFunction<bool>(FileDecryptionProperties.FileDecryptionProperties_Plaintext_Files_Allowed));
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00005C10 File Offset: 0x00003E10
		[Nullable(2)]
		public DecryptionKeyRetriever KeyRetriever
		{
			[NullableContext(2)]
			get
			{
				IntPtr intPtr = ExceptionInfo.Return<IntPtr>(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(FileDecryptionProperties.FileDecryptionProperties_Key_Retriever));
				if (!(intPtr == IntPtr.Zero))
				{
					return DecryptionKeyRetriever.GetGcHandleTarget(intPtr);
				}
				return null;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00005C54 File Offset: 0x00003E54
		[Nullable(2)]
		public AadPrefixVerifier AadPrefixVerifier
		{
			[NullableContext(2)]
			get
			{
				IntPtr intPtr = ExceptionInfo.Return<IntPtr>(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(FileDecryptionProperties.FileDecryptionProperties_Aad_Prefix_Verifier));
				if (!(intPtr == IntPtr.Zero))
				{
					return AadPrefixVerifier.GetGcHandleTarget(intPtr);
				}
				return null;
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00005C98 File Offset: 0x00003E98
		public FileDecryptionProperties DeepClone()
		{
			return new FileDecryptionProperties(ExceptionInfo.Return<IntPtr>(this.Handle, new ExceptionInfo.GetFunction<IntPtr>(FileDecryptionProperties.FileDecryptionProperties_Deep_Clone)));
		}

		// Token: 0x06000189 RID: 393
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileDecryptionProperties_Deep_Clone(IntPtr properties, out IntPtr clone);

		// Token: 0x0600018A RID: 394
		[DllImport("ParquetSharpNative")]
		private static extern void FileDecryptionProperties_Free(IntPtr properties);

		// Token: 0x0600018B RID: 395
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileDecryptionProperties_Column_Key(IntPtr properties, IntPtr columnPath, out AesKey columnKey);

		// Token: 0x0600018C RID: 396
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileDecryptionProperties_Footer_Key(IntPtr properties, out AesKey footerKey);

		// Token: 0x0600018D RID: 397
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileDecryptionProperties_Aad_Prefix(IntPtr properties, out IntPtr aadPrefix);

		// Token: 0x0600018E RID: 398
		[DllImport("ParquetSharpNative")]
		private static extern void FileDecryptionProperties_Aad_Prefix_Free(IntPtr aadPrefix);

		// Token: 0x0600018F RID: 399
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileDecryptionProperties_Key_Retriever(IntPtr properties, out IntPtr keyRetriever);

		// Token: 0x06000190 RID: 400
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileDecryptionProperties_Check_Plaintext_Footer_Integrity(IntPtr properties, [MarshalAs(UnmanagedType.I1)] out bool checkPlaintextFooterIntegrity);

		// Token: 0x06000191 RID: 401
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileDecryptionProperties_Plaintext_Files_Allowed(IntPtr properties, [MarshalAs(UnmanagedType.I1)] out bool plaintextFilesAllowed);

		// Token: 0x06000192 RID: 402
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileDecryptionProperties_Aad_Prefix_Verifier(IntPtr properties, out IntPtr aadPrefixVerifier);

		// Token: 0x0400006E RID: 110
		internal readonly ParquetHandle Handle;
	}
}
