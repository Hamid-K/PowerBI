using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000B0 RID: 176
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
	public sealed class FilterAttribute : Attribute
	{
		// Token: 0x060005FC RID: 1532 RVA: 0x000153E7 File Offset: 0x000135E7
		public FilterAttribute()
		{
			this._defaultEnableFilter = new bool?(true);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00015408 File Offset: 0x00013608
		public FilterAttribute(params string[] properties)
		{
			foreach (string text in properties)
			{
				this._filterConfigurations.Add(text, true);
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x00015447 File Offset: 0x00013647
		public Dictionary<string, bool> FilterConfigurations
		{
			get
			{
				return this._filterConfigurations;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x0001544F File Offset: 0x0001364F
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x00015458 File Offset: 0x00013658
		public bool Disabled
		{
			get
			{
				return this._disable;
			}
			set
			{
				this._disable = value;
				foreach (string text in this._filterConfigurations.Keys.ToList<string>())
				{
					this._filterConfigurations[text] = !this._disable;
				}
				if (this._filterConfigurations.Count == 0)
				{
					this._defaultEnableFilter = new bool?(!this._disable);
				}
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x000154EC File Offset: 0x000136EC
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x000154F4 File Offset: 0x000136F4
		internal bool? DefaultEnableFilter
		{
			get
			{
				return this._defaultEnableFilter;
			}
			set
			{
				this._defaultEnableFilter = value;
			}
		}

		// Token: 0x04000165 RID: 357
		private bool? _defaultEnableFilter;

		// Token: 0x04000166 RID: 358
		private bool _disable;

		// Token: 0x04000167 RID: 359
		private readonly Dictionary<string, bool> _filterConfigurations = new Dictionary<string, bool>();
	}
}
