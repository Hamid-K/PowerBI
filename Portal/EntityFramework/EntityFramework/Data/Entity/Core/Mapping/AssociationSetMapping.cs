using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200051D RID: 1309
	public class AssociationSetMapping : EntitySetBaseMapping
	{
		// Token: 0x0600406D RID: 16493 RVA: 0x000D9890 File Offset: 0x000D7A90
		public AssociationSetMapping(AssociationSet associationSet, EntitySet storeEntitySet, EntityContainerMapping containerMapping)
			: base(containerMapping)
		{
			Check.NotNull<AssociationSet>(associationSet, "associationSet");
			Check.NotNull<EntitySet>(storeEntitySet, "storeEntitySet");
			this._associationSet = associationSet;
			this._associationTypeMapping = new AssociationTypeMapping(associationSet.ElementType, this);
			this._associationTypeMapping.MappingFragment = new MappingFragment(storeEntitySet, this._associationTypeMapping, false);
		}

		// Token: 0x0600406E RID: 16494 RVA: 0x000D98ED File Offset: 0x000D7AED
		internal AssociationSetMapping(AssociationSet associationSet, EntitySet storeEntitySet)
			: this(associationSet, storeEntitySet, null)
		{
		}

		// Token: 0x0600406F RID: 16495 RVA: 0x000D98F8 File Offset: 0x000D7AF8
		internal AssociationSetMapping(AssociationSet associationSet, EntityContainerMapping containerMapping)
			: base(containerMapping)
		{
			this._associationSet = associationSet;
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06004070 RID: 16496 RVA: 0x000D9908 File Offset: 0x000D7B08
		public AssociationSet AssociationSet
		{
			get
			{
				return this._associationSet;
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06004071 RID: 16497 RVA: 0x000D9910 File Offset: 0x000D7B10
		internal override EntitySetBase Set
		{
			get
			{
				return this.AssociationSet;
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06004072 RID: 16498 RVA: 0x000D9918 File Offset: 0x000D7B18
		// (set) Token: 0x06004073 RID: 16499 RVA: 0x000D9920 File Offset: 0x000D7B20
		public AssociationTypeMapping AssociationTypeMapping
		{
			get
			{
				return this._associationTypeMapping;
			}
			internal set
			{
				this._associationTypeMapping = value;
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06004074 RID: 16500 RVA: 0x000D9929 File Offset: 0x000D7B29
		internal override IEnumerable<TypeMapping> TypeMappings
		{
			get
			{
				yield return this._associationTypeMapping;
				yield break;
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06004075 RID: 16501 RVA: 0x000D9939 File Offset: 0x000D7B39
		// (set) Token: 0x06004076 RID: 16502 RVA: 0x000D9941 File Offset: 0x000D7B41
		public AssociationSetModificationFunctionMapping ModificationFunctionMapping
		{
			get
			{
				return this._modificationFunctionMapping;
			}
			set
			{
				base.ThrowIfReadOnly();
				this._modificationFunctionMapping = value;
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06004077 RID: 16503 RVA: 0x000D9950 File Offset: 0x000D7B50
		// (set) Token: 0x06004078 RID: 16504 RVA: 0x000D9967 File Offset: 0x000D7B67
		public EntitySet StoreEntitySet
		{
			get
			{
				if (this.SingleFragment == null)
				{
					return null;
				}
				return this.SingleFragment.StoreEntitySet;
			}
			internal set
			{
				this.SingleFragment.StoreEntitySet = value;
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06004079 RID: 16505 RVA: 0x000D9975 File Offset: 0x000D7B75
		internal EntityType Table
		{
			get
			{
				if (this.StoreEntitySet == null)
				{
					return null;
				}
				return this.StoreEntitySet.ElementType;
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x0600407A RID: 16506 RVA: 0x000D998C File Offset: 0x000D7B8C
		// (set) Token: 0x0600407B RID: 16507 RVA: 0x000D99AD File Offset: 0x000D7BAD
		public EndPropertyMapping SourceEndMapping
		{
			get
			{
				if (this.SingleFragment == null)
				{
					return null;
				}
				return this.SingleFragment.PropertyMappings.OfType<EndPropertyMapping>().FirstOrDefault<EndPropertyMapping>();
			}
			set
			{
				Check.NotNull<EndPropertyMapping>(value, "value");
				base.ThrowIfReadOnly();
				this.SingleFragment.AddPropertyMapping(value);
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x0600407C RID: 16508 RVA: 0x000D99CD File Offset: 0x000D7BCD
		// (set) Token: 0x0600407D RID: 16509 RVA: 0x000D99EF File Offset: 0x000D7BEF
		public EndPropertyMapping TargetEndMapping
		{
			get
			{
				if (this.SingleFragment == null)
				{
					return null;
				}
				return this.SingleFragment.PropertyMappings.OfType<EndPropertyMapping>().ElementAtOrDefault(1);
			}
			set
			{
				Check.NotNull<EndPropertyMapping>(value, "value");
				base.ThrowIfReadOnly();
				this.SingleFragment.AddPropertyMapping(value);
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x0600407E RID: 16510 RVA: 0x000D9A0F File Offset: 0x000D7C0F
		public ReadOnlyCollection<ConditionPropertyMapping> Conditions
		{
			get
			{
				if (this.SingleFragment == null)
				{
					return new ReadOnlyCollection<ConditionPropertyMapping>(new List<ConditionPropertyMapping>());
				}
				return this.SingleFragment.Conditions;
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x0600407F RID: 16511 RVA: 0x000D9A2F File Offset: 0x000D7C2F
		private MappingFragment SingleFragment
		{
			get
			{
				if (this._associationTypeMapping == null)
				{
					return null;
				}
				return this._associationTypeMapping.MappingFragment;
			}
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x000D9A46 File Offset: 0x000D7C46
		public void AddCondition(ConditionPropertyMapping condition)
		{
			Check.NotNull<ConditionPropertyMapping>(condition, "condition");
			base.ThrowIfReadOnly();
			if (this.SingleFragment != null)
			{
				this.SingleFragment.AddCondition(condition);
			}
		}

		// Token: 0x06004081 RID: 16513 RVA: 0x000D9A6E File Offset: 0x000D7C6E
		public void RemoveCondition(ConditionPropertyMapping condition)
		{
			Check.NotNull<ConditionPropertyMapping>(condition, "condition");
			base.ThrowIfReadOnly();
			if (this.SingleFragment != null)
			{
				this.SingleFragment.RemoveCondition(condition);
			}
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x000D9A96 File Offset: 0x000D7C96
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this._associationTypeMapping);
			MappingItem.SetReadOnly(this._modificationFunctionMapping);
			base.SetReadOnly();
		}

		// Token: 0x0400166E RID: 5742
		private readonly AssociationSet _associationSet;

		// Token: 0x0400166F RID: 5743
		private AssociationTypeMapping _associationTypeMapping;

		// Token: 0x04001670 RID: 5744
		private AssociationSetModificationFunctionMapping _modificationFunctionMapping;
	}
}
