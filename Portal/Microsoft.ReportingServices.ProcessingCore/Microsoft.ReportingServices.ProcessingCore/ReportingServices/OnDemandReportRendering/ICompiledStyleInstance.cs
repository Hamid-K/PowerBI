using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000206 RID: 518
	internal interface ICompiledStyleInstance
	{
		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06001356 RID: 4950
		// (set) Token: 0x06001357 RID: 4951
		ReportColor BackgroundGradientEndColor { get; set; }

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06001358 RID: 4952
		// (set) Token: 0x06001359 RID: 4953
		ReportColor BackgroundColor { get; set; }

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x0600135A RID: 4954
		// (set) Token: 0x0600135B RID: 4955
		ReportColor Color { get; set; }

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x0600135C RID: 4956
		// (set) Token: 0x0600135D RID: 4957
		FontStyles FontStyle { get; set; }

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x0600135E RID: 4958
		// (set) Token: 0x0600135F RID: 4959
		string FontFamily { get; set; }

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06001360 RID: 4960
		// (set) Token: 0x06001361 RID: 4961
		FontWeights FontWeight { get; set; }

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06001362 RID: 4962
		// (set) Token: 0x06001363 RID: 4963
		string Format { get; set; }

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06001364 RID: 4964
		// (set) Token: 0x06001365 RID: 4965
		TextDecorations TextDecoration { get; set; }

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06001366 RID: 4966
		// (set) Token: 0x06001367 RID: 4967
		TextAlignments TextAlign { get; set; }

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06001368 RID: 4968
		// (set) Token: 0x06001369 RID: 4969
		VerticalAlignments VerticalAlign { get; set; }

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x0600136A RID: 4970
		// (set) Token: 0x0600136B RID: 4971
		Directions Direction { get; set; }

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x0600136C RID: 4972
		// (set) Token: 0x0600136D RID: 4973
		WritingModes WritingMode { get; set; }

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x0600136E RID: 4974
		// (set) Token: 0x0600136F RID: 4975
		string Language { get; set; }

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06001370 RID: 4976
		// (set) Token: 0x06001371 RID: 4977
		UnicodeBiDiTypes UnicodeBiDi { get; set; }

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06001372 RID: 4978
		// (set) Token: 0x06001373 RID: 4979
		Calendars Calendar { get; set; }

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06001374 RID: 4980
		// (set) Token: 0x06001375 RID: 4981
		string CurrencyLanguage { get; set; }

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06001376 RID: 4982
		// (set) Token: 0x06001377 RID: 4983
		string NumeralLanguage { get; set; }

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06001378 RID: 4984
		// (set) Token: 0x06001379 RID: 4985
		BackgroundGradients BackgroundGradientType { get; set; }

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x0600137A RID: 4986
		// (set) Token: 0x0600137B RID: 4987
		ReportSize FontSize { get; set; }

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x0600137C RID: 4988
		// (set) Token: 0x0600137D RID: 4989
		ReportSize PaddingLeft { get; set; }

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x0600137E RID: 4990
		// (set) Token: 0x0600137F RID: 4991
		ReportSize PaddingRight { get; set; }

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06001380 RID: 4992
		// (set) Token: 0x06001381 RID: 4993
		ReportSize PaddingTop { get; set; }

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06001382 RID: 4994
		// (set) Token: 0x06001383 RID: 4995
		ReportSize PaddingBottom { get; set; }

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06001384 RID: 4996
		// (set) Token: 0x06001385 RID: 4997
		ReportSize LineHeight { get; set; }

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06001386 RID: 4998
		// (set) Token: 0x06001387 RID: 4999
		int NumeralVariant { get; set; }

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06001388 RID: 5000
		// (set) Token: 0x06001389 RID: 5001
		TextEffects TextEffect { get; set; }

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x0600138A RID: 5002
		// (set) Token: 0x0600138B RID: 5003
		BackgroundHatchTypes BackgroundHatchType { get; set; }

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x0600138C RID: 5004
		// (set) Token: 0x0600138D RID: 5005
		ReportColor ShadowColor { get; set; }

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x0600138E RID: 5006
		// (set) Token: 0x0600138F RID: 5007
		ReportSize ShadowOffset { get; set; }
	}
}
