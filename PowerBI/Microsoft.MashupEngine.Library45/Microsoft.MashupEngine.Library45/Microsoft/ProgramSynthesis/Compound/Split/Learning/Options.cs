using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Compound.Split.Learning
{
	// Token: 0x020009E2 RID: 2530
	public class Options : DSLOptions
	{
		// Token: 0x06003CCA RID: 15562 RVA: 0x000BFC7C File Offset: 0x000BDE7C
		public Options()
		{
			this.TextConstraints = new List<Constraint<StringRegion, SplitCell[]>>();
			this.TelemetryTrackSymbols = new HashSet<string>();
			this.LineLengthLimit = 30000;
			this.ReadInputLineCount = 200;
			this.TimeLimit = TimeSpan.FromSeconds(5.0);
			this.AdditionalRecords = new List<StringRegion>();
			this.MaxRowPrefixRegexTokens = 1;
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06003CCB RID: 15563 RVA: 0x000BFCE1 File Offset: 0x000BDEE1
		// (set) Token: 0x06003CCC RID: 15564 RVA: 0x000BFCE9 File Offset: 0x000BDEE9
		public List<Constraint<StringRegion, SplitCell[]>> TextConstraints { get; set; }

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06003CCD RID: 15565 RVA: 0x000BFCF2 File Offset: 0x000BDEF2
		// (set) Token: 0x06003CCE RID: 15566 RVA: 0x000BFCFA File Offset: 0x000BDEFA
		public bool IgnoreSplitRecords { get; set; }

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06003CCF RID: 15567 RVA: 0x000BFD03 File Offset: 0x000BDF03
		// (set) Token: 0x06003CD0 RID: 15568 RVA: 0x000BFD0B File Offset: 0x000BDF0B
		public bool AllowMultiRecord { get; set; }

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06003CD1 RID: 15569 RVA: 0x000BFD14 File Offset: 0x000BDF14
		// (set) Token: 0x06003CD2 RID: 15570 RVA: 0x000BFD1C File Offset: 0x000BDF1C
		public bool EnableTelemetry { get; set; }

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06003CD3 RID: 15571 RVA: 0x000BFD25 File Offset: 0x000BDF25
		// (set) Token: 0x06003CD4 RID: 15572 RVA: 0x000BFD2D File Offset: 0x000BDF2D
		public HashSet<string> TelemetryTrackSymbols { get; set; }

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06003CD5 RID: 15573 RVA: 0x000BFD36 File Offset: 0x000BDF36
		// (set) Token: 0x06003CD6 RID: 15574 RVA: 0x000BFD3E File Offset: 0x000BDF3E
		public int TelemetryLineCount { get; set; }

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06003CD7 RID: 15575 RVA: 0x000BFD47 File Offset: 0x000BDF47
		// (set) Token: 0x06003CD8 RID: 15576 RVA: 0x000BFD4F File Offset: 0x000BDF4F
		public int LineLengthLimit { get; set; }

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06003CD9 RID: 15577 RVA: 0x000BFD58 File Offset: 0x000BDF58
		// (set) Token: 0x06003CDA RID: 15578 RVA: 0x000BFD60 File Offset: 0x000BDF60
		public int ReadInputLineCount { get; set; }

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06003CDB RID: 15579 RVA: 0x000BFD69 File Offset: 0x000BDF69
		// (set) Token: 0x06003CDC RID: 15580 RVA: 0x000BFD71 File Offset: 0x000BDF71
		public TimeSpan TimeLimit { get; set; }

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06003CDD RID: 15581 RVA: 0x000BFD7A File Offset: 0x000BDF7A
		public List<StringRegion> AdditionalRecords { get; }

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06003CDE RID: 15582 RVA: 0x000BFD82 File Offset: 0x000BDF82
		// (set) Token: 0x06003CDF RID: 15583 RVA: 0x000BFD8A File Offset: 0x000BDF8A
		public int SkipFooterLinesCount { get; set; }

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06003CE0 RID: 15584 RVA: 0x000BFD93 File Offset: 0x000BDF93
		// (set) Token: 0x06003CE1 RID: 15585 RVA: 0x000BFD9B File Offset: 0x000BDF9B
		public bool FilterEmptyColummns { get; set; }

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06003CE2 RID: 15586 RVA: 0x000BFDA4 File Offset: 0x000BDFA4
		// (set) Token: 0x06003CE3 RID: 15587 RVA: 0x000BFDAC File Offset: 0x000BDFAC
		public string FixedWidthSchema { get; set; }

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06003CE4 RID: 15588 RVA: 0x000BFDB5 File Offset: 0x000BDFB5
		// (set) Token: 0x06003CE5 RID: 15589 RVA: 0x000BFDBD File Offset: 0x000BDFBD
		public bool IgnoreQuote { get; set; }

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06003CE6 RID: 15590 RVA: 0x000BFDC6 File Offset: 0x000BDFC6
		// (set) Token: 0x06003CE7 RID: 15591 RVA: 0x000BFDCE File Offset: 0x000BDFCE
		public bool IgnoreSplitSequence { get; set; }

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06003CE8 RID: 15592 RVA: 0x000BFDD7 File Offset: 0x000BDFD7
		// (set) Token: 0x06003CE9 RID: 15593 RVA: 0x000BFDDF File Offset: 0x000BDFDF
		public bool IgnoreFilterHeader { get; set; }

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06003CEA RID: 15594 RVA: 0x000BFDE8 File Offset: 0x000BDFE8
		// (set) Token: 0x06003CEB RID: 15595 RVA: 0x000BFDF0 File Offset: 0x000BDFF0
		public bool IgnoreSkip { get; set; }

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06003CEC RID: 15596 RVA: 0x000BFDF9 File Offset: 0x000BDFF9
		// (set) Token: 0x06003CED RID: 15597 RVA: 0x000BFE01 File Offset: 0x000BE001
		public bool IgnoreSelectData { get; set; }

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06003CEE RID: 15598 RVA: 0x000BFE0A File Offset: 0x000BE00A
		// (set) Token: 0x06003CEF RID: 15599 RVA: 0x000BFE12 File Offset: 0x000BE012
		public int MaxRowPrefixRegexTokens { get; set; }

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06003CF0 RID: 15600 RVA: 0x000BFE1B File Offset: 0x000BE01B
		// (set) Token: 0x06003CF1 RID: 15601 RVA: 0x000BFE23 File Offset: 0x000BE023
		public bool LinesTrimmed { get; set; }

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06003CF2 RID: 15602 RVA: 0x000BFE2C File Offset: 0x000BE02C
		// (set) Token: 0x06003CF3 RID: 15603 RVA: 0x000BFE34 File Offset: 0x000BE034
		public string NewLineRecordSeparator { get; set; }

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06003CF4 RID: 15604 RVA: 0x000BFE3D File Offset: 0x000BE03D
		// (set) Token: 0x06003CF5 RID: 15605 RVA: 0x000BFE45 File Offset: 0x000BE045
		public int? SkipLinesCount { get; set; }

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06003CF6 RID: 15606 RVA: 0x000BFE4E File Offset: 0x000BE04E
		// (set) Token: 0x06003CF7 RID: 15607 RVA: 0x000BFE56 File Offset: 0x000BE056
		public bool DelimiterStringsProvided { get; set; }

		// Token: 0x04001C92 RID: 7314
		public const int DefaultLinesToRead = 200;

		// Token: 0x04001C93 RID: 7315
		private const int DefaultLineLengthLimit = 30000;

		// Token: 0x04001C94 RID: 7316
		private const int DefaultTimeLimitSeconds = 5;

		// Token: 0x04001C95 RID: 7317
		private const int DefaultRowPrefixRegexTokens = 1;
	}
}
