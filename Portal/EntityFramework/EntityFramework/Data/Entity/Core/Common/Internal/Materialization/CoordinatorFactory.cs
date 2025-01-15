using System;
using System.Collections.ObjectModel;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x02000637 RID: 1591
	internal abstract class CoordinatorFactory
	{
		// Token: 0x06004C8E RID: 19598 RVA: 0x0010E760 File Offset: 0x0010C960
		protected CoordinatorFactory(int depth, int stateSlot, Func<Shaper, bool> hasData, Func<Shaper, bool> setKeys, Func<Shaper, bool> checkKeys, CoordinatorFactory[] nestedCoordinators, RecordStateFactory[] recordStateFactories)
		{
			this.Depth = depth;
			this.StateSlot = stateSlot;
			this.IsLeafResult = nestedCoordinators.Length == 0;
			if (hasData == null)
			{
				this.HasData = CoordinatorFactory._alwaysTrue;
			}
			else
			{
				this.HasData = hasData;
			}
			if (setKeys == null)
			{
				this.SetKeys = CoordinatorFactory._alwaysTrue;
			}
			else
			{
				this.SetKeys = setKeys;
			}
			if (checkKeys == null)
			{
				if (this.IsLeafResult)
				{
					this.CheckKeys = CoordinatorFactory._alwaysFalse;
				}
				else
				{
					this.CheckKeys = CoordinatorFactory._alwaysTrue;
				}
			}
			else
			{
				this.CheckKeys = checkKeys;
			}
			this.NestedCoordinators = new ReadOnlyCollection<CoordinatorFactory>(nestedCoordinators);
			this.RecordStateFactories = new ReadOnlyCollection<RecordStateFactory>(recordStateFactories);
			this.IsSimple = this.IsLeafResult && checkKeys == null && hasData == null;
		}

		// Token: 0x06004C8F RID: 19599
		internal abstract Coordinator CreateCoordinator(Coordinator parent, Coordinator next);

		// Token: 0x04001B14 RID: 6932
		private static readonly Func<Shaper, bool> _alwaysTrue = (Shaper s) => true;

		// Token: 0x04001B15 RID: 6933
		private static readonly Func<Shaper, bool> _alwaysFalse = (Shaper s) => false;

		// Token: 0x04001B16 RID: 6934
		internal readonly int Depth;

		// Token: 0x04001B17 RID: 6935
		internal readonly int StateSlot;

		// Token: 0x04001B18 RID: 6936
		internal readonly Func<Shaper, bool> HasData;

		// Token: 0x04001B19 RID: 6937
		internal readonly Func<Shaper, bool> SetKeys;

		// Token: 0x04001B1A RID: 6938
		internal readonly Func<Shaper, bool> CheckKeys;

		// Token: 0x04001B1B RID: 6939
		internal readonly ReadOnlyCollection<CoordinatorFactory> NestedCoordinators;

		// Token: 0x04001B1C RID: 6940
		internal readonly bool IsLeafResult;

		// Token: 0x04001B1D RID: 6941
		internal readonly bool IsSimple;

		// Token: 0x04001B1E RID: 6942
		internal readonly ReadOnlyCollection<RecordStateFactory> RecordStateFactories;
	}
}
