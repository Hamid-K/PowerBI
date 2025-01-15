using System;
using System.Globalization;

namespace System.Web.Http.ValueProviders.Providers
{
	// Token: 0x02000042 RID: 66
	internal sealed class ElementalValueProvider : IValueProvider
	{
		// Token: 0x060001CA RID: 458 RVA: 0x00005EC9 File Offset: 0x000040C9
		public ElementalValueProvider(string name, object rawValue, CultureInfo culture)
		{
			this.Name = name;
			this.RawValue = rawValue;
			this.Culture = culture;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00005EE6 File Offset: 0x000040E6
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00005EEE File Offset: 0x000040EE
		public CultureInfo Culture { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00005EF7 File Offset: 0x000040F7
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00005EFF File Offset: 0x000040FF
		public string Name { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00005F08 File Offset: 0x00004108
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00005F10 File Offset: 0x00004110
		public object RawValue { get; private set; }

		// Token: 0x060001D1 RID: 465 RVA: 0x00005F19 File Offset: 0x00004119
		public bool ContainsPrefix(string prefix)
		{
			return PrefixContainer.IsPrefixMatch(prefix, this.Name);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00005F27 File Offset: 0x00004127
		public ValueProviderResult GetValue(string key)
		{
			if (!string.Equals(key, this.Name, StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}
			return new ValueProviderResult(this.RawValue, Convert.ToString(this.RawValue, this.Culture), this.Culture);
		}
	}
}
