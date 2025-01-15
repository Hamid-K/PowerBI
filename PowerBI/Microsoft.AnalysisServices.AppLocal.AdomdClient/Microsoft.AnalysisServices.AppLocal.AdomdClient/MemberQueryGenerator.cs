using System;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000AD RID: 173
	internal class MemberQueryGenerator
	{
		// Token: 0x060009E8 RID: 2536 RVA: 0x00029F34 File Offset: 0x00028134
		private MemberQueryGenerator()
		{
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00029F3C File Offset: 0x0002813C
		public static string GetMemberQuery(string memberSet, string dimensionPropertiesClause, CubeDef cube)
		{
			string text = AdomdUtils.Enquote(cube.Name, "[", "]");
			return string.Format(CultureInfo.InvariantCulture, "SELECT {0} {1} ON 0, {{}} ON 1 FROM {2}", memberSet, dimensionPropertiesClause, text);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00029F74 File Offset: 0x00028174
		public static string GetDimensionPropertiesClause(string[] userSuppliedProperties)
		{
			string text = "DIMENSION PROPERTIES MEMBER_NAME, MEMBER_TYPE";
			if (userSuppliedProperties.Length != 0)
			{
				text = text + ",  " + string.Join(", ", userSuppliedProperties);
			}
			return text;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00029FA3 File Offset: 0x000281A3
		public static string GetBaseSetMemberChilden(Member parentMember)
		{
			return string.Format(CultureInfo.InvariantCulture, "ADDCALCULATEDMEMBERS( {0}.CHILDREN )", parentMember.UniqueName);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00029FBA File Offset: 0x000281BA
		public static string GetBaseSetLevelMembers(Level level)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}.ALLMEMBERS", level.UniqueName);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00029FD4 File Offset: 0x000281D4
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

		// Token: 0x060009EE RID: 2542 RVA: 0x0002A00E File Offset: 0x0002820E
		private static string GetSetWithRange(string filteredSet, long start, long count)
		{
			return string.Format(CultureInfo.InvariantCulture, "SUBSET( {0}, {1}, {2} )", filteredSet, start, count);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0002A02C File Offset: 0x0002822C
		private static string GetSetWithFilter(string baseSet, string filterExpression)
		{
			return string.Format(CultureInfo.InvariantCulture, "FILTER( {0}, {1} )", baseSet, filterExpression);
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0002A040 File Offset: 0x00028240
		private static string GetFilterExpression(string hierarchyUniqueName, MemberFilter[] filters)
		{
			string[] array = new string[filters.Length];
			for (int i = 0; i < filters.Length; i++)
			{
				array[i] = "( " + MemberQueryGenerator.GetIndividualFilter(hierarchyUniqueName, filters[i]) + " )";
			}
			return string.Join(" AND ", array);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0002A08C File Offset: 0x0002828C
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

		// Token: 0x060009F2 RID: 2546 RVA: 0x0002A114 File Offset: 0x00028314
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

		// Token: 0x060009F3 RID: 2547 RVA: 0x0002A169 File Offset: 0x00028369
		private static string GetEqualFilter(string propertyReference, string propertyValue)
		{
			propertyValue = MemberQueryGenerator.EnquoteMdxString(propertyValue);
			return string.Format(CultureInfo.InvariantCulture, "{0} = {1}", propertyReference, propertyValue);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0002A184 File Offset: 0x00028384
		private static string GetBeginsWithFilter(string propertyReference, string propertyValue)
		{
			int length = propertyValue.Length;
			propertyValue = MemberQueryGenerator.EnquoteMdxString(propertyValue);
			return string.Format(CultureInfo.InvariantCulture, "LEFT( {0}, {1} ) = {2}", propertyReference, length, propertyValue);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002A1B8 File Offset: 0x000283B8
		private static string GetEndsWithFilter(string propertyReference, string propertyValue)
		{
			int length = propertyValue.Length;
			propertyValue = MemberQueryGenerator.EnquoteMdxString(propertyValue);
			return string.Format(CultureInfo.InvariantCulture, "RIGHT( {0}, {1} ) = {2}", propertyReference, length, propertyValue);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0002A1EB File Offset: 0x000283EB
		private static string GetContainsFilter(string propertyReference, string propertyValue)
		{
			propertyValue = MemberQueryGenerator.EnquoteMdxString(propertyValue);
			return string.Format(CultureInfo.InvariantCulture, "INSTR( {0}, {1} ) > 0", propertyReference, propertyValue);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0002A206 File Offset: 0x00028406
		private static string EnquoteMdxString(string stringValue)
		{
			return AdomdUtils.Enquote(stringValue, "\"", "\"");
		}

		// Token: 0x04000682 RID: 1666
		internal const int InternallyAddedDimensionPropertyCount = 2;

		// Token: 0x04000683 RID: 1667
		private const string mdxListSeperator = ", ";

		// Token: 0x04000684 RID: 1668
		private const string mdxStringOpenQuote = "\"";

		// Token: 0x04000685 RID: 1669
		private const string mdxStringCloseQuote = "\"";

		// Token: 0x04000686 RID: 1670
		private const string mdxIdentifyOpenQuote = "[";

		// Token: 0x04000687 RID: 1671
		private const string mdxIdentifyCloseQuote = "]";

		// Token: 0x04000688 RID: 1672
		private const string mdxSpace = " ";
	}
}
