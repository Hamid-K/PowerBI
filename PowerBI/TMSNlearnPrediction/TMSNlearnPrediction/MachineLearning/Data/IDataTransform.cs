using System;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000014 RID: 20
	public interface IDataTransform : IDataView, ISchematized, ICanSaveModel
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600005D RID: 93
		IDataView Source { get; }
	}
}
