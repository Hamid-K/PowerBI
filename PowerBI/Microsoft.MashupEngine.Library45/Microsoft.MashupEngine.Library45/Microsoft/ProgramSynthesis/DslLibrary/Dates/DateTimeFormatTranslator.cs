using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x0200083A RID: 2106
	public abstract class DateTimeFormatTranslator
	{
		// Token: 0x06002DAB RID: 11691 RVA: 0x00082548 File Offset: 0x00080748
		protected DateTimeFormatTranslator(DateTimeFormat.Target target)
		{
			this.Target = target;
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x00082558 File Offset: 0x00080758
		public virtual Record<string, IReadOnlyList<DateTimeFormatTranslator.FormatSegment>>? TranslateWithSegmentInfo(DateTimeFormat format, bool strict)
		{
			IReadOnlyList<Record<string, DateTimeFormatTranslator.FormatSegment>?> readOnlyList = format.FormatParts.Select(delegate(DateTimeFormatPart fp)
			{
				string text = fp.FormatStringFor(this.Target, strict);
				if (text == null)
				{
					return null;
				}
				return new Record<string, DateTimeFormatTranslator.FormatSegment>?(Record.Create<string, DateTimeFormatTranslator.FormatSegment>(text, DateTimeFormatTranslator.FormatSegment.For(text, fp)));
			}).WholeNonNullSequence<Record<string, DateTimeFormatTranslator.FormatSegment>?>();
			if (readOnlyList == null)
			{
				return null;
			}
			return new Record<string, IReadOnlyList<DateTimeFormatTranslator.FormatSegment>>?(Record.Create<string, IReadOnlyList<DateTimeFormatTranslator.FormatSegment>>(string.Concat(readOnlyList.Select((Record<string, DateTimeFormatTranslator.FormatSegment>? x) => x.Value.Item1)), readOnlyList.Select((Record<string, DateTimeFormatTranslator.FormatSegment>? x) => x.Value.Item2).ToList<DateTimeFormatTranslator.FormatSegment>()));
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x00082604 File Offset: 0x00080804
		public string Translate(DateTimeFormat format, bool strict)
		{
			if (this.TranslateWithSegmentInfo(format, strict) == null)
			{
				return null;
			}
			Record<string, IReadOnlyList<DateTimeFormatTranslator.FormatSegment>>? record;
			return record.GetValueOrDefault().Item1;
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x00082631 File Offset: 0x00080831
		public virtual IEnumerable<Record<string, IReadOnlyList<DateTimeFormatTranslator.FormatSegment>>> AllTranslationsWithSegmentInfo(DateTimeFormat format, bool strict)
		{
			Record<string, IReadOnlyList<DateTimeFormatTranslator.FormatSegment>>? record = this.TranslateWithSegmentInfo(format, strict);
			if (record != null)
			{
				yield return record.Value;
			}
			yield break;
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x0008264F File Offset: 0x0008084F
		public IEnumerable<string> AllTranslations(DateTimeFormat format, bool strict)
		{
			string text = this.Translate(format, strict);
			if (text != null)
			{
				yield return text;
			}
			yield break;
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x0008266D File Offset: 0x0008086D
		protected DateTimeFormat.Target Target { get; }

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06002DB1 RID: 11697 RVA: 0x0000FA11 File Offset: 0x0000DC11
		internal virtual bool OrdinalDaySupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x00082678 File Offset: 0x00080878
		public static DateTimeFormatTranslator For(DateTimeFormat.Target target)
		{
			if (DateTimeFormatTranslator.Instances == null)
			{
				DateTimeFormatTranslator.Instances = new Dictionary<DateTimeFormat.Target, DateTimeFormatTranslator>();
			}
			DateTimeFormatTranslator dateTimeFormatTranslator;
			if (!DateTimeFormatTranslator.Instances.TryGetValue(target, out dateTimeFormatTranslator))
			{
				switch (target)
				{
				case DateTimeFormat.Target.PosixFormatting:
				case DateTimeFormat.Target.PosixParsing:
				case DateTimeFormat.Target.DayJSFormatting:
					dateTimeFormatTranslator = new BasicDateTimeFormatTranslator(target);
					break;
				case DateTimeFormat.Target.MomentJS:
					dateTimeFormatTranslator = new MomentJsDateTimeFormatTranslator();
					break;
				case DateTimeFormat.Target.PowerAppsFormatting:
					dateTimeFormatTranslator = new PowerAppsDateTimeFormatTranslator();
					break;
				default:
					throw new NotImplementedException(string.Format("Translation for target {0} not supported.", target));
				}
				DateTimeFormatTranslator.Instances[target] = dateTimeFormatTranslator;
			}
			return dateTimeFormatTranslator;
		}

		// Token: 0x04001608 RID: 5640
		[ThreadStatic]
		private static IDictionary<DateTimeFormat.Target, DateTimeFormatTranslator> Instances;

		// Token: 0x0200083B RID: 2107
		public struct FormatSegment
		{
			// Token: 0x170007F2 RID: 2034
			// (get) Token: 0x06002DB3 RID: 11699 RVA: 0x000826FD File Offset: 0x000808FD
			// (set) Token: 0x06002DB4 RID: 11700 RVA: 0x00082705 File Offset: 0x00080905
			public bool IsSignificant { readonly get; set; }

			// Token: 0x170007F3 RID: 2035
			// (get) Token: 0x06002DB5 RID: 11701 RVA: 0x0008270E File Offset: 0x0008090E
			// (set) Token: 0x06002DB6 RID: 11702 RVA: 0x00082716 File Offset: 0x00080916
			public string Segment { readonly get; set; }

			// Token: 0x06002DB7 RID: 11703 RVA: 0x00082720 File Offset: 0x00080920
			public static DateTimeFormatTranslator.FormatSegment For(string format, DateTimeFormatPart fp)
			{
				if (fp is NumericDateTimeFormatPart || fp is StringDateTimeFormatPart || fp is TimeZoneOffsetFormatPart)
				{
					return new DateTimeFormatTranslator.FormatSegment
					{
						IsSignificant = true,
						Segment = format
					};
				}
				return new DateTimeFormatTranslator.FormatSegment
				{
					IsSignificant = false,
					Segment = format
				};
			}

			// Token: 0x06002DB8 RID: 11704 RVA: 0x00082778 File Offset: 0x00080978
			public override string ToString()
			{
				return this.Segment + " [" + (this.IsSignificant ? "Significant" : "Insignificant") + "]";
			}
		}
	}
}
