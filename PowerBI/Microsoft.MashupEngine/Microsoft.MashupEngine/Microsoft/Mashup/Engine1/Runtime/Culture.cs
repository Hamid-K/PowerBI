using System;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012BE RID: 4798
	internal static class Culture
	{
		// Token: 0x06007E19 RID: 32281 RVA: 0x001B038E File Offset: 0x001AE58E
		public static ICulture GetDefaultCulture(IEngineHost host)
		{
			return host.QueryService<ICultureService>().DefaultCulture;
		}

		// Token: 0x06007E1A RID: 32282 RVA: 0x001B039B File Offset: 0x001AE59B
		public static ICulture GetCulture(IEngineHost host, Value specifiedCultureName, ICulture defaultCulture)
		{
			if (!specifiedCultureName.IsNull)
			{
				return Culture.GetCulture(host, specifiedCultureName);
			}
			return defaultCulture;
		}

		// Token: 0x06007E1B RID: 32283 RVA: 0x001B03AE File Offset: 0x001AE5AE
		public static ICulture GetCulture(IEngineHost host, Value cultureName)
		{
			return host.QueryService<ICultureService>().GetCulture(cultureName.AsString);
		}

		// Token: 0x06007E1C RID: 32284 RVA: 0x001B03C4 File Offset: 0x001AE5C4
		public static Value GetDefaultCultureName(IEngineHost host)
		{
			ICulture defaultCulture = Culture.GetDefaultCulture(host);
			if (defaultCulture == null)
			{
				return Value.Null;
			}
			return TextValue.NewOrNull(defaultCulture.Name);
		}

		// Token: 0x06007E1D RID: 32285 RVA: 0x001B03EC File Offset: 0x001AE5EC
		public static CultureInfo GetCultureInfo(IEngineHost host, Value specifiedCultureName, ICulture defaultCulture)
		{
			if (!specifiedCultureName.IsNull)
			{
				return Culture.GetCultureInfo(host, specifiedCultureName);
			}
			return defaultCulture.GetCultureInfo();
		}

		// Token: 0x06007E1E RID: 32286 RVA: 0x001B0404 File Offset: 0x001AE604
		public static CultureInfo GetCultureInfo(this ICulture culture)
		{
			if (culture.Value == null)
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.UnsupportedCulture, TextValue.NewOrNull(culture.Name), null);
			}
			return culture.Value;
		}

		// Token: 0x06007E1F RID: 32287 RVA: 0x001B042B File Offset: 0x001AE62B
		public static ICulture ResolveCulture(IEngineHost engineHost, Value culture)
		{
			return Culture.GetCulture(engineHost, culture, Culture.GetDefaultCulture(engineHost));
		}

		// Token: 0x06007E20 RID: 32288 RVA: 0x001B043A File Offset: 0x001AE63A
		private static CultureInfo GetCultureInfo(IEngineHost host, Value cultureName)
		{
			if (cultureName.AsString.Length == 0)
			{
				return CultureInfo.InvariantCulture;
			}
			return Culture.GetCulture(host, cultureName).GetCultureInfo();
		}
	}
}
