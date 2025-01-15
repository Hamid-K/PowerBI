using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000558 RID: 1368
	internal class ObjectTypeMapping : MappingBase
	{
		// Token: 0x060042CD RID: 17101 RVA: 0x000E5AF0 File Offset: 0x000E3CF0
		internal ObjectTypeMapping(EdmType clrType, EdmType cdmType)
		{
			this.m_clrType = clrType;
			this.m_cdmType = cdmType;
			this.identity = clrType.Identity + ":" + cdmType.Identity;
			if (Helper.IsStructuralType(cdmType))
			{
				this.m_memberMapping = new Dictionary<string, ObjectMemberMapping>(((StructuralType)cdmType).Members.Count);
				return;
			}
			this.m_memberMapping = ObjectTypeMapping.EmptyMemberMapping;
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x060042CE RID: 17102 RVA: 0x000E5B5C File Offset: 0x000E3D5C
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.MetadataItem;
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x060042CF RID: 17103 RVA: 0x000E5B60 File Offset: 0x000E3D60
		internal EdmType ClrType
		{
			get
			{
				return this.m_clrType;
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x060042D0 RID: 17104 RVA: 0x000E5B68 File Offset: 0x000E3D68
		internal override MetadataItem EdmItem
		{
			get
			{
				return this.EdmType;
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x060042D1 RID: 17105 RVA: 0x000E5B70 File Offset: 0x000E3D70
		internal EdmType EdmType
		{
			get
			{
				return this.m_cdmType;
			}
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x060042D2 RID: 17106 RVA: 0x000E5B78 File Offset: 0x000E3D78
		internal override string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x000E5B80 File Offset: 0x000E3D80
		internal ObjectPropertyMapping GetPropertyMap(string propertyName)
		{
			ObjectMemberMapping memberMap = this.GetMemberMap(propertyName, false);
			if (memberMap != null && (memberMap.MemberMappingKind == MemberMappingKind.ScalarPropertyMapping || memberMap.MemberMappingKind == MemberMappingKind.ComplexPropertyMapping))
			{
				return (ObjectPropertyMapping)memberMap;
			}
			return null;
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x000E5BB2 File Offset: 0x000E3DB2
		internal void AddMemberMap(ObjectMemberMapping memberMapping)
		{
			this.m_memberMapping.Add(memberMapping.EdmMember.Name, memberMapping);
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x000E5BCB File Offset: 0x000E3DCB
		internal ObjectMemberMapping GetMemberMapForClrMember(string clrMemberName, bool ignoreCase)
		{
			return this.GetMemberMap(clrMemberName, ignoreCase);
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x000E5BD8 File Offset: 0x000E3DD8
		private ObjectMemberMapping GetMemberMap(string propertyName, bool ignoreCase)
		{
			Check.NotEmpty(propertyName, "propertyName");
			ObjectMemberMapping objectMemberMapping = null;
			if (!ignoreCase)
			{
				this.m_memberMapping.TryGetValue(propertyName, out objectMemberMapping);
			}
			else
			{
				foreach (KeyValuePair<string, ObjectMemberMapping> keyValuePair in this.m_memberMapping)
				{
					if (keyValuePair.Key.Equals(propertyName, StringComparison.OrdinalIgnoreCase))
					{
						if (objectMemberMapping != null)
						{
							throw new MappingException(Strings.Mapping_Duplicate_PropertyMap_CaseInsensitive(propertyName));
						}
						objectMemberMapping = keyValuePair.Value;
					}
				}
			}
			return objectMemberMapping;
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x000E5C70 File Offset: 0x000E3E70
		public override string ToString()
		{
			return this.Identity;
		}

		// Token: 0x040017DF RID: 6111
		private readonly EdmType m_clrType;

		// Token: 0x040017E0 RID: 6112
		private readonly EdmType m_cdmType;

		// Token: 0x040017E1 RID: 6113
		private readonly string identity;

		// Token: 0x040017E2 RID: 6114
		private readonly Dictionary<string, ObjectMemberMapping> m_memberMapping;

		// Token: 0x040017E3 RID: 6115
		private static readonly Dictionary<string, ObjectMemberMapping> EmptyMemberMapping = new Dictionary<string, ObjectMemberMapping>(0);
	}
}
