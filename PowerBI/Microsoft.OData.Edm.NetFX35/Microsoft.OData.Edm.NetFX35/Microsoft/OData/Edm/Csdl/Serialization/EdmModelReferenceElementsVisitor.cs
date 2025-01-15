using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.OData.Edm.Csdl.Serialization
{
	// Token: 0x020000CD RID: 205
	internal class EdmModelReferenceElementsVisitor
	{
		// Token: 0x06000384 RID: 900 RVA: 0x00008105 File Offset: 0x00006305
		internal EdmModelReferenceElementsVisitor(IEdmModel model, XmlWriter xmlWriter, Version edmxVersion)
		{
			this.schemaWriter = new EdmModelCsdlSchemaWriter(model, model.GetNamespaceAliases(), xmlWriter, edmxVersion);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00008124 File Offset: 0x00006324
		internal void VisitEdmReferences(IEdmModel model)
		{
			IEnumerable<IEdmReference> edmReferences = model.GetEdmReferences();
			if (model != null && edmReferences != null)
			{
				foreach (IEdmReference edmReference in edmReferences)
				{
					this.schemaWriter.WriteReferenceElementHeader(edmReference);
					if (edmReference.Includes != null)
					{
						foreach (IEdmInclude edmInclude in edmReference.Includes)
						{
							this.schemaWriter.WriteIncludeElement(edmInclude);
						}
					}
					if (edmReference.IncludeAnnotations != null)
					{
						foreach (IEdmIncludeAnnotations edmIncludeAnnotations in edmReference.IncludeAnnotations)
						{
							this.schemaWriter.WriteIncludeAnnotationsElement(edmIncludeAnnotations);
						}
					}
					this.schemaWriter.WriteEndElement();
				}
			}
		}

		// Token: 0x0400017B RID: 379
		private readonly EdmModelCsdlSchemaWriter schemaWriter;
	}
}
