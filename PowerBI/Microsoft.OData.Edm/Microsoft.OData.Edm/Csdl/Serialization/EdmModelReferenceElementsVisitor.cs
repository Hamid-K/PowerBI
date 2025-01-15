using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.OData.Edm.Csdl.Serialization
{
	// Token: 0x02000159 RID: 345
	internal class EdmModelReferenceElementsVisitor
	{
		// Token: 0x060008CA RID: 2250 RVA: 0x00017F94 File Offset: 0x00016194
		internal EdmModelReferenceElementsVisitor(IEdmModel model, XmlWriter xmlWriter, Version edmxVersion)
		{
			this.schemaWriter = new EdmModelCsdlSchemaWriter(model, model.GetNamespaceAliases(), xmlWriter, edmxVersion);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00017FB0 File Offset: 0x000161B0
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

		// Token: 0x040005B7 RID: 1463
		private readonly EdmModelCsdlSchemaWriter schemaWriter;
	}
}
