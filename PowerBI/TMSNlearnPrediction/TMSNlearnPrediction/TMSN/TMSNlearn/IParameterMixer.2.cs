using System;
using System.Collections.Generic;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004B3 RID: 1203
	public interface IParameterMixer<TOutput>
	{
		// Token: 0x060018DD RID: 6365
		IParameterMixer<TOutput> CombineParameters(IList<IParameterMixer<TOutput>> models);
	}
}
