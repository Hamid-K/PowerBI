using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001DA8 RID: 7592
	public class TraitTrackingService : ITraitTrackingService
	{
		// Token: 0x0600BC3A RID: 48186 RVA: 0x002616C3 File Offset: 0x0025F8C3
		public TraitTrackingService(IEngine engine)
		{
			this.engine = engine;
			this.traits = new HashSet<IRecordValue>();
		}

		// Token: 0x0600BC3B RID: 48187 RVA: 0x002616E0 File Offset: 0x0025F8E0
		public void AddTrait(IRecordValue trait)
		{
			HashSet<IRecordValue> hashSet = this.traits;
			lock (hashSet)
			{
				if (this.traits.Add(trait) && this.hasReturnedTraits)
				{
					throw this.engine.Exception(this.engine.ExceptionRecord(this.engine.Text("Expression.Error"), this.engine.Text(Strings.LineageWasLost), this.engine.Null));
				}
			}
		}

		// Token: 0x0600BC3C RID: 48188 RVA: 0x00261774 File Offset: 0x0025F974
		public IRecordValue[] GetTraits()
		{
			HashSet<IRecordValue> hashSet = this.traits;
			IRecordValue[] array;
			lock (hashSet)
			{
				this.hasReturnedTraits = true;
				array = this.traits.ToArray<IRecordValue>();
			}
			return array;
		}

		// Token: 0x04005FD0 RID: 24528
		private readonly IEngine engine;

		// Token: 0x04005FD1 RID: 24529
		private readonly HashSet<IRecordValue> traits;

		// Token: 0x04005FD2 RID: 24530
		private bool hasReturnedTraits;
	}
}
