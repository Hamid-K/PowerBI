using System;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000AD RID: 173
	internal class MemberQueryGenerator
	{
		// Token: 0x060009DB RID: 2523 RVA: 0x00029C04 File Offset: 0x00027E04
		private MemberQueryGenerator()
		{
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00029C0C File Offset: 0x00027E0C
		public static string GetMemberQuery(string memberSet, string dimensionPropertiesClause, CubeDef cube)
		{
			string text = AdomdUtils.Enquote(cube.Name, "[", "]");
			return string.Format(CultureInfo.InvariantCulture, "SELECT {0} {1} ON 0, {{}} ON 1 FROM {2}", memberSet, dimensionPropertiesClause, text);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00029C44 File Offset: 0x00027E44
		public static string GetDimensionPropertiesClause(string[] userSuppliedProperties)
		{
			string text = "DIMENSION PROPERTIES MEMBER_NAME, MEMBER_TYPE";
			if (userSuppliedProperties.Length != 0)
			{
				text = text + ",  " + string.Join(", ", userSuppliedProperties);
			}
			return text;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00029C73 File Offset: 0x00027E73
		public static string GetBaseSetMemberChilden(Member parentMember)
		{
			return string.Format(CultureInfo.InvariantCulture, "ADDCALCULATEDMEMBERS( {0}.CHILDREN )", parentMember.UniqueName);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00029C8A File Offset: 0x00027E8A
		public static string GetBaseSetLevelMembers(Level level)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}.ALLMEMBERS", level.UniqueName);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00029CA4 File Offset: 0x00027EA4
		public static string GetFilteredAndRangedMemberSet(string baseSet, string hierarchyUniqueName, long start, long count, MemberFilter[] filters)
		{
			string text;
			if (filters.Length != 0)
			{
				string filterExpression = MemberQueryGenerator.GetFilterExpression(hierarchyUniqueName, filters);
				text = MemberQueryGenerator.GetSetWithFilter(baseSet, filterExpression);
			}
			else
			{
				text = baseSet;
			}
			string text2;
			if (count > -1L)
			{
				text2 = MemberQueryGenerator.GetSetWithRange(text, start, count);
			}
			else
			{
				text2 = text;
			}
			return text2;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00029CDE File Offset: 0x00027EDE
		private static string GetSetWithRange(string filteredSet, long start, long count)
		{
			return string.Format(CultureInfo.InvariantCulture, "SUBSET( {0}, {1}, {2} )", filteredSet, start, count);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00029CFC File Offset: 0x00027EFC
		private static string GetSetWithFilter(string baseSet, string filterExpression)
		{
			return string.Format(CultureInfo.InvariantCulture, "FILTER( {0}, {1} )", baseSet, filterExpression);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00029D10 File Offset: 0x00027F10
		private static string GetFilterExpression(string hierarchyUniqueName, MemberFilter[] filters)
		{
			string[] array = new string[filters.Length];
			for (int i = 0; i < filters.Length; i++)
			{
				array[i] = "( " + MemberQueryGenerator.GetIndividualFilter(hierarchyUniqueName, filters[i]) + " )";
			}
			return string.Join(" AND ", array);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00029D5C File Offset: 0x00027F5C
		private static string GetIndividualFilter(string hierarchyUniqueName, MemberFilter filter)
		{
			string propertyReference = MemberQueryGenerator.GetPropertyReference(hierarchyUniqueName, filter.PropertyName);
			string text;
			switch (filter.FilterType)
			{
			case MemberFilterType.Equals:
				text = MemberQueryGenerator.GetEqualFilter(propertyReference, filter.PropertyValue);
				break;
			case MemberFilterType.BeginsWith:
				text = MemberQueryGenerator.GetBeginsWithFilter(propertyReference, filter.PropertyValue);
				break;
			case MemberFilterType.EndsWith:
				text = MemberQueryGenerator.GetEndsWithFilter(propertyReference, filter.PropertyValue);
				break;
			case MemberFilterType.Contains:
				text = MemberQueryGenerator.GetContainsFilter(propertyReference, filter.PropertyValue);
				break;
			default:
				text = MemberQueryGenerator.GetEqualFilter(propertyReference, filter.PropertyValue);
				break;
			}
			return text;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00029DE4 File Offset: 0x00027FE4
		private static string GetPropertyReference(string hierarchyUniqueName, string propertyName)
		{
			string text;
			if (propertyName == "Name" || propertyName == "UniqueName")
			{
				text = propertyName;
			}
			else
			{
				propertyName = MemberQueryGenerator.EnquoteMdxString(propertyName);
				text = string.Format(CultureInfo.InvariantCulture, "Properties({0})", propertyName);
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}.CurrentMember.{1}", hierarchyUniqueName, text);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00029E39 File Offset: 0x00028039
		private static string GetEqualFilter(string propertyReference, string propertyValue)
		{
			propertyValue = MemberQueryGenerator.EnquoteMdxString(propertyValue);
			return string.Format(CultureInfo.InvariantCulture, "{0} = {1}", propertyReference, propertyValue);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00029E54 File Offset: 0x00028054
		private static string GetBeginsWithFilter(string propertyReference, string propertyValue)
		{
			int length = propertyValue.Length;
			propertyValue = MemberQueryGenerator.EnquoteMdxString(propertyValue);
			return string.Format(CultureInfo.InvariantCulture, "LEFT( {0}, {1} ) = {2}", propertyReference, length, propertyValue);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00029E88 File Offset: 0x00028088
		private static string GetEndsWithFilter(string propertyReference, string propertyValue)
		{
			int length = propertyValue.Length;
			propertyValue = MemberQueryGenerator.EnquoteMdxString(propertyValue);
			return string.Format(CultureInfo.InvariantCulture, "RIGHT( {0}, {1} ) = {2}", propertyReference, length, propertyValue);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00029EBB File Offset: 0x000280BB
		private static string GetContainsFilter(string propertyReference, string propertyValue)
		{
			propertyValue = MemberQueryGenerator.EnquoteMdxString(propertyValue);
			return string.Format(CultureInfo.InvariantCulture, "INSTR( {0}, {1} ) > 0", propertyReference, propertyValue);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00029ED6 File Offset: 0x000280D6
		private static string EnquoteMdxString(string stringValue)
		{
			return AdomdUtils.Enquote(stringValue, "\"", "\"");
		}

		// Token: 0x04000675 RID: 1653
		internal const int InternallyAddedDimensionPropertyCount = 2;

		// Token: 0x04000676 RID: 1654
		private const string mdxListSeperator = ", ";

		// Token: 0x04000677 RID: 1655
		private const string mdxStringOpenQuote = "\"";

		// Token: 0x04000678 RID: 1656
		private const string mdxStringCloseQuote = "\"";

		// Token: 0x04000679 RID: 1657
		private const string mdxIdentifyOpenQuote = "[";

		// Token: 0x0400067A RID: 1658
		private const string mdxIdentifyCloseQuote = "]";

		// Token: 0x0400067B RID: 1659
		private const string mdxSpace = " ";
	}
}
