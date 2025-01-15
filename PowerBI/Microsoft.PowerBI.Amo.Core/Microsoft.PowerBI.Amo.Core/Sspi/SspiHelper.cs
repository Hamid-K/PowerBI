using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AnalysisServices.Interop;

namespace Microsoft.AnalysisServices.Sspi
{
	// Token: 0x02000116 RID: 278
	internal static class SspiHelper
	{
		// Token: 0x0600101E RID: 4126 RVA: 0x000380F0 File Offset: 0x000362F0
		public static void AcquireCredentialsHandle(string package, SecurityCredentialUsage usage, out SecHandle handle)
		{
			ulong num2;
			int num = NativeMethods.AcquireCredentialsHandle(null, package, (uint)usage, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, out handle, out num2);
			if (num != 0)
			{
				throw new Win32Exception(num);
			}
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00038128 File Offset: 0x00036328
		public static void AcquireCredentialsHandle(X509Certificate2 certificate, SecurityCredentialUsage usage, out SecHandle handle)
		{
			SChannelCred schannelCred = new SChannelCred
			{
				dwVersion = 4U
			};
			try
			{
				if (certificate != null && certificate.Handle != IntPtr.Zero)
				{
					schannelCred.cCreds = 1U;
					schannelCred.paCred = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)));
					Marshal.WriteIntPtr(schannelCred.paCred, certificate.Handle);
				}
				else
				{
					schannelCred.cCreds = 0U;
					schannelCred.paCred = IntPtr.Zero;
				}
				schannelCred.hRootStore = IntPtr.Zero;
				schannelCred.cMappers = 0U;
				schannelCred.aphMappers = IntPtr.Zero;
				schannelCred.cSupportedAlgs = 0U;
				schannelCred.palgSupportedAlgs = IntPtr.Zero;
				schannelCred.grbitEnabledProtocols = 10880U;
				schannelCred.dwMinimumCipherStrength = 0U;
				schannelCred.dwMaximumCipherStrength = 0U;
				schannelCred.dwSessionLifespan = 0U;
				schannelCred.dwFlags = 48U;
				schannelCred.dwCredFormat = 0U;
				ulong num2;
				int num = NativeMethods.AcquireCredentialsHandle(null, "Microsoft Unified Security Protocol Provider", (uint)usage, IntPtr.Zero, ref schannelCred, IntPtr.Zero, IntPtr.Zero, out handle, out num2);
				GC.KeepAlive(certificate);
				if (num != 0)
				{
					throw new Win32Exception(num);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(schannelCred.paCred);
			}
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00038260 File Offset: 0x00036460
		public static void FreeCredentialsHandle(ref SecHandle credentialsHandle)
		{
			if (!credentialsHandle.IsInvalid)
			{
				NativeMethods.FreeCredentialsHandle(ref credentialsHandle);
				credentialsHandle.Reset();
			}
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x00038278 File Offset: 0x00036478
		public static int InitializeSecurityContext(SecHandle credentialHandle, string targetName, SecurityContextRequirements requirements, SecurityDataRepresentation targetDataRepresentation, out SecHandle contextHandle, SecurityBuffer[] output, out SecurityContextRequirements attributies)
		{
			bool flag = (requirements & SecurityContextRequirements.AllocateMemory) == SecurityContextRequirements.AllocateMemory;
			GCHandle[] array;
			SecBufferDesc secBufferDesc = SecurityBuffer.CreateDescriptor(output, out array);
			int num4;
			try
			{
				uint num2;
				ulong num3;
				int num = NativeMethods.InitializeSecurityContext(ref credentialHandle, IntPtr.Zero, targetName, (uint)requirements, 0U, (int)targetDataRepresentation, IntPtr.Zero, 0U, out contextHandle, ref secBufferDesc, out num2, out num3);
				if ((num & -2147483648) != 0)
				{
					throw new Win32Exception(num);
				}
				if ((num == 0 || num == 590610) && secBufferDesc.cbBuffers > 0U)
				{
					if (flag)
					{
						SecurityBuffer.GetBuffers(secBufferDesc, output);
					}
					else
					{
						SecurityBuffer.UpdateBuffers(secBufferDesc, output);
					}
				}
				attributies = (SecurityContextRequirements)num2;
				num4 = num;
			}
			finally
			{
				secBufferDesc.Release(flag);
				SspiHelper.FreeGCHandles(array);
			}
			return num4;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00038324 File Offset: 0x00036524
		public static int InitializeSecurityContext(SecHandle credentialHandle, ref SecHandle contextHandle, string targetName, SecurityContextRequirements requirements, SecurityDataRepresentation targetDataRepresentation, SecurityBuffer[] input, SecurityBuffer[] output, out SecurityContextRequirements attributies)
		{
			bool flag = (requirements & SecurityContextRequirements.AllocateMemory) == SecurityContextRequirements.AllocateMemory;
			GCHandle[] array;
			SecBufferDesc secBufferDesc = SecurityBuffer.CreateDescriptor(input, out array);
			GCHandle[] array2;
			SecBufferDesc secBufferDesc2 = SecurityBuffer.CreateDescriptor(output, out array2);
			int num4;
			try
			{
				uint num2;
				ulong num3;
				int num = NativeMethods.InitializeSecurityContext(ref credentialHandle, ref contextHandle, targetName, (uint)requirements, 0U, (int)targetDataRepresentation, ref secBufferDesc, 0U, ref contextHandle, ref secBufferDesc2, out num2, out num3);
				if ((num & -2147483648) != 0)
				{
					throw new Win32Exception(num);
				}
				if ((num == 0 || num == 590610) && secBufferDesc2.cbBuffers > 0U)
				{
					if (flag)
					{
						SecurityBuffer.GetBuffers(secBufferDesc2, output);
					}
					else
					{
						SecurityBuffer.UpdateBuffers(secBufferDesc2, output);
					}
				}
				attributies = (SecurityContextRequirements)num2;
				num4 = num;
			}
			finally
			{
				secBufferDesc.Release(false);
				secBufferDesc2.Release(flag);
				SspiHelper.FreeGCHandles(array);
				SspiHelper.FreeGCHandles(array2);
			}
			return num4;
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x000383E8 File Offset: 0x000365E8
		public static void DeleteSecurityContext(ref SecHandle contextHandle)
		{
			if (!contextHandle.IsInvalid)
			{
				NativeMethods.DeleteSecurityContext(ref contextHandle);
				contextHandle.Reset();
			}
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00038400 File Offset: 0x00036600
		public static void EncryptMessage(ref SecHandle contextHandle, SecurityBuffer[] buffers, int sequenceNumber)
		{
			GCHandle[] array;
			SecBufferDesc secBufferDesc = SecurityBuffer.CreateDescriptor(buffers, out array);
			try
			{
				int num = NativeMethods.EncryptMessage(ref contextHandle, 0U, ref secBufferDesc, (uint)sequenceNumber);
				if (num != 0)
				{
					throw new Win32Exception(num);
				}
				SecurityBuffer.UpdateBuffers(secBufferDesc, buffers);
			}
			finally
			{
				secBufferDesc.Release(false);
				SspiHelper.FreeGCHandles(array);
			}
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00038454 File Offset: 0x00036654
		public static bool DecryptMessage(ref SecHandle contextHandle, SecurityBuffer[] buffers, int sequenceNumber, bool isInStreamMode)
		{
			GCHandle[] array;
			SecBufferDesc secBufferDesc = SecurityBuffer.CreateDescriptor(buffers, out array);
			bool flag;
			try
			{
				uint num2;
				int num = NativeMethods.DecryptMessage(ref contextHandle, ref secBufferDesc, (uint)sequenceNumber, out num2);
				if (num != 0 && (!isInStreamMode || num != -2146893032))
				{
					throw new Win32Exception(num);
				}
				if (num != 0)
				{
					flag = false;
				}
				else
				{
					if (isInStreamMode)
					{
						SecurityBuffer.UpdateBuffersAfterDecryptInStreamMode(secBufferDesc, buffers);
					}
					else
					{
						SecurityBuffer.UpdateBuffers(secBufferDesc, buffers);
					}
					flag = true;
				}
			}
			finally
			{
				secBufferDesc.Release(false);
				SspiHelper.FreeGCHandles(array);
			}
			return flag;
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x000384CC File Offset: 0x000366CC
		public static void MakeSignature(ref SecHandle contextHandle, SecurityBuffer[] buffers, int sequenceNumber)
		{
			GCHandle[] array;
			SecBufferDesc secBufferDesc = SecurityBuffer.CreateDescriptor(buffers, out array);
			try
			{
				int num = NativeMethods.MakeSignature(ref contextHandle, 0U, ref secBufferDesc, (uint)sequenceNumber);
				if (num != 0)
				{
					throw new Win32Exception(num);
				}
				SecurityBuffer.UpdateBuffers(secBufferDesc, buffers);
			}
			finally
			{
				secBufferDesc.Release(false);
				SspiHelper.FreeGCHandles(array);
			}
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x00038520 File Offset: 0x00036720
		public static void VerifySignature(ref SecHandle contextHandle, SecurityBuffer[] buffers, int sequenceNumber)
		{
			GCHandle[] array;
			SecBufferDesc secBufferDesc = SecurityBuffer.CreateDescriptor(buffers, out array);
			try
			{
				uint num2;
				int num = NativeMethods.VerifySignature(ref contextHandle, ref secBufferDesc, (uint)sequenceNumber, out num2);
				if (num != 0)
				{
					throw new Win32Exception(num);
				}
				SecurityBuffer.UpdateBuffers(secBufferDesc, buffers);
			}
			finally
			{
				secBufferDesc.Release(false);
				SspiHelper.FreeGCHandles(array);
			}
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00038574 File Offset: 0x00036774
		private static void FreeGCHandles(GCHandle[] gcHandles)
		{
			if (gcHandles == null)
			{
				return;
			}
			for (int i = 0; i < gcHandles.Length; i++)
			{
				if (gcHandles[i].IsAllocated)
				{
					gcHandles[i].Free();
				}
			}
		}

		// Token: 0x040009EE RID: 2542
		private const string SChannelPackageName = "Microsoft Unified Security Protocol Provider";
	}
}
