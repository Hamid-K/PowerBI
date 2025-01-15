using System;
using Microsoft.Lucia.Core.DomainModel.Serialization;

namespace Microsoft.Lucia.Core.DomainModel
{
	// Token: 0x0200018B RID: 395
	public interface ILinguisticSchemaProvider : IDisposable
	{
		// Token: 0x060007AF RID: 1967
		bool TryGetLinguisticSchema(IDomainModelDiagnosticContext diagnosticContext, out LsdlDocument schema);
	}
}
