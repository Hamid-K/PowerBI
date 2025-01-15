using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriBuilder
{
	// Token: 0x020001C1 RID: 449
	internal sealed class SelectExpandClauseToStringBuilder : SelectItemTranslator<string>
	{
		// Token: 0x060010D5 RID: 4309 RVA: 0x0003A7A4 File Offset: 0x000389A4
		[SuppressMessage("DataWeb.Usage", "AC0018:SystemUriEscapeDataStringRule", Justification = "Values passed to this method are model elements like property names or keywords.")]
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

		// Token: 0x060010D6 RID: 4310 RVA: 0x0003A9A8 File Offset: 0x00038BA8
		public override string Translate(WildcardSelectItem item)
		{
			return string.Empty;
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0003A9AF File Offset: 0x00038BAF
		public override string Translate(PathSelectItem item)
		{
			return string.Empty;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0003A9B6 File Offset: 0x00038BB6
		public override string Translate(NamespaceQualifiedWildcardSelectItem item)
		{
			return item.Namespace;
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0003A9C0 File Offset: 0x00038BC0
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

		// Token: 0x060010DA RID: 4314 RVA: 0x0003AAB4 File Offset: 0x00038CB4
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

		// Token: 0x04000771 RID: 1905
		private bool isFirstSelectItem = true;
	}
}
