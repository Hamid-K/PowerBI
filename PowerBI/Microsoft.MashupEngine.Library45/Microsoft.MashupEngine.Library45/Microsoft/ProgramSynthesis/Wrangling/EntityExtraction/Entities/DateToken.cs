using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001C7 RID: 455
	public class DateToken : ValueBasedEntityToken
	{
		// Token: 0x060009F9 RID: 2553 RVA: 0x0001D507 File Offset: 0x0001B707
		public DateToken(string source, int start, int end, DateTime dateTime)
			: base(source, start, end)
		{
			this.DateTime = dateTime;
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0001D51A File Offset: 0x0001B71A
		public DateTime DateTime { get; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0001CD2B File Offset: 0x0001AF2B
		public override double ScoreMultiplier
		{
			get
			{
				return 3.0;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0001D522 File Offset: 0x0001B722
		public override string EntityName
		{
			get
			{
				return "DateTime";
			}
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0001D52C File Offset: 0x0001B72C
		public override void MakeSearchTreeEntries(IAutoCompleteSearchTree tree, bool includeNonExtensionCompletions = false)
		{
			foreach (KeyValuePair<string, double> keyValuePair in DateToken.FullDateFormats)
			{
				string text = this.DateTime.ToString(keyValuePair.Key, CultureInfo.InvariantCulture);
				tree.Add(text, new CompletionInfo(text, this, keyValuePair.Value, null));
				foreach (Match match in DateToken.DateSeparator.NonCachingMatches(text))
				{
					string text2 = text.Substring(match.Index + match.Length);
					tree.Add(text2, new CompletionInfo(text2, this, keyValuePair.Value * (double)text2.Length / (double)text.Length, null));
				}
			}
			if (!includeNonExtensionCompletions)
			{
				return;
			}
			string text3 = this.DateTime.ToString(DateToken.MetadataDateFormat, CultureInfo.InvariantCulture);
			string thisDateTimeDayOfWeek = this.DateTime.DayOfWeek.ToString();
			IEnumerable<string> allDaysOfWeek = DateToken.AllDaysOfWeek;
			Func<string, bool> <>9__0;
			Func<string, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (string s) => s != thisDateTimeDayOfWeek);
			}
			foreach (string text4 in allDaysOfWeek.Where(func))
			{
				string text5 = text4;
				string text6 = text4;
				string thisDateTimeDayOfWeek3 = thisDateTimeDayOfWeek;
				double num = 1.0;
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary["Provenance"] = text3;
				tree.Add(text5, new CompletionInfo(text6, thisDateTimeDayOfWeek3, this, num, dictionary));
			}
			foreach (string text7 in DateToken.DayOfWeekTriggers)
			{
				string text8 = text7;
				string text9 = text7;
				string thisDateTimeDayOfWeek2 = thisDateTimeDayOfWeek;
				double num2 = 0.9;
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
				dictionary2["Provenance"] = text3;
				tree.Add(text8, new CompletionInfo(text9, thisDateTimeDayOfWeek2, this, num2, dictionary2));
			}
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0001D760 File Offset: 0x0001B960
		public override bool ValueBasedEquality(EntityToken other)
		{
			return other == this || (other != null && !(base.GetType() != other.GetType()) && ((DateToken)other).DateTime.Date.Equals(this.DateTime.Date));
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0001D7B8 File Offset: 0x0001B9B8
		public override int ValueBasedHashCode()
		{
			return (this.DateTime.Date.GetHashCode() * 1427) ^ base.GetType().GetHashCode();
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0001D7F0 File Offset: 0x0001B9F0
		// Note: this type is marked as 'beforefieldinit'.
		static DateToken()
		{
			Dictionary<string, double> dictionary = new Dictionary<string, double>();
			dictionary["dddd"] = 1.5;
			dictionary["ddd"] = 1.4;
			dictionary["M/dd/yyyy"] = 1.0;
			dictionary["dddd, MMMM dd, yyyy"] = 1.0;
			dictionary["M"] = 0.5;
			dictionary["m"] = 0.5;
			dictionary["ddd, dd MMM yyyy"] = 1.0;
			dictionary["yyyy-MM-dd"] = 1.0;
			dictionary["Y"] = 0.3;
			dictionary["yyyy"] = 1.2;
			dictionary["M/d/yyyy"] = 1.0;
			dictionary["MM/d/yyyy"] = 1.0;
			dictionary["MMMM"] = 0.9;
			dictionary["MMM"] = 0.9;
			dictionary["MMMM dd, yyyy"] = 1.0;
			dictionary["MMM dd, yyyy"] = 1.0;
			dictionary["dd MMM yyyy"] = 1.0;
			dictionary["dd MMMM yyyy"] = 1.0;
			dictionary["d MMM yyyy"] = 1.0;
			dictionary["d MMMM yyyy"] = 1.0;
			dictionary["dd-MM-yyyy"] = 1.0;
			dictionary["d-MM-yyyy"] = 1.0;
			dictionary["dd-M-yyyy"] = 1.0;
			dictionary["d-M-yyyy"] = 1.0;
			dictionary["dd/MM/yyyy"] = 1.0;
			dictionary["d/MM/yyyy"] = 1.0;
			dictionary["dd/M/yyyy"] = 1.0;
			dictionary["d/M/yyyy"] = 1.0;
			dictionary["MM-dd-yyyy"] = 1.0;
			dictionary["M-dd-yyyy"] = 1.0;
			dictionary["MM-d-yyyy"] = 1.0;
			dictionary["M-d-yyyy"] = 1.0;
			dictionary["MMddyyyy"] = 0.9;
			dictionary["ddMMyyyy"] = 0.9;
			DateToken.FullDateFormats = dictionary;
			DateToken.MetadataDateFormat = "dd-MMM-yyyy";
			DateToken.DateSeparator = new Regex("(?:\\p{P})\\s*", RegexOptions.Compiled);
			DateToken.DayOfWeekTriggers = new string[] { "day", "weekday", "dayofweek" };
			DateToken.AllDaysOfWeek = (from DayOfWeek d in Enum.GetValues(typeof(DayOfWeek))
				select d.ToString()).ToArray<string>();
		}

		// Token: 0x040004D5 RID: 1237
		private static readonly Dictionary<string, double> FullDateFormats;

		// Token: 0x040004D6 RID: 1238
		private static readonly string MetadataDateFormat;

		// Token: 0x040004D7 RID: 1239
		private static readonly Regex DateSeparator;

		// Token: 0x040004D8 RID: 1240
		private static readonly string[] DayOfWeekTriggers;

		// Token: 0x040004D9 RID: 1241
		private static readonly string[] AllDaysOfWeek;
	}
}
