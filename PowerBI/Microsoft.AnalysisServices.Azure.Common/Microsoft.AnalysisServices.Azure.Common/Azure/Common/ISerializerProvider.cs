using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000AE RID: 174
	public interface ISerializerProvider
	{
		// Token: 0x06000614 RID: 1556
		ISerializer<T> GetSerializer<T>();
	}
}
