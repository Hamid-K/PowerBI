using System;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Utils
{
	// Token: 0x02000574 RID: 1396
	internal enum ViewGenErrorCode
	{
		// Token: 0x04001858 RID: 6232
		Value = 3000,
		// Token: 0x04001859 RID: 6233
		InvalidCondition,
		// Token: 0x0400185A RID: 6234
		KeyConstraintViolation,
		// Token: 0x0400185B RID: 6235
		KeyConstraintUpdateViolation,
		// Token: 0x0400185C RID: 6236
		AttributesUnrecoverable,
		// Token: 0x0400185D RID: 6237
		AmbiguousMultiConstants,
		// Token: 0x0400185E RID: 6238
		NonKeyProjectedWithOverlappingPartitions = 3007,
		// Token: 0x0400185F RID: 6239
		ConcurrencyDerivedClass,
		// Token: 0x04001860 RID: 6240
		ConcurrencyTokenHasCondition,
		// Token: 0x04001861 RID: 6241
		DomainConstraintViolation = 3012,
		// Token: 0x04001862 RID: 6242
		ForeignKeyMissingTableMapping,
		// Token: 0x04001863 RID: 6243
		ForeignKeyNotGuaranteedInCSpace,
		// Token: 0x04001864 RID: 6244
		ForeignKeyMissingRelationshipMapping,
		// Token: 0x04001865 RID: 6245
		ForeignKeyUpperBoundMustBeOne,
		// Token: 0x04001866 RID: 6246
		ForeignKeyLowerBoundMustBeOne,
		// Token: 0x04001867 RID: 6247
		ForeignKeyParentTableNotMappedToEnd,
		// Token: 0x04001868 RID: 6248
		ForeignKeyColumnOrderIncorrect,
		// Token: 0x04001869 RID: 6249
		DisjointConstraintViolation,
		// Token: 0x0400186A RID: 6250
		DuplicateCPropertiesMapped,
		// Token: 0x0400186B RID: 6251
		NotNullNoProjectedSlot,
		// Token: 0x0400186C RID: 6252
		NoDefaultValue,
		// Token: 0x0400186D RID: 6253
		KeyNotMappedForCSideExtent,
		// Token: 0x0400186E RID: 6254
		KeyNotMappedForTable,
		// Token: 0x0400186F RID: 6255
		PartitionConstraintViolation,
		// Token: 0x04001870 RID: 6256
		MissingExtentMapping,
		// Token: 0x04001871 RID: 6257
		ImpossibleCondition = 3030,
		// Token: 0x04001872 RID: 6258
		NullableMappingForNonNullableColumn,
		// Token: 0x04001873 RID: 6259
		ErrorPatternConditionError,
		// Token: 0x04001874 RID: 6260
		ErrorPatternSplittingError,
		// Token: 0x04001875 RID: 6261
		ErrorPatternInvalidPartitionError,
		// Token: 0x04001876 RID: 6262
		ErrorPatternMissingMappingError,
		// Token: 0x04001877 RID: 6263
		NoJoinKeyOrFKProvidedInMapping,
		// Token: 0x04001878 RID: 6264
		MultipleFragmentsBetweenCandSExtentWithDistinct
	}
}
