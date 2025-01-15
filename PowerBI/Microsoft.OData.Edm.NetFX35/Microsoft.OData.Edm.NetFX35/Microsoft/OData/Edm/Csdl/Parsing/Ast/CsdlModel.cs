using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x02000185 RID: 389
	internal class CsdlModel
	{
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001193F File Offset: 0x0000FB3F
		public IEnumerable<IEdmReference> CurrentModelReferences
		{
			get
			{
				return this.currentModelReferences;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x00011947 File Offset: 0x0000FB47
		public IEnumerable<IEdmReference> ParentModelReferences
		{
			get
			{
				return this.parentModelReferences;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0001194F File Offset: 0x0000FB4F
		public IEnumerable<CsdlSchema> Schemata
		{
			get
			{
				return this.schemata;
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00011957 File Offset: 0x0000FB57
		public void AddSchema(CsdlSchema schema)
		{
			this.schemata.Add(schema);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00011965 File Offset: 0x0000FB65
		public void AddCurrentModelReferences(IEnumerable<IEdmReference> referencesToAdd)
		{
			this.currentModelReferences.AddRange(referencesToAdd);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00011973 File Offset: 0x0000FB73
		public void AddParentModelReferences(IEdmReference referenceToAdd)
		{
			this.parentModelReferences.Add(referenceToAdd);
		}

		// Token: 0x040003C7 RID: 967
		private readonly List<CsdlSchema> schemata = new List<CsdlSchema>();

		// Token: 0x040003C8 RID: 968
		private readonly List<IEdmReference> currentModelReferences = new List<IEdmReference>();

		// Token: 0x040003C9 RID: 969
		private readonly List<IEdmReference> parentModelReferences = new List<IEdmReference>();
	}
}
