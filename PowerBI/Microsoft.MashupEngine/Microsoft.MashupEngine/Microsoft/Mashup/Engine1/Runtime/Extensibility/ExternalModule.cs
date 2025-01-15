using System;
using System.Resources;

namespace Microsoft.Mashup.Engine1.Runtime.Extensibility
{
	// Token: 0x0200170D RID: 5901
	public abstract class ExternalModule : Module
	{
		// Token: 0x1700273A RID: 10042
		// (get) Token: 0x06009602 RID: 38402
		public abstract ResourceManager DocumentationResources { get; }
	}
}
