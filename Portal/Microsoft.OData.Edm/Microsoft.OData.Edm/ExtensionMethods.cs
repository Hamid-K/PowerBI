using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Microsoft.OData.Edm.Csdl;
using Microsoft.OData.Edm.Csdl.CsdlSemantics;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Csdl.Serialization;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;
using Microsoft.OData.Edm.Vocabularies.Community.V1;
using Microsoft.OData.Edm.Vocabularies.V1;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000080 RID: 128
	public static class ExtensionMethods
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x00006B6C File Offset: 0x00004D6C
		public static Version GetEdmVersion(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "EdmVersion");
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00006B8B File Offset: 0x00004D8B
		public static void SetEdmVersion(this IEdmModel model, Version version)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "EdmVersion", version);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00006BAC File Offset: 0x00004DAC
		public static IEdmSchemaType FindType(this IEdmModel model, string qualifiedName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			string text = model.ReplaceAlias(qualifiedName);
			return model.FindAcrossModels(text, ExtensionMethods.findType, new Func<IEdmSchemaType, IEdmSchemaType, IEdmSchemaType>(RegistrationHelper.CreateAmbiguousTypeBinding));
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00006BF1 File Offset: 0x00004DF1
		public static IEnumerable<IEdmOperation> FindBoundOperations(this IEdmModel model, IEdmType bindingType)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmType>(bindingType, "bindingType");
			return model.FindAcrossModels(bindingType, ExtensionMethods.findBoundOperations, ExtensionMethods.mergeFunctions);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00006C1C File Offset: 0x00004E1C
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
					enumerable = ((enumerable == null) ? enumerable2 : ExtensionMethods.mergeFunctions(enumerable, enumerable2));
				}
			}
			return enumerable;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00006CB0 File Offset: 0x00004EB0
		public static IEdmTerm FindTerm(this IEdmModel model, string qualifiedName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			return model.FindAcrossModels(qualifiedName, ExtensionMethods.findTerm, new Func<IEdmTerm, IEdmTerm, IEdmTerm>(RegistrationHelper.CreateAmbiguousTermBinding));
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00006CE2 File Offset: 0x00004EE2
		public static IEnumerable<IEdmOperation> FindOperations(this IEdmModel model, string qualifiedName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			return model.FindAcrossModels(qualifiedName, ExtensionMethods.findOperations, ExtensionMethods.mergeFunctions);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00006D10 File Offset: 0x00004F10
		public static bool ExistsContainer(this IEdmModel model, string containerName)
		{
			if (model.EntityContainer == null)
			{
				return false;
			}
			string text = (model.EntityContainer.Namespace ?? string.Empty) + "." + (containerName ?? string.Empty);
			return string.Equals(model.EntityContainer.FullName(), text, StringComparison.Ordinal) || string.Equals(model.EntityContainer.FullName(), containerName, StringComparison.Ordinal);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00006D7B File Offset: 0x00004F7B
		public static IEdmEntityContainer FindEntityContainer(this IEdmModel model, string qualifiedName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			return model.FindAcrossModels(qualifiedName, ExtensionMethods.findEntityContainer, new Func<IEdmEntityContainer, IEdmEntityContainer, IEdmEntityContainer>(RegistrationHelper.CreateAmbiguousEntityContainerBinding));
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00006DB0 File Offset: 0x00004FB0
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
						enumerable = enumerable.Concat(model.FindDeclaredVocabularyAnnotations(edmVocabularyAnnotatable));
					}
				}
			}
			return enumerable;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00006E14 File Offset: 0x00005014
		public static IEnumerable<IEdmVocabularyAnnotation> FindVocabularyAnnotations(this IEdmModel model, IEdmVocabularyAnnotatable element)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			IEnumerable<IEdmVocabularyAnnotation> enumerable = model.FindVocabularyAnnotationsIncludingInheritedAnnotations(element);
			foreach (IEdmModel edmModel in model.ReferencedModels)
			{
				enumerable = enumerable.Concat(edmModel.FindVocabularyAnnotationsIncludingInheritedAnnotations(element));
			}
			return enumerable;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00006E8C File Offset: 0x0000508C
		public static IEnumerable<T> FindVocabularyAnnotations<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmTerm term) where T : IEdmVocabularyAnnotation
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			return model.FindVocabularyAnnotations(element, term, null);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00006EBC File Offset: 0x000050BC
		public static IEnumerable<T> FindVocabularyAnnotations<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmTerm term, string qualifier) where T : IEdmVocabularyAnnotation
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			List<T> list = null;
			foreach (T t in model.FindVocabularyAnnotations(element).OfType<T>())
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
			IEnumerable<T> enumerable = list;
			return enumerable ?? Enumerable.Empty<T>();
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00006F74 File Offset: 0x00005174
		public static IEnumerable<T> FindVocabularyAnnotations<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName) where T : IEdmVocabularyAnnotation
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			return model.FindVocabularyAnnotations(element, termName, null);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00006FA3 File Offset: 0x000051A3
		public static IEnumerable<T> FindVocabularyAnnotations<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, string qualifier) where T : IEdmVocabularyAnnotation
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			string namespaceName;
			string name;
			if (EdmUtil.TryGetNamespaceNameFromQualifiedName(termName, out namespaceName, out name))
			{
				foreach (T t in model.FindVocabularyAnnotations(element).OfType<T>())
				{
					IEdmTerm term = t.Term;
					if (term.Namespace == namespaceName && term.Name == name && (qualifier == null || qualifier == t.Qualifier))
					{
						yield return t;
					}
				}
				IEnumerator<T> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00006FC8 File Offset: 0x000051C8
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmStructuredValue context, string termName, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), termName, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000702C File Offset: 0x0000522C
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmStructuredValue context, string termName, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), termName, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00007090 File Offset: 0x00005290
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmStructuredValue context, IEdmTerm term, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), term, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000070F4 File Offset: 0x000052F4
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmStructuredValue context, IEdmTerm term, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), term, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00007158 File Offset: 0x00005358
		public static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, string termName, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), termName, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000071BC File Offset: 0x000053BC
		public static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, string termName, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), termName, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00007220 File Offset: 0x00005420
		public static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmTerm term, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), term, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00007284 File Offset: 0x00005484
		public static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmTerm term, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), term, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000072E8 File Offset: 0x000054E8
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "evaluator");
			return model.GetTermValue(element, termName, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000733C File Offset: 0x0000553C
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "evaluator");
			return model.GetTermValue(element, termName, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00007390 File Offset: 0x00005590
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmTerm term, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "evaluator");
			return model.GetTermValue(element, term, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000073E4 File Offset: 0x000055E4
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmTerm term, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "evaluator");
			return model.GetTermValue(element, term, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00007438 File Offset: 0x00005638
		public static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(element, termName, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000748C File Offset: 0x0000568C
		public static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(element, termName, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000074E0 File Offset: 0x000056E0
		public static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmTerm term, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(element, term, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00007534 File Offset: 0x00005734
		public static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmTerm term, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(element, term, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00007588 File Offset: 0x00005788
		public static object GetAnnotationValue(this IEdmModel model, IEdmElement element, string namespaceName, string localName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return model.DirectValueAnnotationsManager.GetAnnotationValue(element, namespaceName, localName);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000075B0 File Offset: 0x000057B0
		public static T GetAnnotationValue<T>(this IEdmModel model, IEdmElement element, string namespaceName, string localName) where T : class
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return ExtensionMethods.AnnotationValue<T>(model.GetAnnotationValue(element, namespaceName, localName));
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000075D8 File Offset: 0x000057D8
		public static T GetAnnotationValue<T>(this IEdmModel model, IEdmElement element) where T : class
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return model.GetAnnotationValue(element, "http://schemas.microsoft.com/ado/2011/04/edm/internal", ExtensionMethods.TypeName<T>.LocalName);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00007603 File Offset: 0x00005803
		public static void SetAnnotationValue(this IEdmModel model, IEdmElement element, string namespaceName, string localName, object value)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			model.DirectValueAnnotationsManager.SetAnnotationValue(element, namespaceName, localName, value);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00007630 File Offset: 0x00005830
		public static string GetDescriptionAnnotation(this IEdmModel model, IEdmVocabularyAnnotatable target)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(target, "target");
			IEdmVocabularyAnnotation edmVocabularyAnnotation = model.FindVocabularyAnnotations(target, CoreVocabularyModel.DescriptionTerm).FirstOrDefault<IEdmVocabularyAnnotation>();
			if (edmVocabularyAnnotation != null)
			{
				IEdmStringConstantExpression edmStringConstantExpression = edmVocabularyAnnotation.Value as IEdmStringConstantExpression;
				if (edmStringConstantExpression != null)
				{
					return edmStringConstantExpression.Value;
				}
			}
			return null;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00007684 File Offset: 0x00005884
		public static string GetLongDescriptionAnnotation(this IEdmModel model, IEdmVocabularyAnnotatable target)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(target, "target");
			IEdmVocabularyAnnotation edmVocabularyAnnotation = model.FindVocabularyAnnotations(target, CoreVocabularyModel.LongDescriptionTerm).FirstOrDefault<IEdmVocabularyAnnotation>();
			if (edmVocabularyAnnotation != null)
			{
				IEdmStringConstantExpression edmStringConstantExpression = edmVocabularyAnnotation.Value as IEdmStringConstantExpression;
				if (edmStringConstantExpression != null)
				{
					return edmStringConstantExpression.Value;
				}
			}
			return null;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000076D8 File Offset: 0x000058D8
		public static IEnumerable<string> GetDerivedTypeConstraints(this IEdmModel model, IEdmNavigationSource navigationSource)
		{
			if (model == null || navigationSource == null)
			{
				return null;
			}
			IEnumerable<string> enumerable = null;
			EdmNavigationSourceKind edmNavigationSourceKind = navigationSource.NavigationSourceKind();
			if (edmNavigationSourceKind != EdmNavigationSourceKind.EntitySet)
			{
				if (edmNavigationSourceKind == EdmNavigationSourceKind.Singleton)
				{
					enumerable = model.GetDerivedTypeConstraints((IEdmSingleton)navigationSource);
				}
			}
			else
			{
				enumerable = model.GetDerivedTypeConstraints((IEdmEntitySet)navigationSource);
			}
			return enumerable;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00007720 File Offset: 0x00005920
		public static IEnumerable<string> GetDerivedTypeConstraints(this IEdmModel model, IEdmVocabularyAnnotatable target)
		{
			if (model == null || target == null)
			{
				return null;
			}
			IEdmVocabularyAnnotation edmVocabularyAnnotation = model.FindVocabularyAnnotations(target, ValidationVocabularyModel.DerivedTypeConstraintTerm).FirstOrDefault<IEdmVocabularyAnnotation>();
			if (edmVocabularyAnnotation != null)
			{
				IEdmCollectionExpression edmCollectionExpression = edmVocabularyAnnotation.Value as IEdmCollectionExpression;
				if (edmCollectionExpression != null && edmCollectionExpression.Elements != null)
				{
					return from e in edmCollectionExpression.Elements.OfType<IEdmStringConstantExpression>()
						select e.Value;
				}
			}
			return null;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00007794 File Offset: 0x00005994
		public static IEnumerable<IEdmSchemaElement> SchemaElementsAcrossModels(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			IEnumerable<IEdmSchemaElement> enumerable = new IEdmSchemaElement[0];
			foreach (IEdmModel edmModel in model.ReferencedModels)
			{
				enumerable = enumerable.Concat(edmModel.SchemaElements);
			}
			enumerable = enumerable.Concat(model.SchemaElements);
			return enumerable;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00007808 File Offset: 0x00005A08
		public static IEnumerable<IEdmStructuredType> FindAllDerivedTypes(this IEdmModel model, IEdmStructuredType baseType)
		{
			List<IEdmStructuredType> list = new List<IEdmStructuredType>();
			if (baseType is IEdmSchemaElement)
			{
				model.DerivedFrom(baseType, new HashSetInternal<IEdmStructuredType>(), list);
			}
			return list;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00007831 File Offset: 0x00005A31
		public static void SetAnnotationValue<T>(this IEdmModel model, IEdmElement element, T value) where T : class
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			model.SetAnnotationValue(element, "http://schemas.microsoft.com/ado/2011/04/edm/internal", ExtensionMethods.TypeName<T>.LocalName, value);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00007862 File Offset: 0x00005A62
		public static object[] GetAnnotationValues(this IEdmModel model, IEnumerable<IEdmDirectValueAnnotationBinding> annotations)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmDirectValueAnnotationBinding>>(annotations, "annotations");
			return model.DirectValueAnnotationsManager.GetAnnotationValues(annotations);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00007888 File Offset: 0x00005A88
		public static void SetAnnotationValues(this IEdmModel model, IEnumerable<IEdmDirectValueAnnotationBinding> annotations)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmDirectValueAnnotationBinding>>(annotations, "annotations");
			model.DirectValueAnnotationsManager.SetAnnotationValues(annotations);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000078AE File Offset: 0x00005AAE
		public static IEnumerable<IEdmDirectValueAnnotation> DirectValueAnnotations(this IEdmModel model, IEdmElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return model.DirectValueAnnotationsManager.GetDirectValueAnnotations(element);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000078D4 File Offset: 0x00005AD4
		public static bool TryFindContainerQualifiedEntitySet(this IEdmModel model, string containerQualifiedEntitySetName, out IEdmEntitySet entitySet)
		{
			entitySet = null;
			string text = null;
			string text2 = null;
			if (containerQualifiedEntitySetName != null && containerQualifiedEntitySetName.IndexOf(".", StringComparison.Ordinal) > -1 && EdmUtil.TryParseContainerQualifiedElementName(containerQualifiedEntitySetName, out text, out text2) && model.ExistsContainer(text))
			{
				IEdmEntityContainer entityContainer = model.EntityContainer;
				if (entityContainer != null)
				{
					entitySet = entityContainer.FindEntitySetExtended(text2);
				}
			}
			return entitySet != null;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00007928 File Offset: 0x00005B28
		public static bool TryFindContainerQualifiedSingleton(this IEdmModel model, string containerQualifiedSingletonName, out IEdmSingleton singleton)
		{
			singleton = null;
			string text = null;
			string text2 = null;
			if (containerQualifiedSingletonName != null && containerQualifiedSingletonName.IndexOf(".", StringComparison.Ordinal) > -1 && EdmUtil.TryParseContainerQualifiedElementName(containerQualifiedSingletonName, out text, out text2) && model.ExistsContainer(text))
			{
				singleton = model.EntityContainer.FindSingletonExtended(text2);
				if (singleton != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00007978 File Offset: 0x00005B78
		public static bool TryFindContainerQualifiedOperationImports(this IEdmModel model, string containerQualifiedOperationImportName, out IEnumerable<IEdmOperationImport> operationImports)
		{
			operationImports = null;
			string text = null;
			string text2 = null;
			if (containerQualifiedOperationImportName.IndexOf(".", StringComparison.Ordinal) > -1 && EdmUtil.TryParseContainerQualifiedElementName(containerQualifiedOperationImportName, out text, out text2) && model.ExistsContainer(text))
			{
				operationImports = model.EntityContainer.FindOperationImportsExtended(text2);
				if (operationImports != null && operationImports.Count<IEdmOperationImport>() > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000079D0 File Offset: 0x00005BD0
		public static IEdmEntitySet FindDeclaredEntitySet(this IEdmModel model, string qualifiedName)
		{
			IEdmEntitySet edmEntitySet = null;
			if (!model.TryFindContainerQualifiedEntitySet(qualifiedName, out edmEntitySet))
			{
				try
				{
					IEdmEntityContainer entityContainer = model.EntityContainer;
					if (entityContainer != null)
					{
						return entityContainer.FindEntitySetExtended(qualifiedName);
					}
				}
				catch (NotImplementedException)
				{
					return null;
				}
				return edmEntitySet;
			}
			return edmEntitySet;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00007A18 File Offset: 0x00005C18
		public static IEdmSingleton FindDeclaredSingleton(this IEdmModel model, string qualifiedName)
		{
			IEdmSingleton edmSingleton = null;
			if (!model.TryFindContainerQualifiedSingleton(qualifiedName, out edmSingleton))
			{
				try
				{
					IEdmEntityContainer entityContainer = model.EntityContainer;
					if (entityContainer != null)
					{
						return entityContainer.FindSingletonExtended(qualifiedName);
					}
				}
				catch (NotImplementedException)
				{
					return null;
				}
				return edmSingleton;
			}
			return edmSingleton;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00007A60 File Offset: 0x00005C60
		public static IEdmNavigationSource FindDeclaredNavigationSource(this IEdmModel model, string qualifiedName)
		{
			IEdmEntitySet edmEntitySet = model.FindDeclaredEntitySet(qualifiedName);
			if (edmEntitySet != null)
			{
				return edmEntitySet;
			}
			return model.FindDeclaredSingleton(qualifiedName);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00007A84 File Offset: 0x00005C84
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

		// Token: 0x0600030D RID: 781 RVA: 0x00007AB1 File Offset: 0x00005CB1
		public static IPrimitiveValueConverter GetPrimitiveValueConverter(this IEdmModel model, IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "mode");
			if (type == null || !type.IsTypeDefinition())
			{
				return PassThroughPrimitiveValueConverter.Instance;
			}
			return model.GetPrimitiveValueConverter(type.Definition);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00007ADC File Offset: 0x00005CDC
		public static void SetPrimitiveValueConverter(this IEdmModel model, IEdmTypeDefinitionReference typeDefinition, IPrimitiveValueConverter converter)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmTypeDefinitionReference>(typeDefinition, "typeDefinition");
			EdmUtil.CheckArgumentNull<IPrimitiveValueConverter>(converter, "converter");
			model.SetPrimitiveValueConverter(typeDefinition.Definition, converter);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00007B0F File Offset: 0x00005D0F
		public static EdmComplexType AddComplexType(this EdmModel model, string namespaceName, string name)
		{
			return model.AddComplexType(namespaceName, name, null, false);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00007B1B File Offset: 0x00005D1B
		public static EdmComplexType AddComplexType(this EdmModel model, string namespaceName, string name, IEdmComplexType baseType)
		{
			return model.AddComplexType(namespaceName, name, baseType, false, false);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00007B28 File Offset: 0x00005D28
		public static EdmComplexType AddComplexType(this EdmModel model, string namespaceName, string name, IEdmComplexType baseType, bool isAbstract)
		{
			return model.AddComplexType(namespaceName, name, baseType, isAbstract, false);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00007B38 File Offset: 0x00005D38
		public static EdmComplexType AddComplexType(this EdmModel model, string namespaceName, string name, IEdmComplexType baseType, bool isAbstract, bool isOpen)
		{
			EdmComplexType edmComplexType = new EdmComplexType(namespaceName, name, baseType, isAbstract, isOpen);
			model.AddElement(edmComplexType);
			return edmComplexType;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00007B5A File Offset: 0x00005D5A
		public static EdmEntityType AddEntityType(this EdmModel model, string namespaceName, string name)
		{
			return model.AddEntityType(namespaceName, name, null, false, false);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00007B67 File Offset: 0x00005D67
		public static EdmEntityType AddEntityType(this EdmModel model, string namespaceName, string name, IEdmEntityType baseType)
		{
			return model.AddEntityType(namespaceName, name, baseType, false, false);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00007B74 File Offset: 0x00005D74
		public static EdmEntityType AddEntityType(this EdmModel model, string namespaceName, string name, IEdmEntityType baseType, bool isAbstract, bool isOpen)
		{
			return model.AddEntityType(namespaceName, name, baseType, isAbstract, isOpen, false);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00007B84 File Offset: 0x00005D84
		public static EdmEntityType AddEntityType(this EdmModel model, string namespaceName, string name, IEdmEntityType baseType, bool isAbstract, bool isOpen, bool hasStream)
		{
			EdmEntityType edmEntityType = new EdmEntityType(namespaceName, name, baseType, isAbstract, isOpen, hasStream);
			model.AddElement(edmEntityType);
			return edmEntityType;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00007BA8 File Offset: 0x00005DA8
		public static EdmEntityContainer AddEntityContainer(this EdmModel model, string namespaceName, string name)
		{
			EdmEntityContainer edmEntityContainer = new EdmEntityContainer(namespaceName, name);
			model.AddElement(edmEntityContainer);
			return edmEntityContainer;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00007BC8 File Offset: 0x00005DC8
		public static EdmTerm AddTerm(this EdmModel model, string namespaceName, string name, EdmPrimitiveTypeKind kind)
		{
			EdmTerm edmTerm = new EdmTerm(namespaceName, name, kind);
			model.AddElement(edmTerm);
			return edmTerm;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00007BE6 File Offset: 0x00005DE6
		public static EdmTerm AddTerm(this EdmModel model, string namespaceName, string name, IEdmTypeReference type)
		{
			return model.AddTerm(namespaceName, name, type, null, null);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00007BF4 File Offset: 0x00005DF4
		public static EdmTerm AddTerm(this EdmModel model, string namespaceName, string name, IEdmTypeReference type, string appliesTo, string defaultValue)
		{
			EdmTerm edmTerm = new EdmTerm(namespaceName, name, type, appliesTo, defaultValue);
			model.AddElement(edmTerm);
			return edmTerm;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00007C18 File Offset: 0x00005E18
		public static void SetOptimisticConcurrencyAnnotation(this EdmModel model, IEdmEntitySet target, IEnumerable<IEdmStructuralProperty> properties)
		{
			EdmUtil.CheckArgumentNull<EdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmEntitySet>(target, "target");
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmStructuralProperty>>(properties, "properties");
			IEdmCollectionExpression edmCollectionExpression = new EdmCollectionExpression(properties.Select((IEdmStructuralProperty p) => new EdmPropertyPathExpression(p.Name)).ToArray<EdmPropertyPathExpression>());
			IEdmTerm concurrencyTerm = CoreVocabularyModel.ConcurrencyTerm;
			EdmVocabularyAnnotation edmVocabularyAnnotation = new EdmVocabularyAnnotation(target, concurrencyTerm, edmCollectionExpression);
			edmVocabularyAnnotation.SetSerializationLocation(model, new EdmVocabularyAnnotationSerializationLocation?(EdmVocabularyAnnotationSerializationLocation.Inline));
			model.SetVocabularyAnnotation(edmVocabularyAnnotation);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00007C9C File Offset: 0x00005E9C
		public static void SetDescriptionAnnotation(this EdmModel model, IEdmVocabularyAnnotatable target, string description)
		{
			EdmUtil.CheckArgumentNull<EdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(target, "target");
			EdmUtil.CheckArgumentNull<string>(description, "description");
			EdmVocabularyAnnotation edmVocabularyAnnotation = new EdmVocabularyAnnotation(target, CoreVocabularyModel.DescriptionTerm, new EdmStringConstant(description));
			edmVocabularyAnnotation.SetSerializationLocation(model, new EdmVocabularyAnnotationSerializationLocation?(EdmVocabularyAnnotationSerializationLocation.Inline));
			model.SetVocabularyAnnotation(edmVocabularyAnnotation);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00007CF4 File Offset: 0x00005EF4
		public static void SetLongDescriptionAnnotation(this EdmModel model, IEdmVocabularyAnnotatable target, string description)
		{
			EdmUtil.CheckArgumentNull<EdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(target, "target");
			EdmUtil.CheckArgumentNull<string>(description, "description");
			EdmVocabularyAnnotation edmVocabularyAnnotation = new EdmVocabularyAnnotation(target, CoreVocabularyModel.LongDescriptionTerm, new EdmStringConstant(description));
			edmVocabularyAnnotation.SetSerializationLocation(model, new EdmVocabularyAnnotationSerializationLocation?(EdmVocabularyAnnotationSerializationLocation.Inline));
			model.SetVocabularyAnnotation(edmVocabularyAnnotation);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00007D4B File Offset: 0x00005F4B
		public static void SetChangeTrackingAnnotation(this EdmModel model, IEdmEntityContainer target, bool isSupported)
		{
			model.SetChangeTrackingAnnotationImplementation(target, isSupported, null, null);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00007D57 File Offset: 0x00005F57
		public static void SetChangeTrackingAnnotation(this EdmModel model, IEdmEntitySet target, bool isSupported, IEnumerable<IEdmStructuralProperty> filterableProperties, IEnumerable<IEdmNavigationProperty> expandableProperties)
		{
			model.SetChangeTrackingAnnotationImplementation(target, isSupported, filterableProperties, expandableProperties);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00007D64 File Offset: 0x00005F64
		public static IEdmTypeDefinitionReference GetUInt16(this EdmModel model, string namespaceName, bool isNullable)
		{
			return model.GetUIntImplementation(namespaceName, "UInt16", "Edm.Int32", isNullable);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00007D78 File Offset: 0x00005F78
		public static IEdmTypeDefinitionReference GetUInt32(this EdmModel model, string namespaceName, bool isNullable)
		{
			return model.GetUIntImplementation(namespaceName, "UInt32", "Edm.Int64", isNullable);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00007D8C File Offset: 0x00005F8C
		public static IEdmTypeDefinitionReference GetUInt64(this EdmModel model, string namespaceName, bool isNullable)
		{
			return model.GetUIntImplementation(namespaceName, "UInt64", "Edm.Decimal", isNullable);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00007DA0 File Offset: 0x00005FA0
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

		// Token: 0x06000324 RID: 804 RVA: 0x00007DD8 File Offset: 0x00005FD8
		public static IEnumerable<IEdmVocabularyAnnotation> VocabularyAnnotations(this IEdmVocabularyAnnotatable element, IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.FindVocabularyAnnotations(element);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00007DFC File Offset: 0x00005FFC
		public static string FullName(this IEdmSchemaElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmSchemaElement>(element, "element");
			if (element.Name == null)
			{
				return string.Empty;
			}
			if (element.Namespace == null)
			{
				return element.Name;
			}
			IEdmFullNamedElement edmFullNamedElement = element as IEdmFullNamedElement;
			if (edmFullNamedElement != null)
			{
				return edmFullNamedElement.FullName;
			}
			return element.Namespace + "." + element.Name;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00007E59 File Offset: 0x00006059
		public static string ShortQualifiedName(this IEdmSchemaElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmSchemaElement>(element, "element");
			if (element.Namespace != null && element.Namespace.Equals("Edm"))
			{
				return element.Name ?? string.Empty;
			}
			return element.FullName();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00007E97 File Offset: 0x00006097
		public static IEnumerable<IEdmEntitySet> EntitySets(this IEdmEntityContainer container)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			return container.AllElements(100).OfType<IEdmEntitySet>();
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00007EB2 File Offset: 0x000060B2
		public static IEnumerable<IEdmSingleton> Singletons(this IEdmEntityContainer container)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			return container.AllElements(100).OfType<IEdmSingleton>();
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00007ECD File Offset: 0x000060CD
		public static IEnumerable<IEdmOperationImport> OperationImports(this IEdmEntityContainer container)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			return container.AllElements(100).OfType<IEdmOperationImport>();
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00007EE8 File Offset: 0x000060E8
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

		// Token: 0x0600032B RID: 811 RVA: 0x00007F13 File Offset: 0x00006113
		public static string FullName(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.Definition.FullTypeName();
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00007F2C File Offset: 0x0000612C
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

		// Token: 0x0600032D RID: 813 RVA: 0x00007F5C File Offset: 0x0000615C
		public static string FullTypeName(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			EdmCoreModelPrimitiveType edmCoreModelPrimitiveType = type as EdmCoreModelPrimitiveType;
			if (edmCoreModelPrimitiveType != null)
			{
				return edmCoreModelPrimitiveType.FullName;
			}
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

		// Token: 0x0600032E RID: 814 RVA: 0x00007FD8 File Offset: 0x000061D8
		public static IEdmType AsElementType(this IEdmType type)
		{
			IEdmCollectionType edmCollectionType = type as IEdmCollectionType;
			if (edmCollectionType == null)
			{
				return type;
			}
			return edmCollectionType.ElementType.Definition;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00007FFC File Offset: 0x000061FC
		public static IEdmPrimitiveType PrimitiveDefinition(this IEdmPrimitiveTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmPrimitiveTypeReference>(type, "type");
			return (IEdmPrimitiveType)type.Definition;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00008018 File Offset: 0x00006218
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

		// Token: 0x06000331 RID: 817 RVA: 0x00008043 File Offset: 0x00006243
		public static IEnumerable<IEdmProperty> Properties(this IEdmStructuredType type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(type, "type");
			if (type.BaseType != null)
			{
				foreach (IEdmProperty edmProperty in type.BaseType.Properties())
				{
					yield return edmProperty;
				}
				IEnumerator<IEdmProperty> enumerator = null;
			}
			if (type.DeclaredProperties != null)
			{
				foreach (IEdmProperty edmProperty2 in type.DeclaredProperties)
				{
					yield return edmProperty2;
				}
				IEnumerator<IEdmProperty> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00008053 File Offset: 0x00006253
		public static IEnumerable<IEdmStructuralProperty> DeclaredStructuralProperties(this IEdmStructuredType type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(type, "type");
			return type.DeclaredProperties.OfType<IEdmStructuralProperty>();
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000806C File Offset: 0x0000626C
		public static IEnumerable<IEdmStructuralProperty> StructuralProperties(this IEdmStructuredType type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(type, "type");
			return type.Properties().OfType<IEdmStructuralProperty>();
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00008085 File Offset: 0x00006285
		public static IEdmStructuredType StructuredDefinition(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return (IEdmStructuredType)type.Definition;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000809E File Offset: 0x0000629E
		public static bool IsAbstract(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().IsAbstract;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000080B7 File Offset: 0x000062B7
		public static bool IsOpen(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().IsOpen;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000080D0 File Offset: 0x000062D0
		public static bool IsOpen(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			IEdmStructuredType edmStructuredType = type as IEdmStructuredType;
			if (edmStructuredType != null)
			{
				return edmStructuredType.IsOpen;
			}
			IEdmCollectionType edmCollectionType = type as IEdmCollectionType;
			return edmCollectionType != null && edmCollectionType.ElementType.Definition.IsOpen();
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00008116 File Offset: 0x00006316
		public static IEdmStructuredType BaseType(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().BaseType;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000812F File Offset: 0x0000632F
		public static IEnumerable<IEdmStructuralProperty> DeclaredStructuralProperties(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().DeclaredStructuralProperties();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00008148 File Offset: 0x00006348
		public static IEnumerable<IEdmStructuralProperty> StructuralProperties(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().StructuralProperties();
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00008161 File Offset: 0x00006361
		public static IEdmProperty FindProperty(this IEdmStructuredTypeReference type, string name)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			return type.StructuredDefinition().FindProperty(name);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00008187 File Offset: 0x00006387
		public static IEnumerable<IEdmNavigationProperty> NavigationProperties(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().NavigationProperties();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000081A0 File Offset: 0x000063A0
		public static IEnumerable<IEdmNavigationProperty> DeclaredNavigationProperties(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().DeclaredNavigationProperties();
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000081B9 File Offset: 0x000063B9
		public static IEdmNavigationProperty FindNavigationProperty(this IEdmStructuredTypeReference type, string name)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			return type.StructuredDefinition().FindProperty(name) as IEdmNavigationProperty;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000081E4 File Offset: 0x000063E4
		public static IEdmEntityType BaseEntityType(this IEdmEntityType type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(type, "type");
			return type.BaseType as IEdmEntityType;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000081FD File Offset: 0x000063FD
		public static IEdmStructuredType BaseType(this IEdmStructuredType type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(type, "type");
			return type.BaseType;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00008211 File Offset: 0x00006411
		public static IEnumerable<IEdmNavigationProperty> DeclaredNavigationProperties(this IEdmStructuredType type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(type, "type");
			return type.DeclaredProperties.OfType<IEdmNavigationProperty>();
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000822A File Offset: 0x0000642A
		public static IEnumerable<IEdmNavigationProperty> NavigationProperties(this IEdmStructuredType type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(type, "type");
			return type.Properties().OfType<IEdmNavigationProperty>();
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00008244 File Offset: 0x00006444
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

		// Token: 0x06000344 RID: 836 RVA: 0x00008280 File Offset: 0x00006480
		public static bool IsKey(this IEdmProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmProperty>(property, "property");
			IEdmEntityType edmEntityType = property.DeclaringType as IEdmEntityType;
			if (edmEntityType != null)
			{
				foreach (IEdmProperty edmProperty in edmEntityType.Key())
				{
					if (edmProperty == property)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000082EC File Offset: 0x000064EC
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

		// Token: 0x06000346 RID: 838 RVA: 0x00008334 File Offset: 0x00006534
		public static void AddAlternateKeyAnnotation(this EdmModel model, IEdmEntityType type, IDictionary<string, IEdmProperty> alternateKey)
		{
			EdmUtil.CheckArgumentNull<EdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmEntityType>(type, "type");
			EdmUtil.CheckArgumentNull<IDictionary<string, IEdmProperty>>(alternateKey, "alternateKey");
			EdmCollectionExpression edmCollectionExpression = null;
			IEdmVocabularyAnnotation edmVocabularyAnnotation = model.FindVocabularyAnnotations(type, AlternateKeysVocabularyModel.AlternateKeysTerm).FirstOrDefault<IEdmVocabularyAnnotation>();
			if (edmVocabularyAnnotation != null)
			{
				edmCollectionExpression = edmVocabularyAnnotation.Value as EdmCollectionExpression;
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
			EdmVocabularyAnnotation edmVocabularyAnnotation2 = new EdmVocabularyAnnotation(type, AlternateKeysVocabularyModel.AlternateKeysTerm, new EdmCollectionExpression(list));
			edmVocabularyAnnotation2.SetSerializationLocation(model, new EdmVocabularyAnnotationSerializationLocation?(EdmVocabularyAnnotationSerializationLocation.Inline));
			model.SetVocabularyAnnotation(edmVocabularyAnnotation2);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00008494 File Offset: 0x00006694
		public static bool HasDeclaredKeyProperty(this IEdmEntityType entityType, IEdmProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(entityType, "entityType");
			EdmUtil.CheckArgumentNull<IEdmProperty>(property, "property");
			while (entityType != null)
			{
				if (entityType.DeclaredKey != null && entityType.DeclaredKey.Any((IEdmStructuralProperty k) => k == property))
				{
					return true;
				}
				entityType = entityType.BaseEntityType();
			}
			return false;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000084FC File Offset: 0x000066FC
		public static IEdmEntityType EntityDefinition(this IEdmEntityTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			return (IEdmEntityType)type.Definition;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00008515 File Offset: 0x00006715
		public static IEdmEntityType BaseEntityType(this IEdmEntityTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			return type.EntityDefinition().BaseEntityType();
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000852E File Offset: 0x0000672E
		public static IEnumerable<IEdmStructuralProperty> Key(this IEdmEntityTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			return type.EntityDefinition().Key();
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00008547 File Offset: 0x00006747
		public static IEdmComplexType BaseComplexType(this IEdmComplexType type)
		{
			EdmUtil.CheckArgumentNull<IEdmComplexType>(type, "type");
			return type.BaseType as IEdmComplexType;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00008560 File Offset: 0x00006760
		public static IEdmComplexType ComplexDefinition(this IEdmComplexTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmComplexTypeReference>(type, "type");
			return (IEdmComplexType)type.Definition;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00008579 File Offset: 0x00006779
		public static IEdmComplexType BaseComplexType(this IEdmComplexTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmComplexTypeReference>(type, "type");
			return type.ComplexDefinition().BaseComplexType();
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00008592 File Offset: 0x00006792
		public static IEdmEntityReferenceType EntityReferenceDefinition(this IEdmEntityReferenceTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityReferenceTypeReference>(type, "type");
			return (IEdmEntityReferenceType)type.Definition;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000085AB File Offset: 0x000067AB
		public static IEdmEntityType EntityType(this IEdmEntityReferenceTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityReferenceTypeReference>(type, "type");
			return type.EntityReferenceDefinition().EntityType;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000085C4 File Offset: 0x000067C4
		public static IEdmCollectionType CollectionDefinition(this IEdmCollectionTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmCollectionTypeReference>(type, "type");
			return (IEdmCollectionType)type.Definition;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000085DD File Offset: 0x000067DD
		public static IEdmTypeReference ElementType(this IEdmCollectionTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmCollectionTypeReference>(type, "type");
			return type.CollectionDefinition().ElementType;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000085F6 File Offset: 0x000067F6
		public static IEdmEnumType EnumDefinition(this IEdmEnumTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEnumTypeReference>(type, "type");
			return (IEdmEnumType)type.Definition;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000860F File Offset: 0x0000680F
		public static IEdmTypeDefinition TypeDefinition(this IEdmTypeDefinitionReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeDefinitionReference>(type, "type");
			return (IEdmTypeDefinition)type.Definition;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00008628 File Offset: 0x00006828
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

		// Token: 0x06000355 RID: 853 RVA: 0x0000865D File Offset: 0x0000685D
		public static IEdmEntityType ToEntityType(this IEdmNavigationProperty property)
		{
			return property.Type.Definition.AsElementType() as IEdmEntityType;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00008674 File Offset: 0x00006874
		public static IEdmStructuredType ToStructuredType(this IEdmTypeReference propertyTypeReference)
		{
			IEdmType edmType = propertyTypeReference.Definition;
			if (edmType.TypeKind == EdmTypeKind.Collection)
			{
				edmType = ((IEdmCollectionType)edmType).ElementType.Definition;
			}
			return edmType as IEdmStructuredType;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000086A8 File Offset: 0x000068A8
		public static IEdmEntityType DeclaringEntityType(this IEdmNavigationProperty property)
		{
			return (IEdmEntityType)property.DeclaringType;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000086B5 File Offset: 0x000068B5
		public static bool IsPrincipal(this IEdmNavigationProperty navigationProperty)
		{
			return navigationProperty.ReferentialConstraint == null && navigationProperty.Partner != null && navigationProperty.Partner.ReferentialConstraint != null;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000086D7 File Offset: 0x000068D7
		public static IEnumerable<IEdmStructuralProperty> DependentProperties(this IEdmNavigationProperty navigationProperty)
		{
			if (navigationProperty.ReferentialConstraint != null)
			{
				return navigationProperty.ReferentialConstraint.PropertyPairs.Select((EdmReferentialConstraintPropertyPair p) => p.DependentProperty);
			}
			return null;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00008712 File Offset: 0x00006912
		public static IEnumerable<IEdmStructuralProperty> PrincipalProperties(this IEdmNavigationProperty navigationProperty)
		{
			if (navigationProperty.ReferentialConstraint != null)
			{
				return navigationProperty.ReferentialConstraint.PropertyPairs.Select((EdmReferentialConstraintPropertyPair p) => p.PrincipalProperty);
			}
			return null;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000874D File Offset: 0x0000694D
		public static IEdmTerm Term(this IEdmVocabularyAnnotation annotation)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			return annotation.Term;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00008764 File Offset: 0x00006964
		public static bool TryGetRelativeEntitySetPath(this IEdmOperation operation, IEdmModel model, out IEdmOperationParameter parameter, out Dictionary<IEdmNavigationProperty, IEdmPathExpression> relativeNavigations, out IEdmEntityType lastEntityType, out IEnumerable<EdmError> errors)
		{
			errors = Enumerable.Empty<EdmError>();
			parameter = null;
			relativeNavigations = null;
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
			return ExtensionMethods.TryGetRelativeEntitySetPath(operation, collection, operation.EntitySetPath, model, operation.Parameters, out parameter, out relativeNavigations, out lastEntityType);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x000087D8 File Offset: 0x000069D8
		public static bool IsActionImport(this IEdmOperationImport operationImport)
		{
			return operationImport.ContainerElementKind == EdmContainerElementKind.ActionImport;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000087E3 File Offset: 0x000069E3
		public static bool IsFunctionImport(this IEdmOperationImport operationImport)
		{
			return operationImport.ContainerElementKind == EdmContainerElementKind.FunctionImport;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000087F0 File Offset: 0x000069F0
		public static bool TryGetStaticEntitySet(this IEdmOperationImport operationImport, IEdmModel model, out IEdmEntitySetBase entitySet)
		{
			IEdmPathExpression edmPathExpression = operationImport.EntitySet as IEdmPathExpression;
			if (edmPathExpression != null)
			{
				return edmPathExpression.TryGetStaticEntitySet(model, out entitySet);
			}
			entitySet = null;
			return false;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000881C File Offset: 0x00006A1C
		public static bool TryGetRelativeEntitySetPath(this IEdmOperationImport operationImport, IEdmModel model, out IEdmOperationParameter parameter, out Dictionary<IEdmNavigationProperty, IEdmPathExpression> relativeNavigations, out IEnumerable<EdmError> edmErrors)
		{
			EdmUtil.CheckArgumentNull<IEdmOperationImport>(operationImport, "operationImport");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			parameter = null;
			relativeNavigations = null;
			edmErrors = new ReadOnlyCollection<EdmError>(new List<EdmError>());
			IEdmPathExpression edmPathExpression = operationImport.EntitySet as IEdmPathExpression;
			if (edmPathExpression != null)
			{
				IEdmEntityType edmEntityType = null;
				Collection<EdmError> collection = new Collection<EdmError>();
				bool flag = ExtensionMethods.TryGetRelativeEntitySetPath(operationImport, collection, edmPathExpression, model, operationImport.Operation.Parameters, out parameter, out relativeNavigations, out edmEntityType);
				edmErrors = new ReadOnlyCollection<EdmError>(collection);
				return flag;
			}
			return false;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00008890 File Offset: 0x00006A90
		public static bool IsAction(this IEdmOperation operation)
		{
			return operation.SchemaElementKind == EdmSchemaElementKind.Action;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000889B File Offset: 0x00006A9B
		public static bool IsFunction(this IEdmOperation operation)
		{
			return operation.SchemaElementKind == EdmSchemaElementKind.Function;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000088A8 File Offset: 0x00006AA8
		public static IEdmOperationReturn GetReturn(this IEdmOperation operation)
		{
			EdmOperation edmOperation = operation as EdmOperation;
			if (edmOperation != null)
			{
				return edmOperation.Return;
			}
			CsdlSemanticsOperation csdlSemanticsOperation = operation as CsdlSemanticsOperation;
			if (csdlSemanticsOperation != null)
			{
				return csdlSemanticsOperation.Return;
			}
			if (operation == null || operation.ReturnType == null)
			{
				return null;
			}
			return new EdmOperationReturn(operation, operation.ReturnType);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000088F0 File Offset: 0x00006AF0
		public static IEnumerable<IEdmOperation> FilterByName(this IEnumerable<IEdmOperation> operations, bool forceFullyQualifiedNameFilter, string operationName)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmOperation>>(operations, "operations");
			EdmUtil.CheckArgumentNull<string>(operationName, "operationName");
			if (forceFullyQualifiedNameFilter || operationName.IndexOf(".", StringComparison.Ordinal) > -1)
			{
				return operations.Where((IEdmOperation o) => o.FullName() == operationName);
			}
			return operations.Where((IEdmOperation o) => o.Name == operationName);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00008964 File Offset: 0x00006B64
		public static bool HasEquivalentBindingType(this IEdmOperation operation, IEdmType bindingType)
		{
			EdmUtil.CheckArgumentNull<IEdmOperation>(operation, "operation");
			EdmUtil.CheckArgumentNull<IEdmType>(bindingType, "bindingType");
			if (!operation.IsBound)
			{
				return false;
			}
			IEdmOperationParameter edmOperationParameter = operation.Parameters.FirstOrDefault<IEdmOperationParameter>();
			if (edmOperationParameter == null)
			{
				return false;
			}
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

		// Token: 0x06000366 RID: 870 RVA: 0x000089FC File Offset: 0x00006BFC
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

		// Token: 0x06000367 RID: 871 RVA: 0x00008A58 File Offset: 0x00006C58
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

		// Token: 0x06000368 RID: 872 RVA: 0x00008A83 File Offset: 0x00006C83
		public static string FullNavigationSourceName(this IEdmNavigationSource navigationSource)
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationSource>(navigationSource, "navigationSource");
			return string.Join(".", navigationSource.Path.PathSegments.ToArray<string>());
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00008AAC File Offset: 0x00006CAC
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

		// Token: 0x0600036A RID: 874 RVA: 0x00008B12 File Offset: 0x00006D12
		public static void SetEdmReferences(this IEdmModel model, IEnumerable<IEdmReference> edmReferences)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "References", edmReferences);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00008B32 File Offset: 0x00006D32
		public static IEnumerable<IEdmReference> GetEdmReferences(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return (IEnumerable<IEdmReference>)model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "References");
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00008B58 File Offset: 0x00006D58
		public static IEdmPathExpression GetPartnerPath(this IEdmNavigationProperty navigationProperty)
		{
			EdmNavigationProperty edmNavigationProperty = navigationProperty as EdmNavigationProperty;
			if (edmNavigationProperty != null)
			{
				return edmNavigationProperty.PartnerPath;
			}
			CsdlSemanticsNavigationProperty csdlSemanticsNavigationProperty = navigationProperty as CsdlSemanticsNavigationProperty;
			if (csdlSemanticsNavigationProperty != null)
			{
				return ((CsdlNavigationProperty)csdlSemanticsNavigationProperty.Element).PartnerPath;
			}
			if (navigationProperty.Partner != null)
			{
				return new EdmPathExpression(navigationProperty.Partner.Name);
			}
			return null;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00008BAC File Offset: 0x00006DAC
		internal static string ReplaceAlias(this IEdmModel model, string name)
		{
			ExtensionMethods.<>c__DisplayClass158_0 CS$<>8__locals1 = new ExtensionMethods.<>c__DisplayClass158_0();
			CS$<>8__locals1.mappings = model.GetNamespaceAliases();
			VersioningList<string> usedNamespacesHavingAlias = model.GetUsedNamespacesHavingAlias();
			int num = name.IndexOf('.');
			if (usedNamespacesHavingAlias == null || CS$<>8__locals1.mappings == null || num <= 0)
			{
				return name;
			}
			string typeAlias = name.Substring(0, num);
			string text = usedNamespacesHavingAlias.FirstOrDefault(delegate(string n)
			{
				string text2;
				return CS$<>8__locals1.mappings.TryGetValue(n, out text2) && text2 == typeAlias;
			});
			if (text == null)
			{
				return name;
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}", new object[]
			{
				text,
				name.Substring(num)
			});
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00008C45 File Offset: 0x00006E45
		internal static IEnumerable<IEdmOperation> FindOperationsInModelTree(this CsdlSemanticsModel model, string name)
		{
			return model.FindInModelTree(ExtensionMethods.findOperations, name, ExtensionMethods.mergeFunctions);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00008C58 File Offset: 0x00006E58
		internal static IEdmSchemaType FindTypeInModelTree(this CsdlSemanticsModel model, string name)
		{
			return model.FindInModelTree(ExtensionMethods.findType, name, new Func<IEdmSchemaType, IEdmSchemaType, IEdmSchemaType>(RegistrationHelper.CreateAmbiguousTypeBinding));
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00008C74 File Offset: 0x00006E74
		internal static T FindInModelTree<T>(this CsdlSemanticsModel model, Func<IEdmModel, string, T> finderFunc, string qualifiedName, Func<T, T, T> ambiguousCreator)
		{
			EdmUtil.CheckArgumentNull<CsdlSemanticsModel>(model, "model");
			EdmUtil.CheckArgumentNull<Func<IEdmModel, string, T>>(finderFunc, "finderFunc");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			EdmUtil.CheckArgumentNull<Func<T, T, T>>(ambiguousCreator, "ambiguousCreator");
			T t = finderFunc(model, qualifiedName);
			if (model.MainModel != null)
			{
				T t2;
				if ((t2 = finderFunc(model.MainModel, qualifiedName)) != null)
				{
					t = ((t == null) ? t2 : ambiguousCreator(t, t2));
				}
				foreach (IEdmModel edmModel in model.MainModel.ReferencedModels)
				{
					if (edmModel != EdmCoreModel.Instance && edmModel != CoreVocabularyModel.Instance && edmModel != model && (t2 = finderFunc(edmModel, qualifiedName)) != null)
					{
						t = ((t == null) ? t2 : ambiguousCreator(t, t2));
					}
				}
			}
			foreach (IEdmModel edmModel2 in model.ReferencedModels)
			{
				T t2 = finderFunc(edmModel2, qualifiedName);
				if (t2 != null)
				{
					t = ((t == null) ? t2 : ambiguousCreator(t, t2));
				}
			}
			return t;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00008DC8 File Offset: 0x00006FC8
		internal static bool IsUrlEscapeFunction(this IEdmModel model, IEdmFunction function)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmFunction>(function, "function");
			IEdmVocabularyAnnotation edmVocabularyAnnotation = model.FindVocabularyAnnotations(function, CommunityVocabularyModel.UrlEscapeFunctionTerm).FirstOrDefault<IEdmVocabularyAnnotation>();
			if (edmVocabularyAnnotation != null)
			{
				if (edmVocabularyAnnotation.Value == null)
				{
					return true;
				}
				IEdmBooleanConstantExpression edmBooleanConstantExpression = edmVocabularyAnnotation.Value as IEdmBooleanConstantExpression;
				if (edmBooleanConstantExpression != null)
				{
					return edmBooleanConstantExpression.Value;
				}
			}
			return false;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00008E24 File Offset: 0x00007024
		internal static void SetUrlEscapeFunction(this EdmModel model, IEdmFunction function)
		{
			EdmUtil.CheckArgumentNull<EdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmFunction>(function, "function");
			IEdmBooleanConstantExpression edmBooleanConstantExpression = new EdmBooleanConstant(true);
			IEdmTerm urlEscapeFunctionTerm = CommunityVocabularyModel.UrlEscapeFunctionTerm;
			EdmVocabularyAnnotation edmVocabularyAnnotation = new EdmVocabularyAnnotation(function, urlEscapeFunctionTerm, edmBooleanConstantExpression);
			edmVocabularyAnnotation.SetSerializationLocation(model, new EdmVocabularyAnnotationSerializationLocation?(EdmVocabularyAnnotationSerializationLocation.Inline));
			model.SetVocabularyAnnotation(edmVocabularyAnnotation);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00008E74 File Offset: 0x00007074
		internal static bool TryGetRelativeEntitySetPath(IEdmElement element, Collection<EdmError> foundErrors, IEdmPathExpression pathExpression, IEdmModel model, IEnumerable<IEdmOperationParameter> parameters, out IEdmOperationParameter parameter, out Dictionary<IEdmNavigationProperty, IEdmPathExpression> relativeNavigations, out IEdmEntityType lastEntityType)
		{
			parameter = null;
			relativeNavigations = null;
			lastEntityType = null;
			List<string> list = pathExpression.PathSegments.ToList<string>();
			if (list.Count < 1)
			{
				foundErrors.Add(new EdmError(element.Location(), EdmErrorCode.OperationWithInvalidEntitySetPathMissingCompletePath, Strings.EdmModel_Validator_Semantic_InvalidEntitySetPathMissingBindingParameterName("EntitySetPath")));
				return false;
			}
			parameter = parameters.FirstOrDefault<IEdmOperationParameter>();
			if (parameter == null)
			{
				return false;
			}
			bool flag = true;
			string text = list.First<string>();
			if (parameter.Name != text)
			{
				foundErrors.Add(new EdmError(element.Location(), EdmErrorCode.InvalidPathFirstPathParameterNotMatchingFirstParameterName, Strings.EdmModel_Validator_Semantic_InvalidEntitySetPathWithFirstPathParameterNotMatchingFirstParameterName("EntitySetPath", EdmModelCsdlSchemaWriter.PathAsXml(pathExpression.PathSegments), text, parameter.Name)));
				flag = false;
			}
			lastEntityType = parameter.Type.Definition as IEdmEntityType;
			if (lastEntityType == null)
			{
				IEdmCollectionTypeReference edmCollectionTypeReference = parameter.Type as IEdmCollectionTypeReference;
				if (edmCollectionTypeReference == null || !edmCollectionTypeReference.ElementType().IsEntity())
				{
					foundErrors.Add(new EdmError(element.Location(), EdmErrorCode.InvalidPathWithNonEntityBindingParameter, Strings.EdmModel_Validator_Semantic_InvalidEntitySetPathWithNonEntityBindingParameter("EntitySetPath", EdmModelCsdlSchemaWriter.PathAsXml(pathExpression.PathSegments), text)));
					return false;
				}
				lastEntityType = edmCollectionTypeReference.ElementType().Definition as IEdmEntityType;
			}
			Dictionary<IEdmNavigationProperty, IEdmPathExpression> dictionary = new Dictionary<IEdmNavigationProperty, IEdmPathExpression>();
			List<string> list2 = new List<string>();
			foreach (string text2 in list.Skip(1))
			{
				list2.Add(text2);
				if (EdmUtil.IsQualifiedName(text2))
				{
					IEdmSchemaType edmSchemaType = model.FindDeclaredType(text2);
					if (edmSchemaType == null)
					{
						foundErrors.Add(new EdmError(element.Location(), EdmErrorCode.InvalidPathUnknownTypeCastSegment, Strings.EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownTypeCastSegment("EntitySetPath", EdmModelCsdlSchemaWriter.PathAsXml(pathExpression.PathSegments), text2)));
						flag = false;
						break;
					}
					IEdmEntityType edmEntityType = edmSchemaType as IEdmEntityType;
					if (edmEntityType == null)
					{
						foundErrors.Add(new EdmError(element.Location(), EdmErrorCode.InvalidPathTypeCastSegmentMustBeEntityType, Strings.EdmModel_Validator_Semantic_InvalidEntitySetPathTypeCastSegmentMustBeEntityType("EntitySetPath", EdmModelCsdlSchemaWriter.PathAsXml(pathExpression.PathSegments), edmSchemaType.FullName())));
						flag = false;
						break;
					}
					if (!edmEntityType.IsOrInheritsFrom(lastEntityType))
					{
						foundErrors.Add(new EdmError(element.Location(), EdmErrorCode.InvalidPathInvalidTypeCastSegment, Strings.EdmModel_Validator_Semantic_InvalidEntitySetPathInvalidTypeCastSegment("EntitySetPath", EdmModelCsdlSchemaWriter.PathAsXml(pathExpression.PathSegments), lastEntityType.FullName(), edmEntityType.FullName())));
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
						foundErrors.Add(new EdmError(element.Location(), EdmErrorCode.InvalidPathUnknownNavigationProperty, Strings.EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownNavigationProperty("EntitySetPath", EdmModelCsdlSchemaWriter.PathAsXml(pathExpression.PathSegments), text2)));
						flag = false;
						break;
					}
					dictionary[edmNavigationProperty] = new EdmPathExpression(list2);
					if (!edmNavigationProperty.ContainsTarget)
					{
						list2.Clear();
					}
					lastEntityType = edmNavigationProperty.ToEntityType();
				}
			}
			relativeNavigations = dictionary;
			return flag;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00009168 File Offset: 0x00007368
		internal static IEdmEntityType GetPathSegmentEntityType(IEdmTypeReference segmentType)
		{
			return (segmentType.IsCollection() ? segmentType.AsCollection().ElementType() : segmentType).AsEntity().EntityDefinition();
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000918C File Offset: 0x0000738C
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
			return container.Elements.Concat(csdlSemanticsEntityContainer.Extends.AllElements(depth - 1));
		}

		// Token: 0x06000376 RID: 886 RVA: 0x000091E0 File Offset: 0x000073E0
		internal static IEdmEntitySet FindEntitySetExtended(this IEdmEntityContainer container, string qualifiedName)
		{
			return ExtensionMethods.FindInContainerAndExtendsRecursively<IEdmEntitySet>(container, qualifiedName, (IEdmEntityContainer c, string n) => c.FindEntitySet(n), 100);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000920A File Offset: 0x0000740A
		internal static IEdmNavigationSource FindNavigationSourceExtended(this IEdmEntityContainer container, string path)
		{
			return ExtensionMethods.FindInContainerAndExtendsRecursively<IEdmNavigationSource>(container, path, (IEdmEntityContainer c, string n) => c.FindNavigationSource(n), 100);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00009234 File Offset: 0x00007434
		internal static IEdmNavigationSource FindNavigationSource(this IEdmEntityContainer container, string path)
		{
			string[] array = path.Split(new char[] { '.' }).Last<string>().Split(new char[] { '/' });
			IEdmNavigationSource edmNavigationSource = container.FindEntitySet(array[0]);
			if (edmNavigationSource == null)
			{
				edmNavigationSource = container.FindSingleton(array[0]);
			}
			List<string> list = new List<string>();
			int num = 1;
			while (num < array.Length && edmNavigationSource != null)
			{
				list.Add(array[num]);
				IEdmNavigationProperty edmNavigationProperty = edmNavigationSource.EntityType().FindProperty(array[num]) as IEdmNavigationProperty;
				if (edmNavigationProperty != null)
				{
					edmNavigationSource = edmNavigationSource.FindNavigationTarget(edmNavigationProperty, new EdmPathExpression(list));
					list.Clear();
				}
				num++;
			}
			return edmNavigationSource;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x000092CE File Offset: 0x000074CE
		internal static IEdmSingleton FindSingletonExtended(this IEdmEntityContainer container, string qualifiedName)
		{
			return ExtensionMethods.FindInContainerAndExtendsRecursively<IEdmSingleton>(container, qualifiedName, (IEdmEntityContainer c, string n) => c.FindSingleton(n), 100);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000092F8 File Offset: 0x000074F8
		internal static IEnumerable<IEdmOperationImport> FindOperationImportsExtended(this IEdmEntityContainer container, string qualifiedName)
		{
			return ExtensionMethods.FindInContainerAndExtendsRecursively<IEnumerable<IEdmOperationImport>>(container, qualifiedName, (IEdmEntityContainer c, string n) => c.FindOperationImports(n), 100);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00009324 File Offset: 0x00007524
		internal static IPrimitiveValueConverter GetPrimitiveValueConverter(this IEdmModel model, IEdmType typeDefinition)
		{
			IPrimitiveValueConverter annotationValue = model.GetAnnotationValue(typeDefinition, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "PrimitiveValueConverterMap");
			if (annotationValue == null)
			{
				return PassThroughPrimitiveValueConverter.Instance;
			}
			return annotationValue;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000934D File Offset: 0x0000754D
		internal static void SetPrimitiveValueConverter(this IEdmModel model, IEdmType typeDefinition, IPrimitiveValueConverter converter)
		{
			model.SetAnnotationValue(typeDefinition, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "PrimitiveValueConverterMap", converter);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00009364 File Offset: 0x00007564
		internal static bool TryGetStaticEntitySet(this IEdmPathExpression pathExpression, IEdmModel model, out IEdmEntitySetBase entitySet)
		{
			IEnumerator<string> enumerator = pathExpression.PathSegments.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				entitySet = null;
				return false;
			}
			string text = enumerator.Current;
			IEdmEntityContainer edmEntityContainer;
			if (text.Contains("."))
			{
				edmEntityContainer = model.FindEntityContainer(text);
				if (!enumerator.MoveNext())
				{
					entitySet = null;
					return false;
				}
				text = enumerator.Current;
			}
			else
			{
				edmEntityContainer = model.EntityContainer;
			}
			if (edmEntityContainer == null)
			{
				entitySet = null;
				return false;
			}
			IEdmEntitySet edmEntitySet = edmEntityContainer.FindEntitySet(text);
			entitySet = (enumerator.MoveNext() ? null : edmEntitySet);
			return entitySet != null;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000093E8 File Offset: 0x000075E8
		internal static bool HasAny<T>(this IEnumerable<T> enumerable) where T : class
		{
			IList<T> list = enumerable as IList<T>;
			if (list != null)
			{
				return list.Count > 0;
			}
			T[] array = enumerable as T[];
			if (array != null)
			{
				return array.Length != 0;
			}
			return enumerable.FirstOrDefault<T>() != null;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00009428 File Offset: 0x00007628
		private static IEnumerable<IDictionary<string, IEdmProperty>> GetDeclaredAlternateKeysForType(IEdmEntityType type, IEdmModel model)
		{
			IEdmVocabularyAnnotation edmVocabularyAnnotation = model.FindVocabularyAnnotations(type, AlternateKeysVocabularyModel.AlternateKeysTerm).FirstOrDefault<IEdmVocabularyAnnotation>();
			if (edmVocabularyAnnotation != null)
			{
				List<IDictionary<string, IEdmProperty>> list = new List<IDictionary<string, IEdmProperty>>();
				IEdmCollectionExpression edmCollectionExpression = edmVocabularyAnnotation.Value as IEdmCollectionExpression;
				foreach (IEdmRecordExpression edmRecordExpression in edmCollectionExpression.Elements.OfType<IEdmRecordExpression>())
				{
					IEdmPropertyConstructor edmPropertyConstructor = edmRecordExpression.Properties.FirstOrDefault((IEdmPropertyConstructor e) => e.Name == "Key");
					if (edmPropertyConstructor != null)
					{
						IEdmCollectionExpression edmCollectionExpression2 = edmPropertyConstructor.Value as IEdmCollectionExpression;
						IDictionary<string, IEdmProperty> dictionary = new Dictionary<string, IEdmProperty>();
						foreach (IEdmRecordExpression edmRecordExpression2 in edmCollectionExpression2.Elements.OfType<IEdmRecordExpression>())
						{
							IEdmPropertyConstructor edmPropertyConstructor2 = edmRecordExpression2.Properties.FirstOrDefault((IEdmPropertyConstructor e) => e.Name == "Alias");
							string value = ((IEdmStringConstantExpression)edmPropertyConstructor2.Value).Value;
							IEdmPropertyConstructor edmPropertyConstructor3 = edmRecordExpression2.Properties.FirstOrDefault((IEdmPropertyConstructor e) => e.Name == "Name");
							string text = ((IEdmPathExpression)edmPropertyConstructor3.Value).PathSegments.FirstOrDefault<string>();
							dictionary[value] = type.FindProperty(text);
						}
						if (dictionary.Any<KeyValuePair<string, IEdmProperty>>())
						{
							list.Add(dictionary);
						}
					}
				}
				return list;
			}
			return null;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000095F8 File Offset: 0x000077F8
		private static T FindAcrossModels<T, TInput>(this IEdmModel model, TInput qualifiedName, Func<IEdmModel, TInput, T> finder, Func<T, T, T> ambiguousCreator)
		{
			T t = finder(model, qualifiedName);
			foreach (IEdmModel edmModel in model.ReferencedModels)
			{
				T t2 = finder(edmModel, qualifiedName);
				if (t2 != null)
				{
					t = ((t == null) ? t2 : ambiguousCreator(t, t2));
				}
			}
			return t;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00009670 File Offset: 0x00007870
		private static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmEntityType contextType, IEdmTerm term, string qualifier, Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T> evaluator)
		{
			IEnumerable<IEdmVocabularyAnnotation> enumerable = model.FindVocabularyAnnotations(contextType, term, qualifier);
			if (enumerable.Count<IEdmVocabularyAnnotation>() != 1)
			{
				throw new InvalidOperationException(Strings.Edm_Evaluator_NoValueAnnotationOnType(contextType.ToTraceString(), term.ToTraceString()));
			}
			return evaluator(enumerable.Single<IEdmVocabularyAnnotation>().Value, context, term.Type);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000096C4 File Offset: 0x000078C4
		private static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmEntityType contextType, string termName, string qualifier, Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T> evaluator)
		{
			IEnumerable<IEdmVocabularyAnnotation> enumerable = model.FindVocabularyAnnotations(contextType, termName, qualifier);
			if (enumerable.Count<IEdmVocabularyAnnotation>() != 1)
			{
				throw new InvalidOperationException(Strings.Edm_Evaluator_NoValueAnnotationOnType(contextType.ToTraceString(), termName));
			}
			IEdmVocabularyAnnotation edmVocabularyAnnotation = enumerable.Single<IEdmVocabularyAnnotation>();
			return evaluator(edmVocabularyAnnotation.Value, context, edmVocabularyAnnotation.Term().Type);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00009718 File Offset: 0x00007918
		private static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmTerm term, string qualifier, Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T> evaluator)
		{
			IEnumerable<IEdmVocabularyAnnotation> enumerable = model.FindVocabularyAnnotations(element, term, qualifier);
			if (enumerable.Count<IEdmVocabularyAnnotation>() != 1)
			{
				throw new InvalidOperationException(Strings.Edm_Evaluator_NoValueAnnotationOnElement(term.ToTraceString()));
			}
			return evaluator(enumerable.Single<IEdmVocabularyAnnotation>().Value, null, term.Type);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00009764 File Offset: 0x00007964
		private static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, string qualifier, Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T> evaluator)
		{
			IEnumerable<IEdmVocabularyAnnotation> enumerable = model.FindVocabularyAnnotations(element, termName, qualifier);
			if (enumerable.Count<IEdmVocabularyAnnotation>() != 1)
			{
				throw new InvalidOperationException(Strings.Edm_Evaluator_NoValueAnnotationOnElement(termName));
			}
			IEdmVocabularyAnnotation edmVocabularyAnnotation = enumerable.Single<IEdmVocabularyAnnotation>();
			return evaluator(edmVocabularyAnnotation.Value, null, edmVocabularyAnnotation.Term().Type);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000097B0 File Offset: 0x000079B0
		private static T FindInContainerAndExtendsRecursively<T>(IEdmEntityContainer container, string simpleName, Func<IEdmEntityContainer, string, T> finderFunc, int depth)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			if (depth <= 0)
			{
				throw new InvalidOperationException(Strings.Bad_CyclicEntityContainer(container.FullName()));
			}
			T t = finderFunc(container, simpleName);
			IEnumerable<IEdmOperationImport> enumerable = t as IEnumerable<IEdmOperationImport>;
			if (t == null || (enumerable != null && !enumerable.HasAny<IEdmOperationImport>()))
			{
				CsdlSemanticsEntityContainer csdlSemanticsEntityContainer = container as CsdlSemanticsEntityContainer;
				if (csdlSemanticsEntityContainer != null && csdlSemanticsEntityContainer.Extends != null)
				{
					return ExtensionMethods.FindInContainerAndExtendsRecursively<T>(csdlSemanticsEntityContainer.Extends, simpleName, finderFunc, --depth);
				}
			}
			return t;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00009830 File Offset: 0x00007A30
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

		// Token: 0x06000387 RID: 903 RVA: 0x00009890 File Offset: 0x00007A90
		private static void DerivedFrom(this IEdmModel model, IEdmStructuredType baseType, HashSetInternal<IEdmStructuredType> visited, List<IEdmStructuredType> derivedTypes)
		{
			if (visited.Add(baseType))
			{
				IEnumerable<IEdmStructuredType> enumerable = model.FindDirectlyDerivedTypes(baseType);
				if (enumerable != null && enumerable.HasAny<IEdmStructuredType>())
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
					if (enumerable != null && enumerable.HasAny<IEdmStructuredType>())
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

		// Token: 0x06000388 RID: 904 RVA: 0x00009994 File Offset: 0x00007B94
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
			list.Add(new EdmPropertyConstructor("FilterableProperties", new EdmCollectionExpression(filterableProperties.Select((IEdmStructuralProperty p) => new EdmPropertyPathExpression(p.Name)).ToArray<EdmPropertyPathExpression>())));
			list.Add(new EdmPropertyConstructor("ExpandableProperties", new EdmCollectionExpression(expandableProperties.Select((IEdmNavigationProperty p) => new EdmNavigationPropertyPathExpression(p.Name)).ToArray<EdmNavigationPropertyPathExpression>())));
			IList<IEdmPropertyConstructor> list2 = list;
			IEdmRecordExpression edmRecordExpression = new EdmRecordExpression(list2);
			IEdmTerm changeTrackingTerm = CapabilitiesVocabularyModel.ChangeTrackingTerm;
			EdmVocabularyAnnotation edmVocabularyAnnotation = new EdmVocabularyAnnotation(target, changeTrackingTerm, edmRecordExpression);
			edmVocabularyAnnotation.SetSerializationLocation(model, new EdmVocabularyAnnotationSerializationLocation?(EdmVocabularyAnnotationSerializationLocation.Inline));
			model.SetVocabularyAnnotation(edmVocabularyAnnotation);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00009A94 File Offset: 0x00007C94
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
				model.SetPrimitiveValueConverter(edmTypeDefinition, DefaultPrimitiveValueConverter.Instance);
			}
			return new EdmTypeDefinitionReference(edmTypeDefinition, isNullable);
		}

		// Token: 0x040000DC RID: 220
		private const int ContainerExtendsMaxDepth = 100;

		// Token: 0x040000DD RID: 221
		private const string CollectionTypeFormat = "Collection({0})";

		// Token: 0x040000DE RID: 222
		private static readonly IEnumerable<IEdmStructuralProperty> EmptyStructuralProperties = new Collection<IEdmStructuralProperty>();

		// Token: 0x040000DF RID: 223
		private static readonly IEnumerable<IEdmNavigationProperty> EmptyNavigationProperties = new Collection<IEdmNavigationProperty>();

		// Token: 0x040000E0 RID: 224
		private static readonly Func<IEdmModel, string, IEdmSchemaType> findType = (IEdmModel model, string qualifiedName) => model.FindDeclaredType(qualifiedName);

		// Token: 0x040000E1 RID: 225
		private static readonly Func<IEdmModel, IEdmType, IEnumerable<IEdmOperation>> findBoundOperations = (IEdmModel model, IEdmType bindingType) => model.FindDeclaredBoundOperations(bindingType);

		// Token: 0x040000E2 RID: 226
		private static readonly Func<IEdmModel, string, IEdmTerm> findTerm = (IEdmModel model, string qualifiedName) => model.FindDeclaredTerm(qualifiedName);

		// Token: 0x040000E3 RID: 227
		private static readonly Func<IEdmModel, string, IEnumerable<IEdmOperation>> findOperations = (IEdmModel model, string qualifiedName) => model.FindDeclaredOperations(qualifiedName);

		// Token: 0x040000E4 RID: 228
		private static readonly Func<IEdmModel, string, IEdmEntityContainer> findEntityContainer = delegate(IEdmModel model, string qualifiedName)
		{
			if (!model.ExistsContainer(qualifiedName))
			{
				return null;
			}
			return model.EntityContainer;
		};

		// Token: 0x040000E5 RID: 229
		private static readonly Func<IEnumerable<IEdmOperation>, IEnumerable<IEdmOperation>, IEnumerable<IEdmOperation>> mergeFunctions = (IEnumerable<IEdmOperation> f1, IEnumerable<IEdmOperation> f2) => f1.Concat(f2);

		// Token: 0x02000224 RID: 548
		internal static class TypeName<T>
		{
			// Token: 0x040007E3 RID: 2019
			public static readonly string LocalName = typeof(T).ToString().Replace("_", "_____").Replace('.', '_')
				.Replace("[", "")
				.Replace("]", "")
				.Replace(",", "__")
				.Replace("`", "___")
				.Replace("+", "____");
		}
	}
}
