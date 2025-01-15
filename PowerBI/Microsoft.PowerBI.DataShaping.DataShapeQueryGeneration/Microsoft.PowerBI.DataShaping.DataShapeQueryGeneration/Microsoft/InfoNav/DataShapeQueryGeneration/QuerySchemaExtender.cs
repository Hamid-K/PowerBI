using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryExtensionSchema;
using Microsoft.InfoNav.Data.Library;
using Microsoft.InfoNav.DataShapeQueryGeneration.Resolution;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000BF RID: 191
	internal sealed class QuerySchemaExtender : IQuerySchemaExtender
	{
		// Token: 0x060006EE RID: 1774 RVA: 0x00019E94 File Offset: 0x00018094
		private QuerySchemaExtender(IFederatedConceptualSchema federatedConceptualSchema, IConceptualSchema baseSchema, QueryResolutionErrorContext errorContext, NamingContext namingContext, string name)
		{
			this._federatedConceptualSchema = federatedConceptualSchema;
			this._errorContext = errorContext;
			this._namingContext = namingContext;
			this._name = name;
			this._extends = baseSchema.Extends;
			this._baseSchema = baseSchema;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00019ED0 File Offset: 0x000180D0
		internal static QuerySchemaExtender Create(IFederatedConceptualSchema federatedConceptualSchema, DataShapeGenerationErrorContext errorContext, NamingContext namingContext, IReadOnlyList<QueryExtensionSchema> extensionSchemas = null)
		{
			IConceptualSchema conceptualSchema;
			if (!federatedConceptualSchema.TryGetDefaultSchema(out conceptualSchema))
			{
				Contract.RetailFail("Could not find a default schema in the FederatedConceptualSchema");
			}
			HashSet<string> hashSet = federatedConceptualSchema.Schemas.Select((IConceptualSchema s) => s.SchemaId).ToHashSet(null);
			QueryExtensionSchema queryExtensionSchema = (extensionSchemas.IsNullOrEmpty<QueryExtensionSchema>() ? null : extensionSchemas.First<QueryExtensionSchema>());
			string text = ((queryExtensionSchema == null) ? NamingContext.GenerateUniqueName(hashSet, "CalculatedSchema") : queryExtensionSchema.Name);
			DataShapeGenerationErrorContextAdapter dataShapeGenerationErrorContextAdapter = new DataShapeGenerationErrorContextAdapter(errorContext, DataShapeGenerationErrorCode.InvalidExtensionSchema, ErrorSource.User);
			QuerySchemaExtender querySchemaExtender = new QuerySchemaExtender(federatedConceptualSchema, conceptualSchema, new QueryResolutionErrorContext(dataShapeGenerationErrorContextAdapter), namingContext, text);
			querySchemaExtender.Initialize(queryExtensionSchema);
			return querySchemaExtender;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00019F74 File Offset: 0x00018174
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "QueryExtensionSchema", "FederatedConceptualSchema" })]
		internal global::System.ValueTuple<QueryExtensionSchemaContext, IFederatedConceptualSchema> Extend(QueryExtensionSchemaContext resolvedQueryExtensionSchema, IFederatedConceptualSchema federatedConceptualSchema)
		{
			QueryExtensionSchemaContext queryExtensionSchemaContext = this.Extend(resolvedQueryExtensionSchema);
			IFederatedConceptualSchema federatedConceptualSchema2 = this.Extend(federatedConceptualSchema);
			return new global::System.ValueTuple<QueryExtensionSchemaContext, IFederatedConceptualSchema>(queryExtensionSchemaContext, federatedConceptualSchema2);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00019F98 File Offset: 0x00018198
		private QueryExtensionSchemaContext Extend(QueryExtensionSchemaContext resolvedQueryExtensionSchema)
		{
			if (this._queryExtensionSchemaBuilder == null)
			{
				return resolvedQueryExtensionSchema;
			}
			QueryExtensionSchema extensionSchema = this._queryExtensionSchemaBuilder.ToExtensionSchema();
			if (extensionSchema.Entities.IsNullOrEmptyCollection<QueryExtensionEntity>())
			{
				return resolvedQueryExtensionSchema;
			}
			if (resolvedQueryExtensionSchema == null)
			{
				return new QueryExtensionSchemaContext(extensionSchema, QuerySchemaMapping.Empty, this._namingContext, this._extensionColumnDsqExpressions);
			}
			return new QueryExtensionSchemaContext(new List<QueryExtensionSchema>(resolvedQueryExtensionSchema.ExtensionSchemas.Where((QueryExtensionSchema es) => extensionSchema.Name != this._name)) { extensionSchema }, resolvedQueryExtensionSchema.QuerySchemaMapping, this._namingContext, this._extensionColumnDsqExpressions);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001A040 File Offset: 0x00018240
		private IFederatedConceptualSchema Extend(IFederatedConceptualSchema federatedConceptualSchema)
		{
			if (this._extensionConceptualSchemaBuilder == null)
			{
				return federatedConceptualSchema;
			}
			IConceptualSchema conceptualSchema = this._extensionConceptualSchemaBuilder.CompleteInitialization();
			if (conceptualSchema.Entities.IsNullOrEmpty<IConceptualEntity>())
			{
				return federatedConceptualSchema;
			}
			return FederatedConceptualSchemaExtensions.Update(federatedConceptualSchema, conceptualSchema);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001A07C File Offset: 0x0001827C
		public IConceptualColumn CreateColumn(string extendedEntity, ConceptualPrimitiveType conceptualType, string suggestedName, Expression dsqExpression)
		{
			ConceptualEntity.Builder builder;
			QueryExtensionEntityBuilder<QueryExtensionSchemaBuilder<object>> orCreateEntity = this.GetOrCreateEntity(extendedEntity, out builder);
			return this.CreateColumn(conceptualType, suggestedName, dsqExpression, orCreateEntity, builder);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001A0A0 File Offset: 0x000182A0
		private QueryExtensionEntityBuilder<QueryExtensionSchemaBuilder<object>> GetOrCreateEntity(string extendedEntity, out ConceptualEntity.Builder conceptualEntityBuilder)
		{
			QueryExtensionEntityBuilder<QueryExtensionSchemaBuilder<object>> queryExtensionEntityBuilder;
			if (!this._queryExtensionSchemaBuilder.TryGetEntity(extendedEntity, out queryExtensionEntityBuilder))
			{
				queryExtensionEntityBuilder = this._queryExtensionSchemaBuilder.WithEntity(extendedEntity, extendedEntity, QueryExtensionNamingBehavior.Preserve);
			}
			conceptualEntityBuilder = this._extensionConceptualSchemaBuilder.GetEntityBuilder(extendedEntity);
			if (conceptualEntityBuilder == null)
			{
				IConceptualEntity conceptualEntity;
				this._baseSchema.TryGetEntity(extendedEntity, out conceptualEntity);
				conceptualEntityBuilder = ConceptualEntity.Create(extendedEntity, extendedEntity, extendedEntity, null, this._name, conceptualEntity, conceptualEntity.Visibility, null, null, null);
				this._extensionConceptualSchemaBuilder.AddEntityBuilder(conceptualEntityBuilder);
			}
			return queryExtensionEntityBuilder;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001A118 File Offset: 0x00018318
		private void Initialize(QueryExtensionSchema existingExtensionSchema)
		{
			if (this._namingContext == null)
			{
				this._namingContext = new NamingContext(null);
				foreach (IConceptualSchema conceptualSchema in this._federatedConceptualSchema.Schemas)
				{
					DsqGenerationUtils.PopulateModelPropertyNames(conceptualSchema, this._namingContext);
				}
			}
			if (this._queryExtensionSchemaBuilder == null)
			{
				this._queryExtensionSchemaBuilder = new QueryExtensionSchemaBuilder(this._name, this._extends, 1);
				this.InitializeWithExtensionSchema(this._queryExtensionSchemaBuilder, existingExtensionSchema);
			}
			if (this._extensionConceptualSchemaBuilder == null)
			{
				this._extensionConceptualSchemaBuilder = ConceptualSchema.CreateBuilder(this._baseSchema.Language, this._name, null, null, this._baseSchema, this.InitializeEntitiesWithExtensionSchema(existingExtensionSchema), null, this._baseSchema.Capabilities, null);
			}
			if (this._extensionColumnDsqExpressions == null)
			{
				this._extensionColumnDsqExpressions = new Dictionary<QueryExtensionColumn, Expression>();
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001A204 File Offset: 0x00018404
		private IReadOnlyList<ConceptualEntity.Builder> InitializeEntitiesWithExtensionSchema(QueryExtensionSchema existingExtensionSchema)
		{
			if (!(existingExtensionSchema == null))
			{
				return ExtensionConceptualSchemaBuilder.BuildEntities(this._baseSchema, existingExtensionSchema.Entities, this._name, this._errorContext);
			}
			return null;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001A230 File Offset: 0x00018430
		private void InitializeWithExtensionSchema(QueryExtensionSchemaBuilder queryExtensionSchemaBuilder, QueryExtensionSchema extensionSchema)
		{
			if (extensionSchema != null && !extensionSchema.Entities.IsNullOrEmptyCollection<QueryExtensionEntity>())
			{
				foreach (QueryExtensionEntity queryExtensionEntity in extensionSchema.Entities)
				{
					QueryExtensionEntityBuilder<QueryExtensionSchemaBuilder<object>> queryExtensionEntityBuilder = queryExtensionSchemaBuilder.WithEntity(queryExtensionEntity.Name, queryExtensionEntity.Extends, QueryExtensionNamingBehavior.Preserve);
					if (!queryExtensionEntity.Measures.IsNullOrEmptyCollection<QueryExtensionMeasure>())
					{
						foreach (QueryExtensionMeasure queryExtensionMeasure in queryExtensionEntity.Measures)
						{
							queryExtensionEntityBuilder.WithMeasure(queryExtensionMeasure.Name, queryExtensionMeasure.Expression, queryExtensionMeasure.DataType);
						}
					}
					if (!queryExtensionEntity.Columns.IsNullOrEmptyCollection<QueryExtensionColumn>())
					{
						foreach (QueryExtensionColumn queryExtensionColumn in queryExtensionEntity.Columns)
						{
							queryExtensionEntityBuilder.WithColumn(queryExtensionColumn.Name, queryExtensionColumn.Expression, queryExtensionColumn.DataType, QueryExtensionNamingBehavior.Preserve);
						}
					}
				}
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001A370 File Offset: 0x00018570
		private IConceptualColumn CreateColumn(ConceptualPrimitiveType conceptualType, string suggestedName, Expression dsqExpression, QueryExtensionEntityBuilder<QueryExtensionSchemaBuilder<object>> extensionEntityBuilder, ConceptualEntity.Builder conceptualEntityBuilder)
		{
			string text = this._namingContext.GenerateUniqueName(suggestedName);
			QueryExtensionColumn queryExtensionColumn;
			extensionEntityBuilder.WithColumn(text, null, conceptualType, QueryExtensionNamingBehavior.Preserve, out queryExtensionColumn);
			this._extensionColumnDsqExpressions.Add(queryExtensionColumn, dsqExpression);
			DataType typeForPrimitive = DataTypeExtensions.GetTypeForPrimitive(queryExtensionColumn.DataType);
			string name = queryExtensionColumn.Name;
			string name2 = queryExtensionColumn.Name;
			string name3 = queryExtensionColumn.Name;
			DataType dataType = typeForPrimitive;
			ConceptualPrimitiveType dataType2 = queryExtensionColumn.DataType;
			ConceptualColumn.Builder builder = ConceptualColumn.Create(name, extensionEntityBuilder.ActiveObject.Columns.Count - 1, name2, name3, null, dataType, false, false, null, dataType2, ConceptualDataCategory.None, null, true, false);
			return conceptualEntityBuilder.AddColumn(builder);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001A404 File Offset: 0x00018604
		public IConceptualMeasure CreateMeasure(string extendedEntity, ConceptualPrimitiveType conceptualType, string suggestedName, string dax)
		{
			ConceptualEntity.Builder builder;
			QueryExtensionEntityBuilder<QueryExtensionSchemaBuilder<object>> orCreateEntity = this.GetOrCreateEntity(extendedEntity, out builder);
			return this.CreateMeasure(conceptualType, suggestedName, dax, orCreateEntity, builder);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001A428 File Offset: 0x00018628
		private IConceptualMeasure CreateMeasure(ConceptualPrimitiveType conceptualType, string suggestedName, string dax, QueryExtensionEntityBuilder<QueryExtensionSchemaBuilder<object>> extensionEntityBuilder, ConceptualEntity.Builder conceptualEntityBuilder)
		{
			string text = this._namingContext.GenerateUniqueName(suggestedName);
			QueryExtensionMeasure queryExtensionMeasure;
			extensionEntityBuilder.WithMeasure(text, dax, conceptualType, out queryExtensionMeasure);
			DataType typeForPrimitive = DataTypeExtensions.GetTypeForPrimitive(queryExtensionMeasure.DataType);
			string name = queryExtensionMeasure.Name;
			string name2 = queryExtensionMeasure.Name;
			string name3 = queryExtensionMeasure.Name;
			DataType dataType = typeForPrimitive;
			ConceptualPrimitiveType dataType2 = queryExtensionMeasure.DataType;
			ConceptualMeasure.Builder builder = ConceptualMeasure.Create(name, extensionEntityBuilder.ActiveObject.Measures.Count - 1, dataType, dataType2, ConceptualDataCategory.None, name2, name3, null, false, false, null, false, null, true);
			return conceptualEntityBuilder.AddMeasure(builder);
		}

		// Token: 0x040003A4 RID: 932
		private const string DefaultSchemaName = "CalculatedSchema";

		// Token: 0x040003A5 RID: 933
		private readonly IFederatedConceptualSchema _federatedConceptualSchema;

		// Token: 0x040003A6 RID: 934
		private readonly IConceptualSchema _baseSchema;

		// Token: 0x040003A7 RID: 935
		private readonly string _name;

		// Token: 0x040003A8 RID: 936
		private readonly string _extends;

		// Token: 0x040003A9 RID: 937
		private NamingContext _namingContext;

		// Token: 0x040003AA RID: 938
		private QueryResolutionErrorContext _errorContext;

		// Token: 0x040003AB RID: 939
		private Dictionary<QueryExtensionColumn, Expression> _extensionColumnDsqExpressions;

		// Token: 0x040003AC RID: 940
		private QueryExtensionSchemaBuilder _queryExtensionSchemaBuilder;

		// Token: 0x040003AD RID: 941
		private ConceptualSchema.Builder _extensionConceptualSchemaBuilder;
	}
}
