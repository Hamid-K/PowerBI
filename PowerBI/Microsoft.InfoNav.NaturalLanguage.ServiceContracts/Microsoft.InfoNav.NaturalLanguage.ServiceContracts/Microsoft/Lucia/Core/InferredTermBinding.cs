using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000FD RID: 253
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class InferredTermBinding : TermBinding
	{
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00008F55 File Offset: 0x00007155
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x00008F5D File Offset: 0x0000715D
		[DataMember(IsRequired = true, EmitDefaultValue = true, Order = 10)]
		public InferredTermBindingType Type { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00008F66 File Offset: 0x00007166
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x00008F6E File Offset: 0x0000716E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public string DefinitionPrompt { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00008F77 File Offset: 0x00007177
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x00008F7F File Offset: 0x0000717F
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public string DefinitionText { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00008F88 File Offset: 0x00007188
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x00008F90 File Offset: 0x00007190
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public IList<string> AlternateDefinitionTexts { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x00008F99 File Offset: 0x00007199
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x00008FA1 File Offset: 0x000071A1
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public string PrefixText { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00008FAA File Offset: 0x000071AA
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x00008FB2 File Offset: 0x000071B2
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public string SuffixText { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00008FBB File Offset: 0x000071BB
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x00008FC3 File Offset: 0x000071C3
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 70)]
		public string HintText { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00008FCC File Offset: 0x000071CC
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x00008FD4 File Offset: 0x000071D4
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 80)]
		public IList<string> LinguisticSchemaItems { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00008FDD File Offset: 0x000071DD
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x00008FE5 File Offset: 0x000071E5
		[DataMember(IsRequired = true, Order = 90)]
		public string DefinedTerm { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00008FEE File Offset: 0x000071EE
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x00008FF6 File Offset: 0x000071F6
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 100)]
		public string TargetRole { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00008FFF File Offset: 0x000071FF
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x00009007 File Offset: 0x00007207
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 110)]
		public string Metadata { get; set; }

		// Token: 0x060004FB RID: 1275 RVA: 0x00009010 File Offset: 0x00007210
		public override T Accept<T>(TermBindingVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000901C File Offset: 0x0000721C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(150);
			stringBuilder.Append(base.ToString());
			stringBuilder.Append("_");
			stringBuilder.Append(this.Type.ToString());
			stringBuilder.Append("_");
			stringBuilder.Append(this.DefinitionPrompt);
			if (!string.IsNullOrEmpty(this.DefinitionText))
			{
				stringBuilder.Append("_");
				stringBuilder.Append(this.DefinitionText);
			}
			if (!string.IsNullOrEmpty(this.PrefixText))
			{
				stringBuilder.Append("_");
				stringBuilder.Append(this.PrefixText);
			}
			if (!string.IsNullOrEmpty(this.HintText))
			{
				stringBuilder.Append("_");
				stringBuilder.Append(this.HintText);
			}
			if (!string.IsNullOrEmpty(this.SuffixText))
			{
				stringBuilder.Append("_");
				stringBuilder.Append(this.SuffixText);
			}
			if (!this.AlternateDefinitionTexts.IsNullOrEmptyCollection<string>())
			{
				stringBuilder.Append("_[");
				stringBuilder.Append(string.Join(",", this.AlternateDefinitionTexts));
				stringBuilder.Append("]");
			}
			if (!this.LinguisticSchemaItems.IsNullOrEmptyCollection<string>())
			{
				stringBuilder.Append("_");
				stringBuilder.Append(this.LinguisticSchemaItems.StringJoin(","));
			}
			return stringBuilder.ToString();
		}
	}
}
