using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005CE RID: 1486
	internal abstract class PropagatorResult
	{
		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x060047A4 RID: 18340
		internal abstract bool IsNull { get; }

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x060047A5 RID: 18341
		internal abstract bool IsSimple { get; }

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x060047A6 RID: 18342 RVA: 0x000FE210 File Offset: 0x000FC410
		internal virtual PropagatorFlags PropagatorFlags
		{
			get
			{
				return PropagatorFlags.NoFlags;
			}
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x060047A7 RID: 18343 RVA: 0x000FE213 File Offset: 0x000FC413
		internal virtual IEntityStateEntry StateEntry
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x060047A8 RID: 18344 RVA: 0x000FE216 File Offset: 0x000FC416
		internal virtual CurrentValueRecord Record
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x060047A9 RID: 18345 RVA: 0x000FE219 File Offset: 0x000FC419
		internal virtual StructuralType StructuralType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x060047AA RID: 18346 RVA: 0x000FE21C File Offset: 0x000FC41C
		internal virtual int RecordOrdinal
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x060047AB RID: 18347 RVA: 0x000FE21F File Offset: 0x000FC41F
		internal virtual int Identifier
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x060047AC RID: 18348 RVA: 0x000FE222 File Offset: 0x000FC422
		internal virtual PropagatorResult Next
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060047AD RID: 18349 RVA: 0x000FE225 File Offset: 0x000FC425
		internal virtual object GetSimpleValue()
		{
			throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UpdatePipelineResultRequestInvalid, 0, "PropagatorResult.GetSimpleValue");
		}

		// Token: 0x060047AE RID: 18350 RVA: 0x000FE237 File Offset: 0x000FC437
		internal virtual PropagatorResult GetMemberValue(int ordinal)
		{
			throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UpdatePipelineResultRequestInvalid, 0, "PropagatorResult.GetMemberValue");
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x000FE24C File Offset: 0x000FC44C
		internal PropagatorResult GetMemberValue(EdmMember member)
		{
			int num = TypeHelpers.GetAllStructuralMembers(this.StructuralType).IndexOf(member);
			return this.GetMemberValue(num);
		}

		// Token: 0x060047B0 RID: 18352 RVA: 0x000FE272 File Offset: 0x000FC472
		internal virtual PropagatorResult[] GetMemberValues()
		{
			throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UpdatePipelineResultRequestInvalid, 0, "PropagatorResult.GetMembersValues");
		}

		// Token: 0x060047B1 RID: 18353
		internal abstract PropagatorResult ReplicateResultWithNewFlags(PropagatorFlags flags);

		// Token: 0x060047B2 RID: 18354 RVA: 0x000FE284 File Offset: 0x000FC484
		internal virtual PropagatorResult ReplicateResultWithNewValue(object value)
		{
			throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UpdatePipelineResultRequestInvalid, 0, "PropagatorResult.ReplicateResultWithNewValue");
		}

		// Token: 0x060047B3 RID: 18355
		internal abstract PropagatorResult Replace(Func<PropagatorResult, PropagatorResult> map);

		// Token: 0x060047B4 RID: 18356 RVA: 0x000FE296 File Offset: 0x000FC496
		internal virtual PropagatorResult Merge(KeyManager keyManager, PropagatorResult other)
		{
			throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UpdatePipelineResultRequestInvalid, 0, "PropagatorResult.Merge");
		}

		// Token: 0x060047B5 RID: 18357 RVA: 0x000FE2A8 File Offset: 0x000FC4A8
		internal virtual void SetServerGenValue(object value)
		{
			if (this.RecordOrdinal != -1)
			{
				CurrentValueRecord record = this.Record;
				EdmMember fieldType = ((IExtendedDataRecord)record).DataRecordInfo.FieldMetadata[this.RecordOrdinal].FieldType;
				value = value ?? DBNull.Value;
				value = this.AlignReturnValue(value, fieldType);
				record.SetValue(this.RecordOrdinal, value);
			}
		}

		// Token: 0x060047B6 RID: 18358 RVA: 0x000FE308 File Offset: 0x000FC508
		internal object AlignReturnValue(object value, EdmMember member)
		{
			if (DBNull.Value.Equals(value))
			{
				if (BuiltInTypeKind.EdmProperty == member.BuiltInTypeKind && !((EdmProperty)member).Nullable)
				{
					throw EntityUtil.Update(Strings.Update_NullReturnValueForNonNullableMember(member.Name, member.DeclaringType.FullName), null, new IEntityStateEntry[0]);
				}
			}
			else if (!Helper.IsSpatialType(member.TypeUsage))
			{
				Type type = null;
				Type type2;
				if (Helper.IsEnumType(member.TypeUsage.EdmType))
				{
					PrimitiveType primitiveType = Helper.AsPrimitive(member.TypeUsage.EdmType);
					type = this.Record.GetFieldType(this.RecordOrdinal);
					type2 = primitiveType.ClrEquivalentType;
				}
				else
				{
					type2 = ((PrimitiveType)member.TypeUsage.EdmType).ClrEquivalentType;
				}
				try
				{
					value = Convert.ChangeType(value, type2, CultureInfo.InvariantCulture);
					if (type != null)
					{
						value = Enum.ToObject(type, value);
					}
				}
				catch (Exception ex)
				{
					if (ex.RequiresContext())
					{
						Type type3 = type ?? type2;
						throw EntityUtil.Update(Strings.Update_ReturnValueHasUnexpectedType(value.GetType().FullName, type3.FullName, member.Name, member.DeclaringType.FullName), ex, new IEntityStateEntry[0]);
					}
					throw;
				}
			}
			return value;
		}

		// Token: 0x060047B7 RID: 18359 RVA: 0x000FE440 File Offset: 0x000FC640
		internal static PropagatorResult CreateSimpleValue(PropagatorFlags flags, object value)
		{
			return new PropagatorResult.SimpleValue(flags, value);
		}

		// Token: 0x060047B8 RID: 18360 RVA: 0x000FE449 File Offset: 0x000FC649
		internal static PropagatorResult CreateServerGenSimpleValue(PropagatorFlags flags, object value, CurrentValueRecord record, int recordOrdinal)
		{
			return new PropagatorResult.ServerGenSimpleValue(flags, value, record, recordOrdinal);
		}

		// Token: 0x060047B9 RID: 18361 RVA: 0x000FE454 File Offset: 0x000FC654
		internal static PropagatorResult CreateKeyValue(PropagatorFlags flags, object value, IEntityStateEntry stateEntry, int identifier)
		{
			return new PropagatorResult.KeyValue(flags, value, stateEntry, identifier, null);
		}

		// Token: 0x060047BA RID: 18362 RVA: 0x000FE460 File Offset: 0x000FC660
		internal static PropagatorResult CreateServerGenKeyValue(PropagatorFlags flags, object value, IEntityStateEntry stateEntry, int identifier, int recordOrdinal)
		{
			return new PropagatorResult.ServerGenKeyValue(flags, value, stateEntry, identifier, recordOrdinal, null);
		}

		// Token: 0x060047BB RID: 18363 RVA: 0x000FE46E File Offset: 0x000FC66E
		internal static PropagatorResult CreateStructuralValue(PropagatorResult[] values, StructuralType structuralType, bool isModified)
		{
			if (isModified)
			{
				return new PropagatorResult.StructuralValue(values, structuralType);
			}
			return new PropagatorResult.UnmodifiedStructuralValue(values, structuralType);
		}

		// Token: 0x04001982 RID: 6530
		internal const int NullIdentifier = -1;

		// Token: 0x04001983 RID: 6531
		internal const int NullOrdinal = -1;

		// Token: 0x02000C06 RID: 3078
		private class SimpleValue : PropagatorResult
		{
			// Token: 0x06006917 RID: 26903 RVA: 0x001679F6 File Offset: 0x00165BF6
			internal SimpleValue(PropagatorFlags flags, object value)
			{
				this.m_flags = flags;
				this.m_value = value ?? DBNull.Value;
			}

			// Token: 0x1700113B RID: 4411
			// (get) Token: 0x06006918 RID: 26904 RVA: 0x00167A15 File Offset: 0x00165C15
			internal override PropagatorFlags PropagatorFlags
			{
				get
				{
					return this.m_flags;
				}
			}

			// Token: 0x1700113C RID: 4412
			// (get) Token: 0x06006919 RID: 26905 RVA: 0x00167A1D File Offset: 0x00165C1D
			internal override bool IsSimple
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700113D RID: 4413
			// (get) Token: 0x0600691A RID: 26906 RVA: 0x00167A20 File Offset: 0x00165C20
			internal override bool IsNull
			{
				get
				{
					return -1 == this.Identifier && DBNull.Value == this.m_value;
				}
			}

			// Token: 0x0600691B RID: 26907 RVA: 0x00167A3A File Offset: 0x00165C3A
			internal override object GetSimpleValue()
			{
				return this.m_value;
			}

			// Token: 0x0600691C RID: 26908 RVA: 0x00167A42 File Offset: 0x00165C42
			internal override PropagatorResult ReplicateResultWithNewFlags(PropagatorFlags flags)
			{
				return new PropagatorResult.SimpleValue(flags, this.m_value);
			}

			// Token: 0x0600691D RID: 26909 RVA: 0x00167A50 File Offset: 0x00165C50
			internal override PropagatorResult ReplicateResultWithNewValue(object value)
			{
				return new PropagatorResult.SimpleValue(this.PropagatorFlags, value);
			}

			// Token: 0x0600691E RID: 26910 RVA: 0x00167A5E File Offset: 0x00165C5E
			internal override PropagatorResult Replace(Func<PropagatorResult, PropagatorResult> map)
			{
				return map(this);
			}

			// Token: 0x04002FA2 RID: 12194
			private readonly PropagatorFlags m_flags;

			// Token: 0x04002FA3 RID: 12195
			protected readonly object m_value;
		}

		// Token: 0x02000C07 RID: 3079
		private class ServerGenSimpleValue : PropagatorResult.SimpleValue
		{
			// Token: 0x0600691F RID: 26911 RVA: 0x00167A67 File Offset: 0x00165C67
			internal ServerGenSimpleValue(PropagatorFlags flags, object value, CurrentValueRecord record, int recordOrdinal)
				: base(flags, value)
			{
				this.m_record = record;
				this.m_recordOrdinal = recordOrdinal;
			}

			// Token: 0x1700113E RID: 4414
			// (get) Token: 0x06006920 RID: 26912 RVA: 0x00167A80 File Offset: 0x00165C80
			internal override CurrentValueRecord Record
			{
				get
				{
					return this.m_record;
				}
			}

			// Token: 0x1700113F RID: 4415
			// (get) Token: 0x06006921 RID: 26913 RVA: 0x00167A88 File Offset: 0x00165C88
			internal override int RecordOrdinal
			{
				get
				{
					return this.m_recordOrdinal;
				}
			}

			// Token: 0x06006922 RID: 26914 RVA: 0x00167A90 File Offset: 0x00165C90
			internal override PropagatorResult ReplicateResultWithNewFlags(PropagatorFlags flags)
			{
				return new PropagatorResult.ServerGenSimpleValue(flags, this.m_value, this.Record, this.RecordOrdinal);
			}

			// Token: 0x06006923 RID: 26915 RVA: 0x00167AAA File Offset: 0x00165CAA
			internal override PropagatorResult ReplicateResultWithNewValue(object value)
			{
				return new PropagatorResult.ServerGenSimpleValue(this.PropagatorFlags, value, this.Record, this.RecordOrdinal);
			}

			// Token: 0x04002FA4 RID: 12196
			private readonly CurrentValueRecord m_record;

			// Token: 0x04002FA5 RID: 12197
			private readonly int m_recordOrdinal;
		}

		// Token: 0x02000C08 RID: 3080
		private class KeyValue : PropagatorResult.SimpleValue
		{
			// Token: 0x06006924 RID: 26916 RVA: 0x00167AC4 File Offset: 0x00165CC4
			internal KeyValue(PropagatorFlags flags, object value, IEntityStateEntry stateEntry, int identifier, PropagatorResult.KeyValue next)
				: base(flags, value)
			{
				this.m_stateEntry = stateEntry;
				this.m_identifier = identifier;
				this.m_next = next;
			}

			// Token: 0x17001140 RID: 4416
			// (get) Token: 0x06006925 RID: 26917 RVA: 0x00167AE5 File Offset: 0x00165CE5
			internal override IEntityStateEntry StateEntry
			{
				get
				{
					return this.m_stateEntry;
				}
			}

			// Token: 0x17001141 RID: 4417
			// (get) Token: 0x06006926 RID: 26918 RVA: 0x00167AED File Offset: 0x00165CED
			internal override int Identifier
			{
				get
				{
					return this.m_identifier;
				}
			}

			// Token: 0x17001142 RID: 4418
			// (get) Token: 0x06006927 RID: 26919 RVA: 0x00167AF5 File Offset: 0x00165CF5
			internal override CurrentValueRecord Record
			{
				get
				{
					return this.m_stateEntry.CurrentValues;
				}
			}

			// Token: 0x17001143 RID: 4419
			// (get) Token: 0x06006928 RID: 26920 RVA: 0x00167B02 File Offset: 0x00165D02
			internal override PropagatorResult Next
			{
				get
				{
					return this.m_next;
				}
			}

			// Token: 0x06006929 RID: 26921 RVA: 0x00167B0A File Offset: 0x00165D0A
			internal override PropagatorResult ReplicateResultWithNewFlags(PropagatorFlags flags)
			{
				return new PropagatorResult.KeyValue(flags, this.m_value, this.StateEntry, this.Identifier, this.m_next);
			}

			// Token: 0x0600692A RID: 26922 RVA: 0x00167B2A File Offset: 0x00165D2A
			internal override PropagatorResult ReplicateResultWithNewValue(object value)
			{
				return new PropagatorResult.KeyValue(this.PropagatorFlags, value, this.StateEntry, this.Identifier, this.m_next);
			}

			// Token: 0x0600692B RID: 26923 RVA: 0x00167B4A File Offset: 0x00165D4A
			internal virtual PropagatorResult.KeyValue ReplicateResultWithNewNext(PropagatorResult.KeyValue next)
			{
				if (this.m_next != null)
				{
					next = this.m_next.ReplicateResultWithNewNext(next);
				}
				return new PropagatorResult.KeyValue(this.PropagatorFlags, this.m_value, this.m_stateEntry, this.m_identifier, next);
			}

			// Token: 0x0600692C RID: 26924 RVA: 0x00167B80 File Offset: 0x00165D80
			internal override PropagatorResult Merge(KeyManager keyManager, PropagatorResult other)
			{
				PropagatorResult.KeyValue keyValue = other as PropagatorResult.KeyValue;
				if (keyValue == null)
				{
					EntityUtil.InternalError(EntityUtil.InternalErrorCode.UpdatePipelineResultRequestInvalid, 0, "KeyValue.Merge");
				}
				if (this.Identifier != keyValue.Identifier)
				{
					if (keyManager.GetPrincipals(keyValue.Identifier).Contains(this.Identifier))
					{
						return this.ReplicateResultWithNewNext(keyValue);
					}
					return keyValue.ReplicateResultWithNewNext(this);
				}
				else
				{
					if (this.m_stateEntry == null || this.m_stateEntry.IsRelationship)
					{
						return keyValue.ReplicateResultWithNewNext(this);
					}
					return this.ReplicateResultWithNewNext(keyValue);
				}
			}

			// Token: 0x04002FA6 RID: 12198
			private readonly IEntityStateEntry m_stateEntry;

			// Token: 0x04002FA7 RID: 12199
			private readonly int m_identifier;

			// Token: 0x04002FA8 RID: 12200
			protected readonly PropagatorResult.KeyValue m_next;
		}

		// Token: 0x02000C09 RID: 3081
		private class ServerGenKeyValue : PropagatorResult.KeyValue
		{
			// Token: 0x0600692D RID: 26925 RVA: 0x00167C03 File Offset: 0x00165E03
			internal ServerGenKeyValue(PropagatorFlags flags, object value, IEntityStateEntry stateEntry, int identifier, int recordOrdinal, PropagatorResult.KeyValue next)
				: base(flags, value, stateEntry, identifier, next)
			{
				this.m_recordOrdinal = recordOrdinal;
			}

			// Token: 0x17001144 RID: 4420
			// (get) Token: 0x0600692E RID: 26926 RVA: 0x00167C1A File Offset: 0x00165E1A
			internal override int RecordOrdinal
			{
				get
				{
					return this.m_recordOrdinal;
				}
			}

			// Token: 0x0600692F RID: 26927 RVA: 0x00167C22 File Offset: 0x00165E22
			internal override PropagatorResult ReplicateResultWithNewFlags(PropagatorFlags flags)
			{
				return new PropagatorResult.ServerGenKeyValue(flags, this.m_value, this.StateEntry, this.Identifier, this.RecordOrdinal, this.m_next);
			}

			// Token: 0x06006930 RID: 26928 RVA: 0x00167C48 File Offset: 0x00165E48
			internal override PropagatorResult ReplicateResultWithNewValue(object value)
			{
				return new PropagatorResult.ServerGenKeyValue(this.PropagatorFlags, value, this.StateEntry, this.Identifier, this.RecordOrdinal, this.m_next);
			}

			// Token: 0x06006931 RID: 26929 RVA: 0x00167C6E File Offset: 0x00165E6E
			internal override PropagatorResult.KeyValue ReplicateResultWithNewNext(PropagatorResult.KeyValue next)
			{
				if (this.m_next != null)
				{
					next = this.m_next.ReplicateResultWithNewNext(next);
				}
				return new PropagatorResult.ServerGenKeyValue(this.PropagatorFlags, this.m_value, this.StateEntry, this.Identifier, this.RecordOrdinal, next);
			}

			// Token: 0x04002FA9 RID: 12201
			private readonly int m_recordOrdinal;
		}

		// Token: 0x02000C0A RID: 3082
		private class StructuralValue : PropagatorResult
		{
			// Token: 0x06006932 RID: 26930 RVA: 0x00167CAA File Offset: 0x00165EAA
			internal StructuralValue(PropagatorResult[] values, StructuralType structuralType)
			{
				this.m_values = values;
				this.m_structuralType = structuralType;
			}

			// Token: 0x17001145 RID: 4421
			// (get) Token: 0x06006933 RID: 26931 RVA: 0x00167CC0 File Offset: 0x00165EC0
			internal override bool IsSimple
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001146 RID: 4422
			// (get) Token: 0x06006934 RID: 26932 RVA: 0x00167CC3 File Offset: 0x00165EC3
			internal override bool IsNull
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001147 RID: 4423
			// (get) Token: 0x06006935 RID: 26933 RVA: 0x00167CC6 File Offset: 0x00165EC6
			internal override StructuralType StructuralType
			{
				get
				{
					return this.m_structuralType;
				}
			}

			// Token: 0x06006936 RID: 26934 RVA: 0x00167CCE File Offset: 0x00165ECE
			internal override PropagatorResult GetMemberValue(int ordinal)
			{
				return this.m_values[ordinal];
			}

			// Token: 0x06006937 RID: 26935 RVA: 0x00167CD8 File Offset: 0x00165ED8
			internal override PropagatorResult[] GetMemberValues()
			{
				return this.m_values;
			}

			// Token: 0x06006938 RID: 26936 RVA: 0x00167CE0 File Offset: 0x00165EE0
			internal override PropagatorResult ReplicateResultWithNewFlags(PropagatorFlags flags)
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.UpdatePipelineResultRequestInvalid, 0, "StructuralValue.ReplicateResultWithNewFlags");
			}

			// Token: 0x06006939 RID: 26937 RVA: 0x00167CF4 File Offset: 0x00165EF4
			internal override PropagatorResult Replace(Func<PropagatorResult, PropagatorResult> map)
			{
				PropagatorResult[] array = this.ReplaceValues(map);
				if (array != null)
				{
					return new PropagatorResult.StructuralValue(array, this.m_structuralType);
				}
				return this;
			}

			// Token: 0x0600693A RID: 26938 RVA: 0x00167D1C File Offset: 0x00165F1C
			protected PropagatorResult[] ReplaceValues(Func<PropagatorResult, PropagatorResult> map)
			{
				PropagatorResult[] array = new PropagatorResult[this.m_values.Length];
				bool flag = false;
				for (int i = 0; i < array.Length; i++)
				{
					PropagatorResult propagatorResult = this.m_values[i].Replace(map);
					if (propagatorResult != this.m_values[i])
					{
						flag = true;
					}
					array[i] = propagatorResult;
				}
				if (!flag)
				{
					return null;
				}
				return array;
			}

			// Token: 0x04002FAA RID: 12202
			private readonly PropagatorResult[] m_values;

			// Token: 0x04002FAB RID: 12203
			protected readonly StructuralType m_structuralType;
		}

		// Token: 0x02000C0B RID: 3083
		private class UnmodifiedStructuralValue : PropagatorResult.StructuralValue
		{
			// Token: 0x0600693B RID: 26939 RVA: 0x00167D6D File Offset: 0x00165F6D
			internal UnmodifiedStructuralValue(PropagatorResult[] values, StructuralType structuralType)
				: base(values, structuralType)
			{
			}

			// Token: 0x17001148 RID: 4424
			// (get) Token: 0x0600693C RID: 26940 RVA: 0x00167D77 File Offset: 0x00165F77
			internal override PropagatorFlags PropagatorFlags
			{
				get
				{
					return PropagatorFlags.Preserve;
				}
			}

			// Token: 0x0600693D RID: 26941 RVA: 0x00167D7C File Offset: 0x00165F7C
			internal override PropagatorResult Replace(Func<PropagatorResult, PropagatorResult> map)
			{
				PropagatorResult[] array = base.ReplaceValues(map);
				if (array != null)
				{
					return new PropagatorResult.UnmodifiedStructuralValue(array, this.m_structuralType);
				}
				return this;
			}
		}
	}
}
