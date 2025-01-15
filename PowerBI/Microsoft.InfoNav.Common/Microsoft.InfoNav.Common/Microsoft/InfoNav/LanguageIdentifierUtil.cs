using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Microsoft.InfoNav
{
	// Token: 0x0200001F RID: 31
	public static class LanguageIdentifierUtil
	{
		// Token: 0x060001CF RID: 463 RVA: 0x00005A88 File Offset: 0x00003C88
		public static bool TryAsLanguageIdentifier(string name, out LanguageIdentifier result)
		{
			if (name == null)
			{
				result = (LanguageIdentifier)0;
				return false;
			}
			bool flag;
			try
			{
				CultureInfo cultureInfo = CultureInfo.GetCultureInfo(name);
				result = (LanguageIdentifier)cultureInfo.LCID;
				flag = true;
			}
			catch (CultureNotFoundException)
			{
				result = (LanguageIdentifier)0;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005ACC File Offset: 0x00003CCC
		public static LanguageIdentifier ToLanguageIdentifier(string language)
		{
			LanguageIdentifier languageIdentifier;
			if (!LanguageIdentifierUtil.TryAsLanguageIdentifier(language, out languageIdentifier))
			{
				throw Contract.ExceptParam("language");
			}
			return languageIdentifier;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005AEF File Offset: 0x00003CEF
		public static LanguageIdentifier ToLanguageIdentifier(this CultureInfo cultureInfo)
		{
			return (LanguageIdentifier)cultureInfo.LCID;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00005AF7 File Offset: 0x00003CF7
		public static CultureInfo ToCultureInfo(this LanguageIdentifier language)
		{
			return CultureInfo.GetCultureInfo((int)language);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00005AFF File Offset: 0x00003CFF
		public static string ToLanguageName(this LanguageIdentifier language)
		{
			CultureInfo cultureInfo = language.ToCultureInfo();
			Contract.CheckValue<CultureInfo>(cultureInfo, "culture", "ToCultureInfo never returns null so this should never happen.");
			return cultureInfo.Name;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00005B1C File Offset: 0x00003D1C
		public static string ToEnglishName(this LanguageIdentifier language)
		{
			CultureInfo cultureInfo = language.ToCultureInfo();
			Contract.CheckValue<CultureInfo>(cultureInfo, "culture", "ToCultureInfo never returns null so this should never happen.");
			return cultureInfo.EnglishName;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00005B3C File Offset: 0x00003D3C
		public static LanguageIdentifier GetParentLanguage(this LanguageIdentifier language)
		{
			CultureInfo cultureInfo = language.ToCultureInfo();
			if (cultureInfo.IsNeutralCulture)
			{
				return language;
			}
			return cultureInfo.Parent.ToLanguageIdentifier();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00005B65 File Offset: 0x00003D65
		public static bool IsCompatibleWith(this LanguageIdentifier language, LanguageIdentifier other)
		{
			return language.GetParentLanguage() == other.GetParentLanguage();
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00005B75 File Offset: 0x00003D75
		public static bool TryFindDefaultLocale(this LanguageIdentifier parentId, out LanguageIdentifier localeId)
		{
			return LanguageIdentifierUtil._defaultLocaleMap.TryGetValue(parentId, out localeId);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00005B84 File Offset: 0x00003D84
		internal static bool TryFindLanguageSpecification<TSpec>(this IReadOnlyDictionary<LanguageIdentifier, TSpec> dictionary, LanguageIdentifier languageId, out TSpec specification)
		{
			LanguageIdentifier languageIdentifier;
			if (dictionary.TryGetValue(languageId, out specification) || (languageId.TryFindDefaultLocale(out languageIdentifier) && dictionary.TryGetValue(languageIdentifier, out specification)))
			{
				return true;
			}
			specification = default(TSpec);
			return false;
		}

		// Token: 0x0400004C RID: 76
		private static readonly ReadOnlyDictionary<LanguageIdentifier, LanguageIdentifier> _defaultLocaleMap = new Dictionary<LanguageIdentifier, LanguageIdentifier>
		{
			{
				LanguageIdentifier.en,
				LanguageIdentifier.en_US
			},
			{
				LanguageIdentifier.es,
				LanguageIdentifier.es_ES
			},
			{
				LanguageIdentifier.de,
				LanguageIdentifier.de_DE
			}
		}.AsReadOnlyDictionary<LanguageIdentifier, LanguageIdentifier>();
	}
}
