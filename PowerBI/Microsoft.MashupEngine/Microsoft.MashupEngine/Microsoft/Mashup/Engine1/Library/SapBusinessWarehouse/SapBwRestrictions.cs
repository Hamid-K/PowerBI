using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004DB RID: 1243
	public class SapBwRestrictions : List<KeyValuePair<string, object>>
	{
		// Token: 0x06002897 RID: 10391 RVA: 0x00079467 File Offset: 0x00077667
		public void Add(string key, object value)
		{
			base.Add(new KeyValuePair<string, object>(key, value));
		}
	}
}
