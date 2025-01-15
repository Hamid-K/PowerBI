using System;
using System.Reflection;
using Microsoft.Win32;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BF6 RID: 7158
	public static class FxVersionDetector
	{
		// Token: 0x17002CD4 RID: 11476
		// (get) Token: 0x0600B2A8 RID: 45736 RVA: 0x00245E91 File Offset: 0x00244091
		public static ClrVersion InstalledFxVersion
		{
			get
			{
				if (FxVersionDetector.installedFxVersion == null)
				{
					FxVersionDetector.installedFxVersion = new ClrVersion?(FxVersionDetector.DetectInstalledFxVersion());
				}
				return FxVersionDetector.installedFxVersion.Value;
			}
		}

		// Token: 0x17002CD5 RID: 11477
		// (get) Token: 0x0600B2A9 RID: 45737 RVA: 0x00245EB8 File Offset: 0x002440B8
		public static ClrVersion FxVersion
		{
			get
			{
				if (FxVersionDetector.fxVersion == null)
				{
					FxVersionDetector.fxVersion = new ClrVersion?(FxVersionDetector.DetectFxVersion());
				}
				return FxVersionDetector.fxVersion.Value;
			}
		}

		// Token: 0x17002CD6 RID: 11478
		// (get) Token: 0x0600B2AA RID: 45738 RVA: 0x00245EDF File Offset: 0x002440DF
		public static string FrameworkNameForTelemetry
		{
			get
			{
				return FxVersionDetector.FrameworkName ?? "(null)";
			}
		}

		// Token: 0x17002CD7 RID: 11479
		// (get) Token: 0x0600B2AB RID: 45739 RVA: 0x00245EEF File Offset: 0x002440EF
		public static int InstalledFxBuild
		{
			get
			{
				if (FxVersionDetector.installedFxBuild == null)
				{
					FxVersionDetector.installedFxBuild = new int?((FxVersionDetector.InstalledFxVersion != ClrVersion.Net45) ? 0 : FxVersionDetector.DetectInstalledFxBuild());
				}
				return FxVersionDetector.installedFxBuild.Value;
			}
		}

		// Token: 0x17002CD8 RID: 11480
		// (get) Token: 0x0600B2AC RID: 45740 RVA: 0x00245F21 File Offset: 0x00244121
		private static string FrameworkName
		{
			get
			{
				return FxVersionDetector.frameworkName.Value;
			}
		}

		// Token: 0x0600B2AD RID: 45741 RVA: 0x00245F30 File Offset: 0x00244130
		private static ClrVersion DetectInstalledFxVersion()
		{
			if (!(FxVersionDetector.frameworkProperty.Value == null))
			{
				return ClrVersion.Net45;
			}
			string imageRuntimeVersion = typeof(object).Assembly.ImageRuntimeVersion;
			if (imageRuntimeVersion.Length <= 1 || imageRuntimeVersion[1] != '4')
			{
				return ClrVersion.Net35;
			}
			return ClrVersion.Net40;
		}

		// Token: 0x0600B2AE RID: 45742 RVA: 0x00245F80 File Offset: 0x00244180
		private static ClrVersion DetectFxVersion()
		{
			if (FxVersionDetector.InstalledFxVersion < ClrVersion.Net45)
			{
				return FxVersionDetector.InstalledFxVersion;
			}
			if (FxVersionDetector.FrameworkName != null)
			{
				int num = FxVersionDetector.FrameworkName.IndexOf("Version=v", StringComparison.Ordinal);
				if (num >= 0 && FxVersionDetector.FrameworkName[num + 9] == '4' && FxVersionDetector.FrameworkName[num + 10] == '.')
				{
					if (FxVersionDetector.FrameworkName[num + 11] == '0')
					{
						return ClrVersion.Net40;
					}
					if (FxVersionDetector.FrameworkName[num + 11] >= '5')
					{
						return ClrVersion.Net45;
					}
				}
			}
			return ClrVersion.Net40;
		}

		// Token: 0x0600B2AF RID: 45743 RVA: 0x00246004 File Offset: 0x00244204
		private static int DetectInstalledFxBuild()
		{
			using (RegistryKey registryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, string.Empty).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
			{
				if (registryKey != null && registryKey.GetValue("Release") != null)
				{
					return (int)registryKey.GetValue("Release");
				}
			}
			return 0;
		}

		// Token: 0x04005B47 RID: 23367
		public const int NetFx46 = 393295;

		// Token: 0x04005B48 RID: 23368
		public const int NetFx462 = 394802;

		// Token: 0x04005B49 RID: 23369
		private static ClrVersion? installedFxVersion;

		// Token: 0x04005B4A RID: 23370
		private static ClrVersion? fxVersion;

		// Token: 0x04005B4B RID: 23371
		private static int? installedFxBuild;

		// Token: 0x04005B4C RID: 23372
		private static readonly Lazy<string> frameworkName = new Lazy<string>(delegate
		{
			if (FxVersionDetector.frameworkProperty.Value == null)
			{
				return null;
			}
			return FxVersionDetector.frameworkProperty.Value.GetValue(AppDomain.CurrentDomain.SetupInformation, null) as string;
		});

		// Token: 0x04005B4D RID: 23373
		private static readonly Lazy<PropertyInfo> frameworkProperty = new Lazy<PropertyInfo>(() => typeof(object).Assembly.GetType("System.AppDomainSetup").GetProperty("TargetFrameworkName"));
	}
}
