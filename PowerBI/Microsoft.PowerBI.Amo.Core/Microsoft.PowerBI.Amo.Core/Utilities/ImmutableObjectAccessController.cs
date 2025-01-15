using System;
using System.Runtime.CompilerServices;

namespace Microsoft.AnalysisServices.Utilities
{
	// Token: 0x0200013C RID: 316
	internal struct ImmutableObjectAccessController<TObject> where TObject : class, ICloneable
	{
		// Token: 0x060010D5 RID: 4309 RVA: 0x0003A88C File Offset: 0x00038A8C
		public ImmutableObjectAccessController(TObject @object, bool canBeUpdated = true)
		{
			this.@object = @object;
			this.canBeUpdated = canBeUpdated;
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x0003A89C File Offset: 0x00038A9C
		internal bool IsValid
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.@object != null;
			}
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0003A8AC File Offset: 0x00038AAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TObject GetObjectForView(ref ImmutableObjectAccessController<TObject> controller)
		{
			return controller.@object;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0003A8B4 File Offset: 0x00038AB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TObject GetObjectForUpdate(ref ImmutableObjectAccessController<TObject> controller)
		{
			if (!controller.canBeUpdated)
			{
				controller = new ImmutableObjectAccessController<TObject>((TObject)((object)controller.@object.Clone()), true);
			}
			return controller.@object;
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0003A8E5 File Offset: 0x00038AE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TObject GetObjectForUse(ref ImmutableObjectAccessController<TObject> controller)
		{
			if (controller.canBeUpdated)
			{
				controller = new ImmutableObjectAccessController<TObject>(controller.@object, false);
			}
			return controller.@object;
		}

		// Token: 0x04000AC9 RID: 2761
		private readonly TObject @object;

		// Token: 0x04000ACA RID: 2762
		private readonly bool canBeUpdated;
	}
}
