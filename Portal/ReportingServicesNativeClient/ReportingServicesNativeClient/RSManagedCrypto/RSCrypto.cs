using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Permissions;
using Microsoft.Win32;
using Util;

namespace RSManagedCrypto
{
	// Token: 0x02000020 RID: 32
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public sealed class RSCrypto : CriticalFinalizerObject, IDisposable
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00007DC0 File Offset: 0x000071C0
		public unsafe RSCrypto()
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				RSNativeCrypto* ptr = <Module>.@new(40UL);
				RSNativeCrypto* ptr2;
				try
				{
					if (ptr != null)
					{
						ptr2 = <Module>.RSNativeCrypto.{ctor}(ptr, RSCrypto.KeyContainerNameCurrent, RSCrypto.KeyContainerNamePrevious);
					}
					else
					{
						ptr2 = null;
					}
				}
				catch
				{
					<Module>.delete((void*)ptr);
					goto EndFinally_8;
					throw;
				}
				this.m_pNativeCrypto = ptr2;
				EndFinally_8:;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000084A8 File Offset: 0x000078A8
		private void ~RSCrypto()
		{
			this.!RSCrypto();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00007F50 File Offset: 0x00007350
		private unsafe void !RSCrypto()
		{
			RSNativeCrypto* pNativeCrypto = this.m_pNativeCrypto;
			if (pNativeCrypto != null)
			{
				<Module>.RSNativeCrypto.{dtor}(pNativeCrypto);
				<Module>.delete((void*)pNativeCrypto);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00007E4C File Offset: 0x0000724C
		public void InitializeInMemoryContext()
		{
			Marshal.ThrowExceptionForHR(<Module>.RSNativeCrypto.InitializeInMemoryContext(this.m_pNativeCrypto));
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00007E90 File Offset: 0x00007290
		public void CreateKeyContainer()
		{
			Marshal.ThrowExceptionForHR(<Module>.RSNativeCrypto.CreateKeyContainer(this.m_pNativeCrypto));
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00007F7C File Offset: 0x0000737C
		public unsafe byte[] CreateSymmetricKey()
		{
			byte* ptr = null;
			byte[] array = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (!this.IsInit())
				{
					int num = 0;
					Marshal.ThrowExceptionForHR(<Module>.RSNativeCrypto.CreateSymmetricKey(this.m_pNativeCrypto, &ptr, ref num));
					array = this.ConvertNativeArrayToManaged(ptr, num);
				}
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.delete[]((void*)ptr);
				}
			}
			return array;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00007FEC File Offset: 0x000073EC
		public unsafe byte[] ImportSymmetricKey(byte[] keyBlob, string importPassword)
		{
			SafeByteArray safeByteArray = null;
			SafeStringToHGlobalUni safeStringToHGlobalUni = null;
			byte* ptr = null;
			ulong num = 0UL;
			byte[] array = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				safeByteArray = SafeByteArray.Create(keyBlob);
				safeStringToHGlobalUni = SafeStringToHGlobalUni.Create(importPassword);
				StringUtilities.GetStringSize(safeStringToHGlobalUni.DangerousGetHandle(), ref num);
				int num2 = 0;
				Marshal.ThrowExceptionForHR(<Module>.RSNativeCrypto.ImportSymmetricKey(this.m_pNativeCrypto, safeByteArray.ToPointer(), keyBlob.Length, (ushort*)safeStringToHGlobalUni.ToPointer(), &ptr, ref num2));
				array = this.ConvertNativeArrayToManaged(ptr, num2);
			}
			finally
			{
				if (safeByteArray != null)
				{
					safeByteArray.Close();
				}
				if (safeStringToHGlobalUni != null)
				{
					safeStringToHGlobalUni.ZeroString();
					safeStringToHGlobalUni.Close();
				}
				if (ptr != null)
				{
					<Module>.delete[]((void*)ptr);
				}
			}
			return array;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00007EB4 File Offset: 0x000072B4
		public void ImportSymmetricKey(byte[] symKeyBlob)
		{
			SafeByteArray safeByteArray = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (!this.IsInit())
				{
					safeByteArray = SafeByteArray.Create(symKeyBlob);
					Marshal.ThrowExceptionForHR(<Module>.RSNativeCrypto.ImportSymmetricKey(this.m_pNativeCrypto, safeByteArray.ToPointer(), symKeyBlob.Length));
				}
			}
			finally
			{
				if (safeByteArray != null)
				{
					safeByteArray.Close();
				}
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000081BC File Offset: 0x000075BC
		public unsafe byte[] ExportSymmetricKey(string exportPassword)
		{
			SafeStringToHGlobalUni safeStringToHGlobalUni = null;
			byte* ptr = null;
			ulong num = 0UL;
			byte[] array = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				safeStringToHGlobalUni = SafeStringToHGlobalUni.Create(exportPassword);
				StringUtilities.GetStringSize(safeStringToHGlobalUni.DangerousGetHandle(), ref num);
				int num2 = 0;
				Marshal.ThrowExceptionForHR(<Module>.RSNativeCrypto.ExportSymmetricKey(this.m_pNativeCrypto, (ushort*)safeStringToHGlobalUni.ToPointer(), &ptr, &num2));
				array = this.ConvertNativeArrayToManaged(ptr, num2);
			}
			finally
			{
				if (safeStringToHGlobalUni != null)
				{
					safeStringToHGlobalUni.ZeroString();
					safeStringToHGlobalUni.Close();
				}
				if (ptr != null)
				{
					<Module>.delete[]((void*)ptr);
				}
			}
			return array;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00008128 File Offset: 0x00007528
		public unsafe byte[] ExportSymmetricKey(byte[] pPublicKeyBlob)
		{
			SafeByteArray safeByteArray = null;
			byte* ptr = null;
			byte[] array = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (!this.IsInit())
				{
					throw new Exception("Crypto not initialized");
				}
				safeByteArray = SafeByteArray.Create(pPublicKeyBlob);
				int num;
				Marshal.ThrowExceptionForHR(<Module>.RSNativeCrypto.ExportSymmetricKey(this.m_pNativeCrypto, safeByteArray.ToPointer(), pPublicKeyBlob.Length, &ptr, &num));
				array = this.ConvertNativeArrayToManaged(ptr, num);
			}
			finally
			{
				if (safeByteArray != null)
				{
					safeByteArray.Close();
				}
				if (ptr != null)
				{
					<Module>.delete[]((void*)ptr);
				}
			}
			return array;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00008254 File Offset: 0x00007654
		public unsafe byte[] ExportPublicKey()
		{
			byte* ptr = null;
			byte[] array = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				int num = 0;
				Marshal.ThrowExceptionForHR(<Module>.RSNativeCrypto.ExportPublicKey(this.m_pNativeCrypto, &ptr, &num));
				array = this.ConvertNativeArrayToManaged(ptr, num);
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.delete[]((void*)ptr);
				}
			}
			return array;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000082BC File Offset: 0x000076BC
		public unsafe byte[] EncryptData(byte[] pClearText)
		{
			SafeByteArray safeByteArray = null;
			byte* ptr = null;
			byte[] array = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (!this.IsInit())
				{
					throw new Exception("Crypto not initialized");
				}
				safeByteArray = SafeByteArray.Create(pClearText);
				int num = pClearText.Length;
				ulong num2 = (ulong)((long)num);
				int num3 = 0;
				Marshal.ThrowExceptionForHR(<Module>.RSNativeCrypto.EncryptData(this.m_pNativeCrypto, safeByteArray.ToPointer(), num, &ptr, &num3));
				array = this.ConvertNativeArrayToManaged(ptr, num3);
			}
			finally
			{
				if (safeByteArray != null)
				{
					safeByteArray.ZeroArray();
					safeByteArray.Close();
				}
				if (ptr != null)
				{
					<Module>.delete[]((void*)ptr);
				}
			}
			return array;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00008360 File Offset: 0x00007760
		public unsafe byte[] DecryptData(byte[] pCipherText, [MarshalAs(UnmanagedType.U1)] bool useSalt)
		{
			SafeByteArray safeByteArray = null;
			byte* ptr = null;
			int num = 0;
			byte[] array = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (!this.IsInit())
				{
					throw new Exception("Crypto not initialized");
				}
				safeByteArray = SafeByteArray.Create(pCipherText);
				Marshal.ThrowExceptionForHR(<Module>.RSNativeCrypto.DecryptData(this.m_pNativeCrypto, safeByteArray.ToPointer(), pCipherText.Length, useSalt, &ptr, &num));
				array = this.ConvertNativeArrayToManaged(ptr, num);
			}
			finally
			{
				if (safeByteArray != null)
				{
					safeByteArray.Close();
				}
				if (ptr != null)
				{
					<Module>.RtlSecureZeroMemory((void*)ptr, (ulong)((long)num));
					<Module>.delete[]((void*)ptr);
				}
			}
			return array;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00008400 File Offset: 0x00007800
		public unsafe byte[] ReencryptSymmetricKey(byte[] symKeyBlob, byte[] pPublicKeyBlob)
		{
			SafeByteArray safeByteArray = null;
			SafeByteArray safeByteArray2 = null;
			byte* ptr = null;
			int num = 0;
			byte[] array = null;
			try
			{
				if (this.IsInit())
				{
					return null;
				}
				safeByteArray = SafeByteArray.Create(symKeyBlob);
				safeByteArray2 = SafeByteArray.Create(pPublicKeyBlob);
				Marshal.ThrowExceptionForHR(<Module>.RSNativeCrypto.ReencryptSymmetricKey(this.m_pNativeCrypto, safeByteArray.ToPointer(), symKeyBlob.Length, safeByteArray2.ToPointer(), pPublicKeyBlob.Length, &ptr, &num));
				array = this.ConvertNativeArrayToManaged(ptr, num);
			}
			finally
			{
				if (safeByteArray != null)
				{
					safeByteArray.Close();
				}
				if (safeByteArray2 != null)
				{
					safeByteArray2.Close();
				}
				if (ptr != null)
				{
					<Module>.delete[]((void*)ptr);
				}
			}
			return array;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00007E70 File Offset: 0x00007270
		[return: MarshalAs(UnmanagedType.U1)]
		public bool IsInit()
		{
			return <Module>.RSNativeCrypto.HasSymmetricKey(this.m_pNativeCrypto) != null;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000080A0 File Offset: 0x000074A0
		public static RSCrypto Load(Guid id)
		{
			string keyPath = StoreHelper.GetKeyPath(id);
			int num = keyPath.LastIndexOf("\\");
			string text = keyPath.Substring(0, num);
			string text2 = keyPath.Substring(num + 1);
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(text);
			if (registryKey == null)
			{
				return null;
			}
			byte[] array = (byte[])registryKey.GetValue(text2);
			if (array == null)
			{
				return null;
			}
			array = ProtectedData.Unprotect(array, null, DataProtectionScope.LocalMachine);
			RSCrypto rscrypto = new RSCrypto();
			rscrypto.InitializeInMemoryContext();
			rscrypto.ImportSymmetricKey(array, StoreHelper.GetSecondEntropy());
			return rscrypto;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00007F24 File Offset: 0x00007324
		private unsafe byte[] ConvertNativeArrayToManaged(byte* pNativeArray, int numBytes)
		{
			byte[] array = new byte[numBytes];
			Marshal.Copy((IntPtr)((void*)pNativeArray), array, 0, numBytes);
			return array;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000084C4 File Offset: 0x000078C4
		[HandleProcessCorruptedStateExceptions]
		protected void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.~RSCrypto();
			}
			else
			{
				try
				{
					this.!RSCrypto();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000852C File Offset: 0x0000792C
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00008510 File Offset: 0x00007910
		protected sealed override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x0400005A RID: 90
		private unsafe static ushort* KeyContainerNameCurrent = (ushort*)(&<Module>.??_C@_1HG@CPDDCFFG@?$AAM?$AAi?$AAc?$AAr?$AAo?$AAs?$AAo?$AAf?$AAt?$AA?5?$AAS?$AAQ?$AAL?$AA?5?$AAS@);

		// Token: 0x0400005B RID: 91
		private unsafe static ushort* KeyContainerNamePrevious = (ushort*)(&<Module>.??_C@_1GM@LACNADEC@?$AAM?$AAi?$AAc?$AAr?$AAo?$AAs?$AAo?$AAf?$AAt?$AA?5?$AAS?$AAQ?$AAL?$AA?5?$AAS@);

		// Token: 0x0400005C RID: 92
		private unsafe RSNativeCrypto* m_pNativeCrypto;
	}
}
