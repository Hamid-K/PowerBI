using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200020A RID: 522
	internal static class RegistrationHelper
	{
		// Token: 0x06000C4C RID: 3148 RVA: 0x00022AB4 File Offset: 0x00020CB4
		internal static void RegisterSchemaElement(IEdmSchemaElement element, Dictionary<string, IEdmSchemaType> schemaTypeDictionary, Dictionary<string, IEdmValueTerm> valueTermDictionary, Dictionary<string, IList<IEdmOperation>> functionGroupDictionary, Dictionary<string, IEdmEntityContainer> containerDictionary)
		{
			string text = element.FullName();
			switch (element.SchemaElementKind)
			{
			case EdmSchemaElementKind.None:
				throw new InvalidOperationException(Strings.EdmModel_CannotUseElementWithTypeNone);
			case EdmSchemaElementKind.TypeDefinition:
				RegistrationHelper.AddElement<IEdmSchemaType>((IEdmSchemaType)element, text, schemaTypeDictionary, new Func<IEdmSchemaType, IEdmSchemaType, IEdmSchemaType>(RegistrationHelper.CreateAmbiguousTypeBinding));
				return;
			case EdmSchemaElementKind.ValueTerm:
				RegistrationHelper.AddElement<IEdmValueTerm>((IEdmValueTerm)element, text, valueTermDictionary, new Func<IEdmValueTerm, IEdmValueTerm, IEdmValueTerm>(RegistrationHelper.CreateAmbiguousValueTermBinding));
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

		// Token: 0x06000C4D RID: 3149 RVA: 0x00022BA0 File Offset: 0x00020DA0
		internal static void RegisterProperty(IEdmProperty element, string name, Dictionary<string, IEdmProperty> dictionary)
		{
			RegistrationHelper.AddElement<IEdmProperty>(element, name, dictionary, new Func<IEdmProperty, IEdmProperty, IEdmProperty>(RegistrationHelper.CreateAmbiguousPropertyBinding));
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00022BB8 File Offset: 0x00020DB8
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

		// Token: 0x06000C4F RID: 3151 RVA: 0x00022BE8 File Offset: 0x00020DE8
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

		// Token: 0x06000C50 RID: 3152 RVA: 0x00022C18 File Offset: 0x00020E18
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

		// Token: 0x06000C51 RID: 3153 RVA: 0x00022C68 File Offset: 0x00020E68
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

		// Token: 0x06000C52 RID: 3154 RVA: 0x00022C98 File Offset: 0x00020E98
		internal static IEdmValueTerm CreateAmbiguousValueTermBinding(IEdmValueTerm first, IEdmValueTerm second)
		{
			AmbiguousValueTermBinding ambiguousValueTermBinding = first as AmbiguousValueTermBinding;
			if (ambiguousValueTermBinding != null)
			{
				ambiguousValueTermBinding.AddBinding(second);
				return ambiguousValueTermBinding;
			}
			return new AmbiguousValueTermBinding(first, second);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00022CC0 File Offset: 0x00020EC0
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

		// Token: 0x06000C54 RID: 3156 RVA: 0x00022CE8 File Offset: 0x00020EE8
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

		// Token: 0x06000C55 RID: 3157 RVA: 0x00022D10 File Offset: 0x00020F10
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

		// Token: 0x06000C56 RID: 3158 RVA: 0x00022D38 File Offset: 0x00020F38
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
