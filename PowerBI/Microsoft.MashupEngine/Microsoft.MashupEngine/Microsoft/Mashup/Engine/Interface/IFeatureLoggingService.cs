using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200006C RID: 108
	public interface IFeatureLoggingService
	{
		// Token: 0x060001B1 RID: 433
		void LogFeature(string feature);

		// Token: 0x060001B2 RID: 434
		IEnumerable<string> GetLoggedFeatures();
	}
}
