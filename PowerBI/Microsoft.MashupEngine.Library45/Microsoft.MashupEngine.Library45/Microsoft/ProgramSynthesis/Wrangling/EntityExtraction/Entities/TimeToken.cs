using System;
using System.Globalization;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x0200020D RID: 525
	public class TimeToken : ValueBasedEntityToken
	{
		// Token: 0x06000B51 RID: 2897 RVA: 0x00022776 File Offset: 0x00020976
		public TimeToken(string source, int start, int end, DateTime time)
			: base(source, start, end)
		{
			this.Time = time;
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00022789 File Offset: 0x00020989
		public DateTime Time { get; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x0001CD2B File Offset: 0x0001AF2B
		public override double ScoreMultiplier
		{
			get
			{
				return 3.0;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x00022791 File Offset: 0x00020991
		public override string EntityName
		{
			get
			{
				return "Time";
			}
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00022798 File Offset: 0x00020998
		public override void MakeSearchTreeEntries(IAutoCompleteSearchTree tree, bool includeNonExtensionCompletions = false)
		{
			foreach (string text in TimeToken.Formats)
			{
				string text2 = this.Time.ToString(text, CultureInfo.InvariantCulture);
				tree.Add(text2, new CompletionInfo(text2, this, 1.0, null));
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x000227EC File Offset: 0x000209EC
		public override bool ValueBasedEquality(EntityToken other)
		{
			return other == this || (other != null && !(other.GetType() != base.GetType()) && ((TimeToken)other).Time.TimeOfDay == this.Time.TimeOfDay);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00022840 File Offset: 0x00020A40
		public override int ValueBasedHashCode()
		{
			return (this.Time.TimeOfDay.GetHashCode() * 1663) ^ base.GetType().GetHashCode();
		}

		// Token: 0x040005D7 RID: 1495
		private static readonly string[] Formats = new string[] { "hh:mm", "h:mm", "hh:m", "h:m", "H:mm", "hh:mm:ss tt", "HH:mm:ss", "h:mm:ss", "h:mm:ss tt" };
	}
}
