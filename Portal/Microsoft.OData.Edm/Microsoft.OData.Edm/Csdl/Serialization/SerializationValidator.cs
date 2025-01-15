using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.Serialization
{
	// Token: 0x0200015A RID: 346
	internal static class SerializationValidator
	{
		// Token: 0x060008CC RID: 2252 RVA: 0x000180BC File Offset: 0x000162BC
		public static IEnumerable<EdmError> GetSerializationErrors(this IEdmModel root)
		{
			IEnumerable<EdmError> enumerable;
			root.Validate(SerializationValidator.serializationRuleSet, out enumerable);
			enumerable = enumerable.Where(new Func<EdmError, bool>(SerializationValidator.SignificantToSerialization));
			return enumerable;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x000180EC File Offset: 0x000162EC
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
				else if (errorCode != EdmErrorCode.OperationImportEntitySetExpressionIsInvalid && errorCode != EdmErrorCode.SystemNamespaceEncountered && errorCode != EdmErrorCode.InvalidNamespaceName)
				{
					return false;
				}
			}
			else if (errorCode <= EdmErrorCode.EnumMemberValueOutOfRange)
			{
				if (errorCode != EdmErrorCode.OperationImportParameterIncorrectType && errorCode != EdmErrorCode.EnumMemberValueOutOfRange)
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
				case EdmErrorCode.SchemaElementMustNotHaveKindOfNone:
				case EdmErrorCode.EntityContainerElementMustNotHaveKindOfNone:
				case EdmErrorCode.BinaryValueCannotHaveEmptyValue:
					break;
				case EdmErrorCode.PropertyTypeCannotBeCollectionOfAbstractType:
					return false;
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

		// Token: 0x040005B8 RID: 1464
		private static readonly ValidationRule<IEdmTypeReference> TypeReferenceTargetMustHaveValidName = new ValidationRule<IEdmTypeReference>(delegate(ValidationContext context, IEdmTypeReference typeReference)
		{
			IEdmSchemaType edmSchemaType = typeReference.Definition as IEdmSchemaType;
			if (edmSchemaType != null && !EdmUtil.IsQualifiedName(edmSchemaType.FullName()))
			{
				context.AddError(typeReference.Location(), EdmErrorCode.ReferencedTypeMustHaveValidName, Strings.Serializer_ReferencedTypeMustHaveValidName(edmSchemaType.FullName()));
			}
		});

		// Token: 0x040005B9 RID: 1465
		private static readonly ValidationRule<IEdmEntityReferenceType> EntityReferenceTargetMustHaveValidName = new ValidationRule<IEdmEntityReferenceType>(delegate(ValidationContext context, IEdmEntityReferenceType entityReference)
		{
			if (!EdmUtil.IsQualifiedName(entityReference.EntityType.FullName()))
			{
				context.AddError(entityReference.Location(), EdmErrorCode.ReferencedTypeMustHaveValidName, Strings.Serializer_ReferencedTypeMustHaveValidName(entityReference.EntityType.FullName()));
			}
		});

		// Token: 0x040005BA RID: 1466
		private static readonly ValidationRule<IEdmEntitySet> EntitySetTypeMustHaveValidName = new ValidationRule<IEdmEntitySet>(delegate(ValidationContext context, IEdmEntitySet set)
		{
			if (!EdmUtil.IsQualifiedName(set.EntityType().FullName()))
			{
				context.AddError(set.Location(), EdmErrorCode.ReferencedTypeMustHaveValidName, Strings.Serializer_ReferencedTypeMustHaveValidName(set.EntityType().FullName()));
			}
		});

		// Token: 0x040005BB RID: 1467
		private static readonly ValidationRule<IEdmStructuredType> StructuredTypeBaseTypeMustHaveValidName = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType type)
		{
			IEdmSchemaType edmSchemaType2 = type.BaseType as IEdmSchemaType;
			if (edmSchemaType2 != null && !EdmUtil.IsQualifiedName(edmSchemaType2.FullName()))
			{
				context.AddError(type.Location(), EdmErrorCode.ReferencedTypeMustHaveValidName, Strings.Serializer_ReferencedTypeMustHaveValidName(edmSchemaType2.FullName()));
			}
		});

		// Token: 0x040005BC RID: 1468
		private static readonly ValidationRule<IEdmVocabularyAnnotation> VocabularyAnnotationOutOfLineMustHaveValidTargetName = new ValidationRule<IEdmVocabularyAnnotation>(delegate(ValidationContext context, IEdmVocabularyAnnotation annotation)
		{
			if (annotation.GetSerializationLocation(context.Model) == EdmVocabularyAnnotationSerializationLocation.OutOfLine && !EdmUtil.IsQualifiedName(annotation.TargetString()))
			{
				context.AddError(annotation.Location(), EdmErrorCode.InvalidName, Strings.Serializer_OutOfLineAnnotationTargetMustHaveValidName(EdmUtil.FullyQualifiedName(annotation.Target)));
			}
		});

		// Token: 0x040005BD RID: 1469
		private static readonly ValidationRule<IEdmVocabularyAnnotation> VocabularyAnnotationMustHaveValidTermName = new ValidationRule<IEdmVocabularyAnnotation>(delegate(ValidationContext context, IEdmVocabularyAnnotation annotation)
		{
			if (!EdmUtil.IsQualifiedName(annotation.Term.FullName()))
			{
				context.AddError(annotation.Location(), EdmErrorCode.InvalidName, Strings.Serializer_OutOfLineAnnotationTargetMustHaveValidName(annotation.Term.FullName()));
			}
		});

		// Token: 0x040005BE RID: 1470
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
			ValidationRules.SchemaElementMustNotHaveKindOfNone,
			ValidationRules.EntityContainerElementMustNotHaveKindOfNone,
			ValidationRules.EnumMustHaveIntegerUnderlyingType,
			ValidationRules.EnumMemberValueMustHaveSameTypeAsUnderlyingType
		});
	}
}
