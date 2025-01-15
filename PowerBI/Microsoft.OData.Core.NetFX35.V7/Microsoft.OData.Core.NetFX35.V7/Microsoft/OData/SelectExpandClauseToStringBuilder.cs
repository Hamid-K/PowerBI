using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.UriParser;

namespace Microsoft.OData
{
	// Token: 0x020000D7 RID: 215
	internal sealed class SelectExpandClauseToStringBuilder : SelectItemTranslator<string>
	{
		// Token: 0x0600084A RID: 2122 RVA: 0x0001794C File Offset: 0x00015B4C
		public string TranslateSelectExpandClause(SelectExpandClause selectExpandClause, bool firstFlag)
		{
			ExceptionUtils.CheckArgumentNotNull<SelectExpandClause>(selectExpandClause, "selectExpandClause");
			List<string> currentLevelSelectList = selectExpandClause.GetCurrentLevelSelectList();
			string text = null;
			if (Enumerable.Any<string>(currentLevelSelectList))
			{
				text = string.Join(",", currentLevelSelectList.ToArray());
			}
			text = (string.IsNullOrEmpty(text) ? null : ("$select" + "=" + (this.isFirstSelectItem ? Uri.EscapeDataString(text) : text)));
			this.isFirstSelectItem = false;
			string text2 = null;
			foreach (SelectItem selectItem in Enumerable.Where<SelectItem>(selectExpandClause.SelectedItems, (SelectItem I) => I.GetType() == typeof(ExpandedNavigationSelectItem)))
			{
				ExpandedNavigationSelectItem expandedNavigationSelectItem = (ExpandedNavigationSelectItem)selectItem;
				if (string.IsNullOrEmpty(text2))
				{
					text2 = (firstFlag ? text2 : ("$expand" + "="));
				}
				else
				{
					text2 += ",";
				}
				text2 += this.Translate(expandedNavigationSelectItem);
			}
			foreach (SelectItem selectItem2 in Enumerable.Where<SelectItem>(selectExpandClause.SelectedItems, (SelectItem I) => I.GetType() == typeof(ExpandedReferenceSelectItem)))
			{
				ExpandedReferenceSelectItem expandedReferenceSelectItem = (ExpandedReferenceSelectItem)selectItem2;
				if (string.IsNullOrEmpty(text2))
				{
					text2 = (firstFlag ? text2 : ("$expand" + "="));
				}
				else
				{
					text2 += ",";
				}
				text2 = text2 + this.Translate(expandedReferenceSelectItem) + "/$ref";
			}
			if (string.IsNullOrEmpty(text2))
			{
				return text;
			}
			if (firstFlag)
			{
				if (!string.IsNullOrEmpty(text))
				{
					return text + "&$expand=" + Uri.EscapeDataString(text2);
				}
				return "$expand=" + Uri.EscapeDataString(text2);
			}
			else
			{
				if (!string.IsNullOrEmpty(text))
				{
					return text + (";" + text2);
				}
				return text2;
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00017B54 File Offset: 0x00015D54
		public override string Translate(WildcardSelectItem item)
		{
			return string.Empty;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00017B54 File Offset: 0x00015D54
		public override string Translate(PathSelectItem item)
		{
			return string.Empty;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00017B5B File Offset: 0x00015D5B
		public override string Translate(NamespaceQualifiedWildcardSelectItem item)
		{
			return item.Namespace;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00017B64 File Offset: 0x00015D64
		public override string Translate(ExpandedNavigationSelectItem item)
		{
			string text = string.Empty;
			if (item.SelectAndExpand != null)
			{
				string text2 = this.TranslateSelectExpandClause(item.SelectAndExpand, false);
				text = text + ((!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2)) ? ";" : null) + text2;
			}
			if (item.LevelsOption != null)
			{
				text += (string.IsNullOrEmpty(text) ? null : ";");
				text += "$levels";
				text += "=";
				text += NodeToStringBuilder.TranslateLevelsClause(item.LevelsOption);
			}
			string text3 = this.Translate(item);
			if (text3.EndsWith(")", 4))
			{
				return text3.Insert(text3.Length - 1, string.IsNullOrEmpty(text) ? string.Empty : (";" + text));
			}
			return text3 + (string.IsNullOrEmpty(text) ? null : ("(" + text + ")"));
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00017C58 File Offset: 0x00015E58
		public override string Translate(ExpandedReferenceSelectItem item)
		{
			NodeToStringBuilder nodeToStringBuilder = new NodeToStringBuilder();
			string text = string.Join("/", Enumerable.ToArray<string>(item.PathToNavigationProperty.WalkWith<string>(PathSegmentToStringTranslator.Instance)));
			string text2 = string.Empty;
			if (item.FilterOption != null)
			{
				text2 = text2 + "$filter=" + nodeToStringBuilder.TranslateFilterClause(item.FilterOption);
			}
			if (item.OrderByOption != null)
			{
				text2 += (string.IsNullOrEmpty(text2) ? null : ";");
				text2 = text2 + "$orderby=" + nodeToStringBuilder.TranslateOrderByClause(item.OrderByOption);
			}
			if (item.TopOption != null)
			{
				text2 += (string.IsNullOrEmpty(text2) ? null : ";");
				text2 = text2 + "$top=" + item.TopOption.ToString();
			}
			if (item.SkipOption != null)
			{
				text2 += (string.IsNullOrEmpty(text2) ? null : ";");
				text2 = text2 + "$skip=" + item.SkipOption.ToString();
			}
			if (item.CountOption != null)
			{
				text2 += (string.IsNullOrEmpty(text2) ? null : ";");
				text2 += "$count";
				text2 += "=";
				if (item.CountOption == true)
				{
					text2 += "true";
				}
				else
				{
					text2 += "false";
				}
			}
			if (item.SearchOption != null)
			{
				text2 += (string.IsNullOrEmpty(text2) ? null : ";");
				text2 += "$search";
				text2 += "=";
				text2 += nodeToStringBuilder.TranslateSearchClause(item.SearchOption);
			}
			return text + (string.IsNullOrEmpty(text2) ? null : ("(" + text2 + ")"));
		}

		// Token: 0x04000393 RID: 915
		private bool isFirstSelectItem = true;
	}
}
