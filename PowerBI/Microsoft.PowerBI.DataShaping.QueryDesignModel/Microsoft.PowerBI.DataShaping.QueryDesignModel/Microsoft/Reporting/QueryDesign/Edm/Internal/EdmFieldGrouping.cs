using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001F8 RID: 504
	public sealed class EdmFieldGrouping
	{
		// Token: 0x060017E8 RID: 6120 RVA: 0x00042039 File Offset: 0x00040239
		internal EdmFieldGrouping(EdmField field, string modelGroupingBehavior)
		{
			this._field = field;
			this._modelGroupingBehavior = modelGroupingBehavior;
			this._fieldsWithThisAsIdentity = new List<EdmField>();
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x0004205A File Offset: 0x0004025A
		internal GroupingIdentity Identity
		{
			get
			{
				return this._identity.Value;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x00042067 File Offset: 0x00040267
		internal ReadOnlyCollection<EdmField> IdentityFields
		{
			get
			{
				return this._identityFields;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x0004206F File Offset: 0x0004026F
		internal bool IsIdentityOnEntityKey
		{
			get
			{
				return this.Identity == GroupingIdentity.EntityKey;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x0004207A File Offset: 0x0004027A
		internal ReadOnlyCollection<EdmField> QueryGroupFields
		{
			get
			{
				return this._queryGroupFields;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x00042082 File Offset: 0x00040282
		internal ReadOnlyCollection<EdmField> GroupByFields
		{
			get
			{
				return this._modelGroupBy;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x0004208A File Offset: 0x0004028A
		internal IList<EdmField> FieldsWithThisAsIdentity
		{
			get
			{
				return this._fieldsWithThisAsIdentity;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x00042092 File Offset: 0x00040292
		internal bool IsQueryGroupOnEntityKey
		{
			get
			{
				return this._queryGroupFieldsContainEntityKeys.Value;
			}
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x000420A0 File Offset: 0x000402A0
		internal void InitializeModelGroupBy(ReadOnlyCollection<EdmField> groupBy)
		{
			EntityType entityType = this._field.DeclaringType as EntityType;
			Contracts.CheckParam(this._modelGroupingBehavior != "GroupOnEntityKey" || groupBy == null || !groupBy.Any<EdmField>() || entityType.KeyFields.IsSupersetOf(groupBy), "groupBy", DevErrors.EdmField.GroupByIsNotKeysWhenGroupOnEntityKey(entityType.Name, this._field.Name));
			this._modelGroupBy = groupBy;
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00042114 File Offset: 0x00040314
		internal void CompleteInitialization()
		{
			EntityType entityType = this._field.DeclaringType as EntityType;
			GroupingIdentity groupingIdentity = EdmFieldGrouping.CalculateDefaultIdentity(entityType, this._field, this._modelGroupingBehavior);
			this._identityFields = EdmFieldGrouping.CalculateIdentityFields(entityType, this._field, groupingIdentity, this._modelGroupBy);
			this._identity = new GroupingIdentity?(EdmFieldGrouping.CalculateIdentity(entityType, this._field, groupingIdentity, this._identityFields));
			GroupingIdentity? identity = this._identity;
			GroupingIdentity groupingIdentity2 = GroupingIdentity.EntityKey;
			if (!((identity.GetValueOrDefault() == groupingIdentity2) & (identity != null)))
			{
				this._queryGroupFields = EdmFieldGrouping.CalculateQueryGroupFields(this._field, this._identityFields, this._field.OrderByFields);
				this._queryGroupFieldsContainEntityKeys = new bool?(this._queryGroupFields.IsSupersetOf(entityType.KeyFields));
				this.UpdateReverseQueryGroupFieldReferences(this._queryGroupFields);
				if (!this._queryGroupFieldsContainEntityKeys.GetValueOrDefault())
				{
					this.UpdateReverseQueryGroupFieldReferences(entityType.KeyFields);
				}
				return;
			}
			this._identityFields = entityType.KeyFields;
			this._queryGroupFields = this._identityFields;
			if (!entityType.IsKeyStable())
			{
				this._queryGroupFields = Util.EmptyReadOnlyCollection<EdmField>();
				this._queryGroupFieldsContainEntityKeys = new bool?(false);
				return;
			}
			this._queryGroupFieldsContainEntityKeys = new bool?(true);
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x00042241 File Offset: 0x00040441
		private static GroupingIdentity CalculateDefaultIdentity(EntityType entityType, EdmField field, string modelGroupingBehavior)
		{
			if (!field.CanGroupOnValue())
			{
				return GroupingIdentity.EntityKey;
			}
			if (!entityType.IsKeyStable())
			{
				return GroupingIdentity.Value;
			}
			if (entityType.IsKeyStable() && (field.IsSingleDisplayKey() || field.IsDefaultImage()))
			{
				return GroupingIdentity.EntityKey;
			}
			if (modelGroupingBehavior == "GroupOnEntityKey")
			{
				return GroupingIdentity.EntityKey;
			}
			return GroupingIdentity.Value;
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00042281 File Offset: 0x00040481
		private static ReadOnlyCollection<EdmField> CalculateIdentityFields(EntityType entityType, EdmField field, GroupingIdentity defaultIdentity, ReadOnlyCollection<EdmField> modelGroupBy)
		{
			if (defaultIdentity == GroupingIdentity.EntityKey)
			{
				return entityType.KeyFields;
			}
			if (modelGroupBy != null && modelGroupBy.Count > 0)
			{
				return modelGroupBy;
			}
			return new ReadOnlyCollection<EdmField>(new EdmField[] { field });
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x000422AB File Offset: 0x000404AB
		private static GroupingIdentity CalculateIdentity(EntityType entityType, EdmField field, GroupingIdentity defaultIdentity, ReadOnlyCollection<EdmField> identityFields)
		{
			if (field.IsSingleKey() || entityType.KeyFields.ToSet<EdmField>().SetEquals(identityFields))
			{
				return GroupingIdentity.EntityKey;
			}
			if (identityFields.Count != 1 || identityFields[0] != field)
			{
				return GroupingIdentity.Fields;
			}
			return defaultIdentity;
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x000422E0 File Offset: 0x000404E0
		private static ReadOnlyCollection<EdmField> CalculateQueryGroupFields(EdmField field, ReadOnlyCollection<EdmField> identityFields, ReadOnlyCollection<EdmField> modelOrderBy)
		{
			ReadOnlyCollection<EdmField> readOnlyCollection = identityFields;
			IEnumerable<EdmField> enumerable = null;
			if (modelOrderBy.Count > 0)
			{
				enumerable = modelOrderBy;
				if (identityFields.Concat(new EdmField[] { field }).IsSupersetOf(enumerable))
				{
					enumerable = null;
				}
			}
			if (enumerable != null)
			{
				readOnlyCollection = readOnlyCollection.Union(enumerable).ToReadOnlyCollection<EdmField>();
			}
			return readOnlyCollection;
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x00042328 File Offset: 0x00040528
		private void UpdateReverseQueryGroupFieldReferences(ReadOnlyCollection<EdmField> queryGroupFields)
		{
			foreach (EdmField edmField in queryGroupFields)
			{
				edmField.Grouping._fieldsWithThisAsIdentity.Add(this._field);
			}
		}

		// Token: 0x04000CD6 RID: 3286
		private const string GroupOnEntityKey = "GroupOnEntityKey";

		// Token: 0x04000CD7 RID: 3287
		private readonly EdmField _field;

		// Token: 0x04000CD8 RID: 3288
		private readonly string _modelGroupingBehavior;

		// Token: 0x04000CD9 RID: 3289
		private GroupingIdentity? _identity;

		// Token: 0x04000CDA RID: 3290
		private ReadOnlyCollection<EdmField> _modelGroupBy;

		// Token: 0x04000CDB RID: 3291
		private ReadOnlyCollection<EdmField> _identityFields;

		// Token: 0x04000CDC RID: 3292
		private ReadOnlyCollection<EdmField> _queryGroupFields;

		// Token: 0x04000CDD RID: 3293
		private IList<EdmField> _fieldsWithThisAsIdentity;

		// Token: 0x04000CDE RID: 3294
		private bool? _queryGroupFieldsContainEntityKeys;
	}
}
