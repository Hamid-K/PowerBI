using System;
using System.Runtime.CompilerServices;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000147 RID: 327
	internal struct ImmutableObjectAccessController<TObject> where TObject : class, ICloneable
	{
		// Token: 0x06001047 RID: 4167 RVA: 0x00037F88 File Offset: 0x00036188
		public ImmutableObjectAccessController(TObject @object, bool canBeUpdated = true)
		{
			this.@object = @object;
			this.canBeUpdated = canBeUpdated;
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x00037F98 File Offset: 0x00036198
		internal bool IsValid
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.@object != null;
			}
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00037FA8 File Offset: 0x000361A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TObject GetObjectForView(ref ImmutableObjectAccessController<TObject> controller)
		{
			return controller.@object;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x00037FB0 File Offset: 0x000361B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TObject GetObjectForUpdate(ref ImmutableObjectAccessController<TObject> controller)
		{
			if (!controller.canBeUpdated)
			{
				controller = new ImmutableObjectAccessController<TObject>((TObject)((object)controller.@object.Clone()), true);
			}
			return controller.@object;
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x00037FE1 File Offset: 0x000361E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TObject GetObjectForUse(ref ImmutableObjectAccessController<TObject> controller)
		{
			if (controller.canBeUpdated)
			{
				controller = new ImmutableObjectAccessController<TObject>(controller.@object, false);
			}
			return controller.@object;
		}

		// Token: 0x04000B10 RID: 2832
		private readonly TObject @object;

		// Token: 0x04000B11 RID: 2833
		private readonly bool canBeUpdated;
	}
}
