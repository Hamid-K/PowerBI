using System;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001E1 RID: 481
	internal static class Constants
	{
		// Token: 0x0200042E RID: 1070
		internal static class Savepoints
		{
			// Token: 0x040013A1 RID: 5025
			internal const string Synced = "Synced";

			// Token: 0x040013A2 RID: 5026
			internal const string SyncedMostRecent = "SyncedMostRecent";

			// Token: 0x040013A3 RID: 5027
			internal const string Saved = "Saved";

			// Token: 0x040013A4 RID: 5028
			internal const string Modified = "Modified";

			// Token: 0x040013A5 RID: 5029
			internal const string BeginTransaction = "BeginTransaction";
		}

		// Token: 0x0200042F RID: 1071
		internal static class CompatibilityLevel
		{
			// Token: 0x06002895 RID: 10389 RVA: 0x000EF9AF File Offset: 0x000EDBAF
			internal static bool IsValidLevel(int level)
			{
				return level == -1 || level == int.MaxValue || (level >= 1200 && level <= 1000000);
			}

			// Token: 0x040013A6 RID: 5030
			internal const int Unknown = -3;

			// Token: 0x040013A7 RID: 5031
			internal const int Unsupported = -2;

			// Token: 0x040013A8 RID: 5032
			internal const int Unbound = -1;

			// Token: 0x040013A9 RID: 5033
			internal const int Level1200 = 1200;

			// Token: 0x040013AA RID: 5034
			internal const int Level1400 = 1400;

			// Token: 0x040013AB RID: 5035
			internal const int Level1450 = 1450;

			// Token: 0x040013AC RID: 5036
			internal const int Level1455 = 1455;

			// Token: 0x040013AD RID: 5037
			internal const int Level1460 = 1460;

			// Token: 0x040013AE RID: 5038
			internal const int Level1465 = 1465;

			// Token: 0x040013AF RID: 5039
			internal const int Level1470 = 1470;

			// Token: 0x040013B0 RID: 5040
			internal const int Level1475 = 1475;

			// Token: 0x040013B1 RID: 5041
			internal const int Level1480 = 1480;

			// Token: 0x040013B2 RID: 5042
			internal const int Level1500 = 1500;

			// Token: 0x040013B3 RID: 5043
			internal const int Level1510 = 1510;

			// Token: 0x040013B4 RID: 5044
			internal const int Level1520 = 1520;

			// Token: 0x040013B5 RID: 5045
			internal const int Level1530 = 1530;

			// Token: 0x040013B6 RID: 5046
			internal const int Level1535 = 1535;

			// Token: 0x040013B7 RID: 5047
			internal const int Level1540 = 1540;

			// Token: 0x040013B8 RID: 5048
			internal const int Level1545 = 1545;

			// Token: 0x040013B9 RID: 5049
			internal const int Level1550 = 1550;

			// Token: 0x040013BA RID: 5050
			internal const int Level1560 = 1560;

			// Token: 0x040013BB RID: 5051
			internal const int Level1561 = 1561;

			// Token: 0x040013BC RID: 5052
			internal const int Level1562 = 1562;

			// Token: 0x040013BD RID: 5053
			internal const int Level1563 = 1563;

			// Token: 0x040013BE RID: 5054
			internal const int Level1564 = 1564;

			// Token: 0x040013BF RID: 5055
			internal const int Level1565 = 1565;

			// Token: 0x040013C0 RID: 5056
			internal const int Level1566 = 1566;

			// Token: 0x040013C1 RID: 5057
			internal const int Level1567 = 1567;

			// Token: 0x040013C2 RID: 5058
			internal const int Level1568 = 1568;

			// Token: 0x040013C3 RID: 5059
			internal const int Level1569 = 1569;

			// Token: 0x040013C4 RID: 5060
			internal const int Level1570 = 1570;

			// Token: 0x040013C5 RID: 5061
			internal const int Level1571 = 1571;

			// Token: 0x040013C6 RID: 5062
			internal const int Level1572 = 1572;

			// Token: 0x040013C7 RID: 5063
			internal const int Level1600 = 1600;

			// Token: 0x040013C8 RID: 5064
			internal const int Level1601 = 1601;

			// Token: 0x040013C9 RID: 5065
			internal const int Level1602 = 1602;

			// Token: 0x040013CA RID: 5066
			internal const int Level1603 = 1603;

			// Token: 0x040013CB RID: 5067
			internal const int Level1604 = 1604;

			// Token: 0x040013CC RID: 5068
			internal const int Level1605 = 1605;

			// Token: 0x040013CD RID: 5069
			internal const int Level1606 = 1606;

			// Token: 0x040013CE RID: 5070
			internal const int Level1607 = 1607;

			// Token: 0x040013CF RID: 5071
			internal const int Preview = 1000000;

			// Token: 0x040013D0 RID: 5072
			internal const int Internal = 2147483647;

			// Token: 0x040013D1 RID: 5073
			internal const int Default = 1600;

			// Token: 0x040013D2 RID: 5074
			internal const int Minimum = 1200;

			// Token: 0x040013D3 RID: 5075
			internal static readonly int[] SupportedCompatibilityLevels = new int[]
			{
				1200, 1400, 1450, 1455, 1460, 1465, 1470, 1475, 1480, 1500,
				1510, 1520, 1530, 1535, 1540, 1545, 1550, 1560, 1561, 1562,
				1563, 1564, 1565, 1566, 1567, 1568, 1569, 1570, 1571, 1572,
				1600, 1601, 1602, 1603, 1604, 1605, 1606, 1607, 1000000
			};
		}
	}
}
