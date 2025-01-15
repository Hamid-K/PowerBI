using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Data.Entity.Core.Query.PlanCompiler;
using System.Data.Entity.Core.Query.ResultAssembly;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.EntityClient.Internal
{
	// Token: 0x020005E5 RID: 1509
	internal class EntityCommandDefinition : DbCommandDefinition
	{
		// Token: 0x060049B2 RID: 18866 RVA: 0x001055E3 File Offset: 0x001037E3
		internal EntityCommandDefinition()
		{
		}

		// Token: 0x060049B3 RID: 18867 RVA: 0x001055EC File Offset: 0x001037EC
		internal EntityCommandDefinition(DbProviderFactory storeProviderFactory, DbCommandTree commandTree, DbInterceptionContext interceptionContext, IDbDependencyResolver resolver = null, BridgeDataReaderFactory bridgeDataReaderFactory = null, ColumnMapFactory columnMapFactory = null)
		{
			this._bridgeDataReaderFactory = bridgeDataReaderFactory ?? new BridgeDataReaderFactory(null);
			this._columnMapFactory = columnMapFactory ?? new ColumnMapFactory();
			this._storeProviderServices = ((resolver != null) ? resolver.GetService(storeProviderFactory.GetProviderInvariantName()) : null) ?? storeProviderFactory.GetProviderServices();
			try
			{
				if (commandTree.CommandTreeKind == DbCommandTreeKind.Query)
				{
					List<ProviderCommandInfo> list = new List<ProviderCommandInfo>();
					ColumnMap columnMap;
					int num;
					PlanCompiler.Compile(commandTree, out list, out columnMap, out num, out this._entitySets);
					this._columnMapGenerators = new EntityCommandDefinition.IColumnMapGenerator[]
					{
						new EntityCommandDefinition.ConstantColumnMapGenerator(columnMap, num)
					};
					this._mappedCommandDefinitions = new List<DbCommandDefinition>(list.Count);
					using (List<ProviderCommandInfo>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ProviderCommandInfo providerCommandInfo = enumerator.Current;
							DbCommandDefinition dbCommandDefinition = this._storeProviderServices.CreateCommandDefinition(providerCommandInfo.CommandTree, interceptionContext);
							if (dbCommandDefinition == null)
							{
								throw new ProviderIncompatibleException(Strings.ProviderReturnedNullForCreateCommandDefinition);
							}
							this._mappedCommandDefinitions.Add(dbCommandDefinition);
						}
						goto IL_023C;
					}
				}
				DbFunctionCommandTree dbFunctionCommandTree = (DbFunctionCommandTree)commandTree;
				FunctionImportMappingNonComposable targetFunctionMapping = EntityCommandDefinition.GetTargetFunctionMapping(dbFunctionCommandTree);
				IList<FunctionParameter> returnParameters = dbFunctionCommandTree.EdmFunction.ReturnParameters;
				int num2 = ((returnParameters.Count > 1) ? returnParameters.Count : 1);
				this._columnMapGenerators = new EntityCommandDefinition.IColumnMapGenerator[num2];
				TypeUsage typeUsage = this.DetermineStoreResultType(targetFunctionMapping, 0, out this._columnMapGenerators[0]);
				for (int i = 1; i < num2; i++)
				{
					this.DetermineStoreResultType(targetFunctionMapping, i, out this._columnMapGenerators[i]);
				}
				List<KeyValuePair<string, TypeUsage>> list2 = new List<KeyValuePair<string, TypeUsage>>();
				foreach (KeyValuePair<string, TypeUsage> keyValuePair in dbFunctionCommandTree.Parameters)
				{
					list2.Add(keyValuePair);
				}
				DbFunctionCommandTree dbFunctionCommandTree2 = new DbFunctionCommandTree(dbFunctionCommandTree.MetadataWorkspace, DataSpace.SSpace, targetFunctionMapping.TargetFunction, typeUsage, list2);
				DbCommandDefinition dbCommandDefinition2 = this._storeProviderServices.CreateCommandDefinition(dbFunctionCommandTree2);
				this._mappedCommandDefinitions = new List<DbCommandDefinition>(1) { dbCommandDefinition2 };
				if (targetFunctionMapping.FunctionImport.EntitySets.FirstOrDefault<EntitySet>() != null)
				{
					this._entitySets = new Set<EntitySet>();
					this._entitySets.Add(targetFunctionMapping.FunctionImport.EntitySets.FirstOrDefault<EntitySet>());
					this._entitySets.MakeReadOnly();
				}
				IL_023C:
				List<EntityParameter> list3 = new List<EntityParameter>();
				foreach (KeyValuePair<string, TypeUsage> keyValuePair2 in commandTree.Parameters)
				{
					EntityParameter entityParameter = EntityCommandDefinition.CreateEntityParameterFromQueryParameter(keyValuePair2);
					list3.Add(entityParameter);
				}
				this._parameters = new ReadOnlyCollection<EntityParameter>(list3);
			}
			catch (EntityCommandCompilationException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new EntityCommandCompilationException(Strings.EntityClient_CommandDefinitionPreparationFailed, ex);
				}
				throw;
			}
		}

		// Token: 0x060049B4 RID: 18868 RVA: 0x00105920 File Offset: 0x00103B20
		protected EntityCommandDefinition(BridgeDataReaderFactory factory = null, ColumnMapFactory columnMapFactory = null, List<DbCommandDefinition> mappedCommandDefinitions = null)
		{
			this._bridgeDataReaderFactory = factory ?? new BridgeDataReaderFactory(null);
			this._columnMapFactory = columnMapFactory ?? new ColumnMapFactory();
			this._mappedCommandDefinitions = mappedCommandDefinitions;
		}

		// Token: 0x060049B5 RID: 18869 RVA: 0x00105950 File Offset: 0x00103B50
		private TypeUsage DetermineStoreResultType(FunctionImportMappingNonComposable mapping, int resultSetIndex, out EntityCommandDefinition.IColumnMapGenerator columnMapGenerator)
		{
			EdmFunction functionImport = mapping.FunctionImport;
			StructuralType structuralType;
			TypeUsage typeUsage;
			if (MetadataHelper.TryGetFunctionImportReturnType<StructuralType>(functionImport, resultSetIndex, out structuralType))
			{
				EntityCommandDefinition.ValidateEdmResultType(structuralType, functionImport);
				EntitySet entitySet = ((functionImport.EntitySets.Count > resultSetIndex) ? functionImport.EntitySets[resultSetIndex] : null);
				columnMapGenerator = new EntityCommandDefinition.FunctionColumnMapGenerator(mapping, resultSetIndex, entitySet, structuralType, this._columnMapFactory);
				typeUsage = mapping.GetExpectedTargetResultType(resultSetIndex);
			}
			else
			{
				FunctionParameter returnParameter = MetadataHelper.GetReturnParameter(functionImport, resultSetIndex);
				if (returnParameter != null && returnParameter.TypeUsage != null)
				{
					typeUsage = returnParameter.TypeUsage;
					ScalarColumnMap scalarColumnMap = new ScalarColumnMap(((CollectionType)typeUsage.EdmType).TypeUsage, string.Empty, 0, 0);
					SimpleCollectionColumnMap simpleCollectionColumnMap = new SimpleCollectionColumnMap(typeUsage, string.Empty, scalarColumnMap, null, null);
					columnMapGenerator = new EntityCommandDefinition.ConstantColumnMapGenerator(simpleCollectionColumnMap, 1);
				}
				else
				{
					typeUsage = null;
					columnMapGenerator = new EntityCommandDefinition.ConstantColumnMapGenerator(null, 0);
				}
			}
			return typeUsage;
		}

		// Token: 0x060049B6 RID: 18870 RVA: 0x00105A14 File Offset: 0x00103C14
		private static void ValidateEdmResultType(EdmType resultType, EdmFunction functionImport)
		{
			if (Helper.IsComplexType(resultType))
			{
				ComplexType complexType = resultType as ComplexType;
				foreach (EdmProperty edmProperty in complexType.Properties)
				{
					if (edmProperty.TypeUsage.EdmType.BuiltInTypeKind == BuiltInTypeKind.ComplexType)
					{
						throw new NotSupportedException(Strings.ComplexTypeAsReturnTypeAndNestedComplexProperty(edmProperty.Name, complexType.Name, functionImport.FullName));
					}
				}
			}
		}

		// Token: 0x060049B7 RID: 18871 RVA: 0x00105AA0 File Offset: 0x00103CA0
		private static FunctionImportMappingNonComposable GetTargetFunctionMapping(DbFunctionCommandTree functionCommandTree)
		{
			FunctionImportMapping functionImportMapping;
			if (!functionCommandTree.MetadataWorkspace.TryGetFunctionImportMapping(functionCommandTree.EdmFunction, out functionImportMapping))
			{
				throw new InvalidOperationException(Strings.EntityClient_UnmappedFunctionImport(functionCommandTree.EdmFunction.FullName));
			}
			return (FunctionImportMappingNonComposable)functionImportMapping;
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x060049B8 RID: 18872 RVA: 0x00105ADE File Offset: 0x00103CDE
		internal virtual IEnumerable<EntityParameter> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x060049B9 RID: 18873 RVA: 0x00105AE6 File Offset: 0x00103CE6
		internal virtual Set<EntitySet> EntitySets
		{
			get
			{
				return this._entitySets;
			}
		}

		// Token: 0x060049BA RID: 18874 RVA: 0x00105AEE File Offset: 0x00103CEE
		public override DbCommand CreateCommand()
		{
			return new EntityCommand(this, new DbInterceptionContext(), null);
		}

		// Token: 0x060049BB RID: 18875 RVA: 0x00105AFC File Offset: 0x00103CFC
		internal ColumnMap CreateColumnMap(DbDataReader storeDataReader)
		{
			return this.CreateColumnMap(storeDataReader, 0);
		}

		// Token: 0x060049BC RID: 18876 RVA: 0x00105B06 File Offset: 0x00103D06
		internal virtual ColumnMap CreateColumnMap(DbDataReader storeDataReader, int resultSetIndex)
		{
			return this._columnMapGenerators[resultSetIndex].CreateColumnMap(storeDataReader);
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x00105B16 File Offset: 0x00103D16
		private static EntityParameter CreateEntityParameterFromQueryParameter(KeyValuePair<string, TypeUsage> queryParameter)
		{
			EntityParameter entityParameter = new EntityParameter();
			entityParameter.ParameterName = queryParameter.Key;
			EntityCommandDefinition.PopulateParameterFromTypeUsage(entityParameter, queryParameter.Value, false);
			return entityParameter;
		}

		// Token: 0x060049BE RID: 18878 RVA: 0x00105B38 File Offset: 0x00103D38
		internal static void PopulateParameterFromTypeUsage(EntityParameter parameter, TypeUsage type, bool isOutParam)
		{
			if (type != null)
			{
				PrimitiveTypeKind primitiveTypeKind;
				if (Helper.IsEnumType(type.EdmType))
				{
					type = TypeUsage.Create(Helper.GetUnderlyingEdmTypeForEnumType(type.EdmType));
				}
				else if (Helper.IsSpatialType(type, out primitiveTypeKind))
				{
					parameter.EdmType = EdmProviderManifest.Instance.GetPrimitiveType(primitiveTypeKind);
				}
			}
			DbCommandDefinition.PopulateParameterFromTypeUsage(parameter, type, isOutParam);
		}

		// Token: 0x060049BF RID: 18879 RVA: 0x00105B8C File Offset: 0x00103D8C
		internal virtual DbDataReader Execute(EntityCommand entityCommand, CommandBehavior behavior)
		{
			if (CommandBehavior.SequentialAccess != (behavior & CommandBehavior.SequentialAccess))
			{
				throw new InvalidOperationException(Strings.ADP_MustUseSequentialAccess);
			}
			DbDataReader dbDataReader = this.ExecuteStoreCommands(entityCommand, behavior & ~CommandBehavior.SequentialAccess);
			DbDataReader dbDataReader2 = null;
			if (dbDataReader != null)
			{
				try
				{
					ColumnMap columnMap = this.CreateColumnMap(dbDataReader, 0);
					if (columnMap == null)
					{
						CommandHelper.ConsumeReader(dbDataReader);
						dbDataReader2 = dbDataReader;
					}
					else
					{
						MetadataWorkspace metadataWorkspace = entityCommand.Connection.GetMetadataWorkspace();
						IEnumerable<ColumnMap> nextResultColumnMaps = this.GetNextResultColumnMaps(dbDataReader);
						dbDataReader2 = this._bridgeDataReaderFactory.Create(dbDataReader, columnMap, metadataWorkspace, nextResultColumnMaps);
					}
				}
				catch
				{
					dbDataReader.Dispose();
					throw;
				}
			}
			return dbDataReader2;
		}

		// Token: 0x060049C0 RID: 18880 RVA: 0x00105C18 File Offset: 0x00103E18
		internal virtual async Task<DbDataReader> ExecuteAsync(EntityCommand entityCommand, CommandBehavior behavior, CancellationToken cancellationToken)
		{
			if (CommandBehavior.SequentialAccess != (behavior & CommandBehavior.SequentialAccess))
			{
				throw new InvalidOperationException(Strings.ADP_MustUseSequentialAccess);
			}
			cancellationToken.ThrowIfCancellationRequested();
			DbDataReader dbDataReader = await this.ExecuteStoreCommandsAsync(entityCommand, behavior & ~CommandBehavior.SequentialAccess, cancellationToken).WithCurrentCulture<DbDataReader>();
			DbDataReader storeDataReader = dbDataReader;
			DbDataReader dbDataReader2 = null;
			if (storeDataReader != null)
			{
				try
				{
					ColumnMap columnMap = this.CreateColumnMap(storeDataReader, 0);
					if (columnMap == null)
					{
						await CommandHelper.ConsumeReaderAsync(storeDataReader, cancellationToken).WithCurrentCulture();
						dbDataReader2 = storeDataReader;
					}
					else
					{
						MetadataWorkspace metadataWorkspace = entityCommand.Connection.GetMetadataWorkspace();
						IEnumerable<ColumnMap> nextResultColumnMaps = this.GetNextResultColumnMaps(storeDataReader);
						dbDataReader2 = this._bridgeDataReaderFactory.Create(storeDataReader, columnMap, metadataWorkspace, nextResultColumnMaps);
					}
				}
				catch
				{
					storeDataReader.Dispose();
					throw;
				}
			}
			return dbDataReader2;
		}

		// Token: 0x060049C1 RID: 18881 RVA: 0x00105C75 File Offset: 0x00103E75
		private IEnumerable<ColumnMap> GetNextResultColumnMaps(DbDataReader storeDataReader)
		{
			int num;
			for (int i = 1; i < this._columnMapGenerators.Length; i = num)
			{
				yield return this.CreateColumnMap(storeDataReader, i);
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x060049C2 RID: 18882 RVA: 0x00105C8C File Offset: 0x00103E8C
		internal virtual DbDataReader ExecuteStoreCommands(EntityCommand entityCommand, CommandBehavior behavior)
		{
			DbCommand dbCommand = this.PrepareEntityCommandBeforeExecution(entityCommand);
			DbDataReader dbDataReader = null;
			try
			{
				dbDataReader = dbCommand.ExecuteReader(behavior);
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new EntityCommandExecutionException(Strings.EntityClient_CommandDefinitionExecutionFailed, ex);
				}
				throw;
			}
			return dbDataReader;
		}

		// Token: 0x060049C3 RID: 18883 RVA: 0x00105CD8 File Offset: 0x00103ED8
		internal virtual async Task<DbDataReader> ExecuteStoreCommandsAsync(EntityCommand entityCommand, CommandBehavior behavior, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			DbCommand dbCommand = this.PrepareEntityCommandBeforeExecution(entityCommand);
			DbDataReader dbDataReader = null;
			try
			{
				dbDataReader = await dbCommand.ExecuteReaderAsync(behavior, cancellationToken).WithCurrentCulture<DbDataReader>();
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new EntityCommandExecutionException(Strings.EntityClient_CommandDefinitionExecutionFailed, ex);
				}
				throw;
			}
			return dbDataReader;
		}

		// Token: 0x060049C4 RID: 18884 RVA: 0x00105D38 File Offset: 0x00103F38
		private DbCommand PrepareEntityCommandBeforeExecution(EntityCommand entityCommand)
		{
			if (1 != this._mappedCommandDefinitions.Count)
			{
				throw new NotSupportedException("MARS");
			}
			EntityTransaction entityTransaction = entityCommand.ValidateAndGetEntityTransaction();
			InterceptableDbCommand interceptableDbCommand = new InterceptableDbCommand(this._mappedCommandDefinitions[0].CreateCommand(), entityCommand.InterceptionContext, null);
			CommandHelper.SetStoreProviderCommandState(entityCommand, entityTransaction, interceptableDbCommand);
			bool flag = false;
			if (interceptableDbCommand.Parameters != null)
			{
				foreach (object obj in interceptableDbCommand.Parameters)
				{
					DbParameter dbParameter = (DbParameter)obj;
					int num = entityCommand.Parameters.IndexOf(dbParameter.ParameterName);
					if (-1 != num)
					{
						EntityCommandDefinition.SyncParameterProperties(entityCommand.Parameters[num], dbParameter, this._storeProviderServices);
						if (dbParameter.Direction != ParameterDirection.Input)
						{
							flag = true;
						}
					}
				}
			}
			if (flag)
			{
				entityCommand.SetStoreProviderCommand(interceptableDbCommand);
			}
			return interceptableDbCommand;
		}

		// Token: 0x060049C5 RID: 18885 RVA: 0x00105E28 File Offset: 0x00104028
		private static void SyncParameterProperties(EntityParameter entityParameter, DbParameter storeParameter, DbProviderServices storeProviderServices)
		{
			TypeUsage primitiveTypeUsageForScalar = TypeHelpers.GetPrimitiveTypeUsageForScalar(entityParameter.GetTypeUsage());
			storeProviderServices.SetParameterValue(storeParameter, primitiveTypeUsageForScalar, entityParameter.Value);
			if (entityParameter.IsDirectionSpecified)
			{
				storeParameter.Direction = entityParameter.Direction;
			}
			if (entityParameter.IsIsNullableSpecified)
			{
				storeParameter.IsNullable = entityParameter.IsNullable;
			}
			if (entityParameter.IsSizeSpecified)
			{
				storeParameter.Size = entityParameter.Size;
			}
			if (entityParameter.IsPrecisionSpecified)
			{
				((IDbDataParameter)storeParameter).Precision = entityParameter.Precision;
			}
			if (entityParameter.IsScaleSpecified)
			{
				((IDbDataParameter)storeParameter).Scale = entityParameter.Scale;
			}
		}

		// Token: 0x060049C6 RID: 18886 RVA: 0x00105EB8 File Offset: 0x001040B8
		internal virtual string ToTraceString()
		{
			if (this._mappedCommandDefinitions == null)
			{
				return string.Empty;
			}
			if (this._mappedCommandDefinitions.Count == 1)
			{
				return this._mappedCommandDefinitions[0].CreateCommand().CommandText;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (DbCommandDefinition dbCommandDefinition in this._mappedCommandDefinitions)
			{
				DbCommand dbCommand = dbCommandDefinition.CreateCommand();
				stringBuilder.Append(dbCommand.CommandText);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001A04 RID: 6660
		private readonly List<DbCommandDefinition> _mappedCommandDefinitions;

		// Token: 0x04001A05 RID: 6661
		private readonly EntityCommandDefinition.IColumnMapGenerator[] _columnMapGenerators;

		// Token: 0x04001A06 RID: 6662
		private readonly ReadOnlyCollection<EntityParameter> _parameters;

		// Token: 0x04001A07 RID: 6663
		private readonly Set<EntitySet> _entitySets;

		// Token: 0x04001A08 RID: 6664
		private readonly BridgeDataReaderFactory _bridgeDataReaderFactory;

		// Token: 0x04001A09 RID: 6665
		private readonly ColumnMapFactory _columnMapFactory;

		// Token: 0x04001A0A RID: 6666
		private readonly DbProviderServices _storeProviderServices;

		// Token: 0x02000C2A RID: 3114
		private interface IColumnMapGenerator
		{
			// Token: 0x060069C6 RID: 27078
			ColumnMap CreateColumnMap(DbDataReader reader);
		}

		// Token: 0x02000C2B RID: 3115
		private sealed class ConstantColumnMapGenerator : EntityCommandDefinition.IColumnMapGenerator
		{
			// Token: 0x060069C7 RID: 27079 RVA: 0x0016A30C File Offset: 0x0016850C
			internal ConstantColumnMapGenerator(ColumnMap columnMap, int fieldsRequired)
			{
				this._columnMap = columnMap;
				this._fieldsRequired = fieldsRequired;
			}

			// Token: 0x060069C8 RID: 27080 RVA: 0x0016A322 File Offset: 0x00168522
			ColumnMap EntityCommandDefinition.IColumnMapGenerator.CreateColumnMap(DbDataReader reader)
			{
				if (reader != null && reader.FieldCount < this._fieldsRequired)
				{
					throw new EntityCommandExecutionException(Strings.EntityClient_TooFewColumns);
				}
				return this._columnMap;
			}

			// Token: 0x04003032 RID: 12338
			private readonly ColumnMap _columnMap;

			// Token: 0x04003033 RID: 12339
			private readonly int _fieldsRequired;
		}

		// Token: 0x02000C2C RID: 3116
		private sealed class FunctionColumnMapGenerator : EntityCommandDefinition.IColumnMapGenerator
		{
			// Token: 0x060069C9 RID: 27081 RVA: 0x0016A346 File Offset: 0x00168546
			internal FunctionColumnMapGenerator(FunctionImportMappingNonComposable mapping, int resultSetIndex, EntitySet entitySet, StructuralType baseStructuralType, ColumnMapFactory columnMapFactory)
			{
				this._mapping = mapping;
				this._entitySet = entitySet;
				this._baseStructuralType = baseStructuralType;
				this._resultSetIndex = resultSetIndex;
				this._columnMapFactory = columnMapFactory;
			}

			// Token: 0x060069CA RID: 27082 RVA: 0x0016A373 File Offset: 0x00168573
			ColumnMap EntityCommandDefinition.IColumnMapGenerator.CreateColumnMap(DbDataReader reader)
			{
				return this._columnMapFactory.CreateFunctionImportStructuralTypeColumnMap(reader, this._mapping, this._resultSetIndex, this._entitySet, this._baseStructuralType);
			}

			// Token: 0x04003034 RID: 12340
			private readonly FunctionImportMappingNonComposable _mapping;

			// Token: 0x04003035 RID: 12341
			private readonly EntitySet _entitySet;

			// Token: 0x04003036 RID: 12342
			private readonly StructuralType _baseStructuralType;

			// Token: 0x04003037 RID: 12343
			private readonly int _resultSetIndex;

			// Token: 0x04003038 RID: 12344
			private readonly ColumnMapFactory _columnMapFactory;
		}
	}
}
