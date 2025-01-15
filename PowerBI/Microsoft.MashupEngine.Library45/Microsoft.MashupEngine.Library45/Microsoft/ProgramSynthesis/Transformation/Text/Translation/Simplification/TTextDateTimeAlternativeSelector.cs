using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation.Simplification
{
	// Token: 0x02001DA8 RID: 7592
	internal abstract class TTextDateTimeAlternativeSelector : TTextAlternativeSelector
	{
		// Token: 0x0600FEB3 RID: 65203
		protected abstract bool HasPosixTranslation(DateTimeFormatPart part);

		// Token: 0x0600FEB4 RID: 65204
		protected abstract DateTimeFormatPart PickAlternative(DateTimeFormatPart x, IEnumerable<DateTimeFormatPart> choices);

		// Token: 0x0600FEB5 RID: 65205 RVA: 0x00366B00 File Offset: 0x00364D00
		protected TTextDateTimeAlternativeSelector()
		{
			this.generalizations = (from x in DateTimeFormatPart.AllFormats
				where x is NumericDateTimeFormatPart
				select x into part
				group part by part.BaseFormatString[0] into g
				select Record.Create<IEnumerable<DateTimeFormatPart>, IEnumerable<DateTimeFormatPart>>(g.Where((DateTimeFormatPart x) => !this.HasPosixTranslation(x)), g.Where((DateTimeFormatPart x) => this.HasPosixTranslation(x))) into r
				where r.Item1.Any<DateTimeFormatPart>() && r.Item2.Any<DateTimeFormatPart>()
				select r).SelectMany((Record<IEnumerable<DateTimeFormatPart>, IEnumerable<DateTimeFormatPart>> r) => r.Item1.Select((DateTimeFormatPart x) => KVP.Create<DateTimeFormatPart, DateTimeFormatPart>(x, this.PickAlternative(x, r.Item2)))).ToDictionary<DateTimeFormatPart, DateTimeFormatPart>();
		}

		// Token: 0x0600FEB6 RID: 65206 RVA: 0x00366BB4 File Offset: 0x00364DB4
		protected DateTimeFormat GeneralizeDateTimeFormat(DateTimeFormat dtFormat)
		{
			DateTimeFormat dateTimeFormat = new DateTimeFormat(dtFormat.FormatParts.Select((DateTimeFormatPart y) => this.generalizations.MaybeGet(y).OrElse(y)).ToArray<DateTimeFormatPart>());
			if (dateTimeFormat.FormatParts.Any((DateTimeFormatPart fp) => !this.HasPosixTranslation(fp)))
			{
				return dtFormat;
			}
			return dateTimeFormat;
		}

		// Token: 0x04005F89 RID: 24457
		private IReadOnlyDictionary<DateTimeFormatPart, DateTimeFormatPart> generalizations;
	}
}
