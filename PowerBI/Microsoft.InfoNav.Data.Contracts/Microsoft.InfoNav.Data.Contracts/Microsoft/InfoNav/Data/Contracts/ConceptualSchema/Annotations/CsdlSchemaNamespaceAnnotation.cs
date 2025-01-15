using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x0200012A RID: 298
	public sealed class CsdlSchemaNamespaceAnnotation
	{
		// Token: 0x060007C4 RID: 1988 RVA: 0x00010244 File Offset: 0x0000E444
		public CsdlSchemaNamespaceAnnotation(string namespaceName)
		{
			this.NamespaceName = namespaceName;
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00010253 File Offset: 0x0000E453
		public string NamespaceName { get; }
	}
}
