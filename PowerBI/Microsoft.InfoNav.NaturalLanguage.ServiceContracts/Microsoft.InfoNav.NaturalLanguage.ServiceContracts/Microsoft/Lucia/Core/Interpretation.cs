using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200009D RID: 157
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class Interpretation
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00006265 File Offset: 0x00004465
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000626D File Offset: 0x0000446D
		[DataMember(IsRequired = true, Order = 1)]
		public string Restatement { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00006276 File Offset: 0x00004476
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000627E File Offset: 0x0000447E
		[DataMember(IsRequired = true, Order = 2)]
		public double Score { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00006287 File Offset: 0x00004487
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000628F File Offset: 0x0000448F
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public Dictionary<string, double> ScoreVector { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00006298 File Offset: 0x00004498
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x000062A0 File Offset: 0x000044A0
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
		public ResultConfidenceLevel ConfidenceLevel { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x000062A9 File Offset: 0x000044A9
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x000062B1 File Offset: 0x000044B1
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 5)]
		public ShowDataCommand Command { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x000062BA File Offset: 0x000044BA
		// (set) Token: 0x060002CA RID: 714 RVA: 0x000062C2 File Offset: 0x000044C2
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public IList<Term> Terms { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002CB RID: 715 RVA: 0x000062CB File Offset: 0x000044CB
		// (set) Token: 0x060002CC RID: 716 RVA: 0x000062D3 File Offset: 0x000044D3
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public FrameTree FrameTree { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002CD RID: 717 RVA: 0x000062DC File Offset: 0x000044DC
		// (set) Token: 0x060002CE RID: 718 RVA: 0x000062E4 File Offset: 0x000044E4
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public IList<int> UsedContextEvents { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002CF RID: 719 RVA: 0x000062ED File Offset: 0x000044ED
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x000062F5 File Offset: 0x000044F5
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public IList<string> SuggestedFollowUps { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x000062FE File Offset: 0x000044FE
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x00006306 File Offset: 0x00004506
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public string CorrectedUtterance { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000630F File Offset: 0x0000450F
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x00006317 File Offset: 0x00004517
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public string ReplacementUtterance { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00006320 File Offset: 0x00004520
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x00006328 File Offset: 0x00004528
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 65)]
		public IList<TextSegment> CorrectedTextSegments { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00006331 File Offset: 0x00004531
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x00006339 File Offset: 0x00004539
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 70)]
		public IList<SuggestedUtterance> SuggestedUtterances { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00006342 File Offset: 0x00004542
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0000634A File Offset: 0x0000454A
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 80)]
		public bool IsComplete { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00006353 File Offset: 0x00004553
		// (set) Token: 0x060002DC RID: 732 RVA: 0x0000635B File Offset: 0x0000455B
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 90)]
		public InterpretationDiagnostics Diagnostics { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00006364 File Offset: 0x00004564
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0000636C File Offset: 0x0000456C
		internal object SemanticInterpretation { get; set; }

		// Token: 0x060002DF RID: 735 RVA: 0x00006375 File Offset: 0x00004575
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00006380 File Offset: 0x00004580
		public string ToString(bool skipQueryDefinition)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(this.Restatement))
			{
				stringBuilder.Append("Restatement: ");
				stringBuilder.AppendLine(this.Restatement);
			}
			stringBuilder.Append("Score: ");
			stringBuilder.AppendLine(this.Score.ToString(CultureInfo.InvariantCulture));
			if (this.ScoreVector != null)
			{
				stringBuilder.AppendLine((from s in this.ScoreVector
					where s.Value < 1.0
					select StringUtil.FormatInvariant("{0}: {1}", s.Key, s.Value)).StringJoin(null).Indent(null));
			}
			stringBuilder.Append("ConfidenceLevel: ");
			stringBuilder.AppendLine(this.ConfidenceLevel.ToString());
			if (this.Command != null)
			{
				stringBuilder.AppendLine("Command:");
				stringBuilder.AppendLine(this.Command.ToString(skipQueryDefinition).Indent(null));
			}
			if (this.UsedContextEvents != null)
			{
				stringBuilder.AppendLine(StringUtil.FormatInvariant("Used Contexts: [{0}]", string.Join(",", this.UsedContextEvents.Select((int e) => e.ToString()))));
			}
			if (this.FrameTree != null)
			{
				stringBuilder.AppendLine("FrameTree:");
				stringBuilder.AppendLine(this.FrameTree.ToString().Indent(null));
			}
			if (this.Terms != null)
			{
				stringBuilder.AppendLine("Terms:");
				stringBuilder.AppendLine(this.Terms.StringJoin(null).Indent(null));
			}
			if (this.SuggestedFollowUps != null)
			{
				stringBuilder.AppendLine("SuggestedFollowUps:");
				stringBuilder.AppendLine(this.SuggestedFollowUps.StringJoin(null).Indent(null));
			}
			if (!string.IsNullOrEmpty(this.CorrectedUtterance))
			{
				stringBuilder.Append("CorrectedUtterance: ");
				stringBuilder.AppendLine(this.CorrectedUtterance);
			}
			if (!string.IsNullOrEmpty(this.ReplacementUtterance))
			{
				stringBuilder.Append("ReplacementUtterance: ");
				stringBuilder.AppendLine(this.ReplacementUtterance);
			}
			if (this.SuggestedUtterances != null)
			{
				stringBuilder.AppendLine("SuggestedUtterances:");
				stringBuilder.AppendLine(this.SuggestedUtterances.StringJoin(null).Indent(null));
			}
			return stringBuilder.ToString();
		}
	}
}
