using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.OData.Edm.Csdl.Serialization
{
	// Token: 0x0200014D RID: 333
	internal class EdmModelReferenceElementsVisitor
	{
		// Token: 0x06000829 RID: 2089 RVA: 0x0001627C File Offset: 0x0001447C
		internal EdmModelReferenceElementsVisitor(IEdmModel model, XmlWriter xmlWriter, Version edmxVersion)
		{
			this.schemaWriter = new EdmModelCsdlSchemaWriter(model, model.GetNamespaceAliases(), xmlWriter, edmxVersion);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00016298 File Offset: 0x00014498
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

		// Token: 0x0400054D RID: 1357
		private readonly EdmModelCsdlSchemaWriter schemaWriter;
	}
}
