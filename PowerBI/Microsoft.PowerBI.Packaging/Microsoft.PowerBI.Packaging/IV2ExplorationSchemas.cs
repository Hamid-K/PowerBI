using System;
using Newtonsoft.Json.Schema;

namespace Microsoft.PowerBI.Packaging
{
	// Token: 0x0200001F RID: 31
	public interface IV2ExplorationSchemas
	{
		// Token: 0x060000BF RID: 191
		JSchema GetSchema(string schemaKey, Version schemaVersion);
	}
}
