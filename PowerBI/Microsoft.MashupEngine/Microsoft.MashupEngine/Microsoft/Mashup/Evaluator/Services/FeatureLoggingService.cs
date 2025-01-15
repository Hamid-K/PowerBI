using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001D82 RID: 7554
	public sealed class FeatureLoggingService : IFeatureLoggingService
	{
		// Token: 0x0600BBAF RID: 48047 RVA: 0x0025FA7C File Offset: 0x0025DC7C
		public FeatureLoggingService()
		{
			this.features = new HashSet<string>();
		}

		// Token: 0x0600BBB0 RID: 48048 RVA: 0x0025FA90 File Offset: 0x0025DC90
		public void LogFeature(string feature)
		{
			HashSet<string> hashSet = this.features;
			lock (hashSet)
			{
				this.features.Add(feature);
			}
		}

		// Token: 0x0600BBB1 RID: 48049 RVA: 0x0025FAD8 File Offset: 0x0025DCD8
		public IEnumerable<string> GetLoggedFeatures()
		{
			HashSet<string> hashSet = this.features;
			IEnumerable<string> enumerable;
			lock (hashSet)
			{
				enumerable = this.features.ToArray<string>();
			}
			return enumerable;
		}

		// Token: 0x04005F79 RID: 24441
		private readonly HashSet<string> features;
	}
}
