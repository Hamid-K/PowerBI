using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000A2 RID: 162
	public enum LevelTypeEnum
	{
		// Token: 0x0400060E RID: 1550
		Regular,
		// Token: 0x0400060F RID: 1551
		All,
		// Token: 0x04000610 RID: 1552
		Calculated,
		// Token: 0x04000611 RID: 1553
		Time = 4,
		// Token: 0x04000612 RID: 1554
		Reserved1 = 8,
		// Token: 0x04000613 RID: 1555
		TimeYears = 20,
		// Token: 0x04000614 RID: 1556
		TimeHalfYears = 36,
		// Token: 0x04000615 RID: 1557
		TimeQuarters = 68,
		// Token: 0x04000616 RID: 1558
		TimeMonths = 132,
		// Token: 0x04000617 RID: 1559
		TimeWeeks = 260,
		// Token: 0x04000618 RID: 1560
		TimeDays = 516,
		// Token: 0x04000619 RID: 1561
		TimeHours = 772,
		// Token: 0x0400061A RID: 1562
		TimeMinutes = 1028,
		// Token: 0x0400061B RID: 1563
		TimeSeconds = 2052,
		// Token: 0x0400061C RID: 1564
		TimeUndefined = 4100,
		// Token: 0x0400061D RID: 1565
		GeoContinent = 8193,
		// Token: 0x0400061E RID: 1566
		GeoRegion,
		// Token: 0x0400061F RID: 1567
		GeoCountry,
		// Token: 0x04000620 RID: 1568
		GeoStateOrProvince,
		// Token: 0x04000621 RID: 1569
		GeoCounty,
		// Token: 0x04000622 RID: 1570
		GeoCity,
		// Token: 0x04000623 RID: 1571
		GeoPostalCode,
		// Token: 0x04000624 RID: 1572
		GeoPoint,
		// Token: 0x04000625 RID: 1573
		OrgUnit = 4113,
		// Token: 0x04000626 RID: 1574
		BomResource,
		// Token: 0x04000627 RID: 1575
		Quantitative,
		// Token: 0x04000628 RID: 1576
		Account,
		// Token: 0x04000629 RID: 1577
		Customer = 4129,
		// Token: 0x0400062A RID: 1578
		CustomerGroup,
		// Token: 0x0400062B RID: 1579
		CustomerHousehold,
		// Token: 0x0400062C RID: 1580
		Product = 4145,
		// Token: 0x0400062D RID: 1581
		ProductGroup,
		// Token: 0x0400062E RID: 1582
		Scenario = 4117,
		// Token: 0x0400062F RID: 1583
		Utility,
		// Token: 0x04000630 RID: 1584
		Person = 4161,
		// Token: 0x04000631 RID: 1585
		Company,
		// Token: 0x04000632 RID: 1586
		CurrencySource = 4177,
		// Token: 0x04000633 RID: 1587
		CurrencyDestination,
		// Token: 0x04000634 RID: 1588
		Channel = 4193,
		// Token: 0x04000635 RID: 1589
		Representative,
		// Token: 0x04000636 RID: 1590
		Promotion = 4209
	}
}
