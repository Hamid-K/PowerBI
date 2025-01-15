using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DF2 RID: 7666
	public interface IResourcePathService
	{
		// Token: 0x0600BDA7 RID: 48551
		bool StartsWith(IResource permittedResource, IResource attemptedResource);
	}
}
