using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Edm
{
	// Token: 0x0200080C RID: 2060
	internal sealed class Capabilities
	{
		// Token: 0x06003B61 RID: 15201 RVA: 0x000C14F4 File Offset: 0x000BF6F4
		public Capabilities()
		{
			this.IsIndexableByKey = true;
			this.IsInsertable = true;
			this.IsDeletable = true;
			this.IsUpdatable = true;
			this.DisplayAnnotations = new List<NamedValue>();
			this.NonCountableProperties = new HashSet<string>();
			this.NonCountableNavigationProperties = new HashSet<string>();
			this.NonExpandableProperties = new HashSet<string>();
			this.AscendingOnlyProperties = new HashSet<string>();
			this.DescendingOnlyProperties = new HashSet<string>();
			this.NonSortableProperties = new HashSet<string>();
			this.FilterFunctions = new HashSet<string>();
			this.RequiredPropertiesInFilter = new HashSet<string>();
			this.NonFilterableProperties = new HashSet<string>();
			this.AggregateTransformations = new HashSet<string>();
			this.PropertyRestrictions = false;
			this.ApplySupported = false;
		}

		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x06003B62 RID: 15202 RVA: 0x000C15AA File Offset: 0x000BF7AA
		// (set) Token: 0x06003B63 RID: 15203 RVA: 0x000C15B2 File Offset: 0x000BF7B2
		public Uri ResourcePath { get; set; }

		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x06003B64 RID: 15204 RVA: 0x000C15BB File Offset: 0x000BF7BB
		// (set) Token: 0x06003B65 RID: 15205 RVA: 0x000C15C3 File Offset: 0x000BF7C3
		public bool PropertyRestrictions { get; set; }

		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x06003B66 RID: 15206 RVA: 0x000C15CC File Offset: 0x000BF7CC
		// (set) Token: 0x06003B67 RID: 15207 RVA: 0x000C15D4 File Offset: 0x000BF7D4
		public bool ApplySupported { get; set; }

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x06003B68 RID: 15208 RVA: 0x000C15DD File Offset: 0x000BF7DD
		// (set) Token: 0x06003B69 RID: 15209 RVA: 0x000C15E5 File Offset: 0x000BF7E5
		public HashSet<string> AggregateTransformations { get; set; }

		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x06003B6A RID: 15210 RVA: 0x000C15EE File Offset: 0x000BF7EE
		// (set) Token: 0x06003B6B RID: 15211 RVA: 0x000C15F6 File Offset: 0x000BF7F6
		public bool IsIndexableByKey { get; set; }

		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x06003B6C RID: 15212 RVA: 0x000C15FF File Offset: 0x000BF7FF
		// (set) Token: 0x06003B6D RID: 15213 RVA: 0x000C1607 File Offset: 0x000BF807
		public bool SupportsTop { get; set; }

		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x06003B6E RID: 15214 RVA: 0x000C1610 File Offset: 0x000BF810
		// (set) Token: 0x06003B6F RID: 15215 RVA: 0x000C1618 File Offset: 0x000BF818
		public bool SupportsSkip { get; set; }

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x06003B70 RID: 15216 RVA: 0x000C1621 File Offset: 0x000BF821
		// (set) Token: 0x06003B71 RID: 15217 RVA: 0x000C1629 File Offset: 0x000BF829
		public bool SupportsSelect { get; set; }

		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x06003B72 RID: 15218 RVA: 0x000C1632 File Offset: 0x000BF832
		// (set) Token: 0x06003B73 RID: 15219 RVA: 0x000C163A File Offset: 0x000BF83A
		public List<NamedValue> DisplayAnnotations { get; set; }

		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x06003B74 RID: 15220 RVA: 0x000C1643 File Offset: 0x000BF843
		// (set) Token: 0x06003B75 RID: 15221 RVA: 0x000C164B File Offset: 0x000BF84B
		public bool SupportsCount { get; set; }

		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x06003B76 RID: 15222 RVA: 0x000C1654 File Offset: 0x000BF854
		// (set) Token: 0x06003B77 RID: 15223 RVA: 0x000C165C File Offset: 0x000BF85C
		public HashSet<string> NonCountableProperties { get; set; }

		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x06003B78 RID: 15224 RVA: 0x000C1665 File Offset: 0x000BF865
		// (set) Token: 0x06003B79 RID: 15225 RVA: 0x000C166D File Offset: 0x000BF86D
		public HashSet<string> NonCountableNavigationProperties { get; set; }

		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x06003B7A RID: 15226 RVA: 0x000C1676 File Offset: 0x000BF876
		// (set) Token: 0x06003B7B RID: 15227 RVA: 0x000C167E File Offset: 0x000BF87E
		public bool SupportsExpand { get; set; }

		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x06003B7C RID: 15228 RVA: 0x000C1687 File Offset: 0x000BF887
		// (set) Token: 0x06003B7D RID: 15229 RVA: 0x000C168F File Offset: 0x000BF88F
		public HashSet<string> NonExpandableProperties { get; set; }

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x06003B7E RID: 15230 RVA: 0x000C1698 File Offset: 0x000BF898
		// (set) Token: 0x06003B7F RID: 15231 RVA: 0x000C16A0 File Offset: 0x000BF8A0
		public bool SupportsSort { get; set; }

		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x06003B80 RID: 15232 RVA: 0x000C16A9 File Offset: 0x000BF8A9
		// (set) Token: 0x06003B81 RID: 15233 RVA: 0x000C16B1 File Offset: 0x000BF8B1
		public HashSet<string> AscendingOnlyProperties { get; set; }

		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x06003B82 RID: 15234 RVA: 0x000C16BA File Offset: 0x000BF8BA
		// (set) Token: 0x06003B83 RID: 15235 RVA: 0x000C16C2 File Offset: 0x000BF8C2
		public HashSet<string> DescendingOnlyProperties { get; set; }

		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x06003B84 RID: 15236 RVA: 0x000C16CB File Offset: 0x000BF8CB
		// (set) Token: 0x06003B85 RID: 15237 RVA: 0x000C16D3 File Offset: 0x000BF8D3
		public HashSet<string> NonSortableProperties { get; set; }

		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x06003B86 RID: 15238 RVA: 0x000C16DC File Offset: 0x000BF8DC
		// (set) Token: 0x06003B87 RID: 15239 RVA: 0x000C16E4 File Offset: 0x000BF8E4
		public bool SupportsFilter { get; set; }

		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x06003B88 RID: 15240 RVA: 0x000C16ED File Offset: 0x000BF8ED
		// (set) Token: 0x06003B89 RID: 15241 RVA: 0x000C16F5 File Offset: 0x000BF8F5
		public bool RequiresFilter { get; set; }

		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x06003B8A RID: 15242 RVA: 0x000C16FE File Offset: 0x000BF8FE
		// (set) Token: 0x06003B8B RID: 15243 RVA: 0x000C1706 File Offset: 0x000BF906
		public HashSet<string> FilterFunctions { get; set; }

		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x06003B8C RID: 15244 RVA: 0x000C170F File Offset: 0x000BF90F
		// (set) Token: 0x06003B8D RID: 15245 RVA: 0x000C1717 File Offset: 0x000BF917
		public HashSet<string> RequiredPropertiesInFilter { get; set; }

		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x06003B8E RID: 15246 RVA: 0x000C1720 File Offset: 0x000BF920
		// (set) Token: 0x06003B8F RID: 15247 RVA: 0x000C1728 File Offset: 0x000BF928
		public HashSet<string> NonFilterableProperties { get; set; }

		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x06003B90 RID: 15248 RVA: 0x000C1731 File Offset: 0x000BF931
		// (set) Token: 0x06003B91 RID: 15249 RVA: 0x000C1739 File Offset: 0x000BF939
		public bool IsInsertable { get; set; }

		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x06003B92 RID: 15250 RVA: 0x000C1742 File Offset: 0x000BF942
		// (set) Token: 0x06003B93 RID: 15251 RVA: 0x000C174A File Offset: 0x000BF94A
		public bool IsUpdatable { get; set; }

		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x06003B94 RID: 15252 RVA: 0x000C1753 File Offset: 0x000BF953
		// (set) Token: 0x06003B95 RID: 15253 RVA: 0x000C175B File Offset: 0x000BF95B
		public bool IsDeletable { get; set; }

		// Token: 0x06003B96 RID: 15254 RVA: 0x000C1764 File Offset: 0x000BF964
		public bool CanSort(string columnName, bool isAscending)
		{
			if (!this.SupportsSort || this.NonSortableProperties.Contains(columnName))
			{
				return false;
			}
			if (isAscending)
			{
				return !this.DescendingOnlyProperties.Contains(columnName);
			}
			return !this.AscendingOnlyProperties.Contains(columnName);
		}

		// Token: 0x06003B97 RID: 15255 RVA: 0x000C17A0 File Offset: 0x000BF9A0
		public bool CanFilter(string columnName)
		{
			return this.SupportsFilter && !this.NonFilterableProperties.Contains(columnName);
		}

		// Token: 0x06003B98 RID: 15256 RVA: 0x000C17BC File Offset: 0x000BF9BC
		public bool RequiredPropertiesExist(List<string> filteredProperties)
		{
			if (this.RequiresFilter)
			{
				foreach (string text in this.RequiredPropertiesInFilter)
				{
					if (!filteredProperties.Contains(text))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06003B99 RID: 15257 RVA: 0x000C1820 File Offset: 0x000BFA20
		public bool CanExpand(string columnName)
		{
			return this.SupportsExpand && !this.NonExpandableProperties.Contains(columnName);
		}
	}
}
