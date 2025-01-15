using System;
using System.Globalization;
using Microsoft.ProgramSynthesis.Wrangling.Session;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json
{
	// Token: 0x020019FB RID: 6651
	public class Session : NonInteractiveSession<Program, JToken, JToken>
	{
		// Token: 0x0600D8AD RID: 55469 RVA: 0x002DF61A File Offset: 0x002DD81A
		public Session(IJournalStorage journalStorage = null, CultureInfo culture = null)
			: base(Learner.Instance, Loader.Instance, journalStorage, culture, null, null, true)
		{
		}
	}
}
