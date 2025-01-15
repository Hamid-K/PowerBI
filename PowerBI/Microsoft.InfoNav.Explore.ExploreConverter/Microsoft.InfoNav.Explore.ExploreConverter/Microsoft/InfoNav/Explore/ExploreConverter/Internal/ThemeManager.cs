using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000BC RID: 188
	internal sealed class ThemeManager
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x0001444C File Offset: 0x0001264C
		internal ThemeManager()
		{
			string text;
			using (StreamReader streamReader = new StreamReader(base.GetType().Assembly.GetManifestResourceStream("Microsoft.InfoNav.Explore.ExploreConverter.Themes.json")))
			{
				text = streamReader.ReadToEnd();
			}
			Theme[] array = ThemeManager.ParseThemes(text);
			this._themes = ThemeManager.ConvertFromListToDictoniary(array).ToReadOnlyDictionary<string, Theme>();
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x000144B8 File Offset: 0x000126B8
		private static Dictionary<string, Theme> ConvertFromListToDictoniary(IEnumerable<Theme> themes)
		{
			if (themes == null)
			{
				return null;
			}
			Dictionary<string, Theme> dictionary = new Dictionary<string, Theme>();
			foreach (Theme theme in themes)
			{
				dictionary[theme.Name] = theme;
			}
			return dictionary;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00014514 File Offset: 0x00012714
		private static Theme[] ParseThemes(string themeJson)
		{
			return new DataContractJsonSerializer(typeof(Theme[])).FromJsonString(themeJson);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001452C File Offset: 0x0001272C
		public Theme GetTheme(string themeName)
		{
			Theme theme = null;
			if (this._themes != null && this._themes.TryGetValue(themeName, out theme))
			{
				return theme;
			}
			return null;
		}

		// Token: 0x040002AE RID: 686
		private readonly ReadOnlyDictionary<string, Theme> _themes;
	}
}
