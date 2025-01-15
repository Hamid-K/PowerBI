using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client.AuthScheme.PoP;
using Microsoft.Identity.Client.Cache;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;
using Microsoft.Identity.Client.PlatformsCommon.Shared;
using Microsoft.Identity.Client.UI;
using Microsoft.Win32;

namespace Microsoft.Identity.Client.Platforms.netdesktop
{
	// Token: 0x02000187 RID: 391
	internal class NetDesktopPlatformProxy : AbstractPlatformProxy
	{
		// Token: 0x060012B7 RID: 4791 RVA: 0x0003FA08 File Offset: 0x0003DC08
		public NetDesktopPlatformProxy(ILoggerAdapter logger)
			: base(logger)
		{
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x0003FA14 File Offset: 0x0003DC14
		private static bool IsWindows
		{
			get
			{
				PlatformID platform = Environment.OSVersion.Platform;
				return platform <= PlatformID.WinCE;
			}
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0003FA33 File Offset: 0x0003DC33
		public override Task<string> GetUserPrincipalNameAsync()
		{
			return Task.FromResult<string>(NetDesktopPlatformProxy.GetUserPrincipalName(8));
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0003FA40 File Offset: 0x0003DC40
		private static string GetUserPrincipalName(int nameFormat)
		{
			uint num = 0U;
			WindowsNativeMethods.GetUserNameEx(nameFormat, null, ref num);
			if (num == 0U)
			{
				throw new MsalClientException("get_user_name_failed", "Failed to get user name. ", new Win32Exception(Marshal.GetLastWin32Error()));
			}
			StringBuilder stringBuilder = new StringBuilder((int)num);
			if (!WindowsNativeMethods.GetUserNameEx(nameFormat, stringBuilder, ref num))
			{
				throw new MsalClientException("get_user_name_failed", "Failed to get user name. ", new Win32Exception(Marshal.GetLastWin32Error()));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0003FAA8 File Offset: 0x0003DCA8
		public override string GetDefaultRedirectUri(string clientId, bool useRecommendedRedirectUri = false)
		{
			if (useRecommendedRedirectUri)
			{
				return "https://login.microsoftonline.com/common/oauth2/nativeclient";
			}
			return "urn:ietf:wg:oauth:2.0:oob";
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0003FAB8 File Offset: 0x0003DCB8
		public override ILegacyCachePersistence CreateLegacyCachePersistence()
		{
			return new InMemoryLegacyCachePersistance();
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0003FABF File Offset: 0x0003DCBF
		protected override IWebUIFactory CreateWebUiFactory()
		{
			return new NetDesktopWebUIFactory();
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0003FAC6 File Offset: 0x0003DCC6
		protected override string InternalGetDeviceModel()
		{
			return null;
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0003FACC File Offset: 0x0003DCCC
		protected override string InternalGetOperatingSystem()
		{
			if (!NetDesktopPlatformProxy.IsWindows)
			{
				return Environment.OSVersion.Platform.ToString();
			}
			return DesktopOsHelper.GetWindowsVersionString();
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0003FAFE File Offset: 0x0003DCFE
		protected override string InternalGetProcessorArchitecture()
		{
			if (!NetDesktopPlatformProxy.IsWindows)
			{
				return null;
			}
			return WindowsNativeMethods.GetProcessorArchitecture();
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0003FB0E File Offset: 0x0003DD0E
		protected override string InternalGetCallingApplicationName()
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			if (entryAssembly == null)
			{
				return null;
			}
			AssemblyName name = entryAssembly.GetName();
			if (name == null)
			{
				return null;
			}
			return name.Name;
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0003FB2B File Offset: 0x0003DD2B
		protected override string InternalGetCallingApplicationVersion()
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			if (entryAssembly == null)
			{
				return null;
			}
			AssemblyName name = entryAssembly.GetName();
			if (name == null)
			{
				return null;
			}
			Version version = name.Version;
			if (version == null)
			{
				return null;
			}
			return version.ToString();
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x0003FB54 File Offset: 0x0003DD54
		protected override string InternalGetDeviceId()
		{
			string text;
			try
			{
				text = (from nic in NetworkInterface.GetAllNetworkInterfaces()
					where nic.OperationalStatus == OperationalStatus.Up
					select nic).Select(delegate(NetworkInterface nic)
				{
					PhysicalAddress physicalAddress = nic.GetPhysicalAddress();
					if (physicalAddress == null)
					{
						return null;
					}
					return physicalAddress.ToString();
				}).FirstOrDefault<string>();
			}
			catch (EntryPointNotFoundException)
			{
				text = null;
			}
			return text;
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x0003FBCC File Offset: 0x0003DDCC
		protected override string InternalGetProductName()
		{
			return "MSAL.Desktop";
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0003FBD4 File Offset: 0x0003DDD4
		protected override string InternalGetRuntimeVersion()
		{
			string text2;
			try
			{
				string text = "SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\";
				using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(text))
				{
					if (registryKey != null && registryKey.GetValue("Release") != null)
					{
						int num = (int)registryKey.GetValue("Release");
						if (num >= 528040)
						{
							return "4.8 or later";
						}
						if (num >= 461808)
						{
							return "4.7.2";
						}
						if (num >= 461308)
						{
							return "4.7.1";
						}
						if (num >= 460798)
						{
							return "4.7";
						}
						if (num >= 394802)
						{
							return "4.6.2";
						}
						if (num >= 394254)
						{
							return "4.6.1";
						}
						if (num >= 393295)
						{
							return "4.6";
						}
						if (num >= 379893)
						{
							return "4.5.2";
						}
						if (num >= 378675)
						{
							return "4.5.1";
						}
						if (num >= 378389)
						{
							return "4.5";
						}
					}
				}
				text2 = string.Empty;
			}
			catch
			{
				text2 = string.Empty;
			}
			return text2;
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x0003FD20 File Offset: 0x0003DF20
		protected override ICryptographyManager InternalGetCryptographyManager()
		{
			return new CommonCryptographyManager(base.Logger);
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x0003FD2D File Offset: 0x0003DF2D
		protected override IPlatformLogger InternalGetPlatformLogger()
		{
			return new EventSourcePlatformLogger();
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0003FD34 File Offset: 0x0003DF34
		protected override IFeatureFlags CreateFeatureFlags()
		{
			return new NetDesktopFeatureFlags();
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0003FD3C File Offset: 0x0003DF3C
		public override Task StartDefaultOsBrowserAsync(string url, bool isBrokerConfigured)
		{
			try
			{
				Process.Start(new ProcessStartInfo
				{
					FileName = url,
					UseShellExecute = true
				});
			}
			catch
			{
				url = url.Replace("&", "^&");
				Process.Start(new ProcessStartInfo("cmd", "/c start " + url)
				{
					CreateNoWindow = true
				});
			}
			return Task.FromResult<int>(0);
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0003FDB4 File Offset: 0x0003DFB4
		public override IPoPCryptoProvider GetDefaultPoPCryptoProvider()
		{
			return PoPProviderFactory.GetOrCreateProvider();
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0003FDBB File Offset: 0x0003DFBB
		public override IDeviceAuthManager CreateDeviceAuthManager()
		{
			return new DeviceAuthManager(base.CryptographyManager);
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x0003FDC8 File Offset: 0x0003DFC8
		public override bool BrokerSupportsWamAccounts
		{
			get
			{
				return true;
			}
		}
	}
}
