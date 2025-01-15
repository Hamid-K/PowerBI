using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Evaluator.Services;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D56 RID: 7510
	internal class PartitionTraitTrackingService : ITraitTrackingService
	{
		// Token: 0x0600BAD0 RID: 47824 RVA: 0x0025D271 File Offset: 0x0025B471
		public PartitionTraitTrackingService(IEngineHost engineHost)
		{
			this.traitTrackingService = new TraitTrackingService(engineHost.QueryService<IEngine>());
			this.baseTraitTrackingService = engineHost.QueryService<ITraitTrackingService>();
		}

		// Token: 0x0600BAD1 RID: 47825 RVA: 0x0025D296 File Offset: 0x0025B496
		public static IEngineHost WrapTrackingService(IEngineHost engineHost)
		{
			return new CompositeEngineHost(new IEngineHost[]
			{
				new SimpleEngineHost<ITraitTrackingService>(new PartitionTraitTrackingService(engineHost)),
				engineHost
			});
		}

		// Token: 0x0600BAD2 RID: 47826 RVA: 0x0025D2B5 File Offset: 0x0025B4B5
		public void AddTrait(IRecordValue trait)
		{
			this.traitTrackingService.AddTrait(trait);
			this.baseTraitTrackingService.AddTrait(trait);
		}

		// Token: 0x0600BAD3 RID: 47827 RVA: 0x0025D2CF File Offset: 0x0025B4CF
		public IRecordValue[] GetTraits()
		{
			return this.traitTrackingService.GetTraits();
		}

		// Token: 0x04005F13 RID: 24339
		private readonly TraitTrackingService traitTrackingService;

		// Token: 0x04005F14 RID: 24340
		private readonly ITraitTrackingService baseTraitTrackingService;
	}
}
