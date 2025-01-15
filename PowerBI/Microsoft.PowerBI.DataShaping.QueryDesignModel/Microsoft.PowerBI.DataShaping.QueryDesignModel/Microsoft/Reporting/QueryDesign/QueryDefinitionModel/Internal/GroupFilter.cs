using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000D2 RID: 210
	internal sealed class GroupFilter
	{
		// Token: 0x06000D7D RID: 3453 RVA: 0x00022860 File Offset: 0x00020A60
		internal GroupFilter(CompoundFilterCondition filter, IEnumerable<string> groupRefs)
		{
			this._filter = ArgumentValidation.CheckCondition<CompoundFilterCondition>(filter, !filter.IsNullOrEmpty(), "filter");
			this._groupRefs = ArgumentValidation.CheckNotNullOrEmpty<string>(groupRefs, "groupRefs").ToReadOnlyCollection<string>();
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00022898 File Offset: 0x00020A98
		internal GroupFilter(CompoundFilterCondition filter, IEnumerable<Group> groups)
			: this(filter, ArgumentValidation.CheckNotNullOrEmpty<Group>(groups, "groups").GetNames())
		{
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x000228B1 File Offset: 0x00020AB1
		public CompoundFilterCondition Filter
		{
			get
			{
				return this._filter;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x000228B9 File Offset: 0x00020AB9
		public ReadOnlyCollection<string> GroupRefs
		{
			get
			{
				return this._groupRefs;
			}
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x000228C4 File Offset: 0x00020AC4
		public bool RefersTo(Group group)
		{
			return this._groupRefs.Any((string name) => group.GroupNameEquals(name));
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x000228F8 File Offset: 0x00020AF8
		internal GroupFilter OmitMissingGroups(IEnumerable<Group> groups)
		{
			IEnumerable<string> enumerable = this.GroupRefs.OmitMissingGroups(groups);
			if (enumerable.Any<string>())
			{
				return new GroupFilter(this.Filter, enumerable);
			}
			return null;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00022928 File Offset: 0x00020B28
		internal bool IsValid(IEnumerable<Group> groups)
		{
			return this.GroupRefs.OmitMissingGroups(groups).Count<string>() == this.GroupRefs.Count;
		}

		// Token: 0x0400097E RID: 2430
		private readonly CompoundFilterCondition _filter;

		// Token: 0x0400097F RID: 2431
		private readonly ReadOnlyCollection<string> _groupRefs;
	}
}
