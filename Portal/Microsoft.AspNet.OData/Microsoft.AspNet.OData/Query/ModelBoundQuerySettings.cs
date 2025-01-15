using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000B8 RID: 184
	public class ModelBoundQuerySettings
	{
		// Token: 0x06000634 RID: 1588 RVA: 0x00015B51 File Offset: 0x00013D51
		public ModelBoundQuerySettings()
		{
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00015B94 File Offset: 0x00013D94
		public ModelBoundQuerySettings(ModelBoundQuerySettings querySettings)
		{
			this._maxTop = querySettings.MaxTop;
			this.PageSize = querySettings.PageSize;
			this.Countable = querySettings.Countable;
			this.DefaultEnableFilter = querySettings.DefaultEnableFilter;
			this.DefaultEnableOrderBy = querySettings.DefaultEnableOrderBy;
			this.DefaultExpandType = querySettings.DefaultExpandType;
			this.DefaultMaxDepth = querySettings.DefaultMaxDepth;
			this.DefaultSelectType = querySettings.DefaultSelectType;
			this.CopyOrderByConfigurations(querySettings.OrderByConfigurations);
			this.CopyFilterConfigurations(querySettings.FilterConfigurations);
			this.CopyExpandConfigurations(querySettings.ExpandConfigurations);
			this.CopySelectConfigurations(querySettings.SelectConfigurations);
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00015C6F File Offset: 0x00013E6F
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x00015C78 File Offset: 0x00013E78
		public int? MaxTop
		{
			get
			{
				return this._maxTop;
			}
			set
			{
				if (value != null)
				{
					int? num = value;
					int num2 = 0;
					if ((num.GetValueOrDefault() <= num2) & (num != null))
					{
						throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 1);
					}
				}
				this._maxTop = value;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00015CC7 File Offset: 0x00013EC7
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x00015CD0 File Offset: 0x00013ED0
		public int? PageSize
		{
			get
			{
				return this._pageSize;
			}
			set
			{
				if (value != null)
				{
					int? num = value;
					int num2 = 0;
					if ((num.GetValueOrDefault() <= num2) & (num != null))
					{
						throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 1);
					}
				}
				this._pageSize = value;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x00015D1F File Offset: 0x00013F1F
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x00015D27 File Offset: 0x00013F27
		public bool? Countable { get; set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x00015D30 File Offset: 0x00013F30
		public Dictionary<string, ExpandConfiguration> ExpandConfigurations
		{
			get
			{
				return this._expandConfigurations;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x00015D38 File Offset: 0x00013F38
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x00015D40 File Offset: 0x00013F40
		public SelectExpandType? DefaultExpandType { get; set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x00015D49 File Offset: 0x00013F49
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x00015D51 File Offset: 0x00013F51
		public int DefaultMaxDepth { get; set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x00015D5A File Offset: 0x00013F5A
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x00015D62 File Offset: 0x00013F62
		public bool? DefaultEnableOrderBy { get; set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x00015D6B File Offset: 0x00013F6B
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x00015D73 File Offset: 0x00013F73
		public bool? DefaultEnableFilter { get; set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x00015D7C File Offset: 0x00013F7C
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x00015D84 File Offset: 0x00013F84
		public SelectExpandType? DefaultSelectType { get; set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x00015D8D File Offset: 0x00013F8D
		public Dictionary<string, bool> OrderByConfigurations
		{
			get
			{
				return this._orderByConfigurations;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00015D95 File Offset: 0x00013F95
		public Dictionary<string, bool> FilterConfigurations
		{
			get
			{
				return this._filterConfigurations;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x00015D9D File Offset: 0x00013F9D
		public Dictionary<string, SelectExpandType> SelectConfigurations
		{
			get
			{
				return this._selectConfigurations;
			}
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00015DA8 File Offset: 0x00013FA8
		internal void CopyExpandConfigurations(Dictionary<string, ExpandConfiguration> expandConfigurations)
		{
			this._expandConfigurations.Clear();
			foreach (KeyValuePair<string, ExpandConfiguration> keyValuePair in expandConfigurations)
			{
				this._expandConfigurations.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00015E14 File Offset: 0x00014014
		internal void CopyOrderByConfigurations(Dictionary<string, bool> orderByConfigurations)
		{
			this._orderByConfigurations.Clear();
			foreach (KeyValuePair<string, bool> keyValuePair in orderByConfigurations)
			{
				this._orderByConfigurations.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00015E80 File Offset: 0x00014080
		internal void CopySelectConfigurations(Dictionary<string, SelectExpandType> selectConfigurations)
		{
			this._selectConfigurations.Clear();
			foreach (KeyValuePair<string, SelectExpandType> keyValuePair in selectConfigurations)
			{
				this._selectConfigurations.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00015EEC File Offset: 0x000140EC
		internal void CopyFilterConfigurations(Dictionary<string, bool> filterConfigurations)
		{
			this._filterConfigurations.Clear();
			foreach (KeyValuePair<string, bool> keyValuePair in filterConfigurations)
			{
				this._filterConfigurations.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00015F58 File Offset: 0x00014158
		internal bool Expandable(string propertyName)
		{
			ExpandConfiguration expandConfiguration;
			if (this.ExpandConfigurations.TryGetValue(propertyName, out expandConfiguration))
			{
				return expandConfiguration.ExpandType != SelectExpandType.Disabled;
			}
			if (this.DefaultExpandType != null)
			{
				SelectExpandType? defaultExpandType = this.DefaultExpandType;
				SelectExpandType selectExpandType = SelectExpandType.Disabled;
				return !((defaultExpandType.GetValueOrDefault() == selectExpandType) & (defaultExpandType != null));
			}
			return false;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00015FB4 File Offset: 0x000141B4
		internal bool Selectable(string propertyName)
		{
			SelectExpandType selectExpandType;
			if (this.SelectConfigurations.TryGetValue(propertyName, out selectExpandType))
			{
				return selectExpandType != SelectExpandType.Disabled;
			}
			if (this.DefaultSelectType != null)
			{
				SelectExpandType? defaultSelectType = this.DefaultSelectType;
				SelectExpandType selectExpandType2 = SelectExpandType.Disabled;
				return !((defaultSelectType.GetValueOrDefault() == selectExpandType2) & (defaultSelectType != null));
			}
			return false;
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001600C File Offset: 0x0001420C
		internal bool Sortable(string propertyName)
		{
			bool flag;
			if (this.OrderByConfigurations.TryGetValue(propertyName, out flag))
			{
				return flag;
			}
			bool? defaultEnableOrderBy = this.DefaultEnableOrderBy;
			bool flag2 = true;
			return (defaultEnableOrderBy.GetValueOrDefault() == flag2) & (defaultEnableOrderBy != null);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00016048 File Offset: 0x00014248
		internal bool Filterable(string propertyName)
		{
			bool flag;
			if (this.FilterConfigurations.TryGetValue(propertyName, out flag))
			{
				return flag;
			}
			bool? defaultEnableFilter = this.DefaultEnableFilter;
			bool flag2 = true;
			return (defaultEnableFilter.GetValueOrDefault() == flag2) & (defaultEnableFilter != null);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00016084 File Offset: 0x00014284
		internal bool IsAutomaticExpand(string propertyName)
		{
			ExpandConfiguration expandConfiguration;
			if (this.ExpandConfigurations.TryGetValue(propertyName, out expandConfiguration))
			{
				return expandConfiguration.ExpandType == SelectExpandType.Automatic;
			}
			if (this.DefaultExpandType != null)
			{
				SelectExpandType? defaultExpandType = this.DefaultExpandType;
				SelectExpandType selectExpandType = SelectExpandType.Automatic;
				return (defaultExpandType.GetValueOrDefault() == selectExpandType) & (defaultExpandType != null);
			}
			return false;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000160D8 File Offset: 0x000142D8
		internal bool IsAutomaticSelect(string propertyName)
		{
			SelectExpandType selectExpandType;
			if (this.SelectConfigurations.TryGetValue(propertyName, out selectExpandType))
			{
				return selectExpandType == SelectExpandType.Automatic;
			}
			if (this.DefaultSelectType != null)
			{
				SelectExpandType? defaultSelectType = this.DefaultSelectType;
				SelectExpandType selectExpandType2 = SelectExpandType.Automatic;
				return (defaultSelectType.GetValueOrDefault() == selectExpandType2) & (defaultSelectType != null);
			}
			return false;
		}

		// Token: 0x0400017E RID: 382
		private int? _pageSize;

		// Token: 0x0400017F RID: 383
		private int? _maxTop = new int?(0);

		// Token: 0x04000180 RID: 384
		private Dictionary<string, ExpandConfiguration> _expandConfigurations = new Dictionary<string, ExpandConfiguration>();

		// Token: 0x04000181 RID: 385
		private Dictionary<string, SelectExpandType> _selectConfigurations = new Dictionary<string, SelectExpandType>();

		// Token: 0x04000182 RID: 386
		private Dictionary<string, bool> _orderByConfigurations = new Dictionary<string, bool>();

		// Token: 0x04000183 RID: 387
		private Dictionary<string, bool> _filterConfigurations = new Dictionary<string, bool>();

		// Token: 0x04000184 RID: 388
		internal static ModelBoundQuerySettings DefaultModelBoundQuerySettings = new ModelBoundQuerySettings
		{
			_maxTop = new int?(0)
		};
	}
}
