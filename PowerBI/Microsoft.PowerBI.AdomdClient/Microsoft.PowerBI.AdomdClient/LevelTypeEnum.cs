using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000A2 RID: 162
	public enum LevelTypeEnum
	{
		// Token: 0x04000601 RID: 1537
		Regular,
		// Token: 0x04000602 RID: 1538
		All,
		// Token: 0x04000603 RID: 1539
		Calculated,
		// Token: 0x04000604 RID: 1540
		Time = 4,
		// Token: 0x04000605 RID: 1541
		Reserved1 = 8,
		// Token: 0x04000606 RID: 1542
		TimeYears = 20,
		// Token: 0x04000607 RID: 1543
		TimeHalfYears = 36,
		// Token: 0x04000608 RID: 1544
		TimeQuarters = 68,
		// Token: 0x04000609 RID: 1545
		TimeMonths = 132,
		// Token: 0x0400060A RID: 1546
		TimeWeeks = 260,
		// Token: 0x0400060B RID: 1547
		TimeDays = 516,
		// Token: 0x0400060C RID: 1548
		TimeHours = 772,
		// Token: 0x0400060D RID: 1549
		TimeMinutes = 1028,
		// Token: 0x0400060E RID: 1550
		TimeSeconds = 2052,
		// Token: 0x0400060F RID: 1551
		TimeUndefined = 4100,
		// Token: 0x04000610 RID: 1552
		GeoContinent = 8193,
		// Token: 0x04000611 RID: 1553
		GeoRegion,
		// Token: 0x04000612 RID: 1554
		GeoCountry,
		// Token: 0x04000613 RID: 1555
		GeoStateOrProvince,
		// Token: 0x04000614 RID: 1556
		GeoCounty,
		// Token: 0x04000615 RID: 1557
		GeoCity,
		// Token: 0x04000616 RID: 1558
		GeoPostalCode,
		// Token: 0x04000617 RID: 1559
		GeoPoint,
		// Token: 0x04000618 RID: 1560
		OrgUnit = 4113,
		// Token: 0x04000619 RID: 1561
		BomResource,
		// Token: 0x0400061A RID: 1562
		Quantitative,
		// Token: 0x0400061B RID: 1563
		Account,
		// Token: 0x0400061C RID: 1564
		Customer = 4129,
		// Token: 0x0400061D RID: 1565
		CustomerGroup,
		// Token: 0x0400061E RID: 1566
		CustomerHousehold,
		// Token: 0x0400061F RID: 1567
		Product = 4145,
		// Token: 0x04000620 RID: 1568
		ProductGroup,
		// Token: 0x04000621 RID: 1569
		Scenario = 4117,
		// Token: 0x04000622 RID: 1570
		Utility,
		// Token: 0x04000623 RID: 1571
		Person = 4161,
		// Token: 0x04000624 RID: 1572
		Company,
		// Token: 0x04000625 RID: 1573
		CurrencySource = 4177,
		// Token: 0x04000626 RID: 1574
		CurrencyDestination,
		// Token: 0x04000627 RID: 1575
		Channel = 4193,
		// Token: 0x04000628 RID: 1576
		Representative,
		// Token: 0x04000629 RID: 1577
		Promotion = 4209
	}
}
