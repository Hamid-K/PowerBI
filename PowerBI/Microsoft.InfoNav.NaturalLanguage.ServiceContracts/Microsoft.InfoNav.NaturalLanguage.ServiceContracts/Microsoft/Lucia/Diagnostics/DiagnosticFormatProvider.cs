using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Lucia.Diagnostics
{
	// Token: 0x02000034 RID: 52
	public class DiagnosticFormatProvider : IFormatProvider, ICustomFormatter
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x0000398D File Offset: 0x00001B8D
		public DiagnosticFormatProvider([Nullable] IFormatProvider baseFormatProvider = null)
		{
			this._baseFormatProvider = baseFormatProvider ?? CultureInfo.InvariantCulture;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000039A8 File Offset: 0x00001BA8
		public static DiagnosticFormatProvider GetInstanceOrDefault([Nullable] IFormatProvider formatProvider = null)
		{
			if (formatProvider == null || formatProvider == CultureInfo.InvariantCulture)
			{
				return DiagnosticFormatProvider._defaultInvariant;
			}
			DiagnosticFormatProvider diagnosticFormatProvider = formatProvider as DiagnosticFormatProvider;
			if (diagnosticFormatProvider != null)
			{
				return diagnosticFormatProvider;
			}
			CultureInfo cultureInfo = formatProvider as CultureInfo;
			if (cultureInfo != null && cultureInfo == CultureInfo.GetCultureInfo(cultureInfo.Name))
			{
				return DiagnosticFormatProvider._defaultInstances.GetOrAdd(cultureInfo, (CultureInfo ci) => new DiagnosticFormatProvider(ci));
			}
			return new DiagnosticFormatProvider(formatProvider);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003A1B File Offset: 0x00001C1B
		public object GetFormat(Type formatType)
		{
			if (formatType == typeof(ICustomFormatter))
			{
				return this;
			}
			return this._baseFormatProvider.GetFormat(formatType);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003A40 File Offset: 0x00001C40
		public virtual string Format([Nullable] string format, [Nullable] object arg, [Nullable] IFormatProvider formatProvider)
		{
			if (arg == null)
			{
				return this.FormatNull();
			}
			if (format != null)
			{
				ContentClassificationKind contentClassificationKind;
				if (DiagnosticFormatProvider._formatSpecifiers.TryGetValue(format, out contentClassificationKind))
				{
					return this.FormatTaggedContent(contentClassificationKind, null, arg, formatProvider);
				}
				int num = format.LastIndexOf(':');
				if (num >= 0)
				{
					string text = format.Substring(num + 1);
					if (DiagnosticFormatProvider._formatSpecifiers.TryGetValue(text, out contentClassificationKind))
					{
						string text2 = format.Substring(0, num);
						return this.FormatTaggedContent(contentClassificationKind, text2, arg, formatProvider);
					}
				}
			}
			IFormattable formattable = arg as IFormattable;
			if (formattable != null)
			{
				return formattable.ToString(format, formatProvider);
			}
			return arg.ToString();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003AC9 File Offset: 0x00001CC9
		protected virtual string FormatNull()
		{
			return string.Empty;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003AD0 File Offset: 0x00001CD0
		protected virtual string FormatTaggedContent(ContentClassificationKind contentClassification, [Nullable] string format, object arg, [Nullable] IFormatProvider formatProvider)
		{
			return this.Format(format, arg, formatProvider);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003ADC File Offset: 0x00001CDC
		// Note: this type is marked as 'beforefieldinit'.
		static DiagnosticFormatProvider()
		{
			Dictionary<string, ContentClassificationKind> dictionary = new Dictionary<string, ContentClassificationKind>(StringComparer.OrdinalIgnoreCase);
			dictionary["ccon"] = ContentClassificationKind.CustomerContent;
			dictionary["euii"] = ContentClassificationKind.Euii;
			dictionary["ip"] = ContentClassificationKind.IPAddress;
			DiagnosticFormatProvider._formatSpecifiers = dictionary;
			DiagnosticFormatProvider._defaultInvariant = new DiagnosticFormatProvider(null);
			DiagnosticFormatProvider._defaultInstances = new ConcurrentDictionary<CultureInfo, DiagnosticFormatProvider>();
		}

		// Token: 0x0400005A RID: 90
		private const char FormatSeparator = ':';

		// Token: 0x0400005B RID: 91
		private static readonly IReadOnlyDictionary<string, ContentClassificationKind> _formatSpecifiers;

		// Token: 0x0400005C RID: 92
		private static readonly DiagnosticFormatProvider _defaultInvariant;

		// Token: 0x0400005D RID: 93
		private static readonly ConcurrentDictionary<CultureInfo, DiagnosticFormatProvider> _defaultInstances;

		// Token: 0x0400005E RID: 94
		private readonly IFormatProvider _baseFormatProvider;
	}
}
