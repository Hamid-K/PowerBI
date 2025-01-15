using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.Errors;
using Microsoft.DataShaping.InternalContracts.Utils;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Edm;
using Microsoft.InfoNav.Utils;

namespace Microsoft.DataShaping.InternalContracts.Model
{
	// Token: 0x02000025 RID: 37
	internal static class ConceptualSchemaParser
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x000037B8 File Offset: 0x000019B8
		internal static IConceptualSchema ParseConceptualSchema(Stream modelMetadata, IFeatureSwitchProvider featureSwitchProvider, Microsoft.DataShaping.ServiceContracts.ITracer tracer)
		{
			ConceptualSchemaBuilderOptions conceptualSchemaBuilderOptions = new ConceptualSchemaBuilderOptions(featureSwitchProvider.IsEnabled(FeatureSwitchKind.SparklineData));
			IConceptualSchema schema;
			IList<ConceptualSchemaLoadError> list;
			try
			{
				ConceptualSchemaUtils.TryParseConceptualSchemaWithAssociationSetFill(modelMetadata, conceptualSchemaBuilderOptions, new InfoNavTracerAdapter(tracer), out schema, out list);
				schema.RegisterAnnotationProvider<ComparerAnnotation, IConceptualSchema>(new ComparerAnnotationProvider(() => ComparerAnnotationProvider.BuildComparerAnnotation(schema), schema));
			}
			catch (Exception ex)
			{
				if (ErrorUtils.IsStoppingException(ex))
				{
					throw;
				}
				throw new DataModelParsingException("DataModelParsingError", ex.Message.MarkAsCustomerContent(), ex);
			}
			if (list != null && list.Any<ConceptualSchemaLoadError>())
			{
				IEnumerable<string> enumerable = list.Select((ConceptualSchemaLoadError e) => e.ToString());
				string text = string.Join(Environment.NewLine, enumerable);
				tracer.SanitizedTrace(TraceLevel.Error, "Error(s) encountered when attempting to parse CSDL: {0}", new string[] { text.MarkAsCustomerContent() });
				throw new DataModelParsingException("DataModelParsingError", text, null);
			}
			return schema;
		}
	}
}
