using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Transactions;
using Microsoft.Win32;

namespace Microsoft.HostIntegration.XaClient
{
	// Token: 0x020006FD RID: 1789
	public class XaClient
	{
		// Token: 0x060038DA RID: 14554 RVA: 0x000BE700 File Offset: 0x000BC900
		static XaClient()
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Host Integration Server\\XADLL");
			if (registryKey == null)
			{
				return;
			}
			string text = registryKey.GetValue("MSHISXAC.DLL") as string;
			if (text == null)
			{
				return;
			}
			text = Environment.ExpandEnvironmentVariables(text);
			text = text.Trim();
			if (text.Length == 0)
			{
				return;
			}
			if (!text.EndsWith("MSHISXAC.DLL", StringComparison.InvariantCultureIgnoreCase))
			{
				return;
			}
			if (!File.Exists(text))
			{
				return;
			}
			text = text.Substring(0, text.Length - "MSHISXAC.DLL".Length);
			if (text.Length != 0)
			{
				if (!text.EndsWith("\\", StringComparison.InvariantCultureIgnoreCase))
				{
					return;
				}
				try
				{
					if (!NativeMethods.SetDllDirectory(text))
					{
						return;
					}
				}
				catch (Exception)
				{
					return;
				}
			}
			XaClient.foundUnmanagedCode = true;
			int num = NativeMethods.InitializeAppDomain(AppDomain.CurrentDomain.FriendlyName, out XaClient.typeHdl);
			if (num != 0)
			{
				throw new CustomXaClientException(SR.InitializeAppDomainFailed(Marshal.GetExceptionForHR(num).Message));
			}
			AppDomain.CurrentDomain.DomainUnload += XaClient.Uninitialize;
			AppDomain.CurrentDomain.ProcessExit += XaClient.Uninitialize;
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x000BE818 File Offset: 0x000BCA18
		private static void Uninitialize(object sender, EventArgs e)
		{
			if (XaClient.typeHdl != (IntPtr)0)
			{
				NativeMethods.UninitializeAppDomain(XaClient.typeHdl);
			}
			XaClient.typeHdl = 0;
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x000BE840 File Offset: 0x000BCA40
		public static Xid EnlistXa(IXaClientEnlistment clientEnlistment, string recoveryInformation)
		{
			if (Transaction.Current == null)
			{
				throw new CustomXaClientException(SR.NoCurrentTransaction);
			}
			if (!XaClient.foundUnmanagedCode)
			{
				throw new CustomXaClientException(SR.NoUnmanagedCode);
			}
			int num2;
			int num = NativeMethods.RegisterRecoveryInformation(recoveryInformation, XaClient.typeHdl, out num2);
			if (num != 0)
			{
				throw new CustomXaClientException(SR.RegisterRecoveryInformationFailed(Marshal.GetExceptionForHR(num).Message));
			}
			ManagedDispatcher.AddInformation(num2, clientEnlistment);
			int num3;
			int num4;
			int num5;
			byte[] array;
			num = NativeMethods.EnlistInXaTransaction(TransactionInterop.GetDtcTransaction(Transaction.Current), num2, out num3, out num4, out num5, out array);
			if (num != 0)
			{
				ManagedDispatcher.RemoveInformation(num2);
				throw new CustomXaClientException(SR.EnlistInXaTransactionFailed(Marshal.GetExceptionForHR(num).Message));
			}
			return new Xid(num3, num4, num5, array);
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x000BE8E8 File Offset: 0x000BCAE8
		public static void EnlistXa(IXaClientStartEnlistment clientEnlistment, string recoveryInformation)
		{
			if (Transaction.Current == null)
			{
				throw new CustomXaClientException(SR.NoCurrentTransaction);
			}
			if (!XaClient.foundUnmanagedCode)
			{
				throw new CustomXaClientException(SR.NoUnmanagedCode);
			}
			int num2;
			int num = NativeMethods.RegisterRecoveryInformation(recoveryInformation, XaClient.typeHdl, out num2);
			if (num != 0)
			{
				throw new CustomXaClientException(SR.RegisterRecoveryInformationFailed(Marshal.GetExceptionForHR(num).Message));
			}
			ManagedDispatcher.AddInformation(num2, clientEnlistment);
			int num3;
			int num4;
			int num5;
			byte[] array;
			num = NativeMethods.EnlistInXaTransaction(TransactionInterop.GetDtcTransaction(Transaction.Current), num2, out num3, out num4, out num5, out array);
			if (num != 0)
			{
				ManagedDispatcher.RemoveInformation(num2);
				throw new CustomXaClientException(SR.EnlistInXaTransactionFailed(Marshal.GetExceptionForHR(num).Message));
			}
			Xid xid = new Xid(num3, num4, num5, array);
			clientEnlistment.Start(xid);
		}

		// Token: 0x040020F7 RID: 8439
		private static bool foundUnmanagedCode;

		// Token: 0x040020F8 RID: 8440
		internal static IntPtr typeHdl;
	}
}
