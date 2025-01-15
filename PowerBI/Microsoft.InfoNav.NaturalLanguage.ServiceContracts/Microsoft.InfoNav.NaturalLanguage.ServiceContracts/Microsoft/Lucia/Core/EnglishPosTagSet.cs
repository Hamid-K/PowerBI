using System;
using System.ComponentModel;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000081 RID: 129
	[ImmutableObject(true)]
	public sealed class EnglishPosTagSet : PosTagSet
	{
		// Token: 0x06000243 RID: 579 RVA: 0x00005463 File Offset: 0x00003663
		private EnglishPosTagSet()
		{
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000546B File Offset: 0x0000366B
		public override bool IsNoun(PosTagKind kind)
		{
			return kind.HasFeature(EnglishPosTagSet.AnyNN, EnglishPosTagSet.AnyNN);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000547E File Offset: 0x0000367E
		public override bool IsVerb(PosTagKind kind)
		{
			return kind.HasFeature(EnglishPosTagSet.AnyVB, EnglishPosTagSet.AnyVB);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00005491 File Offset: 0x00003691
		public override bool IsModal(PosTagKind kind)
		{
			return kind.HasFeature(EnglishPosTagSet.MD, EnglishPosTagSet.MD);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000054A4 File Offset: 0x000036A4
		public override bool IsAdjective(PosTagKind kind)
		{
			return kind.HasFeature(EnglishPosTagSet.AnyJJ, EnglishPosTagSet.AnyJJ);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000054B7 File Offset: 0x000036B7
		public override bool IsAdverb(PosTagKind kind)
		{
			return kind.HasFeature(EnglishPosTagSet.AnyRB, EnglishPosTagSet.AnyRB);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000054CC File Offset: 0x000036CC
		public override bool IsPreposition(PosTagKind kind)
		{
			return kind.Value == EnglishPosTagSet.IN.Value;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000054F0 File Offset: 0x000036F0
		public override bool IsNounPlural(PosTagKind kind)
		{
			return kind.Value == EnglishPosTagSet.NNS.Value;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00005513 File Offset: 0x00003713
		public override Func<PosTagKind, bool> IsNonNoun()
		{
			return EnglishPosTagSet._isNonNoun;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000551A File Offset: 0x0000371A
		public override Func<StemmerSuggestion, bool> StemIsNoun()
		{
			return EnglishPosTagSet._stemIsNoun;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00005521 File Offset: 0x00003721
		public override Func<StemmerSuggestion, bool> StemIsNounPlural()
		{
			return EnglishPosTagSet._stemIsNounPlural;
		}

		// Token: 0x0400029C RID: 668
		public static readonly PosTagKind CC = new PosTagKind(1L);

		// Token: 0x0400029D RID: 669
		public static readonly PosTagKind CD = new PosTagKind(2L);

		// Token: 0x0400029E RID: 670
		public static readonly PosTagKind DT = new PosTagKind(3L);

		// Token: 0x0400029F RID: 671
		public static readonly PosTagKind EX = new PosTagKind(4L);

		// Token: 0x040002A0 RID: 672
		public static readonly PosTagKind FW = new PosTagKind(5L);

		// Token: 0x040002A1 RID: 673
		public static readonly PosTagKind IN = new PosTagKind(6L);

		// Token: 0x040002A2 RID: 674
		public static readonly PosTagKind AnyJJ = new PosTagKind(256L);

		// Token: 0x040002A3 RID: 675
		public static readonly PosTagKind JJ = new PosTagKind(257L);

		// Token: 0x040002A4 RID: 676
		public static readonly PosTagKind JJR = new PosTagKind(258L);

		// Token: 0x040002A5 RID: 677
		public static readonly PosTagKind JJS = new PosTagKind(259L);

		// Token: 0x040002A6 RID: 678
		public static readonly PosTagKind JJN = new PosTagKind(260L);

		// Token: 0x040002A7 RID: 679
		public static readonly PosTagKind JJRN = new PosTagKind(261L);

		// Token: 0x040002A8 RID: 680
		public static readonly PosTagKind JJSN = new PosTagKind(262L);

		// Token: 0x040002A9 RID: 681
		public static readonly PosTagKind LS = new PosTagKind(7L);

		// Token: 0x040002AA RID: 682
		public static readonly PosTagKind MD = new PosTagKind(8L);

		// Token: 0x040002AB RID: 683
		public static readonly PosTagKind AnyNN = new PosTagKind(512L);

		// Token: 0x040002AC RID: 684
		public static readonly PosTagKind NN = new PosTagKind(513L);

		// Token: 0x040002AD RID: 685
		public static readonly PosTagKind NNS = new PosTagKind(514L);

		// Token: 0x040002AE RID: 686
		public static readonly PosTagKind NNP = new PosTagKind(515L);

		// Token: 0x040002AF RID: 687
		public static readonly PosTagKind NNPS = new PosTagKind(516L);

		// Token: 0x040002B0 RID: 688
		public static readonly PosTagKind PDT = new PosTagKind(9L);

		// Token: 0x040002B1 RID: 689
		public static readonly PosTagKind POS = new PosTagKind(16L);

		// Token: 0x040002B2 RID: 690
		public static readonly PosTagKind AnyPRP = new PosTagKind(1024L);

		// Token: 0x040002B3 RID: 691
		public static readonly PosTagKind PRP = new PosTagKind(1025L);

		// Token: 0x040002B4 RID: 692
		public static readonly PosTagKind PRPS = new PosTagKind(1026L);

		// Token: 0x040002B5 RID: 693
		public static readonly PosTagKind AnyRB = new PosTagKind(2048L);

		// Token: 0x040002B6 RID: 694
		public static readonly PosTagKind RB = new PosTagKind(2049L);

		// Token: 0x040002B7 RID: 695
		public static readonly PosTagKind RBR = new PosTagKind(2050L);

		// Token: 0x040002B8 RID: 696
		public static readonly PosTagKind RBS = new PosTagKind(2051L);

		// Token: 0x040002B9 RID: 697
		public static readonly PosTagKind RP = new PosTagKind(17L);

		// Token: 0x040002BA RID: 698
		public static readonly PosTagKind SYM = new PosTagKind(18L);

		// Token: 0x040002BB RID: 699
		public static readonly PosTagKind TO = new PosTagKind(19L);

		// Token: 0x040002BC RID: 700
		public static readonly PosTagKind UH = new PosTagKind(20L);

		// Token: 0x040002BD RID: 701
		public static readonly PosTagKind AnyVB = new PosTagKind(4096L);

		// Token: 0x040002BE RID: 702
		public static readonly PosTagKind VB = new PosTagKind(4097L);

		// Token: 0x040002BF RID: 703
		public static readonly PosTagKind VBD = new PosTagKind(4098L);

		// Token: 0x040002C0 RID: 704
		public static readonly PosTagKind VBG = new PosTagKind(4099L);

		// Token: 0x040002C1 RID: 705
		public static readonly PosTagKind VBN = new PosTagKind(4100L);

		// Token: 0x040002C2 RID: 706
		public static readonly PosTagKind VBP = new PosTagKind(4101L);

		// Token: 0x040002C3 RID: 707
		public static readonly PosTagKind VBZ = new PosTagKind(4102L);

		// Token: 0x040002C4 RID: 708
		public static readonly PosTagKind WDT = new PosTagKind(21L);

		// Token: 0x040002C5 RID: 709
		public static readonly PosTagKind AnyWP = new PosTagKind(8192L);

		// Token: 0x040002C6 RID: 710
		public static readonly PosTagKind WP = new PosTagKind(8193L);

		// Token: 0x040002C7 RID: 711
		public static readonly PosTagKind WPS = new PosTagKind(8194L);

		// Token: 0x040002C8 RID: 712
		public static readonly PosTagKind WRB = new PosTagKind(22L);

		// Token: 0x040002C9 RID: 713
		private static readonly Func<PosTagKind, bool> _isNonNoun = (PosTagKind x) => !EnglishPosTagSet.Instance.IsNoun(x);

		// Token: 0x040002CA RID: 714
		private static readonly Func<StemmerSuggestion, bool> _stemIsNoun = (StemmerSuggestion x) => EnglishPosTagSet.Instance.IsNoun(x.PosTagKind);

		// Token: 0x040002CB RID: 715
		private static readonly Func<StemmerSuggestion, bool> _stemIsNounPlural = (StemmerSuggestion x) => EnglishPosTagSet.Instance.IsNounPlural(x.PosTagKind);

		// Token: 0x040002CC RID: 716
		public static readonly EnglishPosTagSet Instance = new EnglishPosTagSet();
	}
}
