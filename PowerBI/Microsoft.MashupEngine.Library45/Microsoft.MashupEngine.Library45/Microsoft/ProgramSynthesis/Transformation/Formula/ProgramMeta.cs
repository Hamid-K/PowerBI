using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Intent;
using Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Visitors;
using Microsoft.ProgramSynthesis.Utils.Text;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula
{
	// Token: 0x020014E0 RID: 5344
	public class ProgramMeta
	{
		// Token: 0x17001C94 RID: 7316
		// (get) Token: 0x0600A396 RID: 41878 RVA: 0x0022A72F File Offset: 0x0022892F
		// (set) Token: 0x0600A397 RID: 41879 RVA: 0x0022A737 File Offset: 0x00228937
		public IReadOnlyList<string> ColumnsUsed { get; set; }

		// Token: 0x17001C95 RID: 7317
		// (get) Token: 0x0600A398 RID: 41880 RVA: 0x0022A740 File Offset: 0x00228940
		// (set) Token: 0x0600A399 RID: 41881 RVA: 0x0022A748 File Offset: 0x00228948
		public int? ConcatCount { get; set; }

		// Token: 0x17001C96 RID: 7318
		// (get) Token: 0x0600A39A RID: 41882 RVA: 0x0022A751 File Offset: 0x00228951
		// (set) Token: 0x0600A39B RID: 41883 RVA: 0x0022A759 File Offset: 0x00228959
		public bool? ConsistentInput { get; set; }

		// Token: 0x17001C97 RID: 7319
		// (get) Token: 0x0600A39C RID: 41884 RVA: 0x0022A762 File Offset: 0x00228962
		// (set) Token: 0x0600A39D RID: 41885 RVA: 0x0022A76A File Offset: 0x0022896A
		public bool? ConsistentOutput { get; set; }

		// Token: 0x17001C98 RID: 7320
		// (get) Token: 0x0600A39E RID: 41886 RVA: 0x0022A773 File Offset: 0x00228973
		// (set) Token: 0x0600A39F RID: 41887 RVA: 0x0022A77B File Offset: 0x0022897B
		public bool? ConstantOutput { get; set; }

		// Token: 0x17001C99 RID: 7321
		// (get) Token: 0x0600A3A0 RID: 41888 RVA: 0x0022A784 File Offset: 0x00228984
		// (set) Token: 0x0600A3A1 RID: 41889 RVA: 0x0022A78C File Offset: 0x0022898C
		public int? ConstStrLength { get; set; }

		// Token: 0x17001C9A RID: 7322
		// (get) Token: 0x0600A3A2 RID: 41890 RVA: 0x0022A795 File Offset: 0x00228995
		// (set) Token: 0x0600A3A3 RID: 41891 RVA: 0x0022A79D File Offset: 0x0022899D
		public int? DistinctExampleCount { get; set; }

		// Token: 0x17001C9B RID: 7323
		// (get) Token: 0x0600A3A4 RID: 41892 RVA: 0x0022A7A6 File Offset: 0x002289A6
		// (set) Token: 0x0600A3A5 RID: 41893 RVA: 0x0022A7AE File Offset: 0x002289AE
		public int? DistinctOutputCount { get; set; }

		// Token: 0x17001C9C RID: 7324
		// (get) Token: 0x0600A3A6 RID: 41894 RVA: 0x0022A7B7 File Offset: 0x002289B7
		// (set) Token: 0x0600A3A7 RID: 41895 RVA: 0x0022A7BF File Offset: 0x002289BF
		public int? ExampleCount { get; set; }

		// Token: 0x17001C9D RID: 7325
		// (get) Token: 0x0600A3A8 RID: 41896 RVA: 0x0022A7C8 File Offset: 0x002289C8
		// (set) Token: 0x0600A3A9 RID: 41897 RVA: 0x0022A7D0 File Offset: 0x002289D0
		public IEnumerable<ExampleSubstringInfo> ExampleSubstrings { get; set; }

		// Token: 0x17001C9E RID: 7326
		// (get) Token: 0x0600A3AA RID: 41898 RVA: 0x0022A7D9 File Offset: 0x002289D9
		// (set) Token: 0x0600A3AB RID: 41899 RVA: 0x0022A7E1 File Offset: 0x002289E1
		public int? InputCount { get; set; }

		// Token: 0x17001C9F RID: 7327
		// (get) Token: 0x0600A3AC RID: 41900 RVA: 0x0022A7EC File Offset: 0x002289EC
		public string IntentName
		{
			get
			{
				ProgramIntentSummary intentSummary = this.IntentSummary;
				if (intentSummary == null)
				{
					return null;
				}
				return intentSummary.Intent.ToString();
			}
		}

		// Token: 0x17001CA0 RID: 7328
		// (get) Token: 0x0600A3AD RID: 41901 RVA: 0x0022A818 File Offset: 0x00228A18
		// (set) Token: 0x0600A3AE RID: 41902 RVA: 0x0022A820 File Offset: 0x00228A20
		public ProgramIntentSummary IntentSummary { get; set; }

		// Token: 0x17001CA1 RID: 7329
		// (get) Token: 0x0600A3AF RID: 41903 RVA: 0x0022A829 File Offset: 0x00228A29
		// (set) Token: 0x0600A3B0 RID: 41904 RVA: 0x0022A831 File Offset: 0x00228A31
		public int? MatchCount { get; set; }

		// Token: 0x17001CA2 RID: 7330
		// (get) Token: 0x0600A3B1 RID: 41905 RVA: 0x0022A83A File Offset: 0x00228A3A
		// (set) Token: 0x0600A3B2 RID: 41906 RVA: 0x0022A842 File Offset: 0x00228A42
		public double? MetadataTime { get; set; }

		// Token: 0x17001CA3 RID: 7331
		// (get) Token: 0x0600A3B3 RID: 41907 RVA: 0x0022A84B File Offset: 0x00228A4B
		// (set) Token: 0x0600A3B4 RID: 41908 RVA: 0x0022A853 File Offset: 0x00228A53
		public IReadOnlyList<ProgramNodeDetail> Nodes { get; set; }

		// Token: 0x17001CA4 RID: 7332
		// (get) Token: 0x0600A3B5 RID: 41909 RVA: 0x0022A85C File Offset: 0x00228A5C
		// (set) Token: 0x0600A3B6 RID: 41910 RVA: 0x0022A864 File Offset: 0x00228A64
		public int? OutputConstStrLength { get; set; }

		// Token: 0x17001CA5 RID: 7333
		// (get) Token: 0x0600A3B7 RID: 41911 RVA: 0x0022A86D File Offset: 0x00228A6D
		// (set) Token: 0x0600A3B8 RID: 41912 RVA: 0x0022A875 File Offset: 0x00228A75
		public double? OutputConstStrRatio { get; set; }

		// Token: 0x17001CA6 RID: 7334
		// (get) Token: 0x0600A3B9 RID: 41913 RVA: 0x0022A87E File Offset: 0x00228A7E
		// (set) Token: 0x0600A3BA RID: 41914 RVA: 0x0022A886 File Offset: 0x00228A86
		public double? OutputConstStrRatioAvg { get; set; }

		// Token: 0x17001CA7 RID: 7335
		// (get) Token: 0x0600A3BB RID: 41915 RVA: 0x0022A88F File Offset: 0x00228A8F
		// (set) Token: 0x0600A3BC RID: 41916 RVA: 0x0022A897 File Offset: 0x00228A97
		public double? OutputLengthStdDev { get; set; }

		// Token: 0x17001CA8 RID: 7336
		// (get) Token: 0x0600A3BD RID: 41917 RVA: 0x0022A8A0 File Offset: 0x00228AA0
		// (set) Token: 0x0600A3BE RID: 41918 RVA: 0x0022A8A8 File Offset: 0x00228AA8
		public PipelineModel Pipeline { get; set; }

		// Token: 0x17001CA9 RID: 7337
		// (get) Token: 0x0600A3BF RID: 41919 RVA: 0x0022A8B1 File Offset: 0x00228AB1
		// (set) Token: 0x0600A3C0 RID: 41920 RVA: 0x0022A8B9 File Offset: 0x00228AB9
		public int? StrCount { get; set; }

		// Token: 0x17001CAA RID: 7338
		// (get) Token: 0x0600A3C1 RID: 41921 RVA: 0x0022A8C2 File Offset: 0x00228AC2
		// (set) Token: 0x0600A3C2 RID: 41922 RVA: 0x0022A8CA File Offset: 0x00228ACA
		public bool? WholeColumnOutput { get; set; }

		// Token: 0x17001CAB RID: 7339
		// (get) Token: 0x0600A3C3 RID: 41923 RVA: 0x0022A8D3 File Offset: 0x00228AD3
		// (set) Token: 0x0600A3C4 RID: 41924 RVA: 0x0022A8DB File Offset: 0x00228ADB
		public LearnConfidenceResult LearnConfidence { get; set; }

		// Token: 0x0600A3C5 RID: 41925 RVA: 0x0022A8E4 File Offset: 0x00228AE4
		public string Render(bool detail = false, bool includePipeline = true)
		{
			TextFieldSetBuilder textFieldSetBuilder = TextFieldSetBuilder.Create(0, 0, ":", false, "--");
			string text = "Intent";
			ProgramIntentSummary intentSummary = this.IntentSummary;
			TextFieldSetBuilder textFieldSetBuilder2 = textFieldSetBuilder.AddField(text, (intentSummary != null) ? intentSummary.ToString() : null, null);
			if (detail || includePipeline)
			{
				TextFieldSetBuilder textFieldSetBuilder3 = textFieldSetBuilder2;
				string text2 = "Pipeline";
				PipelineModel pipeline = this.Pipeline;
				textFieldSetBuilder3.AddField(text2, (pipeline != null) ? pipeline.ToString() : null, null);
			}
			if (detail)
			{
				TextFieldSetBuilder textFieldSetBuilder4 = textFieldSetBuilder2;
				string text3 = "ColumnsUsed";
				IReadOnlyList<string> columnsUsed = this.ColumnsUsed;
				textFieldSetBuilder4.AddField(text3, (columnsUsed != null) ? columnsUsed.ToJoinString(", ") : null, null).AddField("ExampleCount", this.ExampleCount, "N0").AddField("InputCount", this.InputCount, "N0")
					.AddField("DistinctExampleCount", this.DistinctExampleCount, "N0")
					.AddField("DistinctOutputCount", this.DistinctOutputCount, "N0")
					.AddField("ConstantOutput", this.ConstantOutput)
					.AddField("WholeColumnOutput", this.WholeColumnOutput)
					.AddField("ConsistentInput", this.ConsistentInput)
					.AddField("ConsistentOutput", this.ConsistentOutput)
					.AddField("OutputLengthStdDev", this.OutputLengthStdDev, "N3")
					.AddField("OutputConstStrLength", this.OutputConstStrLength, "N0")
					.AddField("OutputConstStrRatio", this.OutputConstStrRatio, "N3")
					.AddField("OutputConstStrRatioAvg", this.OutputConstStrRatioAvg, "N3");
				IEnumerable<ExampleSubstringInfo> exampleSubstrings = this.ExampleSubstrings;
				if (exampleSubstrings != null && exampleSubstrings.Any<ExampleSubstringInfo>())
				{
					textFieldSetBuilder2.AddField("ExampleSubstrings", this.ExampleSubstrings.Select((ExampleSubstringInfo s) => s.Render()).ToJoinNewlineString(), null);
				}
			}
			TextFieldSetBuilder textFieldSetBuilder5 = textFieldSetBuilder2;
			string text4 = "LearnConfidence";
			LearnConfidenceResult learnConfidence = this.LearnConfidence;
			return textFieldSetBuilder5.AddField(text4, (learnConfidence != null) ? learnConfidence.ToString() : null, null).AddField("MetadataTime", string.Format("{0:N3}ms", this.MetadataTime), null).Render();
		}

		// Token: 0x0600A3C6 RID: 41926 RVA: 0x0022AAEA File Offset: 0x00228CEA
		public JObject ToJson()
		{
			return JObject.FromObject(this);
		}

		// Token: 0x0600A3C7 RID: 41927 RVA: 0x0022AAF4 File Offset: 0x00228CF4
		public override string ToString()
		{
			string text;
			if ((text = this._toString) == null)
			{
				text = (this._toString = this.Render(false, true));
			}
			return text;
		}

		// Token: 0x04004262 RID: 16994
		private string _toString;
	}
}
