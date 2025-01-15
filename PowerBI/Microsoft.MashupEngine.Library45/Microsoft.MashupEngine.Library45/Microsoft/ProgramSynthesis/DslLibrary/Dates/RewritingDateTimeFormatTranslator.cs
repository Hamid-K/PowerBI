using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary.Dates
{
	// Token: 0x0200087D RID: 2173
	internal abstract class RewritingDateTimeFormatTranslator : BasicDateTimeFormatTranslator
	{
		// Token: 0x06002F84 RID: 12164 RVA: 0x0008B65C File Offset: 0x0008985C
		public RewritingDateTimeFormatTranslator(DateTimeFormat.Target target)
			: base(target)
		{
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06002F85 RID: 12165
		public abstract IReadOnlyList<Record<string[], string>> Rewrites { get; }

		// Token: 0x06002F86 RID: 12166 RVA: 0x0008B668 File Offset: 0x00089868
		private Record<string, IReadOnlyList<DateTimeFormatTranslator.FormatSegment>> RewriteAndTranslate(IReadOnlyList<Record<string, DateTimeFormatTranslator.FormatSegment>> parts, out bool anyRewritten)
		{
			anyRewritten = false;
			if (parts.Count == 0)
			{
				return Record.Create<string, IReadOnlyList<DateTimeFormatTranslator.FormatSegment>>(string.Empty, new List<DateTimeFormatTranslator.FormatSegment>());
			}
			string text = string.Empty;
			List<DateTimeFormatTranslator.FormatSegment> list = new List<DateTimeFormatTranslator.FormatSegment>();
			int i = 0;
			while (i < parts.Count)
			{
				bool flag = false;
				foreach (Record<string[], string> record in this.Rewrites.OrderByDescending((Record<string[], string> x) => x.Item1.Length))
				{
					string[] array;
					string text2;
					record.Deconstruct(out array, out text2);
					string[] array2 = array;
					string text3 = text2;
					if ((from x in parts.Skip(i)
						select x.Item1).SequencePrefixEqual(array2, array2.Length, null))
					{
						i += array2.Length;
						text += text3;
						list.Add(new DateTimeFormatTranslator.FormatSegment
						{
							IsSignificant = true,
							Segment = text3
						});
						flag = true;
						anyRewritten = true;
					}
				}
				if (!flag)
				{
					text += parts[i].Item1;
					list.Add(parts[i].Item2);
					i++;
				}
			}
			return Record.Create<string, IReadOnlyList<DateTimeFormatTranslator.FormatSegment>>(text, list);
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x0008B7CC File Offset: 0x000899CC
		private IReadOnlyList<Record<string, DateTimeFormatTranslator.FormatSegment>> TranslateParts(DateTimeFormat format, bool strict)
		{
			IReadOnlyList<Record<string, DateTimeFormatTranslator.FormatSegment>?> readOnlyList = format.FormatParts.Select(delegate(DateTimeFormatPart x)
			{
				string text = x.FormatStringFor(this.Target, strict);
				if (text == null)
				{
					return null;
				}
				return new Record<string, DateTimeFormatTranslator.FormatSegment>?(Record.Create<string, DateTimeFormatTranslator.FormatSegment>(text, DateTimeFormatTranslator.FormatSegment.For(text, x)));
			}).WholeNonNullSequence<Record<string, DateTimeFormatTranslator.FormatSegment>?>();
			if (readOnlyList == null)
			{
				return null;
			}
			return readOnlyList.Select((Record<string, DateTimeFormatTranslator.FormatSegment>? x) => Record.Create<string, DateTimeFormatTranslator.FormatSegment>(x.Value.Item1, x.Value.Item2)).ToList<Record<string, DateTimeFormatTranslator.FormatSegment>>();
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x0008B838 File Offset: 0x00089A38
		public override Record<string, IReadOnlyList<DateTimeFormatTranslator.FormatSegment>>? TranslateWithSegmentInfo(DateTimeFormat format, bool strict)
		{
			IReadOnlyList<Record<string, DateTimeFormatTranslator.FormatSegment>> readOnlyList = this.TranslateParts(format, strict);
			if (readOnlyList == null)
			{
				return null;
			}
			bool flag;
			return new Record<string, IReadOnlyList<DateTimeFormatTranslator.FormatSegment>>?(this.RewriteAndTranslate(readOnlyList, out flag));
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x0008B869 File Offset: 0x00089A69
		public override IEnumerable<Record<string, IReadOnlyList<DateTimeFormatTranslator.FormatSegment>>> AllTranslationsWithSegmentInfo(DateTimeFormat format, bool strict)
		{
			IReadOnlyList<Record<string, DateTimeFormatTranslator.FormatSegment>> formatParts = this.TranslateParts(format, strict);
			if (formatParts == null)
			{
				yield break;
			}
			bool rewritten;
			yield return this.RewriteAndTranslate(formatParts, out rewritten);
			if (rewritten)
			{
				yield return Record.Create<string, IReadOnlyList<DateTimeFormatTranslator.FormatSegment>>(string.Concat(formatParts.Select((Record<string, DateTimeFormatTranslator.FormatSegment> x) => x.Item1)), formatParts.Select((Record<string, DateTimeFormatTranslator.FormatSegment> x) => x.Item2).ToList<DateTimeFormatTranslator.FormatSegment>());
			}
			yield break;
		}
	}
}
