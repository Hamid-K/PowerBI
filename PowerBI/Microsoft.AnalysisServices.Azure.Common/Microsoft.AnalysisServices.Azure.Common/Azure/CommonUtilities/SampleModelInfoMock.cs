using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Azure.Common;
using Microsoft.Cloud.Platform.Modularization;

namespace Microsoft.AnalysisServices.Azure.CommonUtilities
{
	// Token: 0x02000030 RID: 48
	[BlockServiceProvider(typeof(ISampleModelInfo))]
	public class SampleModelInfoMock : Block, ISampleModelInfo
	{
		// Token: 0x06000316 RID: 790 RVA: 0x0000E3AE File Offset: 0x0000C5AE
		public SampleModelInfoMock()
			: base("AnalyticsSampleModelInfoMock")
		{
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000E3BB File Offset: 0x0000C5BB
		public virtual IEnumerable<DatabaseMoniker> GetSampleModelMonikers()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000E3BB File Offset: 0x0000C5BB
		public virtual IEnumerable<string> GetSampleModelNames()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000E3BB File Offset: 0x0000C5BB
		public virtual IEnumerable<SampleModelProperty> GetSampleModelProperties()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000E3C2 File Offset: 0x0000C5C2
		public virtual bool IsSampleModel(string modelName)
		{
			return false;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000E3C2 File Offset: 0x0000C5C2
		public virtual bool IsSampleModel(DatabaseMoniker moniker)
		{
			return false;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000E3BB File Offset: 0x0000C5BB
		public virtual string GetModelName(string fileName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000E3BB File Offset: 0x0000C5BB
		public bool TryGetSampleModelName(string inputModelName, out string modelName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000E3BB File Offset: 0x0000C5BB
		public virtual bool IsSampleModelVirtualServer(string virtualServer)
		{
			throw new NotImplementedException();
		}
	}
}
