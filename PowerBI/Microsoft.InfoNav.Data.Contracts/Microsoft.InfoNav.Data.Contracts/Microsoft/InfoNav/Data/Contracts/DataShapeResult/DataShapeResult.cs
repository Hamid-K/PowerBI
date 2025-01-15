using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x0200010C RID: 268
	[DataContract]
	[JsonConverter(typeof(DataShapeResultJsonConverter))]
	public sealed class DataShapeResult
	{
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0000EF7E File Offset: 0x0000D17E
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x0000EF86 File Offset: 0x0000D186
		[DataMember(Name = "DataShapes", IsRequired = true, Order = 0)]
		public IList<DataShape> DataShapes { get; set; }
	}
}
