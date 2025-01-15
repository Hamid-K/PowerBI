using System;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000110 RID: 272
	internal class NavigationEntryMetadata : MemberEntryMetadata
	{
		// Token: 0x06001330 RID: 4912 RVA: 0x00032570 File Offset: 0x00030770
		public NavigationEntryMetadata(Type declaringType, Type propertyType, string propertyName, bool isCollection)
			: base(declaringType, propertyType, propertyName)
		{
			this._isCollection = isCollection;
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x00032583 File Offset: 0x00030783
		public override MemberEntryType MemberEntryType
		{
			get
			{
				if (!this._isCollection)
				{
					return MemberEntryType.ReferenceNavigationProperty;
				}
				return MemberEntryType.CollectionNavigationProperty;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x00032590 File Offset: 0x00030790
		public override Type MemberType
		{
			get
			{
				if (!this._isCollection)
				{
					return base.ElementType;
				}
				return DbHelpers.CollectionType(base.ElementType);
			}
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x000325AC File Offset: 0x000307AC
		public override InternalMemberEntry CreateMemberEntry(InternalEntityEntry internalEntityEntry, InternalPropertyEntry parentPropertyEntry)
		{
			if (!this._isCollection)
			{
				return new InternalReferenceEntry(internalEntityEntry, this);
			}
			return new InternalCollectionEntry(internalEntityEntry, this);
		}

		// Token: 0x0400094B RID: 2379
		private readonly bool _isCollection;
	}
}
