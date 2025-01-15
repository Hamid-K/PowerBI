using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000B1 RID: 177
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
	public sealed class OrderByAttribute : Attribute
	{
		// Token: 0x06000603 RID: 1539 RVA: 0x000154FD File Offset: 0x000136FD
		public OrderByAttribute()
		{
			this._defaultEnableOrderBy = new bool?(true);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001551C File Offset: 0x0001371C
		public OrderByAttribute(params string[] properties)
		{
			foreach (string text in properties)
			{
				if (!this._orderByConfigurations.ContainsKey(text))
				{
					this._orderByConfigurations.Add(text, true);
				}
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x00015569 File Offset: 0x00013769
		public Dictionary<string, bool> OrderByConfigurations
		{
			get
			{
				return this._orderByConfigurations;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x00015571 File Offset: 0x00013771
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x0001557C File Offset: 0x0001377C
		public bool Disabled
		{
			get
			{
				return this._disable;
			}
			set
			{
				this._disable = value;
				foreach (string text in this._orderByConfigurations.Keys.ToList<string>())
				{
					this._orderByConfigurations[text] = !this._disable;
				}
				if (this._orderByConfigurations.Count == 0)
				{
					this._defaultEnableOrderBy = new bool?(!this._disable);
				}
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00015610 File Offset: 0x00013810
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x00015618 File Offset: 0x00013818
		internal bool? DefaultEnableOrderBy
		{
			get
			{
				return this._defaultEnableOrderBy;
			}
			set
			{
				this._defaultEnableOrderBy = value;
			}
		}

		// Token: 0x04000168 RID: 360
		private bool? _defaultEnableOrderBy;

		// Token: 0x04000169 RID: 361
		private bool _disable;

		// Token: 0x0400016A RID: 362
		private readonly Dictionary<string, bool> _orderByConfigurations = new Dictionary<string, bool>();
	}
}
