using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Validation.Internal;

namespace Microsoft.OData.Edm.Csdl.Serialization
{
	// Token: 0x020000CE RID: 206
	internal static class SerializationValidator
	{
		// Token: 0x06000386 RID: 902 RVA: 0x00008238 File Offset: 0x00006438
		public static IEnumerable<EdmError> GetSerializationErrors(this IEdmModel root)
		{
			IEnumerable<EdmError> enumerable;
			root.Validate(SerializationValidator.serializationRuleSet, out enumerable);
			enumerable = Enumerable.Where<EdmError>(enumerable, new Func<EdmError, bool>(SerializationValidator.SignificantToSerialization));
			return enumerable;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00008268 File Offset: 0x00006468
		internal static bool SignificantToSerialization(EdmError error)
		{
			if (ValidationHelper.IsInterfaceCritical(error))
			{
				return true;
			}
			EdmErrorCode errorCode = error.ErrorCode;
			if (errorCode <= EdmErrorCode.InvalidNamespaceName)
			{
				if (errorCode <= EdmErrorCode.NameTooLong)
				{
					if (errorCode != EdmErrorCode.InvalidName && errorCode != EdmErrorCode.NameTooLong)
					{
						return false;
					}
				}
				else if (errorCode != EdmErrorCode.OperationImportEntitySetExpressionIsInvalid)
				{
					switch (errorCode)
					{
					case EdmErrorCode.SystemNamespaceEncountered:
					case EdmErrorCode.InvalidNamespaceName:
						break;
					case (EdmErrorCode)162:
						return false;
					default:
						return false;
					}
				}
			}
			else if (errorCode <= EdmErrorCode.EnumMemberTypeMustMatchEnumUnderlyingType)
			{
				if (errorCode != EdmErrorCode.OperationImportParameterIncorrectType && errorCode != EdmErrorCode.EnumMemberTypeMustMatchEnumUnderlyingType)
				{
					return false;
				}
			}
			else if (errorCode != EdmErrorCode.ReferencedTypeMustHaveValidName)
			{
				switch (errorCode)
				{
				case EdmErrorCode.InvalidOperationImportParameterMode:
				case EdmErrorCode.TypeMustNotHaveKindOfNone:
				case EdmErrorCode.PrimitiveTypeMustNotHaveKindOfNone:
				case EdmErrorCode.PropertyMustNotHaveKindOfNone:
				case EdmErrorCode.TermMustNotHaveKindOfNone:
				case EdmErrorCode.SchemaElementMustNotHaveKindOfNone:
				case EdmErrorCode.EntityContainerElementMustNotHaveKindOfNone:
				case EdmErrorCode.BinaryValueCannotHaveEmptyValue:
					break;
				default:
					if (errorCode != EdmErrorCode.EnumMustHaveIntegerUnderlyingType)
					{
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x0400017C RID: 380
		private static readonly ValidationRule<IEdmTypeReference> TypeReferenceTargetMustHaveValidName = new ValidationRule<IEdmTypeReference>(delegate(ValidationContext context, IEdmTypeReference typeReference)
		{
			IEdmSchemaType edmSchemaType = typeReference.Definition as IEdmSchemaType;
			if (edmSchemaType != null && !EdmUtil.IsQualifiedName(edmSchemaType.FullName()))
			{
				context.AddError(typeReference.Location(), EdmErrorCode.ReferencedTypeMustHaveValidName, Strings.Serializer_ReferencedTypeMustHaveValidName(edmSchemaType.FullName()));
			}
		});

		// Token: 0x0400017D RID: 381
		private static readonly ValidationRule<IEdmEntityReferenceType> EntityReferenceTargetMustHaveValidName = new ValidationRule<IEdmEntityReferenceType>(delegate(ValidationContext context, IEdmEntityReferenceType entityReference)
		{
			if (!EdmUtil.IsQualifiedName(entityReference.EntityType.FullName()))
			{
				context.AddError(entityReference.Location(), EdmErrorCode.ReferencedTypeMustHaveValidName, Strings.Serializer_ReferencedTypeMustHaveValidName(entityReference.EntityType.FullName()));
			}
		});

		// Token: 0x0400017E RID: 382
		private static readonly ValidationRule<IEdmEntitySet> EntitySetTypeMustHaveValidName = new ValidationRule<IEdmEntitySet>(delegate(ValidationContext context, IEdmEntitySet set)
		{
			if (!EdmUtil.IsQualifiedName(set.EntityType().FullName()))
			{
				context.AddError(set.Location(), EdmErrorCode.ReferencedTypeMustHaveValidName, Strings.Serializer_ReferencedTypeMustHaveValidName(set.EntityType().FullName()));
			}
		});

		// Token: 0x0400017F RID: 383
		private static readonly ValidationRule<IEdmStructuredType> StructuredTypeBaseTypeMustHaveValidName = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType type)
		{
			IEdmSchemaType edmSchemaType2 = type.BaseType as IEdmSchemaType;
			if (edmSchemaType2 != null && !EdmUtil.IsQualifiedName(edmSchemaType2.FullName()))
			{
				context.AddError(type.Location(), EdmErrorCode.ReferencedTypeMustHaveValidName, Strings.Serializer_ReferencedTypeMustHaveValidName(edmSchemaType2.FullName()));
			}
		});

		// Token: 0x04000180 RID: 384
		private static readonly ValidationRule<IEdmVocabularyAnnotation> VocabularyAnnotationOutOfLineMustHaveValidTargetName = new ValidationRule<IEdmVocabularyAnnotation>(delegate(ValidationContext context, IEdmVocabularyAnnotation annotation)
		{
			if (annotation.GetSerializationLocation(context.Model) == EdmVocabularyAnnotationSerializationLocation.OutOfLine && !EdmUtil.IsQualifiedName(annotation.TargetString()))
			{
				context.AddError(annotation.Location(), EdmErrorCode.InvalidName, Strings.Serializer_OutOfLineAnnotationTargetMustHaveValidName(EdmUtil.FullyQualifiedName(annotation.Target)));
			}
		});

		// Token: 0x04000181 RID: 385
		private static readonly ValidationRule<IEdmVocabularyAnnotation> VocabularyAnnotationMustHaveValidTermName = new ValidationRule<IEdmVocabularyAnnotation>(delegate(ValidationContext context, IEdmVocabularyAnnotation annotation)
		{
			if (!EdmUtil.IsQualifiedName(annotation.Term.FullName()))
			{
				context.AddError(annotation.Location(), EdmErrorCode.InvalidName, Strings.Serializer_OutOfLineAnnotationTargetMustHaveValidName(annotation.Term.FullName()));
			}
		});

		// Token: 0x04000182 RID: 386
		private static ValidationRuleSet serializationRuleSet = new ValidationRuleSet(new ValidationRule[]
		{
			SerializationValidator.TypeReferenceTargetMustHaveValidName,
			SerializationValidator.EntityReferenceTargetMustHaveValidName,
			SerializationValidator.EntitySetTypeMustHaveValidName,
			SerializationValidator.StructuredTypeBaseTypeMustHaveValidName,
			SerializationValidator.VocabularyAnnotationOutOfLineMustHaveValidTargetName,
			SerializationValidator.VocabularyAnnotationMustHaveValidTermName,
			ValidationRules.OperationImportEntitySetExpressionIsInvalid,
			ValidationRules.TypeMustNotHaveKindOfNone,
			ValidationRules.PrimitiveTypeMustNotHaveKindOfNone,
			ValidationRules.PropertyMustNotHaveKindOfNone,
			ValidationRules.TermMustNotHaveKindOfNone,
			ValidationRules.SchemaElementMustNotHaveKindOfNone,
			ValidationRules.EntityContainerElementMustNotHaveKindOfNone,
			ValidationRules.EnumMustHaveIntegerUnderlyingType,
			ValidationRules.EnumMemberValueMustHaveSameTypeAsUnderlyingType
		});
	}
}
