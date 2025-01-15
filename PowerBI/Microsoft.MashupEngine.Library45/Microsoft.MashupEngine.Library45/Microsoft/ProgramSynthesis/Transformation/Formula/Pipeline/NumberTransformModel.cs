using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x02001998 RID: 6552
	public class NumberTransformModel : PipelineModel
	{
		// Token: 0x1700237E RID: 9086
		// (get) Token: 0x0600D621 RID: 54817 RVA: 0x002D9A6E File Offset: 0x002D7C6E
		// (set) Token: 0x0600D622 RID: 54818 RVA: 0x002D9A76 File Offset: 0x002D7C76
		public FormatNumberTransformModel Format { get; set; }

		// Token: 0x1700237F RID: 9087
		// (get) Token: 0x0600D623 RID: 54819 RVA: 0x002D9A7F File Offset: 0x002D7C7F
		// (set) Token: 0x0600D624 RID: 54820 RVA: 0x002D9A87 File Offset: 0x002D7C87
		public ForwardFillLinearTransform ForwardFillLinear { get; set; }

		// Token: 0x17002380 RID: 9088
		// (get) Token: 0x0600D625 RID: 54821 RVA: 0x002D9A90 File Offset: 0x002D7C90
		public bool HasFormat
		{
			get
			{
				return this.Format != null;
			}
		}

		// Token: 0x17002381 RID: 9089
		// (get) Token: 0x0600D626 RID: 54822 RVA: 0x002D9A9B File Offset: 0x002D7C9B
		public bool HasForwardFillLinear
		{
			get
			{
				return this.ForwardFillLinear != null;
			}
		}

		// Token: 0x17002382 RID: 9090
		// (get) Token: 0x0600D627 RID: 54823 RVA: 0x002D9AA6 File Offset: 0x002D7CA6
		public bool HasRound
		{
			get
			{
				return this.Round != null;
			}
		}

		// Token: 0x17002383 RID: 9091
		// (get) Token: 0x0600D628 RID: 54824 RVA: 0x002D9AB1 File Offset: 0x002D7CB1
		// (set) Token: 0x0600D629 RID: 54825 RVA: 0x002D9AB9 File Offset: 0x002D7CB9
		public RoundNumberTransform Round { get; set; }

		// Token: 0x0600D62A RID: 54826 RVA: 0x002D9AC4 File Offset: 0x002D7CC4
		public override string ToOperatorString()
		{
			List<string> list = new List<string>();
			if (this.HasRound)
			{
				list.Add(this.Round.ToString());
			}
			if (this.HasFormat)
			{
				list.Add(this.Format.ToString());
			}
			if (this.HasForwardFillLinear)
			{
				list.Add(this.ForwardFillLinear.ToString());
			}
			if (!list.None<string>())
			{
				return list.ToJoinString(" -> ");
			}
			return string.Empty;
		}
	}
}
