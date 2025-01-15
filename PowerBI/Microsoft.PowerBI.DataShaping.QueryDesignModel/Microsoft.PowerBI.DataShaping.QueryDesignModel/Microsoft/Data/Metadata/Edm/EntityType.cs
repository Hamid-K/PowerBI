using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x0200008A RID: 138
	public class EntityType : EntityTypeBase
	{
		// Token: 0x06000A1E RID: 2590 RVA: 0x00017FCB File Offset: 0x000161CB
		internal EntityType(string name, string namespaceName, DataSpace dataSpace)
			: base(name, namespaceName, dataSpace)
		{
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00017FE1 File Offset: 0x000161E1
		internal EntityType(string name, string namespaceName, DataSpace dataSpace, IEnumerable<string> keyMemberNames, IEnumerable<EdmMember> members)
			: base(name, namespaceName, dataSpace)
		{
			if (members != null)
			{
				EntityTypeBase.CheckAndAddMembers(members, this);
			}
			if (keyMemberNames != null)
			{
				base.CheckAndAddKeyMembers(keyMemberNames);
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0001800F File Offset: 0x0001620F
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EntityType;
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00018013 File Offset: 0x00016213
		internal override void ValidateMemberForAdd(EdmMember member)
		{
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00018015 File Offset: 0x00016215
		internal bool TryGetMemberSql(EdmMember member, out string sql)
		{
			sql = null;
			return this._memberSql != null && this._memberSql.TryGetValue(member, out sql);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00018034 File Offset: 0x00016234
		internal void SetMemberSql(EdmMember member, string sql)
		{
			object memberSqlLock = this._memberSqlLock;
			lock (memberSqlLock)
			{
				if (this._memberSql == null)
				{
					this._memberSql = new Dictionary<EdmMember, string>();
				}
				this._memberSql[member] = sql;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00018090 File Offset: 0x00016290
		public ReadOnlyMetadataCollection<NavigationProperty> NavigationProperties
		{
			get
			{
				ReadOnlyMetadataCollection<EdmMember> members = base.Members;
				Predicate<EdmMember> predicate;
				if ((predicate = EntityType.<>O.<0>__IsNavigationProperty) == null)
				{
					predicate = (EntityType.<>O.<0>__IsNavigationProperty = new Predicate<EdmMember>(Helper.IsNavigationProperty));
				}
				return new FilteredReadOnlyMetadataCollection<NavigationProperty, EdmMember>(members, predicate);
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x000180B8 File Offset: 0x000162B8
		public ReadOnlyMetadataCollection<EdmProperty> Properties
		{
			get
			{
				if (this._properties == null)
				{
					ReadOnlyMetadataCollection<EdmMember> members = base.Members;
					Predicate<EdmMember> predicate;
					if ((predicate = EntityType.<>O.<1>__IsEdmProperty) == null)
					{
						predicate = (EntityType.<>O.<1>__IsEdmProperty = new Predicate<EdmMember>(Helper.IsEdmProperty));
					}
					Interlocked.CompareExchange<ReadOnlyMetadataCollection<EdmProperty>>(ref this._properties, new FilteredReadOnlyMetadataCollection<EdmProperty, EdmMember>(members, predicate), null);
				}
				return this._properties;
			}
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00018106 File Offset: 0x00016306
		public RefType GetReferenceType()
		{
			if (this._referenceType == null)
			{
				Interlocked.CompareExchange<RefType>(ref this._referenceType, new RefType(this), null);
			}
			return this._referenceType;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0001812C File Offset: 0x0001632C
		internal bool TryGetNavigationProperty(string relationshipType, string fromName, string toName, out NavigationProperty navigationProperty)
		{
			foreach (NavigationProperty navigationProperty2 in this.NavigationProperties)
			{
				if (navigationProperty2.RelationshipType.FullName == relationshipType && navigationProperty2.FromEndMember.Name == fromName && navigationProperty2.ToEndMember.Name == toName)
				{
					navigationProperty = navigationProperty2;
					return true;
				}
			}
			navigationProperty = null;
			return false;
		}

		// Token: 0x04000827 RID: 2087
		private RefType _referenceType;

		// Token: 0x04000828 RID: 2088
		private ReadOnlyMetadataCollection<EdmProperty> _properties;

		// Token: 0x04000829 RID: 2089
		private Dictionary<EdmMember, string> _memberSql;

		// Token: 0x0400082A RID: 2090
		private object _memberSqlLock = new object();

		// Token: 0x020002BB RID: 699
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000FBE RID: 4030
			public static Predicate<EdmMember> <0>__IsNavigationProperty;

			// Token: 0x04000FBF RID: 4031
			public static Predicate<EdmMember> <1>__IsEdmProperty;
		}
	}
}
