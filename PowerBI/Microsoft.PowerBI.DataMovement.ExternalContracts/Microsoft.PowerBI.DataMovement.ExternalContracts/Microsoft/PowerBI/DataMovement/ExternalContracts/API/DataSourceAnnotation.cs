using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000037 RID: 55
	[DataContract]
	public abstract class DataSourceAnnotation
	{
		// Token: 0x06000140 RID: 320 RVA: 0x0000337C File Offset: 0x0000157C
		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
