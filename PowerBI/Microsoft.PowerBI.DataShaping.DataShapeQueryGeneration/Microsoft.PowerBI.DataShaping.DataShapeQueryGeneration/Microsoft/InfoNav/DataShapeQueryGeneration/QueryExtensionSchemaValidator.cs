using System;
using System.Collections.Generic;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryExtensionSchema;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000E4 RID: 228
	internal sealed class QueryExtensionSchemaValidator
	{
		// Token: 0x060007D7 RID: 2007 RVA: 0x0001DD68 File Offset: 0x0001BF68
		private QueryExtensionSchemaValidator(DataShapeGenerationErrorContext errorContext, DataShapeGenerationTelemetry telemetry, IConceptualSchema baseSchema, IFeatureSwitchProvider featureSwitchProvider, string extensionSchemaName)
		{
			this._errorContext = errorContext;
			this._telemetry = telemetry;
			this._baseSchema = baseSchema;
			this._extensionSchemaName = extensionSchemaName;
			this._daxReferenceableMeasuresByName = new Dictionary<string, QueryExtensionSchemaValidator.ExtensionPropertyContext>(QueryExtensionSchemaValidator.DaxNameComparer);
			this._daxReferenceableColumnsByName = new Dictionary<string, QueryExtensionSchemaValidator.ExtensionPropertyContext>(QueryExtensionSchemaValidator.DaxNameComparer);
			this._featureSwitchProvider = featureSwitchProvider;
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0001DDC0 File Offset: 0x0001BFC0
		internal static bool IsValid(QueryExtensionSchema extensionSchema, DataShapeGenerationErrorContext errorContext, DataShapeGenerationTelemetry telemetry, IConceptualSchema baseSchema, IFeatureSwitchProvider featureSwitchProvider)
		{
			return extensionSchema == null || new QueryExtensionSchemaValidator(errorContext, telemetry, baseSchema, featureSwitchProvider, extensionSchema.Name).IsValid(extensionSchema);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001DDE4 File Offset: 0x0001BFE4
		private bool IsValid(QueryExtensionSchema extensionSchema)
		{
			if (!this.HasValidStructure(extensionSchema))
			{
				return false;
			}
			if (!this.HasValidName(extensionSchema))
			{
				return false;
			}
			if (!this.AreValid(extensionSchema.Entities))
			{
				return false;
			}
			if (!this.HasValidNames())
			{
				return false;
			}
			if (this._overallMeasureCount > 0)
			{
				this._telemetry.NumExtMeasures = new int?(this._overallMeasureCount);
			}
			if (this._overallColumnCount > 0)
			{
				this._telemetry.NumExtColumns = new int?(this._overallColumnCount);
			}
			return !this.HasTooManyProperties();
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001DE6C File Offset: 0x0001C06C
		private bool HasValidName(QueryExtensionSchema extensionSchema)
		{
			if (this._baseSchema.Entities.IsNullOrEmpty<IConceptualEntity>())
			{
				return true;
			}
			string entityContainerName = this._baseSchema.Entities[0].EntityContainerName;
			if (extensionSchema.Name == entityContainerName)
			{
				this._errorContext.Register(DataShapeGenerationMessages.ExtensionSchemaNameMatchesBaseEntityContainer(EngineMessageSeverity.Error, extensionSchema.Name));
				return false;
			}
			return true;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001DECC File Offset: 0x0001C0CC
		private bool HasValidStructure(QueryExtensionSchema extensionSchema)
		{
			if (!extensionSchema.IsValid())
			{
				this._errorContext.Register(DataShapeGenerationMessages.InvalidOrMalformedExtensionSchema(EngineMessageSeverity.Error));
				return false;
			}
			return true;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001DEEC File Offset: 0x0001C0EC
		private bool HasTooManyProperties()
		{
			int num = this._overallMeasureCount + this._overallColumnCount;
			if (num > 100)
			{
				this._errorContext.Register(DataShapeGenerationMessages.TooManyExtensionProperties(EngineMessageSeverity.Error, num, 100));
				return true;
			}
			return false;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0001DF24 File Offset: 0x0001C124
		private bool HasValidNames()
		{
			IReadOnlyList<IConceptualEntity> entities = this._baseSchema.Entities;
			if (entities == null)
			{
				return true;
			}
			for (int i = 0; i < entities.Count; i++)
			{
				if (!this.HasValidNames(entities[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001DF68 File Offset: 0x0001C168
		private bool HasValidNames(IConceptualEntity entity)
		{
			IReadOnlyList<IConceptualProperty> properties = entity.Properties;
			if (properties == null)
			{
				return true;
			}
			for (int i = 0; i < properties.Count; i++)
			{
				IConceptualProperty conceptualProperty = properties[i];
				QueryExtensionSchemaValidator.ExtensionPropertyContext extensionPropertyContext;
				if (this._daxReferenceableMeasuresByName.TryGetValue(conceptualProperty.Name, out extensionPropertyContext))
				{
					this._errorContext.Register(DataShapeGenerationMessages.ExtensionMeasureNameNotUniqueModel(EngineMessageSeverity.Error, this._extensionSchemaName, extensionPropertyContext.PropertyName, extensionPropertyContext.EntityName, conceptualProperty.Name, entity.Name));
					return false;
				}
				QueryExtensionSchemaValidator.ExtensionPropertyContext extensionPropertyContext2;
				if (this._daxReferenceableColumnsByName.TryGetValue(conceptualProperty.Name, out extensionPropertyContext2))
				{
					this._errorContext.Register(DataShapeGenerationMessages.ExtensionColumnNameNotUniqueModel(EngineMessageSeverity.Error, this._extensionSchemaName, extensionPropertyContext2.PropertyName, extensionPropertyContext2.EntityName, conceptualProperty.Name, entity.Name));
					return false;
				}
			}
			return true;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001E030 File Offset: 0x0001C230
		private bool AreValid(IList<QueryExtensionEntity> entities)
		{
			if (entities == null)
			{
				return true;
			}
			HashSet<string> hashSet = new HashSet<string>(QueryExtensionSchemaValidator.DaxNameComparer);
			bool flag = true;
			for (int i = 0; i < entities.Count; i++)
			{
				flag &= this.IsValid(entities[i], ref hashSet);
			}
			return flag;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001E074 File Offset: 0x0001C274
		private bool IsValid(QueryExtensionEntity extensionEntity, ref HashSet<string> extensionEntityNames)
		{
			if (!extensionEntityNames.Add(extensionEntity.Name))
			{
				this._errorContext.Register(DataShapeGenerationMessages.ExtensionEntityNameNotUnique(EngineMessageSeverity.Error, this._extensionSchemaName, extensionEntity.Name));
				return false;
			}
			if (extensionEntity.Extends != null && !ConceptualNameComparer.Instance.Equals(extensionEntity.Name, extensionEntity.Extends))
			{
				this._errorContext.Register(DataShapeGenerationMessages.ExtensionEntityNameDoesNotMatchExtends(EngineMessageSeverity.Error, extensionEntity.Name, extensionEntity.Extends));
				return false;
			}
			if (extensionEntity.NamingBehavior == QueryExtensionNamingBehavior.Flexible)
			{
				if (!this._featureSwitchProvider.IsEnabled(FeatureSwitchKind.QueryNativeExpressions))
				{
					this._errorContext.Register(DataShapeGenerationMessages.UnsupportedFeatureInSemanticQueryDataShapeCommand(EngineMessageSeverity.Error, "QueryNativeExpression"));
					return false;
				}
				if (!string.IsNullOrEmpty(extensionEntity.Extends))
				{
					this._errorContext.Register(DataShapeGenerationMessages.FlexibleExtensionEntityShouldNotHaveExtendsProperty(EngineMessageSeverity.Error, extensionEntity.Name));
					return false;
				}
				if (!extensionEntity.Columns.IsNullOrEmptyCollection<QueryExtensionColumn>())
				{
					this._errorContext.Register(DataShapeGenerationMessages.FlexibleExtensionEntityCanNotHaveColumns(EngineMessageSeverity.Error, extensionEntity.Name));
					return false;
				}
			}
			return this.AreValid(extensionEntity, extensionEntity.Measures) && this.AreValid(extensionEntity, extensionEntity.Columns);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001E188 File Offset: 0x0001C388
		private bool AreValid(QueryExtensionEntity extensionEntity, IList<QueryExtensionMeasure> measures)
		{
			if (measures.IsNullOrEmptyCollection<QueryExtensionMeasure>())
			{
				return true;
			}
			HashSet<string> hashSet = null;
			bool flag = true;
			for (int i = 0; i < measures.Count; i++)
			{
				flag &= this.IsValid(measures[i], extensionEntity, ref hashSet);
			}
			return flag;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001E1C8 File Offset: 0x0001C3C8
		private bool IsValid(QueryExtensionMeasure extensionMeasure, QueryExtensionEntity extensionEntity, ref HashSet<string> flexibleMeasureNames)
		{
			this._overallMeasureCount++;
			if (extensionEntity.NamingBehavior == QueryExtensionNamingBehavior.Flexible)
			{
				if (flexibleMeasureNames == null)
				{
					flexibleMeasureNames = new HashSet<string>(ConceptualNameComparer.Instance);
				}
				if (!flexibleMeasureNames.Add(extensionMeasure.Name))
				{
					this._errorContext.Register(DataShapeGenerationMessages.ExtensionMeasureNameNotUniqueFlexible(EngineMessageSeverity.Error, this._extensionSchemaName, extensionMeasure.Name, extensionEntity.Name));
					return false;
				}
			}
			else
			{
				QueryExtensionSchemaValidator.ExtensionPropertyContext extensionPropertyContext;
				if (this._daxReferenceableMeasuresByName.TryGetValue(extensionMeasure.Name, out extensionPropertyContext))
				{
					this._errorContext.Register(DataShapeGenerationMessages.ExtensionMeasureNameNotUnique(EngineMessageSeverity.Error, this._extensionSchemaName, extensionMeasure.Name, extensionEntity.Name, extensionPropertyContext.PropertyName, extensionPropertyContext.EntityName));
					return false;
				}
				QueryExtensionSchemaValidator.ExtensionPropertyContext extensionPropertyContext2;
				if (this._daxReferenceableColumnsByName.TryGetValue(extensionMeasure.Name, out extensionPropertyContext2))
				{
					this._errorContext.Register(DataShapeGenerationMessages.ExtensionColumnAndMeasureNamesNotUnique(EngineMessageSeverity.Error, this._extensionSchemaName, extensionPropertyContext2.PropertyName, extensionPropertyContext2.EntityName, "measure", "column"));
					return false;
				}
				this._daxReferenceableMeasuresByName.Add(extensionMeasure.Name, new QueryExtensionSchemaValidator.ExtensionPropertyContext(extensionMeasure.Name, extensionEntity.Name));
			}
			if (!QueryExtensionSchemaValidator.IsValid(extensionMeasure.DataType))
			{
				this._errorContext.Register(DataShapeGenerationMessages.UnsupportedExtensionMeasureDataType(EngineMessageSeverity.Error, this._extensionSchemaName, extensionMeasure.Name, extensionEntity.Name, extensionMeasure.DataType));
				return false;
			}
			if (string.IsNullOrWhiteSpace(extensionMeasure.Expression))
			{
				this._errorContext.Register(DataShapeGenerationMessages.ExtensionMeasureEmptyExpression(EngineMessageSeverity.Error, this._extensionSchemaName, extensionMeasure.Name, extensionEntity.Name));
				return false;
			}
			return true;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0001E34C File Offset: 0x0001C54C
		private bool AreValid(QueryExtensionEntity extensionEntity, IList<QueryExtensionColumn> columns)
		{
			if (columns.IsNullOrEmptyCollection<QueryExtensionColumn>())
			{
				return true;
			}
			if (!this._featureSwitchProvider.IsEnabled(FeatureSwitchKind.QueryExtensionColumns))
			{
				this._errorContext.Register(DataShapeGenerationMessages.UnsupportedFeatureInSemanticQueryDataShapeCommand(EngineMessageSeverity.Error, "QueryExtensionColumn"));
				return false;
			}
			HashSet<string> hashSet = null;
			bool flag = true;
			for (int i = 0; i < columns.Count; i++)
			{
				flag &= this.IsValid(columns[i], extensionEntity, ref hashSet);
			}
			return flag;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001E3B4 File Offset: 0x0001C5B4
		private bool IsValid(QueryExtensionColumn extensionColumn, QueryExtensionEntity extensionEntity, ref HashSet<string> flexibleColumnNames)
		{
			this._overallColumnCount++;
			if (extensionEntity.NamingBehavior == QueryExtensionNamingBehavior.Flexible)
			{
				if (!this.TryAddToFlexibleColumnNames(extensionColumn, extensionEntity, ref flexibleColumnNames))
				{
					return false;
				}
			}
			else if (extensionColumn.NamingBehavior == QueryExtensionNamingBehavior.Flexible)
			{
				if (string.IsNullOrEmpty(extensionEntity.Extends))
				{
					this._errorContext.Register(DataShapeGenerationMessages.FlexibleExtensionColumnWithNoExtensionEntityExtendsProperty(EngineMessageSeverity.Error, this._extensionSchemaName, extensionColumn.Name, extensionEntity.Name));
					return false;
				}
				if (!this.TryAddToFlexibleColumnNames(extensionColumn, extensionEntity, ref flexibleColumnNames))
				{
					return false;
				}
			}
			else
			{
				QueryExtensionSchemaValidator.ExtensionPropertyContext extensionPropertyContext;
				if (this._daxReferenceableColumnsByName.TryGetValue(extensionColumn.Name, out extensionPropertyContext))
				{
					this._errorContext.Register(DataShapeGenerationMessages.ExtensionColumnNameNotUnique(EngineMessageSeverity.Error, this._extensionSchemaName, extensionColumn.Name, extensionEntity.Name, extensionPropertyContext.PropertyName, extensionPropertyContext.EntityName));
					return false;
				}
				QueryExtensionSchemaValidator.ExtensionPropertyContext extensionPropertyContext2;
				if (this._daxReferenceableMeasuresByName.TryGetValue(extensionColumn.Name, out extensionPropertyContext2))
				{
					this._errorContext.Register(DataShapeGenerationMessages.ExtensionColumnAndMeasureNamesNotUnique(EngineMessageSeverity.Error, this._extensionSchemaName, extensionPropertyContext2.PropertyName, extensionPropertyContext2.EntityName, "column", "measure"));
					return false;
				}
				this._daxReferenceableColumnsByName.Add(extensionColumn.Name, new QueryExtensionSchemaValidator.ExtensionPropertyContext(extensionColumn.Name, extensionEntity.Name));
			}
			if (!QueryExtensionSchemaValidator.IsValid(extensionColumn.DataType))
			{
				this._errorContext.Register(DataShapeGenerationMessages.UnsupportedExtensionColumnDataType(EngineMessageSeverity.Error, this._extensionSchemaName, extensionColumn.Name, extensionEntity.Name, extensionColumn.DataType));
				return false;
			}
			if (string.IsNullOrWhiteSpace(extensionColumn.Expression))
			{
				this._errorContext.Register(DataShapeGenerationMessages.ExtensionColumnEmptyExpression(EngineMessageSeverity.Error, this._extensionSchemaName, extensionColumn.Name, extensionEntity.Name));
				return false;
			}
			return true;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001E54C File Offset: 0x0001C74C
		private bool TryAddToFlexibleColumnNames(QueryExtensionColumn extensionColumn, QueryExtensionEntity extensionEntity, ref HashSet<string> flexibleColumnNames)
		{
			if (flexibleColumnNames == null)
			{
				flexibleColumnNames = new HashSet<string>(ConceptualNameComparer.Instance);
			}
			if (!flexibleColumnNames.Add(extensionColumn.Name))
			{
				this._errorContext.Register(DataShapeGenerationMessages.ExtensionColumnNameNotUniqueFlexible(EngineMessageSeverity.Error, this._extensionSchemaName, extensionColumn.Name, extensionEntity.Name));
				return false;
			}
			return true;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0001E59E File Offset: 0x0001C79E
		private static bool IsValid(ConceptualPrimitiveType primitiveType)
		{
			switch (primitiveType)
			{
			case ConceptualPrimitiveType.Text:
			case ConceptualPrimitiveType.Decimal:
			case ConceptualPrimitiveType.Double:
			case ConceptualPrimitiveType.Integer:
			case ConceptualPrimitiveType.Boolean:
			case ConceptualPrimitiveType.DateTime:
			case ConceptualPrimitiveType.Binary:
				return true;
			}
			return false;
		}

		// Token: 0x04000414 RID: 1044
		private static readonly StringComparer DaxNameComparer = StringComparer.OrdinalIgnoreCase;

		// Token: 0x04000415 RID: 1045
		private readonly DataShapeGenerationErrorContext _errorContext;

		// Token: 0x04000416 RID: 1046
		private readonly DataShapeGenerationTelemetry _telemetry;

		// Token: 0x04000417 RID: 1047
		private readonly IConceptualSchema _baseSchema;

		// Token: 0x04000418 RID: 1048
		private readonly string _extensionSchemaName;

		// Token: 0x04000419 RID: 1049
		private readonly Dictionary<string, QueryExtensionSchemaValidator.ExtensionPropertyContext> _daxReferenceableMeasuresByName;

		// Token: 0x0400041A RID: 1050
		private readonly Dictionary<string, QueryExtensionSchemaValidator.ExtensionPropertyContext> _daxReferenceableColumnsByName;

		// Token: 0x0400041B RID: 1051
		private readonly IFeatureSwitchProvider _featureSwitchProvider;

		// Token: 0x0400041C RID: 1052
		private int _overallMeasureCount;

		// Token: 0x0400041D RID: 1053
		private int _overallColumnCount;

		// Token: 0x0200015B RID: 347
		private struct ExtensionPropertyContext
		{
			// Token: 0x060009E4 RID: 2532 RVA: 0x00026050 File Offset: 0x00024250
			public ExtensionPropertyContext(string propertyName, string entityName)
			{
				this.PropertyName = propertyName;
				this.EntityName = entityName;
			}

			// Token: 0x04000558 RID: 1368
			public string PropertyName;

			// Token: 0x04000559 RID: 1369
			public string EntityName;
		}
	}
}
