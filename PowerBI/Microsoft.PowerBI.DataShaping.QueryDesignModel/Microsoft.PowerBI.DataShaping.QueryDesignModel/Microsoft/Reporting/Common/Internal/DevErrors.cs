using System;

namespace Microsoft.Reporting.Common.Internal
{
	// Token: 0x02000284 RID: 644
	internal static class DevErrors
	{
		// Token: 0x02000413 RID: 1043
		internal static class RecordableObservableCollection
		{
			// Token: 0x04001476 RID: 5238
			internal const string NonEmptyCollection = "The collection is not empty.";

			// Token: 0x04001477 RID: 5239
			internal const string ItemHasParent = "Item already has a parent set.";

			// Token: 0x04001478 RID: 5240
			internal const string ItemParentChanged = "Parent property changed while a member of an RecordableObservableCollection.";

			// Token: 0x04001479 RID: 5241
			internal const string PendingCollectionChangeNotFired = "A previous collection change was not fired.";

			// Token: 0x0400147A RID: 5242
			internal const string NoPendingCollectionChangeToFire = "No collection change that has not been fired.";
		}

		// Token: 0x02000414 RID: 1044
		internal static class RecordablePropertyStore
		{
			// Token: 0x0400147B RID: 5243
			internal const string SetOwnerWhenAlreadyHasOwner = "SetOwner called for RecordablePropertyStore that already has an Owner.";

			// Token: 0x0400147C RID: 5244
			internal const string ValueAndPropertyTypeInconsistent = "Value and PropertyType are not consistent.";

			// Token: 0x0400147D RID: 5245
			internal const string ValueAlreadyHasParent = "Value already has a Parent set.";

			// Token: 0x0400147E RID: 5246
			internal const string PropertyChangedEventArgsNotConsistentWithPropertyKeyName = "PropertyChangedEventArgs are not consistent with the provided key and propertyName.";

			// Token: 0x0400147F RID: 5247
			internal const string SynchronizationContextMismatch = "SynchronizationContext of executing thread does not match SynchronizationContext of the object.";
		}

		// Token: 0x02000415 RID: 1045
		internal static class RSTraceUtils
		{
			// Token: 0x04001480 RID: 5248
			internal const string ReservedCharacterUsed = "Using reserved character is not permitted";
		}

		// Token: 0x02000416 RID: 1046
		internal static class CompositeValue
		{
			// Token: 0x04001481 RID: 5249
			internal const string ComparisonErrorValueCountMismatch = "Cannot compare CompositeValue objects with different numbers of values.";

			// Token: 0x04001482 RID: 5250
			internal const string ComparisonErrorCannotCompareOtherType = "Cannot compare CompositeValue objects with objects of a different type.";
		}

		// Token: 0x02000417 RID: 1047
		internal static class ScalarValue
		{
			// Token: 0x04001483 RID: 5251
			internal const string ComparisonErrorCannotCompareOtherType = "Cannot compare ScalarValue objects with objects of a different type.";
		}
	}
}
