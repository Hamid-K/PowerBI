using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Csdl.Serialization;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x02000155 RID: 341
	public class CsdlWriter
	{
		// Token: 0x060008B4 RID: 2228 RVA: 0x0001799D File Offset: 0x00015B9D
		private CsdlWriter(IEdmModel model, IEnumerable<EdmSchema> schemas, XmlWriter writer, Version edmxVersion, CsdlTarget target)
		{
			this.model = model;
			this.schemas = schemas;
			this.writer = writer;
			this.edmxVersion = edmxVersion;
			this.target = target;
			this.edmxNamespace = CsdlConstants.SupportedEdmxVersions[edmxVersion];
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x000179DC File Offset: 0x00015BDC
		public static bool TryWriteCsdl(IEdmModel model, XmlWriter writer, CsdlTarget target, out IEnumerable<EdmError> errors)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<XmlWriter>(writer, "writer");
			errors = model.GetSerializationErrors();
			if (errors.FirstOrDefault<EdmError>() != null)
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
			else if (!CsdlConstants.EdmToEdmxVersions.TryGetValue(model.GetEdmVersion() ?? EdmConstants.EdmVersionDefault, out version))
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

		// Token: 0x060008B6 RID: 2230 RVA: 0x00017ABC File Offset: 0x00015CBC
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

		// Token: 0x060008B7 RID: 2231 RVA: 0x00017B05 File Offset: 0x00015D05
		private void WriteODataCsdl()
		{
			this.WriteEdmxElement();
			this.WriteReferenceElements();
			this.WriteDataServicesElement();
			this.WriteSchemas();
			this.EndElement();
			this.EndElement();
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00017B2B File Offset: 0x00015D2B
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

		// Token: 0x060008B9 RID: 2233 RVA: 0x00017B57 File Offset: 0x00015D57
		private void WriteEdmxElement()
		{
			this.writer.WriteStartElement("edmx", "Edmx", this.edmxNamespace);
			this.writer.WriteAttributeString("Version", CsdlWriter.GetVersionString(this.edmxVersion));
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00017B8F File Offset: 0x00015D8F
		private void WriteRuntimeElement()
		{
			this.writer.WriteStartElement("edmx", "Runtime", this.edmxNamespace);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00017BAC File Offset: 0x00015DAC
		private void WriteConceptualModelsElement()
		{
			this.writer.WriteStartElement("edmx", "ConceptualModels", this.edmxNamespace);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00017BCC File Offset: 0x00015DCC
		private void WriteReferenceElements()
		{
			EdmModelReferenceElementsVisitor edmModelReferenceElementsVisitor = new EdmModelReferenceElementsVisitor(this.model, this.writer, this.edmxVersion);
			edmModelReferenceElementsVisitor.VisitEdmReferences(this.model);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00017BFD File Offset: 0x00015DFD
		private void WriteDataServicesElement()
		{
			this.writer.WriteStartElement("edmx", "DataServices", this.edmxNamespace);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00017C1C File Offset: 0x00015E1C
		private void WriteSchemas()
		{
			Version version = this.model.GetEdmVersion() ?? EdmConstants.EdmVersionDefault;
			foreach (EdmSchema edmSchema in this.schemas)
			{
				EdmModelCsdlSerializationVisitor edmModelCsdlSerializationVisitor = new EdmModelCsdlSerializationVisitor(this.model, this.writer, version);
				edmModelCsdlSerializationVisitor.VisitEdmSchema(edmSchema, this.model.GetNamespacePrefixMappings());
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00017C9C File Offset: 0x00015E9C
		private void EndElement()
		{
			this.writer.WriteEndElement();
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00017CA9 File Offset: 0x00015EA9
		private static string GetVersionString(Version version)
		{
			if (version == EdmConstants.EdmVersion401)
			{
				return "4.01";
			}
			return version.ToString();
		}

		// Token: 0x040004FB RID: 1275
		private readonly IEdmModel model;

		// Token: 0x040004FC RID: 1276
		private readonly IEnumerable<EdmSchema> schemas;

		// Token: 0x040004FD RID: 1277
		private readonly XmlWriter writer;

		// Token: 0x040004FE RID: 1278
		private readonly Version edmxVersion;

		// Token: 0x040004FF RID: 1279
		private readonly string edmxNamespace;

		// Token: 0x04000500 RID: 1280
		private readonly CsdlTarget target;
	}
}
