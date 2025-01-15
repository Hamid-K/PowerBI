using System;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000076 RID: 118
	public interface ITransformTemplate : IDataTransform, IDataView, ISchematized, ICanSaveModel
	{
		// Token: 0x06000212 RID: 530
		IDataTransform ApplyToData(IHostEnvironment env, IDataView newSource);
	}
}
