using System;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000151 RID: 337
	public enum TriggerAction
	{
		// Token: 0x04000A41 RID: 2625
		Invalid,
		// Token: 0x04000A42 RID: 2626
		Insert,
		// Token: 0x04000A43 RID: 2627
		Update,
		// Token: 0x04000A44 RID: 2628
		Delete,
		// Token: 0x04000A45 RID: 2629
		CreateTable = 21,
		// Token: 0x04000A46 RID: 2630
		AlterTable,
		// Token: 0x04000A47 RID: 2631
		DropTable,
		// Token: 0x04000A48 RID: 2632
		CreateIndex,
		// Token: 0x04000A49 RID: 2633
		AlterIndex,
		// Token: 0x04000A4A RID: 2634
		DropIndex,
		// Token: 0x04000A4B RID: 2635
		CreateSynonym = 34,
		// Token: 0x04000A4C RID: 2636
		DropSynonym = 36,
		// Token: 0x04000A4D RID: 2637
		CreateSecurityExpression = 31,
		// Token: 0x04000A4E RID: 2638
		DropSecurityExpression = 33,
		// Token: 0x04000A4F RID: 2639
		CreateView = 41,
		// Token: 0x04000A50 RID: 2640
		AlterView,
		// Token: 0x04000A51 RID: 2641
		DropView,
		// Token: 0x04000A52 RID: 2642
		CreateProcedure = 51,
		// Token: 0x04000A53 RID: 2643
		AlterProcedure,
		// Token: 0x04000A54 RID: 2644
		DropProcedure,
		// Token: 0x04000A55 RID: 2645
		CreateFunction = 61,
		// Token: 0x04000A56 RID: 2646
		AlterFunction,
		// Token: 0x04000A57 RID: 2647
		DropFunction,
		// Token: 0x04000A58 RID: 2648
		CreateTrigger = 71,
		// Token: 0x04000A59 RID: 2649
		AlterTrigger,
		// Token: 0x04000A5A RID: 2650
		DropTrigger,
		// Token: 0x04000A5B RID: 2651
		CreateEventNotification,
		// Token: 0x04000A5C RID: 2652
		DropEventNotification = 76,
		// Token: 0x04000A5D RID: 2653
		CreateType = 91,
		// Token: 0x04000A5E RID: 2654
		DropType = 93,
		// Token: 0x04000A5F RID: 2655
		CreateAssembly = 101,
		// Token: 0x04000A60 RID: 2656
		AlterAssembly,
		// Token: 0x04000A61 RID: 2657
		DropAssembly,
		// Token: 0x04000A62 RID: 2658
		CreateUser = 131,
		// Token: 0x04000A63 RID: 2659
		AlterUser,
		// Token: 0x04000A64 RID: 2660
		DropUser,
		// Token: 0x04000A65 RID: 2661
		CreateRole,
		// Token: 0x04000A66 RID: 2662
		AlterRole,
		// Token: 0x04000A67 RID: 2663
		DropRole,
		// Token: 0x04000A68 RID: 2664
		CreateAppRole,
		// Token: 0x04000A69 RID: 2665
		AlterAppRole,
		// Token: 0x04000A6A RID: 2666
		DropAppRole,
		// Token: 0x04000A6B RID: 2667
		CreateSchema = 141,
		// Token: 0x04000A6C RID: 2668
		AlterSchema,
		// Token: 0x04000A6D RID: 2669
		DropSchema,
		// Token: 0x04000A6E RID: 2670
		CreateLogin,
		// Token: 0x04000A6F RID: 2671
		AlterLogin,
		// Token: 0x04000A70 RID: 2672
		DropLogin,
		// Token: 0x04000A71 RID: 2673
		CreateMsgType = 151,
		// Token: 0x04000A72 RID: 2674
		DropMsgType = 153,
		// Token: 0x04000A73 RID: 2675
		CreateContract,
		// Token: 0x04000A74 RID: 2676
		DropContract = 156,
		// Token: 0x04000A75 RID: 2677
		CreateQueue,
		// Token: 0x04000A76 RID: 2678
		AlterQueue,
		// Token: 0x04000A77 RID: 2679
		DropQueue,
		// Token: 0x04000A78 RID: 2680
		CreateService = 161,
		// Token: 0x04000A79 RID: 2681
		AlterService,
		// Token: 0x04000A7A RID: 2682
		DropService,
		// Token: 0x04000A7B RID: 2683
		CreateRoute,
		// Token: 0x04000A7C RID: 2684
		AlterRoute,
		// Token: 0x04000A7D RID: 2685
		DropRoute,
		// Token: 0x04000A7E RID: 2686
		GrantStatement,
		// Token: 0x04000A7F RID: 2687
		DenyStatement,
		// Token: 0x04000A80 RID: 2688
		RevokeStatement,
		// Token: 0x04000A81 RID: 2689
		GrantObject,
		// Token: 0x04000A82 RID: 2690
		DenyObject,
		// Token: 0x04000A83 RID: 2691
		RevokeObject,
		// Token: 0x04000A84 RID: 2692
		CreateBinding = 174,
		// Token: 0x04000A85 RID: 2693
		AlterBinding,
		// Token: 0x04000A86 RID: 2694
		DropBinding,
		// Token: 0x04000A87 RID: 2695
		CreatePartitionFunction = 191,
		// Token: 0x04000A88 RID: 2696
		AlterPartitionFunction,
		// Token: 0x04000A89 RID: 2697
		DropPartitionFunction,
		// Token: 0x04000A8A RID: 2698
		CreatePartitionScheme,
		// Token: 0x04000A8B RID: 2699
		AlterPartitionScheme,
		// Token: 0x04000A8C RID: 2700
		DropPartitionScheme
	}
}
