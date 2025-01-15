using System;
using System.Reflection;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000364 RID: 868
	public interface IParameterFactory
	{
		// Token: 0x060019F1 RID: 6641
		bool TryCreateParameterMetadata(ParameterInfo methodParameter, out ParameterMetadata parameterMetadata);

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060019F2 RID: 6642
		string ValidTypesAsString { get; }
	}
}
