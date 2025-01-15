using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.EntityClient.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005D8 RID: 1496
	internal class UpdateTranslator
	{
		// Token: 0x0600482D RID: 18477 RVA: 0x001004F4 File Offset: 0x000FE6F4
		public UpdateTranslator(EntityAdapter adapter)
			: this()
		{
			this._stateManager = adapter.Context.ObjectStateManager;
			this._interceptionContext = adapter.Context.InterceptionContext;
			this._adapter = adapter;
			this._providerServices = adapter.Connection.StoreProviderFactory.GetProviderServices();
		}

		// Token: 0x0600482E RID: 18478 RVA: 0x00100548 File Offset: 0x000FE748
		protected UpdateTranslator()
		{
			this._changes = new Dictionary<EntitySetBase, ChangeNode>();
			this._functionChanges = new Dictionary<EntitySetBase, List<ExtractedStateEntry>>();
			this._stateEntries = new List<IEntityStateEntry>();
			this._knownEntityKeys = new Set<EntityKey>();
			this._requiredEntities = new Dictionary<EntityKey, AssociationSet>();
			this._optionalEntities = new Set<EntityKey>();
			this._includedValueEntities = new Set<EntityKey>();
			this._interceptionContext = new DbInterceptionContext();
			this._recordConverter = new RecordConverter(this);
			this._constraintValidator = new UpdateTranslator.RelationshipConstraintValidator();
			this._extractorMetadata = new Dictionary<Tuple<EntitySetBase, StructuralType>, ExtractorMetadata>();
			KeyManager keyManager = new KeyManager();
			this.KeyManager = keyManager;
			this.KeyComparer = CompositeKey.CreateComparer(keyManager);
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x0600482F RID: 18479 RVA: 0x001005EE File Offset: 0x000FE7EE
		internal MetadataWorkspace MetadataWorkspace
		{
			get
			{
				return this.Connection.GetMetadataWorkspace();
			}
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06004830 RID: 18480 RVA: 0x001005FB File Offset: 0x000FE7FB
		// (set) Token: 0x06004831 RID: 18481 RVA: 0x00100603 File Offset: 0x000FE803
		internal virtual KeyManager KeyManager { get; private set; }

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06004832 RID: 18482 RVA: 0x0010060C File Offset: 0x000FE80C
		internal ViewLoader ViewLoader
		{
			get
			{
				return this.MetadataWorkspace.GetUpdateViewLoader();
			}
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06004833 RID: 18483 RVA: 0x00100619 File Offset: 0x000FE819
		internal RecordConverter RecordConverter
		{
			get
			{
				return this._recordConverter;
			}
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06004834 RID: 18484 RVA: 0x00100621 File Offset: 0x000FE821
		internal virtual EntityConnection Connection
		{
			get
			{
				return this._adapter.Connection;
			}
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06004835 RID: 18485 RVA: 0x0010062E File Offset: 0x000FE82E
		internal virtual int? CommandTimeout
		{
			get
			{
				return this._adapter.CommandTimeout;
			}
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06004836 RID: 18486 RVA: 0x0010063B File Offset: 0x000FE83B
		public virtual DbInterceptionContext InterceptionContext
		{
			get
			{
				return this._interceptionContext;
			}
		}

		// Token: 0x06004837 RID: 18487 RVA: 0x00100644 File Offset: 0x000FE844
		internal void RegisterReferentialConstraints(IEntityStateEntry stateEntry)
		{
			if (stateEntry.IsRelationship)
			{
				AssociationSet associationSet = (AssociationSet)stateEntry.EntitySet;
				if (0 >= associationSet.ElementType.ReferentialConstraints.Count)
				{
					return;
				}
				DbDataRecord dbDataRecord = ((stateEntry.State == EntityState.Added) ? stateEntry.CurrentValues : stateEntry.OriginalValues);
				using (ReadOnlyMetadataCollection<ReferentialConstraint>.Enumerator enumerator = associationSet.ElementType.ReferentialConstraints.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ReferentialConstraint referentialConstraint = enumerator.Current;
						EntityKey entityKey = (EntityKey)dbDataRecord[referentialConstraint.FromRole.Name];
						EntityKey entityKey2 = (EntityKey)dbDataRecord[referentialConstraint.ToRole.Name];
						using (ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator2 = referentialConstraint.FromProperties.GetEnumerator())
						{
							using (ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator3 = referentialConstraint.ToProperties.GetEnumerator())
							{
								while (enumerator2.MoveNext() && enumerator3.MoveNext())
								{
									int num;
									int keyMemberOffset = UpdateTranslator.GetKeyMemberOffset(referentialConstraint.FromRole, enumerator2.Current, out num);
									int num2;
									int keyMemberOffset2 = UpdateTranslator.GetKeyMemberOffset(referentialConstraint.ToRole, enumerator3.Current, out num2);
									int keyIdentifierForMemberOffset = this.KeyManager.GetKeyIdentifierForMemberOffset(entityKey, keyMemberOffset, num);
									int keyIdentifierForMemberOffset2 = this.KeyManager.GetKeyIdentifierForMemberOffset(entityKey2, keyMemberOffset2, num2);
									this.KeyManager.AddReferentialConstraint(stateEntry, keyIdentifierForMemberOffset2, keyIdentifierForMemberOffset);
								}
							}
						}
					}
					return;
				}
			}
			if (!stateEntry.IsKeyEntry)
			{
				if (stateEntry.State == EntityState.Added || stateEntry.State == EntityState.Modified)
				{
					this.RegisterEntityReferentialConstraints(stateEntry, true);
				}
				if (stateEntry.State == EntityState.Deleted || stateEntry.State == EntityState.Modified)
				{
					this.RegisterEntityReferentialConstraints(stateEntry, false);
				}
			}
		}

		// Token: 0x06004838 RID: 18488 RVA: 0x00100814 File Offset: 0x000FEA14
		private void RegisterEntityReferentialConstraints(IEntityStateEntry stateEntry, bool currentValues)
		{
			IExtendedDataRecord extendedDataRecord;
			if (!currentValues)
			{
				extendedDataRecord = (IExtendedDataRecord)stateEntry.OriginalValues;
			}
			else
			{
				IExtendedDataRecord currentValues2 = stateEntry.CurrentValues;
				extendedDataRecord = currentValues2;
			}
			IExtendedDataRecord extendedDataRecord2 = extendedDataRecord;
			EntitySet entitySet = (EntitySet)stateEntry.EntitySet;
			EntityKey entityKey = stateEntry.EntityKey;
			foreach (Tuple<AssociationSet, ReferentialConstraint> tuple in entitySet.ForeignKeyDependents)
			{
				AssociationSet item = tuple.Item1;
				ReferentialConstraint item2 = tuple.Item2;
				if (MetadataHelper.GetEntityTypeForEnd((AssociationEndMember)item2.ToRole).IsAssignableFrom(extendedDataRecord2.DataRecordInfo.RecordType.EdmType))
				{
					EntityKey entityKey2 = null;
					if (!currentValues || !this._stateManager.TryGetReferenceKey(entityKey, (AssociationEndMember)item2.FromRole, out entityKey2))
					{
						EntityType entityTypeForEnd = MetadataHelper.GetEntityTypeForEnd((AssociationEndMember)item2.FromRole);
						bool flag = false;
						object[] array = new object[entityTypeForEnd.KeyMembers.Count];
						int i = 0;
						int num = array.Length;
						while (i < num)
						{
							EdmProperty edmProperty = (EdmProperty)entityTypeForEnd.KeyMembers[i];
							int num2 = item2.FromProperties.IndexOf(edmProperty);
							int ordinal = extendedDataRecord2.GetOrdinal(item2.ToProperties[num2].Name);
							if (extendedDataRecord2.IsDBNull(ordinal))
							{
								flag = true;
								break;
							}
							array[i] = extendedDataRecord2.GetValue(ordinal);
							i++;
						}
						if (!flag)
						{
							EntitySet entitySet2 = item.AssociationSetEnds[item2.FromRole.Name].EntitySet;
							if (1 == array.Length)
							{
								entityKey2 = new EntityKey(entitySet2, array[0]);
							}
							else
							{
								entityKey2 = new EntityKey(entitySet2, array);
							}
						}
					}
					if (null != entityKey2)
					{
						IEntityStateEntry entityStateEntry;
						EntityKey entityKey3;
						if (!this._stateManager.TryGetEntityStateEntry(entityKey2, out entityStateEntry) && currentValues && this.KeyManager.TryGetTempKey(entityKey2, out entityKey3))
						{
							if (null == entityKey3)
							{
								throw EntityUtil.Update(Strings.Update_AmbiguousForeignKey(item2.ToRole.DeclaringType.FullName), null, new IEntityStateEntry[] { stateEntry });
							}
							entityKey2 = entityKey3;
						}
						this.AddValidAncillaryKey(entityKey2, this._optionalEntities);
						int j = 0;
						int count = item2.FromProperties.Count;
						while (j < count)
						{
							EdmProperty edmProperty2 = item2.FromProperties[j];
							EdmProperty edmProperty3 = item2.ToProperties[j];
							int num3;
							int keyMemberOffset = UpdateTranslator.GetKeyMemberOffset(item2.FromRole, edmProperty2, out num3);
							int keyIdentifierForMemberOffset = this.KeyManager.GetKeyIdentifierForMemberOffset(entityKey2, keyMemberOffset, num3);
							int num5;
							if (entitySet.ElementType.KeyMembers.Contains(edmProperty3))
							{
								int num4;
								int keyMemberOffset2 = UpdateTranslator.GetKeyMemberOffset(item2.ToRole, edmProperty3, out num4);
								num5 = this.KeyManager.GetKeyIdentifierForMemberOffset(entityKey, keyMemberOffset2, num4);
							}
							else
							{
								num5 = this.KeyManager.GetKeyIdentifierForMember(entityKey, edmProperty3.Name, currentValues);
							}
							if (currentValues && entityStateEntry != null && entityStateEntry.State == EntityState.Deleted && (stateEntry.State == EntityState.Added || stateEntry.State == EntityState.Modified))
							{
								throw EntityUtil.Update(Strings.Update_InsertingOrUpdatingReferenceToDeletedEntity(item.ElementType.FullName), null, new IEntityStateEntry[] { stateEntry, entityStateEntry });
							}
							this.KeyManager.AddReferentialConstraint(stateEntry, num5, keyIdentifierForMemberOffset);
							j++;
						}
					}
				}
			}
		}

		// Token: 0x06004839 RID: 18489 RVA: 0x00100B68 File Offset: 0x000FED68
		private static int GetKeyMemberOffset(RelationshipEndMember role, EdmProperty property, out int keyMemberCount)
		{
			EntityType entityType = (EntityType)((RefType)role.TypeUsage.EdmType).ElementType;
			keyMemberCount = entityType.KeyMembers.Count;
			return entityType.KeyMembers.IndexOf(property);
		}

		// Token: 0x0600483A RID: 18490 RVA: 0x00100BA9 File Offset: 0x000FEDA9
		internal IEnumerable<IEntityStateEntry> GetRelationships(EntityKey entityKey)
		{
			return this._stateManager.FindRelationshipsByKey(entityKey);
		}

		// Token: 0x0600483B RID: 18491 RVA: 0x00100BB8 File Offset: 0x000FEDB8
		internal virtual int Update()
		{
			Dictionary<int, object> dictionary = new Dictionary<int, object>();
			List<KeyValuePair<PropagatorResult, object>> list = new List<KeyValuePair<PropagatorResult, object>>();
			IEnumerable<UpdateCommand> enumerable = this.ProduceCommands();
			UpdateCommand updateCommand = null;
			try
			{
				foreach (UpdateCommand updateCommand2 in enumerable)
				{
					long num = (updateCommand = updateCommand2).Execute(dictionary, list);
					this.ValidateRowsAffected(num, updateCommand);
				}
			}
			catch (Exception ex)
			{
				if (ex.RequiresContext())
				{
					throw new UpdateException(Strings.Update_GeneralExecutionException, ex, this.DetermineStateEntriesFromSource(updateCommand).Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
				}
				throw;
			}
			this.BackPropagateServerGen(list);
			return this.AcceptChanges();
		}

		// Token: 0x0600483C RID: 18492 RVA: 0x00100C6C File Offset: 0x000FEE6C
		internal virtual async Task<int> UpdateAsync(CancellationToken cancellationToken)
		{
			Dictionary<int, object> identifierValues = new Dictionary<int, object>();
			List<KeyValuePair<PropagatorResult, object>> generatedValues = new List<KeyValuePair<PropagatorResult, object>>();
			IEnumerable<UpdateCommand> enumerable = this.ProduceCommands();
			UpdateCommand source = null;
			try
			{
				foreach (UpdateCommand updateCommand in enumerable)
				{
					source = updateCommand;
					long num = await updateCommand.ExecuteAsync(identifierValues, generatedValues, cancellationToken).WithCurrentCulture<long>();
					this.ValidateRowsAffected(num, source);
				}
				IEnumerator<UpdateCommand> enumerator = null;
			}
			catch (Exception ex)
			{
				if (ex.RequiresContext())
				{
					throw new UpdateException(Strings.Update_GeneralExecutionException, ex, this.DetermineStateEntriesFromSource(source).Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
				}
				throw;
			}
			this.BackPropagateServerGen(generatedValues);
			return this.AcceptChanges();
		}

		// Token: 0x0600483D RID: 18493 RVA: 0x00100CBC File Offset: 0x000FEEBC
		protected virtual IEnumerable<UpdateCommand> ProduceCommands()
		{
			this.PullModifiedEntriesFromStateManager();
			this.PullUnchangedEntriesFromStateManager();
			this._constraintValidator.ValidateConstraints();
			this.KeyManager.ValidateReferentialIntegrityGraphAcyclic();
			IEnumerable<UpdateCommand> enumerable = this.ProduceDynamicCommands();
			IEnumerable<UpdateCommand> enumerable2 = this.ProduceFunctionCommands();
			IEnumerable<UpdateCommand> enumerable3;
			IEnumerable<UpdateCommand> enumerable4;
			if (!new UpdateCommandOrderer(enumerable.Concat(enumerable2), this).TryTopologicalSort(out enumerable3, out enumerable4))
			{
				throw this.DependencyOrderingError(enumerable4);
			}
			return enumerable3;
		}

		// Token: 0x0600483E RID: 18494 RVA: 0x00100D18 File Offset: 0x000FEF18
		private void ValidateRowsAffected(long rowsAffected, UpdateCommand source)
		{
			if (rowsAffected == 0L)
			{
				IEnumerable<IEntityStateEntry> enumerable = this.DetermineStateEntriesFromSource(source);
				throw new OptimisticConcurrencyException(Strings.Update_ConcurrencyError(rowsAffected), null, enumerable.Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
			}
		}

		// Token: 0x0600483F RID: 18495 RVA: 0x00100D4D File Offset: 0x000FEF4D
		private IEnumerable<IEntityStateEntry> DetermineStateEntriesFromSource(UpdateCommand source)
		{
			if (source == null)
			{
				return Enumerable.Empty<IEntityStateEntry>();
			}
			return source.GetStateEntries(this);
		}

		// Token: 0x06004840 RID: 18496 RVA: 0x00100D60 File Offset: 0x000FEF60
		private void BackPropagateServerGen(List<KeyValuePair<PropagatorResult, object>> generatedValues)
		{
			foreach (KeyValuePair<PropagatorResult, object> keyValuePair in generatedValues)
			{
				PropagatorResult key;
				if (-1 == keyValuePair.Key.Identifier || !this.KeyManager.TryGetIdentifierOwner(keyValuePair.Key.Identifier, out key))
				{
					key = keyValuePair.Key;
				}
				object value = keyValuePair.Value;
				if (key.Identifier == -1)
				{
					key.SetServerGenValue(value);
				}
				else
				{
					foreach (int num in this.KeyManager.GetDependents(key.Identifier))
					{
						if (this.KeyManager.TryGetIdentifierOwner(num, out key))
						{
							key.SetServerGenValue(value);
						}
					}
				}
			}
		}

		// Token: 0x06004841 RID: 18497 RVA: 0x00100E58 File Offset: 0x000FF058
		private int AcceptChanges()
		{
			int num = 0;
			foreach (IEntityStateEntry entityStateEntry in this._stateEntries)
			{
				if (EntityState.Unchanged != entityStateEntry.State)
				{
					if (this._adapter.AcceptChangesDuringUpdate)
					{
						entityStateEntry.AcceptChanges();
					}
					num++;
				}
			}
			return num;
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x00100EC8 File Offset: 0x000FF0C8
		private IEnumerable<EntitySetBase> GetDynamicModifiedExtents()
		{
			return this._changes.Keys;
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x00100ED5 File Offset: 0x000FF0D5
		private IEnumerable<EntitySetBase> GetFunctionModifiedExtents()
		{
			return this._functionChanges.Keys;
		}

		// Token: 0x06004844 RID: 18500 RVA: 0x00100EE2 File Offset: 0x000FF0E2
		private IEnumerable<UpdateCommand> ProduceDynamicCommands()
		{
			UpdateCompiler updateCompiler = new UpdateCompiler(this);
			Set<EntitySet> set = new Set<EntitySet>();
			foreach (EntitySetBase entitySetBase in this.GetDynamicModifiedExtents())
			{
				Set<EntitySet> affectedTables = this.ViewLoader.GetAffectedTables(entitySetBase, this.MetadataWorkspace);
				if (affectedTables.Count == 0)
				{
					throw EntityUtil.Update(Strings.Update_MappingNotFound(entitySetBase.Name), null, new IEntityStateEntry[0]);
				}
				foreach (EntitySet entitySet in affectedTables)
				{
					set.Add(entitySet);
				}
			}
			foreach (EntitySet entitySet2 in set)
			{
				DbQueryCommandTree cqtView = this.Connection.GetMetadataWorkspace().GetCqtView(entitySet2);
				ChangeNode changeNode = Propagator.Propagate(this, entitySet2, cqtView);
				TableChangeProcessor tableChangeProcessor = new TableChangeProcessor(entitySet2);
				foreach (UpdateCommand updateCommand in tableChangeProcessor.CompileCommands(changeNode, updateCompiler))
				{
					yield return updateCommand;
				}
				List<UpdateCommand>.Enumerator enumerator4 = default(List<UpdateCommand>.Enumerator);
			}
			HashSet<EntitySet>.Enumerator enumerator3 = default(HashSet<EntitySet>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06004845 RID: 18501 RVA: 0x00100EF4 File Offset: 0x000FF0F4
		internal DbCommandDefinition GenerateCommandDefinition(ModificationFunctionMapping functionMapping)
		{
			if (this._modificationFunctionCommandDefinitions == null)
			{
				this._modificationFunctionCommandDefinitions = new Dictionary<ModificationFunctionMapping, DbCommandDefinition>();
			}
			DbCommandDefinition dbCommandDefinition;
			if (!this._modificationFunctionCommandDefinitions.TryGetValue(functionMapping, out dbCommandDefinition))
			{
				TypeUsage typeUsage = null;
				if (functionMapping.ResultBindings != null && functionMapping.ResultBindings.Count > 0)
				{
					List<EdmProperty> list = new List<EdmProperty>(functionMapping.ResultBindings.Count);
					foreach (ModificationFunctionResultBinding modificationFunctionResultBinding in functionMapping.ResultBindings)
					{
						list.Add(new EdmProperty(modificationFunctionResultBinding.ColumnName, modificationFunctionResultBinding.Property.TypeUsage));
					}
					typeUsage = TypeUsage.Create(new CollectionType(new RowType(list)));
				}
				IEnumerable<KeyValuePair<string, TypeUsage>> enumerable = functionMapping.Function.Parameters.Select((FunctionParameter paramInfo) => new KeyValuePair<string, TypeUsage>(paramInfo.Name, paramInfo.TypeUsage));
				DbFunctionCommandTree dbFunctionCommandTree = new DbFunctionCommandTree(this.MetadataWorkspace, DataSpace.SSpace, functionMapping.Function, typeUsage, enumerable);
				dbCommandDefinition = this._providerServices.CreateCommandDefinition(dbFunctionCommandTree, this._interceptionContext);
				this._modificationFunctionCommandDefinitions.Add(functionMapping, dbCommandDefinition);
			}
			return dbCommandDefinition;
		}

		// Token: 0x06004846 RID: 18502 RVA: 0x0010102C File Offset: 0x000FF22C
		private IEnumerable<UpdateCommand> ProduceFunctionCommands()
		{
			foreach (EntitySetBase entitySetBase in this.GetFunctionModifiedExtents())
			{
				ModificationFunctionMappingTranslator translator = this.ViewLoader.GetFunctionMappingTranslator(entitySetBase, this.MetadataWorkspace);
				if (translator != null)
				{
					foreach (ExtractedStateEntry extractedStateEntry in this.GetExtentFunctionModifications(entitySetBase))
					{
						FunctionUpdateCommand functionUpdateCommand = translator.Translate(this, extractedStateEntry);
						if (functionUpdateCommand != null)
						{
							yield return functionUpdateCommand;
						}
					}
					List<ExtractedStateEntry>.Enumerator enumerator2 = default(List<ExtractedStateEntry>.Enumerator);
				}
				translator = null;
			}
			IEnumerator<EntitySetBase> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x0010103C File Offset: 0x000FF23C
		internal ExtractorMetadata GetExtractorMetadata(EntitySetBase entitySetBase, StructuralType type)
		{
			Tuple<EntitySetBase, StructuralType> tuple = Tuple.Create<EntitySetBase, StructuralType>(entitySetBase, type);
			ExtractorMetadata extractorMetadata;
			if (!this._extractorMetadata.TryGetValue(tuple, out extractorMetadata))
			{
				extractorMetadata = new ExtractorMetadata(entitySetBase, type, this);
				this._extractorMetadata.Add(tuple, extractorMetadata);
			}
			return extractorMetadata;
		}

		// Token: 0x06004848 RID: 18504 RVA: 0x00101078 File Offset: 0x000FF278
		private UpdateException DependencyOrderingError(IEnumerable<UpdateCommand> remainder)
		{
			HashSet<IEntityStateEntry> hashSet = new HashSet<IEntityStateEntry>();
			foreach (UpdateCommand updateCommand in remainder)
			{
				hashSet.UnionWith(updateCommand.GetStateEntries(this));
			}
			throw new UpdateException(Strings.Update_ConstraintCycle, null, hashSet.Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
		}

		// Token: 0x06004849 RID: 18505 RVA: 0x001010E4 File Offset: 0x000FF2E4
		internal DbCommand CreateCommand(DbModificationCommandTree commandTree)
		{
			DbCommand dbCommand;
			try
			{
				dbCommand = new InterceptableDbCommand(this._providerServices.CreateCommand(commandTree, this._interceptionContext), this._interceptionContext, null);
			}
			catch (Exception ex)
			{
				if (ex.RequiresContext())
				{
					throw new EntityCommandCompilationException(Strings.EntityClient_CommandDefinitionPreparationFailed, ex);
				}
				throw;
			}
			return dbCommand;
		}

		// Token: 0x0600484A RID: 18506 RVA: 0x0010113C File Offset: 0x000FF33C
		internal void SetParameterValue(DbParameter parameter, TypeUsage typeUsage, object value)
		{
			this._providerServices.SetParameterValue(parameter, typeUsage, value);
		}

		// Token: 0x0600484B RID: 18507 RVA: 0x0010114C File Offset: 0x000FF34C
		private void PullModifiedEntriesFromStateManager()
		{
			foreach (IEntityStateEntry entityStateEntry in this._stateManager.GetEntityStateEntries(EntityState.Added))
			{
				if (!entityStateEntry.IsRelationship && !entityStateEntry.IsKeyEntry)
				{
					this.KeyManager.RegisterKeyValueForAddedEntity(entityStateEntry);
				}
			}
			foreach (IEntityStateEntry entityStateEntry2 in this._stateManager.GetEntityStateEntries(EntityState.Added | EntityState.Deleted | EntityState.Modified))
			{
				this.RegisterReferentialConstraints(entityStateEntry2);
			}
			foreach (IEntityStateEntry entityStateEntry3 in this._stateManager.GetEntityStateEntries(EntityState.Added | EntityState.Deleted | EntityState.Modified))
			{
				this.LoadStateEntry(entityStateEntry3);
			}
		}

		// Token: 0x0600484C RID: 18508 RVA: 0x0010123C File Offset: 0x000FF43C
		private void PullUnchangedEntriesFromStateManager()
		{
			foreach (KeyValuePair<EntityKey, AssociationSet> keyValuePair in this._requiredEntities)
			{
				EntityKey key = keyValuePair.Key;
				if (!this._knownEntityKeys.Contains(key))
				{
					IEntityStateEntry entityStateEntry;
					if (!this._stateManager.TryGetEntityStateEntry(key, out entityStateEntry) || entityStateEntry.IsKeyEntry)
					{
						throw EntityUtil.Update(Strings.Update_MissingEntity(keyValuePair.Value.Name, TypeHelpers.GetFullName(key.EntityContainerName, key.EntitySetName)), null, new IEntityStateEntry[0]);
					}
					this.LoadStateEntry(entityStateEntry);
				}
			}
			foreach (EntityKey entityKey in this._optionalEntities)
			{
				IEntityStateEntry entityStateEntry2;
				if (!this._knownEntityKeys.Contains(entityKey) && this._stateManager.TryGetEntityStateEntry(entityKey, out entityStateEntry2) && !entityStateEntry2.IsKeyEntry)
				{
					this.LoadStateEntry(entityStateEntry2);
				}
			}
			foreach (EntityKey entityKey2 in this._includedValueEntities)
			{
				IEntityStateEntry entityStateEntry3;
				if (!this._knownEntityKeys.Contains(entityKey2) && this._stateManager.TryGetEntityStateEntry(entityKey2, out entityStateEntry3))
				{
					this._recordConverter.ConvertCurrentValuesToPropagatorResult(entityStateEntry3, ModifiedPropertiesBehavior.NoneModified);
				}
			}
		}

		// Token: 0x0600484D RID: 18509 RVA: 0x001013C8 File Offset: 0x000FF5C8
		private void ValidateAndRegisterStateEntry(IEntityStateEntry stateEntry)
		{
			EntitySetBase entitySet = stateEntry.EntitySet;
			if (entitySet == null)
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.InvalidStateEntry, 1, null);
			}
			EntityKey entityKey = stateEntry.EntityKey;
			IExtendedDataRecord extendedDataRecord = null;
			if (((EntityState.Unchanged | EntityState.Added | EntityState.Modified) & stateEntry.State) != (EntityState)0)
			{
				extendedDataRecord = stateEntry.CurrentValues;
				this.ValidateRecord(entitySet, extendedDataRecord);
			}
			if (((EntityState.Unchanged | EntityState.Deleted | EntityState.Modified) & stateEntry.State) != (EntityState)0)
			{
				extendedDataRecord = (IExtendedDataRecord)stateEntry.OriginalValues;
				this.ValidateRecord(entitySet, extendedDataRecord);
			}
			AssociationSet associationSet = entitySet as AssociationSet;
			if (associationSet != null)
			{
				AssociationSetMetadata associationSetMetadata = this.ViewLoader.GetAssociationSetMetadata(associationSet, this.MetadataWorkspace);
				if (associationSetMetadata.HasEnds)
				{
					foreach (FieldMetadata fieldMetadata in extendedDataRecord.DataRecordInfo.FieldMetadata)
					{
						EntityKey entityKey2 = (EntityKey)extendedDataRecord.GetValue(fieldMetadata.Ordinal);
						AssociationEndMember associationEndMember = (AssociationEndMember)fieldMetadata.FieldType;
						if (associationSetMetadata.RequiredEnds.Contains(associationEndMember))
						{
							if (!this._requiredEntities.ContainsKey(entityKey2))
							{
								this._requiredEntities.Add(entityKey2, associationSet);
							}
						}
						else if (associationSetMetadata.OptionalEnds.Contains(associationEndMember))
						{
							this.AddValidAncillaryKey(entityKey2, this._optionalEntities);
						}
						else if (associationSetMetadata.IncludedValueEnds.Contains(associationEndMember))
						{
							this.AddValidAncillaryKey(entityKey2, this._includedValueEntities);
						}
					}
				}
				this._constraintValidator.RegisterAssociation(associationSet, extendedDataRecord, stateEntry);
			}
			else
			{
				this._constraintValidator.RegisterEntity(stateEntry);
			}
			this._stateEntries.Add(stateEntry);
			if (entityKey != null)
			{
				this._knownEntityKeys.Add(entityKey);
			}
		}

		// Token: 0x0600484E RID: 18510 RVA: 0x0010156C File Offset: 0x000FF76C
		private void AddValidAncillaryKey(EntityKey key, Set<EntityKey> keySet)
		{
			IEntityStateEntry entityStateEntry;
			if (this._stateManager.TryGetEntityStateEntry(key, out entityStateEntry) && !entityStateEntry.IsKeyEntry && entityStateEntry.State == EntityState.Unchanged)
			{
				keySet.Add(key);
			}
		}

		// Token: 0x0600484F RID: 18511 RVA: 0x001015A4 File Offset: 0x000FF7A4
		private void ValidateRecord(EntitySetBase extent, IExtendedDataRecord record)
		{
			DataRecordInfo dataRecordInfo;
			if (record == null || (dataRecordInfo = record.DataRecordInfo) == null || dataRecordInfo.RecordType == null)
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.InvalidStateEntry, 2, null);
			}
			UpdateTranslator.VerifyExtent(this.MetadataWorkspace, extent);
		}

		// Token: 0x06004850 RID: 18512 RVA: 0x001015E0 File Offset: 0x000FF7E0
		private static void VerifyExtent(MetadataWorkspace workspace, EntitySetBase extent)
		{
			EntityContainer entityContainer = extent.EntityContainer;
			EntityContainer entityContainer2 = null;
			if (entityContainer != null)
			{
				workspace.TryGetEntityContainer(entityContainer.Name, entityContainer.DataSpace, out entityContainer2);
			}
			if (entityContainer == null || entityContainer2 == null || entityContainer != entityContainer2)
			{
				throw EntityUtil.Update(Strings.Update_WorkspaceMismatch, null, new IEntityStateEntry[0]);
			}
		}

		// Token: 0x06004851 RID: 18513 RVA: 0x0010162C File Offset: 0x000FF82C
		private void LoadStateEntry(IEntityStateEntry stateEntry)
		{
			this.ValidateAndRegisterStateEntry(stateEntry);
			ExtractedStateEntry extractedStateEntry = new ExtractedStateEntry(this, stateEntry);
			EntitySetBase entitySet = stateEntry.EntitySet;
			if (this.ViewLoader.GetFunctionMappingTranslator(entitySet, this.MetadataWorkspace) == null)
			{
				ChangeNode extentModifications = this.GetExtentModifications(entitySet);
				if (extractedStateEntry.Original != null)
				{
					extentModifications.Deleted.Add(extractedStateEntry.Original);
				}
				if (extractedStateEntry.Current != null)
				{
					extentModifications.Inserted.Add(extractedStateEntry.Current);
					return;
				}
			}
			else
			{
				this.GetExtentFunctionModifications(entitySet).Add(extractedStateEntry);
			}
		}

		// Token: 0x06004852 RID: 18514 RVA: 0x001016AC File Offset: 0x000FF8AC
		internal ChangeNode GetExtentModifications(EntitySetBase extent)
		{
			ChangeNode changeNode;
			if (!this._changes.TryGetValue(extent, out changeNode))
			{
				changeNode = new ChangeNode(TypeUsage.Create(extent.ElementType));
				this._changes.Add(extent, changeNode);
			}
			return changeNode;
		}

		// Token: 0x06004853 RID: 18515 RVA: 0x001016E8 File Offset: 0x000FF8E8
		internal List<ExtractedStateEntry> GetExtentFunctionModifications(EntitySetBase extent)
		{
			List<ExtractedStateEntry> list;
			if (!this._functionChanges.TryGetValue(extent, out list))
			{
				list = new List<ExtractedStateEntry>();
				this._functionChanges.Add(extent, list);
			}
			return list;
		}

		// Token: 0x0400199B RID: 6555
		private readonly EntityAdapter _adapter;

		// Token: 0x0400199C RID: 6556
		private readonly Dictionary<EntitySetBase, ChangeNode> _changes;

		// Token: 0x0400199D RID: 6557
		private readonly Dictionary<EntitySetBase, List<ExtractedStateEntry>> _functionChanges;

		// Token: 0x0400199E RID: 6558
		private readonly List<IEntityStateEntry> _stateEntries;

		// Token: 0x0400199F RID: 6559
		private readonly Set<EntityKey> _knownEntityKeys;

		// Token: 0x040019A0 RID: 6560
		private readonly Dictionary<EntityKey, AssociationSet> _requiredEntities;

		// Token: 0x040019A1 RID: 6561
		private readonly Set<EntityKey> _optionalEntities;

		// Token: 0x040019A2 RID: 6562
		private readonly Set<EntityKey> _includedValueEntities;

		// Token: 0x040019A3 RID: 6563
		private readonly IEntityStateManager _stateManager;

		// Token: 0x040019A4 RID: 6564
		private readonly DbInterceptionContext _interceptionContext;

		// Token: 0x040019A5 RID: 6565
		private readonly RecordConverter _recordConverter;

		// Token: 0x040019A6 RID: 6566
		private readonly UpdateTranslator.RelationshipConstraintValidator _constraintValidator;

		// Token: 0x040019A7 RID: 6567
		private readonly DbProviderServices _providerServices;

		// Token: 0x040019A8 RID: 6568
		private Dictionary<ModificationFunctionMapping, DbCommandDefinition> _modificationFunctionCommandDefinitions;

		// Token: 0x040019A9 RID: 6569
		private readonly Dictionary<Tuple<EntitySetBase, StructuralType>, ExtractorMetadata> _extractorMetadata;

		// Token: 0x040019AB RID: 6571
		internal readonly IEqualityComparer<CompositeKey> KeyComparer;

		// Token: 0x02000C10 RID: 3088
		private class RelationshipConstraintValidator
		{
			// Token: 0x06006949 RID: 26953 RVA: 0x00167F04 File Offset: 0x00166104
			internal RelationshipConstraintValidator()
			{
				this.m_existingRelationships = new Dictionary<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship, UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship>(EqualityComparer<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship>.Default);
				this.m_impliedRelationships = new Dictionary<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship, IEntityStateEntry>(EqualityComparer<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship>.Default);
				this.m_referencingRelationshipSets = new Dictionary<EntitySet, List<AssociationSet>>(EqualityComparer<EntitySet>.Default);
			}

			// Token: 0x0600694A RID: 26954 RVA: 0x00167F3C File Offset: 0x0016613C
			internal void RegisterEntity(IEntityStateEntry stateEntry)
			{
				if (EntityState.Added == stateEntry.State || EntityState.Deleted == stateEntry.State)
				{
					EntityKey entityKey = stateEntry.EntityKey;
					EntitySet entitySet = (EntitySet)stateEntry.EntitySet;
					EntityType entityType = ((EntityState.Added == stateEntry.State) ? UpdateTranslator.RelationshipConstraintValidator.GetEntityType(stateEntry.CurrentValues) : UpdateTranslator.RelationshipConstraintValidator.GetEntityType(stateEntry.OriginalValues));
					foreach (AssociationSet associationSet in this.GetReferencingAssociationSets(entitySet))
					{
						ReadOnlyMetadataCollection<AssociationSetEnd> associationSetEnds = associationSet.AssociationSetEnds;
						foreach (AssociationSetEnd associationSetEnd in associationSetEnds)
						{
							foreach (AssociationSetEnd associationSetEnd2 in associationSetEnds)
							{
								if (associationSetEnd2.CorrespondingAssociationEndMember != associationSetEnd.CorrespondingAssociationEndMember && associationSetEnd2.EntitySet.EdmEquals(entitySet) && MetadataHelper.GetLowerBoundOfMultiplicity(associationSetEnd.CorrespondingAssociationEndMember.RelationshipMultiplicity) != 0 && MetadataHelper.GetEntityTypeForEnd(associationSetEnd2.CorrespondingAssociationEndMember).IsAssignableFrom(entityType))
								{
									UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship directionalRelationship = new UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship(entityKey, associationSetEnd.CorrespondingAssociationEndMember, associationSetEnd2.CorrespondingAssociationEndMember, associationSet, stateEntry);
									this.m_impliedRelationships.Add(directionalRelationship, stateEntry);
								}
							}
						}
					}
				}
			}

			// Token: 0x0600694B RID: 26955 RVA: 0x001680C4 File Offset: 0x001662C4
			private static EntityType GetEntityType(DbDataRecord dbDataRecord)
			{
				return (EntityType)(dbDataRecord as IExtendedDataRecord).DataRecordInfo.RecordType.EdmType;
			}

			// Token: 0x0600694C RID: 26956 RVA: 0x001680E0 File Offset: 0x001662E0
			internal void RegisterAssociation(AssociationSet associationSet, IExtendedDataRecord record, IEntityStateEntry stateEntry)
			{
				Dictionary<string, EntityKey> dictionary = new Dictionary<string, EntityKey>(StringComparer.Ordinal);
				foreach (FieldMetadata fieldMetadata in record.DataRecordInfo.FieldMetadata)
				{
					string name = fieldMetadata.FieldType.Name;
					EntityKey entityKey = (EntityKey)record.GetValue(fieldMetadata.Ordinal);
					dictionary.Add(name, entityKey);
				}
				ReadOnlyMetadataCollection<AssociationSetEnd> associationSetEnds = associationSet.AssociationSetEnds;
				foreach (AssociationSetEnd associationSetEnd in associationSetEnds)
				{
					foreach (AssociationSetEnd associationSetEnd2 in associationSetEnds)
					{
						if (associationSetEnd2.CorrespondingAssociationEndMember != associationSetEnd.CorrespondingAssociationEndMember)
						{
							UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship directionalRelationship = new UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship(dictionary[associationSetEnd2.CorrespondingAssociationEndMember.Name], associationSetEnd.CorrespondingAssociationEndMember, associationSetEnd2.CorrespondingAssociationEndMember, associationSet, stateEntry);
							this.AddExistingRelationship(directionalRelationship);
						}
					}
				}
			}

			// Token: 0x0600694D RID: 26957 RVA: 0x0016821C File Offset: 0x0016641C
			internal void ValidateConstraints()
			{
				foreach (KeyValuePair<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship, IEntityStateEntry> keyValuePair in this.m_impliedRelationships)
				{
					UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship key = keyValuePair.Key;
					IEntityStateEntry value = keyValuePair.Value;
					int num = this.GetDirectionalRelationshipCountDelta(key);
					if (EntityState.Deleted == value.State)
					{
						num = -num;
					}
					int lowerBoundOfMultiplicity = MetadataHelper.GetLowerBoundOfMultiplicity(key.FromEnd.RelationshipMultiplicity);
					int? upperBoundOfMultiplicity = MetadataHelper.GetUpperBoundOfMultiplicity(key.FromEnd.RelationshipMultiplicity);
					int num2 = ((upperBoundOfMultiplicity != null) ? upperBoundOfMultiplicity.Value : num);
					if (num < lowerBoundOfMultiplicity || num > num2)
					{
						throw EntityUtil.UpdateRelationshipCardinalityConstraintViolation(key.AssociationSet.Name, lowerBoundOfMultiplicity, upperBoundOfMultiplicity, TypeHelpers.GetFullName(key.ToEntityKey.EntityContainerName, key.ToEntityKey.EntitySetName), num, key.FromEnd.Name, value);
					}
				}
				foreach (UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship directionalRelationship in this.m_existingRelationships.Keys)
				{
					int num3;
					int num4;
					directionalRelationship.GetCountsInEquivalenceSet(out num3, out num4);
					int num5 = Math.Abs(num3 - num4);
					int lowerBoundOfMultiplicity2 = MetadataHelper.GetLowerBoundOfMultiplicity(directionalRelationship.FromEnd.RelationshipMultiplicity);
					int? upperBoundOfMultiplicity2 = MetadataHelper.GetUpperBoundOfMultiplicity(directionalRelationship.FromEnd.RelationshipMultiplicity);
					if (upperBoundOfMultiplicity2 != null)
					{
						EntityState? entityState = null;
						int? num6 = null;
						if (num3 > upperBoundOfMultiplicity2.Value)
						{
							entityState = new EntityState?(EntityState.Added);
							num6 = new int?(num3);
						}
						else if (num4 > upperBoundOfMultiplicity2.Value)
						{
							entityState = new EntityState?(EntityState.Deleted);
							num6 = new int?(num4);
						}
						if (entityState != null)
						{
							throw new UpdateException(Strings.Update_RelationshipCardinalityViolation(upperBoundOfMultiplicity2.Value, entityState.Value, directionalRelationship.AssociationSet.ElementType.FullName, directionalRelationship.FromEnd.Name, directionalRelationship.ToEnd.Name, num6.Value), null, (from reln in directionalRelationship.GetEquivalenceSet()
								select reln.StateEntry).Cast<ObjectStateEntry>().Distinct<ObjectStateEntry>());
						}
					}
					if (1 == num5 && 1 == lowerBoundOfMultiplicity2)
					{
						int num7 = 1;
						int? num8 = upperBoundOfMultiplicity2;
						if ((num7 == num8.GetValueOrDefault()) & (num8 != null))
						{
							bool flag = num3 > num4;
							IEntityStateEntry entityStateEntry;
							if (!this.m_impliedRelationships.TryGetValue(directionalRelationship, out entityStateEntry) || (flag && EntityState.Added != entityStateEntry.State) || (!flag && EntityState.Deleted != entityStateEntry.State))
							{
								throw EntityUtil.Update(Strings.Update_MissingRequiredEntity(directionalRelationship.AssociationSet.Name, directionalRelationship.StateEntry.State, directionalRelationship.ToEnd.Name), null, new IEntityStateEntry[] { directionalRelationship.StateEntry });
							}
						}
					}
				}
			}

			// Token: 0x0600694E RID: 26958 RVA: 0x00168548 File Offset: 0x00166748
			private int GetDirectionalRelationshipCountDelta(UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship expectedRelationship)
			{
				UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship directionalRelationship;
				if (this.m_existingRelationships.TryGetValue(expectedRelationship, out directionalRelationship))
				{
					int num;
					int num2;
					directionalRelationship.GetCountsInEquivalenceSet(out num, out num2);
					return num - num2;
				}
				return 0;
			}

			// Token: 0x0600694F RID: 26959 RVA: 0x00168574 File Offset: 0x00166774
			private void AddExistingRelationship(UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship relationship)
			{
				UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship directionalRelationship;
				if (this.m_existingRelationships.TryGetValue(relationship, out directionalRelationship))
				{
					directionalRelationship.AddToEquivalenceSet(relationship);
					return;
				}
				this.m_existingRelationships.Add(relationship, relationship);
			}

			// Token: 0x06006950 RID: 26960 RVA: 0x001685A8 File Offset: 0x001667A8
			private IEnumerable<AssociationSet> GetReferencingAssociationSets(EntitySet entitySet)
			{
				List<AssociationSet> list;
				if (!this.m_referencingRelationshipSets.TryGetValue(entitySet, out list))
				{
					list = new List<AssociationSet>();
					foreach (EntitySetBase entitySetBase in entitySet.EntityContainer.BaseEntitySets)
					{
						AssociationSet associationSet = entitySetBase as AssociationSet;
						if (associationSet != null && !associationSet.ElementType.IsForeignKey)
						{
							using (ReadOnlyMetadataCollection<AssociationSetEnd>.Enumerator enumerator2 = associationSet.AssociationSetEnds.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									if (enumerator2.Current.EntitySet.Equals(entitySet))
									{
										list.Add(associationSet);
										break;
									}
								}
							}
						}
					}
					this.m_referencingRelationshipSets.Add(entitySet, list);
				}
				return list;
			}

			// Token: 0x04002FB3 RID: 12211
			private readonly Dictionary<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship, UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship> m_existingRelationships;

			// Token: 0x04002FB4 RID: 12212
			private readonly Dictionary<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship, IEntityStateEntry> m_impliedRelationships;

			// Token: 0x04002FB5 RID: 12213
			private readonly Dictionary<EntitySet, List<AssociationSet>> m_referencingRelationshipSets;

			// Token: 0x02000D90 RID: 3472
			private class DirectionalRelationship : IEquatable<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship>
			{
				// Token: 0x06006F81 RID: 28545 RVA: 0x0017DAF4 File Offset: 0x0017BCF4
				internal DirectionalRelationship(EntityKey toEntityKey, AssociationEndMember fromEnd, AssociationEndMember toEnd, AssociationSet associationSet, IEntityStateEntry stateEntry)
				{
					this.ToEntityKey = toEntityKey;
					this.FromEnd = fromEnd;
					this.ToEnd = toEnd;
					this.AssociationSet = associationSet;
					this.StateEntry = stateEntry;
					this._equivalenceSetLinkedListNext = this;
					this._hashCode = toEntityKey.GetHashCode() ^ fromEnd.GetHashCode() ^ toEnd.GetHashCode() ^ associationSet.GetHashCode();
				}

				// Token: 0x06006F82 RID: 28546 RVA: 0x0017DB58 File Offset: 0x0017BD58
				internal void AddToEquivalenceSet(UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship other)
				{
					UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship equivalenceSetLinkedListNext = this._equivalenceSetLinkedListNext;
					this._equivalenceSetLinkedListNext = other;
					other._equivalenceSetLinkedListNext = equivalenceSetLinkedListNext;
				}

				// Token: 0x06006F83 RID: 28547 RVA: 0x0017DB7A File Offset: 0x0017BD7A
				internal IEnumerable<UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship> GetEquivalenceSet()
				{
					UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship current = this;
					do
					{
						yield return current;
						current = current._equivalenceSetLinkedListNext;
					}
					while (current != this);
					yield break;
				}

				// Token: 0x06006F84 RID: 28548 RVA: 0x0017DB8C File Offset: 0x0017BD8C
				internal void GetCountsInEquivalenceSet(out int addedCount, out int deletedCount)
				{
					addedCount = 0;
					deletedCount = 0;
					UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship directionalRelationship = this;
					do
					{
						if (directionalRelationship.StateEntry.State == EntityState.Added)
						{
							addedCount++;
						}
						else if (directionalRelationship.StateEntry.State == EntityState.Deleted)
						{
							deletedCount++;
						}
						directionalRelationship = directionalRelationship._equivalenceSetLinkedListNext;
					}
					while (directionalRelationship != this);
				}

				// Token: 0x06006F85 RID: 28549 RVA: 0x0017DBD6 File Offset: 0x0017BDD6
				public override int GetHashCode()
				{
					return this._hashCode;
				}

				// Token: 0x06006F86 RID: 28550 RVA: 0x0017DBE0 File Offset: 0x0017BDE0
				public bool Equals(UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship other)
				{
					return this == other || (other != null && !(this.ToEntityKey != other.ToEntityKey) && this.AssociationSet == other.AssociationSet && this.ToEnd == other.ToEnd && this.FromEnd == other.FromEnd);
				}

				// Token: 0x06006F87 RID: 28551 RVA: 0x0017DC3E File Offset: 0x0017BE3E
				public override bool Equals(object obj)
				{
					return this.Equals(obj as UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship);
				}

				// Token: 0x06006F88 RID: 28552 RVA: 0x0017DC4C File Offset: 0x0017BE4C
				public override string ToString()
				{
					return string.Format(CultureInfo.InvariantCulture, "{0}.{1}-->{2}: {3}", new object[]
					{
						this.AssociationSet.Name,
						this.FromEnd.Name,
						this.ToEnd.Name,
						StringUtil.BuildDelimitedList<EntityKeyMember>(this.ToEntityKey.EntityKeyValues, null, null)
					});
				}

				// Token: 0x04003393 RID: 13203
				internal readonly EntityKey ToEntityKey;

				// Token: 0x04003394 RID: 13204
				internal readonly AssociationEndMember FromEnd;

				// Token: 0x04003395 RID: 13205
				internal readonly AssociationEndMember ToEnd;

				// Token: 0x04003396 RID: 13206
				internal readonly IEntityStateEntry StateEntry;

				// Token: 0x04003397 RID: 13207
				internal readonly AssociationSet AssociationSet;

				// Token: 0x04003398 RID: 13208
				private UpdateTranslator.RelationshipConstraintValidator.DirectionalRelationship _equivalenceSetLinkedListNext;

				// Token: 0x04003399 RID: 13209
				private readonly int _hashCode;
			}
		}
	}
}
