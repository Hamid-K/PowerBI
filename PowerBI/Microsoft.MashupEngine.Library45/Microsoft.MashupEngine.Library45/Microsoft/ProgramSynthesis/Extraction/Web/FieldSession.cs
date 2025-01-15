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
	// Token: 0x02000FB6 RID: 4022
	public class FieldSession : NonInteractiveSession<FieldProgram, WebRegion, string[]>
	{
		// Token: 0x06006F0F RID: 28431 RVA: 0x0016B424 File Offset: 0x00169624
		public FieldSession(IJournalStorage journalStorage = null, CultureInfo culture = null, ILogger logger = null, IEnumerable<EntityDetector> entityDetectorObjects = null)
			: base(FieldProgramLearner.Instance, FieldProgramLoader.Instance, journalStorage, culture, "Extraction.Web.Field", logger, true)
		{
			EntityDetectorsMap entityDetectorsMap = new EntityDetectorsMap(entityDetectorObjects);
			this.DeserializationContext = DeserializationContext.Create<EntityDetectorsMap>(entityDetectorsMap);
			base.Constraints.Add(new EntityDetectorsMapConstraint<WebRegion, string[]>(entityDetectorsMap));
		}

		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x06006F10 RID: 28432 RVA: 0x0016B46F File Offset: 0x0016966F
		public override DeserializationContext DeserializationContext { get; }
	}
}
