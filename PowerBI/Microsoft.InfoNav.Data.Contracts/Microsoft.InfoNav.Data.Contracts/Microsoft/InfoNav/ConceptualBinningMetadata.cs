using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x02000013 RID: 19
	[ImmutableObject(true)]
	internal sealed class ConceptualBinningMetadata
	{
		// Token: 0x0600002B RID: 43 RVA: 0x0000228A File Offset: 0x0000048A
		internal ConceptualBinningMetadata(ConceptualBinSize binSize)
		{
			this._binSize = binSize;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002299 File Offset: 0x00000499
		internal ConceptualBinSize BinSize
		{
			get
			{
				return this._binSize;
			}
		}

		// Token: 0x04000041 RID: 65
		private readonly ConceptualBinSize _binSize;
	}
}
