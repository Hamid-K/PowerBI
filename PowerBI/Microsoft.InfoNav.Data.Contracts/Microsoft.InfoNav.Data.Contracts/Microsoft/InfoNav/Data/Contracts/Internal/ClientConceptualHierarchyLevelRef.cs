using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200015C RID: 348
	[DataContract(Name = "LevelRef", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ClientConceptualHierarchyLevelRef
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x000122A9 File Offset: 0x000104A9
		internal ClientConceptualHierarchyLevelRef()
		{
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x000122B1 File Offset: 0x000104B1
		internal ClientConceptualHierarchyLevelRef(string hierarchyLevel, string hierarchy)
		{
			this._hierarchyLevel = hierarchyLevel;
			this._hierarchy = hierarchy;
		}

		// Token: 0x0400045E RID: 1118
		[DataMember(Name = "Level", IsRequired = true, Order = 0)]
		private string _hierarchyLevel;

		// Token: 0x0400045F RID: 1119
		[DataMember(Name = "Hierarchy", IsRequired = true, Order = 1)]
		private string _hierarchy;
	}
}
