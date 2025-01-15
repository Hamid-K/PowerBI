using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.Session
{
	// Token: 0x0200010D RID: 269
	public interface IJournalStorage : IDictionary<string, string>, ICollection<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>, IEnumerable, IReadOnlyDictionary<string, string>, IReadOnlyCollection<KeyValuePair<string, string>>
	{
	}
}
