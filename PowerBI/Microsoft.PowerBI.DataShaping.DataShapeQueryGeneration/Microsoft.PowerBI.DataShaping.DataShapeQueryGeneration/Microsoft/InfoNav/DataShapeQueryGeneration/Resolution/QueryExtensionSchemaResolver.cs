using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryExtensionSchema;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Resolution
{
	// Token: 0x020000F5 RID: 245
	internal sealed class QueryExtensionSchemaResolver
	{
		// Token: 0x06000840 RID: 2112 RVA: 0x00020D77 File Offset: 0x0001EF77
		private QueryExtensionSchemaResolver(IConceptualSchema baseSchema, string extensionSchemaName, DataShapeGenerationErrorContext errorContext)
		{
			this._baseSchema = baseSchema;
			this._namingContext = new NamingContext(QueryExtensionSchemaResolver.DaxNameComparer);
			this._errorContext = errorContext;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00020DA0 File Offset: 0x0001EFA0
		internal static QueryExtensionSchemaContext Resolve(QueryExtensionSchema extensionSchema, IConceptualSchema baseSchema, DataShapeGenerationErrorContext errorContext)
		{
			if (extensionSchema == null)
			{
				return null;
			}
			if (!extensionSchema.Entities.IsNullOrEmpty<QueryExtensionEntity>())
			{
				if (!extensionSchema.Entities.All(delegate(QueryExtensionEntity e)
				{
					if (e.NamingBehavior == QueryExtensionNamingBehavior.Flexible)
					{
						return false;
					}
					if (!e.Columns.IsNullOrEmpty<QueryExtensionColumn>())
					{
						return e.Columns.All((QueryExtensionColumn c) => c.NamingBehavior != QueryExtensionNamingBehavior.Flexible);
					}
					return true;
				}))
				{
					QueryExtensionSchemaResolver queryExtensionSchemaResolver = new QueryExtensionSchemaResolver(baseSchema, extensionSchema.Name, errorContext);
					QuerySchemaMapping querySchemaMapping = queryExtensionSchemaResolver.Resolve(extensionSchema);
					return new QueryExtensionSchemaContext(extensionSchema, querySchemaMapping, queryExtensionSchemaResolver._namingContext, null);
				}
			}
			return new QueryExtensionSchemaContext(extensionSchema, QuerySchemaMapping.Empty, null, null);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00020E24 File Offset: 0x0001F024
		private QuerySchemaMapping Resolve(QueryExtensionSchema extensionSchema)
		{
			foreach (QueryExtensionEntity queryExtensionEntity in extensionSchema.Entities)
			{
				this.VisitDaxReferenceableItems(queryExtensionEntity);
			}
			this.VisitBaseSchema();
			if (this._errorContext.HasError)
			{
				return null;
			}
			this.VisitFlexiblyNamedItems(extensionSchema);
			if (!this._entityMappings.IsNullOrEmpty<ExtensionEntityMapping>())
			{
				return new QuerySchemaMapping(extensionSchema.Name, this._entityMappings);
			}
			return QuerySchemaMapping.Empty;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00020EB4 File Offset: 0x0001F0B4
		private void VisitDaxReferenceableItems(QueryExtensionEntity extensionEntity)
		{
			if (extensionEntity.Measures.IsNullOrEmpty<QueryExtensionMeasure>() && extensionEntity.Columns.IsNullOrEmpty<QueryExtensionColumn>())
			{
				return;
			}
			if (extensionEntity.NamingBehavior == QueryExtensionNamingBehavior.Preserve)
			{
				if (extensionEntity.Measures != null)
				{
					foreach (QueryExtensionMeasure queryExtensionMeasure in extensionEntity.Measures)
					{
						this._namingContext.RegisterUniqueName(queryExtensionMeasure.Name);
					}
				}
				if (extensionEntity.Columns != null)
				{
					foreach (QueryExtensionColumn queryExtensionColumn in extensionEntity.Columns)
					{
						if (queryExtensionColumn.NamingBehavior == QueryExtensionNamingBehavior.Preserve)
						{
							this._namingContext.RegisterUniqueName(queryExtensionColumn.Name);
						}
					}
				}
			}
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00020F94 File Offset: 0x0001F194
		private void VisitBaseSchema()
		{
			IReadOnlyList<IConceptualEntity> entities = this._baseSchema.Entities;
			if (entities.IsNullOrEmpty<IConceptualEntity>())
			{
				this._errorContext.Register(DataShapeGenerationMessages.FlexibleExtensionSchemaWithEmptyModel(EngineMessageSeverity.Error));
				return;
			}
			foreach (IConceptualEntity conceptualEntity in entities)
			{
				DsqGenerationUtils.PopulateModelPropertyNames(conceptualEntity, this._namingContext);
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00021008 File Offset: 0x0001F208
		private void VisitFlexiblyNamedItems(QueryExtensionSchema schema)
		{
			List<QueryExtensionEntity> list = null;
			QueryExtensionEntity queryExtensionEntity = null;
			string name = this._baseSchema.Entities[0].Name;
			IList<QueryExtensionEntity> entities = schema.Entities;
			for (int i = 0; i < entities.Count; i++)
			{
				QueryExtensionEntity queryExtensionEntity2 = entities[i];
				if (queryExtensionEntity2.NamingBehavior == QueryExtensionNamingBehavior.Flexible)
				{
					this.VisitFlexiblyNamedEntity(queryExtensionEntity2, name);
				}
				else if (!queryExtensionEntity2.Columns.IsNullOrEmpty<QueryExtensionColumn>())
				{
					this.VisitFlexiblyNamedColumns(queryExtensionEntity2.Columns, queryExtensionEntity2.Extends);
				}
				if (ConceptualNameComparer.Instance.Equals(name, queryExtensionEntity2.Name))
				{
					if (queryExtensionEntity == null)
					{
						queryExtensionEntity = queryExtensionEntity2;
					}
					else
					{
						this.MergeEntities(queryExtensionEntity2, queryExtensionEntity);
						if (list == null)
						{
							list = new List<QueryExtensionEntity>(entities.Count - 1);
							for (int j = 0; j < i; j++)
							{
								list.Add(entities[j]);
							}
						}
					}
				}
				else if (list != null)
				{
					list.Add(queryExtensionEntity2);
				}
			}
			IList<QueryExtensionEntity> list2 = list;
			schema.Entities = list2 ?? entities;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0002110C File Offset: 0x0001F30C
		private void VisitFlexiblyNamedEntity(QueryExtensionEntity extensionEntity, string targetEntityName)
		{
			if (extensionEntity.Measures.IsNullOrEmpty<QueryExtensionMeasure>() && extensionEntity.Columns.IsNullOrEmpty<QueryExtensionColumn>())
			{
				return;
			}
			List<ExtensionPropertyMapping> list;
			this.VisitMeasuresInFlexiblyNamedEntity(extensionEntity.Measures, out list);
			string name = extensionEntity.Name;
			extensionEntity.Name = targetEntityName;
			extensionEntity.Extends = targetEntityName;
			extensionEntity.NamingBehavior = QueryExtensionNamingBehavior.Preserve;
			Util.AddToLazyList<ExtensionEntityMapping>(ref this._entityMappings, new ExtensionEntityMapping(name, extensionEntity.Name, list, null));
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00021178 File Offset: 0x0001F378
		private void VisitMeasuresInFlexiblyNamedEntity(IList<QueryExtensionMeasure> measures, out List<ExtensionPropertyMapping> measureMappings)
		{
			measureMappings = null;
			if (!measures.IsNullOrEmpty<QueryExtensionMeasure>())
			{
				foreach (QueryExtensionMeasure queryExtensionMeasure in measures)
				{
					string text;
					this.AddMapping(queryExtensionMeasure.Name, ref measureMappings, out text);
					queryExtensionMeasure.Name = text;
				}
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x000211DC File Offset: 0x0001F3DC
		private void VisitFlexiblyNamedColumns(IList<QueryExtensionColumn> columns, string targetEntityName)
		{
			List<ExtensionPropertyMapping> list = null;
			foreach (QueryExtensionColumn queryExtensionColumn in columns)
			{
				if (queryExtensionColumn.NamingBehavior == QueryExtensionNamingBehavior.Flexible)
				{
					string text;
					this.AddMapping(queryExtensionColumn.Name, ref list, out text);
					queryExtensionColumn.Name = text;
					queryExtensionColumn.NamingBehavior = QueryExtensionNamingBehavior.Preserve;
				}
			}
			if (!list.IsNullOrEmpty<ExtensionPropertyMapping>())
			{
				Util.AddToLazyList<ExtensionEntityMapping>(ref this._entityMappings, new ExtensionEntityMapping(targetEntityName, targetEntityName, null, list));
			}
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00021264 File Offset: 0x0001F464
		private void AddMapping(string name, ref List<ExtensionPropertyMapping> mappings, out string newName)
		{
			newName = this._namingContext.GenerateUniqueName(name);
			this._namingContext.RegisterUniqueName(newName);
			Util.AddToLazyList<ExtensionPropertyMapping>(ref mappings, new ExtensionPropertyMapping(name, newName));
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00021290 File Offset: 0x0001F490
		private void MergeEntities(QueryExtensionEntity fromEntity, QueryExtensionEntity toEntity)
		{
			List<QueryExtensionMeasure> list;
			this.Merge<QueryExtensionMeasure>(fromEntity.Measures, toEntity.Measures, out list);
			toEntity.Measures = list;
			List<QueryExtensionColumn> list2;
			this.Merge<QueryExtensionColumn>(fromEntity.Columns, toEntity.Columns, out list2);
			toEntity.Columns = list2;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000212D4 File Offset: 0x0001F4D4
		private void Merge<T>(IList<T> fromProperties, IList<T> toProperties, out List<T> targetProperties)
		{
			targetProperties = new List<T>((((fromProperties != null) ? new int?(fromProperties.Count) : null) + ((toProperties != null) ? new int?(toProperties.Count) : null)).GetValueOrDefault());
			if (!toProperties.IsNullOrEmpty<T>())
			{
				targetProperties.AddRange(toProperties);
			}
			if (!fromProperties.IsNullOrEmpty<T>())
			{
				targetProperties.AddRange(fromProperties);
			}
		}

		// Token: 0x0400043B RID: 1083
		private static readonly StringComparer DaxNameComparer = StringComparer.OrdinalIgnoreCase;

		// Token: 0x0400043C RID: 1084
		private readonly IConceptualSchema _baseSchema;

		// Token: 0x0400043D RID: 1085
		private readonly NamingContext _namingContext;

		// Token: 0x0400043E RID: 1086
		private readonly DataShapeGenerationErrorContext _errorContext;

		// Token: 0x0400043F RID: 1087
		private List<ExtensionEntityMapping> _entityMappings;
	}
}
