using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005C5 RID: 1477
	internal class ExtractorMetadata
	{
		// Token: 0x06004761 RID: 18273 RVA: 0x000FC884 File Offset: 0x000FAA84
		internal ExtractorMetadata(EntitySetBase entitySetBase, StructuralType type, UpdateTranslator translator)
		{
			this.m_type = type;
			this.m_translator = translator;
			EntityType entityType = null;
			BuiltInTypeKind builtInTypeKind = type.BuiltInTypeKind;
			Set<EdmMember> set;
			Set<EdmMember> set2;
			if (builtInTypeKind != BuiltInTypeKind.EntityType)
			{
				if (builtInTypeKind == BuiltInTypeKind.RowType)
				{
					set = new Set<EdmMember>(((RowType)type).Properties).MakeReadOnly();
					set2 = Set<EdmMember>.Empty;
				}
				else
				{
					set = Set<EdmMember>.Empty;
					set2 = Set<EdmMember>.Empty;
				}
			}
			else
			{
				entityType = (EntityType)type;
				set = new Set<EdmMember>(entityType.KeyMembers).MakeReadOnly();
				set2 = new Set<EdmMember>(((EntitySet)entitySetBase).ForeignKeyDependents.SelectMany((Tuple<AssociationSet, ReferentialConstraint> fk) => fk.Item2.ToProperties)).MakeReadOnly();
			}
			IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(type);
			this.m_memberMap = new ExtractorMetadata.MemberInformation[allStructuralMembers.Count];
			for (int i = 0; i < allStructuralMembers.Count; i++)
			{
				EdmMember edmMember = allStructuralMembers[i];
				PropagatorFlags propagatorFlags = PropagatorFlags.NoFlags;
				int? num = null;
				if (set.Contains(edmMember))
				{
					propagatorFlags |= PropagatorFlags.Key;
					if (entityType != null)
					{
						num = new int?(entityType.KeyMembers.IndexOf(edmMember));
					}
				}
				if (set2.Contains(edmMember))
				{
					propagatorFlags |= PropagatorFlags.ForeignKey;
				}
				if (MetadataHelper.GetConcurrencyMode(edmMember) == ConcurrencyMode.Fixed)
				{
					propagatorFlags |= PropagatorFlags.ConcurrencyValue;
				}
				bool flag = this.m_translator.ViewLoader.IsServerGen(entitySetBase, this.m_translator.MetadataWorkspace, edmMember);
				bool flag2 = this.m_translator.ViewLoader.IsNullConditionMember(entitySetBase, this.m_translator.MetadataWorkspace, edmMember);
				this.m_memberMap[i] = new ExtractorMetadata.MemberInformation(i, num, propagatorFlags, edmMember, flag, flag2);
			}
		}

		// Token: 0x06004762 RID: 18274 RVA: 0x000FCA24 File Offset: 0x000FAC24
		internal PropagatorResult RetrieveMember(IEntityStateEntry stateEntry, IExtendedDataRecord record, bool useCurrentValues, EntityKey key, int ordinal, ModifiedPropertiesBehavior modifiedPropertiesBehavior)
		{
			ExtractorMetadata.MemberInformation memberInformation = this.m_memberMap[ordinal];
			int num;
			if (memberInformation.IsKeyMember)
			{
				int value = memberInformation.EntityKeyOrdinal.Value;
				num = this.m_translator.KeyManager.GetKeyIdentifierForMemberOffset(key, value, ((EntityType)this.m_type).KeyMembers.Count);
			}
			else if (memberInformation.IsForeignKeyMember)
			{
				num = this.m_translator.KeyManager.GetKeyIdentifierForMember(key, record.GetName(ordinal), useCurrentValues);
			}
			else
			{
				num = -1;
			}
			bool flag = modifiedPropertiesBehavior == ModifiedPropertiesBehavior.AllModified || (modifiedPropertiesBehavior == ModifiedPropertiesBehavior.SomeModified && stateEntry.ModifiedProperties != null && stateEntry.ModifiedProperties[memberInformation.Ordinal]);
			if (memberInformation.CheckIsNotNull && record.IsDBNull(ordinal))
			{
				throw EntityUtil.Update(Strings.Update_NullValue(record.GetName(ordinal)), null, new IEntityStateEntry[] { stateEntry });
			}
			object value2 = record.GetValue(ordinal);
			EntityKey entityKey = value2 as EntityKey;
			if (entityKey != null)
			{
				return this.CreateEntityKeyResult(stateEntry, entityKey);
			}
			IExtendedDataRecord extendedDataRecord = value2 as IExtendedDataRecord;
			if (extendedDataRecord != null)
			{
				ModifiedPropertiesBehavior modifiedPropertiesBehavior2 = (flag ? ModifiedPropertiesBehavior.AllModified : ModifiedPropertiesBehavior.NoneModified);
				UpdateTranslator translator = this.m_translator;
				return ExtractorMetadata.ExtractResultFromRecord(stateEntry, flag, extendedDataRecord, useCurrentValues, translator, modifiedPropertiesBehavior2);
			}
			return this.CreateSimpleResult(stateEntry, record, memberInformation, num, flag, ordinal, value2);
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x000FCB58 File Offset: 0x000FAD58
		private PropagatorResult CreateEntityKeyResult(IEntityStateEntry stateEntry, EntityKey entityKey)
		{
			RowType keyRowType = entityKey.GetEntitySet(this.m_translator.MetadataWorkspace).ElementType.GetKeyRowType();
			ExtractorMetadata extractorMetadata = this.m_translator.GetExtractorMetadata(stateEntry.EntitySet, keyRowType);
			PropagatorResult[] array = new PropagatorResult[keyRowType.Properties.Count];
			for (int i = 0; i < keyRowType.Properties.Count; i++)
			{
				EdmMember edmMember = keyRowType.Properties[i];
				ExtractorMetadata.MemberInformation memberInformation = extractorMetadata.m_memberMap[i];
				int keyIdentifierForMemberOffset = this.m_translator.KeyManager.GetKeyIdentifierForMemberOffset(entityKey, i, keyRowType.Properties.Count);
				object obj;
				if (entityKey.IsTemporary)
				{
					obj = stateEntry.StateManager.GetEntityStateEntry(entityKey).CurrentValues[edmMember.Name];
				}
				else
				{
					obj = entityKey.FindValueByName(edmMember.Name);
				}
				array[i] = PropagatorResult.CreateKeyValue(memberInformation.Flags, obj, stateEntry, keyIdentifierForMemberOffset);
			}
			return PropagatorResult.CreateStructuralValue(array, extractorMetadata.m_type, false);
		}

		// Token: 0x06004764 RID: 18276 RVA: 0x000FCC54 File Offset: 0x000FAE54
		private PropagatorResult CreateSimpleResult(IEntityStateEntry stateEntry, IExtendedDataRecord record, ExtractorMetadata.MemberInformation memberInformation, int identifier, bool isModified, int recordOrdinal, object value)
		{
			CurrentValueRecord currentValueRecord = record as CurrentValueRecord;
			PropagatorFlags propagatorFlags = memberInformation.Flags;
			if (!isModified)
			{
				propagatorFlags |= PropagatorFlags.Preserve;
			}
			if (-1 != identifier)
			{
				PropagatorResult propagatorResult;
				if ((memberInformation.IsServerGenerated || memberInformation.IsForeignKeyMember) && currentValueRecord != null)
				{
					propagatorResult = PropagatorResult.CreateServerGenKeyValue(propagatorFlags, value, stateEntry, identifier, recordOrdinal);
				}
				else
				{
					propagatorResult = PropagatorResult.CreateKeyValue(propagatorFlags, value, stateEntry, identifier);
				}
				this.m_translator.KeyManager.RegisterIdentifierOwner(propagatorResult);
				return propagatorResult;
			}
			if ((memberInformation.IsServerGenerated || memberInformation.IsForeignKeyMember) && currentValueRecord != null)
			{
				return PropagatorResult.CreateServerGenSimpleValue(propagatorFlags, value, currentValueRecord, recordOrdinal);
			}
			return PropagatorResult.CreateSimpleValue(propagatorFlags, value);
		}

		// Token: 0x06004765 RID: 18277 RVA: 0x000FCCE8 File Offset: 0x000FAEE8
		internal static PropagatorResult ExtractResultFromRecord(IEntityStateEntry stateEntry, bool isModified, IExtendedDataRecord record, bool useCurrentValues, UpdateTranslator translator, ModifiedPropertiesBehavior modifiedPropertiesBehavior)
		{
			StructuralType structuralType = (StructuralType)record.DataRecordInfo.RecordType.EdmType;
			ExtractorMetadata extractorMetadata = translator.GetExtractorMetadata(stateEntry.EntitySet, structuralType);
			EntityKey entityKey = stateEntry.EntityKey;
			PropagatorResult[] array = new PropagatorResult[record.FieldCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = extractorMetadata.RetrieveMember(stateEntry, record, useCurrentValues, entityKey, i, modifiedPropertiesBehavior);
			}
			return PropagatorResult.CreateStructuralValue(array, structuralType, isModified);
		}

		// Token: 0x0400195C RID: 6492
		private readonly ExtractorMetadata.MemberInformation[] m_memberMap;

		// Token: 0x0400195D RID: 6493
		private readonly StructuralType m_type;

		// Token: 0x0400195E RID: 6494
		private readonly UpdateTranslator m_translator;

		// Token: 0x02000BF3 RID: 3059
		private class MemberInformation
		{
			// Token: 0x17001130 RID: 4400
			// (get) Token: 0x060068A1 RID: 26785 RVA: 0x00164EDB File Offset: 0x001630DB
			internal bool IsKeyMember
			{
				get
				{
					return PropagatorFlags.Key == (this.Flags & PropagatorFlags.Key);
				}
			}

			// Token: 0x17001131 RID: 4401
			// (get) Token: 0x060068A2 RID: 26786 RVA: 0x00164EEA File Offset: 0x001630EA
			internal bool IsForeignKeyMember
			{
				get
				{
					return PropagatorFlags.ForeignKey == (this.Flags & PropagatorFlags.ForeignKey);
				}
			}

			// Token: 0x060068A3 RID: 26787 RVA: 0x00164EFC File Offset: 0x001630FC
			internal MemberInformation(int ordinal, int? entityKeyOrdinal, PropagatorFlags flags, EdmMember member, bool isServerGenerated, bool isNullConditionMember)
			{
				this.Ordinal = ordinal;
				this.EntityKeyOrdinal = entityKeyOrdinal;
				this.Flags = flags;
				this.Member = member;
				this.IsServerGenerated = isServerGenerated;
				this.CheckIsNotNull = !TypeSemantics.IsNullable(member) && (isNullConditionMember || member.TypeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.ComplexType);
			}

			// Token: 0x04002F41 RID: 12097
			internal readonly int Ordinal;

			// Token: 0x04002F42 RID: 12098
			internal readonly int? EntityKeyOrdinal;

			// Token: 0x04002F43 RID: 12099
			internal readonly PropagatorFlags Flags;

			// Token: 0x04002F44 RID: 12100
			internal readonly bool IsServerGenerated;

			// Token: 0x04002F45 RID: 12101
			internal readonly bool CheckIsNotNull;

			// Token: 0x04002F46 RID: 12102
			internal readonly EdmMember Member;
		}
	}
}
