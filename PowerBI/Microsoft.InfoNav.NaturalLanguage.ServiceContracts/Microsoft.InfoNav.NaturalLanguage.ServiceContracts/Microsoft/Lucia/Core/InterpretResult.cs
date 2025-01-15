using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000A5 RID: 165
	[DataContract(Name = "InterpretResult", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class InterpretResult
	{
		// Token: 0x06000343 RID: 835 RVA: 0x000069D5 File Offset: 0x00004BD5
		public InterpretResult()
		{
			this.Interpretations = new List<Interpretation>();
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000344 RID: 836 RVA: 0x000069E8 File Offset: 0x00004BE8
		// (set) Token: 0x06000345 RID: 837 RVA: 0x000069F0 File Offset: 0x00004BF0
		[DataMember(IsRequired = true, Order = 10)]
		public CompletedUtterance CompletedUtterance { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000346 RID: 838 RVA: 0x000069F9 File Offset: 0x00004BF9
		// (set) Token: 0x06000347 RID: 839 RVA: 0x00006A01 File Offset: 0x00004C01
		public LanguageIdentifier Language { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00006A0A File Offset: 0x00004C0A
		// (set) Token: 0x06000349 RID: 841 RVA: 0x00006A24 File Offset: 0x00004C24
		[DataMember(Name = "Language", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		private string SerializableLanguage
		{
			get
			{
				if (this.Language == (LanguageIdentifier)0)
				{
					return null;
				}
				return this.Language.ToLanguageName();
			}
			set
			{
				LanguageIdentifier languageIdentifier;
				if (LanguageIdentifierUtil.TryAsLanguageIdentifier(value, out languageIdentifier))
				{
					this.Language = languageIdentifier;
					return;
				}
				this.Language = (LanguageIdentifier)0;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00006A4A File Offset: 0x00004C4A
		// (set) Token: 0x0600034B RID: 843 RVA: 0x00006A52 File Offset: 0x00004C52
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public IList<Interpretation> Interpretations { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00006A5B File Offset: 0x00004C5B
		// (set) Token: 0x0600034D RID: 845 RVA: 0x00006A63 File Offset: 0x00004C63
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public InterpretWarnings Warnings { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00006A6C File Offset: 0x00004C6C
		// (set) Token: 0x0600034F RID: 847 RVA: 0x00006A74 File Offset: 0x00004C74
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public IList<string> TemplateSchemas { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00006A7D File Offset: 0x00004C7D
		// (set) Token: 0x06000351 RID: 849 RVA: 0x00006A85 File Offset: 0x00004C85
		public LanguageIdentifier ModelLanguage { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00006A8E File Offset: 0x00004C8E
		// (set) Token: 0x06000353 RID: 851 RVA: 0x00006AA8 File Offset: 0x00004CA8
		[DataMember(Name = "ModelLanguage", IsRequired = false, EmitDefaultValue = false, Order = 50)]
		private string SerializableModelLanguage
		{
			get
			{
				if (this.ModelLanguage == (LanguageIdentifier)0)
				{
					return null;
				}
				return this.ModelLanguage.ToLanguageName();
			}
			set
			{
				LanguageIdentifier languageIdentifier;
				if (LanguageIdentifierUtil.TryAsLanguageIdentifier(value, out languageIdentifier))
				{
					this.ModelLanguage = languageIdentifier;
					return;
				}
				this.ModelLanguage = (LanguageIdentifier)0;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00006ACE File Offset: 0x00004CCE
		// (set) Token: 0x06000355 RID: 853 RVA: 0x00006AD6 File Offset: 0x00004CD6
		public ContractLinguisticSchemaStatistics LinguisticSchemaStatistics { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00006ADF File Offset: 0x00004CDF
		// (set) Token: 0x06000357 RID: 855 RVA: 0x00006AE7 File Offset: 0x00004CE7
		public IList<Interpretation> UnknownTermInferenceInterpretations { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00006AF0 File Offset: 0x00004CF0
		// (set) Token: 0x06000359 RID: 857 RVA: 0x00006AF8 File Offset: 0x00004CF8
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Match", "SamplingResult" })]
		internal global::System.ValueTuple<bool, InterpretResult> SamplingInterpretResult
		{
			[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Match", "SamplingResult" })]
			get;
			[param: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Match", "SamplingResult" })]
			set;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00006B04 File Offset: 0x00004D04
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.CompletedUtterance != null)
			{
				stringBuilder.Append("CompletedUtterance: ");
				stringBuilder.AppendLine(this.CompletedUtterance.ToString());
			}
			if (this.Language != (LanguageIdentifier)0)
			{
				stringBuilder.Append("Language: ");
				stringBuilder.AppendLine(this.Language.ToLanguageName());
			}
			if (this.ModelLanguage != (LanguageIdentifier)0)
			{
				stringBuilder.Append("ModelLanguage: ");
				stringBuilder.AppendLine(this.ModelLanguage.ToLanguageName());
			}
			if (this.Warnings != InterpretWarnings.None)
			{
				stringBuilder.Append("Warnings: ");
				stringBuilder.AppendLine(this.Warnings.ToString());
			}
			if (this.Interpretations != null && this.Interpretations.Count > 0)
			{
				stringBuilder.Append(this.Interpretations[0].ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00006BEC File Offset: 0x00004DEC
		public string ToXmlString()
		{
			return InterpretResult._serializer.ToXmlString(this, false);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00006BFA File Offset: 0x00004DFA
		public Interpretation GetBestInterpretation()
		{
			if (this.Interpretations != null && this.Interpretations.Count > 0)
			{
				return this.Interpretations[0];
			}
			return null;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00006C20 File Offset: 0x00004E20
		public double GetBestInterpretationScore()
		{
			Interpretation bestInterpretation = this.GetBestInterpretation();
			if (bestInterpretation == null)
			{
				return 0.0;
			}
			return bestInterpretation.Score;
		}

		// Token: 0x04000399 RID: 921
		private static readonly DataContractSerializer _serializer = new DataContractSerializer(typeof(InterpretResult));
	}
}
