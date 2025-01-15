using System;
using Newtonsoft.Json.Linq;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x02000193 RID: 403
	internal interface ILsdlDocumentDowngradeTransform
	{
		// Token: 0x0600082F RID: 2095
		void Downgrade(JObject schema);
	}
}
