using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Csdl.Serialization;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x020000D3 RID: 211
	public class EdmxWriter
	{
		// Token: 0x060003C1 RID: 961 RVA: 0x00009595 File Offset: 0x00007795
		private EdmxWriter(IEdmModel model, IEnumerable<EdmSchema> schemas, XmlWriter writer, Version edmxVersion, EdmxTarget target)
		{
			this.model = model;
			this.schemas = schemas;
			this.writer = writer;
			this.edmxVersion = edmxVersion;
			this.target = target;
			this.edmxNamespace = CsdlConstants.SupportedEdmxVersions[edmxVersion];
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000095D4 File Offset: 0x000077D4
		public static bool TryWriteEdmx(IEdmModel model, XmlWriter writer, EdmxTarget target, out IEnumerable<EdmError> errors)
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
			EdmxWriter edmxWriter = new EdmxWriter(model, enumerable, writer, version, target);
			edmxWriter.WriteEdmx();
			errors = Enumerable.Empty<EdmError>();
			return true;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000096B8 File Offset: 0x000078B8
		private void WriteEdmx()
		{
			switch (this.target)
			{
			case EdmxTarget.EntityFramework:
				this.WriteEFEdmx();
				return;
			case EdmxTarget.OData:
				this.WriteODataEdmx();
				return;
			default:
				throw new InvalidOperationException(Strings.UnknownEnumVal_EdmxTarget(this.target.ToString()));
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00009704 File Offset: 0x00007904
		private void WriteODataEdmx()
		{
			this.WriteEdmxElement();
			this.WriteReferenceElements();
			this.WriteDataServicesElement();
			this.WriteSchemas();
			this.EndElement();
			this.EndElement();
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000972A File Offset: 0x0000792A
		private void WriteEFEdmx()
		{
			this.WriteEdmxElement();
			this.WriteRuntimeElement();
			this.WriteConceptualModelsElement();
			this.WriteSchemas();
			this.EndElement();
			this.EndElement();
			this.EndElement();
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00009756 File Offset: 0x00007956
		private void WriteEdmxElement()
		{
			this.writer.WriteStartElement("edmx", "Edmx", this.edmxNamespace);
			this.writer.WriteAttributeString("Version", this.edmxVersion.ToString());
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000978E File Offset: 0x0000798E
		private void WriteRuntimeElement()
		{
			this.writer.WriteStartElement("edmx", "Runtime", this.edmxNamespace);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000097AB File Offset: 0x000079AB
		private void WriteConceptualModelsElement()
		{
			this.writer.WriteStartElement("edmx", "ConceptualModels", this.edmxNamespace);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000097C8 File Offset: 0x000079C8
		private void WriteReferenceElements()
		{
			EdmModelReferenceElementsVisitor edmModelReferenceElementsVisitor = new EdmModelReferenceElementsVisitor(this.model, this.writer, this.edmxVersion);
			edmModelReferenceElementsVisitor.VisitEdmReferences(this.model);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000097F9 File Offset: 0x000079F9
		private void WriteDataServicesElement()
		{
			this.writer.WriteStartElement("edmx", "DataServices", this.edmxNamespace);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00009818 File Offset: 0x00007A18
		private void WriteSchemas()
		{
			Version version = this.model.GetEdmVersion() ?? EdmConstants.EdmVersionLatest;
			foreach (EdmSchema edmSchema in this.schemas)
			{
				EdmModelCsdlSerializationVisitor edmModelCsdlSerializationVisitor = new EdmModelCsdlSerializationVisitor(this.model, this.writer, version);
				edmModelCsdlSerializationVisitor.VisitEdmSchema(edmSchema, this.model.GetNamespacePrefixMappings());
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00009898 File Offset: 0x00007A98
		private void EndElement()
		{
			this.writer.WriteEndElement();
		}

		// Token: 0x0400019B RID: 411
		private readonly IEdmModel model;

		// Token: 0x0400019C RID: 412
		private readonly IEnumerable<EdmSchema> schemas;

		// Token: 0x0400019D RID: 413
		private readonly XmlWriter writer;

		// Token: 0x0400019E RID: 414
		private readonly Version edmxVersion;

		// Token: 0x0400019F RID: 415
		private readonly string edmxNamespace;

		// Token: 0x040001A0 RID: 416
		private readonly EdmxTarget target;
	}
}
