using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.AdomdClient.Interop;
using Microsoft.AnalysisServices.AdomdClient.Utilities;

namespace Microsoft.AnalysisServices.AdomdClient.Sspi
{
	// Token: 0x02000117 RID: 279
	internal sealed class SecurityContext : Disposable
	{
		// Token: 0x06000F65 RID: 3941 RVA: 0x00034FBC File Offset: 0x000331BC
		internal SecurityContext(SecurityMode mode, ref SecHandle credentialsHandle, ref SecHandle contextHandle)
		{
			this.Mode = mode;
			this.credentialsHandle = credentialsHandle;
			this.contextHandle = contextHandle;
			credentialsHandle.Reset();
			contextHandle.Reset();
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x00034FEF File Offset: 0x000331EF
		// (set) Token: 0x06000F67 RID: 3943 RVA: 0x00034FF7 File Offset: 0x000331F7
		public SecurityMode Mode { get; private set; }

		// Token: 0x06000F68 RID: 3944 RVA: 0x00035000 File Offset: 0x00033200
		public void QueryContextSizes(out int maxTokenLength, out int maxSignatureLength, out int blockSize, out int securityTrailerLength)
		{
			GCHandle gchandle = GCHandle.Alloc(default(SecPkgContext_Sizes), GCHandleType.Pinned);
			try
			{
				int num = NativeMethods.QueryContextAttributes(ref this.contextHandle, 0U, gchandle.AddrOfPinnedObject());
				if (num != 0)
				{
					throw new Win32Exception(num);
				}
				maxTokenLength = (int)((SecPkgContext_Sizes)gchandle.Target).cbMaxToken;
				maxSignatureLength = (int)((SecPkgContext_Sizes)gchandle.Target).cbMaxSignature;
				blockSize = (int)((SecPkgContext_Sizes)gchandle.Target).cbBlockSize;
				securityTrailerLength = (int)((SecPkgContext_Sizes)gchandle.Target).cbSecurityTrailer;
			}
			finally
			{
				gchandle.Free();
			}
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x000350A8 File Offset: 0x000332A8
		public void QueryContextStreamSizes(out int headerLength, out int trailerLength, out int maxMessageLength, out int buffersCount, out int blockSize)
		{
			GCHandle gchandle = GCHandle.Alloc(default(SecPkgContext_StreamSizes), GCHandleType.Pinned);
			try
			{
				int num = NativeMethods.QueryContextAttributes(ref this.contextHandle, 4U, gchandle.AddrOfPinnedObject());
				if (num != 0)
				{
					throw new Win32Exception(num);
				}
				headerLength = (int)((SecPkgContext_StreamSizes)gchandle.Target).cbHeader;
				trailerLength = (int)((SecPkgContext_StreamSizes)gchandle.Target).cbTrailer;
				maxMessageLength = (int)((SecPkgContext_StreamSizes)gchandle.Target).cbMaximumMessage;
				buffersCount = (int)((SecPkgContext_StreamSizes)gchandle.Target).cBuffers;
				blockSize = (int)((SecPkgContext_StreamSizes)gchandle.Target).cbBlockSize;
			}
			finally
			{
				gchandle.Free();
			}
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x00035164 File Offset: 0x00033364
		public void EncryptMessage(SecurityBuffer[] buffers, int sequenceNumber)
		{
			SspiHelper.EncryptMessage(ref this.contextHandle, buffers, sequenceNumber);
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00035173 File Offset: 0x00033373
		public bool DecryptMessage(SecurityBuffer[] buffers, int sequenceNumber, bool isInStreamMode)
		{
			return SspiHelper.DecryptMessage(ref this.contextHandle, buffers, sequenceNumber, isInStreamMode);
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00035183 File Offset: 0x00033383
		public void MakeSignature(SecurityBuffer[] buffers, int sequenceNumber)
		{
			SspiHelper.MakeSignature(ref this.contextHandle, buffers, sequenceNumber);
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x00035192 File Offset: 0x00033392
		public void VerifySignature(SecurityBuffer[] buffers, int sequenceNumber)
		{
			SspiHelper.VerifySignature(ref this.contextHandle, buffers, sequenceNumber);
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x000351A1 File Offset: 0x000333A1
		protected override void Dispose(bool disposing)
		{
			SspiHelper.DeleteSecurityContext(ref this.contextHandle);
			SspiHelper.FreeCredentialsHandle(ref this.credentialsHandle);
			base.Dispose(disposing);
		}

		// Token: 0x04000974 RID: 2420
		private SecHandle credentialsHandle;

		// Token: 0x04000975 RID: 2421
		private SecHandle contextHandle;
	}
}
