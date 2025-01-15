using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x02000119 RID: 281
	internal sealed class RollupGroup : INamedProjection, INamedItem
	{
		// Token: 0x06001011 RID: 4113 RVA: 0x0002C463 File Offset: 0x0002A663
		private RollupGroup(string aggregateIndicatorName)
		{
			this._aggregateIndicatorName = ArgumentValidation.CheckNotNullOrEmpty(aggregateIndicatorName, "aggregateIndicatorName");
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0002C47C File Offset: 0x0002A67C
		internal RollupGroup(string aggregateIndicatorName, IEnumerable<string> groupNames)
			: this(aggregateIndicatorName)
		{
			ArgumentValidation.CheckNotNullOrEmpty<string>(groupNames, "groupNames");
			this._groupRefs = groupNames.ToReadOnlyCollection<string>();
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0002C4A0 File Offset: 0x0002A6A0
		internal RollupGroup(string aggregateIndicatorName, IEnumerable<Group> groups)
			: this(aggregateIndicatorName)
		{
			ArgumentValidation.CheckNotNullOrEmpty<Group>(groups, "groups");
			this._groupRefs = groups.Select((Group g) => g.Name).ToReadOnlyCollection<string>();
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0002C4F0 File Offset: 0x0002A6F0
		internal RollupGroup(string aggregateIndicatorName, Group group)
			: this(aggregateIndicatorName, new string[] { group.Name })
		{
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x0002C508 File Offset: 0x0002A708
		public string AggregateIndicatorName
		{
			get
			{
				return this._aggregateIndicatorName;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x0002C510 File Offset: 0x0002A710
		public ReadOnlyCollection<string> GroupRefs
		{
			get
			{
				return this._groupRefs;
			}
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0002C518 File Offset: 0x0002A718
		internal bool RefersTo(Group group)
		{
			return this.GroupRefs.Any((string name) => group.GroupNameEquals(name));
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0002C549 File Offset: 0x0002A749
		internal bool AggregateIndicatorNameEquals(string candidate)
		{
			return EdmItem.IdentityComparer.Equals(this.AggregateIndicatorName, candidate);
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0002C55C File Offset: 0x0002A75C
		internal RollupGroup OmitMissingGroups(IEnumerable<Group> groups)
		{
			IEnumerable<string> enumerable = this.GroupRefs.OmitMissingGroups(groups);
			if (enumerable.Any<string>())
			{
				return new RollupGroup(this.AggregateIndicatorName, enumerable);
			}
			return null;
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x0002C58C File Offset: 0x0002A78C
		QueryExpression INamedProjection.Expression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x0002C58F File Offset: 0x0002A78F
		string INamedItem.Name
		{
			get
			{
				return this._aggregateIndicatorName;
			}
		}

		// Token: 0x04000A5B RID: 2651
		private readonly string _aggregateIndicatorName;

		// Token: 0x04000A5C RID: 2652
		private readonly ReadOnlyCollection<string> _groupRefs;
	}
}
