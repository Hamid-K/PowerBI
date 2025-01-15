using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200004B RID: 75
	public interface IConnectionStringService
	{
		// Token: 0x06000146 RID: 326
		bool ValidateSourceConnectionString(string connectionString, out string errorMessage);

		// Token: 0x06000147 RID: 327
		bool TryBuildConnectionString(Dictionary<string, string> keyValuePairs, out string connectionString);

		// Token: 0x06000148 RID: 328
		bool TryParseConnectionString(string connectionString, out Dictionary<string, string> keyValuePairs);
	}
}
