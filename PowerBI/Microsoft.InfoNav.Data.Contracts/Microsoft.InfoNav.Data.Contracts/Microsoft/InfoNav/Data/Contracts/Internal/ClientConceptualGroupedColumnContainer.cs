using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000157 RID: 343
	[DataContract(Name = "ConceptualGroupedColumnContainer", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ClientConceptualGroupedColumnContainer
	{
		// Token: 0x060008C3 RID: 2243 RVA: 0x0001215B File Offset: 0x0001035B
		internal ClientConceptualGroupedColumnContainer()
		{
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00012163 File Offset: 0x00010363
		internal ClientConceptualGroupedColumnContainer(string column)
		{
			this._column = column;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00012172 File Offset: 0x00010372
		internal ClientConceptualGroupedColumnContainer(ClientConceptualHierarchyLevelRef hierarchyLevelRef)
		{
			this._hierarchyLevel = hierarchyLevelRef;
		}

		// Token: 0x0400044E RID: 1102
		[DataMember(Name = "ColumnRef", IsRequired = false, EmitDefaultValue = false, Order = 0)]
		private readonly string _column;

		// Token: 0x0400044F RID: 1103
		[DataMember(Name = "LevelRef", IsRequired = false, EmitDefaultValue = false, Order = 1)]
		private readonly ClientConceptualHierarchyLevelRef _hierarchyLevel;
	}
}
