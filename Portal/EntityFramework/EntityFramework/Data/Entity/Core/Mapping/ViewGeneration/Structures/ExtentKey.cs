using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005B6 RID: 1462
	internal class ExtentKey : InternalBase
	{
		// Token: 0x060046FF RID: 18175 RVA: 0x000FAE91 File Offset: 0x000F9091
		internal ExtentKey(IEnumerable<MemberPath> keyFields)
		{
			this.m_keyFields = new List<MemberPath>(keyFields);
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06004700 RID: 18176 RVA: 0x000FAEA5 File Offset: 0x000F90A5
		internal IEnumerable<MemberPath> KeyFields
		{
			get
			{
				return this.m_keyFields;
			}
		}

		// Token: 0x06004701 RID: 18177 RVA: 0x000FAEB0 File Offset: 0x000F90B0
		internal static List<ExtentKey> GetKeysForEntityType(MemberPath prefix, EntityType entityType)
		{
			ExtentKey primaryKeyForEntityType = ExtentKey.GetPrimaryKeyForEntityType(prefix, entityType);
			return new List<ExtentKey> { primaryKeyForEntityType };
		}

		// Token: 0x06004702 RID: 18178 RVA: 0x000FAED4 File Offset: 0x000F90D4
		internal static ExtentKey GetPrimaryKeyForEntityType(MemberPath prefix, EntityType entityType)
		{
			List<MemberPath> list = new List<MemberPath>();
			foreach (EdmMember edmMember in entityType.KeyMembers)
			{
				list.Add(new MemberPath(prefix, edmMember));
			}
			return new ExtentKey(list);
		}

		// Token: 0x06004703 RID: 18179 RVA: 0x000FAF3C File Offset: 0x000F913C
		internal static ExtentKey GetKeyForRelationType(MemberPath prefix, AssociationType relationType)
		{
			List<MemberPath> list = new List<MemberPath>();
			foreach (AssociationEndMember associationEndMember in relationType.AssociationEndMembers)
			{
				MemberPath memberPath = new MemberPath(prefix, associationEndMember);
				EntityType entityTypeForEnd = MetadataHelper.GetEntityTypeForEnd(associationEndMember);
				ExtentKey primaryKeyForEntityType = ExtentKey.GetPrimaryKeyForEntityType(memberPath, entityTypeForEnd);
				list.AddRange(primaryKeyForEntityType.KeyFields);
			}
			return new ExtentKey(list);
		}

		// Token: 0x06004704 RID: 18180 RVA: 0x000FAFB8 File Offset: 0x000F91B8
		internal string ToUserString()
		{
			return StringUtil.ToCommaSeparatedStringSorted(this.m_keyFields);
		}

		// Token: 0x06004705 RID: 18181 RVA: 0x000FAFC5 File Offset: 0x000F91C5
		internal override void ToCompactString(StringBuilder builder)
		{
			StringUtil.ToCommaSeparatedStringSorted(builder, this.m_keyFields);
		}

		// Token: 0x04001934 RID: 6452
		private readonly List<MemberPath> m_keyFields;
	}
}
