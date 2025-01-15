using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.Interop;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Sspi
{
	// Token: 0x0200010C RID: 268
	internal sealed class SecurityContext : Disposable
	{
		// Token: 0x06001000 RID: 4096 RVA: 0x00037BF0 File Offset: 0x00035DF0
		internal SecurityContext(SecurityMode mode, ref SecHandle credentialsHandle, ref SecHandle contextHandle)
		{
			this.Mode = mode;
			this.credentialsHandle = credentialsHandle;
			this.contextHandle = contextHandle;
			credentialsHandle.Reset();
			contextHandle.Reset();
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x00037C23 File Offset: 0x00035E23
		// (set) Token: 0x06001002 RID: 4098 RVA: 0x00037C2B File Offset: 0x00035E2B
		public SecurityMode Mode { get; private set; }

		// Token: 0x06001003 RID: 4099 RVA: 0x00037C34 File Offset: 0x00035E34
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

		// Token: 0x06001004 RID: 4100 RVA: 0x00037CDC File Offset: 0x00035EDC
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

		// Token: 0x06001005 RID: 4101 RVA: 0x00037D98 File Offset: 0x00035F98
		public void EncryptMessage(SecurityBuffer[] buffers, int sequenceNumber)
		{
			SspiHelper.EncryptMessage(ref this.contextHandle, buffers, sequenceNumber);
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00037DA7 File Offset: 0x00035FA7
		public bool DecryptMessage(SecurityBuffer[] buffers, int sequenceNumber, bool isInStreamMode)
		{
			return SspiHelper.DecryptMessage(ref this.contextHandle, buffers, sequenceNumber, isInStreamMode);
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00037DB7 File Offset: 0x00035FB7
		public void MakeSignature(SecurityBuffer[] buffers, int sequenceNumber)
		{
			SspiHelper.MakeSignature(ref this.contextHandle, buffers, sequenceNumber);
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00037DC6 File Offset: 0x00035FC6
		public void VerifySignature(SecurityBuffer[] buffers, int sequenceNumber)
		{
			SspiHelper.VerifySignature(ref this.contextHandle, buffers, sequenceNumber);
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00037DD5 File Offset: 0x00035FD5
		protected override void Dispose(bool disposing)
		{
			SspiHelper.DeleteSecurityContext(ref this.contextHandle);
			SspiHelper.FreeCredentialsHandle(ref this.credentialsHandle);
			base.Dispose(disposing);
		}

		// Token: 0x0400093A RID: 2362
		private SecHandle credentialsHandle;

		// Token: 0x0400093B RID: 2363
		private SecHandle contextHandle;
	}
}
