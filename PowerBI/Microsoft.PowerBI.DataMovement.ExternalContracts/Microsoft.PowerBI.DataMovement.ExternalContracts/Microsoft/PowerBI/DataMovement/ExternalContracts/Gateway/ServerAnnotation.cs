using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.Gateway
{
	// Token: 0x0200001B RID: 27
	[DataContract]
	public abstract class ServerAnnotation
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00002C5C File Offset: 0x00000E5C
		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
