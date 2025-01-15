using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ProgramSynthesis.Wrangling.Logging;
using Microsoft.ProgramSynthesis.Wrangling.Session;

namespace Microsoft.ProgramSynthesis.Conditionals
{
	// Token: 0x02000A14 RID: 2580
	public class Session : NonInteractiveSession<Program, IEnumerable<string>, IEnumerable<IEnumerable<string>>>
	{
		// Token: 0x06003E23 RID: 15907 RVA: 0x000C17AB File Offset: 0x000BF9AB
		public Session(IJournalStorage journalStorage = null, CultureInfo culture = null, ILogger logger = null)
			: base(Learner.Instance, Loader.Instance, journalStorage, culture, "Conditionals", logger, true)
		{
		}
	}
}
