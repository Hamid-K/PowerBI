using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Microsoft.AnalysisServices.Hosting
{
	// Token: 0x0200015C RID: 348
	[Guid("45BF1B3B-4C61-4681-89A5-29D9D43C462A")]
	public interface IHostMaterializationService
	{
		// Token: 0x060011B6 RID: 4534
		void MaterializeComponent(IComponent component, object parent);

		// Token: 0x060011B7 RID: 4535
		void DematerializeComponent(IComponent component, object parent);

		// Token: 0x060011B8 RID: 4536
		void UpdateMaterialization(IComponent component, bool updatePermanently);

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060011B9 RID: 4537
		bool SitingBlocked { get; }
	}
}
