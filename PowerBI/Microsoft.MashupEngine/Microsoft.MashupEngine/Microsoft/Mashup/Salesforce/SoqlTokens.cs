using System;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x020001FF RID: 511
	internal struct SoqlTokens
	{
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x00016DFF File Offset: 0x00014FFF
		public static Token Select
		{
			get
			{
				return new Token("SELECT");
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x00016E0B File Offset: 0x0001500B
		public static Token From
		{
			get
			{
				return new Token("FROM");
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00016E17 File Offset: 0x00015017
		public new static Token Equals
		{
			get
			{
				return new Token("=");
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x00016E23 File Offset: 0x00015023
		public static Token Comma
		{
			get
			{
				return new Token(",");
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x00016E2F File Offset: 0x0001502F
		public static Token Limit
		{
			get
			{
				return new Token("LIMIT");
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x00016E3B File Offset: 0x0001503B
		public static Token Offset
		{
			get
			{
				return new Token("OFFSET");
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x00016E47 File Offset: 0x00015047
		public static Token Where
		{
			get
			{
				return new Token("WHERE");
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x00016E53 File Offset: 0x00015053
		public static Token LeftParen
		{
			get
			{
				return new Token("(");
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x00016E5F File Offset: 0x0001505F
		public static Token RightParen
		{
			get
			{
				return new Token(")");
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x00016E6B File Offset: 0x0001506B
		public static Token Star
		{
			get
			{
				return new Token("*");
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00016E77 File Offset: 0x00015077
		public static Token GreaterThan
		{
			get
			{
				return new Token(">");
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00016E83 File Offset: 0x00015083
		public static Token LessThan
		{
			get
			{
				return new Token("<");
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x00016E8F File Offset: 0x0001508F
		public static Token GreaterThanOrEquals
		{
			get
			{
				return new Token(">=");
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x00016E9B File Offset: 0x0001509B
		public static Token LessThanOrEquals
		{
			get
			{
				return new Token("<=");
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x00016EA7 File Offset: 0x000150A7
		public static Token And
		{
			get
			{
				return new Token("AND");
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x00016EB3 File Offset: 0x000150B3
		public static Token Or
		{
			get
			{
				return new Token("OR");
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x00016EBF File Offset: 0x000150BF
		public static Token NotEquals
		{
			get
			{
				return new Token("<>");
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x00016ECB File Offset: 0x000150CB
		public static Token Not
		{
			get
			{
				return new Token("NOT ");
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x00016ED7 File Offset: 0x000150D7
		public static Token Count
		{
			get
			{
				return new Token("COUNT");
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00016EE3 File Offset: 0x000150E3
		public static Token Group
		{
			get
			{
				return new Token("GROUP");
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00016EEF File Offset: 0x000150EF
		public static Token Order
		{
			get
			{
				return new Token("ORDER");
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x00016EFB File Offset: 0x000150FB
		public static Token Desc
		{
			get
			{
				return new Token("DESC");
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00016F07 File Offset: 0x00015107
		public static Token By
		{
			get
			{
				return new Token("BY");
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00016F13 File Offset: 0x00015113
		public static Token Avg
		{
			get
			{
				return new Token("AVG");
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x00016F1F File Offset: 0x0001511F
		public static Token Max
		{
			get
			{
				return new Token("MAX");
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x00016F2B File Offset: 0x0001512B
		public static Token Min
		{
			get
			{
				return new Token("MIN");
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x00016F37 File Offset: 0x00015137
		public static Token Sum
		{
			get
			{
				return new Token("SUM");
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00016F43 File Offset: 0x00015143
		public static Token Like
		{
			get
			{
				return new Token("LIKE");
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x00016F4F File Offset: 0x0001514F
		public static Token In
		{
			get
			{
				return new Token("IN");
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x00016F5B File Offset: 0x0001515B
		public static Token CalendarMonth
		{
			get
			{
				return new Token("CALENDAR_MONTH");
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x00016F67 File Offset: 0x00015167
		public static Token CalendarQuarter
		{
			get
			{
				return new Token("CALENDAR_QUARTER");
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00016F73 File Offset: 0x00015173
		public static Token CalendarYear
		{
			get
			{
				return new Token("CALENDAR_YEAR");
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x00016F7F File Offset: 0x0001517F
		public static Token DayInMonth
		{
			get
			{
				return new Token("DAY_IN_MONTH");
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x00016F8B File Offset: 0x0001518B
		public static Token DayInWeek
		{
			get
			{
				return new Token("DAY_IN_WEEK");
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x00016F97 File Offset: 0x00015197
		public static Token DayInYear
		{
			get
			{
				return new Token("DAY_IN_YEAR");
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00016FA3 File Offset: 0x000151A3
		public static Token DayOnly
		{
			get
			{
				return new Token("DAY_ONLY");
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x00016FAF File Offset: 0x000151AF
		public static Token WeekInMonth
		{
			get
			{
				return new Token("WEEK_IN_MONTH");
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00016FBB File Offset: 0x000151BB
		public static Token WeekInYear
		{
			get
			{
				return new Token("WEEK_IN_YEAR");
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x00016FC7 File Offset: 0x000151C7
		public static Token HourInDay
		{
			get
			{
				return new Token("HOUR_IN_DAY");
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00016FD3 File Offset: 0x000151D3
		public static Token Null
		{
			get
			{
				return new Token("NULL");
			}
		}
	}
}
