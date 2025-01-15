using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Csdl.Serialization;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x0200014C RID: 332
	public static class SchemaWriter
	{
		// Token: 0x06000825 RID: 2085 RVA: 0x00016114 File Offset: 0x00014314
		public static bool TryWriteSchema(this IEdmModel model, XmlWriter writer, out IEnumerable<EdmError> errors)
		{
			return SchemaWriter.TryWriteSchema(model, (string x) => writer, true, out errors);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00016142 File Offset: 0x00014342
		public static bool TryWriteSchema(this IEdmModel model, Func<string, XmlWriter> writerProvider, out IEnumerable<EdmError> errors)
		{
			return SchemaWriter.TryWriteSchema(model, writerProvider, false, out errors);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00016150 File Offset: 0x00014350
		internal static bool TryWriteSchema(IEdmModel model, Func<string, XmlWriter> writerProvider, bool singleFileExpected, out IEnumerable<EdmError> errors)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<Func<string, XmlWriter>>(writerProvider, "writerProvider");
			errors = model.GetSerializationErrors();
			if (Enumerable.FirstOrDefault<EdmError>(errors) != null)
			{
				return false;
			}
			IEnumerable<EdmSchema> schemas = new EdmModelSchemaSeparationSerializationVisitor(model).GetSchemas();
			if (Enumerable.Count<EdmSchema>(schemas) > 1 && singleFileExpected)
			{
				errors = new EdmError[]
				{
					new EdmError(new CsdlLocation(0, 0), EdmErrorCode.SingleFileExpected, Strings.Serializer_SingleFileExpected)
				};
				return false;
			}
			if (Enumerable.Count<EdmSchema>(schemas) == 0)
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

		// Token: 0x06000828 RID: 2088 RVA: 0x00016200 File Offset: 0x00014400
		internal static void WriteSchemas(IEdmModel model, IEnumerable<EdmSchema> schemas, Func<string, XmlWriter> writerProvider)
		{
			Version version = model.GetEdmVersion() ?? EdmConstants.EdmVersionLatest;
			foreach (EdmSchema edmSchema in schemas)
			{
				XmlWriter xmlWriter = writerProvider.Invoke(edmSchema.Namespace);
				if (xmlWriter != null)
				{
					EdmModelCsdlSerializationVisitor edmModelCsdlSerializationVisitor = new EdmModelCsdlSerializationVisitor(model, xmlWriter, version);
					edmModelCsdlSerializationVisitor.VisitEdmSchema(edmSchema, model.GetNamespacePrefixMappings());
				}
			}
		}
	}
}
