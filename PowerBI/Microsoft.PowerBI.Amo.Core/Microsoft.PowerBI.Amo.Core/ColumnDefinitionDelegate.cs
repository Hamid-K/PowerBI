using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200003E RID: 62
	// (Invoke) Token: 0x060002BE RID: 702
	internal delegate object ColumnDefinitionDelegate(int ordinal, string name, string theNamespace, string caption, Type type, bool isNested, object parent, string strColumnXsdType);
}
