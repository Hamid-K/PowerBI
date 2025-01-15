using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Evaluation;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Internal;
using Microsoft.Data.Edm.Validation;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm
{
	// Token: 0x020001B3 RID: 435
	public static class ExtensionMethods
	{
		// Token: 0x06000A33 RID: 2611 RVA: 0x0001D3E7 File Offset: 0x0001B5E7
		public static Version GetEdmVersion(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "EdmVersion");
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0001D406 File Offset: 0x0001B606
		public static void SetEdmVersion(this IEdmModel model, Version version)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "EdmVersion", version);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0001D426 File Offset: 0x0001B626
		public static IEdmSchemaType FindType(this IEdmModel model, string qualifiedName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			return model.FindAcrossModels(qualifiedName, ExtensionMethods.findType, new Func<IEdmSchemaType, IEdmSchemaType, IEdmSchemaType>(RegistrationHelper.CreateAmbiguousTypeBinding));
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0001D458 File Offset: 0x0001B658
		public static IEdmValueTerm FindValueTerm(this IEdmModel model, string qualifiedName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			return model.FindAcrossModels(qualifiedName, ExtensionMethods.findValueTerm, new Func<IEdmValueTerm, IEdmValueTerm, IEdmValueTerm>(RegistrationHelper.CreateAmbiguousValueTermBinding));
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0001D48A File Offset: 0x0001B68A
		public static IEnumerable<IEdmFunction> FindFunctions(this IEdmModel model, string qualifiedName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			return model.FindAcrossModels(qualifiedName, ExtensionMethods.findFunctions, ExtensionMethods.mergeFunctions);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0001D4B5 File Offset: 0x0001B6B5
		public static IEdmEntityContainer FindEntityContainer(this IEdmModel model, string qualifiedName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			return model.FindAcrossModels(qualifiedName, ExtensionMethods.findEntityContainer, new Func<IEdmEntityContainer, IEdmEntityContainer, IEdmEntityContainer>(RegistrationHelper.CreateAmbiguousEntityContainerBinding));
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0001D4E7 File Offset: 0x0001B6E7
		public static IEnumerable<IEdmEntityContainer> EntityContainers(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return Enumerable.OfType<IEdmEntityContainer>(model.SchemaElements);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0001D500 File Offset: 0x0001B700
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

		// Token: 0x06000A3B RID: 2619 RVA: 0x0001D564 File Offset: 0x0001B764
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

		// Token: 0x06000A3C RID: 2620 RVA: 0x0001D5DC File Offset: 0x0001B7DC
		public static IEnumerable<T> FindVocabularyAnnotations<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmTerm term) where T : IEdmVocabularyAnnotation
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			return model.FindVocabularyAnnotations(element, term, null);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0001D60C File Offset: 0x0001B80C
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

		// Token: 0x06000A3E RID: 2622 RVA: 0x0001D6C0 File Offset: 0x0001B8C0
		public static IEnumerable<T> FindVocabularyAnnotations<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName) where T : IEdmVocabularyAnnotation
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			return model.FindVocabularyAnnotations(element, termName, null);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0001D98C File Offset: 0x0001BB8C
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

		// Token: 0x06000A40 RID: 2624 RVA: 0x0001D9C0 File Offset: 0x0001BBC0
		public static IEdmValue GetPropertyValue(this IEdmModel model, IEdmStructuredValue context, IEdmProperty property, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmProperty>(property, "property");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetPropertyValue(context, context.Type.AsEntity().EntityDefinition(), property, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0001DA24 File Offset: 0x0001BC24
		public static IEdmValue GetPropertyValue(this IEdmModel model, IEdmStructuredValue context, IEdmProperty property, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmProperty>(property, "property");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetPropertyValue(context, context.Type.AsEntity().EntityDefinition(), property, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0001DA88 File Offset: 0x0001BC88
		public static T GetPropertyValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmProperty property, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmProperty>(property, "property");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetPropertyValue(context, context.Type.AsEntity().EntityDefinition(), property, null, new Func<IEdmExpression, IEdmStructuredValue, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0001DAEC File Offset: 0x0001BCEC
		public static T GetPropertyValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmProperty property, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmProperty>(property, "property");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetPropertyValue(context, context.Type.AsEntity().EntityDefinition(), property, qualifier, new Func<IEdmExpression, IEdmStructuredValue, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0001DB50 File Offset: 0x0001BD50
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmStructuredValue context, string termName, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), termName, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0001DBB4 File Offset: 0x0001BDB4
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmStructuredValue context, string termName, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), termName, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0001DC18 File Offset: 0x0001BE18
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmStructuredValue context, IEdmValueTerm term, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), term, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0001DC7C File Offset: 0x0001BE7C
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmStructuredValue context, IEdmValueTerm term, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), term, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0001DCE0 File Offset: 0x0001BEE0
		public static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, string termName, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), termName, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0001DD44 File Offset: 0x0001BF44
		public static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, string termName, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), termName, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0001DDA8 File Offset: 0x0001BFA8
		public static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmValueTerm term, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), term, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0001DE0C File Offset: 0x0001C00C
		public static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmValueTerm term, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), term, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0001DE70 File Offset: 0x0001C070
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "evaluator");
			return model.GetTermValue(element, termName, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0001DEC4 File Offset: 0x0001C0C4
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "evaluator");
			return model.GetTermValue(element, termName, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0001DF18 File Offset: 0x0001C118
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmValueTerm term, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "evaluator");
			return model.GetTermValue(element, term, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0001DF6C File Offset: 0x0001C16C
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmValueTerm term, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "evaluator");
			return model.GetTermValue(element, term, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0001DFC0 File Offset: 0x0001C1C0
		public static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(element, termName, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0001E014 File Offset: 0x0001C214
		public static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(element, termName, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0001E068 File Offset: 0x0001C268
		public static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmValueTerm term, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(element, term, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0001E0BC File Offset: 0x0001C2BC
		public static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmValueTerm term, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmValueTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(element, term, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0001E110 File Offset: 0x0001C310
		public static IEdmDocumentation GetDocumentation(this IEdmModel model, IEdmElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return (IEdmDocumentation)model.GetAnnotationValue(element, "http://schemas.microsoft.com/ado/2011/04/edm/documentation", "Documentation");
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0001E140 File Offset: 0x0001C340
		public static void SetDocumentation(this IEdmModel model, IEdmElement element, IEdmDocumentation documentation)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			model.SetAnnotationValue(element, "http://schemas.microsoft.com/ado/2011/04/edm/documentation", "Documentation", documentation);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0001E16C File Offset: 0x0001C36C
		public static object GetAnnotationValue(this IEdmModel model, IEdmElement element, string namespaceName, string localName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return model.DirectValueAnnotationsManager.GetAnnotationValue(element, namespaceName, localName);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0001E194 File Offset: 0x0001C394
		public static T GetAnnotationValue<T>(this IEdmModel model, IEdmElement element, string namespaceName, string localName) where T : class
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return ExtensionMethods.AnnotationValue<T>(model.GetAnnotationValue(element, namespaceName, localName));
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0001E1BC File Offset: 0x0001C3BC
		public static T GetAnnotationValue<T>(this IEdmModel model, IEdmElement element) where T : class
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return model.GetAnnotationValue(element, "http://schemas.microsoft.com/ado/2011/04/edm/internal", ExtensionMethods.TypeName<T>.LocalName);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0001E1E7 File Offset: 0x0001C3E7
		public static void SetAnnotationValue(this IEdmModel model, IEdmElement element, string namespaceName, string localName, object value)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			model.DirectValueAnnotationsManager.SetAnnotationValue(element, namespaceName, localName, value);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0001E214 File Offset: 0x0001C414
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

		// Token: 0x06000A5B RID: 2651 RVA: 0x0001E288 File Offset: 0x0001C488
		public static IEnumerable<IEdmStructuredType> FindAllDerivedTypes(this IEdmModel model, IEdmStructuredType baseType)
		{
			List<IEdmStructuredType> list = new List<IEdmStructuredType>();
			if (baseType is IEdmSchemaElement)
			{
				model.DerivedFrom(baseType, new HashSetInternal<IEdmStructuredType>(), list);
			}
			return list;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0001E2B1 File Offset: 0x0001C4B1
		public static void SetAnnotationValue<T>(this IEdmModel model, IEdmElement element, T value) where T : class
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			model.SetAnnotationValue(element, "http://schemas.microsoft.com/ado/2011/04/edm/internal", ExtensionMethods.TypeName<T>.LocalName, value);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0001E2E2 File Offset: 0x0001C4E2
		public static object[] GetAnnotationValues(this IEdmModel model, IEnumerable<IEdmDirectValueAnnotationBinding> annotations)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmDirectValueAnnotationBinding>>(annotations, "annotations");
			return model.DirectValueAnnotationsManager.GetAnnotationValues(annotations);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0001E308 File Offset: 0x0001C508
		public static void SetAnnotationValues(this IEdmModel model, IEnumerable<IEdmDirectValueAnnotationBinding> annotations)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmDirectValueAnnotationBinding>>(annotations, "annotations");
			model.DirectValueAnnotationsManager.SetAnnotationValues(annotations);
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0001E32E File Offset: 0x0001C52E
		public static IEnumerable<IEdmDirectValueAnnotation> DirectValueAnnotations(this IEdmModel model, IEdmElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return model.DirectValueAnnotationsManager.GetDirectValueAnnotations(element);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0001E354 File Offset: 0x0001C554
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

		// Token: 0x06000A61 RID: 2657 RVA: 0x0001E38C File Offset: 0x0001C58C
		public static IEnumerable<IEdmVocabularyAnnotation> VocabularyAnnotations(this IEdmVocabularyAnnotatable element, IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.FindVocabularyAnnotations(element);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0001E3AD File Offset: 0x0001C5AD
		public static string FullName(this IEdmSchemaElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmSchemaElement>(element, "element");
			return (element.Namespace ?? string.Empty) + "." + (element.Name ?? string.Empty);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0001E3E3 File Offset: 0x0001C5E3
		public static IEnumerable<IEdmEntitySet> EntitySets(this IEdmEntityContainer container)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			return Enumerable.OfType<IEdmEntitySet>(container.Elements);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0001E3FC File Offset: 0x0001C5FC
		public static IEnumerable<IEdmFunctionImport> FunctionImports(this IEdmEntityContainer container)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			return Enumerable.OfType<IEdmFunctionImport>(container.Elements);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0001E418 File Offset: 0x0001C618
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

		// Token: 0x06000A66 RID: 2662 RVA: 0x0001E444 File Offset: 0x0001C644
		public static string FullName(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			IEdmSchemaElement edmSchemaElement = type.Definition as IEdmSchemaElement;
			if (edmSchemaElement == null)
			{
				return null;
			}
			return edmSchemaElement.FullName();
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0001E474 File Offset: 0x0001C674
		public static IEdmPrimitiveType PrimitiveDefinition(this IEdmPrimitiveTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmPrimitiveTypeReference>(type, "type");
			return (IEdmPrimitiveType)type.Definition;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0001E490 File Offset: 0x0001C690
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

		// Token: 0x06000A69 RID: 2665 RVA: 0x0001E740 File Offset: 0x0001C940
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

		// Token: 0x06000A6A RID: 2666 RVA: 0x0001E75D File Offset: 0x0001C95D
		public static IEnumerable<IEdmStructuralProperty> DeclaredStructuralProperties(this IEdmStructuredType type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(type, "type");
			return Enumerable.OfType<IEdmStructuralProperty>(type.DeclaredProperties);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0001E776 File Offset: 0x0001C976
		public static IEnumerable<IEdmStructuralProperty> StructuralProperties(this IEdmStructuredType type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(type, "type");
			return Enumerable.OfType<IEdmStructuralProperty>(type.Properties());
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0001E78F File Offset: 0x0001C98F
		public static IEdmStructuredType StructuredDefinition(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return (IEdmStructuredType)type.Definition;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0001E7A8 File Offset: 0x0001C9A8
		public static bool IsAbstract(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().IsAbstract;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0001E7C1 File Offset: 0x0001C9C1
		public static bool IsOpen(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().IsOpen;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0001E7DA File Offset: 0x0001C9DA
		public static IEdmStructuredType BaseType(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().BaseType;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0001E7F3 File Offset: 0x0001C9F3
		public static IEnumerable<IEdmStructuralProperty> DeclaredStructuralProperties(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().DeclaredStructuralProperties();
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0001E80C File Offset: 0x0001CA0C
		public static IEnumerable<IEdmStructuralProperty> StructuralProperties(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().StructuralProperties();
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0001E825 File Offset: 0x0001CA25
		public static IEdmProperty FindProperty(this IEdmStructuredTypeReference type, string name)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			return type.StructuredDefinition().FindProperty(name);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0001E84B File Offset: 0x0001CA4B
		public static IEdmEntityType BaseEntityType(this IEdmEntityType type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(type, "type");
			return type.BaseType as IEdmEntityType;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0001E864 File Offset: 0x0001CA64
		public static IEnumerable<IEdmNavigationProperty> DeclaredNavigationProperties(this IEdmEntityType type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(type, "type");
			return Enumerable.OfType<IEdmNavigationProperty>(type.DeclaredProperties);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0001E87D File Offset: 0x0001CA7D
		public static IEnumerable<IEdmNavigationProperty> NavigationProperties(this IEdmEntityType type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(type, "type");
			return Enumerable.OfType<IEdmNavigationProperty>(type.Properties());
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0001E898 File Offset: 0x0001CA98
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

		// Token: 0x06000A77 RID: 2679 RVA: 0x0001E8E8 File Offset: 0x0001CAE8
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

		// Token: 0x06000A78 RID: 2680 RVA: 0x0001E957 File Offset: 0x0001CB57
		public static IEdmEntityType EntityDefinition(this IEdmEntityTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			return (IEdmEntityType)type.Definition;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0001E970 File Offset: 0x0001CB70
		public static IEdmEntityType BaseEntityType(this IEdmEntityTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			return type.EntityDefinition().BaseEntityType();
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0001E989 File Offset: 0x0001CB89
		public static IEnumerable<IEdmStructuralProperty> Key(this IEdmEntityTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			return type.EntityDefinition().Key();
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0001E9A2 File Offset: 0x0001CBA2
		public static IEnumerable<IEdmNavigationProperty> NavigationProperties(this IEdmEntityTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			return type.EntityDefinition().NavigationProperties();
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0001E9BB File Offset: 0x0001CBBB
		public static IEnumerable<IEdmNavigationProperty> DeclaredNavigationProperties(this IEdmEntityTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			return type.EntityDefinition().DeclaredNavigationProperties();
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0001E9D4 File Offset: 0x0001CBD4
		public static IEdmNavigationProperty FindNavigationProperty(this IEdmEntityTypeReference type, string name)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			return type.EntityDefinition().FindProperty(name) as IEdmNavigationProperty;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0001E9FF File Offset: 0x0001CBFF
		public static IEdmComplexType BaseComplexType(this IEdmComplexType type)
		{
			EdmUtil.CheckArgumentNull<IEdmComplexType>(type, "type");
			return type.BaseType as IEdmComplexType;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0001EA18 File Offset: 0x0001CC18
		public static IEdmComplexType ComplexDefinition(this IEdmComplexTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmComplexTypeReference>(type, "type");
			return (IEdmComplexType)type.Definition;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0001EA31 File Offset: 0x0001CC31
		public static IEdmComplexType BaseComplexType(this IEdmComplexTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmComplexTypeReference>(type, "type");
			return type.ComplexDefinition().BaseComplexType();
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0001EA4A File Offset: 0x0001CC4A
		public static IEdmEntityReferenceType EntityReferenceDefinition(this IEdmEntityReferenceTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityReferenceTypeReference>(type, "type");
			return (IEdmEntityReferenceType)type.Definition;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0001EA63 File Offset: 0x0001CC63
		public static IEdmEntityType EntityType(this IEdmEntityReferenceTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityReferenceTypeReference>(type, "type");
			return type.EntityReferenceDefinition().EntityType;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0001EA7C File Offset: 0x0001CC7C
		public static IEdmCollectionType CollectionDefinition(this IEdmCollectionTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmCollectionTypeReference>(type, "type");
			return (IEdmCollectionType)type.Definition;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0001EA95 File Offset: 0x0001CC95
		public static IEdmTypeReference ElementType(this IEdmCollectionTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmCollectionTypeReference>(type, "type");
			return type.CollectionDefinition().ElementType;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0001EAAE File Offset: 0x0001CCAE
		public static IEdmEnumType EnumDefinition(this IEdmEnumTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEnumTypeReference>(type, "type");
			return (IEdmEnumType)type.Definition;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0001EAC8 File Offset: 0x0001CCC8
		public static EdmMultiplicity Multiplicity(this IEdmNavigationProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(property, "property");
			IEdmNavigationProperty partner = property.Partner;
			if (partner == null)
			{
				return EdmMultiplicity.One;
			}
			IEdmTypeReference type = partner.Type;
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

		// Token: 0x06000A87 RID: 2695 RVA: 0x0001EB0C File Offset: 0x0001CD0C
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

		// Token: 0x06000A88 RID: 2696 RVA: 0x0001EB5A File Offset: 0x0001CD5A
		public static IEdmEntityType DeclaringEntityType(this IEdmNavigationProperty property)
		{
			return (IEdmEntityType)property.DeclaringType;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0001EB67 File Offset: 0x0001CD67
		public static IEdmRowType RowDefinition(this IEdmRowTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmRowTypeReference>(type, "type");
			return (IEdmRowType)type.Definition;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0001EB80 File Offset: 0x0001CD80
		public static IEdmPropertyValueBinding FindPropertyBinding(this IEdmTypeAnnotation annotation, IEdmProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeAnnotation>(annotation, "annotation");
			EdmUtil.CheckArgumentNull<IEdmProperty>(property, "property");
			foreach (IEdmPropertyValueBinding edmPropertyValueBinding in annotation.PropertyValueBindings)
			{
				if (edmPropertyValueBinding.BoundProperty == property)
				{
					return edmPropertyValueBinding;
				}
			}
			return null;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0001EBF0 File Offset: 0x0001CDF0
		public static IEdmPropertyValueBinding FindPropertyBinding(this IEdmTypeAnnotation annotation, string propertyName)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeAnnotation>(annotation, "annotation");
			EdmUtil.CheckArgumentNull<string>(propertyName, "propertyName");
			foreach (IEdmPropertyValueBinding edmPropertyValueBinding in annotation.PropertyValueBindings)
			{
				if (edmPropertyValueBinding.BoundProperty.Name.EqualsOrdinal(propertyName))
				{
					return edmPropertyValueBinding;
				}
			}
			return null;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0001EC68 File Offset: 0x0001CE68
		public static IEdmValueTerm ValueTerm(this IEdmValueAnnotation annotation)
		{
			EdmUtil.CheckArgumentNull<IEdmValueAnnotation>(annotation, "annotation");
			return (IEdmValueTerm)annotation.Term;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0001EC84 File Offset: 0x0001CE84
		public static bool TryGetStaticEntitySet(this IEdmFunctionImport functionImport, out IEdmEntitySet entitySet)
		{
			IEdmEntitySetReferenceExpression edmEntitySetReferenceExpression = functionImport.EntitySet as IEdmEntitySetReferenceExpression;
			entitySet = ((edmEntitySetReferenceExpression != null) ? edmEntitySetReferenceExpression.ReferencedEntitySet : null);
			return entitySet != null;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0001ECB4 File Offset: 0x0001CEB4
		public static bool TryGetRelativeEntitySetPath(this IEdmFunctionImport functionImport, IEdmModel model, out IEdmFunctionParameter parameter, out IEnumerable<IEdmNavigationProperty> path)
		{
			parameter = null;
			path = null;
			IEdmPathExpression edmPathExpression = functionImport.EntitySet as IEdmPathExpression;
			if (edmPathExpression == null)
			{
				return false;
			}
			List<string> list = Enumerable.ToList<string>(edmPathExpression.Path);
			if (list.Count == 0)
			{
				return false;
			}
			parameter = functionImport.FindParameter(list[0]);
			if (parameter == null)
			{
				return false;
			}
			if (list.Count == 1)
			{
				path = Enumerable.Empty<IEdmNavigationProperty>();
				return true;
			}
			IEdmEntityType edmEntityType = ExtensionMethods.GetPathSegmentEntityType(parameter.Type);
			List<IEdmNavigationProperty> list2 = new List<IEdmNavigationProperty>();
			for (int i = 1; i < list.Count; i++)
			{
				string text = list[i];
				if (EdmUtil.IsQualifiedName(text))
				{
					if (i == list.Count - 1)
					{
						return false;
					}
					IEdmEntityType edmEntityType2 = model.FindDeclaredType(text) as IEdmEntityType;
					if (edmEntityType2 == null || !edmEntityType2.IsOrInheritsFrom(edmEntityType))
					{
						return false;
					}
					edmEntityType = edmEntityType2;
				}
				else
				{
					IEdmNavigationProperty edmNavigationProperty = edmEntityType.FindProperty(text) as IEdmNavigationProperty;
					if (edmNavigationProperty == null)
					{
						return false;
					}
					list2.Add(edmNavigationProperty);
					edmEntityType = ExtensionMethods.GetPathSegmentEntityType(edmNavigationProperty.Type);
				}
			}
			path = list2;
			return true;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0001EDB0 File Offset: 0x0001CFB0
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

		// Token: 0x06000A90 RID: 2704 RVA: 0x0001EE0C File Offset: 0x0001D00C
		internal static IEdmEntityType GetPathSegmentEntityType(IEdmTypeReference segmentType)
		{
			return (segmentType.IsCollection() ? segmentType.AsCollection().ElementType() : segmentType).AsEntity().EntityDefinition();
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0001EE30 File Offset: 0x0001D030
		private static T FindAcrossModels<T>(this IEdmModel model, string qualifiedName, Func<IEdmModel, string, T> finder, Func<T, T, T> ambiguousCreator)
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

		// Token: 0x06000A92 RID: 2706 RVA: 0x0001EEA8 File Offset: 0x0001D0A8
		private static T GetPropertyValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmEntityType contextType, IEdmProperty property, string qualifier, Func<IEdmExpression, IEdmStructuredValue, T> evaluator)
		{
			IEdmEntityType edmEntityType = (IEdmEntityType)property.DeclaringType;
			IEnumerable<IEdmTypeAnnotation> enumerable = model.FindVocabularyAnnotations(contextType, edmEntityType, qualifier);
			if (Enumerable.Count<IEdmTypeAnnotation>(enumerable) != 1)
			{
				throw new InvalidOperationException(Strings.Edm_Evaluator_NoTermTypeAnnotationOnType(contextType.ToTraceString(), edmEntityType.ToTraceString()));
			}
			IEdmPropertyValueBinding edmPropertyValueBinding = Enumerable.Single<IEdmTypeAnnotation>(enumerable).FindPropertyBinding(property);
			if (edmPropertyValueBinding == null)
			{
				return default(T);
			}
			return evaluator.Invoke(edmPropertyValueBinding.Value, context);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0001EF14 File Offset: 0x0001D114
		private static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmEntityType contextType, IEdmValueTerm term, string qualifier, Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T> evaluator)
		{
			IEnumerable<IEdmValueAnnotation> enumerable = model.FindVocabularyAnnotations(contextType, term, qualifier);
			if (Enumerable.Count<IEdmValueAnnotation>(enumerable) != 1)
			{
				throw new InvalidOperationException(Strings.Edm_Evaluator_NoValueAnnotationOnType(contextType.ToTraceString(), term.ToTraceString()));
			}
			return evaluator.Invoke(Enumerable.Single<IEdmValueAnnotation>(enumerable).Value, context, term.Type);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0001EF68 File Offset: 0x0001D168
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

		// Token: 0x06000A95 RID: 2709 RVA: 0x0001EFBC File Offset: 0x0001D1BC
		private static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmValueTerm term, string qualifier, Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T> evaluator)
		{
			IEnumerable<IEdmValueAnnotation> enumerable = model.FindVocabularyAnnotations(element, term, qualifier);
			if (Enumerable.Count<IEdmValueAnnotation>(enumerable) != 1)
			{
				throw new InvalidOperationException(Strings.Edm_Evaluator_NoValueAnnotationOnElement(term.ToTraceString()));
			}
			return evaluator.Invoke(Enumerable.Single<IEdmValueAnnotation>(enumerable).Value, null, term.Type);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0001F008 File Offset: 0x0001D208
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

		// Token: 0x06000A97 RID: 2711 RVA: 0x0001F054 File Offset: 0x0001D254
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

		// Token: 0x06000A98 RID: 2712 RVA: 0x0001F0B4 File Offset: 0x0001D2B4
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

		// Token: 0x040004B5 RID: 1205
		private static readonly Func<IEdmModel, string, IEdmSchemaType> findType = (IEdmModel model, string qualifiedName) => model.FindDeclaredType(qualifiedName);

		// Token: 0x040004B6 RID: 1206
		private static readonly Func<IEdmModel, string, IEdmValueTerm> findValueTerm = (IEdmModel model, string qualifiedName) => model.FindDeclaredValueTerm(qualifiedName);

		// Token: 0x040004B7 RID: 1207
		private static readonly Func<IEdmModel, string, IEnumerable<IEdmFunction>> findFunctions = (IEdmModel model, string qualifiedName) => model.FindDeclaredFunctions(qualifiedName);

		// Token: 0x040004B8 RID: 1208
		private static readonly Func<IEdmModel, string, IEdmEntityContainer> findEntityContainer = (IEdmModel model, string qualifiedName) => model.FindDeclaredEntityContainer(qualifiedName);

		// Token: 0x040004B9 RID: 1209
		private static readonly Func<IEnumerable<IEdmFunction>, IEnumerable<IEdmFunction>, IEnumerable<IEdmFunction>> mergeFunctions = (IEnumerable<IEdmFunction> f1, IEnumerable<IEdmFunction> f2) => Enumerable.Concat<IEdmFunction>(f1, f2);

		// Token: 0x020001B4 RID: 436
		internal static class TypeName<T>
		{
			// Token: 0x040004BF RID: 1215
			public static readonly string LocalName = typeof(T).ToString().Replace("_", "_____").Replace('.', '_')
				.Replace("[", "")
				.Replace("]", "")
				.Replace(",", "__")
				.Replace("`", "___")
				.Replace("+", "____");
		}
	}
}
