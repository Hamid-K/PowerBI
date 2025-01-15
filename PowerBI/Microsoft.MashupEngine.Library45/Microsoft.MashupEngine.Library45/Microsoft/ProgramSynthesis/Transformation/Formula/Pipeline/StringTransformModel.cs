using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x020019A0 RID: 6560
	public class StringTransformModel : PipelineModel
	{
		// Token: 0x17002399 RID: 9113
		// (get) Token: 0x0600D661 RID: 54881 RVA: 0x002D9E69 File Offset: 0x002D8069
		// (set) Token: 0x0600D662 RID: 54882 RVA: 0x002D9E71 File Offset: 0x002D8071
		public CaseTransformModel Case { get; set; }

		// Token: 0x1700239A RID: 9114
		// (get) Token: 0x0600D663 RID: 54883 RVA: 0x002D9E7A File Offset: 0x002D807A
		public bool HasCase
		{
			get
			{
				return this.Case != null;
			}
		}

		// Token: 0x1700239B RID: 9115
		// (get) Token: 0x0600D664 RID: 54884 RVA: 0x002D9E85 File Offset: 0x002D8085
		public bool HasExtractDateTime
		{
			get
			{
				return this.HasParseDateTime && this.HasSubstring;
			}
		}

		// Token: 0x1700239C RID: 9116
		// (get) Token: 0x0600D665 RID: 54885 RVA: 0x002D9E97 File Offset: 0x002D8097
		public bool HasExtractNumber
		{
			get
			{
				return this.HasParseNumber && this.HasSubstring;
			}
		}

		// Token: 0x1700239D RID: 9117
		// (get) Token: 0x0600D666 RID: 54886 RVA: 0x002D9EA9 File Offset: 0x002D80A9
		public bool HasLength
		{
			get
			{
				return this.Length;
			}
		}

		// Token: 0x1700239E RID: 9118
		// (get) Token: 0x0600D667 RID: 54887 RVA: 0x002D9EB1 File Offset: 0x002D80B1
		public bool HasParseDateTime
		{
			get
			{
				return this.ParseDateTime != null;
			}
		}

		// Token: 0x1700239F RID: 9119
		// (get) Token: 0x0600D668 RID: 54888 RVA: 0x002D9EBC File Offset: 0x002D80BC
		public bool HasParseNumber
		{
			get
			{
				return this.ParseNumber != null;
			}
		}

		// Token: 0x170023A0 RID: 9120
		// (get) Token: 0x0600D669 RID: 54889 RVA: 0x002D9EC7 File Offset: 0x002D80C7
		public bool HasReplace
		{
			get
			{
				return this.Replace != null;
			}
		}

		// Token: 0x170023A1 RID: 9121
		// (get) Token: 0x0600D66A RID: 54890 RVA: 0x002D9ED2 File Offset: 0x002D80D2
		public bool HasSlice
		{
			get
			{
				return this.Slice != null;
			}
		}

		// Token: 0x170023A2 RID: 9122
		// (get) Token: 0x0600D66B RID: 54891 RVA: 0x002D9EDD File Offset: 0x002D80DD
		public bool HasSliceBetween
		{
			get
			{
				return this.SliceBetween != null;
			}
		}

		// Token: 0x170023A3 RID: 9123
		// (get) Token: 0x0600D66C RID: 54892 RVA: 0x002D9EE8 File Offset: 0x002D80E8
		public bool HasSplit
		{
			get
			{
				return this.Split != null;
			}
		}

		// Token: 0x170023A4 RID: 9124
		// (get) Token: 0x0600D66D RID: 54893 RVA: 0x002D9EF3 File Offset: 0x002D80F3
		public bool HasSubstring
		{
			get
			{
				return this.HasSplit || this.HasSlice;
			}
		}

		// Token: 0x170023A5 RID: 9125
		// (get) Token: 0x0600D66E RID: 54894 RVA: 0x002D9F05 File Offset: 0x002D8105
		public bool HasTrim
		{
			get
			{
				return this.Trim;
			}
		}

		// Token: 0x170023A6 RID: 9126
		// (get) Token: 0x0600D66F RID: 54895 RVA: 0x002D9F0D File Offset: 0x002D810D
		// (set) Token: 0x0600D670 RID: 54896 RVA: 0x002D9F15 File Offset: 0x002D8115
		public bool Length { get; set; }

		// Token: 0x170023A7 RID: 9127
		// (get) Token: 0x0600D671 RID: 54897 RVA: 0x002D9F1E File Offset: 0x002D811E
		// (set) Token: 0x0600D672 RID: 54898 RVA: 0x002D9F26 File Offset: 0x002D8126
		public ParseDateTimeTransformModel ParseDateTime { get; set; }

		// Token: 0x170023A8 RID: 9128
		// (get) Token: 0x0600D673 RID: 54899 RVA: 0x002D9F2F File Offset: 0x002D812F
		// (set) Token: 0x0600D674 RID: 54900 RVA: 0x002D9F37 File Offset: 0x002D8137
		public ParseNumberTransformModel ParseNumber { get; set; }

		// Token: 0x170023A9 RID: 9129
		// (get) Token: 0x0600D675 RID: 54901 RVA: 0x002D9F40 File Offset: 0x002D8140
		// (set) Token: 0x0600D676 RID: 54902 RVA: 0x002D9F48 File Offset: 0x002D8148
		public ReplaceTransformModel Replace { get; set; }

		// Token: 0x170023AA RID: 9130
		// (get) Token: 0x0600D677 RID: 54903 RVA: 0x002D9F51 File Offset: 0x002D8151
		// (set) Token: 0x0600D678 RID: 54904 RVA: 0x002D9F59 File Offset: 0x002D8159
		public SliceTransformModel Slice { get; set; }

		// Token: 0x170023AB RID: 9131
		// (get) Token: 0x0600D679 RID: 54905 RVA: 0x002D9F62 File Offset: 0x002D8162
		// (set) Token: 0x0600D67A RID: 54906 RVA: 0x002D9F6A File Offset: 0x002D816A
		public SliceBetweenTransformModel SliceBetween { get; set; }

		// Token: 0x170023AC RID: 9132
		// (get) Token: 0x0600D67B RID: 54907 RVA: 0x002D9F73 File Offset: 0x002D8173
		// (set) Token: 0x0600D67C RID: 54908 RVA: 0x002D9F7B File Offset: 0x002D817B
		public SplitTransformModel Split { get; set; }

		// Token: 0x170023AD RID: 9133
		// (get) Token: 0x0600D67D RID: 54909 RVA: 0x002D9F84 File Offset: 0x002D8184
		// (set) Token: 0x0600D67E RID: 54910 RVA: 0x002D9F8C File Offset: 0x002D818C
		public bool Trim { get; set; }

		// Token: 0x0600D67F RID: 54911 RVA: 0x002D9F98 File Offset: 0x002D8198
		public override string ToOperatorString()
		{
			List<string> list = new List<string>();
			if (this.HasReplace)
			{
				list.Add(this.Replace.ToString());
			}
			if (this.HasSplit)
			{
				list.Add(this.Split.ToString());
			}
			if (this.HasSlice)
			{
				list.Add(this.Slice.ToString());
			}
			if (this.HasSliceBetween)
			{
				list.Add(this.SliceBetween.ToString());
			}
			if (this.HasParseNumber)
			{
				list.Add(this.ParseNumber.ToString());
			}
			if (this.HasParseDateTime)
			{
				list.Add(this.ParseDateTime.ToString());
			}
			if (this.HasLength)
			{
				list.Add("Length");
			}
			if (this.HasTrim)
			{
				list.Add("Trim");
			}
			if (this.HasCase)
			{
				list.Add(this.Case.ToString());
			}
			if (!list.None<string>())
			{
				return list.ToJoinString(" -> ");
			}
			return string.Empty;
		}
	}
}
