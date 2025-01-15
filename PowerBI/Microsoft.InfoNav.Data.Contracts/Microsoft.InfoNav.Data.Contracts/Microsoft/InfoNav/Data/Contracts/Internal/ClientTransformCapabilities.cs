using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000168 RID: 360
	[DataContract(Name = "TransformCapabilities", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public class ClientTransformCapabilities
	{
		// Token: 0x0600092A RID: 2346 RVA: 0x0001333D File Offset: 0x0001153D
		public ClientTransformCapabilities(List<string> supportedTransforms)
		{
			this.SupportedTransforms = supportedTransforms;
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0001334C File Offset: 0x0001154C
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x00013354 File Offset: 0x00011554
		[DataMember(Name = "SupportedTransforms", IsRequired = false, EmitDefaultValue = false, Order = 0)]
		public List<string> SupportedTransforms { get; private set; }
	}
}
