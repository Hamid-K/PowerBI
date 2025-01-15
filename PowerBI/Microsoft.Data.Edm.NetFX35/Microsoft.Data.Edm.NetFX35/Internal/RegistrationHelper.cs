using System;
using System.Collections.Generic;
using Microsoft.Data.Edm.Library.Internal;

namespace Microsoft.Data.Edm.Internal
{
	// Token: 0x020001DF RID: 479
	internal static class RegistrationHelper
	{
		// Token: 0x06000B60 RID: 2912 RVA: 0x0002101C File Offset: 0x0001F21C
		internal static void RegisterSchemaElement(IEdmSchemaElement element, Dictionary<string, IEdmSchemaType> schemaTypeDictionary, Dictionary<string, IEdmValueTerm> valueTermDictionary, Dictionary<string, object> functionGroupDictionary, Dictionary<string, IEdmEntityContainer> containerDictionary)
		{
			string text = element.FullName();
			switch (element.SchemaElementKind)
			{
			case EdmSchemaElementKind.None:
				throw new InvalidOperationException(Strings.EdmModel_CannotUseElementWithTypeNone);
			case EdmSchemaElementKind.TypeDefinition:
				RegistrationHelper.AddElement<IEdmSchemaType>((IEdmSchemaType)element, text, schemaTypeDictionary, new Func<IEdmSchemaType, IEdmSchemaType, IEdmSchemaType>(RegistrationHelper.CreateAmbiguousTypeBinding));
				return;
			case EdmSchemaElementKind.Function:
				RegistrationHelper.AddFunction<IEdmFunction>((IEdmFunction)element, text, functionGroupDictionary);
				return;
			case EdmSchemaElementKind.ValueTerm:
				RegistrationHelper.AddElement<IEdmValueTerm>((IEdmValueTerm)element, text, valueTermDictionary, new Func<IEdmValueTerm, IEdmValueTerm, IEdmValueTerm>(RegistrationHelper.CreateAmbiguousValueTermBinding));
				return;
			case EdmSchemaElementKind.EntityContainer:
				RegistrationHelper.AddElement<IEdmEntityContainer>((IEdmEntityContainer)element, text, containerDictionary, new Func<IEdmEntityContainer, IEdmEntityContainer, IEdmEntityContainer>(RegistrationHelper.CreateAmbiguousEntityContainerBinding));
				RegistrationHelper.AddElement<IEdmEntityContainer>((IEdmEntityContainer)element, element.Name, containerDictionary, new Func<IEdmEntityContainer, IEdmEntityContainer, IEdmEntityContainer>(RegistrationHelper.CreateAmbiguousEntityContainerBinding));
				return;
			default:
				throw new InvalidOperationException(Strings.UnknownEnumVal_SchemaElementKind(element.SchemaElementKind));
			}
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x000210F2 File Offset: 0x0001F2F2
		internal static void RegisterProperty(IEdmProperty element, string name, Dictionary<string, IEdmProperty> dictionary)
		{
			RegistrationHelper.AddElement<IEdmProperty>(element, name, dictionary, new Func<IEdmProperty, IEdmProperty, IEdmProperty>(RegistrationHelper.CreateAmbiguousPropertyBinding));
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00021108 File Offset: 0x0001F308
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

		// Token: 0x06000B63 RID: 2915 RVA: 0x00021138 File Offset: 0x0001F338
		internal static void AddFunction<T>(T function, string name, Dictionary<string, object> functionListDictionary) where T : class, IEdmFunctionBase
		{
			object obj = null;
			if (functionListDictionary.TryGetValue(name, ref obj))
			{
				List<T> list = obj as List<T>;
				if (list == null)
				{
					T t = (T)((object)obj);
					list = new List<T>();
					list.Add(t);
					functionListDictionary[name] = list;
				}
				list.Add(function);
				return;
			}
			functionListDictionary[name] = function;
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00021190 File Offset: 0x0001F390
		internal static IEdmSchemaType CreateAmbiguousTypeBinding(IEdmSchemaType first, IEdmSchemaType second)
		{
			AmbiguousTypeBinding ambiguousTypeBinding = first as AmbiguousTypeBinding;
			if (ambiguousTypeBinding != null)
			{
				ambiguousTypeBinding.AddBinding(second);
				return ambiguousTypeBinding;
			}
			return new AmbiguousTypeBinding(first, second);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x000211B8 File Offset: 0x0001F3B8
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

		// Token: 0x06000B66 RID: 2918 RVA: 0x000211E0 File Offset: 0x0001F3E0
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

		// Token: 0x06000B67 RID: 2919 RVA: 0x00021208 File Offset: 0x0001F408
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

		// Token: 0x06000B68 RID: 2920 RVA: 0x00021230 File Offset: 0x0001F430
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
