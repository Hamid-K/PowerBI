using System;
using System.Runtime.CompilerServices;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000147 RID: 327
	internal struct ImmutableObjectAccessController<TObject> where TObject : class, ICloneable
	{
		// Token: 0x0600103A RID: 4154 RVA: 0x00037C58 File Offset: 0x00035E58
		public ImmutableObjectAccessController(TObject @object, bool canBeUpdated = true)
		{
			this.@object = @object;
			this.canBeUpdated = canBeUpdated;
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x00037C68 File Offset: 0x00035E68
		internal bool IsValid
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.@object != null;
			}
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00037C78 File Offset: 0x00035E78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TObject GetObjectForView(ref ImmutableObjectAccessController<TObject> controller)
		{
			return controller.@object;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x00037C80 File Offset: 0x00035E80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TObject GetObjectForUpdate(ref ImmutableObjectAccessController<TObject> controller)
		{
			if (!controller.canBeUpdated)
			{
				controller = new ImmutableObjectAccessController<TObject>((TObject)((object)controller.@object.Clone()), true);
			}
			return controller.@object;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00037CB1 File Offset: 0x00035EB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TObject GetObjectForUse(ref ImmutableObjectAccessController<TObject> controller)
		{
			if (controller.canBeUpdated)
			{
				controller = new ImmutableObjectAccessController<TObject>(controller.@object, false);
			}
			return controller.@object;
		}

		// Token: 0x04000B03 RID: 2819
		private readonly TObject @object;

		// Token: 0x04000B04 RID: 2820
		private readonly bool canBeUpdated;
	}
}
