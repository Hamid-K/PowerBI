using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Csdl.Serialization;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x0200014A RID: 330
	public class CsdlWriter
	{
		// Token: 0x06000816 RID: 2070 RVA: 0x00015DB6 File Offset: 0x00013FB6
		private CsdlWriter(IEdmModel model, IEnumerable<EdmSchema> schemas, XmlWriter writer, Version edmxVersion, CsdlTarget target)
		{
			this.model = model;
			this.schemas = schemas;
			this.writer = writer;
			this.edmxVersion = edmxVersion;
			this.target = target;
			this.edmxNamespace = CsdlConstants.SupportedEdmxVersions[edmxVersion];
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00015DF8 File Offset: 0x00013FF8
		public static bool TryWriteCsdl(IEdmModel model, XmlWriter writer, CsdlTarget target, out IEnumerable<EdmError> errors)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<XmlWriter>(writer, "writer");
			errors = model.GetSerializationErrors();
			if (Enumerable.FirstOrDefault<EdmError>(errors) != null)
			{
				return false;
			}
			Version version = model.GetEdmxVersion();
			if (version != null)
			{
				if (!CsdlConstants.SupportedEdmxVersions.ContainsKey(version))
				{
					errors = new EdmError[]
					{
						new EdmError(new CsdlLocation(0, 0), EdmErrorCode.UnknownEdmxVersion, Strings.Serializer_UnknownEdmxVersion)
					};
					return false;
				}
			}
			else if (!CsdlConstants.EdmToEdmxVersions.TryGetValue(model.GetEdmVersion() ?? EdmConstants.EdmVersionLatest, ref version))
			{
				errors = new EdmError[]
				{
					new EdmError(new CsdlLocation(0, 0), EdmErrorCode.UnknownEdmVersion, Strings.Serializer_UnknownEdmVersion)
				};
				return false;
			}
			IEnumerable<EdmSchema> enumerable = new EdmModelSchemaSeparationSerializationVisitor(model).GetSchemas();
			CsdlWriter csdlWriter = new CsdlWriter(model, enumerable, writer, version, target);
			csdlWriter.WriteCsdl();
			errors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00015ED8 File Offset: 0x000140D8
		private void WriteCsdl()
		{
			CsdlTarget csdlTarget = this.target;
			if (csdlTarget == CsdlTarget.EntityFramework)
			{
				this.WriteEFCsdl();
				return;
			}
			if (csdlTarget != CsdlTarget.OData)
			{
				throw new InvalidOperationException(Strings.UnknownEnumVal_CsdlTarget(this.target.ToString()));
			}
			this.WriteODataCsdl();
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00015F21 File Offset: 0x00014121
		private void WriteODataCsdl()
		{
			this.WriteEdmxElement();
			this.WriteReferenceElements();
			this.WriteDataServicesElement();
			this.WriteSchemas();
			this.EndElement();
			this.EndElement();
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00015F47 File Offset: 0x00014147
		private void WriteEFCsdl()
		{
			this.WriteEdmxElement();
			this.WriteRuntimeElement();
			this.WriteConceptualModelsElement();
			this.WriteSchemas();
			this.EndElement();
			this.EndElement();
			this.EndElement();
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00015F73 File Offset: 0x00014173
		private void WriteEdmxElement()
		{
			this.writer.WriteStartElement("edmx", "Edmx", this.edmxNamespace);
			this.writer.WriteAttributeString("Version", this.edmxVersion.ToString());
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00015FAB File Offset: 0x000141AB
		private void WriteRuntimeElement()
		{
			this.writer.WriteStartElement("edmx", "Runtime", this.edmxNamespace);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00015FC8 File Offset: 0x000141C8
		private void WriteConceptualModelsElement()
		{
			this.writer.WriteStartElement("edmx", "ConceptualModels", this.edmxNamespace);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00015FE8 File Offset: 0x000141E8
		private void WriteReferenceElements()
		{
			EdmModelReferenceElementsVisitor edmModelReferenceElementsVisitor = new EdmModelReferenceElementsVisitor(this.model, this.writer, this.edmxVersion);
			edmModelReferenceElementsVisitor.VisitEdmReferences(this.model);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00016019 File Offset: 0x00014219
		private void WriteDataServicesElement()
		{
			this.writer.WriteStartElement("edmx", "DataServices", this.edmxNamespace);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00016038 File Offset: 0x00014238
		private void WriteSchemas()
		{
			Version version = this.model.GetEdmVersion() ?? EdmConstants.EdmVersionLatest;
			foreach (EdmSchema edmSchema in this.schemas)
			{
				EdmModelCsdlSerializationVisitor edmModelCsdlSerializationVisitor = new EdmModelCsdlSerializationVisitor(this.model, this.writer, version);
				edmModelCsdlSerializationVisitor.VisitEdmSchema(edmSchema, this.model.GetNamespacePrefixMappings());
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x000160B8 File Offset: 0x000142B8
		private void EndElement()
		{
			this.writer.WriteEndElement();
		}

		// Token: 0x04000547 RID: 1351
		private readonly IEdmModel model;

		// Token: 0x04000548 RID: 1352
		private readonly IEnumerable<EdmSchema> schemas;

		// Token: 0x04000549 RID: 1353
		private readonly XmlWriter writer;

		// Token: 0x0400054A RID: 1354
		private readonly Version edmxVersion;

		// Token: 0x0400054B RID: 1355
		private readonly string edmxNamespace;

		// Token: 0x0400054C RID: 1356
		private readonly CsdlTarget target;
	}
}
