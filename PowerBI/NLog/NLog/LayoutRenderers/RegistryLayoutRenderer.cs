using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using Microsoft.Win32;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000E4 RID: 228
	[LayoutRenderer("registry")]
	public class RegistryLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000D52 RID: 3410 RVA: 0x00021E4F File Offset: 0x0002004F
		public RegistryLayoutRenderer()
		{
			this.RequireEscapingSlashesInDefaultValue = true;
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x00021E5E File Offset: 0x0002005E
		// (set) Token: 0x06000D54 RID: 3412 RVA: 0x00021E66 File Offset: 0x00020066
		public Layout Value { get; set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x00021E6F File Offset: 0x0002006F
		// (set) Token: 0x06000D56 RID: 3414 RVA: 0x00021E77 File Offset: 0x00020077
		public Layout DefaultValue { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x00021E80 File Offset: 0x00020080
		// (set) Token: 0x06000D58 RID: 3416 RVA: 0x00021E88 File Offset: 0x00020088
		[DefaultValue(true)]
		public bool RequireEscapingSlashesInDefaultValue { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x00021E91 File Offset: 0x00020091
		// (set) Token: 0x06000D5A RID: 3418 RVA: 0x00021E99 File Offset: 0x00020099
		[DefaultValue("Default")]
		public RegistryView View { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x00021EA2 File Offset: 0x000200A2
		// (set) Token: 0x06000D5C RID: 3420 RVA: 0x00021EAA File Offset: 0x000200AA
		[RequiredParameter]
		public Layout Key { get; set; }

		// Token: 0x06000D5D RID: 3421 RVA: 0x00021EB4 File Offset: 0x000200B4
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			object obj = null;
			Layout value = this.Value;
			string text = ((value != null) ? value.Render(logEvent) : null);
			RegistryLayoutRenderer.ParseResult parseResult = RegistryLayoutRenderer.ParseKey(this.Key.Render(logEvent));
			try
			{
				using (RegistryKey registryKey = RegistryKey.OpenBaseKey(parseResult.Hive, this.View))
				{
					if (parseResult.HasSubKey)
					{
						using (RegistryKey registryKey2 = registryKey.OpenSubKey(parseResult.SubKey))
						{
							if (registryKey2 != null)
							{
								obj = registryKey2.GetValue(text);
							}
							goto IL_0076;
						}
					}
					obj = registryKey.GetValue(text);
					IL_0076:;
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Error("Error when writing to registry");
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
			string text2 = null;
			if (obj != null)
			{
				text2 = Convert.ToString(obj, CultureInfo.InvariantCulture);
			}
			else if (this.DefaultValue != null)
			{
				text2 = this.DefaultValue.Render(logEvent);
				if (this.RequireEscapingSlashesInDefaultValue)
				{
					text2 = text2.Replace("\\\\", "\\");
				}
			}
			builder.Append(text2);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00021FD0 File Offset: 0x000201D0
		private static RegistryLayoutRenderer.ParseResult ParseKey(string key)
		{
			int num = key.IndexOfAny(new char[] { '\\', '/' });
			string text = null;
			string text2;
			if (num >= 0)
			{
				text2 = key.Substring(0, num);
				text = key.Substring(num + 1).Replace('/', '\\');
				text = text.TrimStart(new char[] { '\\' });
				text = text.Replace("\\\\", "\\");
			}
			else
			{
				text2 = key;
			}
			RegistryHive registryHive = RegistryLayoutRenderer.ParseHiveName(text2);
			return new RegistryLayoutRenderer.ParseResult
			{
				SubKey = text,
				Hive = registryHive
			};
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00022058 File Offset: 0x00020258
		private static RegistryHive ParseHiveName(string hiveName)
		{
			RegistryHive registryHive;
			if (RegistryLayoutRenderer.HiveAliases.TryGetValue(hiveName, out registryHive))
			{
				return registryHive;
			}
			throw new ArgumentException("Key name is not supported. Root hive '" + hiveName + "' not recognized.");
		}

		// Token: 0x04000399 RID: 921
		private static readonly Dictionary<string, RegistryHive> HiveAliases = new Dictionary<string, RegistryHive>(StringComparer.InvariantCultureIgnoreCase)
		{
			{
				"HKEY_LOCAL_MACHINE",
				RegistryHive.LocalMachine
			},
			{
				"HKLM",
				RegistryHive.LocalMachine
			},
			{
				"HKEY_CURRENT_USER",
				RegistryHive.CurrentUser
			},
			{
				"HKCU",
				RegistryHive.CurrentUser
			},
			{
				"HKEY_CLASSES_ROOT",
				RegistryHive.ClassesRoot
			},
			{
				"HKEY_USERS",
				RegistryHive.Users
			},
			{
				"HKEY_CURRENT_CONFIG",
				RegistryHive.CurrentConfig
			},
			{
				"HKEY_DYN_DATA",
				RegistryHive.DynData
			},
			{
				"HKEY_PERFORMANCE_DATA",
				RegistryHive.PerformanceData
			}
		};

		// Token: 0x0200025E RID: 606
		private class ParseResult
		{
			// Token: 0x1700040D RID: 1037
			// (get) Token: 0x060015F9 RID: 5625 RVA: 0x00039D88 File Offset: 0x00037F88
			// (set) Token: 0x060015FA RID: 5626 RVA: 0x00039D90 File Offset: 0x00037F90
			public string SubKey { get; set; }

			// Token: 0x1700040E RID: 1038
			// (get) Token: 0x060015FB RID: 5627 RVA: 0x00039D99 File Offset: 0x00037F99
			// (set) Token: 0x060015FC RID: 5628 RVA: 0x00039DA1 File Offset: 0x00037FA1
			public RegistryHive Hive { get; set; }

			// Token: 0x1700040F RID: 1039
			// (get) Token: 0x060015FD RID: 5629 RVA: 0x00039DAA File Offset: 0x00037FAA
			public bool HasSubKey
			{
				get
				{
					return !string.IsNullOrEmpty(this.SubKey);
				}
			}
		}
	}
}
