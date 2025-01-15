using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.LoadBalancer
{
	// Token: 0x02000011 RID: 17
	[DataContract]
	public abstract class LoadBalancingSettings
	{
		// Token: 0x06000052 RID: 82 RVA: 0x000027EF File Offset: 0x000009EF
		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
