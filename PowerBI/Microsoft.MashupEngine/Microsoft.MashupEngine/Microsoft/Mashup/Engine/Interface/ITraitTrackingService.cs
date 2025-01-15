using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000072 RID: 114
	public interface ITraitTrackingService
	{
		// Token: 0x060001B8 RID: 440
		void AddTrait(IRecordValue trait);

		// Token: 0x060001B9 RID: 441
		IRecordValue[] GetTraits();
	}
}
