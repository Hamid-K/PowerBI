using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace System.Data.Entity.Core
{
	// Token: 0x020002D0 RID: 720
	[DebuggerDisplay("{ConcatKeyValue()}")]
	[DataContract(IsReference = true)]
	[Serializable]
	public sealed class EntityKey : IEquatable<EntityKey>
	{
		// Token: 0x060022BA RID: 8890 RVA: 0x00062072 File Offset: 0x00060272
		public EntityKey()
		{
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x0006207A File Offset: 0x0006027A
		public EntityKey(string qualifiedEntitySetName, IEnumerable<KeyValuePair<string, object>> entityKeyValues)
		{
			Check.NotEmpty(qualifiedEntitySetName, "qualifiedEntitySetName");
			Check.NotNull<IEnumerable<KeyValuePair<string, object>>>(entityKeyValues, "entityKeyValues");
			this.InitializeEntitySetName(qualifiedEntitySetName);
			this.InitializeKeyValues(entityKeyValues, false, false);
			this._isLocked = true;
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x000620B2 File Offset: 0x000602B2
		public EntityKey(string qualifiedEntitySetName, IEnumerable<EntityKeyMember> entityKeyValues)
		{
			Check.NotEmpty(qualifiedEntitySetName, "qualifiedEntitySetName");
			Check.NotNull<IEnumerable<EntityKeyMember>>(entityKeyValues, "entityKeyValues");
			this.InitializeEntitySetName(qualifiedEntitySetName);
			this.InitializeKeyValues(new EntityKey.KeyValueReader(entityKeyValues), false, false);
			this._isLocked = true;
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x000620F0 File Offset: 0x000602F0
		public EntityKey(string qualifiedEntitySetName, string keyName, object keyValue)
		{
			Check.NotEmpty(qualifiedEntitySetName, "qualifiedEntitySetName");
			Check.NotEmpty(keyName, "keyName");
			Check.NotNull<object>(keyValue, "keyValue");
			this.InitializeEntitySetName(qualifiedEntitySetName);
			EntityKey.ValidateName(keyName);
			this._keyNames = new string[] { keyName };
			this._singletonKeyValue = keyValue;
			this._isLocked = true;
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x00062152 File Offset: 0x00060352
		internal EntityKey(EntitySet entitySet, IExtendedDataRecord record)
		{
			this._entitySetName = entitySet.Name;
			this._entityContainerName = entitySet.EntityContainer.Name;
			this.InitializeKeyValues(entitySet, record);
			this._isLocked = true;
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x00062186 File Offset: 0x00060386
		internal EntityKey(string qualifiedEntitySetName)
		{
			this.InitializeEntitySetName(qualifiedEntitySetName);
			this._isLocked = true;
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x0006219C File Offset: 0x0006039C
		internal EntityKey(EntitySetBase entitySet)
		{
			this._entitySetName = entitySet.Name;
			this._entityContainerName = entitySet.EntityContainer.Name;
			this._isLocked = true;
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x000621C8 File Offset: 0x000603C8
		internal EntityKey(EntitySetBase entitySet, object singletonKeyValue)
		{
			this._singletonKeyValue = singletonKeyValue;
			this._entitySetName = entitySet.Name;
			this._entityContainerName = entitySet.EntityContainer.Name;
			this._keyNames = entitySet.ElementType.KeyMemberNames;
			this._isLocked = true;
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x00062218 File Offset: 0x00060418
		internal EntityKey(EntitySetBase entitySet, object[] compositeKeyValues)
		{
			this._compositeKeyValues = compositeKeyValues;
			this._entitySetName = entitySet.Name;
			this._entityContainerName = entitySet.EntityContainer.Name;
			this._keyNames = entitySet.ElementType.KeyMemberNames;
			this._isLocked = true;
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x00062267 File Offset: 0x00060467
		public static EntityKey NoEntitySetKey
		{
			get
			{
				return EntityKey._noEntitySetKey;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x060022C4 RID: 8900 RVA: 0x0006226E File Offset: 0x0006046E
		public static EntityKey EntityNotValidKey
		{
			get
			{
				return EntityKey._entityNotValidKey;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060022C5 RID: 8901 RVA: 0x00062275 File Offset: 0x00060475
		// (set) Token: 0x060022C6 RID: 8902 RVA: 0x0006227D File Offset: 0x0006047D
		[DataMember]
		public string EntitySetName
		{
			get
			{
				return this._entitySetName;
			}
			set
			{
				this.ValidateWritable(this._entitySetName);
				this._entitySetName = EntityKey.LookupSingletonName(value);
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060022C7 RID: 8903 RVA: 0x00062297 File Offset: 0x00060497
		// (set) Token: 0x060022C8 RID: 8904 RVA: 0x0006229F File Offset: 0x0006049F
		[DataMember]
		public string EntityContainerName
		{
			get
			{
				return this._entityContainerName;
			}
			set
			{
				this.ValidateWritable(this._entityContainerName);
				this._entityContainerName = EntityKey.LookupSingletonName(value);
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060022C9 RID: 8905 RVA: 0x000622BC File Offset: 0x000604BC
		// (set) Token: 0x060022CA RID: 8906 RVA: 0x00062334 File Offset: 0x00060534
		[DataMember]
		public EntityKeyMember[] EntityKeyValues
		{
			get
			{
				if (!this.IsTemporary)
				{
					EntityKeyMember[] array;
					if (this._singletonKeyValue != null)
					{
						array = new EntityKeyMember[]
						{
							new EntityKeyMember(this._keyNames[0], this._singletonKeyValue)
						};
					}
					else
					{
						array = new EntityKeyMember[this._compositeKeyValues.Length];
						for (int i = 0; i < this._compositeKeyValues.Length; i++)
						{
							array[i] = new EntityKeyMember(this._keyNames[i], this._compositeKeyValues[i]);
						}
					}
					return array;
				}
				return null;
			}
			set
			{
				this.ValidateWritable(this._keyNames);
				if (value != null && !this.InitializeKeyValues(new EntityKey.KeyValueReader(value), true, true))
				{
					this._deserializedMembers = value;
				}
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060022CB RID: 8907 RVA: 0x0006235C File Offset: 0x0006055C
		public bool IsTemporary
		{
			get
			{
				return this.SingletonKeyValue == null && this.CompositeKeyValues == null;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060022CC RID: 8908 RVA: 0x00062371 File Offset: 0x00060571
		private object SingletonKeyValue
		{
			get
			{
				if (this.RequiresDeserialization)
				{
					this.DeserializeMembers();
				}
				return this._singletonKeyValue;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x00062387 File Offset: 0x00060587
		private object[] CompositeKeyValues
		{
			get
			{
				if (this.RequiresDeserialization)
				{
					this.DeserializeMembers();
				}
				return this._compositeKeyValues;
			}
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x000623A0 File Offset: 0x000605A0
		public EntitySet GetEntitySet(MetadataWorkspace metadataWorkspace)
		{
			Check.NotNull<MetadataWorkspace>(metadataWorkspace, "metadataWorkspace");
			if (string.IsNullOrEmpty(this._entityContainerName) || string.IsNullOrEmpty(this._entitySetName))
			{
				throw new InvalidOperationException(Strings.EntityKey_MissingEntitySetName);
			}
			return metadataWorkspace.GetEntityContainer(this._entityContainerName, DataSpace.CSpace).GetEntitySetByName(this._entitySetName, false);
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x000623F7 File Offset: 0x000605F7
		public override bool Equals(object obj)
		{
			return EntityKey.InternalEquals(this, obj as EntityKey, true);
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x00062406 File Offset: 0x00060606
		public bool Equals(EntityKey other)
		{
			return EntityKey.InternalEquals(this, other, true);
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x00062410 File Offset: 0x00060610
		public override int GetHashCode()
		{
			int num = this._hashCode;
			if (num == 0)
			{
				this._containsByteArray = false;
				if (this.RequiresDeserialization)
				{
					this.DeserializeMembers();
				}
				if (this._entitySetName != null)
				{
					num = this._entitySetName.GetHashCode();
				}
				if (this._entityContainerName != null)
				{
					num ^= this._entityContainerName.GetHashCode();
				}
				if (this._singletonKeyValue != null)
				{
					num = this.AddHashValue(num, this._singletonKeyValue);
				}
				else if (this._compositeKeyValues != null)
				{
					int i = 0;
					int num2 = this._compositeKeyValues.Length;
					while (i < num2)
					{
						num = this.AddHashValue(num, this._compositeKeyValues[i]);
						i++;
					}
				}
				else
				{
					num = base.GetHashCode();
				}
				if (this._isLocked || (!string.IsNullOrEmpty(this._entitySetName) && !string.IsNullOrEmpty(this._entityContainerName) && (this._singletonKeyValue != null || this._compositeKeyValues != null)))
				{
					this._hashCode = num;
				}
			}
			return num;
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x000624F4 File Offset: 0x000606F4
		private int AddHashValue(int hashCode, object keyValue)
		{
			byte[] array = keyValue as byte[];
			if (array != null)
			{
				hashCode ^= ByValueEqualityComparer.ComputeBinaryHashCode(array);
				this._containsByteArray = true;
				return hashCode;
			}
			return hashCode ^ keyValue.GetHashCode();
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x00062526 File Offset: 0x00060726
		public static bool operator ==(EntityKey key1, EntityKey key2)
		{
			return EntityKey.InternalEquals(key1, key2, true);
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x00062530 File Offset: 0x00060730
		public static bool operator !=(EntityKey key1, EntityKey key2)
		{
			return !EntityKey.InternalEquals(key1, key2, true);
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x00062540 File Offset: 0x00060740
		internal static bool InternalEquals(EntityKey key1, EntityKey key2, bool compareEntitySets)
		{
			if (key1 == key2)
			{
				return true;
			}
			if (key1 == null || key2 == null)
			{
				return false;
			}
			if (EntityKey.NoEntitySetKey == key1 || EntityKey.EntityNotValidKey == key1 || EntityKey.NoEntitySetKey == key2 || EntityKey.EntityNotValidKey == key2)
			{
				return false;
			}
			if ((key1.GetHashCode() != key2.GetHashCode() && compareEntitySets) || key1._containsByteArray != key2._containsByteArray)
			{
				return false;
			}
			if (key1._singletonKeyValue != null)
			{
				if (key1._containsByteArray)
				{
					if (key2._singletonKeyValue == null)
					{
						return false;
					}
					if (!ByValueEqualityComparer.CompareBinaryValues((byte[])key1._singletonKeyValue, (byte[])key2._singletonKeyValue))
					{
						return false;
					}
				}
				else if (!key1._singletonKeyValue.Equals(key2._singletonKeyValue))
				{
					return false;
				}
				if (!string.Equals(key1._keyNames[0], key2._keyNames[0]))
				{
					return false;
				}
			}
			else
			{
				if (key1._compositeKeyValues == null || key2._compositeKeyValues == null || key1._compositeKeyValues.Length != key2._compositeKeyValues.Length)
				{
					return false;
				}
				if (key1._containsByteArray)
				{
					if (!EntityKey.CompositeValuesWithBinaryEqual(key1, key2))
					{
						return false;
					}
				}
				else if (!EntityKey.CompositeValuesEqual(key1, key2))
				{
					return false;
				}
			}
			return !compareEntitySets || (string.Equals(key1._entitySetName, key2._entitySetName) && string.Equals(key1._entityContainerName, key2._entityContainerName));
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x00062678 File Offset: 0x00060878
		internal static bool CompositeValuesWithBinaryEqual(EntityKey key1, EntityKey key2)
		{
			for (int i = 0; i < key1._compositeKeyValues.Length; i++)
			{
				if (key1._keyNames[i].Equals(key2._keyNames[i]))
				{
					if (!ByValueEqualityComparer.Default.Equals(key1._compositeKeyValues[i], key2._compositeKeyValues[i]))
					{
						return false;
					}
				}
				else if (!EntityKey.ValuesWithBinaryEqual(key1._keyNames[i], key1._compositeKeyValues[i], key2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x000626E8 File Offset: 0x000608E8
		private static bool ValuesWithBinaryEqual(string keyName, object keyValue, EntityKey key2)
		{
			for (int i = 0; i < key2._keyNames.Length; i++)
			{
				if (string.Equals(keyName, key2._keyNames[i]))
				{
					return ByValueEqualityComparer.Default.Equals(keyValue, key2._compositeKeyValues[i]);
				}
			}
			return false;
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x00062730 File Offset: 0x00060930
		private static bool CompositeValuesEqual(EntityKey key1, EntityKey key2)
		{
			for (int i = 0; i < key1._compositeKeyValues.Length; i++)
			{
				if (key1._keyNames[i].Equals(key2._keyNames[i]))
				{
					if (!object.Equals(key1._compositeKeyValues[i], key2._compositeKeyValues[i]))
					{
						return false;
					}
				}
				else if (!EntityKey.ValuesEqual(key1._keyNames[i], key1._compositeKeyValues[i], key2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x0006279C File Offset: 0x0006099C
		private static bool ValuesEqual(string keyName, object keyValue, EntityKey key2)
		{
			for (int i = 0; i < key2._keyNames.Length; i++)
			{
				if (string.Equals(keyName, key2._keyNames[i]))
				{
					return object.Equals(keyValue, key2._compositeKeyValues[i]);
				}
			}
			return false;
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x000627DC File Offset: 0x000609DC
		internal KeyValuePair<string, DbExpression>[] GetKeyValueExpressions(EntitySet entitySet)
		{
			int num = 0;
			if (!this.IsTemporary)
			{
				if (this._singletonKeyValue != null)
				{
					num = 1;
				}
				else
				{
					num = this._compositeKeyValues.Length;
				}
			}
			if (entitySet.ElementType.KeyMembers.Count != num)
			{
				throw new ArgumentException(Strings.EntityKey_EntitySetDoesNotMatch(TypeHelpers.GetFullName(entitySet.EntityContainer.Name, entitySet.Name)), "entitySet");
			}
			KeyValuePair<string, DbExpression>[] array;
			if (this._singletonKeyValue != null)
			{
				EdmMember edmMember = entitySet.ElementType.KeyMembers[0];
				array = new KeyValuePair<string, DbExpression>[] { Helper.GetModelTypeUsage(edmMember).Constant(this._singletonKeyValue).As(edmMember.Name) };
			}
			else
			{
				array = new KeyValuePair<string, DbExpression>[this._compositeKeyValues.Length];
				for (int i = 0; i < this._compositeKeyValues.Length; i++)
				{
					EdmMember edmMember2 = entitySet.ElementType.KeyMembers[i];
					array[i] = Helper.GetModelTypeUsage(edmMember2).Constant(this._compositeKeyValues[i]).As(edmMember2.Name);
				}
			}
			return array;
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x000628E4 File Offset: 0x00060AE4
		internal string ConcatKeyValue()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("EntitySet=").Append(this._entitySetName);
			if (!this.IsTemporary)
			{
				foreach (EntityKeyMember entityKeyMember in this.EntityKeyValues)
				{
					stringBuilder.Append(';');
					stringBuilder.Append(entityKeyMember.Key).Append("=").Append(entityKeyMember.Value);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x00062960 File Offset: 0x00060B60
		internal object FindValueByName(string keyName)
		{
			if (this.SingletonKeyValue != null)
			{
				return this._singletonKeyValue;
			}
			object[] compositeKeyValues = this.CompositeKeyValues;
			for (int i = 0; i < compositeKeyValues.Length; i++)
			{
				if (keyName == this._keyNames[i])
				{
					return compositeKeyValues[i];
				}
			}
			throw new ArgumentOutOfRangeException("keyName");
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x000629B0 File Offset: 0x00060BB0
		internal void InitializeEntitySetName(string qualifiedEntitySetName)
		{
			string[] array = qualifiedEntitySetName.Split(new char[] { '.' });
			if (array.Length != 2 || string.IsNullOrWhiteSpace(array[0]) || string.IsNullOrWhiteSpace(array[1]))
			{
				throw new ArgumentException(Strings.EntityKey_InvalidQualifiedEntitySetName, "qualifiedEntitySetName");
			}
			this._entityContainerName = array[0];
			this._entitySetName = array[1];
			EntityKey.ValidateName(this._entityContainerName);
			EntityKey.ValidateName(this._entitySetName);
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x00062A21 File Offset: 0x00060C21
		private static void ValidateName(string name)
		{
			if (!name.IsValidUndottedName())
			{
				throw new ArgumentException(Strings.EntityKey_InvalidName(name));
			}
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x00062A38 File Offset: 0x00060C38
		internal bool InitializeKeyValues(IEnumerable<KeyValuePair<string, object>> entityKeyValues, bool allowNullKeys = false, bool tokenizeStrings = false)
		{
			int num = entityKeyValues.Count<KeyValuePair<string, object>>();
			if (num == 1)
			{
				this._keyNames = new string[1];
				KeyValuePair<string, object> keyValuePair = entityKeyValues.Single<KeyValuePair<string, object>>();
				this.InitializeKeyValue(keyValuePair, 0, tokenizeStrings);
				this._singletonKeyValue = keyValuePair.Value;
			}
			else
			{
				if (num > 1)
				{
					this._keyNames = new string[num];
					this._compositeKeyValues = new object[num];
					int num2 = 0;
					using (IEnumerator<KeyValuePair<string, object>> enumerator = entityKeyValues.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, object> keyValuePair2 = enumerator.Current;
							this.InitializeKeyValue(keyValuePair2, num2, tokenizeStrings);
							this._compositeKeyValues[num2] = keyValuePair2.Value;
							num2++;
						}
						goto IL_00A9;
					}
				}
				if (!allowNullKeys)
				{
					throw new ArgumentException(Strings.EntityKey_EntityKeyMustHaveValues, "entityKeyValues");
				}
			}
			IL_00A9:
			return num > 0;
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x00062B04 File Offset: 0x00060D04
		private void InitializeKeyValue(KeyValuePair<string, object> keyValuePair, int i, bool tokenizeStrings)
		{
			if (EntityUtil.IsNull(keyValuePair.Value) || string.IsNullOrWhiteSpace(keyValuePair.Key))
			{
				throw new ArgumentException(Strings.EntityKey_NoNullsAllowedInKeyValuePairs, "entityKeyValues");
			}
			EntityKey.ValidateName(keyValuePair.Key);
			this._keyNames[i] = (tokenizeStrings ? EntityKey.LookupSingletonName(keyValuePair.Key) : keyValuePair.Key);
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x00062B6C File Offset: 0x00060D6C
		private void InitializeKeyValues(EntitySet entitySet, IExtendedDataRecord record)
		{
			int count = entitySet.ElementType.KeyMembers.Count;
			this._keyNames = entitySet.ElementType.KeyMemberNames;
			EntityType entityType = (EntityType)record.DataRecordInfo.RecordType.EdmType;
			if (count == 1)
			{
				this._singletonKeyValue = record[entityType.KeyMembers[0].Name];
				if (EntityUtil.IsNull(this._singletonKeyValue))
				{
					throw new ArgumentException(Strings.EntityKey_NoNullsAllowedInKeyValuePairs, "record");
				}
			}
			else
			{
				this._compositeKeyValues = new object[count];
				for (int i = 0; i < count; i++)
				{
					this._compositeKeyValues[i] = record[entityType.KeyMembers[i].Name];
					if (EntityUtil.IsNull(this._compositeKeyValues[i]))
					{
						throw new ArgumentException(Strings.EntityKey_NoNullsAllowedInKeyValuePairs, "record");
					}
				}
			}
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x00062C45 File Offset: 0x00060E45
		internal void ValidateEntityKey(MetadataWorkspace workspace, EntitySet entitySet)
		{
			this.ValidateEntityKey(workspace, entitySet, false, null);
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x00062C54 File Offset: 0x00060E54
		internal void ValidateEntityKey(MetadataWorkspace workspace, EntitySet entitySet, bool isArgumentException, string argumentName)
		{
			if (entitySet != null)
			{
				ReadOnlyMetadataCollection<EdmMember> keyMembers = entitySet.ElementType.KeyMembers;
				if (this._singletonKeyValue != null)
				{
					if (keyMembers.Count != 1)
					{
						if (isArgumentException)
						{
							throw new ArgumentException(Strings.EntityKey_IncorrectNumberOfKeyValuePairs(entitySet.ElementType.FullName, keyMembers.Count, 1), argumentName);
						}
						throw new InvalidOperationException(Strings.EntityKey_IncorrectNumberOfKeyValuePairs(entitySet.ElementType.FullName, keyMembers.Count, 1));
					}
					else
					{
						EntityKey.ValidateTypeOfKeyValue(workspace, keyMembers[0], this._singletonKeyValue, isArgumentException, argumentName);
						if (this._keyNames[0] != keyMembers[0].Name)
						{
							if (isArgumentException)
							{
								throw new ArgumentException(Strings.EntityKey_MissingKeyValue(keyMembers[0].Name, entitySet.ElementType.FullName), argumentName);
							}
							throw new InvalidOperationException(Strings.EntityKey_MissingKeyValue(keyMembers[0].Name, entitySet.ElementType.FullName));
						}
					}
				}
				else if (this._compositeKeyValues != null)
				{
					if (keyMembers.Count != this._compositeKeyValues.Length)
					{
						if (isArgumentException)
						{
							throw new ArgumentException(Strings.EntityKey_IncorrectNumberOfKeyValuePairs(entitySet.ElementType.FullName, keyMembers.Count, this._compositeKeyValues.Length), argumentName);
						}
						throw new InvalidOperationException(Strings.EntityKey_IncorrectNumberOfKeyValuePairs(entitySet.ElementType.FullName, keyMembers.Count, this._compositeKeyValues.Length));
					}
					else
					{
						int i = 0;
						while (i < this._compositeKeyValues.Length)
						{
							EdmMember edmMember = entitySet.ElementType.KeyMembers[i];
							bool flag = false;
							for (int j = 0; j < this._compositeKeyValues.Length; j++)
							{
								if (edmMember.Name == this._keyNames[j])
								{
									EntityKey.ValidateTypeOfKeyValue(workspace, edmMember, this._compositeKeyValues[j], isArgumentException, argumentName);
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								if (isArgumentException)
								{
									throw new ArgumentException(Strings.EntityKey_MissingKeyValue(edmMember.Name, entitySet.ElementType.FullName), argumentName);
								}
								throw new InvalidOperationException(Strings.EntityKey_MissingKeyValue(edmMember.Name, entitySet.ElementType.FullName));
							}
							else
							{
								i++;
							}
						}
					}
				}
			}
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x00062E84 File Offset: 0x00061084
		private static void ValidateTypeOfKeyValue(MetadataWorkspace workspace, EdmMember keyMember, object keyValue, bool isArgumentException, string argumentName)
		{
			EdmType edmType = keyMember.TypeUsage.EdmType;
			EnumType enumType;
			if (Helper.IsPrimitiveType(edmType))
			{
				Type clrEquivalentType = ((PrimitiveType)edmType).ClrEquivalentType;
				if (clrEquivalentType != keyValue.GetType())
				{
					if (isArgumentException)
					{
						throw new ArgumentException(Strings.EntityKey_IncorrectValueType(keyMember.Name, clrEquivalentType.FullName, keyValue.GetType().FullName), argumentName);
					}
					throw new InvalidOperationException(Strings.EntityKey_IncorrectValueType(keyMember.Name, clrEquivalentType.FullName, keyValue.GetType().FullName));
				}
			}
			else if (workspace.TryGetObjectSpaceType((EnumType)edmType, out enumType))
			{
				Type clrType = enumType.ClrType;
				if (clrType != keyValue.GetType())
				{
					if (isArgumentException)
					{
						throw new ArgumentException(Strings.EntityKey_IncorrectValueType(keyMember.Name, clrType.FullName, keyValue.GetType().FullName), argumentName);
					}
					throw new InvalidOperationException(Strings.EntityKey_IncorrectValueType(keyMember.Name, clrType.FullName, keyValue.GetType().FullName));
				}
			}
			else
			{
				if (isArgumentException)
				{
					throw new ArgumentException(Strings.EntityKey_NoCorrespondingOSpaceTypeForEnumKeyMember(keyMember.Name, edmType.FullName), argumentName);
				}
				throw new InvalidOperationException(Strings.EntityKey_NoCorrespondingOSpaceTypeForEnumKeyMember(keyMember.Name, edmType.FullName));
			}
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x00062FAC File Offset: 0x000611AC
		[Conditional("DEBUG")]
		private void AssertCorrectState(EntitySetBase entitySetBase, bool isTemporary)
		{
			EntitySet entitySet = (EntitySet)entitySetBase;
			if (this._singletonKeyValue != null)
			{
				return;
			}
			if (this._compositeKeyValues != null)
			{
				for (int i = 0; i < this._compositeKeyValues.Length; i++)
				{
				}
				return;
			}
			bool isTemporary2 = this.IsTemporary;
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x00062FF0 File Offset: 0x000611F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[OnDeserializing]
		public void OnDeserializing(StreamingContext context)
		{
			if (this.RequiresDeserialization)
			{
				this.DeserializeMembers();
			}
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x00063000 File Offset: 0x00061200
		[OnDeserialized]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public void OnDeserialized(StreamingContext context)
		{
			this._entitySetName = EntityKey.LookupSingletonName(this._entitySetName);
			this._entityContainerName = EntityKey.LookupSingletonName(this._entityContainerName);
			if (this._keyNames != null)
			{
				for (int i = 0; i < this._keyNames.Length; i++)
				{
					this._keyNames[i] = EntityKey.LookupSingletonName(this._keyNames[i]);
				}
			}
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x0006305F File Offset: 0x0006125F
		internal static string LookupSingletonName(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				return EntityKey.NameLookup.GetOrAdd(name, (string n) => n);
			}
			return null;
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x00063095 File Offset: 0x00061295
		private void ValidateWritable(object instance)
		{
			if (this._isLocked || instance != null)
			{
				throw new InvalidOperationException(Strings.EntityKey_CannotChangeKey);
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x060022EA RID: 8938 RVA: 0x000630AD File Offset: 0x000612AD
		private bool RequiresDeserialization
		{
			get
			{
				return this._deserializedMembers != null;
			}
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x000630B8 File Offset: 0x000612B8
		private void DeserializeMembers()
		{
			if (this.InitializeKeyValues(new EntityKey.KeyValueReader(this._deserializedMembers), true, true))
			{
				this._deserializedMembers = null;
			}
		}

		// Token: 0x04000BF7 RID: 3063
		private string _entitySetName;

		// Token: 0x04000BF8 RID: 3064
		private string _entityContainerName;

		// Token: 0x04000BF9 RID: 3065
		private object _singletonKeyValue;

		// Token: 0x04000BFA RID: 3066
		private object[] _compositeKeyValues;

		// Token: 0x04000BFB RID: 3067
		private string[] _keyNames;

		// Token: 0x04000BFC RID: 3068
		private readonly bool _isLocked;

		// Token: 0x04000BFD RID: 3069
		[NonSerialized]
		private bool _containsByteArray;

		// Token: 0x04000BFE RID: 3070
		[NonSerialized]
		private EntityKeyMember[] _deserializedMembers;

		// Token: 0x04000BFF RID: 3071
		[NonSerialized]
		private int _hashCode;

		// Token: 0x04000C00 RID: 3072
		private static readonly EntityKey _noEntitySetKey = new EntityKey("NoEntitySetKey.NoEntitySetKey");

		// Token: 0x04000C01 RID: 3073
		private static readonly EntityKey _entityNotValidKey = new EntityKey("EntityNotValidKey.EntityNotValidKey");

		// Token: 0x04000C02 RID: 3074
		private static readonly ConcurrentDictionary<string, string> NameLookup = new ConcurrentDictionary<string, string>();

		// Token: 0x020009B0 RID: 2480
		private class KeyValueReader : IEnumerable<KeyValuePair<string, object>>, IEnumerable
		{
			// Token: 0x06005F12 RID: 24338 RVA: 0x001472D3 File Offset: 0x001454D3
			public KeyValueReader(IEnumerable<EntityKeyMember> enumerator)
			{
				this._enumerator = enumerator;
			}

			// Token: 0x06005F13 RID: 24339 RVA: 0x001472E2 File Offset: 0x001454E2
			public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
			{
				foreach (EntityKeyMember entityKeyMember in this._enumerator)
				{
					if (entityKeyMember != null)
					{
						yield return new KeyValuePair<string, object>(entityKeyMember.Key, entityKeyMember.Value);
					}
				}
				IEnumerator<EntityKeyMember> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06005F14 RID: 24340 RVA: 0x001472F1 File Offset: 0x001454F1
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040027EA RID: 10218
			private readonly IEnumerable<EntityKeyMember> _enumerator;
		}
	}
}
