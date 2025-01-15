using System;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001DF RID: 479
	internal interface ILsdlSerializerSettings
	{
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000A7B RID: 2683
		bool CanonicalBindings { get; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000A7C RID: 2684
		bool CanonicalForm { get; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000A7D RID: 2685
		IConceptualSchema ConceptualSchema { get; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000A7E RID: 2686
		bool OmitVersion { get; }
	}
}
