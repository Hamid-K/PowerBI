using System;
using System.Globalization;
using Microsoft.ProgramSynthesis.Wrangling.Session;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json.TableToJson
{
	// Token: 0x02001A79 RID: 6777
	public class TableToJsonSession : NonInteractiveSession<TableToJsonProgram, Table, JToken>
	{
		// Token: 0x0600DF1E RID: 57118 RVA: 0x002F5ADA File Offset: 0x002F3CDA
		public TableToJsonSession(IJournalStorage journalStorage = null, CultureInfo culture = null)
			: base(TableToJsonLearner.Instance, TableToJsonLoader.Instance, journalStorage, culture, null, null, true)
		{
		}
	}
}
