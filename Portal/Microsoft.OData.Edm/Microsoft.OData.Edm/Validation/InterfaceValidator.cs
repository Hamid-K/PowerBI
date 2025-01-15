using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x0200013D RID: 317
	internal class InterfaceValidator
	{
		// Token: 0x060007E6 RID: 2022 RVA: 0x00012998 File Offset: 0x00010B98
		private InterfaceValidator(HashSetInternal<object> skipVisitation, IEdmModel model, bool validateDirectValueAnnotations)
		{
			this.skipVisitation = skipVisitation;
			this.model = model;
			this.validateDirectValueAnnotations = validateDirectValueAnnotations;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x000129D8 File Offset: 0x00010BD8
		public static IEnumerable<EdmError> ValidateModelStructureAndSemantics(IEdmModel model, ValidationRuleSet semanticRuleSet)
		{
			InterfaceValidator modelValidator = new InterfaceValidator(null, model, true);
			List<EdmError> list = new List<EdmError>(modelValidator.ValidateStructure(model));
			InterfaceValidator referencesValidator = new InterfaceValidator(modelValidator.visited, model, false);
			IEnumerable<object> enumerable = modelValidator.danglingReferences;
			while (enumerable.FirstOrDefault<object>() != null)
			{
				foreach (object obj in enumerable)
				{
					list.AddRange(referencesValidator.ValidateStructure(obj));
				}
				enumerable = referencesValidator.danglingReferences.ToArray<object>();
			}
			if (list.Any(new Func<EdmError, bool>(ValidationHelper.IsInterfaceCritical)))
			{
				return list;
			}
			ValidationContext validationContext = new ValidationContext(model, (object item) => modelValidator.visitedBad.Contains(item) || referencesValidator.visitedBad.Contains(item));
			Dictionary<Type, List<ValidationRule>> dictionary = new Dictionary<Type, List<ValidationRule>>();
			foreach (object obj2 in modelValidator.visited)
			{
				if (!modelValidator.visitedBad.Contains(obj2))
				{
					foreach (ValidationRule validationRule in InterfaceValidator.GetSemanticInterfaceVisitorsForObject(obj2.GetType(), semanticRuleSet, dictionary))
					{
						validationRule.Evaluate(validationContext, obj2);
					}
				}
			}
			list.AddRange(validationContext.Errors);
			return list;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00012B78 File Offset: 0x00010D78
		public static IEnumerable<EdmError> GetStructuralErrors(IEdmElement item)
		{
			IEdmModel edmModel = item as IEdmModel;
			InterfaceValidator interfaceValidator = new InterfaceValidator(null, edmModel, edmModel != null);
			return interfaceValidator.ValidateStructure(item);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00012BA0 File Offset: 0x00010DA0
		private static Dictionary<Type, InterfaceValidator.VisitorBase> CreateInterfaceVisitorsMap()
		{
			Dictionary<Type, InterfaceValidator.VisitorBase> dictionary = new Dictionary<Type, InterfaceValidator.VisitorBase>();
			IEnumerable<Type> nonPublicNestedTypes = typeof(InterfaceValidator).GetNonPublicNestedTypes();
			foreach (Type type in nonPublicNestedTypes)
			{
				if (type.IsClass())
				{
					Type baseType = type.GetBaseType();
					if (baseType.IsGenericType() && baseType.GetBaseType() == typeof(InterfaceValidator.VisitorBase))
					{
						dictionary.Add(baseType.GetGenericArguments()[0], (InterfaceValidator.VisitorBase)Activator.CreateInstance(type));
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00012C40 File Offset: 0x00010E40
		private static IEnumerable<InterfaceValidator.VisitorBase> ComputeInterfaceVisitorsForObject(Type objectType)
		{
			List<InterfaceValidator.VisitorBase> list = new List<InterfaceValidator.VisitorBase>();
			foreach (Type type in objectType.GetInterfaces())
			{
				InterfaceValidator.VisitorBase visitorBase;
				if (InterfaceValidator.InterfaceVisitors.TryGetValue(type, out visitorBase))
				{
					list.Add(visitorBase);
				}
			}
			return list;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00012CA4 File Offset: 0x00010EA4
		private static EdmError CreatePropertyMustNotBeNullError<T>(T item, string propertyName)
		{
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalPropertyValueMustNotBeNull, Strings.EdmModel_Validator_Syntactic_PropertyMustNotBeNull(typeof(T).Name, propertyName));
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00012CCD File Offset: 0x00010ECD
		private static EdmError CreateEnumPropertyOutOfRangeError<T, E>(T item, E enumValue, string propertyName)
		{
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalEnumPropertyValueOutOfRange, Strings.EdmModel_Validator_Syntactic_EnumPropertyValueOutOfRange(typeof(T).Name, propertyName, typeof(E).Name, enumValue));
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00012D0C File Offset: 0x00010F0C
		private static EdmError CheckForInterfaceKindValueMismatchError<T, K, I>(T item, K kind, string propertyName)
		{
			if (item is I)
			{
				return null;
			}
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalKindValueMismatch, Strings.EdmModel_Validator_Syntactic_InterfaceKindValueMismatch(kind, typeof(T).Name, propertyName, typeof(I).Name));
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00012D64 File Offset: 0x00010F64
		private static EdmError CreateInterfaceKindValueUnexpectedError<T, K>(T item, K kind, string propertyName)
		{
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalKindValueUnexpected, Strings.EdmModel_Validator_Syntactic_InterfaceKindValueUnexpected(kind, typeof(T).Name, propertyName));
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00012D93 File Offset: 0x00010F93
		private static EdmError CreateTypeRefInterfaceTypeKindValueMismatchError<T>(T item) where T : IEdmTypeReference
		{
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalKindValueMismatch, Strings.EdmModel_Validator_Syntactic_TypeRefInterfaceTypeKindValueMismatch(typeof(T).Name, item.Definition.TypeKind));
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00012DD4 File Offset: 0x00010FD4
		private static EdmError CreatePrimitiveTypeRefInterfaceTypeKindValueMismatchError<T>(T item) where T : IEdmPrimitiveTypeReference
		{
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalKindValueMismatch, Strings.EdmModel_Validator_Syntactic_TypeRefInterfaceTypeKindValueMismatch(typeof(T).Name, ((IEdmPrimitiveType)item.Definition).PrimitiveKind));
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00012E24 File Offset: 0x00011024
		private static void ProcessEnumerable<T, E>(T item, IEnumerable<E> enumerable, string propertyName, IList targetList, ref List<EdmError> errors)
		{
			if (enumerable == null)
			{
				InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<T>(item, propertyName), ref errors);
				return;
			}
			foreach (E e in enumerable)
			{
				if (e == null)
				{
					InterfaceValidator.CollectErrors(new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalEnumerableMustNotHaveNullElements, Strings.EdmModel_Validator_Syntactic_EnumerableMustNotHaveNullElements(typeof(T).Name, propertyName)), ref errors);
					break;
				}
				targetList.Add(e);
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00012EC0 File Offset: 0x000110C0
		private static void CollectErrors(EdmError newError, ref List<EdmError> errors)
		{
			if (newError != null)
			{
				if (errors == null)
				{
					errors = new List<EdmError>();
				}
				errors.Add(newError);
			}
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00012ED8 File Offset: 0x000110D8
		private static bool IsCheckableBad(object element)
		{
			IEdmCheckable edmCheckable = element as IEdmCheckable;
			return edmCheckable != null && edmCheckable.Errors != null && edmCheckable.Errors.Count<EdmError>() > 0;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00012F08 File Offset: 0x00011108
		private static EdmLocation GetLocation(object item)
		{
			IEdmLocatable edmLocatable = item as IEdmLocatable;
			if (edmLocatable == null || edmLocatable.Location == null)
			{
				return new ObjectLocation(item);
			}
			return edmLocatable.Location;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00012F34 File Offset: 0x00011134
		private static IEnumerable<ValidationRule> GetSemanticInterfaceVisitorsForObject(Type objectType, ValidationRuleSet ruleSet, Dictionary<Type, List<ValidationRule>> concreteTypeSemanticInterfaceVisitors)
		{
			List<ValidationRule> list;
			if (!concreteTypeSemanticInterfaceVisitors.TryGetValue(objectType, out list))
			{
				list = new List<ValidationRule>();
				foreach (Type type in objectType.GetInterfaces())
				{
					list.AddRange(ruleSet.GetRules(type));
				}
				concreteTypeSemanticInterfaceVisitors.Add(objectType, list);
			}
			return list;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00012FA4 File Offset: 0x000111A4
		private IEnumerable<EdmError> ValidateStructure(object item)
		{
			if (item is IEdmCoreModelElement || this.visited.Contains(item) || (this.skipVisitation != null && this.skipVisitation.Contains(item)))
			{
				return Enumerable.Empty<EdmError>();
			}
			this.visited.Add(item);
			if (this.danglingReferences.Contains(item))
			{
				this.danglingReferences.Remove(item);
			}
			List<EdmError> list = null;
			List<object> list2 = new List<object>();
			List<object> list3 = new List<object>();
			IEnumerable<InterfaceValidator.VisitorBase> enumerable = InterfaceValidator.ConcreteTypeInterfaceVisitors.Evaluate(item.GetType());
			foreach (InterfaceValidator.VisitorBase visitorBase in enumerable)
			{
				IEnumerable<EdmError> enumerable2 = visitorBase.Visit(item, list2, list3);
				if (enumerable2 != null)
				{
					foreach (EdmError edmError in enumerable2)
					{
						if (list == null)
						{
							list = new List<EdmError>();
						}
						list.Add(edmError);
					}
				}
			}
			if (list != null)
			{
				this.visitedBad.Add(item);
				return list;
			}
			List<EdmError> list4 = new List<EdmError>();
			if (this.validateDirectValueAnnotations)
			{
				IEdmElement edmElement = item as IEdmElement;
				if (edmElement != null)
				{
					foreach (IEdmDirectValueAnnotation edmDirectValueAnnotation in this.model.DirectValueAnnotations(edmElement))
					{
						list4.AddRange(this.ValidateStructure(edmDirectValueAnnotation));
					}
				}
			}
			IEdmVocabularyAnnotatable edmVocabularyAnnotatable = item as IEdmVocabularyAnnotatable;
			if (this.model != null && edmVocabularyAnnotatable != null)
			{
				foreach (IEdmVocabularyAnnotation edmVocabularyAnnotation in edmVocabularyAnnotatable.VocabularyAnnotations(this.model))
				{
					if (edmVocabularyAnnotation.Target == null)
					{
						list4.AddRange(this.ValidateStructure(new EdmVocabularyAnnotation(edmVocabularyAnnotatable, edmVocabularyAnnotation.Term, edmVocabularyAnnotation.Qualifier, edmVocabularyAnnotation.Value)));
					}
				}
			}
			foreach (object obj in list2)
			{
				list4.AddRange(this.ValidateStructure(obj));
			}
			foreach (object obj2 in list3)
			{
				this.CollectReference(obj2);
			}
			return list4;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001324C File Offset: 0x0001144C
		private void CollectReference(object reference)
		{
			if (!(reference is IEdmCoreModelElement) && !this.visited.Contains(reference) && (this.skipVisitation == null || !this.skipVisitation.Contains(reference)))
			{
				this.danglingReferences.Add(reference);
			}
		}

		// Token: 0x0400037D RID: 893
		private static readonly Dictionary<Type, InterfaceValidator.VisitorBase> InterfaceVisitors = InterfaceValidator.CreateInterfaceVisitorsMap();

		// Token: 0x0400037E RID: 894
		private static readonly Memoizer<Type, IEnumerable<InterfaceValidator.VisitorBase>> ConcreteTypeInterfaceVisitors = new Memoizer<Type, IEnumerable<InterfaceValidator.VisitorBase>>(new Func<Type, IEnumerable<InterfaceValidator.VisitorBase>>(InterfaceValidator.ComputeInterfaceVisitorsForObject), null);

		// Token: 0x0400037F RID: 895
		private readonly HashSetInternal<object> visited = new HashSetInternal<object>();

		// Token: 0x04000380 RID: 896
		private readonly HashSetInternal<object> visitedBad = new HashSetInternal<object>();

		// Token: 0x04000381 RID: 897
		private readonly HashSetInternal<object> danglingReferences = new HashSetInternal<object>();

		// Token: 0x04000382 RID: 898
		private readonly HashSetInternal<object> skipVisitation;

		// Token: 0x04000383 RID: 899
		private readonly bool validateDirectValueAnnotations;

		// Token: 0x04000384 RID: 900
		private readonly IEdmModel model;

		// Token: 0x0200024D RID: 589
		private abstract class VisitorBase
		{
			// Token: 0x06000F18 RID: 3864
			public abstract IEnumerable<EdmError> Visit(object item, List<object> followup, List<object> references);
		}

		// Token: 0x0200024E RID: 590
		private abstract class VisitorOfT<T> : InterfaceValidator.VisitorBase
		{
			// Token: 0x06000F1A RID: 3866 RVA: 0x0002831D File Offset: 0x0002651D
			public override IEnumerable<EdmError> Visit(object item, List<object> followup, List<object> references)
			{
				return this.VisitT((T)((object)item), followup, references);
			}

			// Token: 0x06000F1B RID: 3867
			protected abstract IEnumerable<EdmError> VisitT(T item, List<object> followup, List<object> references);
		}

		// Token: 0x0200024F RID: 591
		private sealed class VisitorOfIEdmCheckable : InterfaceValidator.VisitorOfT<IEdmCheckable>
		{
			// Token: 0x06000F1D RID: 3869 RVA: 0x00028338 File Offset: 0x00026538
			protected override IEnumerable<EdmError> VisitT(IEdmCheckable checkable, List<object> followup, List<object> references)
			{
				List<EdmError> list = new List<EdmError>();
				List<EdmError> list2 = null;
				InterfaceValidator.ProcessEnumerable<IEdmCheckable, EdmError>(checkable, checkable.Errors, "Errors", list, ref list2);
				return list2 ?? list;
			}
		}

		// Token: 0x02000250 RID: 592
		private sealed class VisitorOfIEdmElement : InterfaceValidator.VisitorOfT<IEdmElement>
		{
			// Token: 0x06000F1F RID: 3871 RVA: 0x000026B0 File Offset: 0x000008B0
			protected override IEnumerable<EdmError> VisitT(IEdmElement element, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x02000251 RID: 593
		private sealed class VisitorOfIEdmFullNamedElement : InterfaceValidator.VisitorOfT<IEdmFullNamedElement>
		{
			// Token: 0x06000F21 RID: 3873 RVA: 0x00028377 File Offset: 0x00026577
			protected override IEnumerable<EdmError> VisitT(IEdmFullNamedElement element, List<object> followup, List<object> references)
			{
				if (element.Name == null)
				{
					return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmFullNamedElement>(element, "Name") };
				}
				return null;
			}
		}

		// Token: 0x02000252 RID: 594
		private sealed class VisitorOfIEdmNamedElement : InterfaceValidator.VisitorOfT<IEdmNamedElement>
		{
			// Token: 0x06000F23 RID: 3875 RVA: 0x0002839F File Offset: 0x0002659F
			protected override IEnumerable<EdmError> VisitT(IEdmNamedElement element, List<object> followup, List<object> references)
			{
				if (element.Name == null)
				{
					return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmNamedElement>(element, "Name") };
				}
				return null;
			}
		}

		// Token: 0x02000253 RID: 595
		private sealed class VisitorOfIEdmSchemaElement : InterfaceValidator.VisitorOfT<IEdmSchemaElement>
		{
			// Token: 0x06000F25 RID: 3877 RVA: 0x000283C8 File Offset: 0x000265C8
			protected override IEnumerable<EdmError> VisitT(IEdmSchemaElement element, List<object> followup, List<object> references)
			{
				List<EdmError> list = new List<EdmError>();
				switch (element.SchemaElementKind)
				{
				case EdmSchemaElementKind.None:
					break;
				case EdmSchemaElementKind.TypeDefinition:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmSchemaElement, EdmSchemaElementKind, IEdmSchemaType>(element, element.SchemaElementKind, "SchemaElementKind"), ref list);
					break;
				case EdmSchemaElementKind.Term:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmSchemaElement, EdmSchemaElementKind, IEdmTerm>(element, element.SchemaElementKind, "SchemaElementKind"), ref list);
					break;
				case EdmSchemaElementKind.Action:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmSchemaElement, EdmSchemaElementKind, IEdmOperation>(element, element.SchemaElementKind, "SchemaElementKind"), ref list);
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmSchemaElement, EdmSchemaElementKind, IEdmAction>(element, element.SchemaElementKind, "SchemaElementKind"), ref list);
					break;
				case EdmSchemaElementKind.EntityContainer:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmSchemaElement, EdmSchemaElementKind, IEdmEntityContainer>(element, element.SchemaElementKind, "SchemaElementKind"), ref list);
					break;
				case EdmSchemaElementKind.Function:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmSchemaElement, EdmSchemaElementKind, IEdmOperation>(element, element.SchemaElementKind, "SchemaElementKind"), ref list);
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmSchemaElement, EdmSchemaElementKind, IEdmFunction>(element, element.SchemaElementKind, "SchemaElementKind"), ref list);
					break;
				default:
					InterfaceValidator.CollectErrors(InterfaceValidator.CreateEnumPropertyOutOfRangeError<IEdmSchemaElement, EdmSchemaElementKind>(element, element.SchemaElementKind, "SchemaElementKind"), ref list);
					break;
				}
				if (element.Namespace == null)
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmSchemaElement>(element, "Namespace"), ref list);
				}
				return list;
			}
		}

		// Token: 0x02000254 RID: 596
		private sealed class VisitorOfIEdmModel : InterfaceValidator.VisitorOfT<IEdmModel>
		{
			// Token: 0x06000F27 RID: 3879 RVA: 0x000284F8 File Offset: 0x000266F8
			protected override IEnumerable<EdmError> VisitT(IEdmModel model, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmModel, IEdmSchemaElement>(model, model.SchemaElements, "SchemaElements", followup, ref list);
				InterfaceValidator.ProcessEnumerable<IEdmModel, IEdmVocabularyAnnotation>(model, model.VocabularyAnnotations, "VocabularyAnnotations", followup, ref list);
				return list;
			}
		}

		// Token: 0x02000255 RID: 597
		private sealed class VisitorOfIEdmEntityContainer : InterfaceValidator.VisitorOfT<IEdmEntityContainer>
		{
			// Token: 0x06000F29 RID: 3881 RVA: 0x00028538 File Offset: 0x00026738
			protected override IEnumerable<EdmError> VisitT(IEdmEntityContainer container, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmEntityContainer, IEdmEntityContainerElement>(container, container.Elements, "Elements", followup, ref list);
				return list;
			}
		}

		// Token: 0x02000256 RID: 598
		private sealed class VisitorOfIEdmEntityContainerElement : InterfaceValidator.VisitorOfT<IEdmEntityContainerElement>
		{
			// Token: 0x06000F2B RID: 3883 RVA: 0x00028564 File Offset: 0x00026764
			protected override IEnumerable<EdmError> VisitT(IEdmEntityContainerElement element, List<object> followup, List<object> references)
			{
				EdmError edmError = null;
				switch (element.ContainerElementKind)
				{
				case EdmContainerElementKind.None:
					break;
				case EdmContainerElementKind.EntitySet:
					edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmEntityContainerElement, EdmContainerElementKind, IEdmEntitySet>(element, element.ContainerElementKind, "ContainerElementKind");
					break;
				case EdmContainerElementKind.ActionImport:
				case EdmContainerElementKind.FunctionImport:
					edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmEntityContainerElement, EdmContainerElementKind, IEdmOperationImport>(element, element.ContainerElementKind, "ContainerElementKind");
					break;
				case EdmContainerElementKind.Singleton:
					edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmEntityContainerElement, EdmContainerElementKind, IEdmSingleton>(element, element.ContainerElementKind, "ContainerElementKind");
					break;
				default:
					edmError = InterfaceValidator.CreateEnumPropertyOutOfRangeError<IEdmEntityContainerElement, EdmContainerElementKind>(element, element.ContainerElementKind, "ContainerElementKind");
					break;
				}
				if (edmError == null)
				{
					return null;
				}
				return new EdmError[] { edmError };
			}
		}

		// Token: 0x02000257 RID: 599
		private sealed class VisitorOfIEdmContainedEntitySet : InterfaceValidator.VisitorOfT<IEdmContainedEntitySet>
		{
			// Token: 0x06000F2D RID: 3885 RVA: 0x000285FC File Offset: 0x000267FC
			protected override IEnumerable<EdmError> VisitT(IEdmContainedEntitySet item, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (item.ParentNavigationSource == null)
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmContainedEntitySet>(item, "ParentNavigationSource"), ref list);
				}
				return list;
			}
		}

		// Token: 0x02000258 RID: 600
		private sealed class VisitorOfIEdmNavigationSource : InterfaceValidator.VisitorOfT<IEdmNavigationSource>
		{
			// Token: 0x06000F2F RID: 3887 RVA: 0x00028630 File Offset: 0x00026830
			protected override IEnumerable<EdmError> VisitT(IEdmNavigationSource set, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				List<IEdmNavigationPropertyBinding> list2 = new List<IEdmNavigationPropertyBinding>();
				InterfaceValidator.ProcessEnumerable<IEdmNavigationSource, IEdmNavigationPropertyBinding>(set, set.NavigationPropertyBindings, "NavigationPropertyBindings", list2, ref list);
				foreach (IEdmNavigationPropertyBinding edmNavigationPropertyBinding in list2)
				{
					if (edmNavigationPropertyBinding.NavigationProperty != null)
					{
						references.Add(edmNavigationPropertyBinding.NavigationProperty);
					}
					else
					{
						InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmNavigationPropertyBinding>(edmNavigationPropertyBinding, "NavigationProperty"), ref list);
					}
					if (edmNavigationPropertyBinding.Target != null)
					{
						references.Add(edmNavigationPropertyBinding.Target);
					}
					else
					{
						InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmNavigationPropertyBinding>(edmNavigationPropertyBinding, "Target"), ref list);
					}
				}
				return list;
			}
		}

		// Token: 0x02000259 RID: 601
		private sealed class VisitorOfIEdmEntitySetBase : InterfaceValidator.VisitorOfT<IEdmEntitySetBase>
		{
			// Token: 0x06000F31 RID: 3889 RVA: 0x000286EC File Offset: 0x000268EC
			protected override IEnumerable<EdmError> VisitT(IEdmEntitySetBase set, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (set.Type != null)
				{
					references.Add(set.Type);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmEntitySetBase>(set, "Type"), ref list);
				}
				return list;
			}
		}

		// Token: 0x0200025A RID: 602
		private sealed class VisitorOfIEdmSingleton : InterfaceValidator.VisitorOfT<IEdmSingleton>
		{
			// Token: 0x06000F33 RID: 3891 RVA: 0x0002872C File Offset: 0x0002692C
			protected override IEnumerable<EdmError> VisitT(IEdmSingleton singleton, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (singleton.Type != null)
				{
					references.Add(singleton.Type);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmSingleton>(singleton, "Type"), ref list);
				}
				return list;
			}
		}

		// Token: 0x0200025B RID: 603
		private sealed class VisitorOfIEdmTypeReference : InterfaceValidator.VisitorOfT<IEdmTypeReference>
		{
			// Token: 0x06000F35 RID: 3893 RVA: 0x0002876C File Offset: 0x0002696C
			protected override IEnumerable<EdmError> VisitT(IEdmTypeReference type, List<object> followup, List<object> references)
			{
				if (type.Definition != null)
				{
					if (type.Definition is IEdmSchemaType)
					{
						references.Add(type.Definition);
					}
					else
					{
						followup.Add(type.Definition);
					}
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmTypeReference>(type, "Definition") };
			}
		}

		// Token: 0x0200025C RID: 604
		private sealed class VisitorOfIEdmType : InterfaceValidator.VisitorOfT<IEdmType>
		{
			// Token: 0x06000F37 RID: 3895 RVA: 0x000287C8 File Offset: 0x000269C8
			protected override IEnumerable<EdmError> VisitT(IEdmType type, List<object> followup, List<object> references)
			{
				EdmError edmError = null;
				switch (type.TypeKind)
				{
				case EdmTypeKind.None:
					break;
				case EdmTypeKind.Primitive:
					edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmType, EdmTypeKind, IEdmPrimitiveType>(type, type.TypeKind, "TypeKind");
					break;
				case EdmTypeKind.Entity:
					edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmType, EdmTypeKind, IEdmEntityType>(type, type.TypeKind, "TypeKind");
					break;
				case EdmTypeKind.Complex:
					edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmType, EdmTypeKind, IEdmComplexType>(type, type.TypeKind, "TypeKind");
					break;
				case EdmTypeKind.Collection:
					edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmType, EdmTypeKind, IEdmCollectionType>(type, type.TypeKind, "TypeKind");
					break;
				case EdmTypeKind.EntityReference:
					edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmType, EdmTypeKind, IEdmEntityReferenceType>(type, type.TypeKind, "TypeKind");
					break;
				case EdmTypeKind.Enum:
					edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmType, EdmTypeKind, IEdmEnumType>(type, type.TypeKind, "TypeKind");
					break;
				case EdmTypeKind.TypeDefinition:
					edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmType, EdmTypeKind, IEdmTypeDefinition>(type, type.TypeKind, "TypeKind");
					break;
				default:
					edmError = InterfaceValidator.CreateInterfaceKindValueUnexpectedError<IEdmType, EdmTypeKind>(type, type.TypeKind, "TypeKind");
					break;
				}
				if (edmError == null)
				{
					return null;
				}
				return new EdmError[] { edmError };
			}
		}

		// Token: 0x0200025D RID: 605
		private sealed class VisitorOfIEdmPrimitiveType : InterfaceValidator.VisitorOfT<IEdmPrimitiveType>
		{
			// Token: 0x06000F39 RID: 3897 RVA: 0x000288C1 File Offset: 0x00026AC1
			protected override IEnumerable<EdmError> VisitT(IEdmPrimitiveType type, List<object> followup, List<object> references)
			{
				if (!InterfaceValidator.IsCheckableBad(type) && (type.PrimitiveKind < EdmPrimitiveTypeKind.None || type.PrimitiveKind > EdmPrimitiveTypeKind.GeometryMultiPoint))
				{
					return new EdmError[] { InterfaceValidator.CreateInterfaceKindValueUnexpectedError<IEdmPrimitiveType, EdmPrimitiveTypeKind>(type, type.PrimitiveKind, "PrimitiveKind") };
				}
				return null;
			}
		}

		// Token: 0x0200025E RID: 606
		private sealed class VisitorOfIEdmStructuredType : InterfaceValidator.VisitorOfT<IEdmStructuredType>
		{
			// Token: 0x06000F3B RID: 3899 RVA: 0x00028904 File Offset: 0x00026B04
			protected override IEnumerable<EdmError> VisitT(IEdmStructuredType type, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmStructuredType, IEdmProperty>(type, type.DeclaredProperties, "DeclaredProperties", followup, ref list);
				if (type.BaseType != null)
				{
					HashSetInternal<IEdmStructuredType> hashSetInternal = new HashSetInternal<IEdmStructuredType>();
					hashSetInternal.Add(type);
					for (IEdmStructuredType edmStructuredType = type.BaseType; edmStructuredType != null; edmStructuredType = edmStructuredType.BaseType)
					{
						if (hashSetInternal.Contains(edmStructuredType))
						{
							IEdmSchemaType edmSchemaType = type as IEdmSchemaType;
							string text = ((edmSchemaType != null) ? edmSchemaType.FullName() : typeof(Type).Name);
							InterfaceValidator.CollectErrors(new EdmError(InterfaceValidator.GetLocation(type), EdmErrorCode.InterfaceCriticalCycleInTypeHierarchy, Strings.EdmModel_Validator_Syntactic_InterfaceCriticalCycleInTypeHierarchy(text)), ref list);
							break;
						}
					}
					references.Add(type.BaseType);
				}
				return list;
			}
		}

		// Token: 0x0200025F RID: 607
		private sealed class VisitorOfIEdmEntityType : InterfaceValidator.VisitorOfT<IEdmEntityType>
		{
			// Token: 0x06000F3D RID: 3901 RVA: 0x000289B0 File Offset: 0x00026BB0
			protected override IEnumerable<EdmError> VisitT(IEdmEntityType type, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (type.DeclaredKey != null)
				{
					InterfaceValidator.ProcessEnumerable<IEdmEntityType, IEdmStructuralProperty>(type, type.DeclaredKey, "DeclaredKey", references, ref list);
				}
				return list;
			}
		}

		// Token: 0x02000260 RID: 608
		private sealed class VisitorOfIEdmEntityReferenceType : InterfaceValidator.VisitorOfT<IEdmEntityReferenceType>
		{
			// Token: 0x06000F3F RID: 3903 RVA: 0x000289E4 File Offset: 0x00026BE4
			protected override IEnumerable<EdmError> VisitT(IEdmEntityReferenceType type, List<object> followup, List<object> references)
			{
				if (type.EntityType != null)
				{
					references.Add(type.EntityType);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmEntityReferenceType>(type, "EntityType") };
			}
		}

		// Token: 0x02000261 RID: 609
		private sealed class VisitorOfIEdmUntypedType : InterfaceValidator.VisitorOfT<IEdmUntypedType>
		{
			// Token: 0x06000F41 RID: 3905 RVA: 0x000026B0 File Offset: 0x000008B0
			protected override IEnumerable<EdmError> VisitT(IEdmUntypedType type, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x02000262 RID: 610
		private sealed class VisitorOfIEdmPathType : InterfaceValidator.VisitorOfT<IEdmPathType>
		{
			// Token: 0x06000F43 RID: 3907 RVA: 0x000026B0 File Offset: 0x000008B0
			protected override IEnumerable<EdmError> VisitT(IEdmPathType type, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x02000263 RID: 611
		private sealed class VisitorOfIEdmEnumType : InterfaceValidator.VisitorOfT<IEdmEnumType>
		{
			// Token: 0x06000F45 RID: 3909 RVA: 0x00028A28 File Offset: 0x00026C28
			protected override IEnumerable<EdmError> VisitT(IEdmEnumType type, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmEnumType, IEdmEnumMember>(type, type.Members, "Members", followup, ref list);
				if (type.UnderlyingType != null)
				{
					references.Add(type.UnderlyingType);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmEnumType>(type, "UnderlyingType"), ref list);
				}
				return list;
			}
		}

		// Token: 0x02000264 RID: 612
		private sealed class VisitorOfIEdmTypeDefinition : InterfaceValidator.VisitorOfT<IEdmTypeDefinition>
		{
			// Token: 0x06000F47 RID: 3911 RVA: 0x00028A7C File Offset: 0x00026C7C
			protected override IEnumerable<EdmError> VisitT(IEdmTypeDefinition type, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (type.UnderlyingType != null)
				{
					references.Add(type.UnderlyingType);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmTypeDefinition>(type, "UnderlyingType"), ref list);
				}
				return list;
			}
		}

		// Token: 0x02000265 RID: 613
		private sealed class VisitorOfIEdmTerm : InterfaceValidator.VisitorOfT<IEdmTerm>
		{
			// Token: 0x06000F49 RID: 3913 RVA: 0x00028ABC File Offset: 0x00026CBC
			protected override IEnumerable<EdmError> VisitT(IEdmTerm term, List<object> followup, List<object> references)
			{
				if (term.Type != null)
				{
					followup.Add(term.Type);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmTerm>(term, "Type") };
			}
		}

		// Token: 0x02000266 RID: 614
		private sealed class VisitorOfIEdmCollectionType : InterfaceValidator.VisitorOfT<IEdmCollectionType>
		{
			// Token: 0x06000F4B RID: 3915 RVA: 0x00028AF0 File Offset: 0x00026CF0
			protected override IEnumerable<EdmError> VisitT(IEdmCollectionType type, List<object> followup, List<object> references)
			{
				if (type.ElementType != null)
				{
					followup.Add(type.ElementType);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmCollectionType>(type, "ElementType") };
			}
		}

		// Token: 0x02000267 RID: 615
		private sealed class VisitorOfIEdmProperty : InterfaceValidator.VisitorOfT<IEdmProperty>
		{
			// Token: 0x06000F4D RID: 3917 RVA: 0x00028B24 File Offset: 0x00026D24
			protected override IEnumerable<EdmError> VisitT(IEdmProperty property, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				switch (property.PropertyKind)
				{
				case EdmPropertyKind.None:
					break;
				case EdmPropertyKind.Structural:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmProperty, EdmPropertyKind, IEdmStructuralProperty>(property, property.PropertyKind, "PropertyKind"), ref list);
					break;
				case EdmPropertyKind.Navigation:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmProperty, EdmPropertyKind, IEdmNavigationProperty>(property, property.PropertyKind, "PropertyKind"), ref list);
					break;
				default:
					InterfaceValidator.CollectErrors(InterfaceValidator.CreateInterfaceKindValueUnexpectedError<IEdmProperty, EdmPropertyKind>(property, property.PropertyKind, "PropertyKind"), ref list);
					break;
				}
				if (property.Type != null)
				{
					followup.Add(property.Type);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmProperty>(property, "Type"), ref list);
				}
				if (property.DeclaringType != null)
				{
					references.Add(property.DeclaringType);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmProperty>(property, "DeclaringType"), ref list);
				}
				return list;
			}
		}

		// Token: 0x02000268 RID: 616
		private sealed class VisitorOfIEdmStructuralProperty : InterfaceValidator.VisitorOfT<IEdmStructuralProperty>
		{
			// Token: 0x06000F4F RID: 3919 RVA: 0x000026B0 File Offset: 0x000008B0
			protected override IEnumerable<EdmError> VisitT(IEdmStructuralProperty property, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x02000269 RID: 617
		private sealed class VisitorOfIEdmNavigationProperty : InterfaceValidator.VisitorOfT<IEdmNavigationProperty>
		{
			// Token: 0x06000F51 RID: 3921 RVA: 0x00028BFC File Offset: 0x00026DFC
			protected override IEnumerable<EdmError> VisitT(IEdmNavigationProperty property, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				followup.Add(property.Type);
				if (property.Partner != null)
				{
					followup.Add(property.Partner);
					if (!(property.Partner is BadNavigationProperty) && ((property.Partner.Partner != null && property.Partner.Partner != property) || (property.Partner == property && ValidationHelper.ComputeNavigationPropertyTarget(property) != property.DeclaringType)))
					{
						InterfaceValidator.CollectErrors(new EdmError(InterfaceValidator.GetLocation(property), EdmErrorCode.InterfaceCriticalNavigationPartnerInvalid, Strings.EdmModel_Validator_Syntactic_NavigationPartnerInvalid(property.Name)), ref list);
					}
				}
				if (property.ReferentialConstraint != null)
				{
					followup.Add(property.ReferentialConstraint);
				}
				if (property.OnDelete < EdmOnDeleteAction.None || property.OnDelete > EdmOnDeleteAction.Cascade)
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreateEnumPropertyOutOfRangeError<IEdmNavigationProperty, EdmOnDeleteAction>(property, property.OnDelete, "OnDelete"), ref list);
				}
				return list;
			}
		}

		// Token: 0x0200026A RID: 618
		private sealed class VisitorOfIEdmReferentialConstraint : InterfaceValidator.VisitorOfT<IEdmReferentialConstraint>
		{
			// Token: 0x06000F53 RID: 3923 RVA: 0x00028CD0 File Offset: 0x00026ED0
			protected override IEnumerable<EdmError> VisitT(IEdmReferentialConstraint member, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (member.PropertyPairs == null)
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmReferentialConstraint>(member, "PropertyPairs"), ref list);
				}
				else
				{
					foreach (EdmReferentialConstraintPropertyPair edmReferentialConstraintPropertyPair in member.PropertyPairs)
					{
						if (edmReferentialConstraintPropertyPair == null)
						{
							InterfaceValidator.CollectErrors(new EdmError(InterfaceValidator.GetLocation(member), EdmErrorCode.InterfaceCriticalEnumerableMustNotHaveNullElements, Strings.EdmModel_Validator_Syntactic_EnumerableMustNotHaveNullElements(typeof(IEdmReferentialConstraint).Name, "PropertyPairs")), ref list);
							break;
						}
						followup.Add(edmReferentialConstraintPropertyPair.PrincipalProperty);
						followup.Add(edmReferentialConstraintPropertyPair.DependentProperty);
					}
				}
				return list;
			}
		}

		// Token: 0x0200026B RID: 619
		private sealed class VisitorOfIEdmEnumMember : InterfaceValidator.VisitorOfT<IEdmEnumMember>
		{
			// Token: 0x06000F55 RID: 3925 RVA: 0x00028D88 File Offset: 0x00026F88
			protected override IEnumerable<EdmError> VisitT(IEdmEnumMember member, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (member.DeclaringType != null)
				{
					references.Add(member.DeclaringType);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmEnumMember>(member, "DeclaringType"), ref list);
				}
				if (member.Value != null)
				{
					followup.Add(member.Value);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmEnumMember>(member, "Value"), ref list);
				}
				return list;
			}
		}

		// Token: 0x0200026C RID: 620
		private sealed class VisitorOfIEdmOperation : InterfaceValidator.VisitorOfT<IEdmOperation>
		{
			// Token: 0x06000F57 RID: 3927 RVA: 0x00028DF0 File Offset: 0x00026FF0
			protected override IEnumerable<EdmError> VisitT(IEdmOperation operation, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmOperation, IEdmOperationParameter>(operation, operation.Parameters, "Parameters", followup, ref list);
				if (operation.ReturnType != null)
				{
					followup.Add(operation.ReturnType);
				}
				return list;
			}
		}

		// Token: 0x0200026D RID: 621
		private sealed class VisitorOfIEdmAction : InterfaceValidator.VisitorOfT<IEdmAction>
		{
			// Token: 0x06000F59 RID: 3929 RVA: 0x000026B0 File Offset: 0x000008B0
			protected override IEnumerable<EdmError> VisitT(IEdmAction operation, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x0200026E RID: 622
		private sealed class VisitorOfIEdmFunction : InterfaceValidator.VisitorOfT<IEdmFunction>
		{
			// Token: 0x06000F5B RID: 3931 RVA: 0x000026B0 File Offset: 0x000008B0
			protected override IEnumerable<EdmError> VisitT(IEdmFunction operation, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x0200026F RID: 623
		private sealed class VisitorOfIEdmOperationImport : InterfaceValidator.VisitorOfT<IEdmOperationImport>
		{
			// Token: 0x06000F5D RID: 3933 RVA: 0x00028E40 File Offset: 0x00027040
			protected override IEnumerable<EdmError> VisitT(IEdmOperationImport functionImport, List<object> followup, List<object> references)
			{
				if (functionImport.EntitySet != null)
				{
					followup.Add(functionImport.EntitySet);
				}
				followup.Add(functionImport.Operation);
				return null;
			}
		}

		// Token: 0x02000270 RID: 624
		private sealed class VisitorOfIEdmActionImport : InterfaceValidator.VisitorOfT<IEdmActionImport>
		{
			// Token: 0x06000F5F RID: 3935 RVA: 0x000026B0 File Offset: 0x000008B0
			protected override IEnumerable<EdmError> VisitT(IEdmActionImport actionImport, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x02000271 RID: 625
		private sealed class VisitorOfIEdmFunctionImport : InterfaceValidator.VisitorOfT<IEdmFunctionImport>
		{
			// Token: 0x06000F61 RID: 3937 RVA: 0x000026B0 File Offset: 0x000008B0
			protected override IEnumerable<EdmError> VisitT(IEdmFunctionImport functionImport, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x02000272 RID: 626
		private sealed class VisitorOfIEdmOperationParameter : InterfaceValidator.VisitorOfT<IEdmOperationParameter>
		{
			// Token: 0x06000F63 RID: 3939 RVA: 0x00028E7C File Offset: 0x0002707C
			protected override IEnumerable<EdmError> VisitT(IEdmOperationParameter parameter, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (parameter.Type != null)
				{
					followup.Add(parameter.Type);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmOperationParameter>(parameter, "Type"), ref list);
				}
				if (parameter.DeclaringOperation != null)
				{
					references.Add(parameter.DeclaringOperation);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmOperationParameter>(parameter, "DeclaringFunction"), ref list);
				}
				return list;
			}
		}

		// Token: 0x02000273 RID: 627
		private sealed class VisitorOfIEdmOptionalParameter : InterfaceValidator.VisitorOfT<IEdmOptionalParameter>
		{
			// Token: 0x06000F65 RID: 3941 RVA: 0x000026B0 File Offset: 0x000008B0
			protected override IEnumerable<EdmError> VisitT(IEdmOptionalParameter parameter, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x02000274 RID: 628
		private sealed class VisitorOfIEdmOperationReturn : InterfaceValidator.VisitorOfT<IEdmOperationReturn>
		{
			// Token: 0x06000F67 RID: 3943 RVA: 0x00028EEC File Offset: 0x000270EC
			protected override IEnumerable<EdmError> VisitT(IEdmOperationReturn operationReturn, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (operationReturn.Type != null)
				{
					followup.Add(operationReturn.Type);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmOperationReturn>(operationReturn, "Type"), ref list);
				}
				if (operationReturn.DeclaringOperation != null)
				{
					references.Add(operationReturn.DeclaringOperation);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmOperationReturn>(operationReturn, "DeclaringOperation"), ref list);
				}
				return list;
			}
		}

		// Token: 0x02000275 RID: 629
		private sealed class VisitorOfIEdmCollectionTypeReference : InterfaceValidator.VisitorOfT<IEdmCollectionTypeReference>
		{
			// Token: 0x06000F69 RID: 3945 RVA: 0x00028F54 File Offset: 0x00027154
			protected override IEnumerable<EdmError> VisitT(IEdmCollectionTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Collection)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmCollectionTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000276 RID: 630
		private sealed class VisitorOfIEdmEntityReferenceTypeReference : InterfaceValidator.VisitorOfT<IEdmEntityReferenceTypeReference>
		{
			// Token: 0x06000F6B RID: 3947 RVA: 0x00028F85 File Offset: 0x00027185
			protected override IEnumerable<EdmError> VisitT(IEdmEntityReferenceTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.EntityReference)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmEntityReferenceTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000277 RID: 631
		private sealed class VisitorOfIEdmStructuredTypeReference : InterfaceValidator.VisitorOfT<IEdmStructuredTypeReference>
		{
			// Token: 0x06000F6D RID: 3949 RVA: 0x00028FB6 File Offset: 0x000271B6
			protected override IEnumerable<EdmError> VisitT(IEdmStructuredTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind.IsStructured())
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmStructuredTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000278 RID: 632
		private sealed class VisitorOfIEdmEntityTypeReference : InterfaceValidator.VisitorOfT<IEdmEntityTypeReference>
		{
			// Token: 0x06000F6F RID: 3951 RVA: 0x00028FEB File Offset: 0x000271EB
			protected override IEnumerable<EdmError> VisitT(IEdmEntityTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Entity)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmEntityTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000279 RID: 633
		private sealed class VisitorOfIEdmComplexTypeReference : InterfaceValidator.VisitorOfT<IEdmComplexTypeReference>
		{
			// Token: 0x06000F71 RID: 3953 RVA: 0x0002901C File Offset: 0x0002721C
			protected override IEnumerable<EdmError> VisitT(IEdmComplexTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Complex)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmComplexTypeReference>(typeRef) };
			}
		}

		// Token: 0x0200027A RID: 634
		private sealed class VisitorOfIEdmUntypedTypeReference : InterfaceValidator.VisitorOfT<IEdmUntypedTypeReference>
		{
			// Token: 0x06000F73 RID: 3955 RVA: 0x0002904D File Offset: 0x0002724D
			protected override IEnumerable<EdmError> VisitT(IEdmUntypedTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Untyped)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmUntypedTypeReference>(typeRef) };
			}
		}

		// Token: 0x0200027B RID: 635
		private sealed class VisitorOfIEdmPathTypeReference : InterfaceValidator.VisitorOfT<IEdmPathTypeReference>
		{
			// Token: 0x06000F75 RID: 3957 RVA: 0x0002907E File Offset: 0x0002727E
			protected override IEnumerable<EdmError> VisitT(IEdmPathTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Path)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmPathTypeReference>(typeRef) };
			}
		}

		// Token: 0x0200027C RID: 636
		private sealed class VisitorOfIEdmEnumTypeReference : InterfaceValidator.VisitorOfT<IEdmEnumTypeReference>
		{
			// Token: 0x06000F77 RID: 3959 RVA: 0x000290B0 File Offset: 0x000272B0
			protected override IEnumerable<EdmError> VisitT(IEdmEnumTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Enum)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmEnumTypeReference>(typeRef) };
			}
		}

		// Token: 0x0200027D RID: 637
		private sealed class VisitorOfIEdmTypeDefinitionReference : InterfaceValidator.VisitorOfT<IEdmTypeDefinitionReference>
		{
			// Token: 0x06000F79 RID: 3961 RVA: 0x000290E1 File Offset: 0x000272E1
			protected override IEnumerable<EdmError> VisitT(IEdmTypeDefinitionReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.TypeDefinition)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmTypeDefinitionReference>(typeRef) };
			}
		}

		// Token: 0x0200027E RID: 638
		private sealed class VisitorOfIEdmPrimitiveTypeReference : InterfaceValidator.VisitorOfT<IEdmPrimitiveTypeReference>
		{
			// Token: 0x06000F7B RID: 3963 RVA: 0x00029112 File Offset: 0x00027312
			protected override IEnumerable<EdmError> VisitT(IEdmPrimitiveTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Primitive)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmPrimitiveTypeReference>(typeRef) };
			}
		}

		// Token: 0x0200027F RID: 639
		private sealed class VisitorOfIEdmBinaryTypeReference : InterfaceValidator.VisitorOfT<IEdmBinaryTypeReference>
		{
			// Token: 0x06000F7D RID: 3965 RVA: 0x00029144 File Offset: 0x00027344
			protected override IEnumerable<EdmError> VisitT(IEdmBinaryTypeReference typeRef, List<object> followup, List<object> references)
			{
				IEdmPrimitiveType edmPrimitiveType = typeRef.Definition as IEdmPrimitiveType;
				if (edmPrimitiveType == null || edmPrimitiveType.PrimitiveKind == EdmPrimitiveTypeKind.Binary)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePrimitiveTypeRefInterfaceTypeKindValueMismatchError<IEdmBinaryTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000280 RID: 640
		private sealed class VisitorOfIEdmDecimalTypeReference : InterfaceValidator.VisitorOfT<IEdmDecimalTypeReference>
		{
			// Token: 0x06000F7F RID: 3967 RVA: 0x00029184 File Offset: 0x00027384
			protected override IEnumerable<EdmError> VisitT(IEdmDecimalTypeReference typeRef, List<object> followup, List<object> references)
			{
				IEdmPrimitiveType edmPrimitiveType = typeRef.Definition as IEdmPrimitiveType;
				if (edmPrimitiveType == null || edmPrimitiveType.PrimitiveKind == EdmPrimitiveTypeKind.Decimal)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePrimitiveTypeRefInterfaceTypeKindValueMismatchError<IEdmDecimalTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000281 RID: 641
		private sealed class VisitorOfIEdmStringTypeReference : InterfaceValidator.VisitorOfT<IEdmStringTypeReference>
		{
			// Token: 0x06000F81 RID: 3969 RVA: 0x000291C4 File Offset: 0x000273C4
			protected override IEnumerable<EdmError> VisitT(IEdmStringTypeReference typeRef, List<object> followup, List<object> references)
			{
				IEdmPrimitiveType edmPrimitiveType = typeRef.Definition as IEdmPrimitiveType;
				if (edmPrimitiveType == null || edmPrimitiveType.PrimitiveKind == EdmPrimitiveTypeKind.String)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePrimitiveTypeRefInterfaceTypeKindValueMismatchError<IEdmStringTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000282 RID: 642
		private sealed class VisitorOfIEdmTemporalTypeReference : InterfaceValidator.VisitorOfT<IEdmTemporalTypeReference>
		{
			// Token: 0x06000F83 RID: 3971 RVA: 0x00029204 File Offset: 0x00027404
			protected override IEnumerable<EdmError> VisitT(IEdmTemporalTypeReference typeRef, List<object> followup, List<object> references)
			{
				IEdmPrimitiveType edmPrimitiveType = typeRef.Definition as IEdmPrimitiveType;
				if (edmPrimitiveType == null || edmPrimitiveType.PrimitiveKind.IsTemporal())
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePrimitiveTypeRefInterfaceTypeKindValueMismatchError<IEdmTemporalTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000283 RID: 643
		private sealed class VisitorOfIEdmSpatialTypeReference : InterfaceValidator.VisitorOfT<IEdmSpatialTypeReference>
		{
			// Token: 0x06000F85 RID: 3973 RVA: 0x00029248 File Offset: 0x00027448
			protected override IEnumerable<EdmError> VisitT(IEdmSpatialTypeReference typeRef, List<object> followup, List<object> references)
			{
				IEdmPrimitiveType edmPrimitiveType = typeRef.Definition as IEdmPrimitiveType;
				if (edmPrimitiveType == null || edmPrimitiveType.PrimitiveKind.IsSpatial())
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePrimitiveTypeRefInterfaceTypeKindValueMismatchError<IEdmSpatialTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000284 RID: 644
		private sealed class VisitorOfIEdmReference : InterfaceValidator.VisitorOfT<IEdmReference>
		{
			// Token: 0x06000F87 RID: 3975 RVA: 0x0002928A File Offset: 0x0002748A
			protected override IEnumerable<EdmError> VisitT(IEdmReference edmReference, List<object> followup, List<object> references)
			{
				if (edmReference.Includes.Any<IEdmInclude>() || !edmReference.IncludeAnnotations.Any<IEdmIncludeAnnotations>())
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmReference>(edmReference, "Includes/IncludeAnnotations") };
			}
		}

		// Token: 0x02000285 RID: 645
		private sealed class VisitorOfIEdmInclude : InterfaceValidator.VisitorOfT<IEdmInclude>
		{
			// Token: 0x06000F89 RID: 3977 RVA: 0x000292C4 File Offset: 0x000274C4
			protected override IEnumerable<EdmError> VisitT(IEdmInclude edmInclude, List<object> followup, List<object> references)
			{
				if (!string.IsNullOrEmpty(edmInclude.Namespace))
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmInclude>(edmInclude, "Namespace") };
			}
		}

		// Token: 0x02000286 RID: 646
		private sealed class VisitorOfIEdmIncludeAnnotations : InterfaceValidator.VisitorOfT<IEdmIncludeAnnotations>
		{
			// Token: 0x06000F8B RID: 3979 RVA: 0x000292F1 File Offset: 0x000274F1
			protected override IEnumerable<EdmError> VisitT(IEdmIncludeAnnotations edmIncludeAnnotations, List<object> followup, List<object> references)
			{
				if (!string.IsNullOrEmpty(edmIncludeAnnotations.TermNamespace))
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmIncludeAnnotations>(edmIncludeAnnotations, "TermNamespace") };
			}
		}

		// Token: 0x02000287 RID: 647
		private sealed class VisitorOfIEdmExpression : InterfaceValidator.VisitorOfT<IEdmExpression>
		{
			// Token: 0x06000F8D RID: 3981 RVA: 0x00029320 File Offset: 0x00027520
			protected override IEnumerable<EdmError> VisitT(IEdmExpression expression, List<object> followup, List<object> references)
			{
				EdmError edmError = null;
				if (!InterfaceValidator.IsCheckableBad(expression))
				{
					switch (expression.ExpressionKind)
					{
					case EdmExpressionKind.BinaryConstant:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmBinaryConstantExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.BooleanConstant:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmBooleanConstantExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.DateTimeOffsetConstant:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmDateTimeOffsetConstantExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.DecimalConstant:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmDecimalConstantExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.FloatingConstant:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmFloatingConstantExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.GuidConstant:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmGuidConstantExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.IntegerConstant:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmIntegerConstantExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.StringConstant:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmStringConstantExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.DurationConstant:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmDurationConstantExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.Null:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmNullExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.Record:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmRecordExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.Collection:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmCollectionExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.Path:
					case EdmExpressionKind.PropertyPath:
					case EdmExpressionKind.NavigationPropertyPath:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmPathExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.If:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmIfExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.Cast:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmCastExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.IsType:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmIsTypeExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.FunctionApplication:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmApplyExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.LabeledExpressionReference:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmLabeledExpressionReferenceExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.Labeled:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmLabeledExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.DateConstant:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmDateConstantExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.TimeOfDayConstant:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmTimeOfDayConstantExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.EnumMember:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmEnumMemberExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					default:
						edmError = InterfaceValidator.CreateInterfaceKindValueUnexpectedError<IEdmExpression, EdmExpressionKind>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					}
				}
				if (edmError == null)
				{
					return null;
				}
				return new EdmError[] { edmError };
			}
		}

		// Token: 0x02000288 RID: 648
		private sealed class VisitorOfIEdmRecordExpression : InterfaceValidator.VisitorOfT<IEdmRecordExpression>
		{
			// Token: 0x06000F8F RID: 3983 RVA: 0x000295C0 File Offset: 0x000277C0
			protected override IEnumerable<EdmError> VisitT(IEdmRecordExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmRecordExpression, IEdmPropertyConstructor>(expression, expression.Properties, "Properties", followup, ref list);
				if (expression.DeclaredType != null)
				{
					followup.Add(expression.DeclaredType);
				}
				return list;
			}
		}

		// Token: 0x02000289 RID: 649
		private sealed class VisitorOfIEdmPropertyConstructor : InterfaceValidator.VisitorOfT<IEdmPropertyConstructor>
		{
			// Token: 0x06000F91 RID: 3985 RVA: 0x00029600 File Offset: 0x00027800
			protected override IEnumerable<EdmError> VisitT(IEdmPropertyConstructor expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (expression.Name == null)
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmPropertyConstructor>(expression, "Name"), ref list);
				}
				if (expression.Value != null)
				{
					followup.Add(expression.Value);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmPropertyConstructor>(expression, "Value"), ref list);
				}
				return list;
			}
		}

		// Token: 0x0200028A RID: 650
		private sealed class VisitorOfIEdmCollectionExpression : InterfaceValidator.VisitorOfT<IEdmCollectionExpression>
		{
			// Token: 0x06000F93 RID: 3987 RVA: 0x0002965C File Offset: 0x0002785C
			protected override IEnumerable<EdmError> VisitT(IEdmCollectionExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmCollectionExpression, IEdmExpression>(expression, expression.Elements, "Elements", followup, ref list);
				if (expression.DeclaredType != null)
				{
					followup.Add(expression.DeclaredType);
				}
				return list;
			}
		}

		// Token: 0x0200028B RID: 651
		private sealed class VisitorOfIEdmLabeledElement : InterfaceValidator.VisitorOfT<IEdmLabeledExpression>
		{
			// Token: 0x06000F95 RID: 3989 RVA: 0x0002969C File Offset: 0x0002789C
			protected override IEnumerable<EdmError> VisitT(IEdmLabeledExpression expression, List<object> followup, List<object> references)
			{
				if (expression.Expression != null)
				{
					followup.Add(expression.Expression);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmLabeledExpression>(expression, "Expression") };
			}
		}

		// Token: 0x0200028C RID: 652
		private sealed class VisitorOfIEdmPathExpression : InterfaceValidator.VisitorOfT<IEdmPathExpression>
		{
			// Token: 0x06000F97 RID: 3991 RVA: 0x000296D0 File Offset: 0x000278D0
			protected override IEnumerable<EdmError> VisitT(IEdmPathExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				List<string> list2 = new List<string>();
				InterfaceValidator.ProcessEnumerable<IEdmPathExpression, string>(expression, expression.PathSegments, "Path", list2, ref list);
				return list;
			}
		}

		// Token: 0x0200028D RID: 653
		private sealed class VistorOfIEdmEnumMemberExpression : InterfaceValidator.VisitorOfT<IEdmEnumMemberExpression>
		{
			// Token: 0x06000F99 RID: 3993 RVA: 0x00029704 File Offset: 0x00027904
			protected override IEnumerable<EdmError> VisitT(IEdmEnumMemberExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmEnumMemberExpression, IEdmEnumMember>(expression, expression.EnumMembers, "EnumMembers", followup, ref list);
				return list;
			}
		}

		// Token: 0x0200028E RID: 654
		private sealed class VistorOfIEdmIfExpression : InterfaceValidator.VisitorOfT<IEdmIfExpression>
		{
			// Token: 0x06000F9B RID: 3995 RVA: 0x00029730 File Offset: 0x00027930
			protected override IEnumerable<EdmError> VisitT(IEdmIfExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (expression.TestExpression != null)
				{
					followup.Add(expression.TestExpression);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmIfExpression>(expression, "TestExpression"), ref list);
				}
				if (expression.TrueExpression != null)
				{
					followup.Add(expression.TrueExpression);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmIfExpression>(expression, "TrueExpression"), ref list);
				}
				if (expression.FalseExpression != null)
				{
					followup.Add(expression.FalseExpression);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmIfExpression>(expression, "FalseExpression"), ref list);
				}
				return list;
			}
		}

		// Token: 0x0200028F RID: 655
		private sealed class VistorOfIEdmCastExpression : InterfaceValidator.VisitorOfT<IEdmCastExpression>
		{
			// Token: 0x06000F9D RID: 3997 RVA: 0x000297C0 File Offset: 0x000279C0
			protected override IEnumerable<EdmError> VisitT(IEdmCastExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (expression.Operand != null)
				{
					followup.Add(expression.Operand);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmCastExpression>(expression, "Operand"), ref list);
				}
				if (expression.Type != null)
				{
					followup.Add(expression.Type);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmCastExpression>(expression, "Type"), ref list);
				}
				return list;
			}
		}

		// Token: 0x02000290 RID: 656
		private sealed class VistorOfIEdmIsTypeExpression : InterfaceValidator.VisitorOfT<IEdmIsTypeExpression>
		{
			// Token: 0x06000F9F RID: 3999 RVA: 0x00029828 File Offset: 0x00027A28
			protected override IEnumerable<EdmError> VisitT(IEdmIsTypeExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (expression.Operand != null)
				{
					followup.Add(expression.Operand);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmIsTypeExpression>(expression, "Operand"), ref list);
				}
				if (expression.Type != null)
				{
					followup.Add(expression.Type);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmIsTypeExpression>(expression, "Type"), ref list);
				}
				return list;
			}
		}

		// Token: 0x02000291 RID: 657
		private sealed class VistorOfIEdmFunctionApplicationExpression : InterfaceValidator.VisitorOfT<IEdmApplyExpression>
		{
			// Token: 0x06000FA1 RID: 4001 RVA: 0x00029890 File Offset: 0x00027A90
			protected override IEnumerable<EdmError> VisitT(IEdmApplyExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (expression.AppliedFunction != null)
				{
					followup.Add(expression.AppliedFunction);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmApplyExpression>(expression, "AppliedFunction"), ref list);
				}
				InterfaceValidator.ProcessEnumerable<IEdmApplyExpression, IEdmExpression>(expression, expression.Arguments, "Arguments", followup, ref list);
				return list;
			}
		}

		// Token: 0x02000292 RID: 658
		private sealed class VistorOfIEdmLabeledElementReferenceExpression : InterfaceValidator.VisitorOfT<IEdmLabeledExpressionReferenceExpression>
		{
			// Token: 0x06000FA3 RID: 4003 RVA: 0x000298E4 File Offset: 0x00027AE4
			protected override IEnumerable<EdmError> VisitT(IEdmLabeledExpressionReferenceExpression expression, List<object> followup, List<object> references)
			{
				if (expression.ReferencedLabeledExpression != null)
				{
					references.Add(expression.ReferencedLabeledExpression);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmLabeledExpressionReferenceExpression>(expression, "ReferencedLabeledExpression") };
			}
		}

		// Token: 0x02000293 RID: 659
		private sealed class VisitorOfIEdmValue : InterfaceValidator.VisitorOfT<IEdmValue>
		{
			// Token: 0x06000FA5 RID: 4005 RVA: 0x00029918 File Offset: 0x00027B18
			protected override IEnumerable<EdmError> VisitT(IEdmValue value, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (value.Type != null)
				{
					followup.Add(value.Type);
				}
				switch (value.ValueKind)
				{
				case EdmValueKind.None:
					break;
				case EdmValueKind.Binary:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmBinaryValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.Boolean:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmBooleanValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.Collection:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmCollectionValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.DateTimeOffset:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmDateTimeOffsetValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.Decimal:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmDecimalValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.Enum:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmEnumValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.Floating:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmFloatingValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.Guid:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmGuidValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.Integer:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmIntegerValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.Null:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmNullValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.String:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmStringValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.Structured:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmStructuredValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.Duration:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmDurationValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.Date:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmDateValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				case EdmValueKind.TimeOfDay:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmValue, EdmValueKind, IEdmTimeOfDayValue>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				default:
					InterfaceValidator.CollectErrors(InterfaceValidator.CreateInterfaceKindValueUnexpectedError<IEdmValue, EdmValueKind>(value, value.ValueKind, "ValueKind"), ref list);
					break;
				}
				return list;
			}
		}

		// Token: 0x02000294 RID: 660
		private sealed class VisitorOfIEdmDelayedValue : InterfaceValidator.VisitorOfT<IEdmDelayedValue>
		{
			// Token: 0x06000FA7 RID: 4007 RVA: 0x00029B55 File Offset: 0x00027D55
			protected override IEnumerable<EdmError> VisitT(IEdmDelayedValue value, List<object> followup, List<object> references)
			{
				if (value.Value != null)
				{
					followup.Add(value.Value);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmDelayedValue>(value, "Value") };
			}
		}

		// Token: 0x02000295 RID: 661
		private sealed class VisitorOfIEdmPropertyValue : InterfaceValidator.VisitorOfT<IEdmPropertyValue>
		{
			// Token: 0x06000FA9 RID: 4009 RVA: 0x00029B89 File Offset: 0x00027D89
			protected override IEnumerable<EdmError> VisitT(IEdmPropertyValue value, List<object> followup, List<object> references)
			{
				if (value.Name != null)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmPropertyValue>(value, "Name") };
			}
		}

		// Token: 0x02000296 RID: 662
		private sealed class VisitorOfIEdmEnumValue : InterfaceValidator.VisitorOfT<IEdmEnumValue>
		{
			// Token: 0x06000FAB RID: 4011 RVA: 0x00029BB1 File Offset: 0x00027DB1
			protected override IEnumerable<EdmError> VisitT(IEdmEnumValue value, List<object> followup, List<object> references)
			{
				if (value.Value != null)
				{
					followup.Add(value.Value);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmEnumValue>(value, "Value") };
			}
		}

		// Token: 0x02000297 RID: 663
		private sealed class VisitorOfIEdmCollectionValue : InterfaceValidator.VisitorOfT<IEdmCollectionValue>
		{
			// Token: 0x06000FAD RID: 4013 RVA: 0x00029BE8 File Offset: 0x00027DE8
			protected override IEnumerable<EdmError> VisitT(IEdmCollectionValue value, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmCollectionValue, IEdmDelayedValue>(value, value.Elements, "Elements", followup, ref list);
				return list;
			}
		}

		// Token: 0x02000298 RID: 664
		private sealed class VisitorOfIEdmStructuredValue : InterfaceValidator.VisitorOfT<IEdmStructuredValue>
		{
			// Token: 0x06000FAF RID: 4015 RVA: 0x00029C14 File Offset: 0x00027E14
			protected override IEnumerable<EdmError> VisitT(IEdmStructuredValue value, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmStructuredValue, IEdmPropertyValue>(value, value.PropertyValues, "PropertyValues", followup, ref list);
				return list;
			}
		}

		// Token: 0x02000299 RID: 665
		private sealed class VisitorOfIEdmBinaryValue : InterfaceValidator.VisitorOfT<IEdmBinaryValue>
		{
			// Token: 0x06000FB1 RID: 4017 RVA: 0x00029C40 File Offset: 0x00027E40
			protected override IEnumerable<EdmError> VisitT(IEdmBinaryValue value, List<object> followup, List<object> references)
			{
				if (value.Value != null)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmBinaryValue>(value, "Value") };
			}
		}

		// Token: 0x0200029A RID: 666
		private sealed class VisitorOfIEdmStringValue : InterfaceValidator.VisitorOfT<IEdmStringValue>
		{
			// Token: 0x06000FB3 RID: 4019 RVA: 0x00029C68 File Offset: 0x00027E68
			protected override IEnumerable<EdmError> VisitT(IEdmStringValue value, List<object> followup, List<object> references)
			{
				if (value.Value != null)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmStringValue>(value, "Value") };
			}
		}

		// Token: 0x0200029B RID: 667
		private sealed class VisitorOfIEdmVocabularyAnnotation : InterfaceValidator.VisitorOfT<IEdmVocabularyAnnotation>
		{
			// Token: 0x06000FB5 RID: 4021 RVA: 0x00029C90 File Offset: 0x00027E90
			protected override IEnumerable<EdmError> VisitT(IEdmVocabularyAnnotation annotation, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (annotation.Term != null)
				{
					references.Add(annotation.Term);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmVocabularyAnnotation>(annotation, "Term"), ref list);
				}
				if (annotation.Target != null)
				{
					references.Add(annotation.Target);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmVocabularyAnnotation>(annotation, "Target"), ref list);
				}
				if (annotation.Value != null)
				{
					followup.Add(annotation.Value);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmVocabularyAnnotation>(annotation, "Value"), ref list);
				}
				return list;
			}
		}

		// Token: 0x0200029C RID: 668
		private sealed class VisitorOfIEdmPropertyValueBinding : InterfaceValidator.VisitorOfT<IEdmPropertyValueBinding>
		{
			// Token: 0x06000FB7 RID: 4023 RVA: 0x00029D20 File Offset: 0x00027F20
			protected override IEnumerable<EdmError> VisitT(IEdmPropertyValueBinding binding, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (binding.Value != null)
				{
					followup.Add(binding.Value);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmPropertyValueBinding>(binding, "Value"), ref list);
				}
				if (binding.BoundProperty != null)
				{
					references.Add(binding.BoundProperty);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmPropertyValueBinding>(binding, "BoundProperty"), ref list);
				}
				return list;
			}
		}

		// Token: 0x0200029D RID: 669
		private sealed class VisitorOfIEdmDirectValueAnnotation : InterfaceValidator.VisitorOfT<IEdmDirectValueAnnotation>
		{
			// Token: 0x06000FB9 RID: 4025 RVA: 0x00029D88 File Offset: 0x00027F88
			protected override IEnumerable<EdmError> VisitT(IEdmDirectValueAnnotation annotation, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (annotation.NamespaceUri == null)
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmDirectValueAnnotation>(annotation, "NamespaceUri"), ref list);
				}
				if (annotation.Value == null)
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmDirectValueAnnotation>(annotation, "Value"), ref list);
				}
				return list;
			}
		}
	}
}
