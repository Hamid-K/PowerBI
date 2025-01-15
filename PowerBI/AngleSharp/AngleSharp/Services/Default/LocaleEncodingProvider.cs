using System;
using System.Collections.Generic;
using System.Text;
using AngleSharp.Extensions;

namespace AngleSharp.Services.Default
{
	// Token: 0x0200004D RID: 77
	public class LocaleEncodingProvider : IEncodingProvider
	{
		// Token: 0x06000187 RID: 391 RVA: 0x0000B6CC File Offset: 0x000098CC
		public virtual Encoding Suggest(string locale)
		{
			if (!string.IsNullOrEmpty(locale) && locale.Length > 1)
			{
				string text = locale.Substring(0, 2);
				Encoding encoding = null;
				if (LocaleEncodingProvider.suggestions.TryGetValue(text, out encoding))
				{
					return encoding;
				}
				if (locale.Isi("zh-cn"))
				{
					return TextEncoding.Gb18030;
				}
				if (locale.Isi("zh-tw"))
				{
					return TextEncoding.Big5;
				}
			}
			return TextEncoding.Windows1252;
		}

		// Token: 0x040001CB RID: 459
		private static readonly Dictionary<string, Encoding> suggestions = new Dictionary<string, Encoding>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"ar",
				TextEncoding.Utf8
			},
			{
				"cy",
				TextEncoding.Utf8
			},
			{
				"fa",
				TextEncoding.Utf8
			},
			{
				"hr",
				TextEncoding.Utf8
			},
			{
				"kk",
				TextEncoding.Utf8
			},
			{
				"mk",
				TextEncoding.Utf8
			},
			{
				"or",
				TextEncoding.Utf8
			},
			{
				"ro",
				TextEncoding.Utf8
			},
			{
				"sr",
				TextEncoding.Utf8
			},
			{
				"vi",
				TextEncoding.Utf8
			},
			{
				"be",
				TextEncoding.Latin5
			},
			{
				"bg",
				TextEncoding.Windows1251
			},
			{
				"ru",
				TextEncoding.Windows1251
			},
			{
				"uk",
				TextEncoding.Windows1251
			},
			{
				"cs",
				TextEncoding.Latin2
			},
			{
				"hu",
				TextEncoding.Latin2
			},
			{
				"pl",
				TextEncoding.Latin2
			},
			{
				"sl",
				TextEncoding.Latin2
			},
			{
				"tr",
				TextEncoding.Windows1254
			},
			{
				"ku",
				TextEncoding.Windows1254
			},
			{
				"he",
				TextEncoding.Windows1255
			},
			{
				"lv",
				TextEncoding.Latin13
			},
			{
				"ja",
				TextEncoding.Utf8
			},
			{
				"ko",
				TextEncoding.Korean
			},
			{
				"lt",
				TextEncoding.Windows1257
			},
			{
				"sk",
				TextEncoding.Windows1250
			},
			{
				"th",
				TextEncoding.Windows874
			}
		};
	}
}
