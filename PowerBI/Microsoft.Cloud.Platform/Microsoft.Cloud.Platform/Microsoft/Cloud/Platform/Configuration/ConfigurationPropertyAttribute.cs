using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Microsoft.Cloud.Platform.Configuration
{
	// Token: 0x02000420 RID: 1056
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class ConfigurationPropertyAttribute : Attribute
	{
		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600208C RID: 8332 RVA: 0x0007AAF5 File Offset: 0x00078CF5
		// (set) Token: 0x0600208D RID: 8333 RVA: 0x0007AAFD File Offset: 0x00078CFD
		public bool Encrypt { get; set; }

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600208E RID: 8334 RVA: 0x0007AB06 File Offset: 0x00078D06
		// (set) Token: 0x0600208F RID: 8335 RVA: 0x0007AB0E File Offset: 0x00078D0E
		public bool IsValidClientId { get; set; }

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06002090 RID: 8336 RVA: 0x0007AB17 File Offset: 0x00078D17
		// (set) Token: 0x06002091 RID: 8337 RVA: 0x0007AB1F File Offset: 0x00078D1F
		public bool OnlyLowercase { get; set; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06002092 RID: 8338 RVA: 0x0007AB28 File Offset: 0x00078D28
		// (set) Token: 0x06002093 RID: 8339 RVA: 0x0007AB30 File Offset: 0x00078D30
		public bool LoadToFile { get; set; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06002094 RID: 8340 RVA: 0x0007AB39 File Offset: 0x00078D39
		// (set) Token: 0x06002095 RID: 8341 RVA: 0x0007AB41 File Offset: 0x00078D41
		public bool LoadToParent { get; set; }

		// Token: 0x06002096 RID: 8342 RVA: 0x0007AB4A File Offset: 0x00078D4A
		public ConfigurationPropertyAttribute()
		{
			this.Encrypt = false;
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x0007AB59 File Offset: 0x00078D59
		public static bool IsDefined(PropertyInfo property)
		{
			return Attribute.IsDefined(property, typeof(ConfigurationPropertyAttribute));
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x0007AB6B File Offset: 0x00078D6B
		public static bool ShouldEncrypt(PropertyInfo property)
		{
			return ((ConfigurationPropertyAttribute)Attribute.GetCustomAttribute(property, typeof(ConfigurationPropertyAttribute))).Encrypt;
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x0007AB87 File Offset: 0x00078D87
		public static bool ContainsValidClientId(PropertyInfo property)
		{
			return ((ConfigurationPropertyAttribute)Attribute.GetCustomAttribute(property, typeof(ConfigurationPropertyAttribute))).IsValidClientId;
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x0007ABA3 File Offset: 0x00078DA3
		public static bool ShouldBeOnlyLowercase(PropertyInfo property)
		{
			return ((ConfigurationPropertyAttribute)Attribute.GetCustomAttribute(property, typeof(ConfigurationPropertyAttribute))).OnlyLowercase;
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x0007ABBF File Offset: 0x00078DBF
		public static bool ShouldLoadToFile(PropertyInfo property)
		{
			return ((ConfigurationPropertyAttribute)Attribute.GetCustomAttribute(property, typeof(ConfigurationPropertyAttribute))).LoadToFile;
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x0007ABDB File Offset: 0x00078DDB
		public static bool ShouldLoadToParent(PropertyInfo property)
		{
			return ((ConfigurationPropertyAttribute)Attribute.GetCustomAttribute(property, typeof(ConfigurationPropertyAttribute))).LoadToParent;
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x0007ABF8 File Offset: 0x00078DF8
		public static List<PropertyInfo> GetConfigurationProperties(Type type)
		{
			Dictionary<Type, List<PropertyInfo>> dictionary = ConfigurationPropertyAttribute.s_typeProperties;
			List<PropertyInfo> list;
			lock (dictionary)
			{
				if (ConfigurationPropertyAttribute.s_typeProperties.TryGetValue(type, out list))
				{
					return list;
				}
				list = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(new Func<PropertyInfo, bool>(ConfigurationPropertyAttribute.IsDefined)).ToList<PropertyInfo>();
				ConfigurationPropertyAttribute.s_typeProperties.Add(type, list);
			}
			return list;
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x0007AC74 File Offset: 0x00078E74
		public static List<PropertyInfo> GetConfigurationProperties(object o)
		{
			return ConfigurationPropertyAttribute.GetConfigurationProperties(o.GetType());
		}

		// Token: 0x04000B35 RID: 2869
		private static readonly Dictionary<Type, List<PropertyInfo>> s_typeProperties = new Dictionary<Type, List<PropertyInfo>>();
	}
}
