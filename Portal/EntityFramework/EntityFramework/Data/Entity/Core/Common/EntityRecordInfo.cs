using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common
{
	// Token: 0x020005ED RID: 1517
	public class EntityRecordInfo : DataRecordInfo
	{
		// Token: 0x06004A34 RID: 18996 RVA: 0x00107104 File Offset: 0x00105304
		public EntityRecordInfo(EntityType metadata, IEnumerable<EdmMember> memberInfo, EntityKey entityKey, EntitySet entitySet)
			: base(TypeUsage.Create(metadata), memberInfo)
		{
			Check.NotNull<EntityKey>(entityKey, "entityKey");
			Check.NotNull<EntitySet>(entitySet, "entitySet");
			this._entityKey = entityKey;
			this.ValidateEntityType(entitySet);
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x0010713B File Offset: 0x0010533B
		internal EntityRecordInfo(EntityType metadata, EntityKey entityKey, EntitySet entitySet)
			: base(TypeUsage.Create(metadata))
		{
			this._entityKey = entityKey;
		}

		// Token: 0x06004A36 RID: 18998 RVA: 0x00107150 File Offset: 0x00105350
		internal EntityRecordInfo(DataRecordInfo info, EntityKey entityKey, EntitySet entitySet)
			: base(info)
		{
			this._entityKey = entityKey;
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06004A37 RID: 18999 RVA: 0x00107160 File Offset: 0x00105360
		public EntityKey EntityKey
		{
			get
			{
				return this._entityKey;
			}
		}

		// Token: 0x06004A38 RID: 19000 RVA: 0x00107168 File Offset: 0x00105368
		private void ValidateEntityType(EntitySetBase entitySet)
		{
			if (this.RecordType.EdmType != null && this._entityKey != EntityKey.EntityNotValidKey && this._entityKey != EntityKey.NoEntitySetKey && this.RecordType.EdmType != entitySet.ElementType && !entitySet.ElementType.IsBaseTypeOf(this.RecordType.EdmType))
			{
				throw new ArgumentException(Strings.EntityTypesDoNotAgree);
			}
		}

		// Token: 0x04001A2B RID: 6699
		private readonly EntityKey _entityKey;
	}
}
