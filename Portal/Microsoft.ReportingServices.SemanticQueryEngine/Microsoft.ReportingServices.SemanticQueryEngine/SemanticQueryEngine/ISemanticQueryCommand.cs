using System;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.SemanticQueryEngine
{
	// Token: 0x02000012 RID: 18
	[CLSCompliant(false)]
	public interface ISemanticQueryCommand : IDbCommand, IDisposable, IExtension
	{
		// Token: 0x060000EF RID: 239
		void Initialize(IDbConnection targetConnection);

		// Token: 0x060000F0 RID: 240
		void SetQuery(SemanticQuery query);
	}
}
