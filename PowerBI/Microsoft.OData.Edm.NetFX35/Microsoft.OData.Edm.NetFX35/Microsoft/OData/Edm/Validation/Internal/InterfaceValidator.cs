using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Validation.Internal
{
	// Token: 0x0200021C RID: 540
	internal class InterfaceValidator
	{
		// Token: 0x06000CE6 RID: 3302 RVA: 0x000246B8 File Offset: 0x000228B8
		private InterfaceValidator(HashSetInternal<object> skipVisitation, IEdmModel model, bool validateDirectValueAnnotations)
		{
			this.skipVisitation = skipVisitation;
			this.model = model;
			this.validateDirectValueAnnotations = validateDirectValueAnnotations;
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00024728 File Offset: 0x00022928
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

		// Token: 0x06000CE8 RID: 3304 RVA: 0x000248D0 File Offset: 0x00022AD0
		public static IEnumerable<EdmError> GetStructuralErrors(IEdmElement item)
		{
			IEdmModel edmModel = item as IEdmModel;
			InterfaceValidator interfaceValidator = new InterfaceValidator(null, edmModel, edmModel != null);
			return interfaceValidator.ValidateStructure(item);
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x000248FC File Offset: 0x00022AFC
		private static Dictionary<Type, InterfaceValidator.VisitorBase> CreateInterfaceVisitorsMap()
		{
			Dictionary<Type, InterfaceValidator.VisitorBase> dictionary = new Dictionary<Type, InterfaceValidator.VisitorBase>();
			foreach (Type type in typeof(InterfaceValidator).GetNonPublicNestedTypes())
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

		// Token: 0x06000CEA RID: 3306 RVA: 0x00024994 File Offset: 0x00022B94
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

		// Token: 0x06000CEB RID: 3307 RVA: 0x000249DC File Offset: 0x00022BDC
		private static EdmError CreatePropertyMustNotBeNullError<T>(T item, string propertyName)
		{
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalPropertyValueMustNotBeNull, Strings.EdmModel_Validator_Syntactic_PropertyMustNotBeNull(typeof(T).Name, propertyName));
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00024A05 File Offset: 0x00022C05
		private static EdmError CreateEnumPropertyOutOfRangeError<T, E>(T item, E enumValue, string propertyName)
		{
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalEnumPropertyValueOutOfRange, Strings.EdmModel_Validator_Syntactic_EnumPropertyValueOutOfRange(typeof(T).Name, propertyName, typeof(E).Name, enumValue));
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00024A44 File Offset: 0x00022C44
		private static EdmError CheckForInterfaceKindValueMismatchError<T, K, I>(T item, K kind, string propertyName)
		{
			if (item is I)
			{
				return null;
			}
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalKindValueMismatch, Strings.EdmModel_Validator_Syntactic_InterfaceKindValueMismatch(kind, typeof(T).Name, propertyName, typeof(I).Name));
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00024A9C File Offset: 0x00022C9C
		private static EdmError CreateInterfaceKindValueUnexpectedError<T, K>(T item, K kind, string propertyName)
		{
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalKindValueUnexpected, Strings.EdmModel_Validator_Syntactic_InterfaceKindValueUnexpected(kind, typeof(T).Name, propertyName));
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00024ACB File Offset: 0x00022CCB
		private static EdmError CreateTypeRefInterfaceTypeKindValueMismatchError<T>(T item) where T : IEdmTypeReference
		{
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalKindValueMismatch, Strings.EdmModel_Validator_Syntactic_TypeRefInterfaceTypeKindValueMismatch(typeof(T).Name, item.Definition.TypeKind));
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00024B0C File Offset: 0x00022D0C
		private static EdmError CreatePrimitiveTypeRefInterfaceTypeKindValueMismatchError<T>(T item) where T : IEdmPrimitiveTypeReference
		{
			return new EdmError(InterfaceValidator.GetLocation(item), EdmErrorCode.InterfaceCriticalKindValueMismatch, Strings.EdmModel_Validator_Syntactic_TypeRefInterfaceTypeKindValueMismatch(typeof(T).Name, ((IEdmPrimitiveType)item.Definition).PrimitiveKind));
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00024B5C File Offset: 0x00022D5C
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

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00024BF8 File Offset: 0x00022DF8
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

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00024C10 File Offset: 0x00022E10
		private static bool IsCheckableBad(object element)
		{
			IEdmCheckable edmCheckable = element as IEdmCheckable;
			return edmCheckable != null && edmCheckable.Errors != null && Enumerable.Count<EdmError>(edmCheckable.Errors) > 0;
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00024C40 File Offset: 0x00022E40
		private static EdmLocation GetLocation(object item)
		{
			IEdmLocatable edmLocatable = item as IEdmLocatable;
			if (edmLocatable == null || edmLocatable.Location == null)
			{
				return new ObjectLocation(item);
			}
			return edmLocatable.Location;
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00024C6C File Offset: 0x00022E6C
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

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00024CBC File Offset: 0x00022EBC
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

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00024EDC File Offset: 0x000230DC
		private void CollectReference(object reference)
		{
			if (!(reference is IEdmValidCoreModelElement) && !this.visited.Contains(reference) && (this.skipVisitation == null || !this.skipVisitation.Contains(reference)))
			{
				this.danglingReferences.Add(reference);
			}
		}

		// Token: 0x040005D2 RID: 1490
		private static readonly Dictionary<Type, InterfaceValidator.VisitorBase> InterfaceVisitors = InterfaceValidator.CreateInterfaceVisitorsMap();

		// Token: 0x040005D3 RID: 1491
		private static readonly Memoizer<Type, IEnumerable<InterfaceValidator.VisitorBase>> ConcreteTypeInterfaceVisitors = new Memoizer<Type, IEnumerable<InterfaceValidator.VisitorBase>>(new Func<Type, IEnumerable<InterfaceValidator.VisitorBase>>(InterfaceValidator.ComputeInterfaceVisitorsForObject), null);

		// Token: 0x040005D4 RID: 1492
		private readonly HashSetInternal<object> visited = new HashSetInternal<object>();

		// Token: 0x040005D5 RID: 1493
		private readonly HashSetInternal<object> visitedBad = new HashSetInternal<object>();

		// Token: 0x040005D6 RID: 1494
		private readonly HashSetInternal<object> danglingReferences = new HashSetInternal<object>();

		// Token: 0x040005D7 RID: 1495
		private readonly HashSetInternal<object> skipVisitation;

		// Token: 0x040005D8 RID: 1496
		private readonly bool validateDirectValueAnnotations;

		// Token: 0x040005D9 RID: 1497
		private readonly IEdmModel model;

		// Token: 0x0200021D RID: 541
		private abstract class VisitorBase
		{
			// Token: 0x06000CF9 RID: 3321
			public abstract IEnumerable<EdmError> Visit(object item, List<object> followup, List<object> references);
		}

		// Token: 0x0200021E RID: 542
		private abstract class VisitorOfT<T> : InterfaceValidator.VisitorBase
		{
			// Token: 0x06000CFB RID: 3323 RVA: 0x00024F42 File Offset: 0x00023142
			public override IEnumerable<EdmError> Visit(object item, List<object> followup, List<object> references)
			{
				return this.VisitT((T)((object)item), followup, references);
			}

			// Token: 0x06000CFC RID: 3324
			protected abstract IEnumerable<EdmError> VisitT(T item, List<object> followup, List<object> references);
		}

		// Token: 0x0200021F RID: 543
		private sealed class VisitorOfIEdmCheckable : InterfaceValidator.VisitorOfT<IEdmCheckable>
		{
			// Token: 0x06000CFE RID: 3326 RVA: 0x00024F5C File Offset: 0x0002315C
			protected override IEnumerable<EdmError> VisitT(IEdmCheckable checkable, List<object> followup, List<object> references)
			{
				List<EdmError> list = new List<EdmError>();
				List<EdmError> list2 = null;
				InterfaceValidator.ProcessEnumerable<IEdmCheckable, EdmError>(checkable, checkable.Errors, "Errors", list, ref list2);
				return list2 ?? list;
			}
		}

		// Token: 0x02000220 RID: 544
		private sealed class VisitorOfIEdmElement : InterfaceValidator.VisitorOfT<IEdmElement>
		{
			// Token: 0x06000D00 RID: 3328 RVA: 0x00024F93 File Offset: 0x00023193
			protected override IEnumerable<EdmError> VisitT(IEdmElement element, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x02000221 RID: 545
		private sealed class VisitorOfIEdmNamedElement : InterfaceValidator.VisitorOfT<IEdmNamedElement>
		{
			// Token: 0x06000D02 RID: 3330 RVA: 0x00024FA0 File Offset: 0x000231A0
			protected override IEnumerable<EdmError> VisitT(IEdmNamedElement element, List<object> followup, List<object> references)
			{
				if (element.Name == null)
				{
					return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmNamedElement>(element, "Name") };
				}
				return null;
			}
		}

		// Token: 0x02000222 RID: 546
		private sealed class VisitorOfIEdmSchemaElement : InterfaceValidator.VisitorOfT<IEdmSchemaElement>
		{
			// Token: 0x06000D04 RID: 3332 RVA: 0x00024FD8 File Offset: 0x000231D8
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
				case EdmSchemaElementKind.ValueTerm:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmSchemaElement, EdmSchemaElementKind, IEdmValueTerm>(element, element.SchemaElementKind, "SchemaElementKind"), ref list);
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

		// Token: 0x02000223 RID: 547
		private sealed class VisitorOfIEdmModel : InterfaceValidator.VisitorOfT<IEdmModel>
		{
			// Token: 0x06000D06 RID: 3334 RVA: 0x00025108 File Offset: 0x00023308
			protected override IEnumerable<EdmError> VisitT(IEdmModel model, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmModel, IEdmSchemaElement>(model, model.SchemaElements, "SchemaElements", followup, ref list);
				InterfaceValidator.ProcessEnumerable<IEdmModel, IEdmVocabularyAnnotation>(model, model.VocabularyAnnotations, "VocabularyAnnotations", followup, ref list);
				return list;
			}
		}

		// Token: 0x02000224 RID: 548
		private sealed class VisitorOfIEdmEntityContainer : InterfaceValidator.VisitorOfT<IEdmEntityContainer>
		{
			// Token: 0x06000D08 RID: 3336 RVA: 0x00025148 File Offset: 0x00023348
			protected override IEnumerable<EdmError> VisitT(IEdmEntityContainer container, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmEntityContainer, IEdmEntityContainerElement>(container, container.Elements, "Elements", followup, ref list);
				return list;
			}
		}

		// Token: 0x02000225 RID: 549
		private sealed class VisitorOfIEdmEntityContainerElement : InterfaceValidator.VisitorOfT<IEdmEntityContainerElement>
		{
			// Token: 0x06000D0A RID: 3338 RVA: 0x00025174 File Offset: 0x00023374
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

		// Token: 0x02000226 RID: 550
		private sealed class VisitorOfIEdmContainedEntitySet : InterfaceValidator.VisitorOfT<IEdmContainedEntitySet>
		{
			// Token: 0x06000D0C RID: 3340 RVA: 0x00025210 File Offset: 0x00023410
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

		// Token: 0x02000227 RID: 551
		private sealed class VisitorOfIEdmNavigationSource : InterfaceValidator.VisitorOfT<IEdmNavigationSource>
		{
			// Token: 0x06000D0E RID: 3342 RVA: 0x00025244 File Offset: 0x00023444
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

		// Token: 0x02000228 RID: 552
		private sealed class VisitorOfIEdmEntitySetBase : InterfaceValidator.VisitorOfT<IEdmEntitySetBase>
		{
			// Token: 0x06000D10 RID: 3344 RVA: 0x00025300 File Offset: 0x00023500
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

		// Token: 0x02000229 RID: 553
		private sealed class VisitorOfIEdmSingleton : InterfaceValidator.VisitorOfT<IEdmSingleton>
		{
			// Token: 0x06000D12 RID: 3346 RVA: 0x00025340 File Offset: 0x00023540
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

		// Token: 0x0200022A RID: 554
		private sealed class VisitorOfIEdmTypeReference : InterfaceValidator.VisitorOfT<IEdmTypeReference>
		{
			// Token: 0x06000D14 RID: 3348 RVA: 0x00025380 File Offset: 0x00023580
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

		// Token: 0x0200022B RID: 555
		private sealed class VisitorOfIEdmType : InterfaceValidator.VisitorOfT<IEdmType>
		{
			// Token: 0x06000D16 RID: 3350 RVA: 0x000253DC File Offset: 0x000235DC
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

		// Token: 0x0200022C RID: 556
		private sealed class VisitorOfIEdmPrimitiveType : InterfaceValidator.VisitorOfT<IEdmPrimitiveType>
		{
			// Token: 0x06000D18 RID: 3352 RVA: 0x000254D8 File Offset: 0x000236D8
			protected override IEnumerable<EdmError> VisitT(IEdmPrimitiveType type, List<object> followup, List<object> references)
			{
				if (!InterfaceValidator.IsCheckableBad(type) && (type.PrimitiveKind < EdmPrimitiveTypeKind.None || type.PrimitiveKind > EdmPrimitiveTypeKind.GeometryMultiPoint))
				{
					return new EdmError[] { InterfaceValidator.CreateInterfaceKindValueUnexpectedError<IEdmPrimitiveType, EdmPrimitiveTypeKind>(type, type.PrimitiveKind, "PrimitiveKind") };
				}
				return null;
			}
		}

		// Token: 0x0200022D RID: 557
		private sealed class VisitorOfIEdmStructuredType : InterfaceValidator.VisitorOfT<IEdmStructuredType>
		{
			// Token: 0x06000D1A RID: 3354 RVA: 0x00025528 File Offset: 0x00023728
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

		// Token: 0x0200022E RID: 558
		private sealed class VisitorOfIEdmEntityType : InterfaceValidator.VisitorOfT<IEdmEntityType>
		{
			// Token: 0x06000D1C RID: 3356 RVA: 0x000255D4 File Offset: 0x000237D4
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

		// Token: 0x0200022F RID: 559
		private sealed class VisitorOfIEdmEntityReferenceType : InterfaceValidator.VisitorOfT<IEdmEntityReferenceType>
		{
			// Token: 0x06000D1E RID: 3358 RVA: 0x00025608 File Offset: 0x00023808
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

		// Token: 0x02000230 RID: 560
		private sealed class VisitorOfIEdmEnumType : InterfaceValidator.VisitorOfT<IEdmEnumType>
		{
			// Token: 0x06000D20 RID: 3360 RVA: 0x0002564C File Offset: 0x0002384C
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

		// Token: 0x02000231 RID: 561
		private sealed class VisitorOfIEdmTypeDefinition : InterfaceValidator.VisitorOfT<IEdmTypeDefinition>
		{
			// Token: 0x06000D22 RID: 3362 RVA: 0x000256A0 File Offset: 0x000238A0
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

		// Token: 0x02000232 RID: 562
		private sealed class VisitorOfIEdmTerm : InterfaceValidator.VisitorOfT<IEdmTerm>
		{
			// Token: 0x06000D24 RID: 3364 RVA: 0x000256E0 File Offset: 0x000238E0
			protected override IEnumerable<EdmError> VisitT(IEdmTerm term, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				switch (term.TermKind)
				{
				case EdmTermKind.None:
					break;
				case EdmTermKind.Type:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmTerm, EdmTermKind, IEdmSchemaType>(term, term.TermKind, "TermKind"), ref list);
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmTerm, EdmTermKind, IEdmStructuredType>(term, term.TermKind, "TermKind"), ref list);
					break;
				case EdmTermKind.Value:
					InterfaceValidator.CollectErrors(InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmTerm, EdmTermKind, IEdmValueTerm>(term, term.TermKind, "TermKind"), ref list);
					break;
				default:
					InterfaceValidator.CollectErrors(InterfaceValidator.CreateInterfaceKindValueUnexpectedError<IEdmTerm, EdmTermKind>(term, term.TermKind, "TermKind"), ref list);
					break;
				}
				return list;
			}
		}

		// Token: 0x02000233 RID: 563
		private sealed class VisitorOfIEdmValueTerm : InterfaceValidator.VisitorOfT<IEdmValueTerm>
		{
			// Token: 0x06000D26 RID: 3366 RVA: 0x00025778 File Offset: 0x00023978
			protected override IEnumerable<EdmError> VisitT(IEdmValueTerm term, List<object> followup, List<object> references)
			{
				if (term.Type != null)
				{
					followup.Add(term.Type);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmValueTerm>(term, "Type") };
			}
		}

		// Token: 0x02000234 RID: 564
		private sealed class VisitorOfIEdmCollectionType : InterfaceValidator.VisitorOfT<IEdmCollectionType>
		{
			// Token: 0x06000D28 RID: 3368 RVA: 0x000257BC File Offset: 0x000239BC
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

		// Token: 0x02000235 RID: 565
		private sealed class VisitorOfIEdmProperty : InterfaceValidator.VisitorOfT<IEdmProperty>
		{
			// Token: 0x06000D2A RID: 3370 RVA: 0x00025800 File Offset: 0x00023A00
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

		// Token: 0x02000236 RID: 566
		private sealed class VisitorOfIEdmStructuralProperty : InterfaceValidator.VisitorOfT<IEdmStructuralProperty>
		{
			// Token: 0x06000D2C RID: 3372 RVA: 0x000258D0 File Offset: 0x00023AD0
			protected override IEnumerable<EdmError> VisitT(IEdmStructuralProperty property, List<object> followup, List<object> references)
			{
				if (property.ConcurrencyMode < EdmConcurrencyMode.None || property.ConcurrencyMode > EdmConcurrencyMode.Fixed)
				{
					return new EdmError[] { InterfaceValidator.CreateEnumPropertyOutOfRangeError<IEdmStructuralProperty, EdmConcurrencyMode>(property, property.ConcurrencyMode, "ConcurrencyMode") };
				}
				return null;
			}
		}

		// Token: 0x02000237 RID: 567
		private sealed class VisitorOfIEdmNavigationProperty : InterfaceValidator.VisitorOfT<IEdmNavigationProperty>
		{
			// Token: 0x06000D2E RID: 3374 RVA: 0x00025918 File Offset: 0x00023B18
			protected override IEnumerable<EdmError> VisitT(IEdmNavigationProperty property, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				followup.Add(property.Type);
				if (property.Partner != null)
				{
					followup.Add(property.Partner);
					if (!(property.Partner is BadNavigationProperty) && (property.Partner.Partner != property || (property.Partner == property && ValidationHelper.ComputeNavigationPropertyTarget(property) != property.DeclaringEntityType())))
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

		// Token: 0x02000238 RID: 568
		private sealed class VisitorOfIEdmReferentialConstraint : InterfaceValidator.VisitorOfT<IEdmReferentialConstraint>
		{
			// Token: 0x06000D30 RID: 3376 RVA: 0x000259E0 File Offset: 0x00023BE0
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

		// Token: 0x02000239 RID: 569
		private sealed class VisitorOfIEdmEnumMember : InterfaceValidator.VisitorOfT<IEdmEnumMember>
		{
			// Token: 0x06000D32 RID: 3378 RVA: 0x00025A98 File Offset: 0x00023C98
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

		// Token: 0x0200023A RID: 570
		private sealed class VisitorOfIEdmOperation : InterfaceValidator.VisitorOfT<IEdmOperation>
		{
			// Token: 0x06000D34 RID: 3380 RVA: 0x00025B00 File Offset: 0x00023D00
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

		// Token: 0x0200023B RID: 571
		private sealed class VisitorOfIEdmAction : InterfaceValidator.VisitorOfT<IEdmAction>
		{
			// Token: 0x06000D36 RID: 3382 RVA: 0x00025B40 File Offset: 0x00023D40
			protected override IEnumerable<EdmError> VisitT(IEdmAction operation, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x0200023C RID: 572
		private sealed class VisitorOfIEdmFunction : InterfaceValidator.VisitorOfT<IEdmFunction>
		{
			// Token: 0x06000D38 RID: 3384 RVA: 0x00025B4B File Offset: 0x00023D4B
			protected override IEnumerable<EdmError> VisitT(IEdmFunction operation, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x0200023D RID: 573
		private sealed class VisitorOfIEdmOperationImport : InterfaceValidator.VisitorOfT<IEdmOperationImport>
		{
			// Token: 0x06000D3A RID: 3386 RVA: 0x00025B56 File Offset: 0x00023D56
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

		// Token: 0x0200023E RID: 574
		private sealed class VisitorOfIEdmActionImport : InterfaceValidator.VisitorOfT<IEdmActionImport>
		{
			// Token: 0x06000D3C RID: 3388 RVA: 0x00025B81 File Offset: 0x00023D81
			protected override IEnumerable<EdmError> VisitT(IEdmActionImport actionImport, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x0200023F RID: 575
		private sealed class VisitorOfIEdmFunctionImport : InterfaceValidator.VisitorOfT<IEdmFunctionImport>
		{
			// Token: 0x06000D3E RID: 3390 RVA: 0x00025B8C File Offset: 0x00023D8C
			protected override IEnumerable<EdmError> VisitT(IEdmFunctionImport functionImport, List<object> followup, List<object> references)
			{
				return null;
			}
		}

		// Token: 0x02000240 RID: 576
		private sealed class VisitorOfIEdmOperationParameter : InterfaceValidator.VisitorOfT<IEdmOperationParameter>
		{
			// Token: 0x06000D40 RID: 3392 RVA: 0x00025B98 File Offset: 0x00023D98
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

		// Token: 0x02000241 RID: 577
		private sealed class VisitorOfIEdmCollectionTypeReference : InterfaceValidator.VisitorOfT<IEdmCollectionTypeReference>
		{
			// Token: 0x06000D42 RID: 3394 RVA: 0x00025C00 File Offset: 0x00023E00
			protected override IEnumerable<EdmError> VisitT(IEdmCollectionTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Collection)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmCollectionTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000242 RID: 578
		private sealed class VisitorOfIEdmEntityReferenceTypeReference : InterfaceValidator.VisitorOfT<IEdmEntityReferenceTypeReference>
		{
			// Token: 0x06000D44 RID: 3396 RVA: 0x00025C40 File Offset: 0x00023E40
			protected override IEnumerable<EdmError> VisitT(IEdmEntityReferenceTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.EntityReference)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmEntityReferenceTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000243 RID: 579
		private sealed class VisitorOfIEdmStructuredTypeReference : InterfaceValidator.VisitorOfT<IEdmStructuredTypeReference>
		{
			// Token: 0x06000D46 RID: 3398 RVA: 0x00025C80 File Offset: 0x00023E80
			protected override IEnumerable<EdmError> VisitT(IEdmStructuredTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind.IsStructured())
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmStructuredTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000244 RID: 580
		private sealed class VisitorOfIEdmEntityTypeReference : InterfaceValidator.VisitorOfT<IEdmEntityTypeReference>
		{
			// Token: 0x06000D48 RID: 3400 RVA: 0x00025CC4 File Offset: 0x00023EC4
			protected override IEnumerable<EdmError> VisitT(IEdmEntityTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Entity)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmEntityTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000245 RID: 581
		private sealed class VisitorOfIEdmComplexTypeReference : InterfaceValidator.VisitorOfT<IEdmComplexTypeReference>
		{
			// Token: 0x06000D4A RID: 3402 RVA: 0x00025D04 File Offset: 0x00023F04
			protected override IEnumerable<EdmError> VisitT(IEdmComplexTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Complex)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmComplexTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000246 RID: 582
		private sealed class VisitorOfIEdmEnumTypeReference : InterfaceValidator.VisitorOfT<IEdmEnumTypeReference>
		{
			// Token: 0x06000D4C RID: 3404 RVA: 0x00025D44 File Offset: 0x00023F44
			protected override IEnumerable<EdmError> VisitT(IEdmEnumTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Enum)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmEnumTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000247 RID: 583
		private sealed class VisitorOfIEdmTypeDefinitionReference : InterfaceValidator.VisitorOfT<IEdmTypeDefinitionReference>
		{
			// Token: 0x06000D4E RID: 3406 RVA: 0x00025D84 File Offset: 0x00023F84
			protected override IEnumerable<EdmError> VisitT(IEdmTypeDefinitionReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.TypeDefinition)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmTypeDefinitionReference>(typeRef) };
			}
		}

		// Token: 0x02000248 RID: 584
		private sealed class VisitorOfIEdmPrimitiveTypeReference : InterfaceValidator.VisitorOfT<IEdmPrimitiveTypeReference>
		{
			// Token: 0x06000D50 RID: 3408 RVA: 0x00025DC4 File Offset: 0x00023FC4
			protected override IEnumerable<EdmError> VisitT(IEdmPrimitiveTypeReference typeRef, List<object> followup, List<object> references)
			{
				if (typeRef.Definition == null || typeRef.Definition.TypeKind == EdmTypeKind.Primitive)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreateTypeRefInterfaceTypeKindValueMismatchError<IEdmPrimitiveTypeReference>(typeRef) };
			}
		}

		// Token: 0x02000249 RID: 585
		private sealed class VisitorOfIEdmBinaryTypeReference : InterfaceValidator.VisitorOfT<IEdmBinaryTypeReference>
		{
			// Token: 0x06000D52 RID: 3410 RVA: 0x00025E04 File Offset: 0x00024004
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

		// Token: 0x0200024A RID: 586
		private sealed class VisitorOfIEdmDecimalTypeReference : InterfaceValidator.VisitorOfT<IEdmDecimalTypeReference>
		{
			// Token: 0x06000D54 RID: 3412 RVA: 0x00025E44 File Offset: 0x00024044
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

		// Token: 0x0200024B RID: 587
		private sealed class VisitorOfIEdmStringTypeReference : InterfaceValidator.VisitorOfT<IEdmStringTypeReference>
		{
			// Token: 0x06000D56 RID: 3414 RVA: 0x00025E84 File Offset: 0x00024084
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

		// Token: 0x0200024C RID: 588
		private sealed class VisitorOfIEdmTemporalTypeReference : InterfaceValidator.VisitorOfT<IEdmTemporalTypeReference>
		{
			// Token: 0x06000D58 RID: 3416 RVA: 0x00025EC8 File Offset: 0x000240C8
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

		// Token: 0x0200024D RID: 589
		private sealed class VisitorOfIEdmSpatialTypeReference : InterfaceValidator.VisitorOfT<IEdmSpatialTypeReference>
		{
			// Token: 0x06000D5A RID: 3418 RVA: 0x00025F0C File Offset: 0x0002410C
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

		// Token: 0x0200024E RID: 590
		private sealed class VisitorOfIEdmReference : InterfaceValidator.VisitorOfT<IEdmReference>
		{
			// Token: 0x06000D5C RID: 3420 RVA: 0x00025F50 File Offset: 0x00024150
			protected override IEnumerable<EdmError> VisitT(IEdmReference edmReference, List<object> followup, List<object> references)
			{
				if (Enumerable.Any<IEdmInclude>(edmReference.Includes) || !Enumerable.Any<IEdmIncludeAnnotations>(edmReference.IncludeAnnotations))
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmReference>(edmReference, "Includes/IncludeAnnotations") };
			}
		}

		// Token: 0x0200024F RID: 591
		private sealed class VisitorOfIEdmInclude : InterfaceValidator.VisitorOfT<IEdmInclude>
		{
			// Token: 0x06000D5E RID: 3422 RVA: 0x00025F98 File Offset: 0x00024198
			protected override IEnumerable<EdmError> VisitT(IEdmInclude edmInclude, List<object> followup, List<object> references)
			{
				if (!string.IsNullOrEmpty(edmInclude.Namespace))
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmInclude>(edmInclude, "Namespace") };
			}
		}

		// Token: 0x02000250 RID: 592
		private sealed class VisitorOfIEdmIncludeAnnotations : InterfaceValidator.VisitorOfT<IEdmIncludeAnnotations>
		{
			// Token: 0x06000D60 RID: 3424 RVA: 0x00025FD4 File Offset: 0x000241D4
			protected override IEnumerable<EdmError> VisitT(IEdmIncludeAnnotations edmIncludeAnnotations, List<object> followup, List<object> references)
			{
				if (!string.IsNullOrEmpty(edmIncludeAnnotations.TermNamespace))
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmIncludeAnnotations>(edmIncludeAnnotations, "TermNamespace") };
			}
		}

		// Token: 0x02000251 RID: 593
		private sealed class VisitorOfIEdmExpression : InterfaceValidator.VisitorOfT<IEdmExpression>
		{
			// Token: 0x06000D62 RID: 3426 RVA: 0x00026010 File Offset: 0x00024210
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
					case EdmExpressionKind.ParameterReference:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmParameterReferenceExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.OperationReference:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmOperationReferenceExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.PropertyReference:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmPropertyReferenceExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.ValueTermReference:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmValueTermReferenceExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.EntitySetReference:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmEntitySetReferenceExpression>(expression, expression.ExpressionKind, "ExpressionKind");
						break;
					case EdmExpressionKind.EnumMemberReference:
						edmError = InterfaceValidator.CheckForInterfaceKindValueMismatchError<IEdmExpression, EdmExpressionKind, IEdmEnumMemberReferenceExpression>(expression, expression.ExpressionKind, "ExpressionKind");
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
					case EdmExpressionKind.OperationApplication:
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

		// Token: 0x02000252 RID: 594
		private sealed class VisitorOfIEdmRecordExpression : InterfaceValidator.VisitorOfT<IEdmRecordExpression>
		{
			// Token: 0x06000D64 RID: 3428 RVA: 0x00026354 File Offset: 0x00024554
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

		// Token: 0x02000253 RID: 595
		private sealed class VisitorOfIEdmPropertyConstructor : InterfaceValidator.VisitorOfT<IEdmPropertyConstructor>
		{
			// Token: 0x06000D66 RID: 3430 RVA: 0x00026394 File Offset: 0x00024594
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

		// Token: 0x02000254 RID: 596
		private sealed class VisitorOfIEdmCollectionExpression : InterfaceValidator.VisitorOfT<IEdmCollectionExpression>
		{
			// Token: 0x06000D68 RID: 3432 RVA: 0x000263F0 File Offset: 0x000245F0
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

		// Token: 0x02000255 RID: 597
		private sealed class VisitorOfIEdmLabeledElement : InterfaceValidator.VisitorOfT<IEdmLabeledExpression>
		{
			// Token: 0x06000D6A RID: 3434 RVA: 0x00026430 File Offset: 0x00024630
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

		// Token: 0x02000256 RID: 598
		private sealed class VisitorOfIEdmPathExpression : InterfaceValidator.VisitorOfT<IEdmPathExpression>
		{
			// Token: 0x06000D6C RID: 3436 RVA: 0x00026474 File Offset: 0x00024674
			protected override IEnumerable<EdmError> VisitT(IEdmPathExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				List<string> list2 = new List<string>();
				InterfaceValidator.ProcessEnumerable<IEdmPathExpression, string>(expression, expression.Path, "Path", list2, ref list);
				return list;
			}
		}

		// Token: 0x02000257 RID: 599
		private sealed class VisitorOfIEdmParameterReferenceExpression : InterfaceValidator.VisitorOfT<IEdmParameterReferenceExpression>
		{
			// Token: 0x06000D6E RID: 3438 RVA: 0x000264A8 File Offset: 0x000246A8
			protected override IEnumerable<EdmError> VisitT(IEdmParameterReferenceExpression expression, List<object> followup, List<object> references)
			{
				if (expression.ReferencedParameter != null)
				{
					references.Add(expression.ReferencedParameter);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmParameterReferenceExpression>(expression, "ReferencedParameter") };
			}
		}

		// Token: 0x02000258 RID: 600
		private sealed class VisitorOfIEdmFunctionReferenceExpression : InterfaceValidator.VisitorOfT<IEdmOperationReferenceExpression>
		{
			// Token: 0x06000D70 RID: 3440 RVA: 0x000264EC File Offset: 0x000246EC
			protected override IEnumerable<EdmError> VisitT(IEdmOperationReferenceExpression expression, List<object> followup, List<object> references)
			{
				if (expression.ReferencedOperation != null)
				{
					references.Add(expression.ReferencedOperation);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmOperationReferenceExpression>(expression, "ReferencedFunction") };
			}
		}

		// Token: 0x02000259 RID: 601
		private sealed class VisitorOfIEdmPropertyReferenceExpression : InterfaceValidator.VisitorOfT<IEdmPropertyReferenceExpression>
		{
			// Token: 0x06000D72 RID: 3442 RVA: 0x00026530 File Offset: 0x00024730
			protected override IEnumerable<EdmError> VisitT(IEdmPropertyReferenceExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (expression.Base != null)
				{
					followup.Add(expression.Base);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmPropertyReferenceExpression>(expression, "Base"), ref list);
				}
				if (expression.ReferencedProperty != null)
				{
					references.Add(expression.ReferencedProperty);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmPropertyReferenceExpression>(expression, "ReferencedProperty"), ref list);
				}
				return list;
			}
		}

		// Token: 0x0200025A RID: 602
		private sealed class VisitorOfIEdmValueTermReferenceExpression : InterfaceValidator.VisitorOfT<IEdmValueTermReferenceExpression>
		{
			// Token: 0x06000D74 RID: 3444 RVA: 0x00026598 File Offset: 0x00024798
			protected override IEnumerable<EdmError> VisitT(IEdmValueTermReferenceExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (expression.Base != null)
				{
					followup.Add(expression.Base);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmValueTermReferenceExpression>(expression, "Base"), ref list);
				}
				if (expression.Term != null)
				{
					references.Add(expression.Term);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmValueTermReferenceExpression>(expression, "Term"), ref list);
				}
				return list;
			}
		}

		// Token: 0x0200025B RID: 603
		private sealed class VistorOfIEdmEntitySetReferenceExpression : InterfaceValidator.VisitorOfT<IEdmEntitySetReferenceExpression>
		{
			// Token: 0x06000D76 RID: 3446 RVA: 0x00026600 File Offset: 0x00024800
			protected override IEnumerable<EdmError> VisitT(IEdmEntitySetReferenceExpression expression, List<object> followup, List<object> references)
			{
				if (expression.ReferencedEntitySet != null)
				{
					references.Add(expression.ReferencedEntitySet);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmEntitySetReferenceExpression>(expression, "ReferencedEntitySet") };
			}
		}

		// Token: 0x0200025C RID: 604
		private sealed class VistorOfIEdmEnumMemberReferenceExpression : InterfaceValidator.VisitorOfT<IEdmEnumMemberReferenceExpression>
		{
			// Token: 0x06000D78 RID: 3448 RVA: 0x00026644 File Offset: 0x00024844
			protected override IEnumerable<EdmError> VisitT(IEdmEnumMemberReferenceExpression expression, List<object> followup, List<object> references)
			{
				if (expression.ReferencedEnumMember != null)
				{
					references.Add(expression.ReferencedEnumMember);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmEnumMemberReferenceExpression>(expression, "ReferencedEnumMember") };
			}
		}

		// Token: 0x0200025D RID: 605
		private sealed class VistorOfIEdmEnumMemberExpression : InterfaceValidator.VisitorOfT<IEdmEnumMemberExpression>
		{
			// Token: 0x06000D7A RID: 3450 RVA: 0x00026688 File Offset: 0x00024888
			protected override IEnumerable<EdmError> VisitT(IEdmEnumMemberExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmEnumMemberExpression, IEdmEnumMember>(expression, expression.EnumMembers, "EnumMembers", followup, ref list);
				return list;
			}
		}

		// Token: 0x0200025E RID: 606
		private sealed class VistorOfIEdmIfExpression : InterfaceValidator.VisitorOfT<IEdmIfExpression>
		{
			// Token: 0x06000D7C RID: 3452 RVA: 0x000266B4 File Offset: 0x000248B4
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

		// Token: 0x0200025F RID: 607
		private sealed class VistorOfIEdmCastExpression : InterfaceValidator.VisitorOfT<IEdmCastExpression>
		{
			// Token: 0x06000D7E RID: 3454 RVA: 0x00026744 File Offset: 0x00024944
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

		// Token: 0x02000260 RID: 608
		private sealed class VistorOfIEdmIsTypeExpression : InterfaceValidator.VisitorOfT<IEdmIsTypeExpression>
		{
			// Token: 0x06000D80 RID: 3456 RVA: 0x000267AC File Offset: 0x000249AC
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

		// Token: 0x02000261 RID: 609
		private sealed class VistorOfIEdmFunctionApplicationExpression : InterfaceValidator.VisitorOfT<IEdmApplyExpression>
		{
			// Token: 0x06000D82 RID: 3458 RVA: 0x00026814 File Offset: 0x00024A14
			protected override IEnumerable<EdmError> VisitT(IEdmApplyExpression expression, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				if (expression.AppliedOperation != null)
				{
					followup.Add(expression.AppliedOperation);
				}
				else
				{
					InterfaceValidator.CollectErrors(InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmApplyExpression>(expression, "AppliedFunction"), ref list);
				}
				InterfaceValidator.ProcessEnumerable<IEdmApplyExpression, IEdmExpression>(expression, expression.Arguments, "Arguments", followup, ref list);
				return list;
			}
		}

		// Token: 0x02000262 RID: 610
		private sealed class VistorOfIEdmLabeledElementReferenceExpression : InterfaceValidator.VisitorOfT<IEdmLabeledExpressionReferenceExpression>
		{
			// Token: 0x06000D84 RID: 3460 RVA: 0x00026868 File Offset: 0x00024A68
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

		// Token: 0x02000263 RID: 611
		private sealed class VisitorOfIEdmValue : InterfaceValidator.VisitorOfT<IEdmValue>
		{
			// Token: 0x06000D86 RID: 3462 RVA: 0x000268AC File Offset: 0x00024AAC
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

		// Token: 0x02000264 RID: 612
		private sealed class VisitorOfIEdmDelayedValue : InterfaceValidator.VisitorOfT<IEdmDelayedValue>
		{
			// Token: 0x06000D88 RID: 3464 RVA: 0x00026AEC File Offset: 0x00024CEC
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

		// Token: 0x02000265 RID: 613
		private sealed class VisitorOfIEdmPropertyValue : InterfaceValidator.VisitorOfT<IEdmPropertyValue>
		{
			// Token: 0x06000D8A RID: 3466 RVA: 0x00026B30 File Offset: 0x00024D30
			protected override IEnumerable<EdmError> VisitT(IEdmPropertyValue value, List<object> followup, List<object> references)
			{
				if (value.Name != null)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmPropertyValue>(value, "Name") };
			}
		}

		// Token: 0x02000266 RID: 614
		private sealed class VisitorOfIEdmEnumValue : InterfaceValidator.VisitorOfT<IEdmEnumValue>
		{
			// Token: 0x06000D8C RID: 3468 RVA: 0x00026B68 File Offset: 0x00024D68
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

		// Token: 0x02000267 RID: 615
		private sealed class VisitorOfIEdmCollectionValue : InterfaceValidator.VisitorOfT<IEdmCollectionValue>
		{
			// Token: 0x06000D8E RID: 3470 RVA: 0x00026BAC File Offset: 0x00024DAC
			protected override IEnumerable<EdmError> VisitT(IEdmCollectionValue value, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmCollectionValue, IEdmDelayedValue>(value, value.Elements, "Elements", followup, ref list);
				return list;
			}
		}

		// Token: 0x02000268 RID: 616
		private sealed class VisitorOfIEdmStructuredValue : InterfaceValidator.VisitorOfT<IEdmStructuredValue>
		{
			// Token: 0x06000D90 RID: 3472 RVA: 0x00026BD8 File Offset: 0x00024DD8
			protected override IEnumerable<EdmError> VisitT(IEdmStructuredValue value, List<object> followup, List<object> references)
			{
				List<EdmError> list = null;
				InterfaceValidator.ProcessEnumerable<IEdmStructuredValue, IEdmPropertyValue>(value, value.PropertyValues, "PropertyValues", followup, ref list);
				return list;
			}
		}

		// Token: 0x02000269 RID: 617
		private sealed class VisitorOfIEdmBinaryValue : InterfaceValidator.VisitorOfT<IEdmBinaryValue>
		{
			// Token: 0x06000D92 RID: 3474 RVA: 0x00026C04 File Offset: 0x00024E04
			protected override IEnumerable<EdmError> VisitT(IEdmBinaryValue value, List<object> followup, List<object> references)
			{
				if (value.Value != null)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmBinaryValue>(value, "Value") };
			}
		}

		// Token: 0x0200026A RID: 618
		private sealed class VisitorOfIEdmStringValue : InterfaceValidator.VisitorOfT<IEdmStringValue>
		{
			// Token: 0x06000D94 RID: 3476 RVA: 0x00026C3C File Offset: 0x00024E3C
			protected override IEnumerable<EdmError> VisitT(IEdmStringValue value, List<object> followup, List<object> references)
			{
				if (value.Value != null)
				{
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmStringValue>(value, "Value") };
			}
		}

		// Token: 0x0200026B RID: 619
		private sealed class VisitorOfIEdmVocabularyAnnotation : InterfaceValidator.VisitorOfT<IEdmVocabularyAnnotation>
		{
			// Token: 0x06000D96 RID: 3478 RVA: 0x00026C74 File Offset: 0x00024E74
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
				return list;
			}
		}

		// Token: 0x0200026C RID: 620
		private sealed class VisitorOfIEdmValueAnnotation : InterfaceValidator.VisitorOfT<IEdmValueAnnotation>
		{
			// Token: 0x06000D98 RID: 3480 RVA: 0x00026CDC File Offset: 0x00024EDC
			protected override IEnumerable<EdmError> VisitT(IEdmValueAnnotation annotation, List<object> followup, List<object> references)
			{
				if (annotation.Value != null)
				{
					followup.Add(annotation.Value);
					return null;
				}
				return new EdmError[] { InterfaceValidator.CreatePropertyMustNotBeNullError<IEdmValueAnnotation>(annotation, "Value") };
			}
		}

		// Token: 0x0200026D RID: 621
		private sealed class VisitorOfIEdmPropertyValueBinding : InterfaceValidator.VisitorOfT<IEdmPropertyValueBinding>
		{
			// Token: 0x06000D9A RID: 3482 RVA: 0x00026D20 File Offset: 0x00024F20
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

		// Token: 0x0200026E RID: 622
		private sealed class VisitorOfIEdmDirectValueAnnotation : InterfaceValidator.VisitorOfT<IEdmDirectValueAnnotation>
		{
			// Token: 0x06000D9C RID: 3484 RVA: 0x00026D88 File Offset: 0x00024F88
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
