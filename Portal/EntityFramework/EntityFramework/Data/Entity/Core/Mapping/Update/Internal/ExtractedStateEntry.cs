using System;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005C4 RID: 1476
	internal struct ExtractedStateEntry
	{
		// Token: 0x0600475F RID: 18271 RVA: 0x000FC78D File Offset: 0x000FA98D
		internal ExtractedStateEntry(EntityState state, PropagatorResult original, PropagatorResult current, IEntityStateEntry source)
		{
			this.State = state;
			this.Original = original;
			this.Current = current;
			this.Source = source;
		}

		// Token: 0x06004760 RID: 18272 RVA: 0x000FC7AC File Offset: 0x000FA9AC
		internal ExtractedStateEntry(UpdateTranslator translator, IEntityStateEntry stateEntry)
		{
			this.State = stateEntry.State;
			this.Source = stateEntry;
			EntityState state = stateEntry.State;
			if (state <= EntityState.Added)
			{
				if (state == EntityState.Unchanged)
				{
					this.Original = translator.RecordConverter.ConvertOriginalValuesToPropagatorResult(stateEntry, ModifiedPropertiesBehavior.NoneModified);
					this.Current = translator.RecordConverter.ConvertCurrentValuesToPropagatorResult(stateEntry, ModifiedPropertiesBehavior.NoneModified);
					return;
				}
				if (state == EntityState.Added)
				{
					this.Original = null;
					this.Current = translator.RecordConverter.ConvertCurrentValuesToPropagatorResult(stateEntry, ModifiedPropertiesBehavior.AllModified);
					return;
				}
			}
			else
			{
				if (state == EntityState.Deleted)
				{
					this.Original = translator.RecordConverter.ConvertOriginalValuesToPropagatorResult(stateEntry, ModifiedPropertiesBehavior.AllModified);
					this.Current = null;
					return;
				}
				if (state == EntityState.Modified)
				{
					this.Original = translator.RecordConverter.ConvertOriginalValuesToPropagatorResult(stateEntry, ModifiedPropertiesBehavior.SomeModified);
					this.Current = translator.RecordConverter.ConvertCurrentValuesToPropagatorResult(stateEntry, ModifiedPropertiesBehavior.SomeModified);
					return;
				}
			}
			this.Original = null;
			this.Current = null;
		}

		// Token: 0x04001958 RID: 6488
		internal readonly EntityState State;

		// Token: 0x04001959 RID: 6489
		internal readonly PropagatorResult Original;

		// Token: 0x0400195A RID: 6490
		internal readonly PropagatorResult Current;

		// Token: 0x0400195B RID: 6491
		internal readonly IEntityStateEntry Source;
	}
}
