using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001F8 RID: 504
	internal class CsdlModel
	{
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x0002619A File Offset: 0x0002439A
		public IEnumerable<IEdmReference> CurrentModelReferences
		{
			get
			{
				return this.currentModelReferences;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x000261A2 File Offset: 0x000243A2
		public IEnumerable<IEdmReference> ParentModelReferences
		{
			get
			{
				return this.parentModelReferences;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x000261AA File Offset: 0x000243AA
		public IEnumerable<CsdlSchema> Schemata
		{
			get
			{
				return this.schemata;
			}
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x000261B2 File Offset: 0x000243B2
		public void AddSchema(CsdlSchema schema)
		{
			this.schemata.Add(schema);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x000261C0 File Offset: 0x000243C0
		public void AddCurrentModelReferences(IEnumerable<IEdmReference> referencesToAdd)
		{
			this.currentModelReferences.AddRange(referencesToAdd);
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x000261CE File Offset: 0x000243CE
		public void AddParentModelReferences(IEdmReference referenceToAdd)
		{
			this.parentModelReferences.Add(referenceToAdd);
		}

		// Token: 0x04000785 RID: 1925
		private readonly List<CsdlSchema> schemata = new List<CsdlSchema>();

		// Token: 0x04000786 RID: 1926
		private readonly List<IEdmReference> currentModelReferences = new List<IEdmReference>();

		// Token: 0x04000787 RID: 1927
		private readonly List<IEdmReference> parentModelReferences = new List<IEdmReference>();
	}
}
