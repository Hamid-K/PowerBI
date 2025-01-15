using System;

namespace NLog.Config
{
	// Token: 0x02000189 RID: 393
	public interface INamedItemFactory<TInstanceType, TDefinitionType> where TInstanceType : class
	{
		// Token: 0x060011C9 RID: 4553
		void RegisterDefinition(string itemName, TDefinitionType itemDefinition);

		// Token: 0x060011CA RID: 4554
		bool TryGetDefinition(string itemName, out TDefinitionType result);

		// Token: 0x060011CB RID: 4555
		TInstanceType CreateInstance(string itemName);

		// Token: 0x060011CC RID: 4556
		bool TryCreateInstance(string itemName, out TInstanceType result);
	}
}
