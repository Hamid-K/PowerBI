using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200025E RID: 606
	internal static class SelectExpandClauseExtensions
	{
		// Token: 0x06001559 RID: 5465 RVA: 0x0004B2C8 File Offset: 0x000494C8
		internal static void GetSubSelectExpandClause(this SelectExpandClause clause, string propertyName, out SelectExpandClause subSelectExpand, out TypeSegment typeSegment)
		{
			subSelectExpand = null;
			typeSegment = null;
			ExpandedNavigationSelectItem expandedNavigationSelectItem = Enumerable.FirstOrDefault<ExpandedNavigationSelectItem>(Enumerable.OfType<ExpandedNavigationSelectItem>(clause.SelectedItems), (ExpandedNavigationSelectItem m) => m.PathToNavigationProperty.LastSegment != null && m.PathToNavigationProperty.LastSegment.TranslateWith<string>(PathSegmentToStringTranslator.Instance) == propertyName);
			if (expandedNavigationSelectItem != null)
			{
				subSelectExpand = expandedNavigationSelectItem.SelectAndExpand;
				typeSegment = expandedNavigationSelectItem.PathToNavigationProperty.FirstSegment as TypeSegment;
			}
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0004B324 File Offset: 0x00049524
		internal static void GetSelectExpandPaths(this SelectExpandClause selectExpandClause, out string selectClause, out string expandClause)
		{
			StringBuilder stringBuilder;
			StringBuilder stringBuilder2;
			selectExpandClause.GetSelectExpandPaths(out stringBuilder, out stringBuilder2);
			selectClause = stringBuilder.ToString();
			expandClause = stringBuilder2.ToString();
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0004B34B File Offset: 0x0004954B
		internal static void GetSelectExpandPaths(this SelectExpandClause selectExpandClause, out StringBuilder selectClause, out StringBuilder expandClause)
		{
			selectClause = new StringBuilder();
			expandClause = new StringBuilder();
			selectClause.Append(SelectExpandClauseExtensions.BuildTopLevelSelect(selectExpandClause));
			expandClause.Append(SelectExpandClauseExtensions.BuildExpandsForNode(selectExpandClause));
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0004B380 File Offset: 0x00049580
		internal static List<string> GetCurrentLevelSelectList(this SelectExpandClause selectExpandClause)
		{
			return Enumerable.ToList<string>(Enumerable.Where<string>(Enumerable.Select<SelectItem, string>(selectExpandClause.SelectedItems, new Func<SelectItem, string>(SelectExpandClauseExtensions.GetSelectString)), (string i) => i != null));
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0004B3E8 File Offset: 0x000495E8
		internal static void Traverse<T>(this SelectExpandClause selectExpandClause, Func<string, T, T> processSubResult, Func<IList<string>, IList<T>, T> combineSelectAndExpand, out T result)
		{
			List<string> currentLevelSelectList = selectExpandClause.GetCurrentLevelSelectList();
			List<T> list = new List<T>();
			foreach (SelectItem selectItem in Enumerable.Where<SelectItem>(selectExpandClause.SelectedItems, (SelectItem I) => I.GetType() == typeof(ExpandedNavigationSelectItem)))
			{
				ExpandedNavigationSelectItem expandedNavigationSelectItem = (ExpandedNavigationSelectItem)selectItem;
				string text = string.Join("/", Enumerable.ToArray<string>(expandedNavigationSelectItem.PathToNavigationProperty.WalkWith<string>(PathSegmentToStringTranslator.Instance)));
				T t = default(T);
				if (Enumerable.Any<SelectItem>(expandedNavigationSelectItem.SelectAndExpand.SelectedItems))
				{
					expandedNavigationSelectItem.SelectAndExpand.Traverse(processSubResult, combineSelectAndExpand, out t);
				}
				T t2 = processSubResult.Invoke(text, t);
				if (t2 != null)
				{
					list.Add(t2);
				}
			}
			foreach (SelectItem selectItem2 in Enumerable.Where<SelectItem>(selectExpandClause.SelectedItems, (SelectItem I) => I.GetType() == typeof(ExpandedReferenceSelectItem)))
			{
				ExpandedReferenceSelectItem expandedReferenceSelectItem = (ExpandedReferenceSelectItem)selectItem2;
				string text2 = string.Join("/", Enumerable.ToArray<string>(expandedReferenceSelectItem.PathToNavigationProperty.WalkWith<string>(PathSegmentToStringTranslator.Instance)));
				text2 += "/$ref";
				T t3 = processSubResult.Invoke(text2, default(T));
				if (t3 != null)
				{
					list.Add(t3);
				}
			}
			result = combineSelectAndExpand.Invoke(currentLevelSelectList, list);
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x0004B570 File Offset: 0x00049770
		private static string BuildTopLevelSelect(SelectExpandClause selectExpandClause)
		{
			return string.Join(",", selectExpandClause.GetCurrentLevelSelectList().ToArray());
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x0004B588 File Offset: 0x00049788
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
				return string.Join("/", Enumerable.ToArray<string>(pathSelectItem.SelectedPath.WalkWith<string>(PathSegmentToStringTranslator.Instance)));
			}
			return null;
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0004B614 File Offset: 0x00049814
		private static string BuildExpandsForNode(SelectExpandClause selectExpandClause)
		{
			List<string> list = new List<string>();
			foreach (SelectItem selectItem in Enumerable.Where<SelectItem>(selectExpandClause.SelectedItems, (SelectItem I) => I.GetType() == typeof(ExpandedNavigationSelectItem)))
			{
				ExpandedNavigationSelectItem expandedNavigationSelectItem = (ExpandedNavigationSelectItem)selectItem;
				string text = string.Join("/", Enumerable.ToArray<string>(expandedNavigationSelectItem.PathToNavigationProperty.WalkWith<string>(PathSegmentToStringTranslator.Instance)));
				string text2;
				expandedNavigationSelectItem.SelectAndExpand.Traverse(new Func<string, string, string>(SelectExpandClauseExtensions.ProcessSubExpand), new Func<IList<string>, IList<string>, string>(SelectExpandClauseExtensions.CombineSelectAndExpandResult), out text2);
				if (!string.IsNullOrEmpty(text2))
				{
					text = text + "(" + text2 + ")";
				}
				list.Add(text);
			}
			foreach (SelectItem selectItem2 in Enumerable.Where<SelectItem>(selectExpandClause.SelectedItems, (SelectItem I) => I.GetType() == typeof(ExpandedReferenceSelectItem)))
			{
				ExpandedReferenceSelectItem expandedReferenceSelectItem = (ExpandedReferenceSelectItem)selectItem2;
				string text3 = string.Join("/", Enumerable.ToArray<string>(expandedReferenceSelectItem.PathToNavigationProperty.WalkWith<string>(PathSegmentToStringTranslator.Instance)));
				text3 += "/$ref";
				list.Add(text3);
			}
			return string.Join(",", list.ToArray());
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x0004B79C File Offset: 0x0004999C
		private static string ProcessSubExpand(string expandNode, string subExpand)
		{
			if (!string.IsNullOrEmpty(subExpand))
			{
				return expandNode + "(" + subExpand + ")";
			}
			return expandNode;
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0004B7BC File Offset: 0x000499BC
		private static string CombineSelectAndExpandResult(IList<string> selectList, IList<string> expandList)
		{
			string text = "";
			if (Enumerable.Any<string>(selectList))
			{
				text = text + "$select=" + string.Join(",", Enumerable.ToArray<string>(selectList));
			}
			if (Enumerable.Any<string>(expandList))
			{
				if (!string.IsNullOrEmpty(text))
				{
					text += ";";
				}
				text = text + "$expand=" + string.Join(",", Enumerable.ToArray<string>(expandList));
			}
			return text;
		}
	}
}
