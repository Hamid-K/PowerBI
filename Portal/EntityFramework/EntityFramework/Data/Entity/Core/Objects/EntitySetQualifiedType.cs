using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000408 RID: 1032
	internal struct EntitySetQualifiedType : IEqualityComparer<EntitySetQualifiedType>
	{
		// Token: 0x060030EF RID: 12527 RVA: 0x0009C276 File Offset: 0x0009A476
		internal EntitySetQualifiedType(Type type, EntitySet set)
		{
			this.ClrType = EntityUtil.GetEntityIdentityType(type);
			this.EntitySet = set;
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x0009C28B File Offset: 0x0009A48B
		public bool Equals(EntitySetQualifiedType x, EntitySetQualifiedType y)
		{
			return x.ClrType == y.ClrType && x.EntitySet == y.EntitySet;
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x0009C2AB File Offset: 0x0009A4AB
		public int GetHashCode(EntitySetQualifiedType obj)
		{
			return obj.ClrType.GetHashCode() + obj.EntitySet.Name.GetHashCode() + obj.EntitySet.EntityContainer.Name.GetHashCode();
		}

		// Token: 0x04001025 RID: 4133
		internal static readonly IEqualityComparer<EntitySetQualifiedType> EqualityComparer = default(EntitySetQualifiedType);

		// Token: 0x04001026 RID: 4134
		internal readonly Type ClrType;

		// Token: 0x04001027 RID: 4135
		internal readonly EntitySet EntitySet;
	}
}
