using System;
using System.Collections.Generic;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004B2 RID: 1202
	public interface IParameterMixer
	{
		// Token: 0x060018DC RID: 6364
		IParameterMixer CombineParameters(IList<IParameterMixer> models);
	}
}
