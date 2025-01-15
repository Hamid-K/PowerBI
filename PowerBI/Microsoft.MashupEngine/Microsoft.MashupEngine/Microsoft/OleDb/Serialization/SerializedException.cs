using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Data.Serialization;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FCA RID: 8138
	public class SerializedException : Dictionary<string, string>, ISerializedException, IDictionary<string, string>, ICollection<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>, IEnumerable
	{
		// Token: 0x0600C6DF RID: 50911 RVA: 0x0027A051 File Offset: 0x00278251
		public SerializedException()
		{
		}

		// Token: 0x0600C6E0 RID: 50912 RVA: 0x0027A059 File Offset: 0x00278259
		public SerializedException(int count)
			: base(count)
		{
		}
	}
}
