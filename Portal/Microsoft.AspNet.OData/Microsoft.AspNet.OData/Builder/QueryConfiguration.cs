using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Query;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x0200010A RID: 266
	public class QueryConfiguration
	{
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0002642B File Offset: 0x0002462B
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x00026433 File Offset: 0x00024633
		public ModelBoundQuerySettings ModelBoundQuerySettings
		{
			get
			{
				return this._querySettings;
			}
			set
			{
				this._querySettings = value;
			}
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0002643C File Offset: 0x0002463C
		public virtual void SetCount(bool enableCount)
		{
			this.GetModelBoundQuerySettingsOrDefault().Countable = new bool?(enableCount);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0002644F File Offset: 0x0002464F
		public virtual void SetMaxTop(int? maxTop)
		{
			this.GetModelBoundQuerySettingsOrDefault().MaxTop = maxTop;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0002645D File Offset: 0x0002465D
		public virtual void SetPageSize(int? pageSize)
		{
			this.GetModelBoundQuerySettingsOrDefault().PageSize = pageSize;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0002646C File Offset: 0x0002466C
		public virtual void SetExpand(IEnumerable<string> properties, int? maxDepth, SelectExpandType expandType)
		{
			this.GetModelBoundQuerySettingsOrDefault();
			if (properties == null)
			{
				this.ModelBoundQuerySettings.DefaultExpandType = new SelectExpandType?(expandType);
				this.ModelBoundQuerySettings.DefaultMaxDepth = maxDepth ?? 2;
				return;
			}
			foreach (string text in properties)
			{
				this.ModelBoundQuerySettings.ExpandConfigurations[text] = new ExpandConfiguration
				{
					ExpandType = expandType,
					MaxDepth = (maxDepth ?? 2)
				};
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00026524 File Offset: 0x00024724
		public virtual void SetSelect(IEnumerable<string> properties, SelectExpandType selectType)
		{
			this.GetModelBoundQuerySettingsOrDefault();
			if (properties == null)
			{
				this.ModelBoundQuerySettings.DefaultSelectType = new SelectExpandType?(selectType);
				return;
			}
			foreach (string text in properties)
			{
				this.ModelBoundQuerySettings.SelectConfigurations[text] = selectType;
			}
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00026594 File Offset: 0x00024794
		public virtual void SetOrderBy(IEnumerable<string> properties, bool enableOrderBy)
		{
			this.GetModelBoundQuerySettingsOrDefault();
			if (properties == null)
			{
				this.ModelBoundQuerySettings.DefaultEnableOrderBy = new bool?(enableOrderBy);
				return;
			}
			foreach (string text in properties)
			{
				this.ModelBoundQuerySettings.OrderByConfigurations[text] = enableOrderBy;
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00026604 File Offset: 0x00024804
		public virtual void SetFilter(IEnumerable<string> properties, bool enableFilter)
		{
			this.GetModelBoundQuerySettingsOrDefault();
			if (properties == null)
			{
				this.ModelBoundQuerySettings.DefaultEnableFilter = new bool?(enableFilter);
				return;
			}
			foreach (string text in properties)
			{
				this.ModelBoundQuerySettings.FilterConfigurations[text] = enableFilter;
			}
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00026674 File Offset: 0x00024874
		internal ModelBoundQuerySettings GetModelBoundQuerySettingsOrDefault()
		{
			if (this._querySettings == null)
			{
				this._querySettings = new ModelBoundQuerySettings(ModelBoundQuerySettings.DefaultModelBoundQuerySettings);
			}
			return this._querySettings;
		}

		// Token: 0x040002F1 RID: 753
		private ModelBoundQuerySettings _querySettings;
	}
}
