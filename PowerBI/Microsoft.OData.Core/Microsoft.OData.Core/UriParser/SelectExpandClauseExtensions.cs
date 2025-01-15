using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001A5 RID: 421
	internal static class SelectExpandClauseExtensions
	{
		// Token: 0x06001417 RID: 5143 RVA: 0x0003AF08 File Offset: 0x00039108
		internal static void GetSubSelectExpandClause(this SelectExpandClause clause, string propertyName, out SelectExpandClause subSelectExpand, out TypeSegment typeSegment)
		{
			subSelectExpand = null;
			typeSegment = null;
			ExpandedNavigationSelectItem expandedNavigationSelectItem = clause.SelectedItems.OfType<ExpandedNavigationSelectItem>().FirstOrDefault((ExpandedNavigationSelectItem m) => m.PathToNavigationProperty.LastSegment != null && m.PathToNavigationProperty.LastSegment.TranslateWith<string>(PathSegmentToStringTranslator.Instance) == propertyName);
			if (expandedNavigationSelectItem != null)
			{
				subSelectExpand = expandedNavigationSelectItem.SelectAndExpand;
				typeSegment = expandedNavigationSelectItem.PathToNavigationProperty.FirstSegment as TypeSegment;
			}
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0003AF64 File Offset: 0x00039164
		internal static void GetSelectExpandPaths(this SelectExpandClause selectExpandClause, ODataVersion version, out string selectClause, out string expandClause)
		{
			StringBuilder stringBuilder;
			StringBuilder stringBuilder2;
			selectExpandClause.GetSelectExpandPaths(version, out stringBuilder, out stringBuilder2);
			selectClause = stringBuilder.ToString();
			expandClause = stringBuilder2.ToString();
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0003AF8C File Offset: 0x0003918C
		internal static void GetSelectExpandPaths(this SelectExpandClause selectExpandClause, ODataVersion version, out StringBuilder selectClause, out StringBuilder expandClause)
		{
			selectClause = new StringBuilder();
			expandClause = new StringBuilder();
			selectClause.Append(SelectExpandClauseExtensions.BuildTopLevelSelect(selectExpandClause));
			expandClause.Append(SelectExpandClauseExtensions.BuildExpandsForNode(selectExpandClause));
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0003AFB8 File Offset: 0x000391B8
		internal static List<string> GetCurrentLevelSelectList(this SelectExpandClause selectExpandClause)
		{
			return (from i in selectExpandClause.SelectedItems.Select(new Func<SelectItem, string>(SelectExpandClauseExtensions.GetSelectString))
				where i != null
				select i).ToList<string>();
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0003B008 File Offset: 0x00039208
		internal static void Traverse<T>(this SelectExpandClause selectExpandClause, Func<string, T, T> processSubResult, Func<IList<string>, IList<T>, T> combineSelectAndExpand, Func<ApplyClause, T> processApply, out T result)
		{
			List<string> currentLevelSelectList = selectExpandClause.GetCurrentLevelSelectList();
			List<T> list = new List<T>();
			foreach (SelectItem selectItem in selectExpandClause.SelectedItems.Where((SelectItem I) => I.GetType() == typeof(ExpandedNavigationSelectItem)))
			{
				ExpandedNavigationSelectItem expandedNavigationSelectItem = (ExpandedNavigationSelectItem)selectItem;
				string text = string.Join("/", expandedNavigationSelectItem.PathToNavigationProperty.WalkWith<string>(PathSegmentToStringTranslator.Instance).ToArray<string>());
				T t = default(T);
				if (expandedNavigationSelectItem.SelectAndExpand.SelectedItems.Any<SelectItem>())
				{
					expandedNavigationSelectItem.SelectAndExpand.Traverse(processSubResult, combineSelectAndExpand, processApply, out t);
				}
				if (expandedNavigationSelectItem.ApplyOption != null && processApply != null)
				{
					t = processApply(expandedNavigationSelectItem.ApplyOption);
				}
				T t2 = processSubResult(text, t);
				if (t2 != null)
				{
					list.Add(t2);
				}
			}
			foreach (SelectItem selectItem2 in selectExpandClause.SelectedItems.Where((SelectItem I) => I.GetType() == typeof(ExpandedReferenceSelectItem)))
			{
				ExpandedReferenceSelectItem expandedReferenceSelectItem = (ExpandedReferenceSelectItem)selectItem2;
				string text2 = string.Join("/", expandedReferenceSelectItem.PathToNavigationProperty.WalkWith<string>(PathSegmentToStringTranslator.Instance).ToArray<string>());
				text2 += "/$ref";
				T t3 = processSubResult(text2, default(T));
				if (t3 != null)
				{
					list.Add(t3);
				}
			}
			result = combineSelectAndExpand(currentLevelSelectList, list);
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0003B1D4 File Offset: 0x000393D4
		private static string BuildTopLevelSelect(SelectExpandClause selectExpandClause)
		{
			return string.Join(",", selectExpandClause.GetCurrentLevelSelectList().ToArray());
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0003B1EC File Offset: 0x000393EC
		private static string GetSelectString(SelectItem selectedItem)
		{
			WildcardSelectItem wildcardSelectItem = selectedItem as WildcardSelectItem;
			NamespaceQualifiedWildcardSelectItem namespaceQualifiedWildcardSelectItem = selectedItem as NamespaceQualifiedWildcardSelectItem;
			PathSelectItem pathSelectItem = selectedItem as PathSelectItem;
			if (wildcardSelectItem != null)
			{
				return "*";
			}
			if (namespaceQualifiedWildcardSelectItem != null)
			{
				return namespaceQualifiedWildcardSelectItem.Namespace + ".*";
			}
			if (pathSelectItem != null)
			{
				return string.Join("/", pathSelectItem.SelectedPath.WalkWith<string>(PathSegmentToStringTranslator.Instance).ToArray<string>());
			}
			return null;
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0003B250 File Offset: 0x00039450
		private static string BuildExpandsForNode(SelectExpandClause selectExpandClause)
		{
			List<string> list = new List<string>();
			foreach (SelectItem selectItem in selectExpandClause.SelectedItems.Where((SelectItem I) => I.GetType() == typeof(ExpandedNavigationSelectItem)))
			{
				ExpandedNavigationSelectItem expandedNavigationSelectItem = (ExpandedNavigationSelectItem)selectItem;
				string text = string.Join("/", expandedNavigationSelectItem.PathToNavigationProperty.WalkWith<string>(PathSegmentToStringTranslator.Instance).ToArray<string>());
				string text2;
				expandedNavigationSelectItem.SelectAndExpand.Traverse(new Func<string, string, string>(SelectExpandClauseExtensions.ProcessSubExpand), new Func<IList<string>, IList<string>, string>(SelectExpandClauseExtensions.CombineSelectAndExpandResult), null, out text2);
				if (!string.IsNullOrEmpty(text2))
				{
					text = text + "(" + text2 + ")";
				}
				list.Add(text);
			}
			foreach (SelectItem selectItem2 in selectExpandClause.SelectedItems.Where((SelectItem I) => I.GetType() == typeof(ExpandedReferenceSelectItem)))
			{
				ExpandedReferenceSelectItem expandedReferenceSelectItem = (ExpandedReferenceSelectItem)selectItem2;
				string text3 = string.Join("/", expandedReferenceSelectItem.PathToNavigationProperty.WalkWith<string>(PathSegmentToStringTranslator.Instance).ToArray<string>());
				text3 += "/$ref";
				list.Add(text3);
			}
			return string.Join(",", list.ToArray());
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0003B3DC File Offset: 0x000395DC
		private static string ProcessSubExpand(string expandNode, string subExpand)
		{
			if (!string.IsNullOrEmpty(subExpand))
			{
				return expandNode + "(" + subExpand + ")";
			}
			return expandNode;
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0003B3FC File Offset: 0x000395FC
		private static string CombineSelectAndExpandResult(IList<string> selectList, IList<string> expandList)
		{
			string text = "";
			if (selectList.Any<string>())
			{
				text = text + "$select=" + string.Join(",", selectList.ToArray<string>());
			}
			if (expandList.Any<string>())
			{
				if (!string.IsNullOrEmpty(text))
				{
					text += ";";
				}
				text = text + "$expand=" + string.Join(",", expandList.ToArray<string>());
			}
			return text;
		}
	}
}
