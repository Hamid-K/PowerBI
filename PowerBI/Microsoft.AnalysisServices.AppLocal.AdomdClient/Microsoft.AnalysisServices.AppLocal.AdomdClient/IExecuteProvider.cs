using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000090 RID: 144
	internal interface IExecuteProvider
	{
		// Token: 0x060008AC RID: 2220
		MDDatasetFormatter ExecuteMultidimensional(ICommandContentProvider contentProvider, AdomdPropertyCollection commandProperties, IDataParameterCollection parameters);

		// Token: 0x060008AD RID: 2221
		XmlaReader ExecuteTabular(CommandBehavior behavior, ICommandContentProvider contentProvider, AdomdPropertyCollection commandProperties, IDataParameterCollection parameters);

		// Token: 0x060008AE RID: 2222
		void ExecuteAny(ICommandContentProvider contentProvider, AdomdPropertyCollection commandProperties, IDataParameterCollection parameters);

		// Token: 0x060008AF RID: 2223
		XmlaReader Execute(ICommandContentProvider contentProvider, AdomdPropertyCollection commandProperties, IDataParameterCollection parameters);

		// Token: 0x060008B0 RID: 2224
		void Prepare(ICommandContentProvider contentProvider, AdomdPropertyCollection commandProperties, IDataParameterCollection parameters);
	}
}
