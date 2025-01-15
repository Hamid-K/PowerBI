using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common.DataContracts
{
	// Token: 0x02000157 RID: 343
	[DataContract]
	public sealed class ModelConnectionStrings
	{
		// Token: 0x060011F9 RID: 4601 RVA: 0x0004959A File Offset: 0x0004779A
		public ModelConnectionStrings(IEnumerable<ASConnectionInfo> connections, AdvancedModelFeatures advancedModelFeatures)
		{
			this.Connections = connections;
			this.AdvancedModelFeatures = advancedModelFeatures;
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x000495B0 File Offset: 0x000477B0
		// (set) Token: 0x060011FB RID: 4603 RVA: 0x000495B8 File Offset: 0x000477B8
		[DataMember]
		public IEnumerable<ASConnectionInfo> Connections { get; private set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x000495C1 File Offset: 0x000477C1
		// (set) Token: 0x060011FD RID: 4605 RVA: 0x000495C9 File Offset: 0x000477C9
		[DataMember]
		public AdvancedModelFeatures AdvancedModelFeatures { get; private set; }
	}
}
