using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000CC RID: 204
	internal static class SNINativeMethodWrapper
	{
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x0002DA90 File Offset: 0x0002BC90
		internal static int SniMaxComposedSpnLength
		{
			get
			{
				if (SNINativeMethodWrapper.s_sniMaxComposedSpnLength == -1)
				{
					SNINativeMethodWrapper.s_sniMaxComposedSpnLength = checked((int)SNINativeMethodWrapper.GetSniMaxComposedSpnLength());
				}
				return SNINativeMethodWrapper.s_sniMaxComposedSpnLength;
			}
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0002DAAA File Offset: 0x0002BCAA
		private static AppDomain GetDefaultAppDomainInternal()
		{
			return AppDomain.CurrentDomain;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0002DAB1 File Offset: 0x0002BCB1
		internal static _AppDomain GetDefaultAppDomain()
		{
			return SNINativeMethodWrapper.GetDefaultAppDomainInternal();
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0002DAB8 File Offset: 0x0002BCB8
		internal static byte[] GetData()
		{
			int num;
			IntPtr intPtr = (IntPtr)SNINativeMethodWrapper.SqlDependencyProcessDispatcherStorage.NativeGetData(out num);
			byte[] array = null;
			if (intPtr != IntPtr.Zero)
			{
				array = new byte[num];
				Marshal.Copy(intPtr, array, 0, num);
			}
			return array;
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0002DAF4 File Offset: 0x0002BCF4
		internal unsafe static void SetData(byte[] data)
		{
			fixed (byte* ptr = &data[0])
			{
				byte* ptr2 = ptr;
				SNINativeMethodWrapper.SqlDependencyProcessDispatcherStorage.NativeSetData((void*)ptr2, data.Length);
			}
		}

		// Token: 0x06000E9B RID: 3739
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr LoadLibrary(string dllToLoad);

		// Token: 0x06000E9C RID: 3740
		[DllImport("secur32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern uint QueryContextAttributes(ref SNINativeMethodWrapper.CredHandle contextHandle, [In] SNINativeMethodWrapper.ContextAttribute attribute, [In] IntPtr buffer);

		// Token: 0x06000E9D RID: 3741 RVA: 0x0002DB1C File Offset: 0x0002BD1C
		internal static uint SNIAddProvider(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum ProvNum, [In] ref uint pInfo)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIAddProvider(pConn, ProvNum, ref pInfo);
			case 1:
				return SNINativeManagedWrapperX64.SNIAddProvider(pConn, ProvNum, ref pInfo);
			case 3:
				return SNINativeManagedWrapperARM64.SNIAddProvider(pConn, ProvNum, ref pInfo);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0002DB7C File Offset: 0x0002BD7C
		internal static uint SNIAddProviderWrapper(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum ProvNum, [In] ref SNINativeMethodWrapper.SNICTAIPProviderInfo pInfo)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIAddProviderWrapper(pConn, ProvNum, ref pInfo);
			case 1:
				return SNINativeManagedWrapperX64.SNIAddProviderWrapper(pConn, ProvNum, ref pInfo);
			case 3:
				return SNINativeManagedWrapperARM64.SNIAddProviderWrapper(pConn, ProvNum, ref pInfo);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0002DBDC File Offset: 0x0002BDDC
		internal static uint SNIAddProviderWrapper(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum ProvNum, [In] ref SNINativeMethodWrapper.AuthProviderInfo pInfo)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIAddProviderWrapper(pConn, ProvNum, ref pInfo);
			case 1:
				return SNINativeManagedWrapperX64.SNIAddProviderWrapper(pConn, ProvNum, ref pInfo);
			case 3:
				return SNINativeManagedWrapperARM64.SNIAddProviderWrapper(pConn, ProvNum, ref pInfo);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0002DC3C File Offset: 0x0002BE3C
		internal static uint SNICheckConnection([In] SNIHandle pConn)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNICheckConnection(pConn);
			case 1:
				return SNINativeManagedWrapperX64.SNICheckConnection(pConn);
			case 3:
				return SNINativeManagedWrapperARM64.SNICheckConnection(pConn);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0002DC94 File Offset: 0x0002BE94
		internal static uint SNIClose(IntPtr pConn)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIClose(pConn);
			case 1:
				return SNINativeManagedWrapperX64.SNIClose(pConn);
			case 3:
				return SNINativeManagedWrapperARM64.SNIClose(pConn);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0002DCEC File Offset: 0x0002BEEC
		internal static void SNIGetLastError(out SNINativeMethodWrapper.SNI_Error pErrorStruct)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				SNINativeManagedWrapperX86.SNIGetLastError(out pErrorStruct);
				return;
			case 1:
				SNINativeManagedWrapperX64.SNIGetLastError(out pErrorStruct);
				return;
			case 3:
				SNINativeManagedWrapperARM64.SNIGetLastError(out pErrorStruct);
				return;
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0002DD44 File Offset: 0x0002BF44
		internal static void SNIPacketRelease(IntPtr pPacket)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				SNINativeManagedWrapperX86.SNIPacketRelease(pPacket);
				return;
			case 1:
				SNINativeManagedWrapperX64.SNIPacketRelease(pPacket);
				return;
			case 3:
				SNINativeManagedWrapperARM64.SNIPacketRelease(pPacket);
				return;
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0002DD9C File Offset: 0x0002BF9C
		internal static void SNIPacketReset([In] SNIHandle pConn, SNINativeMethodWrapper.IOType IOType, SNIPacket pPacket, SNINativeMethodWrapper.ConsumerNumber ConsNum)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				SNINativeManagedWrapperX86.SNIPacketReset(pConn, IOType, pPacket, ConsNum);
				return;
			case 1:
				SNINativeManagedWrapperX64.SNIPacketReset(pConn, IOType, pPacket, ConsNum);
				return;
			case 3:
				SNINativeManagedWrapperARM64.SNIPacketReset(pConn, IOType, pPacket, ConsNum);
				return;
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0002DE00 File Offset: 0x0002C000
		internal static uint SNIQueryInfo(SNINativeMethodWrapper.QTypes QType, ref uint pbQInfo)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIQueryInfo(QType, ref pbQInfo);
			case 1:
				return SNINativeManagedWrapperX64.SNIQueryInfo(QType, ref pbQInfo);
			case 3:
				return SNINativeManagedWrapperARM64.SNIQueryInfo(QType, ref pbQInfo);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0002DE5C File Offset: 0x0002C05C
		internal static uint SNIQueryInfo(SNINativeMethodWrapper.QTypes QType, ref IntPtr pbQInfo)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIQueryInfo(QType, ref pbQInfo);
			case 1:
				return SNINativeManagedWrapperX64.SNIQueryInfo(QType, ref pbQInfo);
			case 3:
				return SNINativeManagedWrapperARM64.SNIQueryInfo(QType, ref pbQInfo);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0002DEB8 File Offset: 0x0002C0B8
		internal static uint SNIReadAsync(SNIHandle pConn, ref IntPtr ppNewPacket)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIReadAsync(pConn, ref ppNewPacket);
			case 1:
				return SNINativeManagedWrapperX64.SNIReadAsync(pConn, ref ppNewPacket);
			case 3:
				return SNINativeManagedWrapperARM64.SNIReadAsync(pConn, ref ppNewPacket);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0002DF14 File Offset: 0x0002C114
		internal static uint SNIReadSyncOverAsync(SNIHandle pConn, ref IntPtr ppNewPacket, int timeout)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIReadSyncOverAsync(pConn, ref ppNewPacket, timeout);
			case 1:
				return SNINativeManagedWrapperX64.SNIReadSyncOverAsync(pConn, ref ppNewPacket, timeout);
			case 3:
				return SNINativeManagedWrapperARM64.SNIReadSyncOverAsync(pConn, ref ppNewPacket, timeout);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0002DF74 File Offset: 0x0002C174
		internal static uint SNIRemoveProvider(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum ProvNum)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIRemoveProvider(pConn, ProvNum);
			case 1:
				return SNINativeManagedWrapperX64.SNIRemoveProvider(pConn, ProvNum);
			case 3:
				return SNINativeManagedWrapperARM64.SNIRemoveProvider(pConn, ProvNum);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0002DFD0 File Offset: 0x0002C1D0
		internal static uint SNISecInitPackage(ref uint pcbMaxToken)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNISecInitPackage(ref pcbMaxToken);
			case 1:
				return SNINativeManagedWrapperX64.SNISecInitPackage(ref pcbMaxToken);
			case 3:
				return SNINativeManagedWrapperARM64.SNISecInitPackage(ref pcbMaxToken);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0002E028 File Offset: 0x0002C228
		internal static uint SNISetInfo(SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, [In] ref uint pbQInfo)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNISetInfo(pConn, QType, ref pbQInfo);
			case 1:
				return SNINativeManagedWrapperX64.SNISetInfo(pConn, QType, ref pbQInfo);
			case 3:
				return SNINativeManagedWrapperARM64.SNISetInfo(pConn, QType, ref pbQInfo);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0002E088 File Offset: 0x0002C288
		internal static uint SNITerminate()
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNITerminate();
			case 1:
				return SNINativeManagedWrapperX64.SNITerminate();
			case 3:
				return SNINativeManagedWrapperARM64.SNITerminate();
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0002E0E0 File Offset: 0x0002C2E0
		internal static uint SNIWaitForSSLHandshakeToComplete([In] SNIHandle pConn, int dwMilliseconds, out uint pProtocolVersion)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIWaitForSSLHandshakeToComplete(pConn, dwMilliseconds, out pProtocolVersion);
			case 1:
				return SNINativeManagedWrapperX64.SNIWaitForSSLHandshakeToComplete(pConn, dwMilliseconds, out pProtocolVersion);
			case 3:
				return SNINativeManagedWrapperARM64.SNIWaitForSSLHandshakeToComplete(pConn, dwMilliseconds, out pProtocolVersion);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0002E140 File Offset: 0x0002C340
		internal static uint UnmanagedIsTokenRestricted([In] IntPtr token, [MarshalAs(UnmanagedType.Bool)] out bool isRestricted)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.UnmanagedIsTokenRestricted(token, out isRestricted);
			case 1:
				return SNINativeManagedWrapperX64.UnmanagedIsTokenRestricted(token, out isRestricted);
			case 3:
				return SNINativeManagedWrapperARM64.UnmanagedIsTokenRestricted(token, out isRestricted);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0002E19C File Offset: 0x0002C39C
		private static uint GetSniMaxComposedSpnLength()
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.GetSniMaxComposedSpnLength();
			case 1:
				return SNINativeManagedWrapperX64.GetSniMaxComposedSpnLength();
			case 3:
				return SNINativeManagedWrapperARM64.GetSniMaxComposedSpnLength();
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0002E1F4 File Offset: 0x0002C3F4
		private static uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, out Guid pbQInfo)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIGetInfoWrapper(pConn, QType, out pbQInfo);
			case 1:
				return SNINativeManagedWrapperX64.SNIGetInfoWrapper(pConn, QType, out pbQInfo);
			case 3:
				return SNINativeManagedWrapperARM64.SNIGetInfoWrapper(pConn, QType, out pbQInfo);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0002E254 File Offset: 0x0002C454
		private static uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, [MarshalAs(UnmanagedType.Bool)] out bool pbQInfo)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIGetInfoWrapper(pConn, QType, out pbQInfo);
			case 1:
				return SNINativeManagedWrapperX64.SNIGetInfoWrapper(pConn, QType, out pbQInfo);
			case 3:
				return SNINativeManagedWrapperARM64.SNIGetInfoWrapper(pConn, QType, out pbQInfo);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0002E2B4 File Offset: 0x0002C4B4
		private static uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, ref IntPtr pbQInfo)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIGetInfoWrapper(pConn, QType, ref pbQInfo);
			case 1:
				return SNINativeManagedWrapperX64.SNIGetInfoWrapper(pConn, QType, ref pbQInfo);
			case 3:
				return SNINativeManagedWrapperARM64.SNIGetInfoWrapper(pConn, QType, ref pbQInfo);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0002E314 File Offset: 0x0002C514
		private static uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, out ushort portNum)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIGetInfoWrapper(pConn, QType, out portNum);
			case 1:
				return SNINativeManagedWrapperX64.SNIGetInfoWrapper(pConn, QType, out portNum);
			case 3:
				return SNINativeManagedWrapperARM64.SNIGetInfoWrapper(pConn, QType, out portNum);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0002E374 File Offset: 0x0002C574
		private static uint SNIGetPeerAddrStrWrapper([In] SNIHandle pConn, int bufferSize, StringBuilder addrBuffer, out uint addrLen)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIGetPeerAddrStrWrapper(pConn, bufferSize, addrBuffer, out addrLen);
			case 1:
				return SNINativeManagedWrapperX64.SNIGetPeerAddrStrWrapper(pConn, bufferSize, addrBuffer, out addrLen);
			case 3:
				return SNINativeManagedWrapperARM64.SNIGetPeerAddrStrWrapper(pConn, bufferSize, addrBuffer, out addrLen);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0002E3D8 File Offset: 0x0002C5D8
		private static uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, out SNINativeMethodWrapper.ProviderEnum provNum)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIGetInfoWrapper(pConn, QType, out provNum);
			case 1:
				return SNINativeManagedWrapperX64.SNIGetInfoWrapper(pConn, QType, out provNum);
			case 3:
				return SNINativeManagedWrapperARM64.SNIGetInfoWrapper(pConn, QType, out provNum);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0002E438 File Offset: 0x0002C638
		private static uint SNIInitialize([In] IntPtr pmo)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIInitialize(pmo);
			case 1:
				return SNINativeManagedWrapperX64.SNIInitialize(pmo);
			case 3:
				return SNINativeManagedWrapperARM64.SNIInitialize(pmo);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0002E490 File Offset: 0x0002C690
		private static uint SNIOpenSyncExWrapper(ref SNINativeMethodWrapper.SNI_CLIENT_CONSUMER_INFO pClientConsumerInfo, out IntPtr ppConn)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIOpenSyncExWrapper(ref pClientConsumerInfo, out ppConn);
			case 1:
				return SNINativeManagedWrapperX64.SNIOpenSyncExWrapper(ref pClientConsumerInfo, out ppConn);
			case 3:
				return SNINativeManagedWrapperARM64.SNIOpenSyncExWrapper(ref pClientConsumerInfo, out ppConn);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0002E4EC File Offset: 0x0002C6EC
		private static uint SNIOpenWrapper([In] ref SNINativeMethodWrapper.Sni_Consumer_Info pConsumerInfo, [MarshalAs(UnmanagedType.LPWStr)] string szConnect, [In] SNIHandle pConn, out IntPtr ppConn, [MarshalAs(UnmanagedType.Bool)] bool fSync, SqlConnectionIPAddressPreference ipPreference, [In] ref SNINativeMethodWrapper.SNI_DNSCache_Info pDNSCachedInfo)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIOpenWrapper(ref pConsumerInfo, szConnect, pConn, out ppConn, fSync, ipPreference, ref pDNSCachedInfo);
			case 1:
				return SNINativeManagedWrapperX64.SNIOpenWrapper(ref pConsumerInfo, szConnect, pConn, out ppConn, fSync, ipPreference, ref pDNSCachedInfo);
			case 3:
				return SNINativeManagedWrapperARM64.SNIOpenWrapper(ref pConsumerInfo, szConnect, pConn, out ppConn, fSync, ipPreference, ref pDNSCachedInfo);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0002E560 File Offset: 0x0002C760
		private static IntPtr SNIPacketAllocateWrapper([In] SafeHandle pConn, SNINativeMethodWrapper.IOType IOType)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIPacketAllocateWrapper(pConn, IOType);
			case 1:
				return SNINativeManagedWrapperX64.SNIPacketAllocateWrapper(pConn, IOType);
			case 3:
				return SNINativeManagedWrapperARM64.SNIPacketAllocateWrapper(pConn, IOType);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0002E5BC File Offset: 0x0002C7BC
		private static uint SNIPacketGetDataWrapper([In] IntPtr packet, [In] [Out] byte[] readBuffer, uint readBufferLength, out uint dataSize)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIPacketGetDataWrapper(packet, readBuffer, readBufferLength, out dataSize);
			case 1:
				return SNINativeManagedWrapperX64.SNIPacketGetDataWrapper(packet, readBuffer, readBufferLength, out dataSize);
			case 3:
				return SNINativeManagedWrapperARM64.SNIPacketGetDataWrapper(packet, readBuffer, readBufferLength, out dataSize);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0002E620 File Offset: 0x0002C820
		private unsafe static void SNIPacketSetData(SNIPacket pPacket, [In] byte* pbBuf, uint cbBuf)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				SNINativeManagedWrapperX86.SNIPacketSetData(pPacket, pbBuf, cbBuf);
				return;
			case 1:
				SNINativeManagedWrapperX64.SNIPacketSetData(pPacket, pbBuf, cbBuf);
				return;
			case 3:
				SNINativeManagedWrapperARM64.SNIPacketSetData(pPacket, pbBuf, cbBuf);
				return;
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0002E680 File Offset: 0x0002C880
		private unsafe static uint SNISecGenClientContextWrapper([In] SNIHandle pConn, [In] [Out] byte[] pIn, uint cbIn, [In] [Out] byte[] pOut, [In] ref uint pcbOut, [MarshalAs(UnmanagedType.Bool)] out bool pfDone, byte* szServerInfo, uint cbServerInfo, [MarshalAs(UnmanagedType.LPWStr)] string pwszUserName, [MarshalAs(UnmanagedType.LPWStr)] string pwszPassword)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNISecGenClientContextWrapper(pConn, pIn, cbIn, pOut, ref pcbOut, out pfDone, szServerInfo, cbServerInfo, pwszUserName, pwszPassword);
			case 1:
				return SNINativeManagedWrapperX64.SNISecGenClientContextWrapper(pConn, pIn, cbIn, pOut, ref pcbOut, out pfDone, szServerInfo, cbServerInfo, pwszUserName, pwszPassword);
			case 3:
				return SNINativeManagedWrapperARM64.SNISecGenClientContextWrapper(pConn, pIn, cbIn, pOut, ref pcbOut, out pfDone, szServerInfo, cbServerInfo, pwszUserName, pwszPassword);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0002E708 File Offset: 0x0002C908
		private static uint SNIWriteAsyncWrapper(SNIHandle pConn, [In] SNIPacket pPacket)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIWriteAsyncWrapper(pConn, pPacket);
			case 1:
				return SNINativeManagedWrapperX64.SNIWriteAsyncWrapper(pConn, pPacket);
			case 3:
				return SNINativeManagedWrapperARM64.SNIWriteAsyncWrapper(pConn, pPacket);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0002E764 File Offset: 0x0002C964
		private static uint SNIWriteSyncOverAsync(SNIHandle pConn, [In] SNIPacket pPacket)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIWriteSyncOverAsync(pConn, pPacket);
			case 1:
				return SNINativeManagedWrapperX64.SNIWriteSyncOverAsync(pConn, pPacket);
			case 3:
				return SNINativeManagedWrapperARM64.SNIWriteSyncOverAsync(pConn, pPacket);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0002E7C0 File Offset: 0x0002C9C0
		private static IntPtr SNIClientCertificateFallbackWrapper(IntPtr pCallbackContext)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIClientCertificateFallbackWrapper(pCallbackContext);
			case 1:
				return SNINativeManagedWrapperX64.SNIClientCertificateFallbackWrapper(pCallbackContext);
			case 3:
				return SNINativeManagedWrapperARM64.SNIClientCertificateFallbackWrapper(pCallbackContext);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0002E818 File Offset: 0x0002CA18
		internal static uint SNISecGetServerCertificate(SNIHandle pConnectionObject, ref X509Certificate2 certificate)
		{
			X509Certificate x509Certificate = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			uint num;
			try
			{
				pConnectionObject.DangerousAddRef(ref flag);
				IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf<SNINativeMethodWrapper.CredHandle>());
				num = SNINativeMethodWrapper.SNIGetInfoWrapper(pConnectionObject, SNINativeMethodWrapper.QTypes.SNI_QUERY_CONN_SSL_SECCTXTHANDLE, ref intPtr);
				if (num == 0U)
				{
					SNINativeMethodWrapper.CredHandle credHandle = Marshal.PtrToStructure<SNINativeMethodWrapper.CredHandle>(intPtr);
					if ((num = SNINativeMethodWrapper.QueryContextAttributes(ref credHandle, SNINativeMethodWrapper.ContextAttribute.SECPKG_ATTR_REMOTE_CERT_CONTEXT, x509Certificate.Handle)) == 0U)
					{
						certificate = new X509Certificate2(x509Certificate.Handle);
					}
				}
				Marshal.FreeHGlobal(intPtr);
			}
			finally
			{
				if (x509Certificate != null)
				{
					x509Certificate.Dispose();
				}
				if (flag)
				{
					pConnectionObject.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0002E8A8 File Offset: 0x0002CAA8
		internal static uint SniGetConnectionId(SNIHandle pConn, ref Guid connId)
		{
			return SNINativeMethodWrapper.SNIGetInfoWrapper(pConn, SNINativeMethodWrapper.QTypes.SNI_QUERY_CONN_CONNID, out connId);
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0002E8B3 File Offset: 0x0002CAB3
		internal static uint SniGetProviderNumber(SNIHandle pConn, ref SNINativeMethodWrapper.ProviderEnum provNum)
		{
			return SNINativeMethodWrapper.SNIGetInfoWrapper(pConn, SNINativeMethodWrapper.QTypes.SNI_QUERY_CONN_PROVIDERNUM, out provNum);
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0002E8BD File Offset: 0x0002CABD
		internal static uint SniGetConnectionPort(SNIHandle pConn, ref ushort portNum)
		{
			return SNINativeMethodWrapper.SNIGetInfoWrapper(pConn, SNINativeMethodWrapper.QTypes.SNI_QUERY_CONN_PEERPORT, out portNum);
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0002E8C8 File Offset: 0x0002CAC8
		internal static uint SniGetConnectionIPString(SNIHandle pConn, ref string connIPStr)
		{
			uint num = 0U;
			int num2 = 48;
			StringBuilder stringBuilder = new StringBuilder(num2);
			uint num3 = SNINativeMethodWrapper.SNIGetPeerAddrStrWrapper(pConn, num2, stringBuilder, out num);
			connIPStr = stringBuilder.ToString(0, Convert.ToInt32(num));
			return num3;
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0002E901 File Offset: 0x0002CB01
		internal static uint SNIInitialize()
		{
			return SNINativeMethodWrapper.SNIInitialize(IntPtr.Zero);
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0002E910 File Offset: 0x0002CB10
		internal static IntPtr SNIServerEnumOpen()
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIServerEnumOpen();
			case 1:
				return SNINativeManagedWrapperX64.SNIServerEnumOpen();
			case 3:
				return SNINativeManagedWrapperARM64.SNIServerEnumOpen();
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0002E968 File Offset: 0x0002CB68
		internal static int SNIServerEnumRead([In] IntPtr packet, [In] [Out] char[] readbuffer, int bufferLength, out bool more)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				return SNINativeManagedWrapperX86.SNIServerEnumRead(packet, readbuffer, bufferLength, out more);
			case 1:
				return SNINativeManagedWrapperX64.SNIServerEnumRead(packet, readbuffer, bufferLength, out more);
			case 3:
				return SNINativeManagedWrapperARM64.SNIServerEnumRead(packet, readbuffer, bufferLength, out more);
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0002E9CC File Offset: 0x0002CBCC
		internal static void SNIServerEnumClose([In] IntPtr packet)
		{
			switch (SNINativeMethodWrapper.s_architecture)
			{
			case 0:
				SNINativeManagedWrapperX86.SNIServerEnumClose(packet);
				return;
			case 1:
				SNINativeManagedWrapperX64.SNIServerEnumClose(packet);
				return;
			case 3:
				SNINativeManagedWrapperARM64.SNIServerEnumClose(packet);
				return;
			}
			throw ADP.SNIPlatformNotSupported(SNINativeMethodWrapper.s_architecture.ToString());
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0002EA24 File Offset: 0x0002CC24
		internal static uint SNIOpenMarsSession(SNINativeMethodWrapper.ConsumerInfo consumerInfo, SNIHandle parent, ref IntPtr pConn, bool fSync, SqlConnectionIPAddressPreference ipPreference, SQLDNSInfo cachedDNSInfo)
		{
			SNINativeMethodWrapper.Sni_Consumer_Info sni_Consumer_Info = default(SNINativeMethodWrapper.Sni_Consumer_Info);
			SNINativeMethodWrapper.MarshalConsumerInfo(consumerInfo, ref sni_Consumer_Info);
			SNINativeMethodWrapper.SNI_DNSCache_Info sni_DNSCache_Info = default(SNINativeMethodWrapper.SNI_DNSCache_Info);
			sni_DNSCache_Info.wszCachedFQDN = ((cachedDNSInfo != null) ? cachedDNSInfo.FQDN : null);
			sni_DNSCache_Info.wszCachedTcpIPv4 = ((cachedDNSInfo != null) ? cachedDNSInfo.AddrIPv4 : null);
			sni_DNSCache_Info.wszCachedTcpIPv6 = ((cachedDNSInfo != null) ? cachedDNSInfo.AddrIPv6 : null);
			sni_DNSCache_Info.wszCachedTcpPort = ((cachedDNSInfo != null) ? cachedDNSInfo.Port : null);
			return SNINativeMethodWrapper.SNIOpenWrapper(ref sni_Consumer_Info, "session:", parent, out pConn, fSync, ipPreference, ref sni_DNSCache_Info);
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0002EAB0 File Offset: 0x0002CCB0
		internal unsafe static uint SNIOpenSyncEx(SNINativeMethodWrapper.ConsumerInfo consumerInfo, string constring, ref IntPtr pConn, byte[] spnBuffer, byte[] instanceName, bool fOverrideCache, bool fSync, int timeout, bool fParallel, int transparentNetworkResolutionStateNo, int totalTimeout, bool isAzureSqlServerEndpoint, SqlConnectionIPAddressPreference ipPreference, SQLDNSInfo cachedDNSInfo, string hostNameInCertificate)
		{
			fixed (byte* ptr = &instanceName[0])
			{
				byte* ptr2 = ptr;
				SNINativeMethodWrapper.SNI_CLIENT_CONSUMER_INFO sni_CLIENT_CONSUMER_INFO = default(SNINativeMethodWrapper.SNI_CLIENT_CONSUMER_INFO);
				SNINativeMethodWrapper.MarshalConsumerInfo(consumerInfo, ref sni_CLIENT_CONSUMER_INFO.ConsumerInfo);
				sni_CLIENT_CONSUMER_INFO.wszConnectionString = constring;
				sni_CLIENT_CONSUMER_INFO.HostNameInCertificate = hostNameInCertificate;
				sni_CLIENT_CONSUMER_INFO.networkLibrary = SNINativeMethodWrapper.PrefixEnum.UNKNOWN_PREFIX;
				sni_CLIENT_CONSUMER_INFO.szInstanceName = ptr2;
				sni_CLIENT_CONSUMER_INFO.cchInstanceName = (uint)instanceName.Length;
				sni_CLIENT_CONSUMER_INFO.fOverrideLastConnectCache = fOverrideCache;
				sni_CLIENT_CONSUMER_INFO.fSynchronousConnection = fSync;
				sni_CLIENT_CONSUMER_INFO.timeout = timeout;
				sni_CLIENT_CONSUMER_INFO.fParallel = fParallel;
				sni_CLIENT_CONSUMER_INFO.isAzureSqlServerEndpoint = ADP.IsAzureSqlServerEndpoint(constring);
				switch (transparentNetworkResolutionStateNo)
				{
				case 0:
					sni_CLIENT_CONSUMER_INFO.transparentNetworkResolution = SNINativeMethodWrapper.TransparentNetworkResolutionMode.DisabledMode;
					break;
				case 1:
					sni_CLIENT_CONSUMER_INFO.transparentNetworkResolution = SNINativeMethodWrapper.TransparentNetworkResolutionMode.SequentialMode;
					break;
				case 2:
					sni_CLIENT_CONSUMER_INFO.transparentNetworkResolution = SNINativeMethodWrapper.TransparentNetworkResolutionMode.ParallelMode;
					break;
				}
				sni_CLIENT_CONSUMER_INFO.totalTimeout = totalTimeout;
				sni_CLIENT_CONSUMER_INFO.ipAddressPreference = ipPreference;
				sni_CLIENT_CONSUMER_INFO.DNSCacheInfo.wszCachedFQDN = ((cachedDNSInfo != null) ? cachedDNSInfo.FQDN : null);
				sni_CLIENT_CONSUMER_INFO.DNSCacheInfo.wszCachedTcpIPv4 = ((cachedDNSInfo != null) ? cachedDNSInfo.AddrIPv4 : null);
				sni_CLIENT_CONSUMER_INFO.DNSCacheInfo.wszCachedTcpIPv6 = ((cachedDNSInfo != null) ? cachedDNSInfo.AddrIPv6 : null);
				sni_CLIENT_CONSUMER_INFO.DNSCacheInfo.wszCachedTcpPort = ((cachedDNSInfo != null) ? cachedDNSInfo.Port : null);
				if (spnBuffer != null)
				{
					fixed (byte* ptr3 = &spnBuffer[0])
					{
						byte* ptr4 = ptr3;
						sni_CLIENT_CONSUMER_INFO.szSPN = ptr4;
						sni_CLIENT_CONSUMER_INFO.cchSPN = (uint)spnBuffer.Length;
						return SNINativeMethodWrapper.SNIOpenSyncExWrapper(ref sni_CLIENT_CONSUMER_INFO, out pConn);
					}
				}
				return SNINativeMethodWrapper.SNIOpenSyncExWrapper(ref sni_CLIENT_CONSUMER_INFO, out pConn);
			}
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0002EC1C File Offset: 0x0002CE1C
		internal static uint SNIAddProvider(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum providerEnum, SNINativeMethodWrapper.AuthProviderInfo authInfo)
		{
			uint num = 0U;
			if (authInfo.clientCertificateCallback != null)
			{
				SNINativeMethodWrapper.SNIAuthProviderInfoWrapper sniauthProviderInfoWrapper;
				sniauthProviderInfoWrapper.pDelegateContext = authInfo.clientCertificateCallbackContext;
				sniauthProviderInfoWrapper.pSqlClientCertificateDelegate = authInfo.clientCertificateCallback;
				authInfo.clientCertificateCallbackContext = sniauthProviderInfoWrapper;
				authInfo.clientCertificateCallback = new SNINativeMethodWrapper.SqlClientCertificateDelegate(SNINativeMethodWrapper.SNIClientCertificateFallbackWrapper);
			}
			uint num2 = SNINativeMethodWrapper.SNIAddProviderWrapper(pConn, providerEnum, ref authInfo);
			if (num2 == num)
			{
				bool flag;
				num2 = SNINativeMethodWrapper.SNIGetInfoWrapper(pConn, SNINativeMethodWrapper.QTypes.SNI_QUERY_CONN_SUPPORTS_SYNC_OVER_ASYNC, out flag);
			}
			return num2;
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0002EC88 File Offset: 0x0002CE88
		internal static uint SNIAddProvider(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum providerEnum, SNINativeMethodWrapper.CTAIPProviderInfo authInfo)
		{
			uint num = 0U;
			SNINativeMethodWrapper.SNICTAIPProviderInfo snictaipproviderInfo = default(SNINativeMethodWrapper.SNICTAIPProviderInfo);
			snictaipproviderInfo.prgbAddress = authInfo.originalNetworkAddress[0];
			snictaipproviderInfo.cbAddress = (ulong)((byte)authInfo.originalNetworkAddress.Length);
			snictaipproviderInfo.fFromDataSecurityProxy = authInfo.fromDataSecurityProxy;
			uint num2 = SNINativeMethodWrapper.SNIAddProviderWrapper(pConn, providerEnum, ref snictaipproviderInfo);
			if (num2 == num)
			{
				bool flag;
				num2 = SNINativeMethodWrapper.SNIGetInfoWrapper(pConn, SNINativeMethodWrapper.QTypes.SNI_QUERY_CONN_SUPPORTS_SYNC_OVER_ASYNC, out flag);
			}
			return num2;
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0002ECE6 File Offset: 0x0002CEE6
		internal static void SNIPacketAllocate(SafeHandle pConn, SNINativeMethodWrapper.IOType IOType, ref IntPtr pPacket)
		{
			pPacket = SNINativeMethodWrapper.SNIPacketAllocateWrapper(pConn, IOType);
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0002ECF1 File Offset: 0x0002CEF1
		internal static uint SNIPacketGetData(IntPtr packet, byte[] readBuffer, ref uint dataSize)
		{
			return SNINativeMethodWrapper.SNIPacketGetDataWrapper(packet, readBuffer, (uint)readBuffer.Length, out dataSize);
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0002ED00 File Offset: 0x0002CF00
		internal unsafe static void SNIPacketSetData(SNIPacket packet, byte[] data, int length)
		{
			fixed (byte* ptr = &data[0])
			{
				byte* ptr2 = ptr;
				SNINativeMethodWrapper.SNIPacketSetData(packet, ptr2, (uint)length);
			}
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0002ED24 File Offset: 0x0002CF24
		internal unsafe static void SNIPacketSetData(SNIPacket packet, byte[] data, int length, SecureString[] passwords, int[] passwordOffsets)
		{
			bool flag = false;
			bool flag2 = false;
			IntPtr intPtr = IntPtr.Zero;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				try
				{
					fixed (byte* ptr = &data[0])
					{
						byte* ptr2 = ptr;
					}
				}
				finally
				{
					byte* ptr = null;
				}
				if (passwords != null)
				{
					for (int i = 0; i < passwords.Length; i++)
					{
						if (passwords[i] != null)
						{
							RuntimeHelpers.PrepareConstrainedRegions();
							try
							{
								intPtr = Marshal.SecureStringToCoTaskMemUnicode(passwords[i]);
								char* ptr3 = (char*)intPtr.ToPointer();
								byte* ptr4 = (byte*)intPtr.ToPointer();
								int length2 = passwords[i].Length;
								for (int j = 0; j < length2; j++)
								{
									int num = (int)(*ptr3);
									byte b = (byte)(num & 255);
									byte b2 = (byte)((num >> 8) & 255);
									*(ptr4++) = (byte)((((int)(b & 15) << 4) | (b >> 4)) ^ 165);
									*(ptr4++) = (byte)((((int)(b2 & 15) << 4) | (b2 >> 4)) ^ 165);
									ptr3++;
								}
								flag2 = true;
								Marshal.Copy(intPtr, data, passwordOffsets[i], length2 * 2);
							}
							finally
							{
								if (intPtr != IntPtr.Zero)
								{
									Marshal.ZeroFreeCoTaskMemUnicode(intPtr);
								}
							}
						}
					}
				}
				packet.DangerousAddRef(ref flag);
				try
				{
					fixed (byte* ptr5 = &data[0])
					{
						byte* ptr6 = ptr5;
						SNINativeMethodWrapper.SNIPacketSetData(packet, ptr6, (uint)length);
					}
				}
				finally
				{
					byte* ptr5 = null;
				}
			}
			finally
			{
				if (flag)
				{
					packet.DangerousRelease();
				}
				if (flag2)
				{
					for (int k = 0; k < data.Length; k++)
					{
						data[k] = 0;
					}
				}
			}
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0002EEE8 File Offset: 0x0002D0E8
		internal unsafe static uint SNISecGenClientContext(SNIHandle pConnectionObject, byte[] inBuff, uint receivedLength, byte[] OutBuff, ref uint sendLength, byte[] serverUserName)
		{
			fixed (byte* ptr = &serverUserName[0])
			{
				byte* ptr2 = ptr;
				bool flag;
				return SNINativeMethodWrapper.SNISecGenClientContextWrapper(pConnectionObject, inBuff, receivedLength, OutBuff, ref sendLength, out flag, ptr2, (uint)serverUserName.Length, null, null);
			}
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0002EF15 File Offset: 0x0002D115
		internal static uint SNIWritePacket(SNIHandle pConn, SNIPacket packet, bool sync)
		{
			if (sync)
			{
				return SNINativeMethodWrapper.SNIWriteSyncOverAsync(pConn, packet);
			}
			return SNINativeMethodWrapper.SNIWriteAsyncWrapper(pConn, packet);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0002EF2C File Offset: 0x0002D12C
		private static void MarshalConsumerInfo(SNINativeMethodWrapper.ConsumerInfo consumerInfo, ref SNINativeMethodWrapper.Sni_Consumer_Info native_consumerInfo)
		{
			native_consumerInfo.DefaultUserDataLength = consumerInfo.defaultBufferSize;
			native_consumerInfo.fnReadComp = ((consumerInfo.readDelegate != null) ? Marshal.GetFunctionPointerForDelegate<SNINativeMethodWrapper.SqlAsyncCallbackDelegate>(consumerInfo.readDelegate) : IntPtr.Zero);
			native_consumerInfo.fnWriteComp = ((consumerInfo.writeDelegate != null) ? Marshal.GetFunctionPointerForDelegate<SNINativeMethodWrapper.SqlAsyncCallbackDelegate>(consumerInfo.writeDelegate) : IntPtr.Zero);
			native_consumerInfo.ConsumerKey = consumerInfo.key;
		}

		// Token: 0x04000633 RID: 1587
		private static int s_sniMaxComposedSpnLength = -1;

		// Token: 0x04000634 RID: 1588
		private static readonly Architecture s_architecture = RuntimeInformation.ProcessArchitecture;

		// Token: 0x04000635 RID: 1589
		private const int SniOpenTimeOut = -1;

		// Token: 0x04000636 RID: 1590
		internal const int ConnTerminatedError = 2;

		// Token: 0x04000637 RID: 1591
		internal const int InvalidParameterError = 5;

		// Token: 0x04000638 RID: 1592
		internal const int ProtocolNotSupportedError = 8;

		// Token: 0x04000639 RID: 1593
		internal const int ConnTimeoutError = 11;

		// Token: 0x0400063A RID: 1594
		internal const int ConnNotUsableError = 19;

		// Token: 0x0400063B RID: 1595
		internal const int InvalidConnStringError = 25;

		// Token: 0x0400063C RID: 1596
		internal const int HandshakeFailureError = 31;

		// Token: 0x0400063D RID: 1597
		internal const int InternalExceptionError = 35;

		// Token: 0x0400063E RID: 1598
		internal const int ConnOpenFailedError = 40;

		// Token: 0x0400063F RID: 1599
		internal const int ErrorSpnLookup = 44;

		// Token: 0x04000640 RID: 1600
		internal const int LocalDBErrorCode = 50;

		// Token: 0x04000641 RID: 1601
		internal const int MultiSubnetFailoverWithMoreThan64IPs = 47;

		// Token: 0x04000642 RID: 1602
		internal const int MultiSubnetFailoverWithInstanceSpecified = 48;

		// Token: 0x04000643 RID: 1603
		internal const int MultiSubnetFailoverWithNonTcpProtocol = 49;

		// Token: 0x04000644 RID: 1604
		internal const int MaxErrorValue = 50157;

		// Token: 0x04000645 RID: 1605
		internal const int LocalDBNoInstanceName = 51;

		// Token: 0x04000646 RID: 1606
		internal const int LocalDBNoInstallation = 52;

		// Token: 0x04000647 RID: 1607
		internal const int LocalDBInvalidConfig = 53;

		// Token: 0x04000648 RID: 1608
		internal const int LocalDBNoSqlUserInstanceDllPath = 54;

		// Token: 0x04000649 RID: 1609
		internal const int LocalDBInvalidSqlUserInstanceDllPath = 55;

		// Token: 0x0400064A RID: 1610
		internal const int LocalDBFailedToLoadDll = 56;

		// Token: 0x0400064B RID: 1611
		internal const int LocalDBBadRuntime = 57;

		// Token: 0x0400064C RID: 1612
		internal const int SniIP6AddrStringBufferLength = 48;

		// Token: 0x020001EF RID: 495
		// (Invoke) Token: 0x06001DFD RID: 7677
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		internal delegate void SqlAsyncCallbackDelegate(IntPtr m_ConsKey, IntPtr pPacket, uint dwError);

		// Token: 0x020001F0 RID: 496
		// (Invoke) Token: 0x06001E01 RID: 7681
		internal delegate IntPtr SqlClientCertificateDelegate(IntPtr pCallbackContext);

		// Token: 0x020001F1 RID: 497
		internal class SqlDependencyProcessDispatcherStorage
		{
			// Token: 0x06001E04 RID: 7684 RVA: 0x0007B9DF File Offset: 0x00079BDF
			public unsafe static void* NativeGetData(out int passedSize)
			{
				passedSize = SNINativeMethodWrapper.SqlDependencyProcessDispatcherStorage.size;
				return SNINativeMethodWrapper.SqlDependencyProcessDispatcherStorage.data;
			}

			// Token: 0x06001E05 RID: 7685 RVA: 0x0007B9F0 File Offset: 0x00079BF0
			internal unsafe static bool NativeSetData(void* passedData, int passedSize)
			{
				bool flag = false;
				while (Interlocked.CompareExchange(ref SNINativeMethodWrapper.SqlDependencyProcessDispatcherStorage.thelock, 1, 0) != 0)
				{
					Thread.Sleep(50);
				}
				Trace.Assert(1 == SNINativeMethodWrapper.SqlDependencyProcessDispatcherStorage.thelock);
				if (null == SNINativeMethodWrapper.SqlDependencyProcessDispatcherStorage.data)
				{
					SNINativeMethodWrapper.SqlDependencyProcessDispatcherStorage.data = Marshal.AllocHGlobal(passedSize).ToPointer();
					Trace.Assert(null != SNINativeMethodWrapper.SqlDependencyProcessDispatcherStorage.data);
					Buffer.MemoryCopy(passedData, SNINativeMethodWrapper.SqlDependencyProcessDispatcherStorage.data, (long)passedSize, (long)passedSize);
					Trace.Assert(SNINativeMethodWrapper.SqlDependencyProcessDispatcherStorage.size == 0);
					SNINativeMethodWrapper.SqlDependencyProcessDispatcherStorage.size = passedSize;
					flag = true;
				}
				int num = Interlocked.CompareExchange(ref SNINativeMethodWrapper.SqlDependencyProcessDispatcherStorage.thelock, 0, 1);
				Trace.Assert(1 == num);
				return flag;
			}

			// Token: 0x040014B1 RID: 5297
			private unsafe static void* data;

			// Token: 0x040014B2 RID: 5298
			private static int size;

			// Token: 0x040014B3 RID: 5299
			private static volatile int thelock;
		}

		// Token: 0x020001F2 RID: 498
		internal enum SniSpecialErrors : uint
		{
			// Token: 0x040014B5 RID: 5301
			LocalDBErrorCode = 50U,
			// Token: 0x040014B6 RID: 5302
			MultiSubnetFailoverWithMoreThan64IPs = 47U,
			// Token: 0x040014B7 RID: 5303
			MultiSubnetFailoverWithInstanceSpecified,
			// Token: 0x040014B8 RID: 5304
			MultiSubnetFailoverWithNonTcpProtocol,
			// Token: 0x040014B9 RID: 5305
			MaxErrorValue = 50157U
		}

		// Token: 0x020001F3 RID: 499
		internal struct ConsumerInfo
		{
			// Token: 0x040014BA RID: 5306
			internal int defaultBufferSize;

			// Token: 0x040014BB RID: 5307
			internal SNINativeMethodWrapper.SqlAsyncCallbackDelegate readDelegate;

			// Token: 0x040014BC RID: 5308
			internal SNINativeMethodWrapper.SqlAsyncCallbackDelegate writeDelegate;

			// Token: 0x040014BD RID: 5309
			internal IntPtr key;
		}

		// Token: 0x020001F4 RID: 500
		internal struct AuthProviderInfo
		{
			// Token: 0x040014BE RID: 5310
			public uint flags;

			// Token: 0x040014BF RID: 5311
			[MarshalAs(UnmanagedType.Bool)]
			public bool tlsFirst;

			// Token: 0x040014C0 RID: 5312
			public object certContext;

			// Token: 0x040014C1 RID: 5313
			[MarshalAs(UnmanagedType.LPWStr)]
			public string certId;

			// Token: 0x040014C2 RID: 5314
			[MarshalAs(UnmanagedType.Bool)]
			public bool certHash;

			// Token: 0x040014C3 RID: 5315
			public object clientCertificateCallbackContext;

			// Token: 0x040014C4 RID: 5316
			public SNINativeMethodWrapper.SqlClientCertificateDelegate clientCertificateCallback;

			// Token: 0x040014C5 RID: 5317
			[MarshalAs(UnmanagedType.LPWStr)]
			public string serverCertFileName;
		}

		// Token: 0x020001F5 RID: 501
		internal struct CTAIPProviderInfo
		{
			// Token: 0x040014C6 RID: 5318
			internal byte[] originalNetworkAddress;

			// Token: 0x040014C7 RID: 5319
			internal bool fromDataSecurityProxy;
		}

		// Token: 0x020001F6 RID: 502
		private struct SNIAuthProviderInfoWrapper
		{
			// Token: 0x040014C8 RID: 5320
			internal object pDelegateContext;

			// Token: 0x040014C9 RID: 5321
			internal SNINativeMethodWrapper.SqlClientCertificateDelegate pSqlClientCertificateDelegate;
		}

		// Token: 0x020001F7 RID: 503
		internal struct SNICTAIPProviderInfo
		{
			// Token: 0x040014CA RID: 5322
			internal SNIHandle pConn;

			// Token: 0x040014CB RID: 5323
			internal byte prgbAddress;

			// Token: 0x040014CC RID: 5324
			internal ulong cbAddress;

			// Token: 0x040014CD RID: 5325
			internal bool fFromDataSecurityProxy;
		}

		// Token: 0x020001F8 RID: 504
		internal struct CredHandle
		{
			// Token: 0x040014CE RID: 5326
			internal IntPtr dwLower;

			// Token: 0x040014CF RID: 5327
			internal IntPtr dwUpper;
		}

		// Token: 0x020001F9 RID: 505
		internal enum ContextAttribute
		{
			// Token: 0x040014D1 RID: 5329
			SECPKG_ATTR_SIZES,
			// Token: 0x040014D2 RID: 5330
			SECPKG_ATTR_NAMES,
			// Token: 0x040014D3 RID: 5331
			SECPKG_ATTR_LIFESPAN,
			// Token: 0x040014D4 RID: 5332
			SECPKG_ATTR_DCE_INFO,
			// Token: 0x040014D5 RID: 5333
			SECPKG_ATTR_STREAM_SIZES,
			// Token: 0x040014D6 RID: 5334
			SECPKG_ATTR_AUTHORITY = 6,
			// Token: 0x040014D7 RID: 5335
			SECPKG_ATTR_PACKAGE_INFO = 10,
			// Token: 0x040014D8 RID: 5336
			SECPKG_ATTR_NEGOTIATION_INFO = 12,
			// Token: 0x040014D9 RID: 5337
			SECPKG_ATTR_UNIQUE_BINDINGS = 25,
			// Token: 0x040014DA RID: 5338
			SECPKG_ATTR_ENDPOINT_BINDINGS,
			// Token: 0x040014DB RID: 5339
			SECPKG_ATTR_CLIENT_SPECIFIED_TARGET,
			// Token: 0x040014DC RID: 5340
			SECPKG_ATTR_APPLICATION_PROTOCOL = 35,
			// Token: 0x040014DD RID: 5341
			SECPKG_ATTR_REMOTE_CERT_CONTEXT = 83,
			// Token: 0x040014DE RID: 5342
			SECPKG_ATTR_LOCAL_CERT_CONTEXT,
			// Token: 0x040014DF RID: 5343
			SECPKG_ATTR_ROOT_STORE,
			// Token: 0x040014E0 RID: 5344
			SECPKG_ATTR_ISSUER_LIST_EX = 89,
			// Token: 0x040014E1 RID: 5345
			SECPKG_ATTR_CONNECTION_INFO,
			// Token: 0x040014E2 RID: 5346
			SECPKG_ATTR_UI_INFO = 104
		}

		// Token: 0x020001FA RID: 506
		internal enum ConsumerNumber
		{
			// Token: 0x040014E4 RID: 5348
			SNI_Consumer_SNI,
			// Token: 0x040014E5 RID: 5349
			SNI_Consumer_SSB,
			// Token: 0x040014E6 RID: 5350
			SNI_Consumer_PacketIsReleased,
			// Token: 0x040014E7 RID: 5351
			SNI_Consumer_Invalid
		}

		// Token: 0x020001FB RID: 507
		internal enum IOType
		{
			// Token: 0x040014E9 RID: 5353
			READ,
			// Token: 0x040014EA RID: 5354
			WRITE
		}

		// Token: 0x020001FC RID: 508
		internal enum PrefixEnum
		{
			// Token: 0x040014EC RID: 5356
			UNKNOWN_PREFIX,
			// Token: 0x040014ED RID: 5357
			SM_PREFIX,
			// Token: 0x040014EE RID: 5358
			TCP_PREFIX,
			// Token: 0x040014EF RID: 5359
			NP_PREFIX,
			// Token: 0x040014F0 RID: 5360
			VIA_PREFIX,
			// Token: 0x040014F1 RID: 5361
			INVALID_PREFIX
		}

		// Token: 0x020001FD RID: 509
		internal enum ProviderEnum
		{
			// Token: 0x040014F3 RID: 5363
			HTTP_PROV,
			// Token: 0x040014F4 RID: 5364
			NP_PROV,
			// Token: 0x040014F5 RID: 5365
			SESSION_PROV,
			// Token: 0x040014F6 RID: 5366
			SIGN_PROV,
			// Token: 0x040014F7 RID: 5367
			SM_PROV,
			// Token: 0x040014F8 RID: 5368
			SMUX_PROV,
			// Token: 0x040014F9 RID: 5369
			SSL_PROV,
			// Token: 0x040014FA RID: 5370
			TCP_PROV,
			// Token: 0x040014FB RID: 5371
			VIA_PROV,
			// Token: 0x040014FC RID: 5372
			CTAIP_PROV,
			// Token: 0x040014FD RID: 5373
			MAX_PROVS,
			// Token: 0x040014FE RID: 5374
			INVALID_PROV
		}

		// Token: 0x020001FE RID: 510
		internal enum QTypes
		{
			// Token: 0x04001500 RID: 5376
			SNI_QUERY_CONN_INFO,
			// Token: 0x04001501 RID: 5377
			SNI_QUERY_CONN_BUFSIZE,
			// Token: 0x04001502 RID: 5378
			SNI_QUERY_CONN_KEY,
			// Token: 0x04001503 RID: 5379
			SNI_QUERY_CLIENT_ENCRYPT_POSSIBLE,
			// Token: 0x04001504 RID: 5380
			SNI_QUERY_SERVER_ENCRYPT_POSSIBLE,
			// Token: 0x04001505 RID: 5381
			SNI_QUERY_CERTIFICATE,
			// Token: 0x04001506 RID: 5382
			SNI_QUERY_LOCALDB_HMODULE,
			// Token: 0x04001507 RID: 5383
			SNI_QUERY_CONN_ENCRYPT,
			// Token: 0x04001508 RID: 5384
			SNI_QUERY_CONN_PROVIDERNUM,
			// Token: 0x04001509 RID: 5385
			SNI_QUERY_CONN_CONNID,
			// Token: 0x0400150A RID: 5386
			SNI_QUERY_CONN_PARENTCONNID,
			// Token: 0x0400150B RID: 5387
			SNI_QUERY_CONN_SECPKG,
			// Token: 0x0400150C RID: 5388
			SNI_QUERY_CONN_NETPACKETSIZE,
			// Token: 0x0400150D RID: 5389
			SNI_QUERY_CONN_NODENUM,
			// Token: 0x0400150E RID: 5390
			SNI_QUERY_CONN_PACKETSRECD,
			// Token: 0x0400150F RID: 5391
			SNI_QUERY_CONN_PACKETSSENT,
			// Token: 0x04001510 RID: 5392
			SNI_QUERY_CONN_PEERADDR,
			// Token: 0x04001511 RID: 5393
			SNI_QUERY_CONN_PEERPORT,
			// Token: 0x04001512 RID: 5394
			SNI_QUERY_CONN_LASTREADTIME,
			// Token: 0x04001513 RID: 5395
			SNI_QUERY_CONN_LASTWRITETIME,
			// Token: 0x04001514 RID: 5396
			SNI_QUERY_CONN_CONSUMER_ID,
			// Token: 0x04001515 RID: 5397
			SNI_QUERY_CONN_CONNECTTIME,
			// Token: 0x04001516 RID: 5398
			SNI_QUERY_CONN_HTTPENDPOINT,
			// Token: 0x04001517 RID: 5399
			SNI_QUERY_CONN_LOCALADDR,
			// Token: 0x04001518 RID: 5400
			SNI_QUERY_CONN_LOCALPORT,
			// Token: 0x04001519 RID: 5401
			SNI_QUERY_CONN_SSLHANDSHAKESTATE,
			// Token: 0x0400151A RID: 5402
			SNI_QUERY_CONN_SOBUFAUTOTUNING,
			// Token: 0x0400151B RID: 5403
			SNI_QUERY_CONN_SECPKGNAME,
			// Token: 0x0400151C RID: 5404
			SNI_QUERY_CONN_SECPKGMUTUALAUTH,
			// Token: 0x0400151D RID: 5405
			SNI_QUERY_CONN_CONSUMERCONNID,
			// Token: 0x0400151E RID: 5406
			SNI_QUERY_CONN_SNIUCI,
			// Token: 0x0400151F RID: 5407
			SNI_QUERY_CONN_SUPPORTS_EXTENDED_PROTECTION,
			// Token: 0x04001520 RID: 5408
			SNI_QUERY_CONN_CHANNEL_PROVIDES_AUTHENTICATION_CONTEXT,
			// Token: 0x04001521 RID: 5409
			SNI_QUERY_CONN_PEERID,
			// Token: 0x04001522 RID: 5410
			SNI_QUERY_CONN_SUPPORTS_SYNC_OVER_ASYNC,
			// Token: 0x04001523 RID: 5411
			SNI_QUERY_CONN_SSL_SECCTXTHANDLE
		}

		// Token: 0x020001FF RID: 511
		internal enum TransparentNetworkResolutionMode : byte
		{
			// Token: 0x04001525 RID: 5413
			DisabledMode,
			// Token: 0x04001526 RID: 5414
			SequentialMode,
			// Token: 0x04001527 RID: 5415
			ParallelMode
		}

		// Token: 0x02000200 RID: 512
		internal struct Sni_Consumer_Info
		{
			// Token: 0x04001528 RID: 5416
			public int DefaultUserDataLength;

			// Token: 0x04001529 RID: 5417
			public IntPtr ConsumerKey;

			// Token: 0x0400152A RID: 5418
			public IntPtr fnReadComp;

			// Token: 0x0400152B RID: 5419
			public IntPtr fnWriteComp;

			// Token: 0x0400152C RID: 5420
			public IntPtr fnTrace;

			// Token: 0x0400152D RID: 5421
			public IntPtr fnAcceptComp;

			// Token: 0x0400152E RID: 5422
			public uint dwNumProts;

			// Token: 0x0400152F RID: 5423
			public IntPtr rgListenInfo;

			// Token: 0x04001530 RID: 5424
			public IntPtr NodeAffinity;
		}

		// Token: 0x02000201 RID: 513
		internal struct SNI_CLIENT_CONSUMER_INFO
		{
			// Token: 0x04001531 RID: 5425
			public SNINativeMethodWrapper.Sni_Consumer_Info ConsumerInfo;

			// Token: 0x04001532 RID: 5426
			[MarshalAs(UnmanagedType.LPWStr)]
			public string wszConnectionString;

			// Token: 0x04001533 RID: 5427
			[MarshalAs(UnmanagedType.LPWStr)]
			public string HostNameInCertificate;

			// Token: 0x04001534 RID: 5428
			public SNINativeMethodWrapper.PrefixEnum networkLibrary;

			// Token: 0x04001535 RID: 5429
			public unsafe byte* szSPN;

			// Token: 0x04001536 RID: 5430
			public uint cchSPN;

			// Token: 0x04001537 RID: 5431
			public unsafe byte* szInstanceName;

			// Token: 0x04001538 RID: 5432
			public uint cchInstanceName;

			// Token: 0x04001539 RID: 5433
			[MarshalAs(UnmanagedType.Bool)]
			public bool fOverrideLastConnectCache;

			// Token: 0x0400153A RID: 5434
			[MarshalAs(UnmanagedType.Bool)]
			public bool fSynchronousConnection;

			// Token: 0x0400153B RID: 5435
			public int timeout;

			// Token: 0x0400153C RID: 5436
			[MarshalAs(UnmanagedType.Bool)]
			public bool fParallel;

			// Token: 0x0400153D RID: 5437
			public SNINativeMethodWrapper.TransparentNetworkResolutionMode transparentNetworkResolution;

			// Token: 0x0400153E RID: 5438
			public int totalTimeout;

			// Token: 0x0400153F RID: 5439
			public bool isAzureSqlServerEndpoint;

			// Token: 0x04001540 RID: 5440
			public SqlConnectionIPAddressPreference ipAddressPreference;

			// Token: 0x04001541 RID: 5441
			public SNINativeMethodWrapper.SNI_DNSCache_Info DNSCacheInfo;
		}

		// Token: 0x02000202 RID: 514
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct SNI_DNSCache_Info
		{
			// Token: 0x04001542 RID: 5442
			[MarshalAs(UnmanagedType.LPWStr)]
			public string wszCachedFQDN;

			// Token: 0x04001543 RID: 5443
			[MarshalAs(UnmanagedType.LPWStr)]
			public string wszCachedTcpIPv4;

			// Token: 0x04001544 RID: 5444
			[MarshalAs(UnmanagedType.LPWStr)]
			public string wszCachedTcpIPv6;

			// Token: 0x04001545 RID: 5445
			[MarshalAs(UnmanagedType.LPWStr)]
			public string wszCachedTcpPort;
		}

		// Token: 0x02000203 RID: 515
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct SNI_Error
		{
			// Token: 0x04001546 RID: 5446
			internal SNINativeMethodWrapper.ProviderEnum provider;

			// Token: 0x04001547 RID: 5447
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 261)]
			internal string errorMessage;

			// Token: 0x04001548 RID: 5448
			internal uint nativeError;

			// Token: 0x04001549 RID: 5449
			internal uint sniError;

			// Token: 0x0400154A RID: 5450
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string fileName;

			// Token: 0x0400154B RID: 5451
			[MarshalAs(UnmanagedType.LPWStr)]
			internal string function;

			// Token: 0x0400154C RID: 5452
			internal uint lineNumber;
		}
	}
}
