using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000AC RID: 172
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
	public sealed class SelectAttribute : Attribute
	{
		// Token: 0x060005F0 RID: 1520 RVA: 0x000152A5 File Offset: 0x000134A5
		public SelectAttribute()
		{
			this._defaultSelectType = new SelectExpandType?(SelectExpandType.Allowed);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000152C4 File Offset: 0x000134C4
		public SelectAttribute(params string[] properties)
		{
			foreach (string text in properties)
			{
				if (!this._selectConfigurations.ContainsKey(text))
				{
					this._selectConfigurations.Add(text, SelectExpandType.Allowed);
				}
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00015311 File Offset: 0x00013511
		public Dictionary<string, SelectExpandType> SelectConfigurations
		{
			get
			{
				return this._selectConfigurations;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00015319 File Offset: 0x00013519
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x00015324 File Offset: 0x00013524
		public SelectExpandType SelectType
		{
			get
			{
				return this._selectType;
			}
			set
			{
				this._selectType = value;
				foreach (string text in this._selectConfigurations.Keys.ToList<string>())
				{
					this._selectConfigurations[text] = this._selectType;
				}
				if (this._selectConfigurations.Count == 0)
				{
					this._defaultSelectType = new SelectExpandType?(this._selectType);
				}
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x000153B4 File Offset: 0x000135B4
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x000153BC File Offset: 0x000135BC
		internal SelectExpandType? DefaultSelectType
		{
			get
			{
				return this._defaultSelectType;
			}
			set
			{
				this._defaultSelectType = value;
			}
		}

		// Token: 0x04000159 RID: 345
		private readonly Dictionary<string, SelectExpandType> _selectConfigurations = new Dictionary<string, SelectExpandType>();

		// Token: 0x0400015A RID: 346
		private SelectExpandType _selectType;

		// Token: 0x0400015B RID: 347
		private SelectExpandType? _defaultSelectType;
	}
}
