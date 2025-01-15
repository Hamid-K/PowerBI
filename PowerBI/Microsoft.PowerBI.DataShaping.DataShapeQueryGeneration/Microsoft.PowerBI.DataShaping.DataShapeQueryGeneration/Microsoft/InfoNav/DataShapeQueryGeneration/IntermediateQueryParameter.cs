using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000B9 RID: 185
	internal readonly struct IntermediateQueryParameter
	{
		// Token: 0x060006B8 RID: 1720 RVA: 0x0001936A File Offset: 0x0001756A
		internal IntermediateQueryParameter(string name, ConceptualResultType type)
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0001937A File Offset: 0x0001757A
		internal string Name { get; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00019382 File Offset: 0x00017582
		internal ConceptualResultType Type { get; }
	}
}
