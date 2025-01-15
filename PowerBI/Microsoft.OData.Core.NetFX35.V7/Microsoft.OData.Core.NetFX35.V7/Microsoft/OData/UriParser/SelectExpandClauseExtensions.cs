using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000159 RID: 345
	internal static class SelectExpandClauseExtensions
	{
		// Token: 0x06000EF2 RID: 3826 RVA: 0x0002AFC0 File Offset: 0x000291C0
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

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0002B01C File Offset: 0x0002921C
		internal static void GetSelectExpandPaths(this SelectExpandClause selectExpandClause, out string selectClause, out string expandClause)
		{
			StringBuilder stringBuilder;
			StringBuilder stringBuilder2;
			selectExpandClause.GetSelectExpandPaths(out stringBuilder, out stringBuilder2);
			selectClause = stringBuilder.ToString();
			expandClause = stringBuilder2.ToString();
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0002B043 File Offset: 0x00029243
		internal static void GetSelectExpandPaths(this SelectExpandClause selectExpandClause, out StringBuilder selectClause, out StringBuilder expandClause)
		{
			selectClause = new StringBuilder();
			expandClause = new StringBuilder();
			selectClause.Append(SelectExpandClauseExtensions.BuildTopLevelSelect(selectExpandClause));
			expandClause.Append(SelectExpandClauseExtensions.BuildExpandsForNode(selectExpandClause));
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0002B070 File Offset: 0x00029270
		internal static List<string> GetCurrentLevelSelectList(this SelectExpandClause selectExpandClause)
		{
			return Enumerable.ToList<string>(Enumerable.Where<string>(Enumerable.Select<SelectItem, string>(selectExpandClause.SelectedItems, new Func<SelectItem, string>(SelectExpandClauseExtensions.GetSelectString)), (string i) => i != null));
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0002B0C0 File Offset: 0x000292C0
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

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0002B26C File Offset: 0x0002946C
		private static string BuildTopLevelSelect(SelectExpandClause selectExpandClause)
		{
			return string.Join(",", selectExpandClause.GetCurrentLevelSelectList().ToArray());
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0002B284 File Offset: 0x00029484
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

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0002B2E8 File Offset: 0x000294E8
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

		// Token: 0x06000EFA RID: 3834 RVA: 0x0002B474 File Offset: 0x00029674
		private static string ProcessSubExpand(string expandNode, string subExpand)
		{
			if (!string.IsNullOrEmpty(subExpand))
			{
				return expandNode + "(" + subExpand + ")";
			}
			return expandNode;
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0002B494 File Offset: 0x00029694
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
