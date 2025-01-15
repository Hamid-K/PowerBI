using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Features.InputTransformation
{
	// Token: 0x020007E3 RID: 2019
	public interface IInputTransformer : IEquatable<IInputTransformer>
	{
		// Token: 0x06002B08 RID: 11016
		IEnumerable<State> Transform(IEnumerable<State> inputs);
	}
}
