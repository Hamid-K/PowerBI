using System;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x02000101 RID: 257
	internal static class QdmNames
	{
		// Token: 0x06000EBE RID: 3774 RVA: 0x00027D5C File Offset: 0x00025F5C
		internal static string AddMissingItems(string name)
		{
			return "AddMissingItems" + name;
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x00027D69 File Offset: 0x00025F69
		internal static string AddVisualCalculations(string name)
		{
			return "AddVisualCalculations" + name;
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00027D76 File Offset: 0x00025F76
		internal static string All(string name = null)
		{
			return "All" + name;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00027D83 File Offset: 0x00025F83
		internal static string Scan(string name = null)
		{
			return "Scan" + name;
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00027D90 File Offset: 0x00025F90
		internal static string Apply(string name = null)
		{
			return "Apply" + name;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x00027D9D File Offset: 0x00025F9D
		internal static string Calculate(string name)
		{
			return "Calculate" + name;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00027DAA File Offset: 0x00025FAA
		internal static string CalculateTable(string name = null)
		{
			return "CalculateTable" + name;
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00027DB7 File Offset: 0x00025FB7
		internal static string Cloned(string name)
		{
			return "Cloned" + name;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00027DC4 File Offset: 0x00025FC4
		internal static string CrossJoin(string name)
		{
			return "CrossJoin" + name;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x00027DD1 File Offset: 0x00025FD1
		internal static string FullOuterCrossJoin(string name)
		{
			return "FullOuterCrossJoin" + name;
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00027DDE File Offset: 0x00025FDE
		internal static string CountOf(string name)
		{
			return "Count" + name;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00027DEB File Offset: 0x00025FEB
		internal static string CurrentGroup(string name)
		{
			return "CurrentGroup" + name;
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00027DF8 File Offset: 0x00025FF8
		internal static string Distinct(string name)
		{
			return "Distinct" + name;
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00027E05 File Offset: 0x00026005
		internal static string Filtered(string name)
		{
			return "Filtered" + name;
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x00027E12 File Offset: 0x00026012
		internal static string GenerateAll(string name = null)
		{
			return "GenerateAll" + name;
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00027E1F File Offset: 0x0002601F
		internal static string Grouped(string name = null)
		{
			return "Grouped" + name;
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x00027E2C File Offset: 0x0002602C
		internal static string GroupedAndJoined(string name = null)
		{
			return "GroupedAndJoined" + name;
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x00027E39 File Offset: 0x00026039
		internal static string NonEmpty(string name)
		{
			return "NonEmpty" + name;
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x00027E46 File Offset: 0x00026046
		internal static string Projected(string name = null)
		{
			return "Projected" + name;
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x00027E53 File Offset: 0x00026053
		internal static string Sorted(string name)
		{
			return "Sorted" + name;
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00027E60 File Offset: 0x00026060
		internal static string Join(string name = null)
		{
			return "Join" + name;
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00027E6D File Offset: 0x0002606D
		internal static string Limit(string name)
		{
			return "Limit" + name;
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00027E7A File Offset: 0x0002607A
		internal static string SubstituteWithIndex(string name)
		{
			return "SubstituteWithIndex" + name;
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00027E87 File Offset: 0x00026087
		internal static string TableDeclaration(string name)
		{
			return "TableDeclaration" + name;
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00027E94 File Offset: 0x00026094
		internal static string TreatAs(string name = null)
		{
			return "TreatAs" + name;
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00027EA1 File Offset: 0x000260A1
		internal static string TupleFiltered()
		{
			return "TupleFiltered";
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x00027EA8 File Offset: 0x000260A8
		internal static string Union(string name)
		{
			return "Union" + name;
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00027EB5 File Offset: 0x000260B5
		internal static string UniqueUnqualifiedNames(string name)
		{
			return "UniqueUnqualifiedNames" + name;
		}

		// Token: 0x040009E0 RID: 2528
		internal const string DefaultJoinPredicateFieldName = "JoinPredicate";

		// Token: 0x040009E1 RID: 2529
		internal const string GroupAndJoin = "GroupAndJoin";

		// Token: 0x040009E2 RID: 2530
		internal const string Summarize = "Summarize";

		// Token: 0x040009E3 RID: 2531
		internal const string Row = "Row";

		// Token: 0x040009E4 RID: 2532
		internal const string TableScan = "TableScan";

		// Token: 0x040009E5 RID: 2533
		internal const string ExtensionFunction = "ExtensionFunction";

		// Token: 0x040009E6 RID: 2534
		internal const string DataTable = "DataTable";
	}
}
