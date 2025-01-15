using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.UriParser;

namespace Microsoft.OData
{
	// Token: 0x020000DA RID: 218
	internal sealed class SelectExpandClauseToStringBuilder : SelectItemTranslator<string>
	{
		// Token: 0x06000A34 RID: 2612 RVA: 0x0001ADE8 File Offset: 0x00018FE8
		public string TranslateSelectExpandClause(SelectExpandClause selectExpandClause, bool firstFlag)
		{
			ExceptionUtils.CheckArgumentNotNull<SelectExpandClause>(selectExpandClause, "selectExpandClause");
			List<string> currentLevelSelectList = selectExpandClause.GetCurrentLevelSelectList();
			string text = null;
			if (currentLevelSelectList.Any<string>())
			{
				text = string.Join(",", currentLevelSelectList.ToArray());
			}
			text = (string.IsNullOrEmpty(text) ? null : ("$select" + "=" + (this.isFirstSelectItem ? Uri.EscapeDataString(text) : text)));
			this.isFirstSelectItem = false;
			string text2 = null;
			foreach (SelectItem selectItem in selectExpandClause.SelectedItems.Where((SelectItem I) => I.GetType() == typeof(ExpandedNavigationSelectItem)))
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
			foreach (SelectItem selectItem2 in selectExpandClause.SelectedItems.Where((SelectItem I) => I.GetType() == typeof(ExpandedReferenceSelectItem)))
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

		// Token: 0x06000A35 RID: 2613 RVA: 0x0001AFF0 File Offset: 0x000191F0
		public override string Translate(WildcardSelectItem item)
		{
			return string.Empty;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0001AFF0 File Offset: 0x000191F0
		public override string Translate(PathSelectItem item)
		{
			return string.Empty;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0001AFF7 File Offset: 0x000191F7
		public override string Translate(NamespaceQualifiedWildcardSelectItem item)
		{
			return item.Namespace;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0001B000 File Offset: 0x00019200
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
			if (text3.EndsWith(")", StringComparison.Ordinal))
			{
				return text3.Insert(text3.Length - 1, string.IsNullOrEmpty(text) ? string.Empty : (";" + text));
			}
			return text3 + (string.IsNullOrEmpty(text) ? null : ("(" + text + ")"));
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0001B0F4 File Offset: 0x000192F4
		public override string Translate(ExpandedReferenceSelectItem item)
		{
			NodeToStringBuilder nodeToStringBuilder = new NodeToStringBuilder();
			string text = string.Join("/", item.PathToNavigationProperty.WalkWith<string>(PathSegmentToStringTranslator.Instance).ToArray<string>());
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
			if (item.ComputeOption != null)
			{
				text2 += (string.IsNullOrEmpty(text2) ? null : ";");
				text2 += "$compute";
				text2 += "=";
				text2 += nodeToStringBuilder.TranslateComputeClause(item.ComputeOption);
			}
			if (item.ApplyOption != null)
			{
				text2 += (string.IsNullOrEmpty(text2) ? null : ";");
				ApplyClauseToStringBuilder applyClauseToStringBuilder = new ApplyClauseToStringBuilder();
				text2 += applyClauseToStringBuilder.TranslateApplyClause(item.ApplyOption);
			}
			return text + (string.IsNullOrEmpty(text2) ? null : ("(" + text2 + ")"));
		}

		// Token: 0x040003C2 RID: 962
		private bool isFirstSelectItem = true;
	}
}
