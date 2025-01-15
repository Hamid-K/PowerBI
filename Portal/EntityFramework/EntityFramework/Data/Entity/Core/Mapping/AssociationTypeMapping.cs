using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200051F RID: 1311
	public class AssociationTypeMapping : TypeMapping
	{
		// Token: 0x06004089 RID: 16521 RVA: 0x000D9B65 File Offset: 0x000D7D65
		public AssociationTypeMapping(AssociationSetMapping associationSetMapping)
		{
			Check.NotNull<AssociationSetMapping>(associationSetMapping, "associationSetMapping");
			this._associationSetMapping = associationSetMapping;
			this.m_relation = associationSetMapping.AssociationSet.ElementType;
		}

		// Token: 0x0600408A RID: 16522 RVA: 0x000D9B91 File Offset: 0x000D7D91
		internal AssociationTypeMapping(AssociationType relation, AssociationSetMapping associationSetMapping)
		{
			this._associationSetMapping = associationSetMapping;
			this.m_relation = relation;
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x0600408B RID: 16523 RVA: 0x000D9BA7 File Offset: 0x000D7DA7
		public AssociationSetMapping AssociationSetMapping
		{
			get
			{
				return this._associationSetMapping;
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x0600408C RID: 16524 RVA: 0x000D9BAF File Offset: 0x000D7DAF
		internal override EntitySetBaseMapping SetMapping
		{
			get
			{
				return this.AssociationSetMapping;
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x0600408D RID: 16525 RVA: 0x000D9BB7 File Offset: 0x000D7DB7
		public AssociationType AssociationType
		{
			get
			{
				return this.m_relation;
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x0600408E RID: 16526 RVA: 0x000D9BBF File Offset: 0x000D7DBF
		// (set) Token: 0x0600408F RID: 16527 RVA: 0x000D9BC7 File Offset: 0x000D7DC7
		public MappingFragment MappingFragment
		{
			get
			{
				return this._mappingFragment;
			}
			internal set
			{
				this._mappingFragment = value;
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06004090 RID: 16528 RVA: 0x000D9BD0 File Offset: 0x000D7DD0
		internal override ReadOnlyCollection<MappingFragment> MappingFragments
		{
			get
			{
				if (this._mappingFragment != null)
				{
					return new ReadOnlyCollection<MappingFragment>(new MappingFragment[] { this._mappingFragment });
				}
				return new ReadOnlyCollection<MappingFragment>(new MappingFragment[0]);
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06004091 RID: 16529 RVA: 0x000D9BFA File Offset: 0x000D7DFA
		internal override ReadOnlyCollection<EntityTypeBase> Types
		{
			get
			{
				return new ReadOnlyCollection<EntityTypeBase>(new AssociationType[] { this.m_relation });
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06004092 RID: 16530 RVA: 0x000D9C10 File Offset: 0x000D7E10
		internal override ReadOnlyCollection<EntityTypeBase> IsOfTypes
		{
			get
			{
				return new ReadOnlyCollection<EntityTypeBase>(new List<EntityTypeBase>());
			}
		}

		// Token: 0x06004093 RID: 16531 RVA: 0x000D9C1C File Offset: 0x000D7E1C
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this._mappingFragment);
			base.SetReadOnly();
		}

		// Token: 0x04001674 RID: 5748
		private readonly AssociationSetMapping _associationSetMapping;

		// Token: 0x04001675 RID: 5749
		private MappingFragment _mappingFragment;

		// Token: 0x04001676 RID: 5750
		private readonly AssociationType m_relation;
	}
}
