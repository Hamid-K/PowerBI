using System;
using System.ComponentModel;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200008E RID: 142
	[ImmutableObject(true)]
	public sealed class GermanPosTagSet : PosTagSet
	{
		// Token: 0x06000284 RID: 644 RVA: 0x00005E14 File Offset: 0x00004014
		private GermanPosTagSet()
		{
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00005E1C File Offset: 0x0000401C
		public override bool IsNoun(PosTagKind kind)
		{
			return kind.HasFeature(GermanPosTagSet.AnyNN, GermanPosTagSet.AnyNN);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00005E2F File Offset: 0x0000402F
		public override bool IsVerb(PosTagKind kind)
		{
			return kind.HasFeature(GermanPosTagSet.AnyVB, GermanPosTagSet.AnyVB);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00005E42 File Offset: 0x00004042
		public override bool IsModal(PosTagKind kind)
		{
			return kind.HasFeature(GermanPosTagSet.MD, GermanPosTagSet.MD);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00005E55 File Offset: 0x00004055
		public override bool IsAdjective(PosTagKind kind)
		{
			return kind.HasFeature(GermanPosTagSet.AnyJJ, GermanPosTagSet.AnyJJ);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00005E68 File Offset: 0x00004068
		public override bool IsAdverb(PosTagKind kind)
		{
			return kind.HasFeature(GermanPosTagSet.AnyRB, GermanPosTagSet.AnyRB);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00005E7C File Offset: 0x0000407C
		public override bool IsPreposition(PosTagKind kind)
		{
			return kind.Value == GermanPosTagSet.IN.Value;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00005EA0 File Offset: 0x000040A0
		public override bool IsNounPlural(PosTagKind kind)
		{
			return kind.Value == GermanPosTagSet.NNS.Value;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00005EC3 File Offset: 0x000040C3
		public override Func<PosTagKind, bool> IsNonNoun()
		{
			return GermanPosTagSet._isNonNoun;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00005ECA File Offset: 0x000040CA
		public override Func<StemmerSuggestion, bool> StemIsNoun()
		{
			return GermanPosTagSet._stemIsNoun;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00005ED1 File Offset: 0x000040D1
		public override Func<StemmerSuggestion, bool> StemIsNounPlural()
		{
			return GermanPosTagSet._stemIsNounPlural;
		}

		// Token: 0x040002F9 RID: 761
		public static readonly PosTagKind CC = new PosTagKind(1L);

		// Token: 0x040002FA RID: 762
		public static readonly PosTagKind CD = new PosTagKind(2L);

		// Token: 0x040002FB RID: 763
		public static readonly PosTagKind DT = new PosTagKind(3L);

		// Token: 0x040002FC RID: 764
		public static readonly PosTagKind EX = new PosTagKind(4L);

		// Token: 0x040002FD RID: 765
		public static readonly PosTagKind FW = new PosTagKind(5L);

		// Token: 0x040002FE RID: 766
		public static readonly PosTagKind IN = new PosTagKind(6L);

		// Token: 0x040002FF RID: 767
		public static readonly PosTagKind AnyJJ = new PosTagKind(256L);

		// Token: 0x04000300 RID: 768
		public static readonly PosTagKind JJ = new PosTagKind(257L);

		// Token: 0x04000301 RID: 769
		public static readonly PosTagKind JJR = new PosTagKind(258L);

		// Token: 0x04000302 RID: 770
		public static readonly PosTagKind JJS = new PosTagKind(259L);

		// Token: 0x04000303 RID: 771
		public static readonly PosTagKind JJN = new PosTagKind(260L);

		// Token: 0x04000304 RID: 772
		public static readonly PosTagKind JJRN = new PosTagKind(261L);

		// Token: 0x04000305 RID: 773
		public static readonly PosTagKind JJSN = new PosTagKind(262L);

		// Token: 0x04000306 RID: 774
		public static readonly PosTagKind LS = new PosTagKind(7L);

		// Token: 0x04000307 RID: 775
		public static readonly PosTagKind MD = new PosTagKind(8L);

		// Token: 0x04000308 RID: 776
		public static readonly PosTagKind AnyNN = new PosTagKind(512L);

		// Token: 0x04000309 RID: 777
		public static readonly PosTagKind NN = new PosTagKind(513L);

		// Token: 0x0400030A RID: 778
		public static readonly PosTagKind NNS = new PosTagKind(514L);

		// Token: 0x0400030B RID: 779
		public static readonly PosTagKind NNP = new PosTagKind(515L);

		// Token: 0x0400030C RID: 780
		public static readonly PosTagKind NNPS = new PosTagKind(516L);

		// Token: 0x0400030D RID: 781
		public static readonly PosTagKind PDT = new PosTagKind(9L);

		// Token: 0x0400030E RID: 782
		public static readonly PosTagKind POS = new PosTagKind(16L);

		// Token: 0x0400030F RID: 783
		public static readonly PosTagKind AnyPRP = new PosTagKind(1024L);

		// Token: 0x04000310 RID: 784
		public static readonly PosTagKind PRP = new PosTagKind(1025L);

		// Token: 0x04000311 RID: 785
		public static readonly PosTagKind PRPS = new PosTagKind(1026L);

		// Token: 0x04000312 RID: 786
		public static readonly PosTagKind AnyRB = new PosTagKind(2048L);

		// Token: 0x04000313 RID: 787
		public static readonly PosTagKind RB = new PosTagKind(2049L);

		// Token: 0x04000314 RID: 788
		public static readonly PosTagKind RBR = new PosTagKind(2050L);

		// Token: 0x04000315 RID: 789
		public static readonly PosTagKind RBS = new PosTagKind(2051L);

		// Token: 0x04000316 RID: 790
		public static readonly PosTagKind RP = new PosTagKind(17L);

		// Token: 0x04000317 RID: 791
		public static readonly PosTagKind SYM = new PosTagKind(18L);

		// Token: 0x04000318 RID: 792
		public static readonly PosTagKind TO = new PosTagKind(19L);

		// Token: 0x04000319 RID: 793
		public static readonly PosTagKind UH = new PosTagKind(20L);

		// Token: 0x0400031A RID: 794
		public static readonly PosTagKind AnyVB = new PosTagKind(4096L);

		// Token: 0x0400031B RID: 795
		public static readonly PosTagKind VB = new PosTagKind(4097L);

		// Token: 0x0400031C RID: 796
		public static readonly PosTagKind VBD = new PosTagKind(4098L);

		// Token: 0x0400031D RID: 797
		public static readonly PosTagKind VBG = new PosTagKind(4099L);

		// Token: 0x0400031E RID: 798
		public static readonly PosTagKind VBN = new PosTagKind(4100L);

		// Token: 0x0400031F RID: 799
		public static readonly PosTagKind VBP = new PosTagKind(4101L);

		// Token: 0x04000320 RID: 800
		public static readonly PosTagKind VBZ = new PosTagKind(4102L);

		// Token: 0x04000321 RID: 801
		public static readonly PosTagKind WDT = new PosTagKind(21L);

		// Token: 0x04000322 RID: 802
		public static readonly PosTagKind AnyWP = new PosTagKind(8192L);

		// Token: 0x04000323 RID: 803
		public static readonly PosTagKind WP = new PosTagKind(8193L);

		// Token: 0x04000324 RID: 804
		public static readonly PosTagKind WPS = new PosTagKind(8194L);

		// Token: 0x04000325 RID: 805
		public static readonly PosTagKind WRB = new PosTagKind(22L);

		// Token: 0x04000326 RID: 806
		private static readonly Func<PosTagKind, bool> _isNonNoun = (PosTagKind x) => !GermanPosTagSet.Instance.IsNoun(x);

		// Token: 0x04000327 RID: 807
		private static readonly Func<StemmerSuggestion, bool> _stemIsNoun = (StemmerSuggestion x) => GermanPosTagSet.Instance.IsNoun(x.PosTagKind);

		// Token: 0x04000328 RID: 808
		private static readonly Func<StemmerSuggestion, bool> _stemIsNounPlural = (StemmerSuggestion x) => GermanPosTagSet.Instance.IsNounPlural(x.PosTagKind);

		// Token: 0x04000329 RID: 809
		internal static readonly GermanPosTagSet Instance = new GermanPosTagSet();
	}
}
