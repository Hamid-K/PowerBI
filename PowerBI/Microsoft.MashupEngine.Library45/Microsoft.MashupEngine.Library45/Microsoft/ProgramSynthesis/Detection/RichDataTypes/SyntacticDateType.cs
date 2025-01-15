using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.RichDataTypes
{
	// Token: 0x02000A87 RID: 2695
	public class SyntacticDateType : SyntacticType
	{
		// Token: 0x06004333 RID: 17203 RVA: 0x000D2412 File Offset: 0x000D0612
		public SyntacticDateType(string naValue)
			: base(DataKind.DateTime, null, naValue)
		{
		}

		// Token: 0x06004334 RID: 17204 RVA: 0x000D241D File Offset: 0x000D061D
		public SyntacticDateType(DateTimeFormat format, Dictionary<string, string> substitutions)
			: base(DataKind.DateTime, substitutions, null)
		{
			this.Format = format;
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06004335 RID: 17205 RVA: 0x000D242F File Offset: 0x000D062F
		public DateTimeFormat Format { get; }

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06004336 RID: 17206 RVA: 0x000D2438 File Offset: 0x000D0638
		public bool HasDate
		{
			get
			{
				return !base.IsNaValue && this.Format.MatchedParts.Contains(DateTimePart.Year) && this.Format.MatchedParts.Contains(DateTimePart.Month) && this.Format.MatchedParts.Contains(DateTimePart.Day);
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06004337 RID: 17207 RVA: 0x000D2490 File Offset: 0x000D0690
		public bool HasTime
		{
			get
			{
				return !base.IsNaValue && (this.Format.MatchedParts.Contains(DateTimePart.Hour) || this.Format.MatchedParts.Contains(DateTimePart.HourInPeriod)) && this.Format.MatchedParts.Contains(DateTimePart.Minute);
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06004338 RID: 17208 RVA: 0x000D24EC File Offset: 0x000D06EC
		public bool HasSeconds
		{
			get
			{
				return !base.IsNaValue && this.Format.MatchedParts.Contains(DateTimePart.Second);
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06004339 RID: 17209 RVA: 0x000D2518 File Offset: 0x000D0718
		public bool HasMilliseconds
		{
			get
			{
				return !base.IsNaValue && this.Format.MatchedParts.Contains(DateTimePart.Millisecond);
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x0600433A RID: 17210 RVA: 0x000D2543 File Offset: 0x000D0743
		public bool HasDateTime
		{
			get
			{
				return this.HasDate && this.HasTime;
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x0600433B RID: 17211 RVA: 0x000D2555 File Offset: 0x000D0755
		public override DataKind Kind
		{
			get
			{
				if (this.HasDateTime)
				{
					return DataKind.DateTime;
				}
				if (!this.HasDate)
				{
					return DataKind.Time;
				}
				return DataKind.Date;
			}
		}

		// Token: 0x0600433C RID: 17212 RVA: 0x000D256C File Offset: 0x000D076C
		private string ApplySubstitutions(string value)
		{
			return base.EmptySubstitutions.Concat(base.NonEmptySubstitutions).Aggregate(value, (string acc, KeyValuePair<string, string> kvp) => acc.Replace(kvp.Key, kvp.Value));
		}

		// Token: 0x0600433D RID: 17213 RVA: 0x000D25A4 File Offset: 0x000D07A4
		internal Optional<DateTime> MaybeCastAsType(string value)
		{
			if (base.IsNaValue)
			{
				return Optional<DateTime>.Nothing;
			}
			value = this.ApplySubstitutions(value);
			Optional<DateTimeFormatMatch> optional = this.Format.Parse(value);
			if (!optional.HasValue)
			{
				return Optional<DateTime>.Nothing;
			}
			return optional.Value.PartialDateTime.ToDateTime();
		}

		// Token: 0x0600433E RID: 17214 RVA: 0x000D25F8 File Offset: 0x000D07F8
		public override Optional<string> Canonicalize(string value)
		{
			Optional<DateTime> optional = this.MaybeCastAsType(value);
			if (!optional.HasValue)
			{
				return Optional<string>.Nothing;
			}
			DateTime value2 = optional.Value;
			StringBuilder stringBuilder = new StringBuilder();
			if (this.HasDate)
			{
				stringBuilder.Append("yyyy-MM-dd");
			}
			if (this.HasTime)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append("T");
				}
				stringBuilder.Append("HH:mm");
				if (this.HasSeconds)
				{
					stringBuilder.Append(":ss");
				}
				if (this.HasMilliseconds)
				{
					stringBuilder.Append(".fff");
				}
			}
			string text = stringBuilder.ToString();
			if (!string.IsNullOrEmpty(text))
			{
				return value2.ToString(text).Some<string>();
			}
			return Optional<string>.Nothing;
		}

		// Token: 0x0600433F RID: 17215 RVA: 0x000D26B4 File Offset: 0x000D08B4
		public override bool IsValid(string value)
		{
			if (!base.IsNaValue)
			{
				return this.Format.Parse(this.ApplySubstitutions(value)).HasValue;
			}
			return value == base.NaValue.Value;
		}

		// Token: 0x06004340 RID: 17216 RVA: 0x000D26F8 File Offset: 0x000D08F8
		public override bool Equals(SyntacticType other)
		{
			if (other == this)
			{
				return true;
			}
			if (other == null)
			{
				return false;
			}
			if (base.NaValue != other.NaValue)
			{
				return false;
			}
			SyntacticDateType syntacticDateType = other as SyntacticDateType;
			return syntacticDateType != null && syntacticDateType.Format.Equals(this.Format);
		}

		// Token: 0x06004341 RID: 17217 RVA: 0x000D2744 File Offset: 0x000D0944
		public override string ToString()
		{
			if (!base.IsNaValue)
			{
				return this.Format.ToString();
			}
			return base.NaValue.Value;
		}

		// Token: 0x06004342 RID: 17218 RVA: 0x000D2774 File Offset: 0x000D0974
		internal bool IsConsistentWithVariance(IEnumerable<string> samples)
		{
			if (base.IsNaValue)
			{
				return true;
			}
			IEnumerable<PartialDateTime> allMatchesAsPartialDateTime = from s in samples
				where s != null
				select this.Format.Parse(s) into match
				where match.HasValue
				select match.Value.PartialDateTime;
			Dictionary<DateTimePart, int[]> dictionary = this.Format.MatchedParts.AsEnumerable().ToDictionary((DateTimePart part) => part, (DateTimePart part) => (from dt in allMatchesAsPartialDateTime
				select dt.Get(part) into optVal
				select optVal.Value).ToArray<int>());
			dictionary = dictionary.Where((KeyValuePair<DateTimePart, int[]> kvp) => kvp.Value.Length != 0).ToDictionary((KeyValuePair<DateTimePart, int[]> kvp) => kvp.Key, (KeyValuePair<DateTimePart, int[]> kvp) => kvp.Value);
			Dictionary<DateTimePart, float> means = dictionary.ToDictionary((KeyValuePair<DateTimePart, int[]> kvp) => kvp.Key, (KeyValuePair<DateTimePart, int[]> kvp) => (float)kvp.Value.Sum() / (float)kvp.Value.Length);
			Dictionary<DateTimePart, double> dictionary2 = dictionary.ToDictionary((KeyValuePair<DateTimePart, int[]> kvp) => kvp.Key, (KeyValuePair<DateTimePart, int[]> kvp) => kvp.Value.Select((int v) => Math.Pow((double)((float)v - means[kvp.Key]), 2.0)).Sum() / (double)((float)kvp.Value.Length));
			foreach (DateTimePart dateTimePart in dictionary2.Keys)
			{
				foreach (DateTimePart dateTimePart2 in dictionary2.Keys)
				{
					if (dateTimePart < dateTimePart2 && dictionary2[dateTimePart] > dictionary2[dateTimePart2])
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}
