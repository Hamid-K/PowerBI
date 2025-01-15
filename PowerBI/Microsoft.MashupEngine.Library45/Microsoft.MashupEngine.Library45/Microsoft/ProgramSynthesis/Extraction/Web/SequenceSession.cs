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
	// Token: 0x02000FCC RID: 4044
	public class SequenceSession : NonInteractiveSession<SequenceProgram, IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>>
	{
		// Token: 0x06006F96 RID: 28566 RVA: 0x0016C728 File Offset: 0x0016A928
		public SequenceSession(IJournalStorage journalStorage = null, CultureInfo culture = null, ILogger logger = null, IEnumerable<EntityDetector> entityDetectorObjects = null)
			: base(SequenceLearner.Instance, Loader.Instance, journalStorage, culture, "Extraction.Web.Sequence", logger, true)
		{
			EntityDetectorsMap entityDetectorsMap = new EntityDetectorsMap(entityDetectorObjects);
			this.DeserializationContext = DeserializationContext.Create<EntityDetectorsMap>(entityDetectorsMap);
			base.Constraints.Add(new EntityDetectorsMapConstraint<IEnumerable<WebRegion>, IEnumerable<IEnumerable<WebRegion>>>(entityDetectorsMap));
		}

		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x06006F97 RID: 28567 RVA: 0x0016C773 File Offset: 0x0016A973
		public override DeserializationContext DeserializationContext { get; }
	}
}
