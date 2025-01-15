using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.Win32;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000056 RID: 86
	internal sealed class RegistryTelemetryConfiguration : ITelemetryConfiguration
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000C238 File Offset: 0x0000A438
		public bool IsEnabled
		{
			get
			{
				return this.ReadRegKey("CustomerFeedback") as int? == 1;
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000C270 File Offset: 0x0000A470
		public bool IsInternal()
		{
			string unsafeHostName = this.GetUnsafeHostName();
			foreach (string text in new string[] { ".microsoft.com", ".sys-sqlsvr.local" })
			{
				if (unsafeHostName.EndsWith(text, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000C2BC File Offset: 0x0000A4BC
		public bool IsPublicBuild
		{
			get
			{
				if (RegistryTelemetryConfiguration.InitialRun)
				{
					int? num = this.ReadRegKey("PrivateBuild") as int?;
					RegistryTelemetryConfiguration._isPublicBuild = num == null || num != 1;
				}
				RegistryTelemetryConfiguration.InitialRun = RegistryTelemetryConfiguration.InitialRun && false;
				return RegistryTelemetryConfiguration._isPublicBuild;
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000C327 File Offset: 0x0000A527
		internal string GetUnsafeHostName()
		{
			return Dns.GetHostEntry("LocalHost").HostName.ToLowerInvariant();
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000C340 File Offset: 0x0000A540
		public string GetSHA256Hash(string input)
		{
			if (input == null)
			{
				input = string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				foreach (byte b in sha256Cng.ComputeHash(Encoding.UTF8.GetBytes(input)))
				{
					stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		private string SqlRegistryRoot
		{
			get
			{
				string text = string.Format(CultureInfo.InvariantCulture, "Software\\Microsoft\\Microsoft SQL Server\\{0}\\", ProcessingContext.Configuration.InstanceID + "\\CPE");
				RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Reading CPE key from {0}", new object[] { text });
				return text;
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000C410 File Offset: 0x0000A610
		private object ReadRegKey(string valueName)
		{
			object val = null;
			RevertImpersonationContext.Run(delegate
			{
				RegistryKey registryKey = null;
				try
				{
					registryKey = Registry.LocalMachine.OpenSubKey(this.SqlRegistryRoot);
					if (registryKey != null)
					{
						val = registryKey.GetValue(valueName);
					}
				}
				finally
				{
					if (registryKey != null)
					{
						registryKey.Close();
					}
				}
			});
			return val;
		}

		// Token: 0x040002C8 RID: 712
		private static bool InitialRun = true;

		// Token: 0x040002C9 RID: 713
		private static bool _isPublicBuild = true;

		// Token: 0x040002CA RID: 714
		private const string REGISTRY_ROOT = "Software\\Microsoft\\Microsoft SQL Server\\{0}\\";

		// Token: 0x040002CB RID: 715
		private const string IS_ENABLED = "CustomerFeedback";

		// Token: 0x040002CC RID: 716
		private const string PRIVATE_BUILD = "PrivateBuild";
	}
}
