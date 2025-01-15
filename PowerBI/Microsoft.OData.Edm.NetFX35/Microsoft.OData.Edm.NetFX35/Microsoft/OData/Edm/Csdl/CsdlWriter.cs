using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Csdl.Serialization;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x020001E4 RID: 484
	public static class CsdlWriter
	{
		// Token: 0x06000B78 RID: 2936 RVA: 0x0002094C File Offset: 0x0001EB4C
		public static bool TryWriteCsdl(this IEdmModel model, XmlWriter writer, out IEnumerable<EdmError> errors)
		{
			return CsdlWriter.TryWriteCsdl(model, (string x) => writer, true, out errors);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002097A File Offset: 0x0001EB7A
		public static bool TryWriteCsdl(this IEdmModel model, Func<string, XmlWriter> writerProvider, out IEnumerable<EdmError> errors)
		{
			return CsdlWriter.TryWriteCsdl(model, writerProvider, false, out errors);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00020988 File Offset: 0x0001EB88
		internal static bool TryWriteCsdl(IEdmModel model, Func<string, XmlWriter> writerProvider, bool singleFileExpected, out IEnumerable<EdmError> errors)
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
			CsdlWriter.WriteSchemas(model, schemas, writerProvider);
			errors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00020A3C File Offset: 0x0001EC3C
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
