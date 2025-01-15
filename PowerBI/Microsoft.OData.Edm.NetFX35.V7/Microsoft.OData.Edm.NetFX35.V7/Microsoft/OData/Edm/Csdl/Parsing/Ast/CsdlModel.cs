using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001EB RID: 491
	internal class CsdlModel
	{
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x00024057 File Offset: 0x00022257
		public IEnumerable<IEdmReference> CurrentModelReferences
		{
			get
			{
				return this.currentModelReferences;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x0002405F File Offset: 0x0002225F
		public IEnumerable<IEdmReference> ParentModelReferences
		{
			get
			{
				return this.parentModelReferences;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x00024067 File Offset: 0x00022267
		public IEnumerable<CsdlSchema> Schemata
		{
			get
			{
				return this.schemata;
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0002406F File Offset: 0x0002226F
		public void AddSchema(CsdlSchema schema)
		{
			this.schemata.Add(schema);
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0002407D File Offset: 0x0002227D
		public void AddCurrentModelReferences(IEnumerable<IEdmReference> referencesToAdd)
		{
			this.currentModelReferences.AddRange(referencesToAdd);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0002408B File Offset: 0x0002228B
		public void AddParentModelReferences(IEdmReference referenceToAdd)
		{
			this.parentModelReferences.Add(referenceToAdd);
		}

		// Token: 0x0400070F RID: 1807
		private readonly List<CsdlSchema> schemata = new List<CsdlSchema>();

		// Token: 0x04000710 RID: 1808
		private readonly List<IEdmReference> currentModelReferences = new List<IEdmReference>();

		// Token: 0x04000711 RID: 1809
		private readonly List<IEdmReference> parentModelReferences = new List<IEdmReference>();
	}
}
