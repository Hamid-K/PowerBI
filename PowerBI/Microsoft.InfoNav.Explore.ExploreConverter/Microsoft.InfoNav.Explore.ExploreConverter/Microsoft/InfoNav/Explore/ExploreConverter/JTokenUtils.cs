using System;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace Microsoft.InfoNav.Explore.ExploreConverter
{
	// Token: 0x02000006 RID: 6
	internal static class JTokenUtils
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002280 File Offset: 0x00000480
		internal static decimal ParseDecimal(JToken value, decimal defaultValue)
		{
			string text = JTokenUtils.ParseString(value);
			decimal num;
			if (text != null && decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
			{
				return num;
			}
			return defaultValue;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022AE File Offset: 0x000004AE
		internal static string ParseString(JToken value)
		{
			if (value != null)
			{
				return value.Value<string>();
			}
			return null;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022BB File Offset: 0x000004BB
		internal static bool ParseBool(JToken value, bool defaultValue = false)
		{
			if (value != null)
			{
				return value.Value<bool>();
			}
			return defaultValue;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022C8 File Offset: 0x000004C8
		internal static bool? ParseBoolNullable(JToken value)
		{
			if (value != null)
			{
				return new bool?(value.Value<bool>());
			}
			return null;
		}
	}
}
