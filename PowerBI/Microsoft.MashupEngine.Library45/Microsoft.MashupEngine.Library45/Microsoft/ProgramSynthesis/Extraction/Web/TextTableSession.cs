using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary.EntityDetectors;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Logging;
using Microsoft.ProgramSynthesis.Wrangling.Session;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FDD RID: 4061
	public class TextTableSession : NonInteractiveSession<TextTableProgram, WebRegion, IEnumerable<IEnumerable<string>>>
	{
		// Token: 0x0600700A RID: 28682 RVA: 0x0016E094 File Offset: 0x0016C294
		public TextTableSession(IJournalStorage journalStorage = null, CultureInfo culture = null, ILogger logger = null, IEnumerable<EntityDetector> entityDetectorObjects = null)
			: base(TextTableProgramLearner.Instance, TextTableProgramLoader.Instance, journalStorage, culture, "Extraction.Web.TextTable", logger, true)
		{
			EntityDetectorsMap entityDetectorsMap = new EntityDetectorsMap(entityDetectorObjects);
			this.DeserializationContext = DeserializationContext.Create<EntityDetectorsMap>(entityDetectorsMap);
			base.Constraints.Add(new EntityDetectorsMapConstraint<WebRegion, IEnumerable<IEnumerable<string>>>(entityDetectorsMap));
		}

		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x0600700B RID: 28683 RVA: 0x0016E0DF File Offset: 0x0016C2DF
		public override DeserializationContext DeserializationContext { get; }
	}
}
