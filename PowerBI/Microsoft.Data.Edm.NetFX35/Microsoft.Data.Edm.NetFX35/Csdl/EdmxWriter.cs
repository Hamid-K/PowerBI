using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.Data.Edm.Csdl.Internal.Serialization;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Csdl
{
	// Token: 0x020000B0 RID: 176
	public class EdmxWriter
	{
		// Token: 0x06000322 RID: 802 RVA: 0x0000845B File Offset: 0x0000665B
		private EdmxWriter(IEdmModel model, IEnumerable<EdmSchema> schemas, XmlWriter writer, Version edmxVersion, EdmxTarget target)
		{
			this.model = model;
			this.schemas = schemas;
			this.writer = writer;
			this.edmxVersion = edmxVersion;
			this.target = target;
			this.edmxNamespace = CsdlConstants.SupportedEdmxVersions[edmxVersion];
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000849C File Offset: 0x0000669C
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

		// Token: 0x06000324 RID: 804 RVA: 0x00008580 File Offset: 0x00006780
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

		// Token: 0x06000325 RID: 805 RVA: 0x000085CC File Offset: 0x000067CC
		private void WriteODataEdmx()
		{
			this.WriteEdmxElement();
			this.WriteDataServicesElement();
			this.WriteSchemas();
			this.EndElement();
			this.EndElement();
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000085EC File Offset: 0x000067EC
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

		// Token: 0x06000327 RID: 807 RVA: 0x00008618 File Offset: 0x00006818
		private void WriteEdmxElement()
		{
			this.writer.WriteStartElement("edmx", "Edmx", this.edmxNamespace);
			this.writer.WriteAttributeString("Version", this.edmxVersion.ToString());
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00008650 File Offset: 0x00006850
		private void WriteRuntimeElement()
		{
			this.writer.WriteStartElement("edmx", "Runtime", this.edmxNamespace);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000866D File Offset: 0x0000686D
		private void WriteConceptualModelsElement()
		{
			this.writer.WriteStartElement("edmx", "ConceptualModels", this.edmxNamespace);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000868C File Offset: 0x0000688C
		private void WriteDataServicesElement()
		{
			this.writer.WriteStartElement("edmx", "DataServices", this.edmxNamespace);
			Version dataServiceVersion = this.model.GetDataServiceVersion();
			if (dataServiceVersion != null)
			{
				this.writer.WriteAttributeString("m", "DataServiceVersion", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", dataServiceVersion.ToString());
			}
			Version maxDataServiceVersion = this.model.GetMaxDataServiceVersion();
			if (maxDataServiceVersion != null)
			{
				this.writer.WriteAttributeString("m", "MaxDataServiceVersion", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", maxDataServiceVersion.ToString());
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00008720 File Offset: 0x00006920
		private void WriteSchemas()
		{
			Version version = this.model.GetEdmVersion() ?? EdmConstants.EdmVersionLatest;
			foreach (EdmSchema edmSchema in this.schemas)
			{
				EdmModelCsdlSerializationVisitor edmModelCsdlSerializationVisitor = new EdmModelCsdlSerializationVisitor(this.model, this.writer, version);
				edmModelCsdlSerializationVisitor.VisitEdmSchema(edmSchema, this.model.GetNamespacePrefixMappings());
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000087A0 File Offset: 0x000069A0
		private void EndElement()
		{
			this.writer.WriteEndElement();
		}

		// Token: 0x04000163 RID: 355
		private readonly IEdmModel model;

		// Token: 0x04000164 RID: 356
		private readonly IEnumerable<EdmSchema> schemas;

		// Token: 0x04000165 RID: 357
		private readonly XmlWriter writer;

		// Token: 0x04000166 RID: 358
		private readonly Version edmxVersion;

		// Token: 0x04000167 RID: 359
		private readonly string edmxNamespace;

		// Token: 0x04000168 RID: 360
		private readonly EdmxTarget target;
	}
}
