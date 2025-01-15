using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200001B RID: 27
	internal static class RegistrationHelper
	{
		// Token: 0x0600017F RID: 383 RVA: 0x00007580 File Offset: 0x00005780
		internal static void RegisterSchemaElement(IEdmSchemaElement element, Dictionary<string, IEdmSchemaType> schemaTypeDictionary, Dictionary<string, IEdmTerm> valueTermDictionary, Dictionary<string, IList<IEdmOperation>> functionGroupDictionary, Dictionary<string, IEdmEntityContainer> containerDictionary)
		{
			string text = element.FullName();
			switch (element.SchemaElementKind)
			{
			case EdmSchemaElementKind.None:
				throw new InvalidOperationException(Strings.EdmModel_CannotUseElementWithTypeNone);
			case EdmSchemaElementKind.TypeDefinition:
				RegistrationHelper.AddElement<IEdmSchemaType>((IEdmSchemaType)element, text, schemaTypeDictionary, new Func<IEdmSchemaType, IEdmSchemaType, IEdmSchemaType>(RegistrationHelper.CreateAmbiguousTypeBinding));
				return;
			case EdmSchemaElementKind.Term:
				RegistrationHelper.AddElement<IEdmTerm>((IEdmTerm)element, text, valueTermDictionary, new Func<IEdmTerm, IEdmTerm, IEdmTerm>(RegistrationHelper.CreateAmbiguousTermBinding));
				return;
			case EdmSchemaElementKind.Action:
			case EdmSchemaElementKind.Function:
				RegistrationHelper.AddOperation((IEdmOperation)element, text, functionGroupDictionary);
				return;
			case EdmSchemaElementKind.EntityContainer:
			{
				if (containerDictionary.Count > 0)
				{
					throw new InvalidOperationException(Strings.EdmModel_CannotAddMoreThanOneEntityContainerToOneEdmModel);
				}
				IEdmEntityContainer edmEntityContainer = (IEdmEntityContainer)element;
				RegistrationHelper.AddElement<IEdmEntityContainer>(edmEntityContainer, text, containerDictionary, new Func<IEdmEntityContainer, IEdmEntityContainer, IEdmEntityContainer>(RegistrationHelper.CreateAmbiguousEntityContainerBinding));
				RegistrationHelper.AddElement<IEdmEntityContainer>(edmEntityContainer, element.Name, containerDictionary, new Func<IEdmEntityContainer, IEdmEntityContainer, IEdmEntityContainer>(RegistrationHelper.CreateAmbiguousEntityContainerBinding));
				return;
			}
			default:
				throw new InvalidOperationException(Strings.UnknownEnumVal_SchemaElementKind(element.SchemaElementKind));
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000766C File Offset: 0x0000586C
		internal static void RegisterProperty(IEdmProperty element, string name, Dictionary<string, IEdmProperty> dictionary)
		{
			RegistrationHelper.AddElement<IEdmProperty>(element, name, dictionary, new Func<IEdmProperty, IEdmProperty, IEdmProperty>(RegistrationHelper.CreateAmbiguousPropertyBinding));
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007684 File Offset: 0x00005884
		internal static void AddElement<T>(T element, string name, Dictionary<string, T> elementDictionary, Func<T, T, T> ambiguityCreator) where T : class, IEdmElement
		{
			T t;
			if (elementDictionary.TryGetValue(name, ref t))
			{
				elementDictionary[name] = ambiguityCreator.Invoke(t, element);
				return;
			}
			elementDictionary[name] = element;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000076B4 File Offset: 0x000058B4
		internal static void AddOperation(IEdmOperation operation, string name, Dictionary<string, IList<IEdmOperation>> operationListDictionary)
		{
			IList<IEdmOperation> list = null;
			if (!operationListDictionary.TryGetValue(name, ref list))
			{
				list = new List<IEdmOperation>();
				operationListDictionary.Add(name, list);
			}
			list.Add(operation);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000076E4 File Offset: 0x000058E4
		internal static void AddOperationImport(IEdmOperationImport operationImport, string name, Dictionary<string, object> operationListDictionary)
		{
			object obj = null;
			if (operationListDictionary.TryGetValue(name, ref obj))
			{
				List<IEdmOperationImport> list = obj as List<IEdmOperationImport>;
				if (list == null)
				{
					IEdmOperationImport edmOperationImport = (IEdmOperationImport)obj;
					list = new List<IEdmOperationImport>();
					list.Add(edmOperationImport);
					operationListDictionary[name] = list;
				}
				list.Add(operationImport);
				return;
			}
			operationListDictionary[name] = operationImport;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00007734 File Offset: 0x00005934
		internal static IEdmSchemaType CreateAmbiguousTypeBinding(IEdmSchemaType first, IEdmSchemaType second)
		{
			if (first == second)
			{
				return first;
			}
			AmbiguousTypeBinding ambiguousTypeBinding = first as AmbiguousTypeBinding;
			if (ambiguousTypeBinding != null)
			{
				ambiguousTypeBinding.AddBinding(second);
				return ambiguousTypeBinding;
			}
			return new AmbiguousTypeBinding(first, second);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007764 File Offset: 0x00005964
		internal static IEdmTerm CreateAmbiguousTermBinding(IEdmTerm first, IEdmTerm second)
		{
			AmbiguousTermBinding ambiguousTermBinding = first as AmbiguousTermBinding;
			if (ambiguousTermBinding != null)
			{
				ambiguousTermBinding.AddBinding(second);
				return ambiguousTermBinding;
			}
			return new AmbiguousTermBinding(first, second);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000778C File Offset: 0x0000598C
		internal static IEdmEntitySet CreateAmbiguousEntitySetBinding(IEdmEntitySet first, IEdmEntitySet second)
		{
			AmbiguousEntitySetBinding ambiguousEntitySetBinding = first as AmbiguousEntitySetBinding;
			if (ambiguousEntitySetBinding != null)
			{
				ambiguousEntitySetBinding.AddBinding(second);
				return ambiguousEntitySetBinding;
			}
			return new AmbiguousEntitySetBinding(first, second);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000077B4 File Offset: 0x000059B4
		internal static IEdmSingleton CreateAmbiguousSingletonBinding(IEdmSingleton first, IEdmSingleton second)
		{
			AmbiguousSingletonBinding ambiguousSingletonBinding = first as AmbiguousSingletonBinding;
			if (ambiguousSingletonBinding != null)
			{
				ambiguousSingletonBinding.AddBinding(second);
				return ambiguousSingletonBinding;
			}
			return new AmbiguousSingletonBinding(first, second);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000077DC File Offset: 0x000059DC
		internal static IEdmEntityContainer CreateAmbiguousEntityContainerBinding(IEdmEntityContainer first, IEdmEntityContainer second)
		{
			AmbiguousEntityContainerBinding ambiguousEntityContainerBinding = first as AmbiguousEntityContainerBinding;
			if (ambiguousEntityContainerBinding != null)
			{
				ambiguousEntityContainerBinding.AddBinding(second);
				return ambiguousEntityContainerBinding;
			}
			return new AmbiguousEntityContainerBinding(first, second);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00007804 File Offset: 0x00005A04
		private static IEdmProperty CreateAmbiguousPropertyBinding(IEdmProperty first, IEdmProperty second)
		{
			AmbiguousPropertyBinding ambiguousPropertyBinding = first as AmbiguousPropertyBinding;
			if (ambiguousPropertyBinding != null)
			{
				ambiguousPropertyBinding.AddBinding(second);
				return ambiguousPropertyBinding;
			}
			return new AmbiguousPropertyBinding(first.DeclaringType, first, second);
		}
	}
}
