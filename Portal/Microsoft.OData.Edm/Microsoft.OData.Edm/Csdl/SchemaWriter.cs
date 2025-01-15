using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Csdl.Serialization;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x02000157 RID: 343
	public static class SchemaWriter
	{
		// Token: 0x060008C2 RID: 2242 RVA: 0x00017DCC File Offset: 0x00015FCC
		public static bool TryWriteSchema(this IEdmModel model, XmlWriter writer, out IEnumerable<EdmError> errors)
		{
			return SchemaWriter.TryWriteSchema(model, (string x) => writer, true, out errors);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00017DFA File Offset: 0x00015FFA
		public static bool TryWriteSchema(this IEdmModel model, Func<string, XmlWriter> writerProvider, out IEnumerable<EdmError> errors)
		{
			return SchemaWriter.TryWriteSchema(model, writerProvider, false, out errors);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00017E08 File Offset: 0x00016008
		internal static bool TryWriteSchema(IEdmModel model, Func<string, XmlWriter> writerProvider, bool singleFileExpected, out IEnumerable<EdmError> errors)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<Func<string, XmlWriter>>(writerProvider, "writerProvider");
			errors = model.GetSerializationErrors();
			if (errors.FirstOrDefault<EdmError>() != null)
			{
				return false;
			}
			IEnumerable<EdmSchema> schemas = new EdmModelSchemaSeparationSerializationVisitor(model).GetSchemas();
			if (schemas.Count<EdmSchema>() > 1 && singleFileExpected)
			{
				errors = new EdmError[]
				{
					new EdmError(new CsdlLocation(0, 0), EdmErrorCode.SingleFileExpected, Strings.Serializer_SingleFileExpected)
				};
				return false;
			}
			if (schemas.Count<EdmSchema>() == 0)
			{
				errors = new EdmError[]
				{
					new EdmError(new CsdlLocation(0, 0), EdmErrorCode.NoSchemasProduced, Strings.Serializer_NoSchemasProduced)
				};
				return false;
			}
			SchemaWriter.WriteSchemas(model, schemas, writerProvider);
			errors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00017EB8 File Offset: 0x000160B8
		internal static void WriteSchemas(IEdmModel model, IEnumerable<EdmSchema> schemas, Func<string, XmlWriter> writerProvider)
		{
			Version version = model.GetEdmVersion() ?? EdmConstants.EdmVersionDefault;
			foreach (EdmSchema edmSchema in schemas)
			{
				XmlWriter xmlWriter = writerProvider(edmSchema.Namespace);
				if (xmlWriter != null)
				{
					EdmModelCsdlSerializationVisitor edmModelCsdlSerializationVisitor = new EdmModelCsdlSerializationVisitor(model, xmlWriter, version);
					edmModelCsdlSerializationVisitor.VisitEdmSchema(edmSchema, model.GetNamespacePrefixMappings());
				}
			}
		}
	}
}
