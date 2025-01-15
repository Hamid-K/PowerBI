using System;

namespace NLog.Config
{
	// Token: 0x02000185 RID: 389
	internal interface IFactory
	{
		// Token: 0x060011BD RID: 4541
		void Clear();

		// Token: 0x060011BE RID: 4542
		void ScanTypes(Type[] types, string prefix);

		// Token: 0x060011BF RID: 4543
		void RegisterType(Type type, string itemNamePrefix);
	}
}
