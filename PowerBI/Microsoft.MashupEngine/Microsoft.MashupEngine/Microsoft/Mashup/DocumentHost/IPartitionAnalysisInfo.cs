using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.DocumentHost
{
	// Token: 0x02001943 RID: 6467
	public interface IPartitionAnalysisInfo
	{
		// Token: 0x0600A427 RID: 42023
		void SetStart();

		// Token: 0x0600A428 RID: 42024
		void SetPreviewValue(EvaluationResult2<IPreviewValueSource> result, Func<DateTime> getStaleSince, Func<bool> getSampled);

		// Token: 0x0600A429 RID: 42025
		void SetResources(IEnumerable<IResource> resources);

		// Token: 0x0600A42A RID: 42026
		void SetResourcesAccessed(IEnumerable<IResource> resources);

		// Token: 0x0600A42B RID: 42027
		void SetCultureAccessed();

		// Token: 0x0600A42C RID: 42028
		void SetComplete();

		// Token: 0x0600A42D RID: 42029
		bool TryGetPreviewValue(out EvaluationResult2<IPreviewValueSource> result, out DateTime staleSince, out bool sampled);

		// Token: 0x0600A42E RID: 42030
		bool TryGetResources(out IEnumerable<IResource> resources);

		// Token: 0x0600A42F RID: 42031
		bool TryGetResourcesAccessed(out IEnumerable<IResource> resources);

		// Token: 0x0600A430 RID: 42032
		bool TryGetComplete();
	}
}
