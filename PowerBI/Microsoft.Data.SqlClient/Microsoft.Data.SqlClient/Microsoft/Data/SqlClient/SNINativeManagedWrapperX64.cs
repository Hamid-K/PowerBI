using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000CA RID: 202
	internal static class SNINativeManagedWrapperX64
	{
		// Token: 0x06000E4A RID: 3658
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIAddProviderWrapper")]
		internal static extern uint SNIAddProvider(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum ProvNum, [In] ref uint pInfo);

		// Token: 0x06000E4B RID: 3659
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIAddProviderWrapper(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum ProvNum, [In] ref SNINativeMethodWrapper.SNICTAIPProviderInfo pInfo);

		// Token: 0x06000E4C RID: 3660
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIAddProviderWrapper(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum ProvNum, [In] ref SNINativeMethodWrapper.AuthProviderInfo pInfo);

		// Token: 0x06000E4D RID: 3661
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNICheckConnectionWrapper")]
		internal static extern uint SNICheckConnection([In] SNIHandle pConn);

		// Token: 0x06000E4E RID: 3662
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNICloseWrapper")]
		internal static extern uint SNIClose(IntPtr pConn);

		// Token: 0x06000E4F RID: 3663
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SNIGetLastError(out SNINativeMethodWrapper.SNI_Error pErrorStruct);

		// Token: 0x06000E50 RID: 3664
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SNIPacketRelease(IntPtr pPacket);

		// Token: 0x06000E51 RID: 3665
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIPacketResetWrapper")]
		internal static extern void SNIPacketReset([In] SNIHandle pConn, SNINativeMethodWrapper.IOType IOType, SNIPacket pPacket, SNINativeMethodWrapper.ConsumerNumber ConsNum);

		// Token: 0x06000E52 RID: 3666
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIQueryInfo(SNINativeMethodWrapper.QTypes QType, ref uint pbQInfo);

		// Token: 0x06000E53 RID: 3667
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIQueryInfo(SNINativeMethodWrapper.QTypes QType, ref IntPtr pbQInfo);

		// Token: 0x06000E54 RID: 3668
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIReadAsyncWrapper")]
		internal static extern uint SNIReadAsync(SNIHandle pConn, ref IntPtr ppNewPacket);

		// Token: 0x06000E55 RID: 3669
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIReadSyncOverAsync(SNIHandle pConn, ref IntPtr ppNewPacket, int timeout);

		// Token: 0x06000E56 RID: 3670
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIRemoveProviderWrapper")]
		internal static extern uint SNIRemoveProvider(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum ProvNum);

		// Token: 0x06000E57 RID: 3671
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNISecInitPackage(ref uint pcbMaxToken);

		// Token: 0x06000E58 RID: 3672
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNISetInfoWrapper")]
		internal static extern uint SNISetInfo(SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, [In] ref uint pbQInfo);

		// Token: 0x06000E59 RID: 3673
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNITerminate();

		// Token: 0x06000E5A RID: 3674
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIWaitForSSLHandshakeToCompleteWrapper")]
		internal static extern uint SNIWaitForSSLHandshakeToComplete([In] SNIHandle pConn, int dwMilliseconds, out uint pProtocolVersion);

		// Token: 0x06000E5B RID: 3675
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint UnmanagedIsTokenRestricted([In] IntPtr token, [MarshalAs(UnmanagedType.Bool)] out bool isRestricted);

		// Token: 0x06000E5C RID: 3676
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint GetSniMaxComposedSpnLength();

		// Token: 0x06000E5D RID: 3677
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, out Guid pbQInfo);

		// Token: 0x06000E5E RID: 3678
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, [MarshalAs(UnmanagedType.Bool)] out bool pbQInfo);

		// Token: 0x06000E5F RID: 3679
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, ref IntPtr pbQInfo);

		// Token: 0x06000E60 RID: 3680
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, out ushort portNum);

		// Token: 0x06000E61 RID: 3681
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		internal static extern uint SNIGetPeerAddrStrWrapper([In] SNIHandle pConn, int bufferSize, StringBuilder addrBuffer, out uint addrLen);

		// Token: 0x06000E62 RID: 3682
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, out SNINativeMethodWrapper.ProviderEnum provNum);

		// Token: 0x06000E63 RID: 3683
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIInitialize([In] IntPtr pmo);

		// Token: 0x06000E64 RID: 3684
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIOpenSyncExWrapper(ref SNINativeMethodWrapper.SNI_CLIENT_CONSUMER_INFO pClientConsumerInfo, out IntPtr ppConn);

		// Token: 0x06000E65 RID: 3685
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIOpenWrapper([In] ref SNINativeMethodWrapper.Sni_Consumer_Info pConsumerInfo, [MarshalAs(UnmanagedType.LPWStr)] string szConnect, [In] SNIHandle pConn, out IntPtr ppConn, [MarshalAs(UnmanagedType.Bool)] bool fSync, SqlConnectionIPAddressPreference ipPreference, [In] ref SNINativeMethodWrapper.SNI_DNSCache_Info pDNSCachedInfo);

		// Token: 0x06000E66 RID: 3686
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SNIPacketAllocateWrapper([In] SafeHandle pConn, SNINativeMethodWrapper.IOType IOType);

		// Token: 0x06000E67 RID: 3687
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIPacketGetDataWrapper([In] IntPtr packet, [In] [Out] byte[] readBuffer, uint readBufferLength, out uint dataSize);

		// Token: 0x06000E68 RID: 3688
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal unsafe static extern void SNIPacketSetData(SNIPacket pPacket, [In] byte* pbBuf, uint cbBuf);

		// Token: 0x06000E69 RID: 3689
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal unsafe static extern uint SNISecGenClientContextWrapper([In] SNIHandle pConn, [In] [Out] byte[] pIn, uint cbIn, [In] [Out] byte[] pOut, [In] ref uint pcbOut, [MarshalAs(UnmanagedType.Bool)] out bool pfDone, byte* szServerInfo, uint cbServerInfo, [MarshalAs(UnmanagedType.LPWStr)] string pwszUserName, [MarshalAs(UnmanagedType.LPWStr)] string pwszPassword);

		// Token: 0x06000E6A RID: 3690
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIWriteAsyncWrapper(SNIHandle pConn, [In] SNIPacket pPacket);

		// Token: 0x06000E6B RID: 3691
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIWriteSyncOverAsync(SNIHandle pConn, [In] SNIPacket pPacket);

		// Token: 0x06000E6C RID: 3692
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SNIClientCertificateFallbackWrapper(IntPtr pCallbackContext);

		// Token: 0x06000E6D RID: 3693
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIServerEnumOpenWrapper")]
		internal static extern IntPtr SNIServerEnumOpen();

		// Token: 0x06000E6E RID: 3694
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIServerEnumCloseWrapper")]
		internal static extern void SNIServerEnumClose([In] IntPtr packet);

		// Token: 0x06000E6F RID: 3695
		[DllImport("Microsoft.Data.SqlClient.SNI.x64.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "SNIServerEnumReadWrapper")]
		internal static extern int SNIServerEnumRead([In] IntPtr packet, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] char[] readBuffer, [In] int bufferLength, [MarshalAs(UnmanagedType.Bool)] out bool more);

		// Token: 0x04000631 RID: 1585
		private const string SNI = "Microsoft.Data.SqlClient.SNI.x64.dll";
	}
}
