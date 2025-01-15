using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000087 RID: 135
	public interface IOAuthFactory
	{
		// Token: 0x060001F5 RID: 501
		object CreateProvider(IEngineHost engineHost, IEngine engine, object clientApplication, string resourceUrl);

		// Token: 0x060001F6 RID: 502
		object CreateResource(IEngineHost engineHost, IEngine engine, string resourceUrl);
	}
}
