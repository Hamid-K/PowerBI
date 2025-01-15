using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using <CppImplementationDetails>;
using Util;

namespace RSRemoteRpcClient
{
	// Token: 0x0200001D RID: 29
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class Utilities
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00007404 File Offset: 0x00006804
		public static string GetDefaultInstanceName()
		{
			return new string(<Module>.NativeGetDefaultInstanceName());
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00007424 File Offset: 0x00006824
		public unsafe static string InstanceNameFromInstanceID(string instanceID)
		{
			SafeStringToHGlobalUni safeStringToHGlobalUni = null;
			string text;
			try
			{
				safeStringToHGlobalUni = SafeStringToHGlobalUni.Create(instanceID);
				$ArrayType$$$BY0IA@G $ArrayType$$$BY0IA@G;
				int num = <Module>.NativeInstanceNameFromInstanceID(safeStringToHGlobalUni.ToPointer(), (ushort*)(&$ArrayType$$$BY0IA@G), 128);
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
				text = new string((char*)(&$ArrayType$$$BY0IA@G));
			}
			finally
			{
				if (safeStringToHGlobalUni != null)
				{
					safeStringToHGlobalUni.Close();
				}
			}
			return text;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00007494 File Offset: 0x00006894
		public unsafe static string InstanceIDFromInstanceName(string instanceName)
		{
			SafeStringToHGlobalUni safeStringToHGlobalUni = null;
			string text;
			try
			{
				safeStringToHGlobalUni = SafeStringToHGlobalUni.Create(instanceName);
				$ArrayType$$$BY0IA@G $ArrayType$$$BY0IA@G;
				int num = <Module>.NativeInstanceIDFromInstanceName(safeStringToHGlobalUni.ToPointer(), (ushort*)(&$ArrayType$$$BY0IA@G), 128);
				safeStringToHGlobalUni.Close();
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
				text = new string((char*)(&$ArrayType$$$BY0IA@G));
			}
			finally
			{
				if (safeStringToHGlobalUni != null)
				{
					safeStringToHGlobalUni.Close();
				}
			}
			return text;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00007508 File Offset: 0x00006908
		public unsafe static string RPCEndpointFromInstanceName(string instanceName)
		{
			SafeStringToHGlobalUni safeStringToHGlobalUni = null;
			string text;
			try
			{
				safeStringToHGlobalUni = SafeStringToHGlobalUni.Create(instanceName);
				$ArrayType$$$BY0IA@G $ArrayType$$$BY0IA@G;
				int num = <Module>.NativeRPCEndpointFromInstanceName(safeStringToHGlobalUni.ToPointer(), (ushort*)(&$ArrayType$$$BY0IA@G), 128);
				safeStringToHGlobalUni.Close();
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
				text = new string((char*)(&$ArrayType$$$BY0IA@G));
			}
			finally
			{
				if (safeStringToHGlobalUni != null)
				{
					safeStringToHGlobalUni.Close();
				}
			}
			return text;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000757C File Offset: 0x0000697C
		public unsafe static string RPCEndpointFromInstanceID(string instanceID)
		{
			SafeStringToHGlobalUni safeStringToHGlobalUni = null;
			string text;
			try
			{
				safeStringToHGlobalUni = SafeStringToHGlobalUni.Create(instanceID);
				$ArrayType$$$BY0IA@G $ArrayType$$$BY0IA@G;
				int num = <Module>.NativeRPCEndpointFromInstanceID(safeStringToHGlobalUni.ToPointer(), (ushort*)(&$ArrayType$$$BY0IA@G), 128);
				safeStringToHGlobalUni.Close();
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
				text = new string((char*)(&$ArrayType$$$BY0IA@G));
			}
			finally
			{
				if (safeStringToHGlobalUni != null)
				{
					safeStringToHGlobalUni.Close();
				}
			}
			return text;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000075F0 File Offset: 0x000069F0
		public unsafe static string ServiceNameFromInstanceName(string instanceName)
		{
			SafeStringToHGlobalUni safeStringToHGlobalUni = null;
			string text;
			try
			{
				safeStringToHGlobalUni = SafeStringToHGlobalUni.Create(instanceName);
				$ArrayType$$$BY0IA@G $ArrayType$$$BY0IA@G;
				int num = <Module>.NativeServiceNameFromInstanceName(safeStringToHGlobalUni.ToPointer(), (ushort*)(&$ArrayType$$$BY0IA@G), 128);
				safeStringToHGlobalUni.Close();
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
				text = new string((char*)(&$ArrayType$$$BY0IA@G));
			}
			finally
			{
				if (safeStringToHGlobalUni != null)
				{
					safeStringToHGlobalUni.Close();
				}
			}
			return text;
		}
	}
}
