using System;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core.DomainModel
{
	// Token: 0x0200018A RID: 394
	public interface IConceptualSchemaProvider : IDisposable
	{
		// Token: 0x060007AE RID: 1966
		bool TryGetConceptualSchema(IDomainModelDiagnosticContext diagnosticContext, out IConceptualSchema schema, bool skipYearColumnDetectionHeuristics = false);
	}
}
