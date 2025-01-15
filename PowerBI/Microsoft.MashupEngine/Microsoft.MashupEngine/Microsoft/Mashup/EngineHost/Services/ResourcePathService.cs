using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B31 RID: 6961
	public sealed class ResourcePathService : IResourcePathService
	{
		// Token: 0x0600AE4C RID: 44620 RVA: 0x0023B094 File Offset: 0x00239294
		public bool StartsWith(IResource permittedResource, IResource attemptedResource)
		{
			return ResourcePath.StartsWith(permittedResource, attemptedResource);
		}
	}
}
