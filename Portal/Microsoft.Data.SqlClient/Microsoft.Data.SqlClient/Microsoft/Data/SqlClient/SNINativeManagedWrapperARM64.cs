using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000C9 RID: 201
	internal static class SNINativeManagedWrapperARM64
	{
		// Token: 0x06000E24 RID: 3620
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIAddProviderWrapper")]
		internal static extern uint SNIAddProvider(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum ProvNum, [In] ref uint pInfo);

		// Token: 0x06000E25 RID: 3621
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIAddProviderWrapper(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum ProvNum, [In] ref SNINativeMethodWrapper.SNICTAIPProviderInfo pInfo);

		// Token: 0x06000E26 RID: 3622
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIAddProviderWrapper(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum ProvNum, [In] ref SNINativeMethodWrapper.AuthProviderInfo pInfo);

		// Token: 0x06000E27 RID: 3623
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNICheckConnectionWrapper")]
		internal static extern uint SNICheckConnection([In] SNIHandle pConn);

		// Token: 0x06000E28 RID: 3624
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNICloseWrapper")]
		internal static extern uint SNIClose(IntPtr pConn);

		// Token: 0x06000E29 RID: 3625
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SNIGetLastError(out SNINativeMethodWrapper.SNI_Error pErrorStruct);

		// Token: 0x06000E2A RID: 3626
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void SNIPacketRelease(IntPtr pPacket);

		// Token: 0x06000E2B RID: 3627
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIPacketResetWrapper")]
		internal static extern void SNIPacketReset([In] SNIHandle pConn, SNINativeMethodWrapper.IOType IOType, SNIPacket pPacket, SNINativeMethodWrapper.ConsumerNumber ConsNum);

		// Token: 0x06000E2C RID: 3628
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIQueryInfo(SNINativeMethodWrapper.QTypes QType, ref uint pbQInfo);

		// Token: 0x06000E2D RID: 3629
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIQueryInfo(SNINativeMethodWrapper.QTypes QType, ref IntPtr pbQInfo);

		// Token: 0x06000E2E RID: 3630
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIReadAsyncWrapper")]
		internal static extern uint SNIReadAsync(SNIHandle pConn, ref IntPtr ppNewPacket);

		// Token: 0x06000E2F RID: 3631
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIReadSyncOverAsync(SNIHandle pConn, ref IntPtr ppNewPacket, int timeout);

		// Token: 0x06000E30 RID: 3632
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIRemoveProviderWrapper")]
		internal static extern uint SNIRemoveProvider(SNIHandle pConn, SNINativeMethodWrapper.ProviderEnum ProvNum);

		// Token: 0x06000E31 RID: 3633
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNISecInitPackage(ref uint pcbMaxToken);

		// Token: 0x06000E32 RID: 3634
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNISetInfoWrapper")]
		internal static extern uint SNISetInfo(SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, [In] ref uint pbQInfo);

		// Token: 0x06000E33 RID: 3635
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNITerminate();

		// Token: 0x06000E34 RID: 3636
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIWaitForSSLHandshakeToCompleteWrapper")]
		internal static extern uint SNIWaitForSSLHandshakeToComplete([In] SNIHandle pConn, int dwMilliseconds, out uint pProtocolVersion);

		// Token: 0x06000E35 RID: 3637
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint UnmanagedIsTokenRestricted([In] IntPtr token, [MarshalAs(UnmanagedType.Bool)] out bool isRestricted);

		// Token: 0x06000E36 RID: 3638
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint GetSniMaxComposedSpnLength();

		// Token: 0x06000E37 RID: 3639
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, out Guid pbQInfo);

		// Token: 0x06000E38 RID: 3640
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, [MarshalAs(UnmanagedType.Bool)] out bool pbQInfo);

		// Token: 0x06000E39 RID: 3641
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, ref IntPtr pbQInfo);

		// Token: 0x06000E3A RID: 3642
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, out ushort portNum);

		// Token: 0x06000E3B RID: 3643
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		internal static extern uint SNIGetPeerAddrStrWrapper([In] SNIHandle pConn, int bufferSize, StringBuilder addrBuffer, out uint addrLen);

		// Token: 0x06000E3C RID: 3644
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIGetInfoWrapper([In] SNIHandle pConn, SNINativeMethodWrapper.QTypes QType, out SNINativeMethodWrapper.ProviderEnum provNum);

		// Token: 0x06000E3D RID: 3645
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIInitialize([In] IntPtr pmo);

		// Token: 0x06000E3E RID: 3646
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIOpenSyncExWrapper(ref SNINativeMethodWrapper.SNI_CLIENT_CONSUMER_INFO pClientConsumerInfo, out IntPtr ppConn);

		// Token: 0x06000E3F RID: 3647
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIOpenWrapper([In] ref SNINativeMethodWrapper.Sni_Consumer_Info pConsumerInfo, [MarshalAs(UnmanagedType.LPWStr)] string szConnect, [In] SNIHandle pConn, out IntPtr ppConn, [MarshalAs(UnmanagedType.Bool)] bool fSync, SqlConnectionIPAddressPreference ipPreference, [In] ref SNINativeMethodWrapper.SNI_DNSCache_Info pDNSCachedInfo);

		// Token: 0x06000E40 RID: 3648
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SNIPacketAllocateWrapper([In] SafeHandle pConn, SNINativeMethodWrapper.IOType IOType);

		// Token: 0x06000E41 RID: 3649
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIPacketGetDataWrapper([In] IntPtr packet, [In] [Out] byte[] readBuffer, uint readBufferLength, out uint dataSize);

		// Token: 0x06000E42 RID: 3650
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal unsafe static extern void SNIPacketSetData(SNIPacket pPacket, [In] byte* pbBuf, uint cbBuf);

		// Token: 0x06000E43 RID: 3651
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal unsafe static extern uint SNISecGenClientContextWrapper([In] SNIHandle pConn, [In] [Out] byte[] pIn, uint cbIn, [In] [Out] byte[] pOut, [In] ref uint pcbOut, [MarshalAs(UnmanagedType.Bool)] out bool pfDone, byte* szServerInfo, uint cbServerInfo, [MarshalAs(UnmanagedType.LPWStr)] string pwszUserName, [MarshalAs(UnmanagedType.LPWStr)] string pwszPassword);

		// Token: 0x06000E44 RID: 3652
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIWriteAsyncWrapper(SNIHandle pConn, [In] SNIPacket pPacket);

		// Token: 0x06000E45 RID: 3653
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern uint SNIWriteSyncOverAsync(SNIHandle pConn, [In] SNIPacket pPacket);

		// Token: 0x06000E46 RID: 3654
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr SNIClientCertificateFallbackWrapper(IntPtr pCallbackContext);

		// Token: 0x06000E47 RID: 3655
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIServerEnumOpenWrapper")]
		internal static extern IntPtr SNIServerEnumOpen();

		// Token: 0x06000E48 RID: 3656
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "SNIServerEnumCloseWrapper")]
		internal static extern void SNIServerEnumClose([In] IntPtr packet);

		// Token: 0x06000E49 RID: 3657
		[DllImport("Microsoft.Data.SqlClient.SNI.arm64.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "SNIServerEnumReadWrapper")]
		internal static extern int SNIServerEnumRead([In] IntPtr packet, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] char[] readBuffer, [In] int bufferLength, [MarshalAs(UnmanagedType.Bool)] out bool more);

		// Token: 0x04000630 RID: 1584
		private const string SNI = "Microsoft.Data.SqlClient.SNI.arm64.dll";
	}
}
