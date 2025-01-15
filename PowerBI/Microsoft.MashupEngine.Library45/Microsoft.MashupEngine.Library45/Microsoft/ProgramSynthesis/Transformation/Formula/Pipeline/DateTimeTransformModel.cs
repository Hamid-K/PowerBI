using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x0200199C RID: 6556
	public class DateTimeTransformModel : PipelineModel
	{
		// Token: 0x1700238B RID: 9099
		// (get) Token: 0x0600D640 RID: 54848 RVA: 0x002D9C4A File Offset: 0x002D7E4A
		// (set) Token: 0x0600D641 RID: 54849 RVA: 0x002D9C52 File Offset: 0x002D7E52
		public FormatDateTimeTransformModel Format { get; set; }

		// Token: 0x1700238C RID: 9100
		// (get) Token: 0x0600D642 RID: 54850 RVA: 0x002D9C5B File Offset: 0x002D7E5B
		public bool HasFormat
		{
			get
			{
				return this.Format != null;
			}
		}

		// Token: 0x1700238D RID: 9101
		// (get) Token: 0x0600D643 RID: 54851 RVA: 0x002D9C66 File Offset: 0x002D7E66
		public bool HasPart
		{
			get
			{
				return this.Part != null;
			}
		}

		// Token: 0x1700238E RID: 9102
		// (get) Token: 0x0600D644 RID: 54852 RVA: 0x002D9C71 File Offset: 0x002D7E71
		public bool HasRound
		{
			get
			{
				return this.Round != null;
			}
		}

		// Token: 0x1700238F RID: 9103
		// (get) Token: 0x0600D645 RID: 54853 RVA: 0x002D9C7C File Offset: 0x002D7E7C
		// (set) Token: 0x0600D646 RID: 54854 RVA: 0x002D9C84 File Offset: 0x002D7E84
		public DateTimePartTransformModel Part { get; set; }

		// Token: 0x17002390 RID: 9104
		// (get) Token: 0x0600D647 RID: 54855 RVA: 0x002D9C8D File Offset: 0x002D7E8D
		// (set) Token: 0x0600D648 RID: 54856 RVA: 0x002D9C95 File Offset: 0x002D7E95
		public RoundDateTimeTransformModel Round { get; set; }

		// Token: 0x0600D649 RID: 54857 RVA: 0x002D9CA0 File Offset: 0x002D7EA0
		public override string ToOperatorString()
		{
			List<string> list = new List<string>();
			if (this.HasPart)
			{
				list.Add(this.Part.ToString());
			}
			if (this.HasRound)
			{
				list.Add(this.Round.ToString());
			}
			if (this.HasFormat)
			{
				list.Add(this.Format.ToString());
			}
			if (!list.None<string>())
			{
				return list.ToJoinString(" -> ");
			}
			return string.Empty;
		}
	}
}
