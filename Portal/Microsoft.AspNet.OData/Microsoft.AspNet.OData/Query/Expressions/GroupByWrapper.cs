using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Microsoft.AspNet.OData.Query.Expressions
{
	// Token: 0x020000E6 RID: 230
	[JsonConverter(typeof(DynamicTypeWrapperConverter))]
	internal class GroupByWrapper : DynamicTypeWrapper
	{
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0001CF77 File Offset: 0x0001B177
		// (set) Token: 0x060007BE RID: 1982 RVA: 0x0001CF7F File Offset: 0x0001B17F
		public virtual AggregationPropertyContainer GroupByContainer { get; set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0001CF88 File Offset: 0x0001B188
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x0001CF90 File Offset: 0x0001B190
		public virtual AggregationPropertyContainer Container { get; set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0001CF99 File Offset: 0x0001B199
		public override Dictionary<string, object> Values
		{
			get
			{
				this.EnsureValues();
				return this._values;
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001CFA8 File Offset: 0x0001B1A8
		public override bool Equals(object obj)
		{
			GroupByWrapper groupByWrapper = obj as GroupByWrapper;
			if (groupByWrapper == null)
			{
				return false;
			}
			Dictionary<string, object> values = this.Values;
			Dictionary<string, object> values2 = groupByWrapper.Values;
			return values.Count<KeyValuePair<string, object>>() == values2.Count<KeyValuePair<string, object>>() && !values.Except(values2).Any<KeyValuePair<string, object>>();
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001CFF0 File Offset: 0x0001B1F0
		public override int GetHashCode()
		{
			this.EnsureValues();
			long num = 1870403278L;
			foreach (object obj in this.Values.Values)
			{
				num = num * -1521134295L + (long)((obj == null) ? 0 : obj.GetHashCode());
			}
			return (int)num;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0001D068 File Offset: 0x0001B268
		private void EnsureValues()
		{
			if (this._values == null)
			{
				if (this.GroupByContainer != null)
				{
					this._values = this.GroupByContainer.ToDictionary(GroupByWrapper.DefaultPropertyMapper, true);
				}
				else
				{
					this._values = new Dictionary<string, object>();
				}
				if (this.Container != null)
				{
					this._values = this._values.Concat(this.Container.ToDictionary(GroupByWrapper.DefaultPropertyMapper, true)).ToDictionary((KeyValuePair<string, object> kvp) => kvp.Key, (KeyValuePair<string, object> kvp) => kvp.Value);
				}
			}
		}

		// Token: 0x0400024A RID: 586
		private Dictionary<string, object> _values;

		// Token: 0x0400024B RID: 587
		protected static readonly IPropertyMapper DefaultPropertyMapper = new IdentityPropertyMapper();
	}
}
