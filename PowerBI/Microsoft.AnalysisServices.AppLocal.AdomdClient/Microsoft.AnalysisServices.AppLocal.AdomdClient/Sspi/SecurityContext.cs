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
		// Token: 0x06000F72 RID: 3954 RVA: 0x000352EC File Offset: 0x000334EC
		internal SecurityContext(SecurityMode mode, ref SecHandle credentialsHandle, ref SecHandle contextHandle)
		{
			this.Mode = mode;
			this.credentialsHandle = credentialsHandle;
			this.contextHandle = contextHandle;
			credentialsHandle.Reset();
			contextHandle.Reset();
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x0003531F File Offset: 0x0003351F
		// (set) Token: 0x06000F74 RID: 3956 RVA: 0x00035327 File Offset: 0x00033527
		public SecurityMode Mode { get; private set; }

		// Token: 0x06000F75 RID: 3957 RVA: 0x00035330 File Offset: 0x00033530
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

		// Token: 0x06000F76 RID: 3958 RVA: 0x000353D8 File Offset: 0x000335D8
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

		// Token: 0x06000F77 RID: 3959 RVA: 0x00035494 File Offset: 0x00033694
		public void EncryptMessage(SecurityBuffer[] buffers, int sequenceNumber)
		{
			SspiHelper.EncryptMessage(ref this.contextHandle, buffers, sequenceNumber);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x000354A3 File Offset: 0x000336A3
		public bool DecryptMessage(SecurityBuffer[] buffers, int sequenceNumber, bool isInStreamMode)
		{
			return SspiHelper.DecryptMessage(ref this.contextHandle, buffers, sequenceNumber, isInStreamMode);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x000354B3 File Offset: 0x000336B3
		public void MakeSignature(SecurityBuffer[] buffers, int sequenceNumber)
		{
			SspiHelper.MakeSignature(ref this.contextHandle, buffers, sequenceNumber);
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x000354C2 File Offset: 0x000336C2
		public void VerifySignature(SecurityBuffer[] buffers, int sequenceNumber)
		{
			SspiHelper.VerifySignature(ref this.contextHandle, buffers, sequenceNumber);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x000354D1 File Offset: 0x000336D1
		protected override void Dispose(bool disposing)
		{
			SspiHelper.DeleteSecurityContext(ref this.contextHandle);
			SspiHelper.FreeCredentialsHandle(ref this.credentialsHandle);
			base.Dispose(disposing);
		}

		// Token: 0x04000981 RID: 2433
		private SecHandle credentialsHandle;

		// Token: 0x04000982 RID: 2434
		private SecHandle contextHandle;
	}
}
