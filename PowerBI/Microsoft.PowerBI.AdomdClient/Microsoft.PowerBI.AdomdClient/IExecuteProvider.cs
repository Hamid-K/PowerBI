using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000090 RID: 144
	internal interface IExecuteProvider
	{
		// Token: 0x0600089F RID: 2207
		MDDatasetFormatter ExecuteMultidimensional(ICommandContentProvider contentProvider, AdomdPropertyCollection commandProperties, IDataParameterCollection parameters);

		// Token: 0x060008A0 RID: 2208
		XmlaReader ExecuteTabular(CommandBehavior behavior, ICommandContentProvider contentProvider, AdomdPropertyCollection commandProperties, IDataParameterCollection parameters);

		// Token: 0x060008A1 RID: 2209
		void ExecuteAny(ICommandContentProvider contentProvider, AdomdPropertyCollection commandProperties, IDataParameterCollection parameters);

		// Token: 0x060008A2 RID: 2210
		XmlaReader Execute(ICommandContentProvider contentProvider, AdomdPropertyCollection commandProperties, IDataParameterCollection parameters);

		// Token: 0x060008A3 RID: 2211
		void Prepare(ICommandContentProvider contentProvider, AdomdPropertyCollection commandProperties, IDataParameterCollection parameters);
	}
}
