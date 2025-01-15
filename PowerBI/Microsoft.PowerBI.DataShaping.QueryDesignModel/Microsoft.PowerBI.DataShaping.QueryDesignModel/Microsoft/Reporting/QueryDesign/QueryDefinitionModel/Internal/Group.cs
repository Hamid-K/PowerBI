using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000D9 RID: 217
	internal sealed class Group : INamedItem
	{
		// Token: 0x06000DA2 RID: 3490 RVA: 0x00022FCB File Offset: 0x000211CB
		internal Group(QueryExpression keyExpression, string name, IEnumerable<GroupDetail> details = null, bool isProjected = true, FollowingJoinBehavior followingJoinBehavior = FollowingJoinBehavior.InnerJoin)
			: this(new GroupKey(name, keyExpression), details, isProjected, followingJoinBehavior)
		{
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00022FDF File Offset: 0x000211DF
		internal Group(EntitySet entitySet, string name = null, IEnumerable<GroupDetail> details = null, bool isProjected = true, FollowingJoinBehavior followingJoinBehavior = FollowingJoinBehavior.InnerJoin, IConceptualEntity entity = null)
			: this(name ?? ((entity != null) ? entity.EdmName : null) ?? ArgumentValidation.CheckNotNull<EntitySet>(entitySet, "entity").Name, Group.CreateGroupKeysForEntity(entitySet, name, entity), details, isProjected, followingJoinBehavior)
		{
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0002301C File Offset: 0x0002121C
		private static IEnumerable<GroupKey> CreateGroupKeysForEntity(EntitySet entitySet, string name, IConceptualEntity entity = null)
		{
			List<EdmFieldInstance> list = entitySet.GetKeyFieldInstances().ToList<EdmFieldInstance>();
			IReadOnlyList<IConceptualColumn> readOnlyList = ((entity != null) ? entity.KeyColumns : null);
			if (list.Count == 1)
			{
				string text = name ?? ((entity != null) ? entity.EdmName : null) ?? entitySet.Name;
				return new GroupKey[]
				{
					new GroupKey(text, list.Single<EdmFieldInstance>().QdmReference((readOnlyList != null) ? readOnlyList.Single<IConceptualColumn>() : null))
				};
			}
			return readOnlyList.Select((IConceptualColumn k) => new GroupKey(k.EdmName, k.QdmReference()));
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x000230B3 File Offset: 0x000212B3
		private Group(GroupKey key, IEnumerable<GroupDetail> details, bool isProjected, FollowingJoinBehavior followingJoinBehavior)
			: this(ArgumentValidation.CheckNotNull<GroupKey>(key, "key").Name, new GroupKey[] { ArgumentValidation.CheckNotNull<GroupKey>(key, "key") }, details, isProjected, followingJoinBehavior)
		{
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x000230E4 File Offset: 0x000212E4
		internal Group(string name, IEnumerable<GroupKey> keys, IEnumerable<GroupDetail> details = null, bool isProjected = true, FollowingJoinBehavior followingJoinBehavior = FollowingJoinBehavior.InnerJoin)
		{
			this._name = ArgumentValidation.CheckNotNullOrEmpty(name, "name");
			this._keys = new ReadOnlyQdmNamedItemCollection<GroupKey>(ArgumentValidation.CheckNotNullOrEmpty<GroupKey>(keys, "keys"));
			this._details = new QdmNamedItemCollection<GroupDetail>(details.EmptyIfNull<GroupDetail>());
			this._followingJoinBehavior = followingJoinBehavior;
			this._isProjected = isProjected;
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x00023146 File Offset: 0x00021346
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x0002314E File Offset: 0x0002134E
		public ReadOnlyQdmNamedItemCollection<GroupKey> Keys
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x00023156 File Offset: 0x00021356
		public QdmNamedItemCollection<GroupDetail> Details
		{
			get
			{
				return this._details;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x0002315E File Offset: 0x0002135E
		public FollowingJoinBehavior FollowingJoinBehavior
		{
			get
			{
				return this._followingJoinBehavior;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x00023166 File Offset: 0x00021366
		// (set) Token: 0x06000DAC RID: 3500 RVA: 0x0002316E File Offset: 0x0002136E
		public bool IsProjected
		{
			get
			{
				return this._isProjected;
			}
			internal set
			{
				this._isProjected = value;
			}
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00023177 File Offset: 0x00021377
		public Group OmitProjection(Limit limit = null, Group groupWithConflictingDetails = null)
		{
			return this.OmitDetails(false, limit, groupWithConflictingDetails);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00023182 File Offset: 0x00021382
		internal Group OmitDetails()
		{
			return this.OmitDetails(this.IsProjected, null, null);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00023192 File Offset: 0x00021392
		internal Group ProjectWithoutDetailsForSubqueries()
		{
			return this.OmitDetails(true, null, null);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x000231A0 File Offset: 0x000213A0
		private Group OmitDetails(bool isProjected, Limit limit, Group groupWithConflictingDetails)
		{
			IEnumerable<GroupDetail> enumerable = (from d in this.Details
				where limit != null && limit.Sorting.Any((SortItem s) => s.RefersTo(d))
				select new
				{
					d = d,
					otherDetail = ((groupWithConflictingDetails == null) ? null : groupWithConflictingDetails.Details.FirstOrDefault((GroupDetail od) => od.Expression.Equals(d.Expression)))
				}).Select(delegate(<>h__TransparentIdentifier0)
			{
				if (<>h__TransparentIdentifier0.otherDetail != null)
				{
					return <>h__TransparentIdentifier0.d.OmitProjection();
				}
				return <>h__TransparentIdentifier0.d;
			});
			return this.ReplaceProperties(enumerable, isProjected);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00023216 File Offset: 0x00021416
		internal Group ProjectDetails()
		{
			return this.ReplaceProperties(this.Details.Select((GroupDetail d) => d.ProjectDetail()), this.IsProjected);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0002324E File Offset: 0x0002144E
		private Group ReplaceProperties(IEnumerable<GroupDetail> details, bool isProjected)
		{
			return new Group(this.Name, this.Keys, details, isProjected, this.FollowingJoinBehavior);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00023269 File Offset: 0x00021469
		internal IEnumerable<GroupField> GetProjectedFields(bool includeKeyForNonProjectedGroup = false)
		{
			if (this.IsProjected || includeKeyForNonProjectedGroup)
			{
				foreach (GroupKey groupKey in this.Keys)
				{
					yield return new GroupField(groupKey.Name, GroupFieldType.Key);
				}
				IEnumerator<GroupKey> enumerator = null;
			}
			if (!this.IsProjected)
			{
				yield break;
			}
			foreach (GroupDetail groupDetail in this.Details.Where((GroupDetail d) => d.IsProjected))
			{
				yield return new GroupField(groupDetail.Name, GroupFieldType.Detail);
			}
			IEnumerator<GroupDetail> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00023280 File Offset: 0x00021480
		internal IEnumerable<IEdmFieldInstance> GetKeyAndDetailModelFieldReferences()
		{
			foreach (GroupKey groupKey in this.Keys)
			{
				yield return groupKey.Expression.GetReferencedModelField();
			}
			IEnumerator<GroupKey> enumerator = null;
			foreach (GroupDetail groupDetail in this.Details)
			{
				if (groupDetail.Expression.IsModelFieldReference())
				{
					yield return groupDetail.Expression.GetReferencedModelField();
				}
			}
			IEnumerator<GroupDetail> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00023290 File Offset: 0x00021490
		internal IEnumerable<IConceptualColumn> GetKeyAndDetailModelColumnReferences()
		{
			foreach (GroupKey groupKey in this.Keys)
			{
				yield return groupKey.Expression.GetReferencedModelColumn();
			}
			IEnumerator<GroupKey> enumerator = null;
			foreach (GroupDetail groupDetail in this.Details)
			{
				if (groupDetail.Expression.IsModelFieldReference())
				{
					yield return groupDetail.Expression.GetReferencedModelColumn();
				}
			}
			IEnumerator<GroupDetail> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x000232A0 File Offset: 0x000214A0
		internal QueryExpression GetExpressionByName(string name)
		{
			GroupDetail groupDetail;
			if (this.Details.TryGetItem(name, out groupDetail))
			{
				return groupDetail.Expression;
			}
			GroupKey groupKey;
			if (this.Keys.TryGetItem(name, out groupKey))
			{
				return groupKey.Expression;
			}
			return null;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x000232DC File Offset: 0x000214DC
		internal bool ContainsKey(QueryExpression expr)
		{
			return this._keys.GetExpressions().Contains(expr);
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x000232EF File Offset: 0x000214EF
		internal bool HasNamedItem(string name)
		{
			return this.Keys.Contains(name) || this.Details.Contains(name);
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0002330D File Offset: 0x0002150D
		internal bool GroupNameEquals(string name)
		{
			return EdmItem.IdentityComparer.Equals(this.Name, name);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00023320 File Offset: 0x00021520
		internal bool EqualsByName(Group other)
		{
			return other != null && this.GroupNameEquals(other.Name);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00023333 File Offset: 0x00021533
		public bool ShouldTranslateKeys()
		{
			return !this.Keys.Any((GroupKey g) => g is GeneratedGroupKey);
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x00023362 File Offset: 0x00021562
		internal static IEqualityComparer<Group> NameComparer
		{
			get
			{
				return Group._groupByNameComparer;
			}
		}

		// Token: 0x04000997 RID: 2455
		private readonly string _name;

		// Token: 0x04000998 RID: 2456
		private readonly ReadOnlyQdmNamedItemCollection<GroupKey> _keys;

		// Token: 0x04000999 RID: 2457
		private readonly QdmNamedItemCollection<GroupDetail> _details;

		// Token: 0x0400099A RID: 2458
		private readonly FollowingJoinBehavior _followingJoinBehavior;

		// Token: 0x0400099B RID: 2459
		private bool _isProjected = true;

		// Token: 0x0400099C RID: 2460
		private static Group.GroupByNameComparer _groupByNameComparer = new Group.GroupByNameComparer();

		// Token: 0x020002F6 RID: 758
		private sealed class GroupByNameComparer : IEqualityComparer<Group>
		{
			// Token: 0x06001D17 RID: 7447 RVA: 0x00050257 File Offset: 0x0004E457
			public bool Equals(Group x, Group y)
			{
				if (x == null)
				{
					return y == null;
				}
				return x.EqualsByName(y);
			}

			// Token: 0x06001D18 RID: 7448 RVA: 0x00050268 File Offset: 0x0004E468
			public int GetHashCode(Group obj)
			{
				if (obj == null)
				{
					return 0;
				}
				return EdmItem.IdentityComparer.GetHashCode(obj.Name);
			}
		}
	}
}
