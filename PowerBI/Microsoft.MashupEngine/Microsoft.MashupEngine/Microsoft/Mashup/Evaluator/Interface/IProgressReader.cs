using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DF5 RID: 7669
	public interface IProgressReader
	{
		// Token: 0x0600BDAA RID: 48554
		IEnumerable<byte[]> ReadAllProgress();
	}
}
