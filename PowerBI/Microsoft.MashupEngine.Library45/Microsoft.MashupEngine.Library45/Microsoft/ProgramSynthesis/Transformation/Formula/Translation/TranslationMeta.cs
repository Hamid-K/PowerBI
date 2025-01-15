using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Explanations;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils.Text;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x0200180E RID: 6158
	public class TranslationMeta
	{
		// Token: 0x17002222 RID: 8738
		// (get) Token: 0x0600CA58 RID: 51800 RVA: 0x002B3D26 File Offset: 0x002B1F26
		// (set) Token: 0x0600CA59 RID: 51801 RVA: 0x002B3D2E File Offset: 0x002B1F2E
		public IExplanationMeta Explanation { get; set; }

		// Token: 0x17002223 RID: 8739
		// (get) Token: 0x0600CA5A RID: 51802 RVA: 0x002B3D37 File Offset: 0x002B1F37
		// (set) Token: 0x0600CA5B RID: 51803 RVA: 0x002B3D3F File Offset: 0x002B1F3F
		public FormulaExpression Expression { get; set; }

		// Token: 0x17002224 RID: 8740
		// (get) Token: 0x0600CA5C RID: 51804 RVA: 0x002B3D48 File Offset: 0x002B1F48
		// (set) Token: 0x0600CA5D RID: 51805 RVA: 0x002B3D50 File Offset: 0x002B1F50
		public int? FunctionCount { get; set; }

		// Token: 0x17002225 RID: 8741
		// (get) Token: 0x0600CA5E RID: 51806 RVA: 0x002B3D59 File Offset: 0x002B1F59
		// (set) Token: 0x0600CA5F RID: 51807 RVA: 0x002B3D61 File Offset: 0x002B1F61
		public bool? HighAcceptance { get; set; }

		// Token: 0x17002226 RID: 8742
		// (get) Token: 0x0600CA60 RID: 51808 RVA: 0x002B3D6A File Offset: 0x002B1F6A
		// (set) Token: 0x0600CA61 RID: 51809 RVA: 0x002B3D72 File Offset: 0x002B1F72
		public float? HighAcceptanceScore { get; set; }

		// Token: 0x17002227 RID: 8743
		// (get) Token: 0x0600CA62 RID: 51810 RVA: 0x002B3D7B File Offset: 0x002B1F7B
		// (set) Token: 0x0600CA63 RID: 51811 RVA: 0x002B3D83 File Offset: 0x002B1F83
		public bool? HighPrecision { get; set; }

		// Token: 0x17002228 RID: 8744
		// (get) Token: 0x0600CA64 RID: 51812 RVA: 0x002B3D8C File Offset: 0x002B1F8C
		// (set) Token: 0x0600CA65 RID: 51813 RVA: 0x002B3D94 File Offset: 0x002B1F94
		public float? HighPrecisionScore { get; set; }

		// Token: 0x17002229 RID: 8745
		// (get) Token: 0x0600CA66 RID: 51814 RVA: 0x002B3D9D File Offset: 0x002B1F9D
		// (set) Token: 0x0600CA67 RID: 51815 RVA: 0x002B3DA5 File Offset: 0x002B1FA5
		public int? Length { get; set; }

		// Token: 0x1700222A RID: 8746
		// (get) Token: 0x0600CA68 RID: 51816 RVA: 0x002B3DAE File Offset: 0x002B1FAE
		// (set) Token: 0x0600CA69 RID: 51817 RVA: 0x002B3DB6 File Offset: 0x002B1FB6
		public int? MaxDepth { get; set; }

		// Token: 0x1700222B RID: 8747
		// (get) Token: 0x0600CA6A RID: 51818 RVA: 0x002B3DBF File Offset: 0x002B1FBF
		// (set) Token: 0x0600CA6B RID: 51819 RVA: 0x002B3DC7 File Offset: 0x002B1FC7
		public double? MetadataTime { get; set; }

		// Token: 0x1700222C RID: 8748
		// (get) Token: 0x0600CA6C RID: 51820 RVA: 0x002B3DD0 File Offset: 0x002B1FD0
		// (set) Token: 0x0600CA6D RID: 51821 RVA: 0x002B3DD8 File Offset: 0x002B1FD8
		public SuppressReason? SuppressReason { get; set; }

		// Token: 0x1700222D RID: 8749
		// (get) Token: 0x0600CA6E RID: 51822 RVA: 0x002B3DE1 File Offset: 0x002B1FE1
		// (set) Token: 0x0600CA6F RID: 51823 RVA: 0x002B3DE9 File Offset: 0x002B1FE9
		public bool Valid { get; set; } = true;

		// Token: 0x0600CA70 RID: 51824 RVA: 0x002B3DF4 File Offset: 0x002B1FF4
		public string Render(bool detail = false)
		{
			SuppressReason? suppressReason;
			TextFieldSetBuilder textFieldSetBuilder = TextFieldSetBuilder.Create(0, 0, ":", false, "--").AddField("Valid", new bool?(this.Valid)).AddField("SuppressReason", (this.SuppressReason != null) ? suppressReason.GetValueOrDefault().ToString() : null, null);
			if (this.Explanation != null && (!this.Explanation.Text.IsNullOrEmpty() || !this.Explanation.Key.IsNullOrEmpty()))
			{
				IEnumerable<KeyValuePair<string, string>> enumerable = this.Explanation.Replacements.Where((KeyValuePair<string, string> pair) => pair.Key != "List").ToList<KeyValuePair<string, string>>();
				string text;
				if (!enumerable.None<KeyValuePair<string, string>>())
				{
					text = Environment.NewLine + enumerable.Select((KeyValuePair<string, string> pair) => pair.Key + "=" + pair.Value).ToJoinString("; ") + ";";
				}
				else
				{
					text = string.Empty;
				}
				string text2 = text;
				textFieldSetBuilder.AddField("Explanation", this.Explanation.Key + Environment.NewLine + this.Explanation.Text + text2, null);
			}
			if (detail)
			{
				TextFieldSetBuilder textFieldSetBuilder2 = textFieldSetBuilder.AddField("HighPrecision", this.HighPrecision);
				string text3 = "HighPrecisionScore";
				float? num = this.HighPrecisionScore;
				TextFieldSetBuilder textFieldSetBuilder3 = textFieldSetBuilder2.AddField(text3, (num != null) ? new double?((double)num.GetValueOrDefault()) : null, "N3").AddField("HighAcceptance", this.HighAcceptance);
				string text4 = "HighAcceptanceScore";
				num = this.HighAcceptanceScore;
				textFieldSetBuilder3.AddField(text4, (num != null) ? new double?((double)num.GetValueOrDefault()) : null, "N3").AddField("Length", this.Length, "N0").AddField("FunctionCount", this.FunctionCount, "N0")
					.AddField("MaxDepth", this.MaxDepth, "N0");
			}
			return textFieldSetBuilder.AddField("MetadataTime", string.Format("{0:N3}ms", this.MetadataTime), null).Render();
		}

		// Token: 0x0600CA71 RID: 51825 RVA: 0x0022AAEA File Offset: 0x00228CEA
		public JObject ToJson()
		{
			return JObject.FromObject(this);
		}

		// Token: 0x0600CA72 RID: 51826 RVA: 0x002B4040 File Offset: 0x002B2240
		public override string ToString()
		{
			string text = (this.Valid ? "Valid" : "Invalid");
			string text2 = ((this.SuppressReason == null) ? null : string.Format(" ({0})", this.SuppressReason));
			return text + text2;
		}
	}
}
