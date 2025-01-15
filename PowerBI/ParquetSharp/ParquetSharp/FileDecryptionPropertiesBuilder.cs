using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x0200003A RID: 58
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FileDecryptionPropertiesBuilder : IDisposable
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00005CB8 File Offset: 0x00003EB8
		public FileDecryptionPropertiesBuilder()
		{
			IntPtr intPtr;
			ExceptionInfo.Check(FileDecryptionPropertiesBuilder.FileDecryptionPropertiesBuilder_Create(out intPtr));
			this._handle = new ParquetHandle(intPtr, new Action<IntPtr>(FileDecryptionPropertiesBuilder.FileDecryptionPropertiesBuilder_Free));
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005CF4 File Offset: 0x00003EF4
		public void Dispose()
		{
			this._handle.Dispose();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005D04 File Offset: 0x00003F04
		public FileDecryptionPropertiesBuilder FooterKey(byte[] footerKey)
		{
			AesKey aesKey = new AesKey(footerKey);
			ExceptionInfo.Check(FileDecryptionPropertiesBuilder.FileDecryptionPropertiesBuilder_Footer_Key(this._handle.IntPtr, in aesKey));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005D40 File Offset: 0x00003F40
		public FileDecryptionPropertiesBuilder ColumnKeys(ColumnDecryptionProperties[] columnDecryptionProperties)
		{
			IntPtr[] array = columnDecryptionProperties.Select((ColumnDecryptionProperties p) => p.Handle.IntPtr).ToArray<IntPtr>();
			ExceptionInfo.Check(FileDecryptionPropertiesBuilder.FileDecryptionPropertiesBuilder_Column_Keys(this._handle.IntPtr, array, array.Length));
			GC.KeepAlive(this._handle);
			GC.KeepAlive(columnDecryptionProperties);
			return this;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005DAC File Offset: 0x00003FAC
		public FileDecryptionPropertiesBuilder AadPrefixVerifier(AadPrefixVerifier aadPrefixVerifier)
		{
			IntPtr intPtr = ((aadPrefixVerifier != null) ? aadPrefixVerifier.CreateGcHandle() : IntPtr.Zero);
			try
			{
				ExceptionInfo.Check(FileDecryptionPropertiesBuilder.FileDecryptionPropertiesBuilder_Aad_Prefix_Verifier(this._handle.IntPtr, intPtr, ParquetSharp.AadPrefixVerifier.FreeGcHandleCallback, ParquetSharp.AadPrefixVerifier.VerifyFuncCallback));
			}
			catch
			{
				ParquetSharp.AadPrefixVerifier.FreeGcHandleCallback(intPtr);
				throw;
			}
			GC.KeepAlive(this._handle);
			GC.KeepAlive(aadPrefixVerifier);
			return this;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005E28 File Offset: 0x00004028
		public FileDecryptionPropertiesBuilder KeyRetriever(DecryptionKeyRetriever keyRetriever)
		{
			IntPtr intPtr = ((keyRetriever != null) ? keyRetriever.CreateGcHandle() : IntPtr.Zero);
			try
			{
				ExceptionInfo.Check(FileDecryptionPropertiesBuilder.FileDecryptionPropertiesBuilder_Key_Retriever(this._handle.IntPtr, intPtr, DecryptionKeyRetriever.FreeGcHandleCallback, DecryptionKeyRetriever.GetKeyFuncCallback));
			}
			catch
			{
				DecryptionKeyRetriever.FreeGcHandleCallback(intPtr);
				throw;
			}
			GC.KeepAlive(this._handle);
			GC.KeepAlive(keyRetriever);
			return this;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005EA4 File Offset: 0x000040A4
		public FileDecryptionPropertiesBuilder DisableFooterSignatureVerification()
		{
			ExceptionInfo.Check(FileDecryptionPropertiesBuilder.FileDecryptionPropertiesBuilder_Disable_Footer_Signature_Verification(this._handle.IntPtr));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00005EC8 File Offset: 0x000040C8
		public FileDecryptionPropertiesBuilder AadPrefix(string aadPrefix)
		{
			ExceptionInfo.Check(FileDecryptionPropertiesBuilder.FileDecryptionPropertiesBuilder_Aad_Prefix(this._handle.IntPtr, aadPrefix));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005EEC File Offset: 0x000040EC
		public FileDecryptionPropertiesBuilder PlaintextFilesAllowed()
		{
			ExceptionInfo.Check(FileDecryptionPropertiesBuilder.FileDecryptionPropertiesBuilder_Plaintext_Files_Allowed(this._handle.IntPtr));
			GC.KeepAlive(this._handle);
			return this;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005F10 File Offset: 0x00004110
		public FileDecryptionProperties Build()
		{
			return new FileDecryptionProperties(ExceptionInfo.Return<IntPtr>(this._handle, new ExceptionInfo.GetFunction<IntPtr>(FileDecryptionPropertiesBuilder.FileDecryptionPropertiesBuilder_Build)));
		}

		// Token: 0x0600019D RID: 413
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileDecryptionPropertiesBuilder_Create(out IntPtr builder);

		// Token: 0x0600019E RID: 414
		[DllImport("ParquetSharpNative")]
		private static extern void FileDecryptionPropertiesBuilder_Free(IntPtr builder);

		// Token: 0x0600019F RID: 415
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileDecryptionPropertiesBuilder_Footer_Key(IntPtr builder, in AesKey footerKey);

		// Token: 0x060001A0 RID: 416
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileDecryptionPropertiesBuilder_Column_Keys(IntPtr builder, IntPtr[] columnDecryptionProperties, int numProperties);

		// Token: 0x060001A1 RID: 417
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileDecryptionPropertiesBuilder_Key_Retriever(IntPtr builder, IntPtr gcHandle, DecryptionKeyRetriever.FreeGcHandleFunc freeGcHandle, DecryptionKeyRetriever.GetKeyFunc getKey);

		// Token: 0x060001A2 RID: 418
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileDecryptionPropertiesBuilder_Disable_Footer_Signature_Verification(IntPtr builder);

		// Token: 0x060001A3 RID: 419
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileDecryptionPropertiesBuilder_Aad_Prefix(IntPtr builder, string aadPrefix);

		// Token: 0x060001A4 RID: 420
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileDecryptionPropertiesBuilder_Aad_Prefix_Verifier(IntPtr builder, IntPtr gcHandle, AadPrefixVerifier.FreeGcHandleFunc freeGcHandle, AadPrefixVerifier.VerifyFunc getKey);

		// Token: 0x060001A5 RID: 421
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileDecryptionPropertiesBuilder_Plaintext_Files_Allowed(IntPtr builder);

		// Token: 0x060001A6 RID: 422
		[DllImport("ParquetSharpNative")]
		private static extern IntPtr FileDecryptionPropertiesBuilder_Build(IntPtr builder, out IntPtr properties);

		// Token: 0x0400006F RID: 111
		private readonly ParquetHandle _handle;
	}
}
