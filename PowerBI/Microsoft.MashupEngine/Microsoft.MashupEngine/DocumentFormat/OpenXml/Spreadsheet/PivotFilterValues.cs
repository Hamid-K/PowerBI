using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CF8 RID: 11512
	[GeneratedCode("DomGen", "2.0")]
	internal enum PivotFilterValues
	{
		// Token: 0x0400A208 RID: 41480
		[EnumString("unknown")]
		Unknown,
		// Token: 0x0400A209 RID: 41481
		[EnumString("count")]
		Count,
		// Token: 0x0400A20A RID: 41482
		[EnumString("percent")]
		Percent,
		// Token: 0x0400A20B RID: 41483
		[EnumString("sum")]
		Sum,
		// Token: 0x0400A20C RID: 41484
		[EnumString("captionEqual")]
		CaptionEqual,
		// Token: 0x0400A20D RID: 41485
		[EnumString("captionNotEqual")]
		CaptionNotEqual,
		// Token: 0x0400A20E RID: 41486
		[EnumString("captionBeginsWith")]
		CaptionBeginsWith,
		// Token: 0x0400A20F RID: 41487
		[EnumString("captionNotBeginsWith")]
		CaptionNotBeginsWith,
		// Token: 0x0400A210 RID: 41488
		[EnumString("captionEndsWith")]
		CaptionEndsWith,
		// Token: 0x0400A211 RID: 41489
		[EnumString("captionNotEndsWith")]
		CaptionNotEndsWith,
		// Token: 0x0400A212 RID: 41490
		[EnumString("captionContains")]
		CaptionContains,
		// Token: 0x0400A213 RID: 41491
		[EnumString("captionNotContains")]
		CaptionNotContains,
		// Token: 0x0400A214 RID: 41492
		[EnumString("captionGreaterThan")]
		CaptionGreaterThan,
		// Token: 0x0400A215 RID: 41493
		[EnumString("captionGreaterThanOrEqual")]
		CaptionGreaterThanOrEqual,
		// Token: 0x0400A216 RID: 41494
		[EnumString("captionLessThan")]
		CaptionLessThan,
		// Token: 0x0400A217 RID: 41495
		[EnumString("captionLessThanOrEqual")]
		CaptionLessThanOrEqual,
		// Token: 0x0400A218 RID: 41496
		[EnumString("captionBetween")]
		CaptionBetween,
		// Token: 0x0400A219 RID: 41497
		[EnumString("captionNotBetween")]
		CaptionNotBetween,
		// Token: 0x0400A21A RID: 41498
		[EnumString("valueEqual")]
		ValueEqual,
		// Token: 0x0400A21B RID: 41499
		[EnumString("valueNotEqual")]
		ValueNotEqual,
		// Token: 0x0400A21C RID: 41500
		[EnumString("valueGreaterThan")]
		ValueGreaterThan,
		// Token: 0x0400A21D RID: 41501
		[EnumString("valueGreaterThanOrEqual")]
		ValueGreaterThanOrEqual,
		// Token: 0x0400A21E RID: 41502
		[EnumString("valueLessThan")]
		ValueLessThan,
		// Token: 0x0400A21F RID: 41503
		[EnumString("valueLessThanOrEqual")]
		ValueLessThanOrEqual,
		// Token: 0x0400A220 RID: 41504
		[EnumString("valueBetween")]
		ValueBetween,
		// Token: 0x0400A221 RID: 41505
		[EnumString("valueNotBetween")]
		ValueNotBetween,
		// Token: 0x0400A222 RID: 41506
		[EnumString("dateEqual")]
		DateEqual,
		// Token: 0x0400A223 RID: 41507
		[EnumString("dateNotEqual")]
		DateNotEqual,
		// Token: 0x0400A224 RID: 41508
		[EnumString("dateOlderThan")]
		DateOlderThan,
		// Token: 0x0400A225 RID: 41509
		[EnumString("dateOlderThanOrEqual")]
		DateOlderThanOrEqual,
		// Token: 0x0400A226 RID: 41510
		[EnumString("dateNewerThan")]
		DateNewerThan,
		// Token: 0x0400A227 RID: 41511
		[EnumString("dateNewerThanOrEqual")]
		DateNewerThanOrEqual,
		// Token: 0x0400A228 RID: 41512
		[EnumString("dateBetween")]
		DateBetween,
		// Token: 0x0400A229 RID: 41513
		[EnumString("dateNotBetween")]
		DateNotBetween,
		// Token: 0x0400A22A RID: 41514
		[EnumString("tomorrow")]
		Tomorrow,
		// Token: 0x0400A22B RID: 41515
		[EnumString("today")]
		Today,
		// Token: 0x0400A22C RID: 41516
		[EnumString("yesterday")]
		Yesterday,
		// Token: 0x0400A22D RID: 41517
		[EnumString("nextWeek")]
		NextWeek,
		// Token: 0x0400A22E RID: 41518
		[EnumString("thisWeek")]
		ThisWeek,
		// Token: 0x0400A22F RID: 41519
		[EnumString("lastWeek")]
		LastWeek,
		// Token: 0x0400A230 RID: 41520
		[EnumString("nextMonth")]
		NextMonth,
		// Token: 0x0400A231 RID: 41521
		[EnumString("thisMonth")]
		ThisMonth,
		// Token: 0x0400A232 RID: 41522
		[EnumString("lastMonth")]
		LastMonth,
		// Token: 0x0400A233 RID: 41523
		[EnumString("nextQuarter")]
		NextQuarter,
		// Token: 0x0400A234 RID: 41524
		[EnumString("thisQuarter")]
		ThisQuarter,
		// Token: 0x0400A235 RID: 41525
		[EnumString("lastQuarter")]
		LastQuarter,
		// Token: 0x0400A236 RID: 41526
		[EnumString("nextYear")]
		NextYear,
		// Token: 0x0400A237 RID: 41527
		[EnumString("thisYear")]
		ThisYear,
		// Token: 0x0400A238 RID: 41528
		[EnumString("lastYear")]
		LastYear,
		// Token: 0x0400A239 RID: 41529
		[EnumString("yearToDate")]
		YearToDate,
		// Token: 0x0400A23A RID: 41530
		[EnumString("Q1")]
		Quarter1,
		// Token: 0x0400A23B RID: 41531
		[EnumString("Q2")]
		Quarter2,
		// Token: 0x0400A23C RID: 41532
		[EnumString("Q3")]
		Quarter3,
		// Token: 0x0400A23D RID: 41533
		[EnumString("Q4")]
		Quarter4,
		// Token: 0x0400A23E RID: 41534
		[EnumString("M1")]
		January,
		// Token: 0x0400A23F RID: 41535
		[EnumString("M2")]
		February,
		// Token: 0x0400A240 RID: 41536
		[EnumString("M3")]
		March,
		// Token: 0x0400A241 RID: 41537
		[EnumString("M4")]
		April,
		// Token: 0x0400A242 RID: 41538
		[EnumString("M5")]
		May,
		// Token: 0x0400A243 RID: 41539
		[EnumString("M6")]
		June,
		// Token: 0x0400A244 RID: 41540
		[EnumString("M7")]
		July,
		// Token: 0x0400A245 RID: 41541
		[EnumString("M8")]
		August,
		// Token: 0x0400A246 RID: 41542
		[EnumString("M9")]
		September,
		// Token: 0x0400A247 RID: 41543
		[EnumString("M10")]
		October,
		// Token: 0x0400A248 RID: 41544
		[EnumString("M11")]
		November,
		// Token: 0x0400A249 RID: 41545
		[EnumString("M12")]
		December
	}
}
