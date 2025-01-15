using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005C9 RID: 1481
	internal class KeyManager
	{
		// Token: 0x06004784 RID: 18308 RVA: 0x000FD890 File Offset: 0x000FBA90
		internal int GetCliqueIdentifier(int identifier)
		{
			KeyManager.Partition partition = this._identifiers[identifier].Partition;
			if (partition != null)
			{
				return partition.PartitionId;
			}
			return identifier;
		}

		// Token: 0x06004785 RID: 18309 RVA: 0x000FD8BC File Offset: 0x000FBABC
		internal void AddReferentialConstraint(IEntityStateEntry dependentStateEntry, int dependentIdentifier, int principalIdentifier)
		{
			KeyManager.IdentifierInfo identifierInfo = this._identifiers[dependentIdentifier];
			if (dependentIdentifier != principalIdentifier)
			{
				this.AssociateNodes(dependentIdentifier, principalIdentifier);
				KeyManager.LinkedList<int>.Add(ref identifierInfo.References, principalIdentifier);
				KeyManager.LinkedList<int>.Add(ref this._identifiers[principalIdentifier].ReferencedBy, dependentIdentifier);
			}
			KeyManager.LinkedList<IEntityStateEntry>.Add(ref identifierInfo.DependentStateEntries, dependentStateEntry);
		}

		// Token: 0x06004786 RID: 18310 RVA: 0x000FD911 File Offset: 0x000FBB11
		internal void RegisterIdentifierOwner(PropagatorResult owner)
		{
			this._identifiers[owner.Identifier].Owner = owner;
		}

		// Token: 0x06004787 RID: 18311 RVA: 0x000FD92A File Offset: 0x000FBB2A
		internal bool TryGetIdentifierOwner(int identifier, out PropagatorResult owner)
		{
			owner = this._identifiers[identifier].Owner;
			return owner != null;
		}

		// Token: 0x06004788 RID: 18312 RVA: 0x000FD944 File Offset: 0x000FBB44
		internal int GetKeyIdentifierForMemberOffset(EntityKey entityKey, int memberOffset, int keyMemberCount)
		{
			int num;
			if (!this._keyIdentifiers.TryGetValue(entityKey, out num))
			{
				num = this._identifiers.Count;
				for (int i = 0; i < keyMemberCount; i++)
				{
					this._identifiers.Add(new KeyManager.IdentifierInfo());
				}
				this._keyIdentifiers.Add(entityKey, num);
			}
			num += memberOffset;
			return num;
		}

		// Token: 0x06004789 RID: 18313 RVA: 0x000FD99C File Offset: 0x000FBB9C
		internal int GetKeyIdentifierForMember(EntityKey entityKey, string member, bool currentValues)
		{
			Tuple<EntityKey, string, bool> tuple = Tuple.Create<EntityKey, string, bool>(entityKey, member, currentValues);
			int count;
			if (!this._foreignKeyIdentifiers.TryGetValue(tuple, out count))
			{
				count = this._identifiers.Count;
				this._identifiers.Add(new KeyManager.IdentifierInfo());
				this._foreignKeyIdentifiers.Add(tuple, count);
			}
			return count;
		}

		// Token: 0x0600478A RID: 18314 RVA: 0x000FD9EC File Offset: 0x000FBBEC
		internal IEnumerable<IEntityStateEntry> GetDependentStateEntries(int identifier)
		{
			return KeyManager.LinkedList<IEntityStateEntry>.Enumerate(this._identifiers[identifier].DependentStateEntries);
		}

		// Token: 0x0600478B RID: 18315 RVA: 0x000FDA04 File Offset: 0x000FBC04
		internal object GetPrincipalValue(PropagatorResult result)
		{
			int identifier = result.Identifier;
			if (-1 == identifier)
			{
				return result.GetSimpleValue();
			}
			bool flag = true;
			object obj = null;
			foreach (int num in this.GetPrincipals(identifier))
			{
				PropagatorResult owner = this._identifiers[num].Owner;
				if (owner != null)
				{
					if (flag)
					{
						obj = owner.GetSimpleValue();
						flag = false;
					}
					else if (!ByValueEqualityComparer.Default.Equals(obj, owner.GetSimpleValue()))
					{
						throw new ConstraintException(Strings.Update_ReferentialConstraintIntegrityViolation);
					}
				}
			}
			if (flag)
			{
				obj = result.GetSimpleValue();
			}
			return obj;
		}

		// Token: 0x0600478C RID: 18316 RVA: 0x000FDAB4 File Offset: 0x000FBCB4
		internal IEnumerable<int> GetPrincipals(int identifier)
		{
			return this.WalkGraph(identifier, (KeyManager.IdentifierInfo info) => info.References, true);
		}

		// Token: 0x0600478D RID: 18317 RVA: 0x000FDADD File Offset: 0x000FBCDD
		internal IEnumerable<int> GetDirectReferences(int identifier)
		{
			KeyManager.LinkedList<int> references = this._identifiers[identifier].References;
			foreach (int num in KeyManager.LinkedList<int>.Enumerate(references))
			{
				yield return num;
			}
			IEnumerator<int> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600478E RID: 18318 RVA: 0x000FDAF4 File Offset: 0x000FBCF4
		internal IEnumerable<int> GetDependents(int identifier)
		{
			return this.WalkGraph(identifier, (KeyManager.IdentifierInfo info) => info.ReferencedBy, false);
		}

		// Token: 0x0600478F RID: 18319 RVA: 0x000FDB1D File Offset: 0x000FBD1D
		private IEnumerable<int> WalkGraph(int identifier, Func<KeyManager.IdentifierInfo, KeyManager.LinkedList<int>> successorFunction, bool leavesOnly)
		{
			Stack<int> stack = new Stack<int>();
			stack.Push(identifier);
			while (stack.Count > 0)
			{
				int num = stack.Pop();
				KeyManager.LinkedList<int> linkedList = successorFunction(this._identifiers[num]);
				if (linkedList != null)
				{
					foreach (int num2 in KeyManager.LinkedList<int>.Enumerate(linkedList))
					{
						stack.Push(num2);
					}
					if (!leavesOnly)
					{
						yield return num;
					}
				}
				else
				{
					yield return num;
				}
			}
			yield break;
		}

		// Token: 0x06004790 RID: 18320 RVA: 0x000FDB42 File Offset: 0x000FBD42
		internal bool HasPrincipals(int identifier)
		{
			return this._identifiers[identifier].References != null;
		}

		// Token: 0x06004791 RID: 18321 RVA: 0x000FDB58 File Offset: 0x000FBD58
		internal void ValidateReferentialIntegrityGraphAcyclic()
		{
			byte[] array = new byte[this._identifiers.Count];
			int i = 0;
			int count = this._identifiers.Count;
			while (i < count)
			{
				if (array[i] == 0)
				{
					this.ValidateReferentialIntegrityGraphAcyclic(i, array, null);
				}
				i++;
			}
		}

		// Token: 0x06004792 RID: 18322 RVA: 0x000FDB9C File Offset: 0x000FBD9C
		internal void RegisterKeyValueForAddedEntity(IEntityStateEntry addedEntry)
		{
			EntityKey entityKey = addedEntry.EntityKey;
			ReadOnlyMetadataCollection<EdmMember> keyMembers = addedEntry.EntitySet.ElementType.KeyMembers;
			CurrentValueRecord currentValues = addedEntry.CurrentValues;
			object[] array = new object[keyMembers.Count];
			bool flag = false;
			int i = 0;
			int count = keyMembers.Count;
			while (i < count)
			{
				int ordinal = currentValues.GetOrdinal(keyMembers[i].Name);
				if (currentValues.IsDBNull(ordinal))
				{
					flag = true;
					break;
				}
				array[i] = currentValues.GetValue(ordinal);
				i++;
			}
			if (flag)
			{
				return;
			}
			EntityKey entityKey2 = ((array.Length == 1) ? new EntityKey(addedEntry.EntitySet, array[0]) : new EntityKey(addedEntry.EntitySet, array));
			if (this._valueKeyToTempKey.ContainsKey(entityKey2))
			{
				this._valueKeyToTempKey[entityKey2] = null;
				return;
			}
			this._valueKeyToTempKey.Add(entityKey2, entityKey);
		}

		// Token: 0x06004793 RID: 18323 RVA: 0x000FDC76 File Offset: 0x000FBE76
		internal bool TryGetTempKey(EntityKey valueKey, out EntityKey tempKey)
		{
			return this._valueKeyToTempKey.TryGetValue(valueKey, out tempKey);
		}

		// Token: 0x06004794 RID: 18324 RVA: 0x000FDC88 File Offset: 0x000FBE88
		private void ValidateReferentialIntegrityGraphAcyclic(int node, byte[] color, KeyManager.LinkedList<int> parent)
		{
			color[node] = 2;
			KeyManager.LinkedList<int>.Add(ref parent, node);
			foreach (int num in KeyManager.LinkedList<int>.Enumerate(this._identifiers[node].References))
			{
				byte b = color[num];
				if (b != 0)
				{
					if (b == 2)
					{
						List<IEntityStateEntry> list = new List<IEntityStateEntry>();
						foreach (int num2 in KeyManager.LinkedList<int>.Enumerate(parent))
						{
							PropagatorResult owner = this._identifiers[num2].Owner;
							if (owner != null)
							{
								list.Add(owner.StateEntry);
							}
							if (num2 == num)
							{
								break;
							}
						}
						throw new UpdateException(Strings.Update_CircularRelationships, null, list.Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
					}
				}
				else
				{
					this.ValidateReferentialIntegrityGraphAcyclic(num, color, parent);
				}
			}
			color[node] = 1;
		}

		// Token: 0x06004795 RID: 18325 RVA: 0x000FDD94 File Offset: 0x000FBF94
		internal void AssociateNodes(int firstId, int secondId)
		{
			if (firstId == secondId)
			{
				return;
			}
			KeyManager.Partition partition = this._identifiers[firstId].Partition;
			if (partition != null)
			{
				KeyManager.Partition partition2 = this._identifiers[secondId].Partition;
				if (partition2 != null)
				{
					partition.Merge(this, partition2);
					return;
				}
				partition.AddNode(this, secondId);
				return;
			}
			else
			{
				KeyManager.Partition partition3 = this._identifiers[secondId].Partition;
				if (partition3 != null)
				{
					partition3.AddNode(this, firstId);
					return;
				}
				KeyManager.Partition.CreatePartition(this, firstId, secondId);
				return;
			}
		}

		// Token: 0x04001969 RID: 6505
		private readonly Dictionary<Tuple<EntityKey, string, bool>, int> _foreignKeyIdentifiers = new Dictionary<Tuple<EntityKey, string, bool>, int>();

		// Token: 0x0400196A RID: 6506
		private readonly Dictionary<EntityKey, EntityKey> _valueKeyToTempKey = new Dictionary<EntityKey, EntityKey>();

		// Token: 0x0400196B RID: 6507
		private readonly Dictionary<EntityKey, int> _keyIdentifiers = new Dictionary<EntityKey, int>();

		// Token: 0x0400196C RID: 6508
		private readonly List<KeyManager.IdentifierInfo> _identifiers = new List<KeyManager.IdentifierInfo>
		{
			new KeyManager.IdentifierInfo()
		};

		// Token: 0x0400196D RID: 6509
		private const byte White = 0;

		// Token: 0x0400196E RID: 6510
		private const byte Black = 1;

		// Token: 0x0400196F RID: 6511
		private const byte Gray = 2;

		// Token: 0x02000BFD RID: 3069
		private sealed class Partition
		{
			// Token: 0x060068CB RID: 26827 RVA: 0x001661E7 File Offset: 0x001643E7
			private Partition(int partitionId)
			{
				this._nodeIds = new List<int>(2);
				this.PartitionId = partitionId;
			}

			// Token: 0x060068CC RID: 26828 RVA: 0x00166202 File Offset: 0x00164402
			internal static void CreatePartition(KeyManager manager, int firstId, int secondId)
			{
				KeyManager.Partition partition = new KeyManager.Partition(firstId);
				partition.AddNode(manager, firstId);
				partition.AddNode(manager, secondId);
			}

			// Token: 0x060068CD RID: 26829 RVA: 0x00166219 File Offset: 0x00164419
			internal void AddNode(KeyManager manager, int nodeId)
			{
				this._nodeIds.Add(nodeId);
				manager._identifiers[nodeId].Partition = this;
			}

			// Token: 0x060068CE RID: 26830 RVA: 0x0016623C File Offset: 0x0016443C
			internal void Merge(KeyManager manager, KeyManager.Partition other)
			{
				if (other.PartitionId == this.PartitionId)
				{
					return;
				}
				foreach (int num in other._nodeIds)
				{
					this.AddNode(manager, num);
				}
			}

			// Token: 0x04002F72 RID: 12146
			internal readonly int PartitionId;

			// Token: 0x04002F73 RID: 12147
			private readonly List<int> _nodeIds;
		}

		// Token: 0x02000BFE RID: 3070
		private sealed class LinkedList<T>
		{
			// Token: 0x060068CF RID: 26831 RVA: 0x001662A0 File Offset: 0x001644A0
			private LinkedList(T value, KeyManager.LinkedList<T> previous)
			{
				this._value = value;
				this._previous = previous;
			}

			// Token: 0x060068D0 RID: 26832 RVA: 0x001662B6 File Offset: 0x001644B6
			internal static IEnumerable<T> Enumerate(KeyManager.LinkedList<T> current)
			{
				while (current != null)
				{
					yield return current._value;
					current = current._previous;
				}
				yield break;
			}

			// Token: 0x060068D1 RID: 26833 RVA: 0x001662C6 File Offset: 0x001644C6
			internal static void Add(ref KeyManager.LinkedList<T> list, T value)
			{
				list = new KeyManager.LinkedList<T>(value, list);
			}

			// Token: 0x04002F74 RID: 12148
			private readonly T _value;

			// Token: 0x04002F75 RID: 12149
			private readonly KeyManager.LinkedList<T> _previous;
		}

		// Token: 0x02000BFF RID: 3071
		private sealed class IdentifierInfo
		{
			// Token: 0x04002F76 RID: 12150
			internal KeyManager.Partition Partition;

			// Token: 0x04002F77 RID: 12151
			internal PropagatorResult Owner;

			// Token: 0x04002F78 RID: 12152
			internal KeyManager.LinkedList<IEntityStateEntry> DependentStateEntries;

			// Token: 0x04002F79 RID: 12153
			internal KeyManager.LinkedList<int> References;

			// Token: 0x04002F7A RID: 12154
			internal KeyManager.LinkedList<int> ReferencedBy;
		}
	}
}
