using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000842 RID: 2114
	internal sealed class Capabilities
	{
		// Token: 0x06003CCC RID: 15564 RVA: 0x000C5C30 File Offset: 0x000C3E30
		public Capabilities()
		{
			this.IsIndexableByKey = true;
			this.IsInsertable = true;
			this.IsDeletable = true;
			this.IsUpdatable = true;
			this.DisplayAnnotations = new List<NamedValue>();
			this.RestrictedNavigationProperties = new Dictionary<string, NavigationType>();
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

		// Token: 0x17001415 RID: 5141
		// (get) Token: 0x06003CCD RID: 15565 RVA: 0x000C5CF1 File Offset: 0x000C3EF1
		// (set) Token: 0x06003CCE RID: 15566 RVA: 0x000C5CF9 File Offset: 0x000C3EF9
		public bool PropertyRestrictions { get; set; }

		// Token: 0x17001416 RID: 5142
		// (get) Token: 0x06003CCF RID: 15567 RVA: 0x000C5D02 File Offset: 0x000C3F02
		// (set) Token: 0x06003CD0 RID: 15568 RVA: 0x000C5D0A File Offset: 0x000C3F0A
		public bool ApplySupported { get; set; }

		// Token: 0x17001417 RID: 5143
		// (get) Token: 0x06003CD1 RID: 15569 RVA: 0x000C5D13 File Offset: 0x000C3F13
		// (set) Token: 0x06003CD2 RID: 15570 RVA: 0x000C5D1B File Offset: 0x000C3F1B
		public HashSet<string> AggregateTransformations { get; set; }

		// Token: 0x17001418 RID: 5144
		// (get) Token: 0x06003CD3 RID: 15571 RVA: 0x000C5D24 File Offset: 0x000C3F24
		// (set) Token: 0x06003CD4 RID: 15572 RVA: 0x000C5D2C File Offset: 0x000C3F2C
		public bool IsIndexableByKey { get; set; }

		// Token: 0x17001419 RID: 5145
		// (get) Token: 0x06003CD5 RID: 15573 RVA: 0x000C5D35 File Offset: 0x000C3F35
		// (set) Token: 0x06003CD6 RID: 15574 RVA: 0x000C5D3D File Offset: 0x000C3F3D
		public bool SupportsTop { get; set; }

		// Token: 0x1700141A RID: 5146
		// (get) Token: 0x06003CD7 RID: 15575 RVA: 0x000C5D46 File Offset: 0x000C3F46
		// (set) Token: 0x06003CD8 RID: 15576 RVA: 0x000C5D4E File Offset: 0x000C3F4E
		public bool SupportsSkip { get; set; }

		// Token: 0x1700141B RID: 5147
		// (get) Token: 0x06003CD9 RID: 15577 RVA: 0x000C5D57 File Offset: 0x000C3F57
		// (set) Token: 0x06003CDA RID: 15578 RVA: 0x000C5D5F File Offset: 0x000C3F5F
		public bool SupportsSelect { get; set; }

		// Token: 0x1700141C RID: 5148
		// (get) Token: 0x06003CDB RID: 15579 RVA: 0x000C5D68 File Offset: 0x000C3F68
		// (set) Token: 0x06003CDC RID: 15580 RVA: 0x000C5D70 File Offset: 0x000C3F70
		public List<NamedValue> DisplayAnnotations { get; set; }

		// Token: 0x1700141D RID: 5149
		// (get) Token: 0x06003CDD RID: 15581 RVA: 0x000C5D79 File Offset: 0x000C3F79
		// (set) Token: 0x06003CDE RID: 15582 RVA: 0x000C5D81 File Offset: 0x000C3F81
		public NavigationType Navigability { get; set; }

		// Token: 0x1700141E RID: 5150
		// (get) Token: 0x06003CDF RID: 15583 RVA: 0x000C5D8A File Offset: 0x000C3F8A
		// (set) Token: 0x06003CE0 RID: 15584 RVA: 0x000C5D92 File Offset: 0x000C3F92
		public Dictionary<string, NavigationType> RestrictedNavigationProperties { get; set; }

		// Token: 0x1700141F RID: 5151
		// (get) Token: 0x06003CE1 RID: 15585 RVA: 0x000C5D9B File Offset: 0x000C3F9B
		// (set) Token: 0x06003CE2 RID: 15586 RVA: 0x000C5DA3 File Offset: 0x000C3FA3
		public bool SupportsCount { get; set; }

		// Token: 0x17001420 RID: 5152
		// (get) Token: 0x06003CE3 RID: 15587 RVA: 0x000C5DAC File Offset: 0x000C3FAC
		// (set) Token: 0x06003CE4 RID: 15588 RVA: 0x000C5DB4 File Offset: 0x000C3FB4
		public HashSet<string> NonCountableProperties { get; set; }

		// Token: 0x17001421 RID: 5153
		// (get) Token: 0x06003CE5 RID: 15589 RVA: 0x000C5DBD File Offset: 0x000C3FBD
		// (set) Token: 0x06003CE6 RID: 15590 RVA: 0x000C5DC5 File Offset: 0x000C3FC5
		public HashSet<string> NonCountableNavigationProperties { get; set; }

		// Token: 0x17001422 RID: 5154
		// (get) Token: 0x06003CE7 RID: 15591 RVA: 0x000C5DCE File Offset: 0x000C3FCE
		// (set) Token: 0x06003CE8 RID: 15592 RVA: 0x000C5DD6 File Offset: 0x000C3FD6
		public bool SupportsExpand { get; set; }

		// Token: 0x17001423 RID: 5155
		// (get) Token: 0x06003CE9 RID: 15593 RVA: 0x000C5DDF File Offset: 0x000C3FDF
		// (set) Token: 0x06003CEA RID: 15594 RVA: 0x000C5DE7 File Offset: 0x000C3FE7
		public HashSet<string> NonExpandableProperties { get; set; }

		// Token: 0x17001424 RID: 5156
		// (get) Token: 0x06003CEB RID: 15595 RVA: 0x000C5DF0 File Offset: 0x000C3FF0
		// (set) Token: 0x06003CEC RID: 15596 RVA: 0x000C5DF8 File Offset: 0x000C3FF8
		public bool SupportsSort { get; set; }

		// Token: 0x17001425 RID: 5157
		// (get) Token: 0x06003CED RID: 15597 RVA: 0x000C5E01 File Offset: 0x000C4001
		// (set) Token: 0x06003CEE RID: 15598 RVA: 0x000C5E09 File Offset: 0x000C4009
		public HashSet<string> AscendingOnlyProperties { get; set; }

		// Token: 0x17001426 RID: 5158
		// (get) Token: 0x06003CEF RID: 15599 RVA: 0x000C5E12 File Offset: 0x000C4012
		// (set) Token: 0x06003CF0 RID: 15600 RVA: 0x000C5E1A File Offset: 0x000C401A
		public HashSet<string> DescendingOnlyProperties { get; set; }

		// Token: 0x17001427 RID: 5159
		// (get) Token: 0x06003CF1 RID: 15601 RVA: 0x000C5E23 File Offset: 0x000C4023
		// (set) Token: 0x06003CF2 RID: 15602 RVA: 0x000C5E2B File Offset: 0x000C402B
		public HashSet<string> NonSortableProperties { get; set; }

		// Token: 0x17001428 RID: 5160
		// (get) Token: 0x06003CF3 RID: 15603 RVA: 0x000C5E34 File Offset: 0x000C4034
		// (set) Token: 0x06003CF4 RID: 15604 RVA: 0x000C5E3C File Offset: 0x000C403C
		public bool SupportsFilter { get; set; }

		// Token: 0x17001429 RID: 5161
		// (get) Token: 0x06003CF5 RID: 15605 RVA: 0x000C5E45 File Offset: 0x000C4045
		// (set) Token: 0x06003CF6 RID: 15606 RVA: 0x000C5E4D File Offset: 0x000C404D
		public bool RequiresFilter { get; set; }

		// Token: 0x1700142A RID: 5162
		// (get) Token: 0x06003CF7 RID: 15607 RVA: 0x000C5E56 File Offset: 0x000C4056
		// (set) Token: 0x06003CF8 RID: 15608 RVA: 0x000C5E5E File Offset: 0x000C405E
		public HashSet<string> FilterFunctions { get; set; }

		// Token: 0x1700142B RID: 5163
		// (get) Token: 0x06003CF9 RID: 15609 RVA: 0x000C5E67 File Offset: 0x000C4067
		// (set) Token: 0x06003CFA RID: 15610 RVA: 0x000C5E6F File Offset: 0x000C406F
		public HashSet<string> RequiredPropertiesInFilter { get; set; }

		// Token: 0x1700142C RID: 5164
		// (get) Token: 0x06003CFB RID: 15611 RVA: 0x000C5E78 File Offset: 0x000C4078
		// (set) Token: 0x06003CFC RID: 15612 RVA: 0x000C5E80 File Offset: 0x000C4080
		public HashSet<string> NonFilterableProperties { get; set; }

		// Token: 0x1700142D RID: 5165
		// (get) Token: 0x06003CFD RID: 15613 RVA: 0x000C5E89 File Offset: 0x000C4089
		// (set) Token: 0x06003CFE RID: 15614 RVA: 0x000C5E91 File Offset: 0x000C4091
		public bool IsInsertable { get; set; }

		// Token: 0x1700142E RID: 5166
		// (get) Token: 0x06003CFF RID: 15615 RVA: 0x000C5E9A File Offset: 0x000C409A
		// (set) Token: 0x06003D00 RID: 15616 RVA: 0x000C5EA2 File Offset: 0x000C40A2
		public bool IsUpdatable { get; set; }

		// Token: 0x1700142F RID: 5167
		// (get) Token: 0x06003D01 RID: 15617 RVA: 0x000C5EAB File Offset: 0x000C40AB
		// (set) Token: 0x06003D02 RID: 15618 RVA: 0x000C5EB3 File Offset: 0x000C40B3
		public bool IsDeletable { get; set; }

		// Token: 0x06003D03 RID: 15619 RVA: 0x000C5EBC File Offset: 0x000C40BC
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

		// Token: 0x06003D04 RID: 15620 RVA: 0x000C5EF8 File Offset: 0x000C40F8
		public bool CanFilter(string columnName)
		{
			return this.SupportsFilter && !this.NonFilterableProperties.Contains(columnName);
		}

		// Token: 0x06003D05 RID: 15621 RVA: 0x000C5F14 File Offset: 0x000C4114
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

		// Token: 0x06003D06 RID: 15622 RVA: 0x000C5F78 File Offset: 0x000C4178
		public bool CanExpand(string columnName)
		{
			return this.SupportsExpand && !this.NonExpandableProperties.Contains(columnName);
		}
	}
}
