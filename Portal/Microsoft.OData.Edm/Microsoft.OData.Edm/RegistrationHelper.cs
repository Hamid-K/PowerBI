using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000C7 RID: 199
	internal static class RegistrationHelper
	{
		// Token: 0x060004C2 RID: 1218 RVA: 0x0000C070 File Offset: 0x0000A270
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

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000C15C File Offset: 0x0000A35C
		internal static void RegisterProperty(IEdmProperty element, string name, Dictionary<string, IEdmProperty> dictionary)
		{
			RegistrationHelper.AddElement<IEdmProperty>(element, name, dictionary, new Func<IEdmProperty, IEdmProperty, IEdmProperty>(RegistrationHelper.CreateAmbiguousPropertyBinding));
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000C174 File Offset: 0x0000A374
		internal static void AddElement<T>(T element, string name, Dictionary<string, T> elementDictionary, Func<T, T, T> ambiguityCreator) where T : class, IEdmElement
		{
			T t;
			if (elementDictionary.TryGetValue(name, out t))
			{
				elementDictionary[name] = ambiguityCreator(t, element);
				return;
			}
			elementDictionary[name] = element;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000C1A4 File Offset: 0x0000A3A4
		internal static void AddOperation(IEdmOperation operation, string name, Dictionary<string, IList<IEdmOperation>> operationListDictionary)
		{
			IList<IEdmOperation> list = null;
			if (!operationListDictionary.TryGetValue(name, out list))
			{
				list = new List<IEdmOperation>();
				operationListDictionary.Add(name, list);
			}
			list.Add(operation);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000C1D4 File Offset: 0x0000A3D4
		internal static void AddOperationImport(IEdmOperationImport operationImport, string name, Dictionary<string, object> operationListDictionary)
		{
			object obj = null;
			if (operationListDictionary.TryGetValue(name, out obj))
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

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000C224 File Offset: 0x0000A424
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

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000C254 File Offset: 0x0000A454
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

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000C27C File Offset: 0x0000A47C
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

		// Token: 0x060004CA RID: 1226 RVA: 0x0000C2A4 File Offset: 0x0000A4A4
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

		// Token: 0x060004CB RID: 1227 RVA: 0x0000C2CC File Offset: 0x0000A4CC
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

		// Token: 0x060004CC RID: 1228 RVA: 0x0000C2F4 File Offset: 0x0000A4F4
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
