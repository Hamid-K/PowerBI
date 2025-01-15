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
	// Token: 0x02000FC4 RID: 4036
	public class RegionSession : NonInteractiveSession<RegionProgram, IEnumerable<WebRegion>, IEnumerable<WebRegion>>
	{
		// Token: 0x06006F61 RID: 28513 RVA: 0x0016BF74 File Offset: 0x0016A174
		public RegionSession(IJournalStorage journalStorage = null, CultureInfo culture = null, ILogger logger = null, IEnumerable<EntityDetector> entityDetectorObjects = null)
			: base(RegionLearner.Instance, Loader.Instance, journalStorage, culture, "Extraction.Web.Region", logger, true)
		{
			if (entityDetectorObjects != null)
			{
				EntityDetectorsMap entityDetectorsMap = new EntityDetectorsMap(entityDetectorObjects);
				this.DeserializationContext = DeserializationContext.Create<EntityDetectorsMap>(entityDetectorsMap);
				base.Constraints.Add(new EntityDetectorsMapConstraint<IEnumerable<WebRegion>, IEnumerable<WebRegion>>(entityDetectorsMap));
			}
		}

		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x06006F62 RID: 28514 RVA: 0x0016BFC3 File Offset: 0x0016A1C3
		public override DeserializationContext DeserializationContext { get; }
	}
}
