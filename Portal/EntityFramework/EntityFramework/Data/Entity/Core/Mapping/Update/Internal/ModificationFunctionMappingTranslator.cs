using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005C6 RID: 1478
	internal abstract class ModificationFunctionMappingTranslator
	{
		// Token: 0x06004766 RID: 18278
		internal abstract FunctionUpdateCommand Translate(UpdateTranslator translator, ExtractedStateEntry stateEntry);

		// Token: 0x06004767 RID: 18279 RVA: 0x000FCD59 File Offset: 0x000FAF59
		internal static ModificationFunctionMappingTranslator CreateEntitySetTranslator(EntitySetMapping setMapping)
		{
			return new ModificationFunctionMappingTranslator.EntitySetTranslator(setMapping);
		}

		// Token: 0x06004768 RID: 18280 RVA: 0x000FCD61 File Offset: 0x000FAF61
		internal static ModificationFunctionMappingTranslator CreateAssociationSetTranslator(AssociationSetMapping setMapping)
		{
			return new ModificationFunctionMappingTranslator.AssociationSetTranslator(setMapping);
		}

		// Token: 0x02000BF5 RID: 3061
		private sealed class EntitySetTranslator : ModificationFunctionMappingTranslator
		{
			// Token: 0x060068A7 RID: 26791 RVA: 0x00164F84 File Offset: 0x00163184
			internal EntitySetTranslator(EntitySetMapping setMapping)
			{
				this.m_typeMappings = new Dictionary<EntityType, EntityTypeModificationFunctionMapping>();
				foreach (EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping in setMapping.ModificationFunctionMappings)
				{
					this.m_typeMappings.Add(entityTypeModificationFunctionMapping.EntityType, entityTypeModificationFunctionMapping);
				}
			}

			// Token: 0x060068A8 RID: 26792 RVA: 0x00164FF0 File Offset: 0x001631F0
			internal override FunctionUpdateCommand Translate(UpdateTranslator translator, ExtractedStateEntry stateEntry)
			{
				ModificationFunctionMapping item = this.GetFunctionMapping(stateEntry).Item2;
				EntityKey entityKey = stateEntry.Source.EntityKey;
				HashSet<IEntityStateEntry> hashSet = new HashSet<IEntityStateEntry> { stateEntry.Source };
				IEnumerable<Tuple<AssociationEndMember, IEntityStateEntry>> enumerable = from end in item.CollocatedAssociationSetEnds
					join candidateEntry in translator.GetRelationships(entityKey) on end.CorrespondingAssociationEndMember.DeclaringType equals candidateEntry.EntitySet.ElementType
					select Tuple.Create<AssociationEndMember, IEntityStateEntry>(end.CorrespondingAssociationEndMember, candidateEntry);
				Dictionary<AssociationEndMember, IEntityStateEntry> dictionary = new Dictionary<AssociationEndMember, IEntityStateEntry>();
				Dictionary<AssociationEndMember, IEntityStateEntry> dictionary2 = new Dictionary<AssociationEndMember, IEntityStateEntry>();
				foreach (Tuple<AssociationEndMember, IEntityStateEntry> tuple in enumerable)
				{
					ModificationFunctionMappingTranslator.EntitySetTranslator.ProcessReferenceCandidate(entityKey, hashSet, dictionary, dictionary2, tuple.Item1, tuple.Item2);
				}
				FunctionUpdateCommand functionUpdateCommand;
				if (hashSet.All((IEntityStateEntry e) => e.State == EntityState.Unchanged))
				{
					functionUpdateCommand = null;
				}
				else
				{
					functionUpdateCommand = new FunctionUpdateCommand(item, translator, new ReadOnlyCollection<IEntityStateEntry>(hashSet.ToList<IEntityStateEntry>()), stateEntry);
					ModificationFunctionMappingTranslator.EntitySetTranslator.BindFunctionParameters(translator, stateEntry, item, functionUpdateCommand, dictionary, dictionary2);
					if (item.ResultBindings != null)
					{
						foreach (ModificationFunctionResultBinding modificationFunctionResultBinding in item.ResultBindings)
						{
							PropagatorResult memberValue = stateEntry.Current.GetMemberValue(modificationFunctionResultBinding.Property);
							functionUpdateCommand.AddResultColumn(translator, modificationFunctionResultBinding.ColumnName, memberValue);
						}
					}
				}
				return functionUpdateCommand;
			}

			// Token: 0x060068A9 RID: 26793 RVA: 0x001651B0 File Offset: 0x001633B0
			private static void ProcessReferenceCandidate(EntityKey source, HashSet<IEntityStateEntry> stateEntries, Dictionary<AssociationEndMember, IEntityStateEntry> currentReferenceEnd, Dictionary<AssociationEndMember, IEntityStateEntry> originalReferenceEnd, AssociationEndMember endMember, IEntityStateEntry candidateEntry)
			{
				Func<DbDataRecord, int, EntityKey> getEntityKey = (DbDataRecord record, int ordinal) => (EntityKey)record[ordinal];
				Action<DbDataRecord, Action<IEntityStateEntry>> action = delegate(DbDataRecord record, Action<IEntityStateEntry> registerTarget)
				{
					int ordinal = record.GetOrdinal(endMember.Name);
					int num = ((ordinal == 0) ? 1 : 0);
					if (getEntityKey(record, num) == source)
					{
						stateEntries.Add(candidateEntry);
						registerTarget(candidateEntry);
					}
				};
				EntityState state = candidateEntry.State;
				if (state == EntityState.Unchanged)
				{
					action(candidateEntry.CurrentValues, delegate(IEntityStateEntry target)
					{
						currentReferenceEnd.Add(endMember, target);
						originalReferenceEnd.Add(endMember, target);
					});
					return;
				}
				if (state == EntityState.Added)
				{
					action(candidateEntry.CurrentValues, delegate(IEntityStateEntry target)
					{
						currentReferenceEnd.Add(endMember, target);
					});
					return;
				}
				if (state != EntityState.Deleted)
				{
					return;
				}
				action(candidateEntry.OriginalValues, delegate(IEntityStateEntry target)
				{
					originalReferenceEnd.Add(endMember, target);
				});
			}

			// Token: 0x060068AA RID: 26794 RVA: 0x00165294 File Offset: 0x00163494
			private Tuple<EntityTypeModificationFunctionMapping, ModificationFunctionMapping> GetFunctionMapping(ExtractedStateEntry stateEntry)
			{
				EntityType entityType;
				if (stateEntry.Current != null)
				{
					entityType = (EntityType)stateEntry.Current.StructuralType;
				}
				else
				{
					entityType = (EntityType)stateEntry.Original.StructuralType;
				}
				EntityTypeModificationFunctionMapping entityTypeModificationFunctionMapping = this.m_typeMappings[entityType];
				EntityState state = stateEntry.State;
				ModificationFunctionMapping modificationFunctionMapping;
				if (state <= EntityState.Added)
				{
					if (state != EntityState.Unchanged)
					{
						if (state != EntityState.Added)
						{
							goto IL_00C8;
						}
						modificationFunctionMapping = entityTypeModificationFunctionMapping.InsertFunctionMapping;
						EntityUtil.ValidateNecessaryModificationFunctionMapping(modificationFunctionMapping, "Insert", stateEntry.Source, "EntityType", entityType.Name);
						goto IL_00CA;
					}
				}
				else
				{
					if (state == EntityState.Deleted)
					{
						modificationFunctionMapping = entityTypeModificationFunctionMapping.DeleteFunctionMapping;
						EntityUtil.ValidateNecessaryModificationFunctionMapping(modificationFunctionMapping, "Delete", stateEntry.Source, "EntityType", entityType.Name);
						goto IL_00CA;
					}
					if (state != EntityState.Modified)
					{
						goto IL_00C8;
					}
				}
				modificationFunctionMapping = entityTypeModificationFunctionMapping.UpdateFunctionMapping;
				EntityUtil.ValidateNecessaryModificationFunctionMapping(modificationFunctionMapping, "Update", stateEntry.Source, "EntityType", entityType.Name);
				goto IL_00CA;
				IL_00C8:
				modificationFunctionMapping = null;
				IL_00CA:
				return Tuple.Create<EntityTypeModificationFunctionMapping, ModificationFunctionMapping>(entityTypeModificationFunctionMapping, modificationFunctionMapping);
			}

			// Token: 0x060068AB RID: 26795 RVA: 0x00165374 File Offset: 0x00163574
			private static void BindFunctionParameters(UpdateTranslator translator, ExtractedStateEntry stateEntry, ModificationFunctionMapping functionMapping, FunctionUpdateCommand command, Dictionary<AssociationEndMember, IEntityStateEntry> currentReferenceEnds, Dictionary<AssociationEndMember, IEntityStateEntry> originalReferenceEnds)
			{
				foreach (ModificationFunctionParameterBinding modificationFunctionParameterBinding in functionMapping.ParameterBindings)
				{
					PropagatorResult propagatorResult;
					if (modificationFunctionParameterBinding.MemberPath.AssociationSetEnd != null)
					{
						AssociationEndMember correspondingAssociationEndMember = modificationFunctionParameterBinding.MemberPath.AssociationSetEnd.CorrespondingAssociationEndMember;
						IEntityStateEntry entityStateEntry;
						if (!(modificationFunctionParameterBinding.IsCurrent ? currentReferenceEnds.TryGetValue(correspondingAssociationEndMember, out entityStateEntry) : originalReferenceEnds.TryGetValue(correspondingAssociationEndMember, out entityStateEntry)))
						{
							if (correspondingAssociationEndMember.RelationshipMultiplicity == RelationshipMultiplicity.One)
							{
								object name = stateEntry.Source.EntitySet.Name;
								string name2 = modificationFunctionParameterBinding.MemberPath.AssociationSetEnd.ParentAssociationSet.Name;
								throw new UpdateException(Strings.Update_MissingRequiredRelationshipValue(name, name2), null, command.GetStateEntries(translator).Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
							}
							propagatorResult = PropagatorResult.CreateSimpleValue(PropagatorFlags.NoFlags, null);
						}
						else
						{
							PropagatorResult memberValue = (modificationFunctionParameterBinding.IsCurrent ? translator.RecordConverter.ConvertCurrentValuesToPropagatorResult(entityStateEntry, ModifiedPropertiesBehavior.AllModified) : translator.RecordConverter.ConvertOriginalValuesToPropagatorResult(entityStateEntry, ModifiedPropertiesBehavior.AllModified)).GetMemberValue(correspondingAssociationEndMember);
							EdmProperty edmProperty = (EdmProperty)modificationFunctionParameterBinding.MemberPath.Members[0];
							propagatorResult = memberValue.GetMemberValue(edmProperty);
						}
					}
					else
					{
						propagatorResult = (modificationFunctionParameterBinding.IsCurrent ? stateEntry.Current : stateEntry.Original);
						int i = modificationFunctionParameterBinding.MemberPath.Members.Count;
						while (i > 0)
						{
							i--;
							EdmMember edmMember = modificationFunctionParameterBinding.MemberPath.Members[i];
							propagatorResult = propagatorResult.GetMemberValue(edmMember);
						}
					}
					command.SetParameterValue(propagatorResult, modificationFunctionParameterBinding, translator);
				}
				command.RegisterRowsAffectedParameter(functionMapping.RowsAffectedParameter);
			}

			// Token: 0x04002F49 RID: 12105
			private readonly Dictionary<EntityType, EntityTypeModificationFunctionMapping> m_typeMappings;
		}

		// Token: 0x02000BF6 RID: 3062
		private sealed class AssociationSetTranslator : ModificationFunctionMappingTranslator
		{
			// Token: 0x060068AC RID: 26796 RVA: 0x00165520 File Offset: 0x00163720
			internal AssociationSetTranslator(AssociationSetMapping setMapping)
			{
				if (setMapping != null)
				{
					this.m_mapping = setMapping.ModificationFunctionMapping;
				}
			}

			// Token: 0x060068AD RID: 26797 RVA: 0x00165538 File Offset: 0x00163738
			internal override FunctionUpdateCommand Translate(UpdateTranslator translator, ExtractedStateEntry stateEntry)
			{
				if (this.m_mapping == null)
				{
					return null;
				}
				bool flag = EntityState.Added == stateEntry.State;
				EntityUtil.ValidateNecessaryModificationFunctionMapping(flag ? this.m_mapping.InsertFunctionMapping : this.m_mapping.DeleteFunctionMapping, flag ? "Insert" : "Delete", stateEntry.Source, "AssociationSet", this.m_mapping.AssociationSet.Name);
				ModificationFunctionMapping modificationFunctionMapping = (flag ? this.m_mapping.InsertFunctionMapping : this.m_mapping.DeleteFunctionMapping);
				FunctionUpdateCommand functionUpdateCommand = new FunctionUpdateCommand(modificationFunctionMapping, translator, new ReadOnlyCollection<IEntityStateEntry>(new IEntityStateEntry[] { stateEntry.Source }.ToList<IEntityStateEntry>()), stateEntry);
				PropagatorResult propagatorResult;
				if (flag)
				{
					propagatorResult = stateEntry.Current;
				}
				else
				{
					propagatorResult = stateEntry.Original;
				}
				foreach (ModificationFunctionParameterBinding modificationFunctionParameterBinding in modificationFunctionMapping.ParameterBindings)
				{
					EdmProperty edmProperty = (EdmProperty)modificationFunctionParameterBinding.MemberPath.Members[0];
					AssociationEndMember associationEndMember = (AssociationEndMember)modificationFunctionParameterBinding.MemberPath.Members[1];
					PropagatorResult memberValue = propagatorResult.GetMemberValue(associationEndMember).GetMemberValue(edmProperty);
					functionUpdateCommand.SetParameterValue(memberValue, modificationFunctionParameterBinding, translator);
				}
				functionUpdateCommand.RegisterRowsAffectedParameter(modificationFunctionMapping.RowsAffectedParameter);
				return functionUpdateCommand;
			}

			// Token: 0x04002F4A RID: 12106
			private readonly AssociationSetModificationFunctionMapping m_mapping;
		}
	}
}
