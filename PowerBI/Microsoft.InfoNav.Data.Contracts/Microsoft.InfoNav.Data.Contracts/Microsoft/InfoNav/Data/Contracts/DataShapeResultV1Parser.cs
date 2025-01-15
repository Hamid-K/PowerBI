using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.DataShapeResult;
using Newtonsoft.Json;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x02000084 RID: 132
	internal sealed class DataShapeResultV1Parser : DataShapeResultParserBase
	{
		// Token: 0x0600030B RID: 779 RVA: 0x000086F1 File Offset: 0x000068F1
		private DataShapeResultV1Parser()
			: base(DsrNames.V1)
		{
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000086FE File Offset: 0x000068FE
		protected override IList<DataShape> ParseDataShapes(JsonReader reader, JsonSerializer serializer)
		{
			return serializer.Deserialize<List<DataShape>>(reader);
		}

		// Token: 0x040001B3 RID: 435
		internal static readonly DataShapeResultV1Parser Instance = new DataShapeResultV1Parser();
	}
}
