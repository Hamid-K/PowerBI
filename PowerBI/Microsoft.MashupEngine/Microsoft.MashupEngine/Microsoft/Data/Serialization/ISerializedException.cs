using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Data.Serialization
{
	// Token: 0x02000155 RID: 341
	public interface ISerializedException : IDictionary<string, string>, ICollection<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>, IEnumerable
	{
	}
}
