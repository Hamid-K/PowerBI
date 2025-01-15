using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Csdl;
using Microsoft.OData.Edm.Csdl.CsdlSemantics;
using Microsoft.OData.Edm.Csdl.Serialization;
using Microsoft.OData.Edm.Evaluation;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Library.Annotations;
using Microsoft.OData.Edm.Library.Expressions;
using Microsoft.OData.Edm.Library.Values;
using Microsoft.OData.Edm.PrimitiveValueConverters;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Values;
using Microsoft.OData.Edm.Vocabularies.Community.V1;
using Microsoft.OData.Edm.Vocabularies.V1;

namespace Microsoft.OData.Edm
{
	// Token: 0x020001E2 RID: 482
	public static class ExtensionMethods
	{
		// Token: 0x06000ACD RID: 2765 RVA: 0x0001D74D File Offset: 0x0001B94D
		public static Version GetEdmVersion(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "EdmVersion");
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0001D76C File Offset: 0x0001B96C
		public static void SetEdmVersion(this IEdmModel model, Version version)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "EdmVersion", version);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0001D78C File Offset: 0x0001B98C
		public static IEdmSchemaType FindType(this IEdmModel model, string qualifiedName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			return model.FindAcrossModels(qualifiedName, ExtensionMethods.findType, new Func<IEdmSchemaType, IEdmSchemaType, IEdmSchemaType>(RegistrationHelper.CreateAmbiguousTypeBinding));
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0001D7BE File Offset: 0x0001B9BE
		public static IEnumerable<IEdmOperation> FindBoundOperations(this IEdmModel model, IEdmType bindingType)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmType>(bindingType, "bindingType");
			return model.FindAcrossModels(bindingType, ExtensionMethods.findBoundOperations, ExtensionMethods.mergeFunctions);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0001D7EC File Offset: 0x0001B9EC
		public static IEnumerable<IEdmOperation> FindBoundOperations(this IEdmModel model, string qualifiedName, IEdmType bindingType)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			EdmUtil.CheckArgumentNull<IEdmType>(bindingType, "bindingType");
			IEnumerable<IEdmOperation> enumerable = model.FindDeclaredBoundOperations(qualifiedName, bindingType);
			foreach (IEdmModel edmModel in model.ReferencedModels)
			{
				IEnumerable<IEdmOperation> enumerable2 = edmModel.FindDeclaredBoundOperations(qualifiedName, bindingType);
				if (enumerable2 != null)
				{
					enumerable = ((enumerable == null) ? enumerable2 : ExtensionMethods.mergeFunctions.Invoke(enumerable, enumerable2));
				}
			}
			return enumerable;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0001D880 File Offset: 0x0001BA80
		public static IEdmValueTerm FindValueTerm(this IEdmModel model, string qualifiedName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			return model.FindAcrossModels(qualifiedName, ExtensionMethods.findValueTerm, new Func<IEdmValueTerm, IEdmValueTerm, IEdmValueTerm>(RegistrationHelper.CreateAmbiguousValueTermBinding));
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0001D8B2 File Offset: 0x0001BAB2
		public static IEnumerable<IEdmOperation> FindOperations(this IEdmModel model, string qualifiedName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			return model.FindAcrossModels(qualifiedName, ExtensionMethods.findOperations, ExtensionMethods.mergeFunctions);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0001D8E0 File Offset: 0x0001BAE0
		public static bool ExistsContainer(this IEdmModel model, string containerName)
		{
			if (model.EntityContainer == null)
			{
				return false;
			}
			string text = (model.EntityContainer.Namespace ?? string.Empty) + "." + (containerName ?? string.Empty);
			return string.Equals(model.EntityContainer.FullName(), text, 4) || string.Equals(model.EntityContainer.FullName(), containerName, 4);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0001D94B File Offset: 0x0001BB4B
		public static IEdmEntityContainer FindEntityContainer(this IEdmModel model, string qualifiedName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			return model.FindAcrossModels(qualifiedName, ExtensionMethods.findEntityContainer, new Func<IEdmEntityContainer, IEdmEntityContainer, IEdmEntityContainer>(RegistrationHelper.CreateAmbiguousEntityContainerBinding));
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0001D980 File Offset: 0x0001BB80
		public static IEnumerable<IEdmVocabularyAnnotation> FindVocabularyAnnotationsIncludingInheritedAnnotations(this IEdmModel model, IEdmVocabularyAnnotatable element)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			IEnumerable<IEdmVocabularyAnnotation> enumerable = model.FindDeclaredVocabularyAnnotations(element);
			IEdmStructuredType edmStructuredType = element as IEdmStructuredType;
			if (edmStructuredType != null)
			{
				for (edmStructuredType = edmStructuredType.BaseType; edmStructuredType != null; edmStructuredType = edmStructuredType.BaseType)
				{
					IEdmVocabularyAnnotatable edmVocabularyAnnotatable = edmStructuredType as IEdmVocabularyAnnotatable;
					if (edmVocabularyAnnotatable != null)
					{
						enumerable = Enumerable.Concat<IEdmVocabularyAnnotation>(enumerable, model.FindDeclaredVocabularyAnnotations(edmVocabularyAnnotatable));
					}
				}
			}
			return enumerable;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0001D9E4 File Offset: 0x0001BBE4
		public static IEnumerable<IEdmVocabularyAnnotation> FindVocabularyAnnotations(this IEdmModel model, IEdmVocabularyAnnotatable element)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			IEnumerable<IEdmVocabularyAnnotation> enumerable = model.FindVocabularyAnnotationsIncludingInheritedAnnotations(element);
			foreach (IEdmModel edmModel in model.ReferencedModels)
			{
				enumerable = Enumerable.Concat<IEdmVocabularyAnnotation>(enumerable, edmModel.FindVocabularyAnnotationsIncludingInheritedAnnotations(element));
			}
			return enumerable;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0001DA5C File Offset: 0x0001BC5C
		public static IEnumerable<T> FindVocabularyAnnotations<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmTerm term) where T : IEdmVocabularyAnnotation
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			return model.FindVocabularyAnnotations(element, term, null);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0001DA8C File Offset: 0x0001BC8C
		public static IEnumerable<T> FindVocabularyAnnotations<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmTerm term, string qualifier) where T : IEdmVocabularyAnnotation
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			List<T> list = null;
			foreach (T t in Enumerable.OfType<T>(model.FindVocabularyAnnotations(element)))
			{
				if (t.Term == term && (qualifier == null || qualifier == t.Qualifier))
				{
					if (list == null)
					{
						list = new List<T>();
					}
					list.Add(t);
				}
			}
			return list ?? Enumerable.Empty<T>();
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0001DB40 File Offset: 0x0001BD40
		public static IEnumerable<T> FindVocabularyAnnotations<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName) where T : IEdmVocabularyAnnotation
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			return model.FindVocabularyAnnotations(element, termName, null);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0001DE0C File Offset: 0x0001C00C
		public static IEnumerable<T> FindVocabularyAnnotations<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, string qualifier) where T : IEdmVocabularyAnnotation
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			string namespaceName;
			string name;
			if (EdmUtil.TryGetNamespaceNameFromQualifiedName(termName, out namespaceName, out name))
			{
				foreach (T annotation in Enumerable.OfType<T>(model.FindVocabularyAnnotations(element)))
				{
					T t = annotation;
					IEdmTerm annotationTerm = t.Term;
					if (annotationTerm.Namespace == namespaceName && annotationTerm.Name == name)
					{
						if (qualifier != null)
						{
							T t2 = annotation;
							if (!(qualifier == t2.Qualifier))
							{
								continue;
							}
						}
						yield return annotation;
					}
				}
			}
			yield break;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0001DE40 File Offset: 0x0001C040
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmStructuredValue context, string termName, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), termName, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0001DEA4 File Offset: 0x0001C0A4
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmStructuredValue context, string termName, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), termName, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0001DF08 File Offset: 0x0001C108
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmStructuredValue context, IEdmValueTerm term, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), term, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0001DF6C File Offset: 0x0001C16C
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmStructuredValue context, IEdmValueTerm term, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), term, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0001DFD0 File Offset: 0x0001C1D0
		public static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, string termName, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), termName, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0001E034 File Offset: 0x0001C234
		public static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, string termName, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), termName, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0001E098 File Offset: 0x0001C298
		public static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmValueTerm term, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), term, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0001E0FC File Offset: 0x0001C2FC
		public static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmValueTerm term, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), term, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0001E160 File Offset: 0x0001C360
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "evaluator");
			return model.GetTermValue(element, termName, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0001E1B4 File Offset: 0x0001C3B4
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "evaluator");
			return model.GetTermValue(element, termName, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0001E208 File Offset: 0x0001C408
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmValueTerm term, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "evaluator");
			return model.GetTermValue(element, term, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0001E25C File Offset: 0x0001C45C
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmValueTerm term, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "evaluator");
			return model.GetTermValue(element, term, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0001E2B0 File Offset: 0x0001C4B0
		public static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(element, termName, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0001E304 File Offset: 0x0001C504
		public static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(element, termName, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0001E358 File Offset: 0x0001C558
		public static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmValueTerm term, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(element, term, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0001E3AC File Offset: 0x0001C5AC
		public static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmValueTerm term, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(element, term, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0001E400 File Offset: 0x0001C600
		public static object GetAnnotationValue(this IEdmModel model, IEdmElement element, string namespaceName, string localName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return model.DirectValueAnnotationsManager.GetAnnotationValue(element, namespaceName, localName);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0001E428 File Offset: 0x0001C628
		public static T GetAnnotationValue<T>(this IEdmModel model, IEdmElement element, string namespaceName, string localName) where T : class
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return ExtensionMethods.AnnotationValue<T>(model.GetAnnotationValue(element, namespaceName, localName));
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0001E450 File Offset: 0x0001C650
		public static T GetAnnotationValue<T>(this IEdmModel model, IEdmElement element) where T : class
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return model.GetAnnotationValue(element, "http://schemas.microsoft.com/ado/2011/04/edm/internal", ExtensionMethods.TypeName<T>.LocalName);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0001E47B File Offset: 0x0001C67B
		public static void SetAnnotationValue(this IEdmModel model, IEdmElement element, string namespaceName, string localName, object value)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			model.DirectValueAnnotationsManager.SetAnnotationValue(element, namespaceName, localName, value);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0001E4A8 File Offset: 0x0001C6A8
		public static string GetDescriptionAnnotation(this IEdmModel model, IEdmVocabularyAnnotatable target)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(target, "target");
			IEdmValueAnnotation edmValueAnnotation = Enumerable.FirstOrDefault<IEdmValueAnnotation>(model.FindVocabularyAnnotations(target, CoreVocabularyModel.DescriptionTerm));
			if (edmValueAnnotation != null)
			{
				IEdmStringConstantExpression edmStringConstantExpression = edmValueAnnotation.Value as IEdmStringConstantExpression;
				if (edmStringConstantExpression != null)
				{
					return edmStringConstantExpression.Value;
				}
			}
			return null;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0001E4FC File Offset: 0x0001C6FC
		public static string GetLongDescriptionAnnotation(this IEdmModel model, IEdmVocabularyAnnotatable target)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(target, "target");
			IEdmValueAnnotation edmValueAnnotation = Enumerable.FirstOrDefault<IEdmValueAnnotation>(model.FindVocabularyAnnotations(target, CoreVocabularyModel.LongDescriptionTerm));
			if (edmValueAnnotation != null)
			{
				IEdmStringConstantExpression edmStringConstantExpression = edmValueAnnotation.Value as IEdmStringConstantExpression;
				if (edmStringConstantExpression != null)
				{
					return edmStringConstantExpression.Value;
				}
			}
			return null;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0001E550 File Offset: 0x0001C750
		public static IEnumerable<IEdmSchemaElement> SchemaElementsAcrossModels(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			IEnumerable<IEdmSchemaElement> enumerable = new IEdmSchemaElement[0];
			foreach (IEdmModel edmModel in model.ReferencedModels)
			{
				enumerable = Enumerable.Concat<IEdmSchemaElement>(enumerable, edmModel.SchemaElements);
			}
			enumerable = Enumerable.Concat<IEdmSchemaElement>(enumerable, model.SchemaElements);
			return enumerable;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0001E5C4 File Offset: 0x0001C7C4
		public static IEnumerable<IEdmStructuredType> FindAllDerivedTypes(this IEdmModel model, IEdmStructuredType baseType)
		{
			List<IEdmStructuredType> list = new List<IEdmStructuredType>();
			if (baseType is IEdmSchemaElement)
			{
				model.DerivedFrom(baseType, new HashSetInternal<IEdmStructuredType>(), list);
			}
			return list;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0001E5ED File Offset: 0x0001C7ED
		public static void SetAnnotationValue<T>(this IEdmModel model, IEdmElement element, T value) where T : class
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			model.SetAnnotationValue(element, "http://schemas.microsoft.com/ado/2011/04/edm/internal", ExtensionMethods.TypeName<T>.LocalName, value);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0001E61E File Offset: 0x0001C81E
		public static object[] GetAnnotationValues(this IEdmModel model, IEnumerable<IEdmDirectValueAnnotationBinding> annotations)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmDirectValueAnnotationBinding>>(annotations, "annotations");
			return model.DirectValueAnnotationsManager.GetAnnotationValues(annotations);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0001E644 File Offset: 0x0001C844
		public static void SetAnnotationValues(this IEdmModel model, IEnumerable<IEdmDirectValueAnnotationBinding> annotations)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmDirectValueAnnotationBinding>>(annotations, "annotations");
			model.DirectValueAnnotationsManager.SetAnnotationValues(annotations);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0001E66A File Offset: 0x0001C86A
		public static IEnumerable<IEdmDirectValueAnnotation> DirectValueAnnotations(this IEdmModel model, IEdmElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return model.DirectValueAnnotationsManager.GetDirectValueAnnotations(element);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0001E690 File Offset: 0x0001C890
		public static bool TryFindContainerQualifiedEntitySet(this IEdmModel model, string containerQualifiedEntitySetName, out IEdmEntitySet entitySet)
		{
			entitySet = null;
			string text = null;
			string text2 = null;
			if (containerQualifiedEntitySetName != null && containerQualifiedEntitySetName.IndexOf(".", 4) > -1 && EdmUtil.TryParseContainerQualifiedElementName(containerQualifiedEntitySetName, out text, out text2) && model.ExistsContainer(text))
			{
				IEdmEntityContainer entityContainer = model.EntityContainer;
				if (entityContainer != null)
				{
					entitySet = entityContainer.FindEntitySetExtended(text2);
				}
			}
			return entitySet != null;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0001E6E8 File Offset: 0x0001C8E8
		public static bool TryFindContainerQualifiedSingleton(this IEdmModel model, string containerQualifiedSingletonName, out IEdmSingleton singleton)
		{
			singleton = null;
			string text = null;
			string text2 = null;
			if (containerQualifiedSingletonName != null && containerQualifiedSingletonName.IndexOf(".", 4) > -1 && EdmUtil.TryParseContainerQualifiedElementName(containerQualifiedSingletonName, out text, out text2) && model.ExistsContainer(text))
			{
				singleton = model.EntityContainer.FindSingletonExtended(text2);
				if (singleton != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0001E738 File Offset: 0x0001C938
		public static bool TryFindContainerQualifiedOperationImports(this IEdmModel model, string containerQualifiedOperationImportName, out IEnumerable<IEdmOperationImport> operationImports)
		{
			operationImports = null;
			string text = null;
			string text2 = null;
			if (containerQualifiedOperationImportName.IndexOf(".", 4) > -1 && EdmUtil.TryParseContainerQualifiedElementName(containerQualifiedOperationImportName, out text, out text2) && model.ExistsContainer(text))
			{
				operationImports = model.EntityContainer.FindOperationImportsExtended(text2);
				if (operationImports != null && Enumerable.Count<IEdmOperationImport>(operationImports) > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0001E790 File Offset: 0x0001C990
		public static IEdmEntitySet FindDeclaredEntitySet(this IEdmModel model, string qualifiedName)
		{
			IEdmEntitySet edmEntitySet = null;
			if (!model.TryFindContainerQualifiedEntitySet(qualifiedName, out edmEntitySet))
			{
				IEdmEntityContainer entityContainer = model.EntityContainer;
				if (entityContainer != null)
				{
					return entityContainer.FindEntitySetExtended(qualifiedName);
				}
			}
			return edmEntitySet;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0001E7C0 File Offset: 0x0001C9C0
		public static IEdmSingleton FindDeclaredSingleton(this IEdmModel model, string qualifiedName)
		{
			IEdmSingleton edmSingleton = null;
			if (!model.TryFindContainerQualifiedSingleton(qualifiedName, out edmSingleton))
			{
				IEdmEntityContainer entityContainer = model.EntityContainer;
				if (entityContainer != null)
				{
					return entityContainer.FindSingletonExtended(qualifiedName);
				}
			}
			return edmSingleton;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0001E7F0 File Offset: 0x0001C9F0
		public static IEdmNavigationSource FindDeclaredNavigationSource(this IEdmModel model, string qualifiedName)
		{
			IEdmEntitySet edmEntitySet = model.FindDeclaredEntitySet(qualifiedName);
			if (edmEntitySet != null)
			{
				return edmEntitySet;
			}
			return model.FindDeclaredSingleton(qualifiedName);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0001E814 File Offset: 0x0001CA14
		public static IEnumerable<IEdmOperationImport> FindDeclaredOperationImports(this IEdmModel model, string qualifiedName)
		{
			IEnumerable<IEdmOperationImport> enumerable = null;
			if (!model.TryFindContainerQualifiedOperationImports(qualifiedName, out enumerable))
			{
				IEdmEntityContainer entityContainer = model.EntityContainer;
				if (entityContainer != null)
				{
					return entityContainer.FindOperationImportsExtended(qualifiedName);
				}
			}
			return enumerable;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0001E841 File Offset: 0x0001CA41
		public static IPrimitiveValueConverter GetPrimitiveValueConverter(this IEdmModel model, IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "mode");
			if (type == null || !type.IsTypeDefinition())
			{
				return PassThroughPrimitiveValueConverter.Instance;
			}
			return model.GetPrimitiveValueConverter(type.FullName());
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0001E86C File Offset: 0x0001CA6C
		public static void SetPrimitiveValueConverter(this IEdmModel model, IEdmTypeDefinitionReference typeDefinition, IPrimitiveValueConverter converter)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmTypeDefinitionReference>(typeDefinition, "typeDefinition");
			EdmUtil.CheckArgumentNull<IPrimitiveValueConverter>(converter, "converter");
			model.SetPrimitiveValueConverter(typeDefinition.FullName(), converter);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0001E8AC File Offset: 0x0001CAAC
		public static void SetOptimisticConcurrencyAnnotation(this EdmModel model, IEdmEntitySet target, IEnumerable<IEdmStructuralProperty> properties)
		{
			EdmUtil.CheckArgumentNull<EdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmEntitySet>(target, "target");
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmStructuralProperty>>(properties, "properties");
			IEdmCollectionExpression edmCollectionExpression = new EdmCollectionExpression(Enumerable.ToArray<EdmPropertyPathExpression>(Enumerable.Select<IEdmStructuralProperty, EdmPropertyPathExpression>(properties, (IEdmStructuralProperty p) => new EdmPropertyPathExpression(p.Name))));
			IEdmValueTerm concurrencyTerm = CoreVocabularyModel.ConcurrencyTerm;
			EdmAnnotation edmAnnotation = new EdmAnnotation(target, concurrencyTerm, edmCollectionExpression);
			edmAnnotation.SetSerializationLocation(model, new EdmVocabularyAnnotationSerializationLocation?(EdmVocabularyAnnotationSerializationLocation.Inline));
			model.SetVocabularyAnnotation(edmAnnotation);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0001E93C File Offset: 0x0001CB3C
		[Obsolete("Org.OData.Core.V1.OptimisticConcurrencyControl is obsolete; use SetOptimisticConcurrencyAnnotation to set Org.OData.Core.V1.OptimisticConcurrency instead")]
		public static void SetOptimisticConcurrencyControlAnnotation(this EdmModel model, IEdmEntitySet target, IEnumerable<IEdmStructuralProperty> properties)
		{
			EdmUtil.CheckArgumentNull<EdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmEntitySet>(target, "target");
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmStructuralProperty>>(properties, "properties");
			IEdmCollectionExpression edmCollectionExpression = new EdmCollectionExpression(Enumerable.ToArray<EdmPropertyPathExpression>(Enumerable.Select<IEdmStructuralProperty, EdmPropertyPathExpression>(properties, (IEdmStructuralProperty p) => new EdmPropertyPathExpression(p.Name))));
			IEdmValueTerm concurrencyControlTerm = CoreVocabularyModel.ConcurrencyControlTerm;
			EdmAnnotation edmAnnotation = new EdmAnnotation(target, concurrencyControlTerm, edmCollectionExpression);
			edmAnnotation.SetSerializationLocation(model, new EdmVocabularyAnnotationSerializationLocation?(EdmVocabularyAnnotationSerializationLocation.Inline));
			model.SetVocabularyAnnotation(edmAnnotation);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0001E9C0 File Offset: 0x0001CBC0
		public static void SetDescriptionAnnotation(this EdmModel model, IEdmVocabularyAnnotatable target, string description)
		{
			EdmUtil.CheckArgumentNull<EdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(target, "target");
			EdmUtil.CheckArgumentNull<string>(description, "description");
			EdmAnnotation edmAnnotation = new EdmAnnotation(target, CoreVocabularyModel.DescriptionTerm, new EdmStringConstant(description));
			edmAnnotation.SetSerializationLocation(model, new EdmVocabularyAnnotationSerializationLocation?(EdmVocabularyAnnotationSerializationLocation.Inline));
			model.SetVocabularyAnnotation(edmAnnotation);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0001EA18 File Offset: 0x0001CC18
		public static void SetLongDescriptionAnnotation(this EdmModel model, IEdmVocabularyAnnotatable target, string description)
		{
			EdmUtil.CheckArgumentNull<EdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(target, "target");
			EdmUtil.CheckArgumentNull<string>(description, "description");
			EdmAnnotation edmAnnotation = new EdmAnnotation(target, CoreVocabularyModel.LongDescriptionTerm, new EdmStringConstant(description));
			edmAnnotation.SetSerializationLocation(model, new EdmVocabularyAnnotationSerializationLocation?(EdmVocabularyAnnotationSerializationLocation.Inline));
			model.SetVocabularyAnnotation(edmAnnotation);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0001EA6F File Offset: 0x0001CC6F
		public static void SetChangeTrackingAnnotation(this EdmModel model, IEdmEntityContainer target, bool isSupported)
		{
			model.SetChangeTrackingAnnotationImplementation(target, isSupported, null, null);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0001EA7B File Offset: 0x0001CC7B
		public static void SetChangeTrackingAnnotation(this EdmModel model, IEdmEntitySet target, bool isSupported, IEnumerable<IEdmStructuralProperty> filterableProperties, IEnumerable<IEdmNavigationProperty> expandableProperties)
		{
			model.SetChangeTrackingAnnotationImplementation(target, isSupported, filterableProperties, expandableProperties);
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0001EA88 File Offset: 0x0001CC88
		public static IEdmTypeDefinitionReference GetUInt16(this EdmModel model, string namespaceName, bool isNullable)
		{
			return model.GetUIntImplementation(namespaceName, "UInt16", "Edm.Int32", isNullable);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0001EA9C File Offset: 0x0001CC9C
		public static IEdmTypeDefinitionReference GetUInt32(this EdmModel model, string namespaceName, bool isNullable)
		{
			return model.GetUIntImplementation(namespaceName, "UInt32", "Edm.Int64", isNullable);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0001EAB0 File Offset: 0x0001CCB0
		public static IEdmTypeDefinitionReference GetUInt64(this EdmModel model, string namespaceName, bool isNullable)
		{
			return model.GetUIntImplementation(namespaceName, "UInt64", "Edm.Decimal", isNullable);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0001EAC4 File Offset: 0x0001CCC4
		public static EdmLocation Location(this IEdmElement item)
		{
			EdmUtil.CheckArgumentNull<IEdmElement>(item, "item");
			IEdmLocatable edmLocatable = item as IEdmLocatable;
			if (edmLocatable == null || edmLocatable.Location == null)
			{
				return new ObjectLocation(item);
			}
			return edmLocatable.Location;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0001EAFC File Offset: 0x0001CCFC
		public static IEnumerable<IEdmVocabularyAnnotation> VocabularyAnnotations(this IEdmVocabularyAnnotatable element, IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.FindVocabularyAnnotations(element);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0001EB1D File Offset: 0x0001CD1D
		public static string FullName(this IEdmSchemaElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmSchemaElement>(element, "element");
			return (element.Namespace ?? string.Empty) + "." + (element.Name ?? string.Empty);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0001EB53 File Offset: 0x0001CD53
		public static string ShortQualifiedName(this IEdmSchemaElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmSchemaElement>(element, "element");
			if (element.Namespace != null && element.Namespace.Equals("Edm"))
			{
				return element.Name ?? string.Empty;
			}
			return element.FullName();
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0001EB91 File Offset: 0x0001CD91
		public static IEnumerable<IEdmEntitySet> EntitySets(this IEdmEntityContainer container)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			return Enumerable.OfType<IEdmEntitySet>(container.AllElements(100));
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0001EBAC File Offset: 0x0001CDAC
		public static IEnumerable<IEdmSingleton> Singletons(this IEdmEntityContainer container)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			return Enumerable.OfType<IEdmSingleton>(container.AllElements(100));
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0001EBC7 File Offset: 0x0001CDC7
		public static IEnumerable<IEdmOperationImport> OperationImports(this IEdmEntityContainer container)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			return Enumerable.OfType<IEdmOperationImport>(container.AllElements(100));
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0001EBE4 File Offset: 0x0001CDE4
		public static EdmTypeKind TypeKind(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmType definition = type.Definition;
			if (definition == null)
			{
				return EdmTypeKind.None;
			}
			return definition.TypeKind;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0001EC0F File Offset: 0x0001CE0F
		public static string FullName(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.Definition.FullTypeName();
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0001EC28 File Offset: 0x0001CE28
		public static string ShortQualifiedName(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmSchemaElement edmSchemaElement = type.Definition as IEdmSchemaElement;
			if (edmSchemaElement == null)
			{
				return null;
			}
			return edmSchemaElement.ShortQualifiedName();
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0001EC58 File Offset: 0x0001CE58
		public static string FullTypeName(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			IEdmSchemaElement edmSchemaElement = type as IEdmSchemaElement;
			IEdmCollectionType edmCollectionType = type as IEdmCollectionType;
			if (edmCollectionType == null)
			{
				if (edmSchemaElement == null)
				{
					return null;
				}
				return edmSchemaElement.FullName();
			}
			else
			{
				edmSchemaElement = edmCollectionType.ElementType.Definition as IEdmSchemaElement;
				if (edmSchemaElement == null)
				{
					return null;
				}
				return string.Format(CultureInfo.InvariantCulture, "Collection({0})", new object[] { edmSchemaElement.FullName() });
			}
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0001ECC4 File Offset: 0x0001CEC4
		public static IEdmPrimitiveType PrimitiveDefinition(this IEdmPrimitiveTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmPrimitiveTypeReference>(type, "type");
			return (IEdmPrimitiveType)type.Definition;
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0001ECE0 File Offset: 0x0001CEE0
		public static EdmPrimitiveTypeKind PrimitiveKind(this IEdmPrimitiveTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmPrimitiveTypeReference>(type, "type");
			IEdmPrimitiveType edmPrimitiveType = type.PrimitiveDefinition();
			if (edmPrimitiveType == null)
			{
				return EdmPrimitiveTypeKind.None;
			}
			return edmPrimitiveType.PrimitiveKind;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0001EF90 File Offset: 0x0001D190
		public static IEnumerable<IEdmProperty> Properties(this IEdmStructuredType type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(type, "type");
			if (type.BaseType != null)
			{
				foreach (IEdmProperty baseProperty in type.BaseType.Properties())
				{
					yield return baseProperty;
				}
			}
			if (type.DeclaredProperties != null)
			{
				foreach (IEdmProperty declaredProperty in type.DeclaredProperties)
				{
					yield return declaredProperty;
				}
			}
			yield break;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0001EFAD File Offset: 0x0001D1AD
		public static IEnumerable<IEdmStructuralProperty> DeclaredStructuralProperties(this IEdmStructuredType type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(type, "type");
			return Enumerable.OfType<IEdmStructuralProperty>(type.DeclaredProperties);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0001EFC6 File Offset: 0x0001D1C6
		public static IEnumerable<IEdmStructuralProperty> StructuralProperties(this IEdmStructuredType type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(type, "type");
			return Enumerable.OfType<IEdmStructuralProperty>(type.Properties());
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0001EFDF File Offset: 0x0001D1DF
		public static IEdmStructuredType StructuredDefinition(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return (IEdmStructuredType)type.Definition;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0001EFF8 File Offset: 0x0001D1F8
		public static bool IsAbstract(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().IsAbstract;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0001F011 File Offset: 0x0001D211
		public static bool IsOpen(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().IsOpen;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0001F02A File Offset: 0x0001D22A
		public static IEdmStructuredType BaseType(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().BaseType;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0001F043 File Offset: 0x0001D243
		public static IEnumerable<IEdmStructuralProperty> DeclaredStructuralProperties(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().DeclaredStructuralProperties();
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0001F05C File Offset: 0x0001D25C
		public static IEnumerable<IEdmStructuralProperty> StructuralProperties(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().StructuralProperties();
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0001F075 File Offset: 0x0001D275
		public static IEdmProperty FindProperty(this IEdmStructuredTypeReference type, string name)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			return type.StructuredDefinition().FindProperty(name);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0001F09B File Offset: 0x0001D29B
		public static IEdmEntityType BaseEntityType(this IEdmEntityType type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(type, "type");
			return type.BaseType as IEdmEntityType;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0001F0B4 File Offset: 0x0001D2B4
		public static IEnumerable<IEdmNavigationProperty> DeclaredNavigationProperties(this IEdmEntityType type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(type, "type");
			return Enumerable.OfType<IEdmNavigationProperty>(type.DeclaredProperties);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0001F0CD File Offset: 0x0001D2CD
		public static IEnumerable<IEdmNavigationProperty> NavigationProperties(this IEdmEntityType type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(type, "type");
			return Enumerable.OfType<IEdmNavigationProperty>(type.Properties());
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0001F0E8 File Offset: 0x0001D2E8
		public static IEnumerable<IEdmStructuralProperty> Key(this IEdmEntityType type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(type, "type");
			for (IEdmEntityType edmEntityType = type; edmEntityType != null; edmEntityType = edmEntityType.BaseEntityType())
			{
				if (edmEntityType.DeclaredKey != null)
				{
					return edmEntityType.DeclaredKey;
				}
			}
			return Enumerable.Empty<IEdmStructuralProperty>();
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0001F124 File Offset: 0x0001D324
		public static IEnumerable<IDictionary<string, IEdmProperty>> GetAlternateKeysAnnotation(this IEdmModel model, IEdmEntityType type)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmEntityType>(type, "type");
			for (IEdmEntityType edmEntityType = type; edmEntityType != null; edmEntityType = edmEntityType.BaseEntityType())
			{
				IEnumerable<IDictionary<string, IEdmProperty>> declaredAlternateKeysForType = ExtensionMethods.GetDeclaredAlternateKeysForType(edmEntityType, model);
				if (declaredAlternateKeysForType != null)
				{
					return declaredAlternateKeysForType;
				}
			}
			return Enumerable.Empty<IDictionary<string, IEdmProperty>>();
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0001F16C File Offset: 0x0001D36C
		public static void AddAlternateKeyAnnotation(this EdmModel model, IEdmEntityType type, IDictionary<string, IEdmProperty> alternateKey)
		{
			EdmUtil.CheckArgumentNull<EdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmEntityType>(type, "type");
			EdmUtil.CheckArgumentNull<IDictionary<string, IEdmProperty>>(alternateKey, "alternateKey");
			EdmCollectionExpression edmCollectionExpression = null;
			IEdmValueAnnotation edmValueAnnotation = Enumerable.FirstOrDefault<IEdmValueAnnotation>(model.FindVocabularyAnnotations(type, AlternateKeysVocabularyModel.AlternateKeysTerm));
			if (edmValueAnnotation != null)
			{
				edmCollectionExpression = edmValueAnnotation.Value as EdmCollectionExpression;
			}
			List<IEdmExpression> list = ((edmCollectionExpression != null) ? new List<IEdmExpression>(edmCollectionExpression.Elements) : new List<IEdmExpression>());
			List<IEdmExpression> list2 = new List<IEdmExpression>();
			foreach (KeyValuePair<string, IEdmProperty> keyValuePair in alternateKey)
			{
				IEdmRecordExpression edmRecordExpression = new EdmRecordExpression(new EdmComplexTypeReference(AlternateKeysVocabularyModel.PropertyRefType, false), new IEdmPropertyConstructor[]
				{
					new EdmPropertyConstructor("Alias", new EdmStringConstant(keyValuePair.Key)),
					new EdmPropertyConstructor("Name", new EdmPropertyPathExpression(keyValuePair.Value.Name))
				});
				list2.Add(edmRecordExpression);
			}
			EdmRecordExpression edmRecordExpression2 = new EdmRecordExpression(new EdmComplexTypeReference(AlternateKeysVocabularyModel.AlternateKeyType, false), new IEdmPropertyConstructor[]
			{
				new EdmPropertyConstructor("Key", new EdmCollectionExpression(list2))
			});
			list.Add(edmRecordExpression2);
			EdmAnnotation edmAnnotation = new EdmAnnotation(type, AlternateKeysVocabularyModel.AlternateKeysTerm, new EdmCollectionExpression(list));
			edmAnnotation.SetSerializationLocation(model, new EdmVocabularyAnnotationSerializationLocation?(EdmVocabularyAnnotationSerializationLocation.Inline));
			model.SetVocabularyAnnotation(edmAnnotation);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0001F2E8 File Offset: 0x0001D4E8
		public static bool HasDeclaredKeyProperty(this IEdmEntityType entityType, IEdmProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(entityType, "entityType");
			EdmUtil.CheckArgumentNull<IEdmProperty>(property, "property");
			while (entityType != null)
			{
				if (entityType.DeclaredKey != null)
				{
					if (Enumerable.Any<IEdmStructuralProperty>(entityType.DeclaredKey, (IEdmStructuralProperty k) => k == property))
					{
						return true;
					}
				}
				entityType = entityType.BaseEntityType();
			}
			return false;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0001F357 File Offset: 0x0001D557
		public static IEdmEntityType EntityDefinition(this IEdmEntityTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			return (IEdmEntityType)type.Definition;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0001F370 File Offset: 0x0001D570
		public static IEdmEntityType BaseEntityType(this IEdmEntityTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			return type.EntityDefinition().BaseEntityType();
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0001F389 File Offset: 0x0001D589
		public static IEnumerable<IEdmStructuralProperty> Key(this IEdmEntityTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			return type.EntityDefinition().Key();
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0001F3A2 File Offset: 0x0001D5A2
		public static IEnumerable<IEdmNavigationProperty> NavigationProperties(this IEdmEntityTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			return type.EntityDefinition().NavigationProperties();
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0001F3BB File Offset: 0x0001D5BB
		public static IEnumerable<IEdmNavigationProperty> DeclaredNavigationProperties(this IEdmEntityTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			return type.EntityDefinition().DeclaredNavigationProperties();
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0001F3D4 File Offset: 0x0001D5D4
		public static IEdmNavigationProperty FindNavigationProperty(this IEdmEntityTypeReference type, string name)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			return type.EntityDefinition().FindProperty(name) as IEdmNavigationProperty;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0001F3FF File Offset: 0x0001D5FF
		public static IEdmComplexType BaseComplexType(this IEdmComplexType type)
		{
			EdmUtil.CheckArgumentNull<IEdmComplexType>(type, "type");
			return type.BaseType as IEdmComplexType;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0001F418 File Offset: 0x0001D618
		public static IEdmComplexType ComplexDefinition(this IEdmComplexTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmComplexTypeReference>(type, "type");
			return (IEdmComplexType)type.Definition;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0001F431 File Offset: 0x0001D631
		public static IEdmComplexType BaseComplexType(this IEdmComplexTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmComplexTypeReference>(type, "type");
			return type.ComplexDefinition().BaseComplexType();
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0001F44A File Offset: 0x0001D64A
		public static IEdmEntityReferenceType EntityReferenceDefinition(this IEdmEntityReferenceTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityReferenceTypeReference>(type, "type");
			return (IEdmEntityReferenceType)type.Definition;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0001F463 File Offset: 0x0001D663
		public static IEdmEntityType EntityType(this IEdmEntityReferenceTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityReferenceTypeReference>(type, "type");
			return type.EntityReferenceDefinition().EntityType;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0001F47C File Offset: 0x0001D67C
		public static IEdmCollectionType CollectionDefinition(this IEdmCollectionTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmCollectionTypeReference>(type, "type");
			return (IEdmCollectionType)type.Definition;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0001F495 File Offset: 0x0001D695
		public static IEdmTypeReference ElementType(this IEdmCollectionTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmCollectionTypeReference>(type, "type");
			return type.CollectionDefinition().ElementType;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0001F4AE File Offset: 0x0001D6AE
		public static IEdmEnumType EnumDefinition(this IEdmEnumTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEnumTypeReference>(type, "type");
			return (IEdmEnumType)type.Definition;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0001F4C7 File Offset: 0x0001D6C7
		public static IEdmTypeDefinition TypeDefinition(this IEdmTypeDefinitionReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeDefinitionReference>(type, "type");
			return (IEdmTypeDefinition)type.Definition;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0001F4E0 File Offset: 0x0001D6E0
		public static EdmMultiplicity TargetMultiplicity(this IEdmNavigationProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(property, "property");
			IEdmTypeReference type = property.Type;
			if (type.IsCollection())
			{
				return EdmMultiplicity.Many;
			}
			if (!type.IsNullable)
			{
				return EdmMultiplicity.One;
			}
			return EdmMultiplicity.ZeroOrOne;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0001F518 File Offset: 0x0001D718
		public static IEdmEntityType ToEntityType(this IEdmNavigationProperty property)
		{
			IEdmType edmType = property.Type.Definition;
			if (edmType.TypeKind == EdmTypeKind.Collection)
			{
				edmType = ((IEdmCollectionType)edmType).ElementType.Definition;
			}
			if (edmType.TypeKind == EdmTypeKind.EntityReference)
			{
				edmType = ((IEdmEntityReferenceType)edmType).EntityType;
			}
			return edmType as IEdmEntityType;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0001F566 File Offset: 0x0001D766
		public static IEdmEntityType DeclaringEntityType(this IEdmNavigationProperty property)
		{
			return (IEdmEntityType)property.DeclaringType;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0001F573 File Offset: 0x0001D773
		public static bool IsPrincipal(this IEdmNavigationProperty navigationProperty)
		{
			return navigationProperty.ReferentialConstraint == null && navigationProperty.Partner != null && navigationProperty.Partner.ReferentialConstraint != null;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0001F5A0 File Offset: 0x0001D7A0
		public static IEnumerable<IEdmStructuralProperty> DependentProperties(this IEdmNavigationProperty navigationProperty)
		{
			if (navigationProperty.ReferentialConstraint != null)
			{
				return Enumerable.Select<EdmReferentialConstraintPropertyPair, IEdmStructuralProperty>(navigationProperty.ReferentialConstraint.PropertyPairs, (EdmReferentialConstraintPropertyPair p) => p.DependentProperty);
			}
			return null;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
		public static IEnumerable<IEdmStructuralProperty> PrincipalProperties(this IEdmNavigationProperty navigationProperty)
		{
			if (navigationProperty.ReferentialConstraint != null)
			{
				return Enumerable.Select<EdmReferentialConstraintPropertyPair, IEdmStructuralProperty>(navigationProperty.ReferentialConstraint.PropertyPairs, (EdmReferentialConstraintPropertyPair p) => p.PrincipalProperty);
			}
			return null;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0001F61A File Offset: 0x0001D81A
		public static IEdmValueTerm ValueTerm(this IEdmValueAnnotation annotation)
		{
			EdmUtil.CheckArgumentNull<IEdmValueAnnotation>(annotation, "annotation");
			return (IEdmValueTerm)annotation.Term;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0001F634 File Offset: 0x0001D834
		public static bool TryGetRelativeEntitySetPath(this IEdmOperation operation, IEdmModel model, out IEdmOperationParameter parameter, out IEnumerable<IEdmNavigationProperty> relativePath, out IEdmEntityType lastEntityType, out IEnumerable<EdmError> errors)
		{
			errors = Enumerable.Empty<EdmError>();
			parameter = null;
			relativePath = null;
			lastEntityType = null;
			if (operation.EntitySetPath == null)
			{
				return false;
			}
			Collection<EdmError> collection = new Collection<EdmError>();
			errors = collection;
			if (!operation.IsBound)
			{
				collection.Add(new EdmError(operation.Location(), EdmErrorCode.OperationCannotHaveEntitySetPathWithUnBoundOperation, Strings.EdmModel_Validator_Semantic_OperationCannotHaveEntitySetPathWithUnBoundOperation(operation.Name)));
			}
			return ExtensionMethods.TryGetRelativeEntitySetPath(operation, collection, operation.EntitySetPath, model, operation.Parameters, out parameter, out relativePath, out lastEntityType);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0001F6A8 File Offset: 0x0001D8A8
		public static bool IsActionImport(this IEdmOperationImport operationImport)
		{
			return operationImport.ContainerElementKind == EdmContainerElementKind.ActionImport;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0001F6B3 File Offset: 0x0001D8B3
		public static bool IsFunctionImport(this IEdmOperationImport operationImport)
		{
			return operationImport.ContainerElementKind == EdmContainerElementKind.FunctionImport;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0001F6C0 File Offset: 0x0001D8C0
		public static bool TryGetStaticEntitySet(this IEdmOperationImport operationImport, out IEdmEntitySet entitySet)
		{
			IEdmEntitySetReferenceExpression edmEntitySetReferenceExpression = operationImport.EntitySet as IEdmEntitySetReferenceExpression;
			entitySet = ((edmEntitySetReferenceExpression != null) ? edmEntitySetReferenceExpression.ReferencedEntitySet : null);
			return entitySet != null;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0001F6F0 File Offset: 0x0001D8F0
		public static bool TryGetRelativeEntitySetPath(this IEdmOperationImport operationImport, IEdmModel model, out IEdmOperationParameter parameter, out IEnumerable<IEdmNavigationProperty> relativePath, out IEnumerable<EdmError> edmErrors)
		{
			EdmUtil.CheckArgumentNull<IEdmOperationImport>(operationImport, "operationImport");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			parameter = null;
			relativePath = null;
			edmErrors = new ReadOnlyCollection<EdmError>(new List<EdmError>());
			IEdmPathExpression edmPathExpression = operationImport.EntitySet as IEdmPathExpression;
			if (edmPathExpression != null)
			{
				IEdmEntityType edmEntityType = null;
				Collection<EdmError> collection = new Collection<EdmError>();
				bool flag = ExtensionMethods.TryGetRelativeEntitySetPath(operationImport, collection, edmPathExpression, model, operationImport.Operation.Parameters, out parameter, out relativePath, out edmEntityType);
				edmErrors = new ReadOnlyCollection<EdmError>(collection);
				return flag;
			}
			return false;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0001F764 File Offset: 0x0001D964
		public static bool IsAction(this IEdmOperation operation)
		{
			return operation.SchemaElementKind == EdmSchemaElementKind.Action;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0001F76F File Offset: 0x0001D96F
		public static bool IsFunction(this IEdmOperation operation)
		{
			return operation.SchemaElementKind == EdmSchemaElementKind.Function;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0001F7A8 File Offset: 0x0001D9A8
		public static IEnumerable<IEdmOperation> FilterByName(this IEnumerable<IEdmOperation> operations, bool forceFullyQualifiedNameFilter, string operationName)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmOperation>>(operations, "operations");
			EdmUtil.CheckArgumentNull<string>(operationName, "operationName");
			if (forceFullyQualifiedNameFilter || operationName.IndexOf(".", 4) > -1)
			{
				return Enumerable.Where<IEdmOperation>(operations, (IEdmOperation o) => o.FullName() == operationName);
			}
			return Enumerable.Where<IEdmOperation>(operations, (IEdmOperation o) => o.Name == operationName);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0001F82C File Offset: 0x0001DA2C
		public static bool HasEquivalentBindingType(this IEdmOperation operation, IEdmType bindingType)
		{
			EdmUtil.CheckArgumentNull<IEdmOperation>(operation, "operation");
			EdmUtil.CheckArgumentNull<IEdmType>(bindingType, "bindingType");
			if (!operation.IsBound || !Enumerable.Any<IEdmOperationParameter>(operation.Parameters))
			{
				return false;
			}
			IEdmOperationParameter edmOperationParameter = Enumerable.First<IEdmOperationParameter>(operation.Parameters);
			IEdmType definition = edmOperationParameter.Type.Definition;
			if (definition.TypeKind != bindingType.TypeKind)
			{
				return false;
			}
			if (definition.TypeKind == EdmTypeKind.Collection)
			{
				IEdmCollectionType edmCollectionType = (IEdmCollectionType)definition;
				IEdmCollectionType edmCollectionType2 = (IEdmCollectionType)bindingType;
				return edmCollectionType2.ElementType.Definition.IsOrInheritsFrom(edmCollectionType.ElementType.Definition);
			}
			return bindingType.IsOrInheritsFrom(definition);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0001F8CC File Offset: 0x0001DACC
		public static IEdmPropertyConstructor FindProperty(this IEdmRecordExpression expression, string name)
		{
			foreach (IEdmPropertyConstructor edmPropertyConstructor in expression.Properties)
			{
				if (edmPropertyConstructor.Name == name)
				{
					return edmPropertyConstructor;
				}
			}
			return null;
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0001F928 File Offset: 0x0001DB28
		public static EdmNavigationSourceKind NavigationSourceKind(this IEdmNavigationSource navigationSource)
		{
			if (navigationSource is IEdmEntitySet)
			{
				return EdmNavigationSourceKind.EntitySet;
			}
			if (navigationSource is IEdmSingleton)
			{
				return EdmNavigationSourceKind.Singleton;
			}
			if (navigationSource is IEdmContainedEntitySet)
			{
				return EdmNavigationSourceKind.ContainedEntitySet;
			}
			if (navigationSource is IEdmUnknownEntitySet)
			{
				return EdmNavigationSourceKind.UnknownEntitySet;
			}
			return EdmNavigationSourceKind.None;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0001F954 File Offset: 0x0001DB54
		public static IEdmEntityType EntityType(this IEdmNavigationSource navigationSource)
		{
			IEdmEntitySetBase edmEntitySetBase = navigationSource as IEdmEntitySetBase;
			if (edmEntitySetBase != null)
			{
				IEdmCollectionType edmCollectionType = edmEntitySetBase.Type as IEdmCollectionType;
				if (edmCollectionType != null)
				{
					return edmCollectionType.ElementType.Definition as IEdmEntityType;
				}
				IEdmUnknownEntitySet edmUnknownEntitySet = edmEntitySetBase as IEdmUnknownEntitySet;
				if (edmUnknownEntitySet != null)
				{
					return edmUnknownEntitySet.Type as IEdmEntityType;
				}
				return null;
			}
			else
			{
				IEdmSingleton edmSingleton = navigationSource as IEdmSingleton;
				if (edmSingleton != null)
				{
					return edmSingleton.Type as IEdmEntityType;
				}
				return null;
			}
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0001F9BA File Offset: 0x0001DBBA
		public static void SetEdmReferences(this IEdmModel model, IEnumerable<IEdmReference> edmReferences)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "References", edmReferences);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0001F9DA File Offset: 0x0001DBDA
		public static IEnumerable<IEdmReference> GetEdmReferences(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return (IEnumerable<IEdmReference>)model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "References");
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0001F9FE File Offset: 0x0001DBFE
		internal static IEnumerable<IEdmOperation> FindOperationsInModelTree(this CsdlSemanticsModel model, string name)
		{
			return model.FindInModelTree(ExtensionMethods.findOperations, name, ExtensionMethods.mergeFunctions);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0001FA11 File Offset: 0x0001DC11
		internal static IEdmSchemaType FindTypeInModelTree(this CsdlSemanticsModel model, string name)
		{
			return model.FindInModelTree(ExtensionMethods.findType, name, new Func<IEdmSchemaType, IEdmSchemaType, IEdmSchemaType>(RegistrationHelper.CreateAmbiguousTypeBinding));
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0001FA2C File Offset: 0x0001DC2C
		internal static T FindInModelTree<T>(this CsdlSemanticsModel model, Func<IEdmModel, string, T> finderFunc, string qualifiedName, Func<T, T, T> ambiguousCreator)
		{
			EdmUtil.CheckArgumentNull<CsdlSemanticsModel>(model, "model");
			EdmUtil.CheckArgumentNull<Func<IEdmModel, string, T>>(finderFunc, "finderFunc");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			EdmUtil.CheckArgumentNull<Func<T, T, T>>(ambiguousCreator, "ambiguousCreator");
			T t = finderFunc.Invoke(model, qualifiedName);
			if (model.MainModel != null)
			{
				T t2;
				if ((t2 = finderFunc.Invoke(model.MainModel, qualifiedName)) != null)
				{
					t = ((t == null) ? t2 : ambiguousCreator.Invoke(t, t2));
				}
				foreach (IEdmModel edmModel in model.MainModel.ReferencedModels)
				{
					if (edmModel != EdmCoreModel.Instance && edmModel != CoreVocabularyModel.Instance && edmModel != model && (t2 = finderFunc.Invoke(edmModel, qualifiedName)) != null)
					{
						t = ((t == null) ? t2 : ambiguousCreator.Invoke(t, t2));
					}
				}
			}
			foreach (IEdmModel edmModel2 in model.ReferencedModels)
			{
				T t2 = finderFunc.Invoke(edmModel2, qualifiedName);
				if (t2 != null)
				{
					t = ((t == null) ? t2 : ambiguousCreator.Invoke(t, t2));
				}
			}
			return t;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0001FB84 File Offset: 0x0001DD84
		internal static bool TryGetRelativeEntitySetPath(IEdmElement element, Collection<EdmError> foundErrors, IEdmPathExpression pathExpression, IEdmModel model, IEnumerable<IEdmOperationParameter> parameters, out IEdmOperationParameter parameter, out IEnumerable<IEdmNavigationProperty> relativePath, out IEdmEntityType lastEntityType)
		{
			parameter = null;
			relativePath = null;
			lastEntityType = null;
			List<string> list = Enumerable.ToList<string>(pathExpression.Path);
			if (list.Count < 1)
			{
				foundErrors.Add(new EdmError(element.Location(), EdmErrorCode.OperationWithInvalidEntitySetPathMissingCompletePath, Strings.EdmModel_Validator_Semantic_InvalidEntitySetPathMissingBindingParameterName("EntitySetPath")));
				return false;
			}
			if (!Enumerable.Any<IEdmOperationParameter>(parameters))
			{
				return false;
			}
			bool flag = true;
			string text = Enumerable.First<string>(list);
			parameter = Enumerable.FirstOrDefault<IEdmOperationParameter>(parameters);
			if (parameter.Name != text)
			{
				foundErrors.Add(new EdmError(element.Location(), EdmErrorCode.InvalidPathFirstPathParameterNotMatchingFirstParameterName, Strings.EdmModel_Validator_Semantic_InvalidEntitySetPathWithFirstPathParameterNotMatchingFirstParameterName("EntitySetPath", EdmModelCsdlSchemaWriter.PathAsXml(pathExpression.Path), text, parameter.Name)));
				flag = false;
			}
			lastEntityType = parameter.Type.Definition as IEdmEntityType;
			if (lastEntityType == null)
			{
				IEdmCollectionTypeReference edmCollectionTypeReference = parameter.Type as IEdmCollectionTypeReference;
				if (edmCollectionTypeReference == null || !edmCollectionTypeReference.ElementType().IsEntity())
				{
					foundErrors.Add(new EdmError(element.Location(), EdmErrorCode.InvalidPathWithNonEntityBindingParameter, Strings.EdmModel_Validator_Semantic_InvalidEntitySetPathWithNonEntityBindingParameter("EntitySetPath", EdmModelCsdlSchemaWriter.PathAsXml(pathExpression.Path), text)));
					return false;
				}
				lastEntityType = edmCollectionTypeReference.ElementType().Definition as IEdmEntityType;
			}
			List<IEdmNavigationProperty> list2 = new List<IEdmNavigationProperty>();
			foreach (string text2 in Enumerable.Skip<string>(list, 1))
			{
				if (EdmUtil.IsQualifiedName(text2))
				{
					IEdmSchemaType edmSchemaType = model.FindDeclaredType(text2);
					if (edmSchemaType == null)
					{
						foundErrors.Add(new EdmError(element.Location(), EdmErrorCode.InvalidPathUnknownTypeCastSegment, Strings.EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownTypeCastSegment("EntitySetPath", EdmModelCsdlSchemaWriter.PathAsXml(pathExpression.Path), text2)));
						flag = false;
						break;
					}
					IEdmEntityType edmEntityType = edmSchemaType as IEdmEntityType;
					if (edmEntityType == null)
					{
						foundErrors.Add(new EdmError(element.Location(), EdmErrorCode.InvalidPathTypeCastSegmentMustBeEntityType, Strings.EdmModel_Validator_Semantic_InvalidEntitySetPathTypeCastSegmentMustBeEntityType("EntitySetPath", EdmModelCsdlSchemaWriter.PathAsXml(pathExpression.Path), edmSchemaType.FullName())));
						flag = false;
						break;
					}
					if (!edmEntityType.IsOrInheritsFrom(lastEntityType))
					{
						foundErrors.Add(new EdmError(element.Location(), EdmErrorCode.InvalidPathInvalidTypeCastSegment, Strings.EdmModel_Validator_Semantic_InvalidEntitySetPathInvalidTypeCastSegment("EntitySetPath", EdmModelCsdlSchemaWriter.PathAsXml(pathExpression.Path), lastEntityType.FullName(), edmEntityType.FullName())));
						flag = false;
						break;
					}
					lastEntityType = edmEntityType;
				}
				else
				{
					IEdmNavigationProperty edmNavigationProperty = lastEntityType.FindProperty(text2) as IEdmNavigationProperty;
					if (edmNavigationProperty == null)
					{
						foundErrors.Add(new EdmError(element.Location(), EdmErrorCode.InvalidPathUnknownNavigationProperty, Strings.EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownNavigationProperty("EntitySetPath", EdmModelCsdlSchemaWriter.PathAsXml(pathExpression.Path), text2)));
						flag = false;
						break;
					}
					list2.Add(edmNavigationProperty);
					lastEntityType = edmNavigationProperty.ToEntityType();
				}
			}
			relativePath = list2;
			return flag;
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0001FE50 File Offset: 0x0001E050
		internal static IEdmEntityType GetPathSegmentEntityType(IEdmTypeReference segmentType)
		{
			return (segmentType.IsCollection() ? segmentType.AsCollection().ElementType() : segmentType).AsEntity().EntityDefinition();
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0001FE72 File Offset: 0x0001E072
		internal static IEdmDocumentation GetDocumentation(this IEdmModel model, IEdmElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return (IEdmDocumentation)model.GetAnnotationValue(element, "http://schemas.microsoft.com/ado/2011/04/edm/documentation", "Documentation");
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0001FEA2 File Offset: 0x0001E0A2
		internal static void SetDocumentation(this IEdmModel model, IEdmElement element, IEdmDocumentation documentation)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			model.SetAnnotationValue(element, "http://schemas.microsoft.com/ado/2011/04/edm/documentation", "Documentation", documentation);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0001FED0 File Offset: 0x0001E0D0
		internal static IEnumerable<IEdmEntityContainerElement> AllElements(this IEdmEntityContainer container, int depth = 100)
		{
			if (depth <= 0)
			{
				throw new InvalidOperationException(Strings.Bad_CyclicEntityContainer(container.FullName()));
			}
			CsdlSemanticsEntityContainer csdlSemanticsEntityContainer = container as CsdlSemanticsEntityContainer;
			if (csdlSemanticsEntityContainer == null || csdlSemanticsEntityContainer.Extends == null)
			{
				return container.Elements;
			}
			return Enumerable.Concat<IEdmEntityContainerElement>(container.Elements, csdlSemanticsEntityContainer.Extends.AllElements(depth - 1));
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0001FF2D File Offset: 0x0001E12D
		internal static IEdmEntitySet FindEntitySetExtended(this IEdmEntityContainer container, string qualifiedName)
		{
			return ExtensionMethods.FindInContainerAndExtendsRecursively<IEdmEntitySet>(container, qualifiedName, (IEdmEntityContainer c, string n) => c.FindEntitySet(n), 100);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0001FF5E File Offset: 0x0001E15E
		internal static IEdmSingleton FindSingletonExtended(this IEdmEntityContainer container, string qualifiedName)
		{
			return ExtensionMethods.FindInContainerAndExtendsRecursively<IEdmSingleton>(container, qualifiedName, (IEdmEntityContainer c, string n) => c.FindSingleton(n), 100);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0001FF8F File Offset: 0x0001E18F
		internal static IEnumerable<IEdmOperationImport> FindOperationImportsExtended(this IEdmEntityContainer container, string qualifiedName)
		{
			return ExtensionMethods.FindInContainerAndExtendsRecursively<IEnumerable<IEdmOperationImport>>(container, qualifiedName, (IEdmEntityContainer c, string n) => c.FindOperationImports(n), 100);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0001FFB8 File Offset: 0x0001E1B8
		internal static IPrimitiveValueConverter GetPrimitiveValueConverter(this IEdmModel model, string fullTypeName)
		{
			IDictionary<string, IPrimitiveValueConverter> annotationValue = model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "PrimitiveValueConverterMap");
			if (annotationValue == null)
			{
				return PassThroughPrimitiveValueConverter.Instance;
			}
			IPrimitiveValueConverter primitiveValueConverter;
			if (!annotationValue.TryGetValue(fullTypeName, ref primitiveValueConverter))
			{
				return PassThroughPrimitiveValueConverter.Instance;
			}
			return primitiveValueConverter;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0001FFF4 File Offset: 0x0001E1F4
		internal static void SetPrimitiveValueConverter(this IEdmModel model, string fullTypeName, IPrimitiveValueConverter converter)
		{
			IDictionary<string, IPrimitiveValueConverter> dictionary = model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "PrimitiveValueConverterMap");
			if (dictionary == null)
			{
				dictionary = new Dictionary<string, IPrimitiveValueConverter>(StringComparer.Ordinal);
				model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "PrimitiveValueConverterMap", dictionary);
			}
			dictionary[fullTypeName] = converter;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00020074 File Offset: 0x0001E274
		private static IEnumerable<IDictionary<string, IEdmProperty>> GetDeclaredAlternateKeysForType(IEdmEntityType type, IEdmModel model)
		{
			IEdmValueAnnotation edmValueAnnotation = Enumerable.FirstOrDefault<IEdmValueAnnotation>(model.FindVocabularyAnnotations(type, AlternateKeysVocabularyModel.AlternateKeysTerm));
			if (edmValueAnnotation != null)
			{
				List<IDictionary<string, IEdmProperty>> list = new List<IDictionary<string, IEdmProperty>>();
				IEdmCollectionExpression edmCollectionExpression = edmValueAnnotation.Value as IEdmCollectionExpression;
				foreach (IEdmRecordExpression edmRecordExpression in Enumerable.OfType<IEdmRecordExpression>(edmCollectionExpression.Elements))
				{
					IEdmPropertyConstructor edmPropertyConstructor = Enumerable.FirstOrDefault<IEdmPropertyConstructor>(edmRecordExpression.Properties, (IEdmPropertyConstructor e) => e.Name == "Key");
					if (edmPropertyConstructor != null)
					{
						IEdmCollectionExpression edmCollectionExpression2 = edmPropertyConstructor.Value as IEdmCollectionExpression;
						IDictionary<string, IEdmProperty> dictionary = new Dictionary<string, IEdmProperty>();
						foreach (IEdmRecordExpression edmRecordExpression2 in Enumerable.OfType<IEdmRecordExpression>(edmCollectionExpression2.Elements))
						{
							IEdmPropertyConstructor edmPropertyConstructor2 = Enumerable.FirstOrDefault<IEdmPropertyConstructor>(edmRecordExpression2.Properties, (IEdmPropertyConstructor e) => e.Name == "Alias");
							string value = ((IEdmStringConstantExpression)edmPropertyConstructor2.Value).Value;
							IEdmPropertyConstructor edmPropertyConstructor3 = Enumerable.FirstOrDefault<IEdmPropertyConstructor>(edmRecordExpression2.Properties, (IEdmPropertyConstructor e) => e.Name == "Name");
							string text = Enumerable.FirstOrDefault<string>(((IEdmPathExpression)edmPropertyConstructor3.Value).Path);
							dictionary[value] = type.FindProperty(text);
						}
						if (Enumerable.Any<KeyValuePair<string, IEdmProperty>>(dictionary))
						{
							list.Add(dictionary);
						}
					}
				}
				return list;
			}
			return null;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00020240 File Offset: 0x0001E440
		private static T FindAcrossModels<T, TInput>(this IEdmModel model, TInput qualifiedName, Func<IEdmModel, TInput, T> finder, Func<T, T, T> ambiguousCreator)
		{
			T t = finder.Invoke(model, qualifiedName);
			foreach (IEdmModel edmModel in model.ReferencedModels)
			{
				T t2 = finder.Invoke(edmModel, qualifiedName);
				if (t2 != null)
				{
					t = ((t == null) ? t2 : ambiguousCreator.Invoke(t, t2));
				}
			}
			return t;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x000202B8 File Offset: 0x0001E4B8
		private static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmEntityType contextType, IEdmValueTerm term, string qualifier, Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T> evaluator)
		{
			IEnumerable<IEdmValueAnnotation> enumerable = model.FindVocabularyAnnotations(contextType, term, qualifier);
			if (Enumerable.Count<IEdmValueAnnotation>(enumerable) != 1)
			{
				throw new InvalidOperationException(Strings.Edm_Evaluator_NoValueAnnotationOnType(contextType.ToTraceString(), term.ToTraceString()));
			}
			return evaluator.Invoke(Enumerable.Single<IEdmValueAnnotation>(enumerable).Value, context, term.Type);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002030C File Offset: 0x0001E50C
		private static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmEntityType contextType, string termName, string qualifier, Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T> evaluator)
		{
			IEnumerable<IEdmValueAnnotation> enumerable = model.FindVocabularyAnnotations(contextType, termName, qualifier);
			if (Enumerable.Count<IEdmValueAnnotation>(enumerable) != 1)
			{
				throw new InvalidOperationException(Strings.Edm_Evaluator_NoValueAnnotationOnType(contextType.ToTraceString(), termName));
			}
			IEdmValueAnnotation edmValueAnnotation = Enumerable.Single<IEdmValueAnnotation>(enumerable);
			return evaluator.Invoke(edmValueAnnotation.Value, context, edmValueAnnotation.ValueTerm().Type);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00020360 File Offset: 0x0001E560
		private static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmValueTerm term, string qualifier, Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T> evaluator)
		{
			IEnumerable<IEdmValueAnnotation> enumerable = model.FindVocabularyAnnotations(element, term, qualifier);
			if (Enumerable.Count<IEdmValueAnnotation>(enumerable) != 1)
			{
				throw new InvalidOperationException(Strings.Edm_Evaluator_NoValueAnnotationOnElement(term.ToTraceString()));
			}
			return evaluator.Invoke(Enumerable.Single<IEdmValueAnnotation>(enumerable).Value, null, term.Type);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x000203AC File Offset: 0x0001E5AC
		private static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, string qualifier, Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T> evaluator)
		{
			IEnumerable<IEdmValueAnnotation> enumerable = model.FindVocabularyAnnotations(element, termName, qualifier);
			if (Enumerable.Count<IEdmValueAnnotation>(enumerable) != 1)
			{
				throw new InvalidOperationException(Strings.Edm_Evaluator_NoValueAnnotationOnElement(termName));
			}
			IEdmValueAnnotation edmValueAnnotation = Enumerable.Single<IEdmValueAnnotation>(enumerable);
			return evaluator.Invoke(edmValueAnnotation.Value, null, edmValueAnnotation.ValueTerm().Type);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x000203F8 File Offset: 0x0001E5F8
		private static T FindInContainerAndExtendsRecursively<T>(IEdmEntityContainer container, string simpleName, Func<IEdmEntityContainer, string, T> finderFunc, int deepth)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			if (deepth <= 0)
			{
				throw new InvalidOperationException(Strings.Bad_CyclicEntityContainer(container.FullName()));
			}
			T t = finderFunc.Invoke(container, simpleName);
			IEnumerable<IEdmOperationImport> enumerable = t as IEnumerable<IEdmOperationImport>;
			if (t == null || (enumerable != null && !Enumerable.Any<IEdmOperationImport>(enumerable)))
			{
				CsdlSemanticsEntityContainer csdlSemanticsEntityContainer = container as CsdlSemanticsEntityContainer;
				if (csdlSemanticsEntityContainer != null && csdlSemanticsEntityContainer.Extends != null)
				{
					return ExtensionMethods.FindInContainerAndExtendsRecursively<T>(csdlSemanticsEntityContainer.Extends, simpleName, finderFunc, --deepth);
				}
			}
			return t;
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00020478 File Offset: 0x0001E678
		private static T AnnotationValue<T>(object annotation) where T : class
		{
			if (annotation == null)
			{
				return default(T);
			}
			T t = annotation as T;
			if (t != null)
			{
				return t;
			}
			IEdmValue edmValue = annotation as IEdmValue;
			throw new InvalidOperationException(Strings.Annotations_TypeMismatch(annotation.GetType().Name, typeof(T).Name));
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x000204D8 File Offset: 0x0001E6D8
		private static void DerivedFrom(this IEdmModel model, IEdmStructuredType baseType, HashSetInternal<IEdmStructuredType> visited, List<IEdmStructuredType> derivedTypes)
		{
			if (visited.Add(baseType))
			{
				IEnumerable<IEdmStructuredType> enumerable = model.FindDirectlyDerivedTypes(baseType);
				if (enumerable != null && Enumerable.Any<IEdmStructuredType>(enumerable))
				{
					foreach (IEdmStructuredType edmStructuredType in enumerable)
					{
						derivedTypes.Add(edmStructuredType);
						model.DerivedFrom(edmStructuredType, visited, derivedTypes);
					}
				}
				foreach (IEdmModel edmModel in model.ReferencedModels)
				{
					enumerable = edmModel.FindDirectlyDerivedTypes(baseType);
					if (enumerable != null && Enumerable.Any<IEdmStructuredType>(enumerable))
					{
						foreach (IEdmStructuredType edmStructuredType2 in enumerable)
						{
							derivedTypes.Add(edmStructuredType2);
							model.DerivedFrom(edmStructuredType2, visited, derivedTypes);
						}
					}
				}
			}
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x000205FC File Offset: 0x0001E7FC
		private static void SetChangeTrackingAnnotationImplementation(this EdmModel model, IEdmVocabularyAnnotatable target, bool isSupported, IEnumerable<IEdmStructuralProperty> filterableProperties, IEnumerable<IEdmNavigationProperty> expandableProperties)
		{
			EdmUtil.CheckArgumentNull<EdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(target, "target");
			if (filterableProperties == null)
			{
				filterableProperties = ExtensionMethods.EmptyStructuralProperties;
			}
			if (expandableProperties == null)
			{
				expandableProperties = ExtensionMethods.EmptyNavigationProperties;
			}
			List<IEdmPropertyConstructor> list = new List<IEdmPropertyConstructor>();
			list.Add(new EdmPropertyConstructor("Supported", new EdmBooleanConstant(isSupported)));
			list.Add(new EdmPropertyConstructor("FilterableProperties", new EdmCollectionExpression(Enumerable.ToArray<EdmPropertyPathExpression>(Enumerable.Select<IEdmStructuralProperty, EdmPropertyPathExpression>(filterableProperties, (IEdmStructuralProperty p) => new EdmPropertyPathExpression(p.Name))))));
			list.Add(new EdmPropertyConstructor("ExpandableProperties", new EdmCollectionExpression(Enumerable.ToArray<EdmNavigationPropertyPathExpression>(Enumerable.Select<IEdmNavigationProperty, EdmNavigationPropertyPathExpression>(expandableProperties, (IEdmNavigationProperty p) => new EdmNavigationPropertyPathExpression(p.Name))))));
			IList<IEdmPropertyConstructor> list2 = list;
			IEdmRecordExpression edmRecordExpression = new EdmRecordExpression(list2);
			IEdmValueTerm changeTrackingTerm = CapabilitiesVocabularyModel.ChangeTrackingTerm;
			EdmAnnotation edmAnnotation = new EdmAnnotation(target, changeTrackingTerm, edmRecordExpression);
			edmAnnotation.SetSerializationLocation(model, new EdmVocabularyAnnotationSerializationLocation?(EdmVocabularyAnnotationSerializationLocation.Inline));
			model.SetVocabularyAnnotation(edmAnnotation);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00020700 File Offset: 0x0001E900
		private static IEdmTypeDefinitionReference GetUIntImplementation(this EdmModel model, string namespaceName, string name, string underlyingType, bool isNullable)
		{
			EdmUtil.CheckArgumentNull<EdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			string text = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[] { namespaceName, name });
			IEdmTypeDefinition edmTypeDefinition = model.FindDeclaredType(text) as IEdmTypeDefinition;
			if (edmTypeDefinition == null)
			{
				edmTypeDefinition = new EdmTypeDefinition(namespaceName, name, EdmCoreModel.Instance.GetPrimitiveTypeKind(underlyingType));
				model.AddElement(edmTypeDefinition);
				model.SetPrimitiveValueConverter(text, DefaultPrimitiveValueConverter.Instance);
			}
			return new EdmTypeDefinitionReference(edmTypeDefinition, isNullable);
		}

		// Token: 0x040004F1 RID: 1265
		private const int ContainerExtendsMaxDepth = 100;

		// Token: 0x040004F2 RID: 1266
		private const string CollectionTypeFormat = "Collection({0})";

		// Token: 0x040004F3 RID: 1267
		private static readonly IEnumerable<IEdmStructuralProperty> EmptyStructuralProperties = new Collection<IEdmStructuralProperty>();

		// Token: 0x040004F4 RID: 1268
		private static readonly IEnumerable<IEdmNavigationProperty> EmptyNavigationProperties = new Collection<IEdmNavigationProperty>();

		// Token: 0x040004F5 RID: 1269
		private static readonly Func<IEdmModel, string, IEdmSchemaType> findType = (IEdmModel model, string qualifiedName) => model.FindDeclaredType(qualifiedName);

		// Token: 0x040004F6 RID: 1270
		private static readonly Func<IEdmModel, IEdmType, IEnumerable<IEdmOperation>> findBoundOperations = (IEdmModel model, IEdmType bindingType) => model.FindDeclaredBoundOperations(bindingType);

		// Token: 0x040004F7 RID: 1271
		private static readonly Func<IEdmModel, string, IEdmValueTerm> findValueTerm = (IEdmModel model, string qualifiedName) => model.FindDeclaredValueTerm(qualifiedName);

		// Token: 0x040004F8 RID: 1272
		private static readonly Func<IEdmModel, string, IEnumerable<IEdmOperation>> findOperations = (IEdmModel model, string qualifiedName) => model.FindDeclaredOperations(qualifiedName);

		// Token: 0x040004F9 RID: 1273
		private static readonly Func<IEdmModel, string, IEdmEntityContainer> findEntityContainer = delegate(IEdmModel model, string qualifiedName)
		{
			if (!model.ExistsContainer(qualifiedName))
			{
				return null;
			}
			return model.EntityContainer;
		};

		// Token: 0x040004FA RID: 1274
		private static readonly Func<IEnumerable<IEdmOperation>, IEnumerable<IEdmOperation>, IEnumerable<IEdmOperation>> mergeFunctions = (IEnumerable<IEdmOperation> f1, IEnumerable<IEdmOperation> f2) => Enumerable.Concat<IEdmOperation>(f1, f2);

		// Token: 0x020001E3 RID: 483
		internal static class TypeName<T>
		{
			// Token: 0x0400050D RID: 1293
			public static readonly string LocalName = typeof(T).ToString().Replace("_", "_____").Replace('.', '_')
				.Replace("[", "")
				.Replace("]", "")
				.Replace(",", "__")
				.Replace("`", "___")
				.Replace("+", "____");
		}
	}
}
