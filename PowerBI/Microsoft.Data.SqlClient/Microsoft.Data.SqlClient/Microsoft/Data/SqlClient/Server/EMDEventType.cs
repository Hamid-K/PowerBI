using System;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000150 RID: 336
	internal enum EMDEventType
	{
		// Token: 0x040009E3 RID: 2531
		x_eet_Invalid,
		// Token: 0x040009E4 RID: 2532
		x_eet_Insert,
		// Token: 0x040009E5 RID: 2533
		x_eet_Update,
		// Token: 0x040009E6 RID: 2534
		x_eet_Delete,
		// Token: 0x040009E7 RID: 2535
		x_eet_Create_Table = 21,
		// Token: 0x040009E8 RID: 2536
		x_eet_Alter_Table,
		// Token: 0x040009E9 RID: 2537
		x_eet_Drop_Table,
		// Token: 0x040009EA RID: 2538
		x_eet_Create_Index,
		// Token: 0x040009EB RID: 2539
		x_eet_Alter_Index,
		// Token: 0x040009EC RID: 2540
		x_eet_Drop_Index,
		// Token: 0x040009ED RID: 2541
		x_eet_Create_Stats,
		// Token: 0x040009EE RID: 2542
		x_eet_Update_Stats,
		// Token: 0x040009EF RID: 2543
		x_eet_Drop_Stats,
		// Token: 0x040009F0 RID: 2544
		x_eet_Create_Secexpr = 31,
		// Token: 0x040009F1 RID: 2545
		x_eet_Drop_Secexpr = 33,
		// Token: 0x040009F2 RID: 2546
		x_eet_Create_Synonym,
		// Token: 0x040009F3 RID: 2547
		x_eet_Drop_Synonym = 36,
		// Token: 0x040009F4 RID: 2548
		x_eet_Create_View = 41,
		// Token: 0x040009F5 RID: 2549
		x_eet_Alter_View,
		// Token: 0x040009F6 RID: 2550
		x_eet_Drop_View,
		// Token: 0x040009F7 RID: 2551
		x_eet_Create_Procedure = 51,
		// Token: 0x040009F8 RID: 2552
		x_eet_Alter_Procedure,
		// Token: 0x040009F9 RID: 2553
		x_eet_Drop_Procedure,
		// Token: 0x040009FA RID: 2554
		x_eet_Create_Function = 61,
		// Token: 0x040009FB RID: 2555
		x_eet_Alter_Function,
		// Token: 0x040009FC RID: 2556
		x_eet_Drop_Function,
		// Token: 0x040009FD RID: 2557
		x_eet_Create_Trigger = 71,
		// Token: 0x040009FE RID: 2558
		x_eet_Alter_Trigger,
		// Token: 0x040009FF RID: 2559
		x_eet_Drop_Trigger,
		// Token: 0x04000A00 RID: 2560
		x_eet_Create_Event_Notification,
		// Token: 0x04000A01 RID: 2561
		x_eet_Drop_Event_Notification = 76,
		// Token: 0x04000A02 RID: 2562
		x_eet_Create_Type = 91,
		// Token: 0x04000A03 RID: 2563
		x_eet_Drop_Type = 93,
		// Token: 0x04000A04 RID: 2564
		x_eet_Create_Assembly = 101,
		// Token: 0x04000A05 RID: 2565
		x_eet_Alter_Assembly,
		// Token: 0x04000A06 RID: 2566
		x_eet_Drop_Assembly,
		// Token: 0x04000A07 RID: 2567
		x_eet_Create_User = 131,
		// Token: 0x04000A08 RID: 2568
		x_eet_Alter_User,
		// Token: 0x04000A09 RID: 2569
		x_eet_Drop_User,
		// Token: 0x04000A0A RID: 2570
		x_eet_Create_Role,
		// Token: 0x04000A0B RID: 2571
		x_eet_Alter_Role,
		// Token: 0x04000A0C RID: 2572
		x_eet_Drop_Role,
		// Token: 0x04000A0D RID: 2573
		x_eet_Create_AppRole,
		// Token: 0x04000A0E RID: 2574
		x_eet_Alter_AppRole,
		// Token: 0x04000A0F RID: 2575
		x_eet_Drop_AppRole,
		// Token: 0x04000A10 RID: 2576
		x_eet_Create_Schema = 141,
		// Token: 0x04000A11 RID: 2577
		x_eet_Alter_Schema,
		// Token: 0x04000A12 RID: 2578
		x_eet_Drop_Schema,
		// Token: 0x04000A13 RID: 2579
		x_eet_Create_Login,
		// Token: 0x04000A14 RID: 2580
		x_eet_Alter_Login,
		// Token: 0x04000A15 RID: 2581
		x_eet_Drop_Login,
		// Token: 0x04000A16 RID: 2582
		x_eet_Create_MsgType = 151,
		// Token: 0x04000A17 RID: 2583
		x_eet_Alter_MsgType,
		// Token: 0x04000A18 RID: 2584
		x_eet_Drop_MsgType,
		// Token: 0x04000A19 RID: 2585
		x_eet_Create_Contract,
		// Token: 0x04000A1A RID: 2586
		x_eet_Alter_Contract,
		// Token: 0x04000A1B RID: 2587
		x_eet_Drop_Contract,
		// Token: 0x04000A1C RID: 2588
		x_eet_Create_Queue,
		// Token: 0x04000A1D RID: 2589
		x_eet_Alter_Queue,
		// Token: 0x04000A1E RID: 2590
		x_eet_Drop_Queue,
		// Token: 0x04000A1F RID: 2591
		x_eet_Create_Service = 161,
		// Token: 0x04000A20 RID: 2592
		x_eet_Alter_Service,
		// Token: 0x04000A21 RID: 2593
		x_eet_Drop_Service,
		// Token: 0x04000A22 RID: 2594
		x_eet_Create_Route,
		// Token: 0x04000A23 RID: 2595
		x_eet_Alter_Route,
		// Token: 0x04000A24 RID: 2596
		x_eet_Drop_Route,
		// Token: 0x04000A25 RID: 2597
		x_eet_Grant_Statement,
		// Token: 0x04000A26 RID: 2598
		x_eet_Deny_Statement,
		// Token: 0x04000A27 RID: 2599
		x_eet_Revoke_Statement,
		// Token: 0x04000A28 RID: 2600
		x_eet_Grant_Object,
		// Token: 0x04000A29 RID: 2601
		x_eet_Deny_Object,
		// Token: 0x04000A2A RID: 2602
		x_eet_Revoke_Object,
		// Token: 0x04000A2B RID: 2603
		x_eet_Activation,
		// Token: 0x04000A2C RID: 2604
		x_eet_Create_Binding,
		// Token: 0x04000A2D RID: 2605
		x_eet_Alter_Binding,
		// Token: 0x04000A2E RID: 2606
		x_eet_Drop_Binding,
		// Token: 0x04000A2F RID: 2607
		x_eet_Create_XmlSchema,
		// Token: 0x04000A30 RID: 2608
		x_eet_Alter_XmlSchema,
		// Token: 0x04000A31 RID: 2609
		x_eet_Drop_XmlSchema,
		// Token: 0x04000A32 RID: 2610
		x_eet_Create_HttpEndpoint = 181,
		// Token: 0x04000A33 RID: 2611
		x_eet_Alter_HttpEndpoint,
		// Token: 0x04000A34 RID: 2612
		x_eet_Drop_HttpEndpoint,
		// Token: 0x04000A35 RID: 2613
		x_eet_Create_Partition_Function = 191,
		// Token: 0x04000A36 RID: 2614
		x_eet_Alter_Partition_Function,
		// Token: 0x04000A37 RID: 2615
		x_eet_Drop_Partition_Function,
		// Token: 0x04000A38 RID: 2616
		x_eet_Create_Partition_Scheme,
		// Token: 0x04000A39 RID: 2617
		x_eet_Alter_Partition_Scheme,
		// Token: 0x04000A3A RID: 2618
		x_eet_Drop_Partition_Scheme,
		// Token: 0x04000A3B RID: 2619
		x_eet_Create_Database = 201,
		// Token: 0x04000A3C RID: 2620
		x_eet_Alter_Database,
		// Token: 0x04000A3D RID: 2621
		x_eet_Drop_Database,
		// Token: 0x04000A3E RID: 2622
		x_eet_Trace_Start = 1000,
		// Token: 0x04000A3F RID: 2623
		x_eet_Trace_End = 1999
	}
}
