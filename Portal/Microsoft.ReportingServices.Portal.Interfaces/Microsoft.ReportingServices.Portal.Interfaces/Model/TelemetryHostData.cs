using System;

namespace Model
{
	// Token: 0x0200007B RID: 123
	public sealed class TelemetryHostData
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060003CB RID: 971 RVA: 0x00004782 File Offset: 0x00002982
		// (set) Token: 0x060003CC RID: 972 RVA: 0x0000478A File Offset: 0x0000298A
		public string Build { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00004793 File Offset: 0x00002993
		// (set) Token: 0x060003CE RID: 974 RVA: 0x0000479B File Offset: 0x0000299B
		public string ExternalUser { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060003CF RID: 975 RVA: 0x000047A4 File Offset: 0x000029A4
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x000047AC File Offset: 0x000029AC
		public bool IsPublicBuild { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x000047B5 File Offset: 0x000029B5
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x000047BD File Offset: 0x000029BD
		public string Host { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x000047C6 File Offset: 0x000029C6
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x000047CE File Offset: 0x000029CE
		public string HashedUserId { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x000047D7 File Offset: 0x000029D7
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x000047DF File Offset: 0x000029DF
		public string InstallationId { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x000047E8 File Offset: 0x000029E8
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x000047F0 File Offset: 0x000029F0
		public bool IsEnabled { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x000047F9 File Offset: 0x000029F9
		// (set) Token: 0x060003DA RID: 986 RVA: 0x00004801 File Offset: 0x00002A01
		public string Edition { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000480A File Offset: 0x00002A0A
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00004812 File Offset: 0x00002A12
		public string AuthenticationTypes { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000481B File Offset: 0x00002A1B
		// (set) Token: 0x060003DE RID: 990 RVA: 0x00004823 File Offset: 0x00002A23
		public int NumberOfProcessors { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000482C File Offset: 0x00002A2C
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x00004834 File Offset: 0x00002A34
		public int NumberOfCores { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000483D File Offset: 0x00002A3D
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x00004845 File Offset: 0x00002A45
		public bool IsVirtualMachine { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000484E File Offset: 0x00002A4E
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x00004856 File Offset: 0x00002A56
		public string MachineId { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000485F File Offset: 0x00002A5F
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x00004867 File Offset: 0x00002A67
		public int CountInstances { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00004870 File Offset: 0x00002A70
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x00004878 File Offset: 0x00002A78
		public int Count14xInstances { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00004881 File Offset: 0x00002A81
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x00004889 File Offset: 0x00002A89
		public int Count13xInstances { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00004892 File Offset: 0x00002A92
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000489A File Offset: 0x00002A9A
		public int Count12xInstances { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x000048A3 File Offset: 0x00002AA3
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x000048AB File Offset: 0x00002AAB
		public int Count11xInstances { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x000048B4 File Offset: 0x00002AB4
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x000048BC File Offset: 0x00002ABC
		public string ProductSku { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x000048C5 File Offset: 0x00002AC5
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x000048CD File Offset: 0x00002ACD
		public string PortalVersion { get; set; }
	}
}
