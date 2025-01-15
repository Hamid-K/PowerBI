using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000CB RID: 203
	internal static class SNINativeManagedWrapperX86
	{
		// Token: 0x06000E70 RID: 3696
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIAddProviderWrapper")]
		internal static extern uint SNIAddProvider(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum ProvNum, [In] ref uint pInfo);

		// Token: 0x06000E71 RID: 3697
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIAddProviderWrapper(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum ProvNum, [In] ref SNINativeMethodWrapper.SNICTAIPProviderInfo pInfo);

		// Token: 0x06000E72 RID: 3698
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIAddProviderWrapper(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum ProvNum, [In] ref SNINativeMethodWrapper.AuthProviderInfo pInfo);

		// Token: 0x06000E73 RID: 3699
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNICheckConnectionWrapper")]
		internal static extern uint SNICheckConnection([In] SNIHandle pConn);

		// Token: 0x06000E74 RID: 3700
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNICloseWrapper")]
		internal static extern uint SNIClose(IntPtr pConn);

		// Token: 0x06000E75 RID: 3701
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SNIGetLastError(out SNINativeMethodWrapper.SNI_Error pErrorStruct);

		// Token: 0x06000E76 RID: 3702
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SNIPacketRelease(IntPtr pPacket);

		// Token: 0x06000E77 RID: 3703
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIPacketResetWrapper")]
		internal static extern void SNIPacketReset([In] SNIHandle pConn, SNINativeMethodWrapper.IOType IOType, SNIPacket pPacket, SNINativeMethodWrapper.ConsumerNumber ConsNum);

		// Token: 0x06000E78 RID: 3704
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIQueryInfo(SNINativeMethodWrapper.QTypes QType, ref uint pbQInfo);

		// Token: 0x06000E79 RID: 3705
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIQueryInfo(SNINativeMethodWrapper.QTypes QType, ref IntPtr pbQInfo);

		// Token: 0x06000E7A RID: 3706
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIReadAsyncWrapper")]
		internal static extern uint SNIReadAsync(SNIHandle pConn, ref IntPtr ppNewPacket);

		// Token: 0x06000E7B RID: 3707
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIReadSyncOverAsync(SNIHandle pConn, ref IntPtr ppNewPacket, int timeout);

		// Token: 0x06000E7C RID: 3708
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIRemoveProviderWrapper")]
		internal static extern uint SNIRemoveProvider(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum ProvNum);

		// Token: 0x06000E7D RID: 3709
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNISecInitPackage(ref uint pcbMaxToken);

		// Token: 0x06000E7E RID: 3710
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNISetInfoWrapper")]
		internal static extern uint SNISetInfo(SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, [In] ref uint pbQInfo);

		// Token: 0x06000E7F RID: 3711
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNITerminate();

		// Token: 0x06000E80 RID: 3712
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIWaitForSSLHandshakeToCompleteWrapper")]
		internal static extern uint SNIWaitForSSLHandshakeToComplete([In] SNIHandle pConn, int dwMilliseconds, out uint pProtocolVersion);

		// Token: 0x06000E81 RID: 3713
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint UnmanagedIsTokenRestricted([In] IntPtr token, [MarshalAs(UnmanagedType.Bool)] out bool isRestricted);

		// Token: 0x06000E82 RID: 3714
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint GetSniMaxComposedSpnLength();

		// Token: 0x06000E83 RID: 3715
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, out Guid pbQInfo);

		// Token: 0x06000E84 RID: 3716
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, [MarshalAs(UnmanagedType.Bool)] out bool pbQInfo);

		// Token: 0x06000E85 RID: 3717
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, ref IntPtr pbQInfo);

		// Token: 0x06000E86 RID: 3718
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, out ushort portNum);

		// Token: 0x06000E87 RID: 3719
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		internal static extern uint SNIGetPeerAddrStrWrapper([In] SNIHandle pConn, int bufferSize, StringBuilder addrBuffer, out uint addrLen);

		// Token: 0x06000E88 RID: 3720
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, out SNINativeMethodWrapper.ProviderEnum provNum);

		// Token: 0x06000E89 RID: 3721
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIInitialize([In] IntPtr pmo);

		// Token: 0x06000E8A RID: 3722
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIOpenSyncExWrapper(ref SNINativeMethodWrapper.SNI_CLIENT_CONSUMER_INFO pClientConsumerInfo, out IntPtr ppConn);

		// Token: 0x06000E8B RID: 3723
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIOpenWrapper([In] ref SNINativeMethodWrapper.Sni_Consumer_Info pConsumerInfo, [MarshalAs(UnmanagedType.LPWStr)] string szConnect, [In] SNIHandle pConn, out IntPtr ppConn, [MarshalAs(UnmanagedType.Bool)] bool fSync, SqlConnectionIPAddressPreference ipPreference, [In] ref SNINativeMethodWrapper.SNI_DNSCache_Info pDNSCachedInfo);

		// Token: 0x06000E8C RID: 3724
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SNIPacketAllocateWrapper([In] SafeHandle pConn, SNINativeMethodWrapper.IOType IOType);

		// Token: 0x06000E8D RID: 3725
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIPacketGetDataWrapper([In] IntPtr packet, [In] [Out] byte[] readBuffer, uint readBufferLength, out uint dataSize);

		// Token: 0x06000E8E RID: 3726
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal unsafe static extern void SNIPacketSetData(SNIPacket pPacket, [In] byte* pbBuf, uint cbBuf);

		// Token: 0x06000E8F RID: 3727
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal unsafe static extern uint SNISecGenClientContextWrapper([In] SNIHandle pConn, [In] [Out] byte[] pIn, uint cbIn, [In] [Out] byte[] pOut, [In] ref uint pcbOut, [MarshalAs(UnmanagedType.Bool)] out bool pfDone, byte* szServerInfo, uint cbServerInfo, [MarshalAs(UnmanagedType.LPWStr)] string pwszUserName, [MarshalAs(UnmanagedType.LPWStr)] string pwszPassword);

		// Token: 0x06000E90 RID: 3728
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIWriteAsyncWrapper(SNIHandle pConn, [In] SNIPacket pPacket);

		// Token: 0x06000E91 RID: 3729
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIWriteSyncOverAsync(SNIHandle pConn, [In] SNIPacket pPacket);

		// Token: 0x06000E92 RID: 3730
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SNIClientCertificateFallbackWrapper(IntPtr pCallbackContext);

		// Token: 0x06000E93 RID: 3731
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIServerEnumOpenWrapper")]
		internal static extern IntPtr SNIServerEnumOpen();

		// Token: 0x06000E94 RID: 3732
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIServerEnumCloseWrapper")]
		internal static extern void SNIServerEnumClose([In] IntPtr packet);

		// Token: 0x06000E95 RID: 3733
		[DllImport("Microsoft.Data.SqlClient.SNI.x86.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "SNIServerEnumReadWrapper")]
		internal static extern int SNIServerEnumRead([In] IntPtr packet, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] char[] readBuffer, [In] int bufferLength, [MarshalAs(UnmanagedType.Bool)] out bool more);

		// Token: 0x04000632 RID: 1586
		private const string SNI = "Microsoft.Data.SqlClient.SNI.x86.dll";
	}
}
