using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x020000DC RID: 220
	internal class InterfaceValidator
	{
		// Token: 0x06000651 RID: 1617 RVA: 0x000108FA File Offset: 0x0000EAFA
		private InterfaceValidator(HashSetInternal<object> skipVisitation, IEdmModel model, bool validateDirectValueAnnotations)
		{
			this.skipVisitation = skipVisitation;
			this.model = model;
			this.validateDirectValueAnnotations = validateDirectValueAnnotations;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00010938 File Offset: 0x0000EB38
		public static IEnumerable<EdmError> ValidateModelStructureAndSemantics(IEdmModel model, ValidationRuleSet semanticRuleSet)
		{
			InterfaceValidator modelValidator = new InterfaceValidator(null, model, true);
			List<EdmError> list = new List<EdmError>(modelValidator.ValidateStructure(model));
			InterfaceValidator referencesValidator = new InterfaceValidator(modelValidator.visited, model, false);
			IEnumerable<object> enumerable = modelValidator.danglingReferences;
			while (Enumerable.FirstOrDefault<object>(enumerable) != null)
			{
				foreach (object obj in enumerable)
				{
					list.AddRange(referencesValidator.ValidateStructure(obj));
				}
				enumerable = Enumerable.ToArray<object>(referencesValidator.danglingReferences);
			}
			if (Enumerable.Any<EdmError>(list, new Func<EdmError, bool>(ValidationHelper.IsInterfaceCritical)))
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

		// Token: 0x06000653 RID: 1619 RVA: 0x00010AD8 File Offset: 0x0000ECD8
		public static IEnumerable<EdmError> GetStructuralErrors(IEdmElement item)
		{
			IEdmModel edmModel = item as IEdmModel;
			InterfaceValidator interfaceValidator = new InterfaceValidator(null, edmModel, edmModel != null);
			return interfaceValidator.ValidateStructure(item);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00010B00 File Offset: 0x0000ED00
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

		// Token: 0x06000655 RID: 1621 RVA: 0x00010BA0 File Offset: 0x0000EDA0
		private static IEnumerable<InterfaceValidator.VisitorBase> ComputeInterfaceVisitorsForObject(Type objectType)
		{
			List<InterfaceValidator.VisitorBase> list = new List<InterfaceValidator.VisitorBase>();
			foreach (Type type in objectType.GetInterfaces())
			{
				InterfaceValidator.VisitorBase visitorBase;
				if (InterfaceValidator.InterfaceVisitors.TryGetValue(type, ref visitorBase))
				{
					list.Add(visitorBase);
				}
			}
			return list;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00010BE4 File Offset: 0x0000EDE4
		private static EdmError CreatePropertyMustNotBeNullError<T>(T item, string propertyName)
		{
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalPropertyValueMustNotBeNull, Strings.EdmModel_Validator_Syntactic_PropertyMustNotBeNull(typeof(T).Name, propertyName));
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00010C0D File Offset: 0x0000EE0D
		private static EdmError CreateEnumPropertyOutOfRangeError<T, E>(T item, E enumValue, string propertyName)
		{
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalEnumPropertyValueOutOfRange, Strings.EdmModel_Validator_Syntactic_EnumPropertyValueOutOfRange(typeof(T).Name, propertyName, typeof(E).Name, enumValue));
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00010C4C File Offset: 0x0000EE4C
		private static EdmError CheckForInterfaceKindValueMismatchError<T, K, I>(T item, K kind, string propertyName)
		{
			if (item is I)
			{
				return null;
			}
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalKindValueMismatch, Strings.EdmModel_Validator_Syntactic_InterfaceKindValueMismatch(kind, typeof(T).Name, propertyName, typeof(I).Name));
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00010CA4 File Offset: 0x0000EEA4
		private static EdmError CreateInterfaceKindValueUnexpectedError<T, K>(T item, K kind, string propertyName)
		{
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalKindValueUnexpected, Strings.EdmModel_Validator_Syntactic_InterfaceKindValueUnexpected(kind, typeof(T).Name, propertyName));
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00010CD3 File Offset: 0x0000EED3
		private static EdmError CreateTypeRefInterfaceTypeKindValueMismatchError<T>(T item) where T : IEdmTypeReference
		{
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalKindValueMismatch, Strings.EdmModel_Validator_Syntactic_TypeRefInterfaceTypeKindValueMismatch(typeof(T).Name, item.Definition.TypeKind));
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00010D14 File Offset: 0x0000EF14
		private static EdmError CreatePrimitiveTypeRefInterfaceTypeKindValueMismatchError<T>(T item) where T : IEdmPrimitiveTypeReference
		{
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalKindValueMismatch, Strings.EdmModel_Validator_Syntactic_TypeRefInterfaceTypeKindValueMismatch(typeof(T).Name, ((IEdmPrimitiveType)item.Definition).PrimitiveKind));
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00010D64 File Offset: 0x0000EF64
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

		// Token: 0x0600065D RID: 1629 RVA: 0x00010E00 File Offset: 0x0000F000
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

		// Token: 0x0600065E RID: 1630 RVA: 0x00010E18 File Offset: 0x0000F018
		private static bool IsCheckableBad(object element)
		{
			IEdmCheckable edmCheckable = element as IEdmCheckable;
			return edmCheckable != null && edmCheckable.Errors != null && Enumerable.Count<EdmError>(edmCheckable.Errors) > 0;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00010E48 File Offset: 0x0000F048
		private static EdmLocation GetLocation(object item)
		{
			IEdmLocatable edmLocatable = item as IEdmLocatable;
			if (edmLocatable == null || edmLocatable.Location == null)
			{
				return new ObjectLocation(item);
			}
			return edmLocatable.Location;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00010E74 File Offset: 0x0000F074
		private static IEnumerable<ValidationRule> GetSemanticInterfaceVisitorsForObject(Type objectType, ValidationRuleSet ruleSet, Dictionary<Type, List<ValidationRule>> concreteTypeSemanticInterfaceVisitors)
		{
			List<ValidationRule> list;
			if (!concreteTypeSemanticInterfaceVisitors.TryGetValue(objectType, ref list))
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

		// Token: 0x06000661 RID: 1633 RVA: 0x00010EC4 File Offset: 0x0000F0C4
		private IEnumerable<EdmError> ValidateStructure(object item)
		{
			if (item is IEdmValidCoreModelElement || this.visited.Contains(item) || (this.skipVisitation != null && this.skipVisitation.Contains(item)))
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

		// Token: 0x06000662 RID: 1634 RVA: 0x000110E4 File Offset: 0x0000F2E4
		private void CollectReference(object reference)
		{
			if (!(reference is IEdmValidCoreModelElement) && !this.visited.Contains(reference) && (this.skipVisitation == null || !this.skipVisitation.Contains(reference)))
			{
				this.danglingReferences.Add(reference);
			}
		}

		// Token: 0x040003DF RID: 991
		private static readonly Dictionary<Type, InterfaceValidator.VisitorBase> InterfaceVisitors = InterfaceValidator.CreateInterfaceVisitorsMap();

		// Token: 0x040003E0 RID: 992
		private static readonly Memoizer<Type, IEnumerable<InterfaceValidator.VisitorBase>> ConcreteTypeInterfaceVisitors = new Memoizer<Type, IEnumerable<InterfaceValidator.VisitorBase>>(new Func<Type, IEnumerable<InterfaceValidator.VisitorBase>>(InterfaceValidator.ComputeInterfaceVisitorsForObject), null);

		// Token: 0x040003E1 RID: 993
		private readonly HashSetInternal<object> visited = new HashSetInternal<object>();

		// Token: 0x040003E2 RID: 994
		private readonly HashSetInternal<object> visitedBad = new HashSetInternal<object>();

		// Token: 0x040003E3 RID: 995
		private readonly HashSetInternal<object> danglingReferences = new HashSetInternal<object>();

		// Token: 0x040003E4 RID: 996
		private readonly HashSetInternal<object> skipVisitation;

		// Token: 0x040003E5 RID: 997
		private readonly bool validateDirectValueAnnotations;

		// Token: 0x040003E6 RID: 998
		private readonly IEdmModel model;

		// Token: 0x0200022D RID: 557
		private abstract class VisitorBase
		{
			// Token: 0x06000E81 RID: 3713
			public abstract IEnumerable<EdmError> Visit(object item, List<object> followup, List<object> references);
		}

		// Token: 0x0200022E RID: 558
		private abstract class VisitorOfT<T> : InterfaceValidator.VisitorBase
		{
			// Token: 0x06000E83 RID: 3715 RVA: 0x0002884D File Offset: 0x00026A4D
			public override IEnumerable<EdmError> Visit(object item, List<object> followup, List<object> references)
			{
				return this.VisitT((T)((object)item), followup, references);
			}

			// Token: 0x06000E84 RID: 3716
			protected abstract IEnumerable<EdmError> VisitT(T item, List<object> followup, List<object> references);
		}

		// Token: 0x0200022F RID: 559
		private sealed class VisitorOfIEdmCheckable : InterfaceValidator.VisitorOfT<IEdmCheckable>
		{
			// Token: 0x06000E86 RID: 3718 RVA: 0x00028868 File Offset: 0x00026A68
			protected override IEnumerable<EdmError> VisitT(IEdmCheckable checkable, List<object> followup, List<object> references)
			{
				List<EdmError> list = new List<EdmError>();
				List<EdmError> list2 = null;
				InterfaceValidator.ProcessEnumerable<IEdmCheckable, EdmError>(checkable, checkable.Errors, "Errors", list, ref list2);
				return list2 ?? list;
			}
		}

		// Token: 0x02000230 RID: 560
		private sealed class VisitorOfIEdmElement : InterfaceValidator.VisitorOfT<IEdmElement>
		{
			// Token: 0x06000E88 RID: 3720 RVA: 0x00008D69 File Offset: 0x00006F69
			protected override IEnumerable<EdmError> VisitT(IEdmElement element, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x02000231 RID: 561
		private sealed class VisitorOfIEdmNamedElement : InterfaceValidator.VisitorOfT<IEdmNamedElement>
		{
			// Token: 0x06000E8A RID: 3722 RVA: 0x000288A7 File Offset: 0x00026AA7
			protected override IEnumerable<EdmError> VisitT(IEdmNamedElement element, List<object> followup, List<object> references)
			{
				if (element.Name == null)
				{
					return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmNamedElement>(element, "Name") };
				}
				return null;
			}
		}

		// Token: 0x02000232 RID: 562
		private sealed class VisitorOfIEdmSchemaElement : InterfaceValidator.VisitorOfT<IEdmSchemaElement>
		{
			// Token: 0x06000E8C RID: 3724 RVA: 0x000288D0 File Offset: 0x00026AD0
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

		// Token: 0x02000233 RID: 563
		private sealed class VisitorOfIEdmModel : InterfaceValidator.VisitorOfT<IEdmModel>
		{
			// Token: 0x06000E8E RID: 3726 RVA: 0x00028A00 File Offset: 0x00026C00
			protected override IEnumerable<EdmError> VisitT(IEdmModel model, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmModel, IEdmSchemaElement>(model, model.SchemaElements, "SchemaElements", followup, ref list);
				InterfaceValidator.ProcessEnumerable<IEdmModel, IEdmVocabularyAnnotation>(model, model.VocabularyAnnotations, "VocabularyAnnotations", followup, ref list);
				return list;
			}
		}

		// Token: 0x02000234 RID: 564
		private sealed class VisitorOfIEdmEntityContainer : InterfaceValidator.VisitorOfT<IEdmEntityContainer>
		{
			// Token: 0x06000E90 RID: 3728 RVA: 0x00028A40 File Offset: 0x00026C40
			protected override IEnumerable<EdmError> VisitT(IEdmEntityContainer container, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmEntityContainer, IEdmEntityContainerElement>(container, container.Elements, "Elements", followup, ref list);
				return list;
			}
		}

		// Token: 0x02000235 RID: 565
		private sealed class VisitorOfIEdmEntityContainerElement : InterfaceValidator.VisitorOfT<IEdmEntityContainerElement>
		{
			// Token: 0x06000E92 RID: 3730 RVA: 0x00028A6C File Offset: 0x00026C6C
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

		// Token: 0x02000236 RID: 566
		private sealed class VisitorOfIEdmContainedEntitySet : InterfaceValidator.VisitorOfT<IEdmContainedEntitySet>
		{
			// Token: 0x06000E94 RID: 3732 RVA: 0x00028B04 File Offset: 0x00026D04
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

		// Token: 0x02000237 RID: 567
		private sealed class VisitorOfIEdmNavigationSource : InterfaceValidator.VisitorOfT<IEdmNavigationSource>
		{
			// Token: 0x06000E96 RID: 3734 RVA: 0x00028B38 File Offset: 0x00026D38
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

		// Token: 0x02000238 RID: 568
		private sealed class VisitorOfIEdmEntitySetBase : InterfaceValidator.VisitorOfT<IEdmEntitySetBase>
		{
			// Token: 0x06000E98 RID: 3736 RVA: 0x00028BF4 File Offset: 0x00026DF4
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

		// Token: 0x02000239 RID: 569
		private sealed class VisitorOfIEdmSingleton : InterfaceValidator.VisitorOfT<IEdmSingleton>
		{
			// Token: 0x06000E9A RID: 3738 RVA: 0x00028C34 File Offset: 0x00026E34
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

		// Token: 0x0200023A RID: 570
		private sealed class VisitorOfIEdmTypeReference : InterfaceValidator.VisitorOfT<IEdmTypeReference>
		{
			// Token: 0x06000E9C RID: 3740 RVA: 0x00028C74 File Offset: 0x00026E74
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

		// Token: 0x0200023B RID: 571
		private sealed class VisitorOfIEdmType : InterfaceValidator.VisitorOfT<IEdmType>
		{
			// Token: 0x06000E9E RID: 3742 RVA: 0x00028CD0 File Offset: 0x00026ED0
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

		// Token: 0x0200023C RID: 572
		private sealed class VisitorOfIEdmPrimitiveType : InterfaceValidator.VisitorOfT<IEdmPrimitiveType>
		{
			// Token: 0x06000EA0 RID: 3744 RVA: 0x00028DC9 File Offset: 0x00026FC9
			protected override IEnumerable<EdmError> VisitT(IEdmPrimitiveType type, List<object> followup, List<object> references)
			{
				if (!InterfaceValidator.IsCheckableBad(type) && (type.PrimitiveKind < EdmPrimitiveTypeKind.None || type.PrimitiveKind > EdmPrimitiveTypeKind.GeometryMultiPoint))
				{
					return new EdmError[] { InterfaceValidator.CreateInterfaceKindValueUnexpectedError<IEdmPrimitiveType, EdmPrimitiveTypeKind>(type, type.PrimitiveKind, "PrimitiveKind") };
				}
				return null;
			}
		}

		// Token: 0x0200023D RID: 573
		private sealed class VisitorOfIEdmStructuredType : InterfaceValidator.VisitorOfT<IEdmStructuredType>
		{
			// Token: 0x06000EA2 RID: 3746 RVA: 0x00028E0C File Offset: 0x0002700C
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

		// Token: 0x0200023E RID: 574
		private sealed class VisitorOfIEdmEntityType : InterfaceValidator.VisitorOfT<IEdmEntityType>
		{
			// Token: 0x06000EA4 RID: 3748 RVA: 0x00028EB8 File Offset: 0x000270B8
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

		// Token: 0x0200023F RID: 575
		private sealed class VisitorOfIEdmEntityReferenceType : InterfaceValidator.VisitorOfT<IEdmEntityReferenceType>
		{
			// Token: 0x06000EA6 RID: 3750 RVA: 0x00028EEC File Offset: 0x000270EC
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

		// Token: 0x02000240 RID: 576
		private sealed class VisitorOfIEdmUntypedType : InterfaceValidator.VisitorOfT<IEdmUntypedType>
		{
			// Token: 0x06000EA8 RID: 3752 RVA: 0x00008D69 File Offset: 0x00006F69
			protected override IEnumerable<EdmError> VisitT(IEdmUntypedType type, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x02000241 RID: 577
		private sealed class VisitorOfIEdmEnumType : InterfaceValidator.VisitorOfT<IEdmEnumType>
		{
			// Token: 0x06000EAA RID: 3754 RVA: 0x00028F28 File Offset: 0x00027128
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

		// Token: 0x02000242 RID: 578
		private sealed class VisitorOfIEdmTypeDefinition : InterfaceValidator.VisitorOfT<IEdmTypeDefinition>
		{
			// Token: 0x06000EAC RID: 3756 RVA: 0x00028F7C File Offset: 0x0002717C
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

		// Token: 0x02000243 RID: 579
		private sealed class VisitorOfIEdmTerm : InterfaceValidator.VisitorOfT<IEdmTerm>
		{
			// Token: 0x06000EAE RID: 3758 RVA: 0x00028FBC File Offset: 0x000271BC
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

		// Token: 0x02000244 RID: 580
		private sealed class VisitorOfIEdmCollectionType : InterfaceValidator.VisitorOfT<IEdmCollectionType>
		{
			// Token: 0x06000EB0 RID: 3760 RVA: 0x00028FF0 File Offset: 0x000271F0
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

		// Token: 0x02000245 RID: 581
		private sealed class VisitorOfIEdmProperty : InterfaceValidator.VisitorOfT<IEdmProperty>
		{
			// Token: 0x06000EB2 RID: 3762 RVA: 0x00029024 File Offset: 0x00027224
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

		// Token: 0x02000246 RID: 582
		private sealed class VisitorOfIEdmStructuralProperty : InterfaceValidator.VisitorOfT<IEdmStructuralProperty>
		{
			// Token: 0x06000EB4 RID: 3764 RVA: 0x00008D69 File Offset: 0x00006F69
			protected override IEnumerable<EdmError> VisitT(IEdmStructuralProperty property, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x02000247 RID: 583
		private sealed class VisitorOfIEdmNavigationProperty : InterfaceValidator.VisitorOfT<IEdmNavigationProperty>
		{
			// Token: 0x06000EB6 RID: 3766 RVA: 0x000290FC File Offset: 0x000272FC
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

		// Token: 0x02000248 RID: 584
		private sealed class VisitorOfIEdmReferentialConstraint : InterfaceValidator.VisitorOfT<IEdmReferentialConstraint>
		{
			// Token: 0x06000EB8 RID: 3768 RVA: 0x000291D0 File Offset: 0x000273D0
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

		// Token: 0x02000249 RID: 585
		private sealed class VisitorOfIEdmEnumMember : InterfaceValidator.VisitorOfT<IEdmEnumMember>
		{
			// Token: 0x06000EBA RID: 3770 RVA: 0x00029288 File Offset: 0x00027488
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

		// Token: 0x0200024A RID: 586
		private sealed class VisitorOfIEdmOperation : InterfaceValidator.VisitorOfT<IEdmOperation>
		{
			// Token: 0x06000EBC RID: 3772 RVA: 0x000292F0 File Offset: 0x000274F0
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

		// Token: 0x0200024B RID: 587
		private sealed class VisitorOfIEdmAction : InterfaceValidator.VisitorOfT<IEdmAction>
		{
			// Token: 0x06000EBE RID: 3774 RVA: 0x00008D69 File Offset: 0x00006F69
			protected override IEnumerable<EdmError> VisitT(IEdmAction operation, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x0200024C RID: 588
		private sealed class VisitorOfIEdmFunction : InterfaceValidator.VisitorOfT<IEdmFunction>
		{
			// Token: 0x06000EC0 RID: 3776 RVA: 0x00008D69 File Offset: 0x00006F69
			protected override IEnumerable<EdmError> VisitT(IEdmFunction operation, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x0200024D RID: 589
		private sealed class VisitorOfIEdmOperationImport : InterfaceValidator.VisitorOfT<IEdmOperationImport>
		{
			// Token: 0x06000EC2 RID: 3778 RVA: 0x00029340 File Offset: 0x00027540
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

		// Token: 0x0200024E RID: 590
		private sealed class VisitorOfIEdmActionImport : InterfaceValidator.VisitorOfT<IEdmActionImport>
		{
			// Token: 0x06000EC4 RID: 3780 RVA: 0x00008D69 File Offset: 0x00006F69
			protected override IEnumerable<EdmError> VisitT(IEdmActionImport actionImport, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x0200024F RID: 591
		private sealed class VisitorOfIEdmFunctionImport : InterfaceValidator.VisitorOfT<IEdmFunctionImport>
		{
			// Token: 0x06000EC6 RID: 3782 RVA: 0x00008D69 File Offset: 0x00006F69
			protected override IEnumerable<EdmError> VisitT(IEdmFunctionImport functionImport, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x02000250 RID: 592
		private sealed class VisitorOfIEdmOperationParameter : InterfaceValidator.VisitorOfT<IEdmOperationParameter>
		{
			// Token: 0x06000EC8 RID: 3784 RVA: 0x0002937C File Offset: 0x0002757C
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

		// Token: 0x02000251 RID: 593
		private sealed class VisitorOfIEdmOptionalParameter : InterfaceValidator.VisitorOfT<IEdmOptionalParameter>
		{
			// Token: 0x06000ECA RID: 3786 RVA: 0x00008D69 File Offset: 0x00006F69
			protected override IEnumerable<EdmError> VisitT(IEdmOptionalParameter parameter, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x02000252 RID: 594
		private sealed class VisitorOfIEdmCollectionTypeReference : InterfaceValidator.VisitorOfT<IEdmCollectionTypeReference>
		{
			// Token: 0x06000ECC RID: 3788 RVA: 0x000293EC File Offset: 0x000275EC
			protected override IEnumerable<EdmError> VisitT(IEdmCollectionTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Collection)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmCollectionTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000253 RID: 595
		private sealed class VisitorOfIEdmEntityReferenceTypeReference : InterfaceValidator.VisitorOfT<IEdmEntityReferenceTypeReference>
		{
			// Token: 0x06000ECE RID: 3790 RVA: 0x0002941D File Offset: 0x0002761D
			protected override IEnumerable<EdmError> VisitT(IEdmEntityReferenceTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.EntityReference)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmEntityReferenceTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000254 RID: 596
		private sealed class VisitorOfIEdmStructuredTypeReference : InterfaceValidator.VisitorOfT<IEdmStructuredTypeReference>
		{
			// Token: 0x06000ED0 RID: 3792 RVA: 0x0002944E File Offset: 0x0002764E
			protected override IEnumerable<EdmError> VisitT(IEdmStructuredTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind.IsStructured())
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmStructuredTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000255 RID: 597
		private sealed class VisitorOfIEdmEntityTypeReference : InterfaceValidator.VisitorOfT<IEdmEntityTypeReference>
		{
			// Token: 0x06000ED2 RID: 3794 RVA: 0x00029483 File Offset: 0x00027683
			protected override IEnumerable<EdmError> VisitT(IEdmEntityTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Entity)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmEntityTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000256 RID: 598
		private sealed class VisitorOfIEdmComplexTypeReference : InterfaceValidator.VisitorOfT<IEdmComplexTypeReference>
		{
			// Token: 0x06000ED4 RID: 3796 RVA: 0x000294B4 File Offset: 0x000276B4
			protected override IEnumerable<EdmError> VisitT(IEdmComplexTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Complex)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmComplexTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000257 RID: 599
		private sealed class VisitorOfIEdmUntypedTypeReference : InterfaceValidator.VisitorOfT<IEdmUntypedTypeReference>
		{
			// Token: 0x06000ED6 RID: 3798 RVA: 0x000294E5 File Offset: 0x000276E5
			protected override IEnumerable<EdmError> VisitT(IEdmUntypedTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Untyped)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmUntypedTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000258 RID: 600
		private sealed class VisitorOfIEdmEnumTypeReference : InterfaceValidator.VisitorOfT<IEdmEnumTypeReference>
		{
			// Token: 0x06000ED8 RID: 3800 RVA: 0x00029516 File Offset: 0x00027716
			protected override IEnumerable<EdmError> VisitT(IEdmEnumTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Enum)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmEnumTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000259 RID: 601
		private sealed class VisitorOfIEdmTypeDefinitionReference : InterfaceValidator.VisitorOfT<IEdmTypeDefinitionReference>
		{
			// Token: 0x06000EDA RID: 3802 RVA: 0x00029547 File Offset: 0x00027747
			protected override IEnumerable<EdmError> VisitT(IEdmTypeDefinitionReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.TypeDefinition)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmTypeDefinitionReference>(typeRef) };
			}
		}

		// Token: 0x0200025A RID: 602
		private sealed class VisitorOfIEdmPrimitiveTypeReference : InterfaceValidator.VisitorOfT<IEdmPrimitiveTypeReference>
		{
			// Token: 0x06000EDC RID: 3804 RVA: 0x00029578 File Offset: 0x00027778
			protected override IEnumerable<EdmError> VisitT(IEdmPrimitiveTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Primitive)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmPrimitiveTypeReference>(typeRef) };
			}
		}

		// Token: 0x0200025B RID: 603
		private sealed class VisitorOfIEdmBinaryTypeReference : InterfaceValidator.VisitorOfT<IEdmBinaryTypeReference>
		{
			// Token: 0x06000EDE RID: 3806 RVA: 0x000295AC File Offset: 0x000277AC
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

		// Token: 0x0200025C RID: 604
		private sealed class VisitorOfIEdmDecimalTypeReference : InterfaceValidator.VisitorOfT<IEdmDecimalTypeReference>
		{
			// Token: 0x06000EE0 RID: 3808 RVA: 0x000295EC File Offset: 0x000277EC
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

		// Token: 0x0200025D RID: 605
		private sealed class VisitorOfIEdmStringTypeReference : InterfaceValidator.VisitorOfT<IEdmStringTypeReference>
		{
			// Token: 0x06000EE2 RID: 3810 RVA: 0x0002962C File Offset: 0x0002782C
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

		// Token: 0x0200025E RID: 606
		private sealed class VisitorOfIEdmTemporalTypeReference : InterfaceValidator.VisitorOfT<IEdmTemporalTypeReference>
		{
			// Token: 0x06000EE4 RID: 3812 RVA: 0x0002966C File Offset: 0x0002786C
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

		// Token: 0x0200025F RID: 607
		private sealed class VisitorOfIEdmSpatialTypeReference : InterfaceValidator.VisitorOfT<IEdmSpatialTypeReference>
		{
			// Token: 0x06000EE6 RID: 3814 RVA: 0x000296B0 File Offset: 0x000278B0
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

		// Token: 0x02000260 RID: 608
		private sealed class VisitorOfIEdmReference : InterfaceValidator.VisitorOfT<IEdmReference>
		{
			// Token: 0x06000EE8 RID: 3816 RVA: 0x000296F2 File Offset: 0x000278F2
			protected override IEnumerable<EdmError> VisitT(IEdmReference edmReference, List<object> followup, List<object> references)
			{
				if (Enumerable.Any<IEdmInclude>(edmReference.Includes) || !Enumerable.Any<IEdmIncludeAnnotations>(edmReference.IncludeAnnotations))
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmReference>(edmReference, "Includes/IncludeAnnotations") };
			}
		}

		// Token: 0x02000261 RID: 609
		private sealed class VisitorOfIEdmInclude : InterfaceValidator.VisitorOfT<IEdmInclude>
		{
			// Token: 0x06000EEA RID: 3818 RVA: 0x0002972C File Offset: 0x0002792C
			protected override IEnumerable<EdmError> VisitT(IEdmInclude edmInclude, List<object> followup, List<object> references)
			{
				if (!string.IsNullOrEmpty(edmInclude.Namespace))
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmInclude>(edmInclude, "Namespace") };
			}
		}

		// Token: 0x02000262 RID: 610
		private sealed class VisitorOfIEdmIncludeAnnotations : InterfaceValidator.VisitorOfT<IEdmIncludeAnnotations>
		{
			// Token: 0x06000EEC RID: 3820 RVA: 0x00029759 File Offset: 0x00027959
			protected override IEnumerable<EdmError> VisitT(IEdmIncludeAnnotations edmIncludeAnnotations, List<object> followup, List<object> references)
			{
				if (!string.IsNullOrEmpty(edmIncludeAnnotations.TermNamespace))
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmIncludeAnnotations>(edmIncludeAnnotations, "TermNamespace") };
			}
		}

		// Token: 0x02000263 RID: 611
		private sealed class VisitorOfIEdmExpression : InterfaceValidator.VisitorOfT<IEdmExpression>
		{
			// Token: 0x06000EEE RID: 3822 RVA: 0x00029788 File Offset: 0x00027988
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

		// Token: 0x02000264 RID: 612
		private sealed class VisitorOfIEdmRecordExpression : InterfaceValidator.VisitorOfT<IEdmRecordExpression>
		{
			// Token: 0x06000EF0 RID: 3824 RVA: 0x00029A28 File Offset: 0x00027C28
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

		// Token: 0x02000265 RID: 613
		private sealed class VisitorOfIEdmPropertyConstructor : InterfaceValidator.VisitorOfT<IEdmPropertyConstructor>
		{
			// Token: 0x06000EF2 RID: 3826 RVA: 0x00029A68 File Offset: 0x00027C68
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

		// Token: 0x02000266 RID: 614
		private sealed class VisitorOfIEdmCollectionExpression : InterfaceValidator.VisitorOfT<IEdmCollectionExpression>
		{
			// Token: 0x06000EF4 RID: 3828 RVA: 0x00029AC4 File Offset: 0x00027CC4
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

		// Token: 0x02000267 RID: 615
		private sealed class VisitorOfIEdmLabeledElement : InterfaceValidator.VisitorOfT<IEdmLabeledExpression>
		{
			// Token: 0x06000EF6 RID: 3830 RVA: 0x00029B04 File Offset: 0x00027D04
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

		// Token: 0x02000268 RID: 616
		private sealed class VisitorOfIEdmPathExpression : InterfaceValidator.VisitorOfT<IEdmPathExpression>
		{
			// Token: 0x06000EF8 RID: 3832 RVA: 0x00029B38 File Offset: 0x00027D38
			protected override IEnumerable<EdmError> VisitT(IEdmPathExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				List<string> list2 = new List<string>();
				InterfaceValidator.ProcessEnumerable<IEdmPathExpression, string>(expression, expression.PathSegments, "Path", list2, ref list);
				return list;
			}
		}

		// Token: 0x02000269 RID: 617
		private sealed class VistorOfIEdmEnumMemberExpression : InterfaceValidator.VisitorOfT<IEdmEnumMemberExpression>
		{
			// Token: 0x06000EFA RID: 3834 RVA: 0x00029B6C File Offset: 0x00027D6C
			protected override IEnumerable<EdmError> VisitT(IEdmEnumMemberExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmEnumMemberExpression, IEdmEnumMember>(expression, expression.EnumMembers, "EnumMembers", followup, ref list);
				return list;
			}
		}

		// Token: 0x0200026A RID: 618
		private sealed class VistorOfIEdmIfExpression : InterfaceValidator.VisitorOfT<IEdmIfExpression>
		{
			// Token: 0x06000EFC RID: 3836 RVA: 0x00029B98 File Offset: 0x00027D98
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

		// Token: 0x0200026B RID: 619
		private sealed class VistorOfIEdmCastExpression : InterfaceValidator.VisitorOfT<IEdmCastExpression>
		{
			// Token: 0x06000EFE RID: 3838 RVA: 0x00029C28 File Offset: 0x00027E28
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

		// Token: 0x0200026C RID: 620
		private sealed class VistorOfIEdmIsTypeExpression : InterfaceValidator.VisitorOfT<IEdmIsTypeExpression>
		{
			// Token: 0x06000F00 RID: 3840 RVA: 0x00029C90 File Offset: 0x00027E90
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

		// Token: 0x0200026D RID: 621
		private sealed class VistorOfIEdmFunctionApplicationExpression : InterfaceValidator.VisitorOfT<IEdmApplyExpression>
		{
			// Token: 0x06000F02 RID: 3842 RVA: 0x00029CF8 File Offset: 0x00027EF8
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

		// Token: 0x0200026E RID: 622
		private sealed class VistorOfIEdmLabeledElementReferenceExpression : InterfaceValidator.VisitorOfT<IEdmLabeledExpressionReferenceExpression>
		{
			// Token: 0x06000F04 RID: 3844 RVA: 0x00029D4C File Offset: 0x00027F4C
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

		// Token: 0x0200026F RID: 623
		private sealed class VisitorOfIEdmValue : InterfaceValidator.VisitorOfT<IEdmValue>
		{
			// Token: 0x06000F06 RID: 3846 RVA: 0x00029D80 File Offset: 0x00027F80
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

		// Token: 0x02000270 RID: 624
		private sealed class VisitorOfIEdmDelayedValue : InterfaceValidator.VisitorOfT<IEdmDelayedValue>
		{
			// Token: 0x06000F08 RID: 3848 RVA: 0x00029FBD File Offset: 0x000281BD
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

		// Token: 0x02000271 RID: 625
		private sealed class VisitorOfIEdmPropertyValue : InterfaceValidator.VisitorOfT<IEdmPropertyValue>
		{
			// Token: 0x06000F0A RID: 3850 RVA: 0x00029FF1 File Offset: 0x000281F1
			protected override IEnumerable<EdmError> VisitT(IEdmPropertyValue value, List<object> followup, List<object> references)
			{
				if (value.Name != null)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmPropertyValue>(value, "Name") };
			}
		}

		// Token: 0x02000272 RID: 626
		private sealed class VisitorOfIEdmEnumValue : InterfaceValidator.VisitorOfT<IEdmEnumValue>
		{
			// Token: 0x06000F0C RID: 3852 RVA: 0x0002A019 File Offset: 0x00028219
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

		// Token: 0x02000273 RID: 627
		private sealed class VisitorOfIEdmCollectionValue : InterfaceValidator.VisitorOfT<IEdmCollectionValue>
		{
			// Token: 0x06000F0E RID: 3854 RVA: 0x0002A050 File Offset: 0x00028250
			protected override IEnumerable<EdmError> VisitT(IEdmCollectionValue value, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmCollectionValue, IEdmDelayedValue>(value, value.Elements, "Elements", followup, ref list);
				return list;
			}
		}

		// Token: 0x02000274 RID: 628
		private sealed class VisitorOfIEdmStructuredValue : InterfaceValidator.VisitorOfT<IEdmStructuredValue>
		{
			// Token: 0x06000F10 RID: 3856 RVA: 0x0002A07C File Offset: 0x0002827C
			protected override IEnumerable<EdmError> VisitT(IEdmStructuredValue value, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmStructuredValue, IEdmPropertyValue>(value, value.PropertyValues, "PropertyValues", followup, ref list);
				return list;
			}
		}

		// Token: 0x02000275 RID: 629
		private sealed class VisitorOfIEdmBinaryValue : InterfaceValidator.VisitorOfT<IEdmBinaryValue>
		{
			// Token: 0x06000F12 RID: 3858 RVA: 0x0002A0A8 File Offset: 0x000282A8
			protected override IEnumerable<EdmError> VisitT(IEdmBinaryValue value, List<object> followup, List<object> references)
			{
				if (value.Value != null)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmBinaryValue>(value, "Value") };
			}
		}

		// Token: 0x02000276 RID: 630
		private sealed class VisitorOfIEdmStringValue : InterfaceValidator.VisitorOfT<IEdmStringValue>
		{
			// Token: 0x06000F14 RID: 3860 RVA: 0x0002A0D0 File Offset: 0x000282D0
			protected override IEnumerable<EdmError> VisitT(IEdmStringValue value, List<object> followup, List<object> references)
			{
				if (value.Value != null)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmStringValue>(value, "Value") };
			}
		}

		// Token: 0x02000277 RID: 631
		private sealed class VisitorOfIEdmVocabularyAnnotation : InterfaceValidator.VisitorOfT<IEdmVocabularyAnnotation>
		{
			// Token: 0x06000F16 RID: 3862 RVA: 0x0002A0F8 File Offset: 0x000282F8
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

		// Token: 0x02000278 RID: 632
		private sealed class VisitorOfIEdmPropertyValueBinding : InterfaceValidator.VisitorOfT<IEdmPropertyValueBinding>
		{
			// Token: 0x06000F18 RID: 3864 RVA: 0x0002A188 File Offset: 0x00028388
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

		// Token: 0x02000279 RID: 633
		private sealed class VisitorOfIEdmDirectValueAnnotation : InterfaceValidator.VisitorOfT<IEdmDirectValueAnnotation>
		{
			// Token: 0x06000F1A RID: 3866 RVA: 0x0002A1F0 File Offset: 0x000283F0
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
