using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Common;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200010B RID: 267
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class Term : IRange
	{
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0000946A File Offset: 0x0000766A
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x00009472 File Offset: 0x00007672
		[DataMember(IsRequired = true, Order = 10)]
		public int StartIndex { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0000947B File Offset: 0x0000767B
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x00009483 File Offset: 0x00007683
		[DataMember(IsRequired = true, Order = 20)]
		public int Length { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0000948C File Offset: 0x0000768C
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x00009494 File Offset: 0x00007694
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 22)]
		public int? CorrectedStartIndex { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0000949D File Offset: 0x0000769D
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x000094A5 File Offset: 0x000076A5
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 23)]
		public int? CorrectedLength { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x000094AE File Offset: 0x000076AE
		public int TokenCount
		{
			get
			{
				if (this.Tokens != null)
				{
					return this.Tokens.Count;
				}
				return 0;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x000094C5 File Offset: 0x000076C5
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x000094CD File Offset: 0x000076CD
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 25)]
		public IList<TextSegment> Tokens { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x000094D6 File Offset: 0x000076D6
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x000094DE File Offset: 0x000076DE
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 28)]
		public IList<PartOfSpeech> CandidatePartsOfSpeech { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x000094E8 File Offset: 0x000076E8
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x00009533 File Offset: 0x00007733
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public bool Unrecognized
		{
			get
			{
				bool? unrecognized = this._unrecognized;
				if (unrecognized == null)
				{
					return this.Resolution == TermResolution.NotUnderstood || this.Resolution == TermResolution.Unknown || this.Resolution == TermResolution.NotApplicable || this.Resolution == TermResolution.Inferred;
				}
				return unrecognized.GetValueOrDefault();
			}
			set
			{
				this._unrecognized = new bool?(value);
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x00009541 File Offset: 0x00007741
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x00009549 File Offset: 0x00007749
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 35)]
		public TermResolution Resolution { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00009552 File Offset: 0x00007752
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x0000955A File Offset: 0x0000775A
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public TermBindingContainer Binding { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00009563 File Offset: 0x00007763
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x0000956B File Offset: 0x0000776B
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public IList<TermBindingContainer> AlternateBindings { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00009574 File Offset: 0x00007774
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x0000957C File Offset: 0x0000777C
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public IList<TermBindingContainer> PhraseCompletions { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00009585 File Offset: 0x00007785
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x0000958D File Offset: 0x0000778D
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 65)]
		public IList<TermBindingContainer> PhraseExtensions { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00009596 File Offset: 0x00007796
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x0000959E File Offset: 0x0000779E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 70)]
		public IList<TermBindingContainer> SuggestedReplacements { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x000095A7 File Offset: 0x000077A7
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x000095AF File Offset: 0x000077AF
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 75)]
		public IList<TermBindingContainer> InferredTermBindings { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x000095B8 File Offset: 0x000077B8
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x00009623 File Offset: 0x00007823
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 77)]
		public bool InferredTermBindingsUnsupported
		{
			get
			{
				bool? inferredTermBindingsUnsupported = this._inferredTermBindingsUnsupported;
				bool flag = true;
				return ((inferredTermBindingsUnsupported.GetValueOrDefault() == flag) & (inferredTermBindingsUnsupported != null)) || (this.Resolution == TermResolution.Unknown && this.CandidatePartsOfSpeech != null && this.CandidatePartsOfSpeech.Count != 0 && !this.CandidatePartsOfSpeech.Contains(PartOfSpeech.Adjective) && !this.CandidatePartsOfSpeech.Contains(PartOfSpeech.Noun));
			}
			set
			{
				this._inferredTermBindingsUnsupported = new bool?(value);
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00009631 File Offset: 0x00007831
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x00009639 File Offset: 0x00007839
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 80)]
		public SpanBindingSource Source { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00009642 File Offset: 0x00007842
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x0000964A File Offset: 0x0000784A
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 90)]
		public IList<Term> Subspans { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00009653 File Offset: 0x00007853
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x0000965B File Offset: 0x0000785B
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 100)]
		public bool Overlaps { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00009664 File Offset: 0x00007864
		[JsonIgnore]
		public int FirstIndex
		{
			get
			{
				return this.StartIndex;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0000966C File Offset: 0x0000786C
		[JsonIgnore]
		public int LastIndex
		{
			get
			{
				return this.StartIndex + this.Length - 1;
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00009680 File Offset: 0x00007880
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormatInvariant("[{0},{1}]{2}{3}", new object[]
			{
				this.StartIndex,
				this.Length,
				this.Unrecognized ? "u" : string.Empty,
				(this.Source == SpanBindingSource.System) ? string.Empty : ("(" + this.Source.ToString() + ")")
			});
			if (this.Binding != null)
			{
				stringBuilder.AppendFormatInvariant(":{0}", new object[] { this.Binding.Binding });
			}
			if (this.AlternateBindings != null)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("ALTERNATE BINDINGS:");
				stringBuilder.Append(this.AlternateBindings.Select((TermBindingContainer e) => e.Binding).StringJoin(null).Indent(null));
			}
			if (this.PhraseCompletions != null)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("PHRASE COMPLETIONS:");
				stringBuilder.Append(this.PhraseCompletions.Select((TermBindingContainer e) => e.Binding).StringJoin(null).Indent(null));
			}
			if (this.PhraseExtensions != null)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("PHRASE EXTENSIONS:");
				stringBuilder.Append(this.PhraseExtensions.Select((TermBindingContainer e) => e.Binding).StringJoin(null).Indent(null));
			}
			if (this.SuggestedReplacements != null)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("SUGGESTED REPLACEMENTS:");
				stringBuilder.Append(this.SuggestedReplacements.Select((TermBindingContainer e) => e.Binding).StringJoin(null).Indent(null));
			}
			if (this.InferredTermBindings != null)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("INFERRED TERM BINDINGS:");
				stringBuilder.Append(this.InferredTermBindings.Select((TermBindingContainer e) => e.Binding).StringJoin(null).Indent(null));
			}
			else if (this.InferredTermBindingsUnsupported)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("Inferred Term Bindings not supported");
			}
			if (this.Subspans != null)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append(this.Subspans.StringJoin(null).Indent(null));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000596 RID: 1430
		private bool? _unrecognized;

		// Token: 0x04000597 RID: 1431
		private bool? _inferredTermBindingsUnsupported;
	}
}
