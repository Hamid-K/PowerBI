using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000432 RID: 1074
	internal class StateManagerTypeMetadata
	{
		// Token: 0x06003431 RID: 13361 RVA: 0x000A873E File Offset: 0x000A693E
		internal StateManagerTypeMetadata()
		{
		}

		// Token: 0x06003432 RID: 13362 RVA: 0x000A8748 File Offset: 0x000A6948
		internal StateManagerTypeMetadata(EdmType edmType, ObjectTypeMapping mapping)
		{
			this._typeUsage = TypeUsage.Create(edmType);
			this._recordInfo = new DataRecordInfo(this._typeUsage);
			ReadOnlyMetadataCollection<EdmProperty> properties = TypeHelpers.GetProperties(edmType);
			this._members = new StateManagerMemberMetadata[properties.Count];
			this._objectNameToOrdinal = new Dictionary<string, int>(properties.Count);
			this._cLayerNameToOrdinal = new Dictionary<string, int>(properties.Count);
			ReadOnlyMetadataCollection<EdmMember> readOnlyMetadataCollection = null;
			if (Helper.IsEntityType(edmType))
			{
				readOnlyMetadataCollection = ((EntityType)edmType).KeyMembers;
			}
			for (int i = 0; i < this._members.Length; i++)
			{
				EdmProperty edmProperty = properties[i];
				ObjectPropertyMapping objectPropertyMapping = null;
				if (mapping != null)
				{
					objectPropertyMapping = mapping.GetPropertyMap(edmProperty.Name);
					if (objectPropertyMapping != null)
					{
						this._objectNameToOrdinal.Add(objectPropertyMapping.ClrProperty.Name, i);
					}
				}
				this._cLayerNameToOrdinal.Add(edmProperty.Name, i);
				this._members[i] = new StateManagerMemberMetadata(objectPropertyMapping, edmProperty, readOnlyMetadataCollection != null && readOnlyMetadataCollection.Contains(edmProperty));
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06003433 RID: 13363 RVA: 0x000A8842 File Offset: 0x000A6A42
		internal TypeUsage CdmMetadata
		{
			get
			{
				return this._typeUsage;
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06003434 RID: 13364 RVA: 0x000A884A File Offset: 0x000A6A4A
		internal DataRecordInfo DataRecordInfo
		{
			get
			{
				return this._recordInfo;
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06003435 RID: 13365 RVA: 0x000A8852 File Offset: 0x000A6A52
		internal virtual int FieldCount
		{
			get
			{
				return this._members.Length;
			}
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x000A885C File Offset: 0x000A6A5C
		internal Type GetFieldType(int ordinal)
		{
			return this.Member(ordinal).ClrType;
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x000A886A File Offset: 0x000A6A6A
		internal virtual StateManagerMemberMetadata Member(int ordinal)
		{
			if (ordinal < this._members.Length)
			{
				return this._members[ordinal];
			}
			throw new ArgumentOutOfRangeException("ordinal");
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06003438 RID: 13368 RVA: 0x000A888A File Offset: 0x000A6A8A
		internal IEnumerable<StateManagerMemberMetadata> Members
		{
			get
			{
				return this._members;
			}
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x000A8892 File Offset: 0x000A6A92
		internal string CLayerMemberName(int ordinal)
		{
			return this.Member(ordinal).CLayerName;
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x000A88A0 File Offset: 0x000A6AA0
		internal int GetOrdinalforOLayerMemberName(string name)
		{
			int num;
			if (string.IsNullOrEmpty(name) || !this._objectNameToOrdinal.TryGetValue(name, out num))
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x000A88C8 File Offset: 0x000A6AC8
		internal int GetOrdinalforCLayerMemberName(string name)
		{
			int num;
			if (string.IsNullOrEmpty(name) || !this._cLayerNameToOrdinal.TryGetValue(name, out num))
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x040010D9 RID: 4313
		private readonly TypeUsage _typeUsage;

		// Token: 0x040010DA RID: 4314
		private readonly StateManagerMemberMetadata[] _members;

		// Token: 0x040010DB RID: 4315
		private readonly Dictionary<string, int> _objectNameToOrdinal;

		// Token: 0x040010DC RID: 4316
		private readonly Dictionary<string, int> _cLayerNameToOrdinal;

		// Token: 0x040010DD RID: 4317
		private readonly DataRecordInfo _recordInfo;
	}
}
