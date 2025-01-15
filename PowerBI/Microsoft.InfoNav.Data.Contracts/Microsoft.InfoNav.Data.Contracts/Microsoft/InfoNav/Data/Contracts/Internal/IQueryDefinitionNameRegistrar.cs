using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000265 RID: 613
	internal interface IQueryDefinitionNameRegistrar
	{
		// Token: 0x0600125D RID: 4701
		void PushName(string name, bool isUnique);

		// Token: 0x0600125E RID: 4702
		void PopName(string name);

		// Token: 0x0600125F RID: 4703
		string GetNextName();
	}
}
