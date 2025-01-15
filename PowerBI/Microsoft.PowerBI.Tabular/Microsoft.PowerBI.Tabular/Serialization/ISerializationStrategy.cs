using System;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x0200017A RID: 378
	internal interface ISerializationStrategy
	{
		// Token: 0x060017BA RID: 6074
		string GetObjectLogicalPath(ObjectType objectType, string objectName, out bool isSerializedInParentScope);
	}
}
