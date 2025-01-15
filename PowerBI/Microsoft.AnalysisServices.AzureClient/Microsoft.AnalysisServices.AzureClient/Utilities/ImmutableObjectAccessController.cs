using System;
using System.Runtime.CompilerServices;

namespace Microsoft.AnalysisServices.AzureClient.Utilities
{
	// Token: 0x0200002B RID: 43
	internal struct ImmutableObjectAccessController<TObject> where TObject : class, ICloneable
	{
		// Token: 0x06000150 RID: 336 RVA: 0x00006C80 File Offset: 0x00004E80
		public ImmutableObjectAccessController(TObject @object, bool canBeUpdated = true)
		{
			this.@object = @object;
			this.canBeUpdated = canBeUpdated;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00006C90 File Offset: 0x00004E90
		internal bool IsValid
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.@object != null;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006CA0 File Offset: 0x00004EA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TObject GetObjectForView(ref ImmutableObjectAccessController<TObject> controller)
		{
			return controller.@object;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006CA8 File Offset: 0x00004EA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TObject GetObjectForUpdate(ref ImmutableObjectAccessController<TObject> controller)
		{
			if (!controller.canBeUpdated)
			{
				controller = new ImmutableObjectAccessController<TObject>((TObject)((object)controller.@object.Clone()), true);
			}
			return controller.@object;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006CD9 File Offset: 0x00004ED9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TObject GetObjectForUse(ref ImmutableObjectAccessController<TObject> controller)
		{
			if (controller.canBeUpdated)
			{
				controller = new ImmutableObjectAccessController<TObject>(controller.@object, false);
			}
			return controller.@object;
		}

		// Token: 0x040000CE RID: 206
		private readonly TObject @object;

		// Token: 0x040000CF RID: 207
		private readonly bool canBeUpdated;
	}
}
