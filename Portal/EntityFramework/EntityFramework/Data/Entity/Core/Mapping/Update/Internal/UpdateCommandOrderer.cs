using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005D5 RID: 1493
	internal class UpdateCommandOrderer : Graph<UpdateCommand>
	{
		// Token: 0x060047EB RID: 18411 RVA: 0x000FF080 File Offset: 0x000FD280
		internal UpdateCommandOrderer(IEnumerable<UpdateCommand> commands, UpdateTranslator translator)
			: base(EqualityComparer<UpdateCommand>.Default)
		{
			this._translator = translator;
			this._keyComparer = new UpdateCommandOrderer.ForeignKeyValueComparer(this._translator.KeyComparer);
			HashSet<EntitySet> hashSet = new HashSet<EntitySet>();
			HashSet<EntityContainer> hashSet2 = new HashSet<EntityContainer>();
			foreach (UpdateCommand updateCommand in commands)
			{
				if (updateCommand.Table != null)
				{
					hashSet.Add(updateCommand.Table);
					hashSet2.Add(updateCommand.Table.EntityContainer);
				}
				base.AddVertex(updateCommand);
				if (updateCommand.Kind == UpdateCommandKind.Function)
				{
					this._hasFunctionCommands = true;
				}
			}
			UpdateCommandOrderer.InitializeForeignKeyMaps(hashSet2, hashSet, out this._sourceMap, out this._targetMap);
			this.AddServerGenDependencies();
			this.AddForeignKeyDependencies();
			if (this._hasFunctionCommands)
			{
				this.AddModelDependencies();
			}
		}

		// Token: 0x060047EC RID: 18412 RVA: 0x000FF160 File Offset: 0x000FD360
		private static void InitializeForeignKeyMaps(HashSet<EntityContainer> containers, HashSet<EntitySet> tables, out KeyToListMap<EntitySetBase, ReferentialConstraint> sourceMap, out KeyToListMap<EntitySetBase, ReferentialConstraint> targetMap)
		{
			sourceMap = new KeyToListMap<EntitySetBase, ReferentialConstraint>(EqualityComparer<EntitySetBase>.Default);
			targetMap = new KeyToListMap<EntitySetBase, ReferentialConstraint>(EqualityComparer<EntitySetBase>.Default);
			foreach (EntityContainer entityContainer in containers)
			{
				foreach (EntitySetBase entitySetBase in entityContainer.BaseEntitySets)
				{
					AssociationSet associationSet = entitySetBase as AssociationSet;
					if (associationSet != null)
					{
						AssociationSetEnd associationSetEnd = null;
						AssociationSetEnd associationSetEnd2 = null;
						ReadOnlyMetadataCollection<AssociationSetEnd> associationSetEnds = associationSet.AssociationSetEnds;
						if (2 == associationSetEnds.Count)
						{
							AssociationType elementType = associationSet.ElementType;
							bool flag = false;
							ReferentialConstraint referentialConstraint = null;
							foreach (ReferentialConstraint referentialConstraint2 in elementType.ReferentialConstraints)
							{
								if (!flag)
								{
									flag = true;
								}
								associationSetEnd = associationSet.AssociationSetEnds[referentialConstraint2.ToRole.Name];
								associationSetEnd2 = associationSet.AssociationSetEnds[referentialConstraint2.FromRole.Name];
								referentialConstraint = referentialConstraint2;
							}
							if (associationSetEnd2 != null && associationSetEnd != null && tables.Contains(associationSetEnd2.EntitySet) && tables.Contains(associationSetEnd.EntitySet))
							{
								sourceMap.Add(associationSetEnd.EntitySet, referentialConstraint);
								targetMap.Add(associationSetEnd2.EntitySet, referentialConstraint);
							}
						}
					}
				}
			}
		}

		// Token: 0x060047ED RID: 18413 RVA: 0x000FF314 File Offset: 0x000FD514
		private void AddServerGenDependencies()
		{
			Dictionary<int, UpdateCommand> dictionary = new Dictionary<int, UpdateCommand>();
			foreach (UpdateCommand updateCommand in base.Vertices)
			{
				foreach (int num in updateCommand.OutputIdentifiers)
				{
					try
					{
						dictionary.Add(num, updateCommand);
					}
					catch (ArgumentException ex)
					{
						throw new UpdateException(Strings.Update_AmbiguousServerGenIdentifier, ex, updateCommand.GetStateEntries(this._translator).Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
					}
				}
			}
			foreach (UpdateCommand updateCommand2 in base.Vertices)
			{
				foreach (int num2 in updateCommand2.InputIdentifiers)
				{
					UpdateCommand updateCommand3;
					if (dictionary.TryGetValue(num2, out updateCommand3))
					{
						base.AddEdge(updateCommand3, updateCommand2);
					}
				}
			}
		}

		// Token: 0x060047EE RID: 18414 RVA: 0x000FF458 File Offset: 0x000FD658
		private void AddForeignKeyDependencies()
		{
			KeyToListMap<UpdateCommandOrderer.ForeignKeyValue, UpdateCommand> keyToListMap = this.DetermineForeignKeyPredecessors();
			this.AddForeignKeyEdges(keyToListMap);
		}

		// Token: 0x060047EF RID: 18415 RVA: 0x000FF474 File Offset: 0x000FD674
		private void AddForeignKeyEdges(KeyToListMap<UpdateCommandOrderer.ForeignKeyValue, UpdateCommand> predecessors)
		{
			foreach (DynamicUpdateCommand dynamicUpdateCommand in base.Vertices.OfType<DynamicUpdateCommand>())
			{
				if (dynamicUpdateCommand.Operator == ModificationOperator.Update || ModificationOperator.Insert == dynamicUpdateCommand.Operator)
				{
					foreach (ReferentialConstraint referentialConstraint in this._sourceMap.EnumerateValues(dynamicUpdateCommand.Table))
					{
						UpdateCommandOrderer.ForeignKeyValue foreignKeyValue;
						UpdateCommandOrderer.ForeignKeyValue foreignKeyValue2;
						if (UpdateCommandOrderer.ForeignKeyValue.TryCreateSourceKey(referentialConstraint, dynamicUpdateCommand.CurrentValues, true, out foreignKeyValue) && (dynamicUpdateCommand.Operator != ModificationOperator.Update || !UpdateCommandOrderer.ForeignKeyValue.TryCreateSourceKey(referentialConstraint, dynamicUpdateCommand.OriginalValues, true, out foreignKeyValue2) || !this._keyComparer.Equals(foreignKeyValue2, foreignKeyValue)))
						{
							foreach (UpdateCommand updateCommand in predecessors.EnumerateValues(foreignKeyValue))
							{
								if (updateCommand != dynamicUpdateCommand)
								{
									base.AddEdge(updateCommand, dynamicUpdateCommand);
								}
							}
						}
					}
				}
				if (dynamicUpdateCommand.Operator == ModificationOperator.Update || ModificationOperator.Delete == dynamicUpdateCommand.Operator)
				{
					foreach (ReferentialConstraint referentialConstraint2 in this._targetMap.EnumerateValues(dynamicUpdateCommand.Table))
					{
						UpdateCommandOrderer.ForeignKeyValue foreignKeyValue3;
						UpdateCommandOrderer.ForeignKeyValue foreignKeyValue4;
						if (UpdateCommandOrderer.ForeignKeyValue.TryCreateTargetKey(referentialConstraint2, dynamicUpdateCommand.OriginalValues, false, out foreignKeyValue3) && (dynamicUpdateCommand.Operator != ModificationOperator.Update || !UpdateCommandOrderer.ForeignKeyValue.TryCreateTargetKey(referentialConstraint2, dynamicUpdateCommand.CurrentValues, false, out foreignKeyValue4) || !this._keyComparer.Equals(foreignKeyValue4, foreignKeyValue3)))
						{
							foreach (UpdateCommand updateCommand2 in predecessors.EnumerateValues(foreignKeyValue3))
							{
								if (updateCommand2 != dynamicUpdateCommand)
								{
									base.AddEdge(updateCommand2, dynamicUpdateCommand);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060047F0 RID: 18416 RVA: 0x000FF6C4 File Offset: 0x000FD8C4
		private KeyToListMap<UpdateCommandOrderer.ForeignKeyValue, UpdateCommand> DetermineForeignKeyPredecessors()
		{
			KeyToListMap<UpdateCommandOrderer.ForeignKeyValue, UpdateCommand> keyToListMap = new KeyToListMap<UpdateCommandOrderer.ForeignKeyValue, UpdateCommand>(this._keyComparer);
			foreach (DynamicUpdateCommand dynamicUpdateCommand in base.Vertices.OfType<DynamicUpdateCommand>())
			{
				if (dynamicUpdateCommand.Operator == ModificationOperator.Update || ModificationOperator.Insert == dynamicUpdateCommand.Operator)
				{
					foreach (ReferentialConstraint referentialConstraint in this._targetMap.EnumerateValues(dynamicUpdateCommand.Table))
					{
						UpdateCommandOrderer.ForeignKeyValue foreignKeyValue;
						UpdateCommandOrderer.ForeignKeyValue foreignKeyValue2;
						if (UpdateCommandOrderer.ForeignKeyValue.TryCreateTargetKey(referentialConstraint, dynamicUpdateCommand.CurrentValues, true, out foreignKeyValue) && (dynamicUpdateCommand.Operator != ModificationOperator.Update || !UpdateCommandOrderer.ForeignKeyValue.TryCreateTargetKey(referentialConstraint, dynamicUpdateCommand.OriginalValues, true, out foreignKeyValue2) || !this._keyComparer.Equals(foreignKeyValue2, foreignKeyValue)))
						{
							keyToListMap.Add(foreignKeyValue, dynamicUpdateCommand);
						}
					}
				}
				if (dynamicUpdateCommand.Operator == ModificationOperator.Update || ModificationOperator.Delete == dynamicUpdateCommand.Operator)
				{
					foreach (ReferentialConstraint referentialConstraint2 in this._sourceMap.EnumerateValues(dynamicUpdateCommand.Table))
					{
						UpdateCommandOrderer.ForeignKeyValue foreignKeyValue3;
						UpdateCommandOrderer.ForeignKeyValue foreignKeyValue4;
						if (UpdateCommandOrderer.ForeignKeyValue.TryCreateSourceKey(referentialConstraint2, dynamicUpdateCommand.OriginalValues, false, out foreignKeyValue3) && (dynamicUpdateCommand.Operator != ModificationOperator.Update || !UpdateCommandOrderer.ForeignKeyValue.TryCreateSourceKey(referentialConstraint2, dynamicUpdateCommand.CurrentValues, false, out foreignKeyValue4) || !this._keyComparer.Equals(foreignKeyValue4, foreignKeyValue3)))
						{
							keyToListMap.Add(foreignKeyValue3, dynamicUpdateCommand);
						}
					}
				}
			}
			return keyToListMap;
		}

		// Token: 0x060047F1 RID: 18417 RVA: 0x000FF878 File Offset: 0x000FDA78
		private void AddModelDependencies()
		{
			KeyToListMap<EntityKey, UpdateCommand> keyToListMap = new KeyToListMap<EntityKey, UpdateCommand>(EqualityComparer<EntityKey>.Default);
			KeyToListMap<EntityKey, UpdateCommand> keyToListMap2 = new KeyToListMap<EntityKey, UpdateCommand>(EqualityComparer<EntityKey>.Default);
			KeyToListMap<EntityKey, UpdateCommand> keyToListMap3 = new KeyToListMap<EntityKey, UpdateCommand>(EqualityComparer<EntityKey>.Default);
			KeyToListMap<EntityKey, UpdateCommand> keyToListMap4 = new KeyToListMap<EntityKey, UpdateCommand>(EqualityComparer<EntityKey>.Default);
			foreach (UpdateCommand updateCommand in base.Vertices)
			{
				updateCommand.GetRequiredAndProducedEntities(this._translator, keyToListMap, keyToListMap2, keyToListMap3, keyToListMap4);
			}
			this.AddModelDependencies(keyToListMap, keyToListMap3);
			this.AddModelDependencies(keyToListMap4, keyToListMap2);
		}

		// Token: 0x060047F2 RID: 18418 RVA: 0x000FF910 File Offset: 0x000FDB10
		private void AddModelDependencies(KeyToListMap<EntityKey, UpdateCommand> producedMap, KeyToListMap<EntityKey, UpdateCommand> requiredMap)
		{
			foreach (KeyValuePair<EntityKey, List<UpdateCommand>> keyValuePair in requiredMap.KeyValuePairs)
			{
				EntityKey key = keyValuePair.Key;
				List<UpdateCommand> value = keyValuePair.Value;
				foreach (UpdateCommand updateCommand in producedMap.EnumerateValues(key))
				{
					foreach (UpdateCommand updateCommand2 in value)
					{
						if (updateCommand != updateCommand2 && (updateCommand.Kind == UpdateCommandKind.Function || updateCommand2.Kind == UpdateCommandKind.Function))
						{
							base.AddEdge(updateCommand, updateCommand2);
						}
					}
				}
			}
		}

		// Token: 0x04001994 RID: 6548
		private readonly UpdateCommandOrderer.ForeignKeyValueComparer _keyComparer;

		// Token: 0x04001995 RID: 6549
		private readonly KeyToListMap<EntitySetBase, ReferentialConstraint> _sourceMap;

		// Token: 0x04001996 RID: 6550
		private readonly KeyToListMap<EntitySetBase, ReferentialConstraint> _targetMap;

		// Token: 0x04001997 RID: 6551
		private readonly bool _hasFunctionCommands;

		// Token: 0x04001998 RID: 6552
		private readonly UpdateTranslator _translator;

		// Token: 0x02000C0D RID: 3085
		private struct ForeignKeyValue
		{
			// Token: 0x06006940 RID: 26944 RVA: 0x00167DD4 File Offset: 0x00165FD4
			private ForeignKeyValue(ReferentialConstraint metadata, PropagatorResult record, bool isTarget, bool isInsert)
			{
				this.Metadata = metadata;
				IList<EdmProperty> list = (isTarget ? metadata.FromProperties : metadata.ToProperties);
				PropagatorResult[] array = new PropagatorResult[list.Count];
				bool flag = false;
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = record.GetMemberValue(list[i]);
					if (array[i].IsNull)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					this.Key = null;
				}
				else
				{
					this.Key = new CompositeKey(array);
				}
				this.IsInsert = isInsert;
			}

			// Token: 0x06006941 RID: 26945 RVA: 0x00167E54 File Offset: 0x00166054
			internal static bool TryCreateTargetKey(ReferentialConstraint metadata, PropagatorResult record, bool isInsert, out UpdateCommandOrderer.ForeignKeyValue key)
			{
				key = new UpdateCommandOrderer.ForeignKeyValue(metadata, record, true, isInsert);
				return key.Key != null;
			}

			// Token: 0x06006942 RID: 26946 RVA: 0x00167E70 File Offset: 0x00166070
			internal static bool TryCreateSourceKey(ReferentialConstraint metadata, PropagatorResult record, bool isInsert, out UpdateCommandOrderer.ForeignKeyValue key)
			{
				key = new UpdateCommandOrderer.ForeignKeyValue(metadata, record, false, isInsert);
				return key.Key != null;
			}

			// Token: 0x04002FAD RID: 12205
			internal readonly ReferentialConstraint Metadata;

			// Token: 0x04002FAE RID: 12206
			internal readonly CompositeKey Key;

			// Token: 0x04002FAF RID: 12207
			internal readonly bool IsInsert;
		}

		// Token: 0x02000C0E RID: 3086
		private class ForeignKeyValueComparer : IEqualityComparer<UpdateCommandOrderer.ForeignKeyValue>
		{
			// Token: 0x06006943 RID: 26947 RVA: 0x00167E8C File Offset: 0x0016608C
			internal ForeignKeyValueComparer(IEqualityComparer<CompositeKey> baseComparer)
			{
				this._baseComparer = baseComparer;
			}

			// Token: 0x06006944 RID: 26948 RVA: 0x00167E9B File Offset: 0x0016609B
			public bool Equals(UpdateCommandOrderer.ForeignKeyValue x, UpdateCommandOrderer.ForeignKeyValue y)
			{
				return x.IsInsert == y.IsInsert && x.Metadata == y.Metadata && this._baseComparer.Equals(x.Key, y.Key);
			}

			// Token: 0x06006945 RID: 26949 RVA: 0x00167ED2 File Offset: 0x001660D2
			public int GetHashCode(UpdateCommandOrderer.ForeignKeyValue obj)
			{
				return this._baseComparer.GetHashCode(obj.Key);
			}

			// Token: 0x04002FB0 RID: 12208
			private readonly IEqualityComparer<CompositeKey> _baseComparer;
		}
	}
}
