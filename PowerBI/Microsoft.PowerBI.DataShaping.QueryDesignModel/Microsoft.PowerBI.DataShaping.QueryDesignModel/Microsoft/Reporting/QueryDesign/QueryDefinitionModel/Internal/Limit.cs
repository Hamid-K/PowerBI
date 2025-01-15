using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000ED RID: 237
	internal sealed class Limit
	{
		// Token: 0x06000E01 RID: 3585 RVA: 0x000239DC File Offset: 0x00021BDC
		internal Limit(LimitOperator limitOp, IEnumerable<string> groupRefs, IEnumerable<SortItem> sorting)
		{
			this._operator = ArgumentValidation.CheckNotNull<LimitOperator>(limitOp, "limitOp");
			this._groupRefs = ArgumentValidation.CheckNotNullOrEmpty<string>(groupRefs, "groupRefs").ToReadOnlyCollection<string>();
			this._sorting = ArgumentValidation.CheckNotNull<IEnumerable<SortItem>>(sorting, "sorting").ToReadOnlyCollection<SortItem>();
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00023A2C File Offset: 0x00021C2C
		internal Limit(LimitOperator limitOp, Group group, IEnumerable<SortItem> sorting)
			: this(limitOp, new Group[] { group }, sorting)
		{
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00023A40 File Offset: 0x00021C40
		internal Limit(LimitOperator limitOp, IEnumerable<Group> groups, IEnumerable<SortItem> sorting)
			: this(limitOp, groups.Select((Group g) => g.Name), sorting)
		{
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x00023A6F File Offset: 0x00021C6F
		public LimitOperator Operator
		{
			get
			{
				return this._operator;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x00023A77 File Offset: 0x00021C77
		public ReadOnlyCollection<string> GroupRefs
		{
			get
			{
				return this._groupRefs;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x00023A7F File Offset: 0x00021C7F
		public ReadOnlyCollection<SortItem> Sorting
		{
			get
			{
				return this._sorting;
			}
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00023A88 File Offset: 0x00021C88
		public bool RefersTo(Group group)
		{
			return this._groupRefs.Any((string name) => group.GroupNameEquals(name));
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x00023AB9 File Offset: 0x00021CB9
		public bool SpansToMultipleGroups
		{
			get
			{
				return this._groupRefs.Count > 1;
			}
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00023AC9 File Offset: 0x00021CC9
		public bool IsSpanningLimitStartingFrom(Group group)
		{
			return this.SpansToMultipleGroups && group.GroupNameEquals(this._groupRefs[0]);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00023AE7 File Offset: 0x00021CE7
		public bool IsSpanningLimitEndingWith(Group group)
		{
			return this.SpansToMultipleGroups && group.GroupNameEquals(this._groupRefs[this._groupRefs.Count - 1]);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00023B14 File Offset: 0x00021D14
		public Limit OmitMissingGroups(IEnumerable<Group> groups, Rollup rollup)
		{
			IEnumerable<string> enumerable = this.GroupRefs.OmitMissingGroups(groups);
			if (enumerable.Any<string>())
			{
				return new Limit(this.Operator, enumerable, this.Sorting.OmitMissingGroups(groups, rollup));
			}
			return null;
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00023B54 File Offset: 0x00021D54
		public int GetLastGroupIndexFrom(IList<Group> groups)
		{
			int num = this.GroupRefs.Count - 1;
			for (int i = 0; i < groups.Count; i++)
			{
				if (groups[i].GroupNameEquals(this.GroupRefs[num]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x040009B5 RID: 2485
		private readonly LimitOperator _operator;

		// Token: 0x040009B6 RID: 2486
		private readonly ReadOnlyCollection<string> _groupRefs;

		// Token: 0x040009B7 RID: 2487
		private readonly ReadOnlyCollection<SortItem> _sorting;
	}
}
