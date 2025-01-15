using System;
using Microsoft.TMSN.TMSNlearn;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020003E5 RID: 997
	public enum WhiteningKind
	{
		// Token: 0x04000CD3 RID: 3283
		[TGUI(Label = "PCA whitening")]
		Pca,
		// Token: 0x04000CD4 RID: 3284
		[TGUI(Label = "ZCA whitening")]
		Zca
	}
}
