using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Library;
using Microsoft.InfoNav.Defaults;
using Microsoft.InfoNav.Utils;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001F6 RID: 502
	internal sealed class ExtensionConceptualSchemaBuilder
	{
		// Token: 0x06000DAA RID: 3498 RVA: 0x0001A81A File Offset: 0x00018A1A
		private ExtensionConceptualSchemaBuilder(IConceptualSchema baseModel, QueryExtensionSchema extension, QueryResolutionErrorContext errorContext, ITracer tracer)
		{
			this._baseModel = baseModel;
			this._extension = extension;
			this._errorContext = errorContext;
			this._tracer = tracer;
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0001A83F File Offset: 0x00018A3F
		internal static bool TryCreateConceptualSchema(IConceptualSchema baseModel, QueryExtensionSchema extension, QueryResolutionErrorContext errorContext, out IConceptualSchema schema)
		{
			return ExtensionConceptualSchemaBuilder.TryCreateConceptualSchema(baseModel, extension, errorContext, DefaultTracer.Instance, out schema);
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0001A84F File Offset: 0x00018A4F
		internal static bool TryCreateConceptualSchema(IConceptualSchema baseModel, QueryExtensionSchema extension, QueryResolutionErrorContext errorContext, ITracer tracer, out IConceptualSchema schema)
		{
			schema = new ExtensionConceptualSchemaBuilder(baseModel, extension, errorContext, tracer).Create();
			return !errorContext.HasError;
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0001A86C File Offset: 0x00018A6C
		private IConceptualSchema Create()
		{
			if (!ConceptualNameComparer.Instance.Equals(this._extension.Extends ?? "", this._baseModel.SchemaId ?? ""))
			{
				this._errorContext.RegisterError(new QueryResolutionMessage(StringUtil.FormatInvariant("Extension schema '{0}' does not extend base model '{1}'.", this._extension.Extends, this._baseModel.SchemaId), new ScrubbedString(this._extension.Extends)));
			}
			return ConceptualSchema.Create(this._baseModel.Language, this._extension.Name, null, this._baseModel.ConceptualCollation, this._baseModel, ExtensionConceptualSchemaBuilder.BuildEntities(this._baseModel, this._extension.Entities, this._extension.Name, this._errorContext), null, this._baseModel.Capabilities, null);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0001A950 File Offset: 0x00018B50
		internal static IReadOnlyList<ConceptualEntity.Builder> BuildEntities(IConceptualSchema baseModel, IList<QueryExtensionEntity> entities, string containerName, QueryResolutionErrorContext errorContext)
		{
			List<ConceptualEntity.Builder> list = new List<ConceptualEntity.Builder>();
			if (entities != null)
			{
				list.Capacity = entities.Count;
				for (int i = 0; i < entities.Count; i++)
				{
					QueryExtensionEntity queryExtensionEntity = entities[i];
					IConceptualEntity conceptualEntity;
					if (baseModel.TryGetEntity(queryExtensionEntity.Extends, out conceptualEntity))
					{
						list.Add(ConceptualEntity.Create(queryExtensionEntity.Name, queryExtensionEntity.Extends, queryExtensionEntity.Name, null, containerName, conceptualEntity, conceptualEntity.Visibility, ExtensionConceptualSchemaBuilder.BuildMeasures(queryExtensionEntity.Measures), ExtensionConceptualSchemaBuilder.BuildColumns(queryExtensionEntity.Columns), null));
					}
					else
					{
						errorContext.RegisterError(new QueryResolutionMessage(StringUtil.FormatInvariant("Extension entity extends missing entity ({0}).", queryExtensionEntity.Extends), new ScrubbedString(queryExtensionEntity.Extends)));
					}
				}
			}
			return list;
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0001AA0C File Offset: 0x00018C0C
		private static IReadOnlyList<ConceptualMeasure.Builder> BuildMeasures(IList<QueryExtensionMeasure> measures)
		{
			List<ConceptualMeasure.Builder> list = new List<ConceptualMeasure.Builder>();
			if (measures != null)
			{
				list.Capacity = measures.Count;
				for (int i = 0; i < measures.Count; i++)
				{
					QueryExtensionMeasure queryExtensionMeasure = measures[i];
					DataType typeForPrimitive = DataTypeExtensions.GetTypeForPrimitive(queryExtensionMeasure.DataType);
					List<ConceptualMeasure.Builder> list2 = list;
					string name = queryExtensionMeasure.Name;
					string name2 = queryExtensionMeasure.Name;
					string name3 = queryExtensionMeasure.Name;
					DataType dataType = typeForPrimitive;
					ConceptualPrimitiveType dataType2 = queryExtensionMeasure.DataType;
					list2.Add(ConceptualMeasure.Create(name, i, dataType, dataType2, ConceptualDataCategory.None, name2, name3, null, false, false, null, false, null, true));
				}
			}
			return list;
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0001AA94 File Offset: 0x00018C94
		private static IReadOnlyList<ConceptualColumn.Builder> BuildColumns(IList<QueryExtensionColumn> columns)
		{
			List<ConceptualColumn.Builder> list = new List<ConceptualColumn.Builder>();
			if (columns != null)
			{
				list.Capacity = columns.Count;
				for (int i = 0; i < columns.Count; i++)
				{
					QueryExtensionColumn queryExtensionColumn = columns[i];
					list.Add(ExtensionConceptualSchemaBuilder.BuildColumn(queryExtensionColumn, i));
				}
			}
			return list;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0001AAE0 File Offset: 0x00018CE0
		private static ConceptualColumn.Builder BuildColumn(QueryExtensionColumn extensionColumn, int ordinal)
		{
			DataType typeForPrimitive = DataTypeExtensions.GetTypeForPrimitive(extensionColumn.DataType);
			string name = extensionColumn.Name;
			string name2 = extensionColumn.Name;
			string name3 = extensionColumn.Name;
			DataType dataType = typeForPrimitive;
			ConceptualPrimitiveType dataType2 = extensionColumn.DataType;
			return ConceptualColumn.Create(name, ordinal, name2, name3, null, dataType, false, false, null, dataType2, ConceptualDataCategory.None, null, true, false);
		}

		// Token: 0x040006F2 RID: 1778
		private readonly IConceptualSchema _baseModel;

		// Token: 0x040006F3 RID: 1779
		private readonly QueryExtensionSchema _extension;

		// Token: 0x040006F4 RID: 1780
		private readonly QueryResolutionErrorContext _errorContext;

		// Token: 0x040006F5 RID: 1781
		private readonly ITracer _tracer;
	}
}
