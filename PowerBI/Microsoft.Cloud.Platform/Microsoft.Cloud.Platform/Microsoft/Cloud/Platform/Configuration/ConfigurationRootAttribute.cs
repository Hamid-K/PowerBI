using System;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Configuration
{
	// Token: 0x0200041E RID: 1054
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class ConfigurationRootAttribute : Attribute
	{
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06002083 RID: 8323 RVA: 0x0007AA00 File Offset: 0x00078C00
		// (set) Token: 0x06002084 RID: 8324 RVA: 0x0007AA08 File Offset: 0x00078C08
		public string Consumers { get; set; }

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06002085 RID: 8325 RVA: 0x0007AA11 File Offset: 0x00078C11
		public string ConsumersRegex
		{
			get
			{
				return this.Consumers.TrimEnd(new char[] { '|' });
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06002086 RID: 8326 RVA: 0x0007AA29 File Offset: 0x00078C29
		// (set) Token: 0x06002087 RID: 8327 RVA: 0x0007AA31 File Offset: 0x00078C31
		public ConfigurationOptions Options { get; set; }

		// Token: 0x06002088 RID: 8328 RVA: 0x0007AA3A File Offset: 0x00078C3A
		public ConfigurationRootAttribute()
		{
			this.Consumers = string.Empty;
			this.Options = ConfigurationOptions.None;
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x0007AA54 File Offset: 0x00078C54
		public static bool IsConfigurationRoot([NotNull] Type type)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Type>(type, "type");
			return Attribute.IsDefined(type, typeof(ConfigurationRootAttribute));
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x0007AA74 File Offset: 0x00078C74
		public static ConfigurationRootAttribute GetConfigurationRootDefinition([NotNull] Type type)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Type>(type, "type");
			if (!ConfigurationRootAttribute.IsConfigurationRoot(type))
			{
				throw new CCSValidationException("Type {0} is not a configuration root".FormatWithCurrentCulture(new object[] { type }));
			}
			return (ConfigurationRootAttribute)Attribute.GetCustomAttribute(type, typeof(ConfigurationRootAttribute));
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x0007AAC3 File Offset: 0x00078CC3
		public static bool IsAutoReconfigure([NotNull] Type type)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Type>(type, "type");
			return ConfigurationRootAttribute.IsConfigurationRoot(type) && ConfigurationRootAttribute.GetConfigurationRootDefinition(type).Options.HasFlag(ConfigurationOptions.AutoReconfigure);
		}

		// Token: 0x04000B2E RID: 2862
		public const string AllConsumers = ".*";

		// Token: 0x04000B2F RID: 2863
		private const char c_consumersListSeparator = '|';
	}
}
