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
	// Token: 0x02000011 RID: 17
	public static class ExtensionMethods
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00003EE7 File Offset: 0x000020E7
		public static Version GetEdmVersion(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "EdmVersion");
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003F06 File Offset: 0x00002106
		public static void SetEdmVersion(this IEdmModel model, Version version)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "EdmVersion", version);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003F26 File Offset: 0x00002126
		public static IEdmSchemaType FindType(this IEdmModel model, string qualifiedName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			return model.FindAcrossModels(qualifiedName, ExtensionMethods.findType, new Func<IEdmSchemaType, IEdmSchemaType, IEdmSchemaType>(RegistrationHelper.CreateAmbiguousTypeBinding));
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003F58 File Offset: 0x00002158
		public static IEnumerable<IEdmOperation> FindBoundOperations(this IEdmModel model, IEdmType bindingType)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmType>(bindingType, "bindingType");
			return model.FindAcrossModels(bindingType, ExtensionMethods.findBoundOperations, ExtensionMethods.mergeFunctions);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003F84 File Offset: 0x00002184
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

		// Token: 0x060000B4 RID: 180 RVA: 0x00004018 File Offset: 0x00002218
		public static IEdmTerm FindTerm(this IEdmModel model, string qualifiedName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			return model.FindAcrossModels(qualifiedName, ExtensionMethods.findTerm, new Func<IEdmTerm, IEdmTerm, IEdmTerm>(RegistrationHelper.CreateAmbiguousTermBinding));
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000404A File Offset: 0x0000224A
		public static IEnumerable<IEdmOperation> FindOperations(this IEdmModel model, string qualifiedName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			return model.FindAcrossModels(qualifiedName, ExtensionMethods.findOperations, ExtensionMethods.mergeFunctions);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004078 File Offset: 0x00002278
		public static bool ExistsContainer(this IEdmModel model, string containerName)
		{
			if (model.EntityContainer == null)
			{
				return false;
			}
			string text = (model.EntityContainer.Namespace ?? string.Empty) + "." + (containerName ?? string.Empty);
			return string.Equals(model.EntityContainer.FullName(), text, 4) || string.Equals(model.EntityContainer.FullName(), containerName, 4);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000040E3 File Offset: 0x000022E3
		public static IEdmEntityContainer FindEntityContainer(this IEdmModel model, string qualifiedName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<string>(qualifiedName, "qualifiedName");
			return model.FindAcrossModels(qualifiedName, ExtensionMethods.findEntityContainer, new Func<IEdmEntityContainer, IEdmEntityContainer, IEdmEntityContainer>(RegistrationHelper.CreateAmbiguousEntityContainerBinding));
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004118 File Offset: 0x00002318
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

		// Token: 0x060000B9 RID: 185 RVA: 0x0000417C File Offset: 0x0000237C
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

		// Token: 0x060000BA RID: 186 RVA: 0x000041F4 File Offset: 0x000023F4
		public static IEnumerable<T> FindVocabularyAnnotations<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmTerm term) where T : IEdmVocabularyAnnotation
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			return model.FindVocabularyAnnotations(element, term, null);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004224 File Offset: 0x00002424
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
			IEnumerable<T> enumerable = list;
			return enumerable ?? Enumerable.Empty<T>();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000042DC File Offset: 0x000024DC
		public static IEnumerable<T> FindVocabularyAnnotations<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName) where T : IEdmVocabularyAnnotation
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			return model.FindVocabularyAnnotations(element, termName, null);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000430B File Offset: 0x0000250B
		public static IEnumerable<T> FindVocabularyAnnotations<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, string qualifier) where T : IEdmVocabularyAnnotation
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			string namespaceName;
			string name;
			if (EdmUtil.TryGetNamespaceNameFromQualifiedName(termName, out namespaceName, out name))
			{
				foreach (T t in Enumerable.OfType<T>(model.FindVocabularyAnnotations(element)))
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

		// Token: 0x060000BE RID: 190 RVA: 0x00004330 File Offset: 0x00002530
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmStructuredValue context, string termName, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), termName, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004394 File Offset: 0x00002594
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmStructuredValue context, string termName, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), termName, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000043F8 File Offset: 0x000025F8
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmStructuredValue context, IEdmTerm term, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), term, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000445C File Offset: 0x0000265C
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmStructuredValue context, IEdmTerm term, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "expressionEvaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), term, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000044C0 File Offset: 0x000026C0
		public static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, string termName, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), termName, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004524 File Offset: 0x00002724
		public static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, string termName, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), termName, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004588 File Offset: 0x00002788
		public static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmTerm term, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), term, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000045EC File Offset: 0x000027EC
		public static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmTerm term, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmStructuredValue>(context, "context");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(context, context.Type.AsEntity().EntityDefinition(), term, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004650 File Offset: 0x00002850
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "evaluator");
			return model.GetTermValue(element, termName, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000046A4 File Offset: 0x000028A4
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "evaluator");
			return model.GetTermValue(element, termName, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000046F8 File Offset: 0x000028F8
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmTerm term, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "evaluator");
			return model.GetTermValue(element, term, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000474C File Offset: 0x0000294C
		public static IEdmValue GetTermValue(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmTerm term, string qualifier, EdmExpressionEvaluator expressionEvaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmExpressionEvaluator>(expressionEvaluator, "evaluator");
			return model.GetTermValue(element, term, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, IEdmValue>(expressionEvaluator.Evaluate));
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000047A0 File Offset: 0x000029A0
		public static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(element, termName, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000047F4 File Offset: 0x000029F4
		public static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<string>(termName, "termName");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(element, termName, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004848 File Offset: 0x00002A48
		public static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmTerm term, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(element, term, null, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000489C File Offset: 0x00002A9C
		public static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmTerm term, string qualifier, EdmToClrEvaluator evaluator)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmTerm>(term, "term");
			EdmUtil.CheckArgumentNull<EdmToClrEvaluator>(evaluator, "evaluator");
			return model.GetTermValue(element, term, qualifier, new Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T>(evaluator.EvaluateToClrValue<T>));
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000048F0 File Offset: 0x00002AF0
		public static object GetAnnotationValue(this IEdmModel model, IEdmElement element, string namespaceName, string localName)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return model.DirectValueAnnotationsManager.GetAnnotationValue(element, namespaceName, localName);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004918 File Offset: 0x00002B18
		public static T GetAnnotationValue<T>(this IEdmModel model, IEdmElement element, string namespaceName, string localName) where T : class
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return ExtensionMethods.AnnotationValue<T>(model.GetAnnotationValue(element, namespaceName, localName));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004940 File Offset: 0x00002B40
		public static T GetAnnotationValue<T>(this IEdmModel model, IEdmElement element) where T : class
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return model.GetAnnotationValue(element, "http://schemas.microsoft.com/ado/2011/04/edm/internal", ExtensionMethods.TypeName<T>.LocalName);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000496B File Offset: 0x00002B6B
		public static void SetAnnotationValue(this IEdmModel model, IEdmElement element, string namespaceName, string localName, object value)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			model.DirectValueAnnotationsManager.SetAnnotationValue(element, namespaceName, localName, value);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004998 File Offset: 0x00002B98
		public static string GetDescriptionAnnotation(this IEdmModel model, IEdmVocabularyAnnotatable target)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(target, "target");
			IEdmVocabularyAnnotation edmVocabularyAnnotation = Enumerable.FirstOrDefault<IEdmVocabularyAnnotation>(model.FindVocabularyAnnotations(target, CoreVocabularyModel.DescriptionTerm));
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

		// Token: 0x060000D3 RID: 211 RVA: 0x000049EC File Offset: 0x00002BEC
		public static string GetLongDescriptionAnnotation(this IEdmModel model, IEdmVocabularyAnnotatable target)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(target, "target");
			IEdmVocabularyAnnotation edmVocabularyAnnotation = Enumerable.FirstOrDefault<IEdmVocabularyAnnotation>(model.FindVocabularyAnnotations(target, CoreVocabularyModel.LongDescriptionTerm));
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

		// Token: 0x060000D4 RID: 212 RVA: 0x00004A40 File Offset: 0x00002C40
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

		// Token: 0x060000D5 RID: 213 RVA: 0x00004AB4 File Offset: 0x00002CB4
		public static IEnumerable<IEdmStructuredType> FindAllDerivedTypes(this IEdmModel model, IEdmStructuredType baseType)
		{
			List<IEdmStructuredType> list = new List<IEdmStructuredType>();
			if (baseType is IEdmSchemaElement)
			{
				model.DerivedFrom(baseType, new HashSetInternal<IEdmStructuredType>(), list);
			}
			return list;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004ADD File Offset: 0x00002CDD
		public static void SetAnnotationValue<T>(this IEdmModel model, IEdmElement element, T value) where T : class
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			model.SetAnnotationValue(element, "http://schemas.microsoft.com/ado/2011/04/edm/internal", ExtensionMethods.TypeName<T>.LocalName, value);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004B0E File Offset: 0x00002D0E
		public static object[] GetAnnotationValues(this IEdmModel model, IEnumerable<IEdmDirectValueAnnotationBinding> annotations)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmDirectValueAnnotationBinding>>(annotations, "annotations");
			return model.DirectValueAnnotationsManager.GetAnnotationValues(annotations);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004B34 File Offset: 0x00002D34
		public static void SetAnnotationValues(this IEdmModel model, IEnumerable<IEdmDirectValueAnnotationBinding> annotations)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmDirectValueAnnotationBinding>>(annotations, "annotations");
			model.DirectValueAnnotationsManager.SetAnnotationValues(annotations);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004B5A File Offset: 0x00002D5A
		public static IEnumerable<IEdmDirectValueAnnotation> DirectValueAnnotations(this IEdmModel model, IEdmElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return model.DirectValueAnnotationsManager.GetDirectValueAnnotations(element);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004B80 File Offset: 0x00002D80
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

		// Token: 0x060000DB RID: 219 RVA: 0x00004BD4 File Offset: 0x00002DD4
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

		// Token: 0x060000DC RID: 220 RVA: 0x00004C24 File Offset: 0x00002E24
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

		// Token: 0x060000DD RID: 221 RVA: 0x00004C7C File Offset: 0x00002E7C
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

		// Token: 0x060000DE RID: 222 RVA: 0x00004CC4 File Offset: 0x00002EC4
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

		// Token: 0x060000DF RID: 223 RVA: 0x00004D0C File Offset: 0x00002F0C
		public static IEdmNavigationSource FindDeclaredNavigationSource(this IEdmModel model, string qualifiedName)
		{
			IEdmEntitySet edmEntitySet = model.FindDeclaredEntitySet(qualifiedName);
			if (edmEntitySet != null)
			{
				return edmEntitySet;
			}
			return model.FindDeclaredSingleton(qualifiedName);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004D30 File Offset: 0x00002F30
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

		// Token: 0x060000E1 RID: 225 RVA: 0x00004D5D File Offset: 0x00002F5D
		public static IPrimitiveValueConverter GetPrimitiveValueConverter(this IEdmModel model, IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "mode");
			if (type == null || !type.IsTypeDefinition())
			{
				return PassThroughPrimitiveValueConverter.Instance;
			}
			return model.GetPrimitiveValueConverter(type.Definition);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004D88 File Offset: 0x00002F88
		public static void SetPrimitiveValueConverter(this IEdmModel model, IEdmTypeDefinitionReference typeDefinition, IPrimitiveValueConverter converter)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmTypeDefinitionReference>(typeDefinition, "typeDefinition");
			EdmUtil.CheckArgumentNull<IPrimitiveValueConverter>(converter, "converter");
			model.SetPrimitiveValueConverter(typeDefinition.Definition, converter);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004DBB File Offset: 0x00002FBB
		public static EdmComplexType AddComplexType(this EdmModel model, string namespaceName, string name)
		{
			return model.AddComplexType(namespaceName, name, null, false);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004DC7 File Offset: 0x00002FC7
		public static EdmComplexType AddComplexType(this EdmModel model, string namespaceName, string name, IEdmComplexType baseType)
		{
			return model.AddComplexType(namespaceName, name, baseType, false, false);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004DD4 File Offset: 0x00002FD4
		public static EdmComplexType AddComplexType(this EdmModel model, string namespaceName, string name, IEdmComplexType baseType, bool isAbstract)
		{
			return model.AddComplexType(namespaceName, name, baseType, isAbstract, false);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004DE4 File Offset: 0x00002FE4
		public static EdmComplexType AddComplexType(this EdmModel model, string namespaceName, string name, IEdmComplexType baseType, bool isAbstract, bool isOpen)
		{
			EdmComplexType edmComplexType = new EdmComplexType(namespaceName, name, baseType, isAbstract, isOpen);
			model.AddElement(edmComplexType);
			return edmComplexType;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004E06 File Offset: 0x00003006
		public static EdmEntityType AddEntityType(this EdmModel model, string namespaceName, string name)
		{
			return model.AddEntityType(namespaceName, name, null, false, false);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004E13 File Offset: 0x00003013
		public static EdmEntityType AddEntityType(this EdmModel model, string namespaceName, string name, IEdmEntityType baseType)
		{
			return model.AddEntityType(namespaceName, name, baseType, false, false);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004E20 File Offset: 0x00003020
		public static EdmEntityType AddEntityType(this EdmModel model, string namespaceName, string name, IEdmEntityType baseType, bool isAbstract, bool isOpen)
		{
			return model.AddEntityType(namespaceName, name, baseType, isAbstract, isOpen, false);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004E30 File Offset: 0x00003030
		public static EdmEntityType AddEntityType(this EdmModel model, string namespaceName, string name, IEdmEntityType baseType, bool isAbstract, bool isOpen, bool hasStream)
		{
			EdmEntityType edmEntityType = new EdmEntityType(namespaceName, name, baseType, isAbstract, isOpen, hasStream);
			model.AddElement(edmEntityType);
			return edmEntityType;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004E54 File Offset: 0x00003054
		public static EdmEntityContainer AddEntityContainer(this EdmModel model, string namespaceName, string name)
		{
			EdmEntityContainer edmEntityContainer = new EdmEntityContainer(namespaceName, name);
			model.AddElement(edmEntityContainer);
			return edmEntityContainer;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004E74 File Offset: 0x00003074
		public static void SetOptimisticConcurrencyAnnotation(this EdmModel model, IEdmEntitySet target, IEnumerable<IEdmStructuralProperty> properties)
		{
			EdmUtil.CheckArgumentNull<EdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmEntitySet>(target, "target");
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmStructuralProperty>>(properties, "properties");
			IEdmCollectionExpression edmCollectionExpression = new EdmCollectionExpression(Enumerable.ToArray<EdmPropertyPathExpression>(Enumerable.Select<IEdmStructuralProperty, EdmPropertyPathExpression>(properties, (IEdmStructuralProperty p) => new EdmPropertyPathExpression(p.Name))));
			IEdmTerm concurrencyTerm = CoreVocabularyModel.ConcurrencyTerm;
			EdmVocabularyAnnotation edmVocabularyAnnotation = new EdmVocabularyAnnotation(target, concurrencyTerm, edmCollectionExpression);
			edmVocabularyAnnotation.SetSerializationLocation(model, new EdmVocabularyAnnotationSerializationLocation?(EdmVocabularyAnnotationSerializationLocation.Inline));
			model.SetVocabularyAnnotation(edmVocabularyAnnotation);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004EF8 File Offset: 0x000030F8
		public static void SetDescriptionAnnotation(this EdmModel model, IEdmVocabularyAnnotatable target, string description)
		{
			EdmUtil.CheckArgumentNull<EdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(target, "target");
			EdmUtil.CheckArgumentNull<string>(description, "description");
			EdmVocabularyAnnotation edmVocabularyAnnotation = new EdmVocabularyAnnotation(target, CoreVocabularyModel.DescriptionTerm, new EdmStringConstant(description));
			edmVocabularyAnnotation.SetSerializationLocation(model, new EdmVocabularyAnnotationSerializationLocation?(EdmVocabularyAnnotationSerializationLocation.Inline));
			model.SetVocabularyAnnotation(edmVocabularyAnnotation);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004F50 File Offset: 0x00003150
		public static void SetLongDescriptionAnnotation(this EdmModel model, IEdmVocabularyAnnotatable target, string description)
		{
			EdmUtil.CheckArgumentNull<EdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(target, "target");
			EdmUtil.CheckArgumentNull<string>(description, "description");
			EdmVocabularyAnnotation edmVocabularyAnnotation = new EdmVocabularyAnnotation(target, CoreVocabularyModel.LongDescriptionTerm, new EdmStringConstant(description));
			edmVocabularyAnnotation.SetSerializationLocation(model, new EdmVocabularyAnnotationSerializationLocation?(EdmVocabularyAnnotationSerializationLocation.Inline));
			model.SetVocabularyAnnotation(edmVocabularyAnnotation);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004FA7 File Offset: 0x000031A7
		public static void SetChangeTrackingAnnotation(this EdmModel model, IEdmEntityContainer target, bool isSupported)
		{
			model.SetChangeTrackingAnnotationImplementation(target, isSupported, null, null);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004FB3 File Offset: 0x000031B3
		public static void SetChangeTrackingAnnotation(this EdmModel model, IEdmEntitySet target, bool isSupported, IEnumerable<IEdmStructuralProperty> filterableProperties, IEnumerable<IEdmNavigationProperty> expandableProperties)
		{
			model.SetChangeTrackingAnnotationImplementation(target, isSupported, filterableProperties, expandableProperties);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004FC0 File Offset: 0x000031C0
		public static IEdmTypeDefinitionReference GetUInt16(this EdmModel model, string namespaceName, bool isNullable)
		{
			return model.GetUIntImplementation(namespaceName, "UInt16", "Edm.Int32", isNullable);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004FD4 File Offset: 0x000031D4
		public static IEdmTypeDefinitionReference GetUInt32(this EdmModel model, string namespaceName, bool isNullable)
		{
			return model.GetUIntImplementation(namespaceName, "UInt32", "Edm.Int64", isNullable);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004FE8 File Offset: 0x000031E8
		public static IEdmTypeDefinitionReference GetUInt64(this EdmModel model, string namespaceName, bool isNullable)
		{
			return model.GetUIntImplementation(namespaceName, "UInt64", "Edm.Decimal", isNullable);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004FFC File Offset: 0x000031FC
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

		// Token: 0x060000F5 RID: 245 RVA: 0x00005034 File Offset: 0x00003234
		public static IEnumerable<IEdmVocabularyAnnotation> VocabularyAnnotations(this IEdmVocabularyAnnotatable element, IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotatable>(element, "element");
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return model.FindVocabularyAnnotations(element);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005058 File Offset: 0x00003258
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
			return element.Namespace + "." + element.Name;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000050A4 File Offset: 0x000032A4
		public static string ShortQualifiedName(this IEdmSchemaElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmSchemaElement>(element, "element");
			if (element.Namespace != null && element.Namespace.Equals("Edm"))
			{
				return element.Name ?? string.Empty;
			}
			return element.FullName();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000050E2 File Offset: 0x000032E2
		public static IEnumerable<IEdmEntitySet> EntitySets(this IEdmEntityContainer container)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			return Enumerable.OfType<IEdmEntitySet>(container.AllElements(100));
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000050FD File Offset: 0x000032FD
		public static IEnumerable<IEdmSingleton> Singletons(this IEdmEntityContainer container)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			return Enumerable.OfType<IEdmSingleton>(container.AllElements(100));
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005118 File Offset: 0x00003318
		public static IEnumerable<IEdmOperationImport> OperationImports(this IEdmEntityContainer container)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainer>(container, "container");
			return Enumerable.OfType<IEdmOperationImport>(container.AllElements(100));
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005134 File Offset: 0x00003334
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

		// Token: 0x060000FC RID: 252 RVA: 0x0000515F File Offset: 0x0000335F
		public static string FullName(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return type.Definition.FullTypeName();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005178 File Offset: 0x00003378
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

		// Token: 0x060000FE RID: 254 RVA: 0x000051A8 File Offset: 0x000033A8
		public static string FullTypeName(this IEdmType type)
		{
			EdmUtil.CheckArgumentNull<IEdmType>(type, "type");
			EdmCoreModel.EdmValidCoreModelPrimitiveType edmValidCoreModelPrimitiveType = type as EdmCoreModel.EdmValidCoreModelPrimitiveType;
			if (edmValidCoreModelPrimitiveType != null)
			{
				return edmValidCoreModelPrimitiveType.FullName;
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

		// Token: 0x060000FF RID: 255 RVA: 0x00005224 File Offset: 0x00003424
		public static IEdmType AsElementType(this IEdmType type)
		{
			IEdmCollectionType edmCollectionType = type as IEdmCollectionType;
			if (edmCollectionType == null)
			{
				return type;
			}
			return edmCollectionType.ElementType.Definition;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005248 File Offset: 0x00003448
		public static IEdmPrimitiveType PrimitiveDefinition(this IEdmPrimitiveTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmPrimitiveTypeReference>(type, "type");
			return (IEdmPrimitiveType)type.Definition;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005264 File Offset: 0x00003464
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

		// Token: 0x06000102 RID: 258 RVA: 0x0000528F File Offset: 0x0000348F
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

		// Token: 0x06000103 RID: 259 RVA: 0x0000529F File Offset: 0x0000349F
		public static IEnumerable<IEdmStructuralProperty> DeclaredStructuralProperties(this IEdmStructuredType type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(type, "type");
			return Enumerable.OfType<IEdmStructuralProperty>(type.DeclaredProperties);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000052B8 File Offset: 0x000034B8
		public static IEnumerable<IEdmStructuralProperty> StructuralProperties(this IEdmStructuredType type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(type, "type");
			return Enumerable.OfType<IEdmStructuralProperty>(type.Properties());
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000052D1 File Offset: 0x000034D1
		public static IEdmStructuredType StructuredDefinition(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return (IEdmStructuredType)type.Definition;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000052EA File Offset: 0x000034EA
		public static bool IsAbstract(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().IsAbstract;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005303 File Offset: 0x00003503
		public static bool IsOpen(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().IsOpen;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000531C File Offset: 0x0000351C
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

		// Token: 0x06000109 RID: 265 RVA: 0x00005362 File Offset: 0x00003562
		public static IEdmStructuredType BaseType(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().BaseType;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000537B File Offset: 0x0000357B
		public static IEnumerable<IEdmStructuralProperty> DeclaredStructuralProperties(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().DeclaredStructuralProperties();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005394 File Offset: 0x00003594
		public static IEnumerable<IEdmStructuralProperty> StructuralProperties(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().StructuralProperties();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000053AD File Offset: 0x000035AD
		public static IEdmProperty FindProperty(this IEdmStructuredTypeReference type, string name)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			return type.StructuredDefinition().FindProperty(name);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000053D3 File Offset: 0x000035D3
		public static IEnumerable<IEdmNavigationProperty> NavigationProperties(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().NavigationProperties();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000053EC File Offset: 0x000035EC
		public static IEnumerable<IEdmNavigationProperty> DeclaredNavigationProperties(this IEdmStructuredTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			return type.StructuredDefinition().DeclaredNavigationProperties();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005405 File Offset: 0x00003605
		public static IEdmNavigationProperty FindNavigationProperty(this IEdmStructuredTypeReference type, string name)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredTypeReference>(type, "type");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			return type.StructuredDefinition().FindProperty(name) as IEdmNavigationProperty;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005430 File Offset: 0x00003630
		public static IEdmEntityType BaseEntityType(this IEdmEntityType type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(type, "type");
			return type.BaseType as IEdmEntityType;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005449 File Offset: 0x00003649
		public static IEdmStructuredType BaseType(this IEdmStructuredType type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(type, "type");
			return type.BaseType;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000545D File Offset: 0x0000365D
		public static IEnumerable<IEdmNavigationProperty> DeclaredNavigationProperties(this IEdmStructuredType type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(type, "type");
			return Enumerable.OfType<IEdmNavigationProperty>(type.DeclaredProperties);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005476 File Offset: 0x00003676
		public static IEnumerable<IEdmNavigationProperty> NavigationProperties(this IEdmStructuredType type)
		{
			EdmUtil.CheckArgumentNull<IEdmStructuredType>(type, "type");
			return Enumerable.OfType<IEdmNavigationProperty>(type.Properties());
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005490 File Offset: 0x00003690
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

		// Token: 0x06000115 RID: 277 RVA: 0x000054CC File Offset: 0x000036CC
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

		// Token: 0x06000116 RID: 278 RVA: 0x00005514 File Offset: 0x00003714
		public static void AddAlternateKeyAnnotation(this EdmModel model, IEdmEntityType type, IDictionary<string, IEdmProperty> alternateKey)
		{
			EdmUtil.CheckArgumentNull<EdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmEntityType>(type, "type");
			EdmUtil.CheckArgumentNull<IDictionary<string, IEdmProperty>>(alternateKey, "alternateKey");
			EdmCollectionExpression edmCollectionExpression = null;
			IEdmVocabularyAnnotation edmVocabularyAnnotation = Enumerable.FirstOrDefault<IEdmVocabularyAnnotation>(model.FindVocabularyAnnotations(type, AlternateKeysVocabularyModel.AlternateKeysTerm));
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

		// Token: 0x06000117 RID: 279 RVA: 0x00005674 File Offset: 0x00003874
		public static bool HasDeclaredKeyProperty(this IEdmEntityType entityType, IEdmProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(entityType, "entityType");
			EdmUtil.CheckArgumentNull<IEdmProperty>(property, "property");
			while (entityType != null)
			{
				if (entityType.DeclaredKey != null && Enumerable.Any<IEdmStructuralProperty>(entityType.DeclaredKey, (IEdmStructuralProperty k) => k == property))
				{
					return true;
				}
				entityType = entityType.BaseEntityType();
			}
			return false;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000056DC File Offset: 0x000038DC
		public static IEdmEntityType EntityDefinition(this IEdmEntityTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			return (IEdmEntityType)type.Definition;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000056F5 File Offset: 0x000038F5
		public static IEdmEntityType BaseEntityType(this IEdmEntityTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			return type.EntityDefinition().BaseEntityType();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000570E File Offset: 0x0000390E
		public static IEnumerable<IEdmStructuralProperty> Key(this IEdmEntityTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityTypeReference>(type, "type");
			return type.EntityDefinition().Key();
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005727 File Offset: 0x00003927
		public static IEdmComplexType BaseComplexType(this IEdmComplexType type)
		{
			EdmUtil.CheckArgumentNull<IEdmComplexType>(type, "type");
			return type.BaseType as IEdmComplexType;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005740 File Offset: 0x00003940
		public static IEdmComplexType ComplexDefinition(this IEdmComplexTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmComplexTypeReference>(type, "type");
			return (IEdmComplexType)type.Definition;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005759 File Offset: 0x00003959
		public static IEdmComplexType BaseComplexType(this IEdmComplexTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmComplexTypeReference>(type, "type");
			return type.ComplexDefinition().BaseComplexType();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005772 File Offset: 0x00003972
		public static IEdmEntityReferenceType EntityReferenceDefinition(this IEdmEntityReferenceTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityReferenceTypeReference>(type, "type");
			return (IEdmEntityReferenceType)type.Definition;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000578B File Offset: 0x0000398B
		public static IEdmEntityType EntityType(this IEdmEntityReferenceTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityReferenceTypeReference>(type, "type");
			return type.EntityReferenceDefinition().EntityType;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000057A4 File Offset: 0x000039A4
		public static IEdmCollectionType CollectionDefinition(this IEdmCollectionTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmCollectionTypeReference>(type, "type");
			return (IEdmCollectionType)type.Definition;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000057BD File Offset: 0x000039BD
		public static IEdmTypeReference ElementType(this IEdmCollectionTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmCollectionTypeReference>(type, "type");
			return type.CollectionDefinition().ElementType;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000057D6 File Offset: 0x000039D6
		public static IEdmEnumType EnumDefinition(this IEdmEnumTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmEnumTypeReference>(type, "type");
			return (IEdmEnumType)type.Definition;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000057EF File Offset: 0x000039EF
		public static IEdmTypeDefinition TypeDefinition(this IEdmTypeDefinitionReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeDefinitionReference>(type, "type");
			return (IEdmTypeDefinition)type.Definition;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005808 File Offset: 0x00003A08
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

		// Token: 0x06000125 RID: 293 RVA: 0x0000583D File Offset: 0x00003A3D
		public static IEdmEntityType ToEntityType(this IEdmNavigationProperty property)
		{
			return property.Type.Definition.AsElementType() as IEdmEntityType;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005854 File Offset: 0x00003A54
		public static IEdmStructuredType ToStructuredType(this IEdmTypeReference propertyTypeReference)
		{
			IEdmType edmType = propertyTypeReference.Definition;
			if (edmType.TypeKind == EdmTypeKind.Collection)
			{
				edmType = ((IEdmCollectionType)edmType).ElementType.Definition;
			}
			return edmType as IEdmStructuredType;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005888 File Offset: 0x00003A88
		public static IEdmEntityType DeclaringEntityType(this IEdmNavigationProperty property)
		{
			return (IEdmEntityType)property.DeclaringType;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005895 File Offset: 0x00003A95
		public static bool IsPrincipal(this IEdmNavigationProperty navigationProperty)
		{
			return navigationProperty.ReferentialConstraint == null && navigationProperty.Partner != null && navigationProperty.Partner.ReferentialConstraint != null;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000058B7 File Offset: 0x00003AB7
		public static IEnumerable<IEdmStructuralProperty> DependentProperties(this IEdmNavigationProperty navigationProperty)
		{
			if (navigationProperty.ReferentialConstraint != null)
			{
				return Enumerable.Select<EdmReferentialConstraintPropertyPair, IEdmStructuralProperty>(navigationProperty.ReferentialConstraint.PropertyPairs, (EdmReferentialConstraintPropertyPair p) => p.DependentProperty);
			}
			return null;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000058F2 File Offset: 0x00003AF2
		public static IEnumerable<IEdmStructuralProperty> PrincipalProperties(this IEdmNavigationProperty navigationProperty)
		{
			if (navigationProperty.ReferentialConstraint != null)
			{
				return Enumerable.Select<EdmReferentialConstraintPropertyPair, IEdmStructuralProperty>(navigationProperty.ReferentialConstraint.PropertyPairs, (EdmReferentialConstraintPropertyPair p) => p.PrincipalProperty);
			}
			return null;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000592D File Offset: 0x00003B2D
		public static IEdmTerm Term(this IEdmVocabularyAnnotation annotation)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			return annotation.Term;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005944 File Offset: 0x00003B44
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

		// Token: 0x0600012D RID: 301 RVA: 0x000059B8 File Offset: 0x00003BB8
		public static bool IsActionImport(this IEdmOperationImport operationImport)
		{
			return operationImport.ContainerElementKind == EdmContainerElementKind.ActionImport;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000059C3 File Offset: 0x00003BC3
		public static bool IsFunctionImport(this IEdmOperationImport operationImport)
		{
			return operationImport.ContainerElementKind == EdmContainerElementKind.FunctionImport;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000059D0 File Offset: 0x00003BD0
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

		// Token: 0x06000130 RID: 304 RVA: 0x000059FC File Offset: 0x00003BFC
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

		// Token: 0x06000131 RID: 305 RVA: 0x00005A70 File Offset: 0x00003C70
		public static bool IsAction(this IEdmOperation operation)
		{
			return operation.SchemaElementKind == EdmSchemaElementKind.Action;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00005A7B File Offset: 0x00003C7B
		public static bool IsFunction(this IEdmOperation operation)
		{
			return operation.SchemaElementKind == EdmSchemaElementKind.Function;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005A88 File Offset: 0x00003C88
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

		// Token: 0x06000134 RID: 308 RVA: 0x00005AFC File Offset: 0x00003CFC
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

		// Token: 0x06000135 RID: 309 RVA: 0x00005B9C File Offset: 0x00003D9C
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

		// Token: 0x06000136 RID: 310 RVA: 0x00005BF8 File Offset: 0x00003DF8
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

		// Token: 0x06000137 RID: 311 RVA: 0x00005C23 File Offset: 0x00003E23
		public static string FullNavigationSourceName(this IEdmNavigationSource navigationSource)
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationSource>(navigationSource, "navigationSource");
			return string.Join(".", Enumerable.ToArray<string>(navigationSource.Path.PathSegments));
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005C4C File Offset: 0x00003E4C
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

		// Token: 0x06000139 RID: 313 RVA: 0x00005CB2 File Offset: 0x00003EB2
		public static void SetEdmReferences(this IEdmModel model, IEnumerable<IEdmReference> edmReferences)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			model.SetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "References", edmReferences);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005CD2 File Offset: 0x00003ED2
		public static IEnumerable<IEdmReference> GetEdmReferences(this IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			return (IEnumerable<IEdmReference>)model.GetAnnotationValue(model, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "References");
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005CF8 File Offset: 0x00003EF8
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

		// Token: 0x0600013C RID: 316 RVA: 0x00005D4B File Offset: 0x00003F4B
		internal static IEnumerable<IEdmOperation> FindOperationsInModelTree(this CsdlSemanticsModel model, string name)
		{
			return model.FindInModelTree(ExtensionMethods.findOperations, name, ExtensionMethods.mergeFunctions);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005D5E File Offset: 0x00003F5E
		internal static IEdmSchemaType FindTypeInModelTree(this CsdlSemanticsModel model, string name)
		{
			return model.FindInModelTree(ExtensionMethods.findType, name, new Func<IEdmSchemaType, IEdmSchemaType, IEdmSchemaType>(RegistrationHelper.CreateAmbiguousTypeBinding));
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005D78 File Offset: 0x00003F78
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

		// Token: 0x0600013F RID: 319 RVA: 0x00005ECC File Offset: 0x000040CC
		internal static bool TryGetRelativeEntitySetPath(IEdmElement element, Collection<EdmError> foundErrors, IEdmPathExpression pathExpression, IEdmModel model, IEnumerable<IEdmOperationParameter> parameters, out IEdmOperationParameter parameter, out Dictionary<IEdmNavigationProperty, IEdmPathExpression> relativeNavigations, out IEdmEntityType lastEntityType)
		{
			parameter = null;
			relativeNavigations = null;
			lastEntityType = null;
			List<string> list = Enumerable.ToList<string>(pathExpression.PathSegments);
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
			foreach (string text2 in Enumerable.Skip<string>(list, 1))
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

		// Token: 0x06000140 RID: 320 RVA: 0x000061C4 File Offset: 0x000043C4
		internal static IEdmEntityType GetPathSegmentEntityType(IEdmTypeReference segmentType)
		{
			return (segmentType.IsCollection() ? segmentType.AsCollection().ElementType() : segmentType).AsEntity().EntityDefinition();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000061E6 File Offset: 0x000043E6
		internal static IEdmDocumentation GetDocumentation(this IEdmModel model, IEdmElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return (IEdmDocumentation)model.GetAnnotationValue(element, "http://schemas.microsoft.com/ado/2011/04/edm/documentation", "Documentation");
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006216 File Offset: 0x00004416
		internal static void SetDocumentation(this IEdmModel model, IEdmElement element, IEdmDocumentation documentation)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			model.SetAnnotationValue(element, "http://schemas.microsoft.com/ado/2011/04/edm/documentation", "Documentation", documentation);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006244 File Offset: 0x00004444
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

		// Token: 0x06000144 RID: 324 RVA: 0x00006298 File Offset: 0x00004498
		internal static IEdmEntitySet FindEntitySetExtended(this IEdmEntityContainer container, string qualifiedName)
		{
			return ExtensionMethods.FindInContainerAndExtendsRecursively<IEdmEntitySet>(container, qualifiedName, (IEdmEntityContainer c, string n) => c.FindEntitySet(n), 100);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000062C2 File Offset: 0x000044C2
		internal static IEdmSingleton FindSingletonExtended(this IEdmEntityContainer container, string qualifiedName)
		{
			return ExtensionMethods.FindInContainerAndExtendsRecursively<IEdmSingleton>(container, qualifiedName, (IEdmEntityContainer c, string n) => c.FindSingleton(n), 100);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000062EC File Offset: 0x000044EC
		internal static IEnumerable<IEdmOperationImport> FindOperationImportsExtended(this IEdmEntityContainer container, string qualifiedName)
		{
			return ExtensionMethods.FindInContainerAndExtendsRecursively<IEnumerable<IEdmOperationImport>>(container, qualifiedName, (IEdmEntityContainer c, string n) => c.FindOperationImports(n), 100);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006318 File Offset: 0x00004518
		internal static IPrimitiveValueConverter GetPrimitiveValueConverter(this IEdmModel model, IEdmType typeDefinition)
		{
			IPrimitiveValueConverter annotationValue = model.GetAnnotationValue(typeDefinition, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "PrimitiveValueConverterMap");
			if (annotationValue == null)
			{
				return PassThroughPrimitiveValueConverter.Instance;
			}
			return annotationValue;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006341 File Offset: 0x00004541
		internal static void SetPrimitiveValueConverter(this IEdmModel model, IEdmType typeDefinition, IPrimitiveValueConverter converter)
		{
			model.SetAnnotationValue(typeDefinition, "http://schemas.microsoft.com/ado/2011/04/edm/internal", "PrimitiveValueConverterMap", converter);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006358 File Offset: 0x00004558
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

		// Token: 0x0600014A RID: 330 RVA: 0x000063DC File Offset: 0x000045DC
		private static IEnumerable<IDictionary<string, IEdmProperty>> GetDeclaredAlternateKeysForType(IEdmEntityType type, IEdmModel model)
		{
			IEdmVocabularyAnnotation edmVocabularyAnnotation = Enumerable.FirstOrDefault<IEdmVocabularyAnnotation>(model.FindVocabularyAnnotations(type, AlternateKeysVocabularyModel.AlternateKeysTerm));
			if (edmVocabularyAnnotation != null)
			{
				List<IDictionary<string, IEdmProperty>> list = new List<IDictionary<string, IEdmProperty>>();
				IEdmCollectionExpression edmCollectionExpression = edmVocabularyAnnotation.Value as IEdmCollectionExpression;
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
							string text = Enumerable.FirstOrDefault<string>(((IEdmPathExpression)edmPropertyConstructor3.Value).PathSegments);
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

		// Token: 0x0600014B RID: 331 RVA: 0x000065AC File Offset: 0x000047AC
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

		// Token: 0x0600014C RID: 332 RVA: 0x00006624 File Offset: 0x00004824
		private static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmEntityType contextType, IEdmTerm term, string qualifier, Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T> evaluator)
		{
			IEnumerable<IEdmVocabularyAnnotation> enumerable = model.FindVocabularyAnnotations(contextType, term, qualifier);
			if (Enumerable.Count<IEdmVocabularyAnnotation>(enumerable) != 1)
			{
				throw new InvalidOperationException(Strings.Edm_Evaluator_NoValueAnnotationOnType(contextType.ToTraceString(), term.ToTraceString()));
			}
			return evaluator.Invoke(Enumerable.Single<IEdmVocabularyAnnotation>(enumerable).Value, context, term.Type);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006678 File Offset: 0x00004878
		private static T GetTermValue<T>(this IEdmModel model, IEdmStructuredValue context, IEdmEntityType contextType, string termName, string qualifier, Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T> evaluator)
		{
			IEnumerable<IEdmVocabularyAnnotation> enumerable = model.FindVocabularyAnnotations(contextType, termName, qualifier);
			if (Enumerable.Count<IEdmVocabularyAnnotation>(enumerable) != 1)
			{
				throw new InvalidOperationException(Strings.Edm_Evaluator_NoValueAnnotationOnType(contextType.ToTraceString(), termName));
			}
			IEdmVocabularyAnnotation edmVocabularyAnnotation = Enumerable.Single<IEdmVocabularyAnnotation>(enumerable);
			return evaluator.Invoke(edmVocabularyAnnotation.Value, context, edmVocabularyAnnotation.Term().Type);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000066CC File Offset: 0x000048CC
		private static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, IEdmTerm term, string qualifier, Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T> evaluator)
		{
			IEnumerable<IEdmVocabularyAnnotation> enumerable = model.FindVocabularyAnnotations(element, term, qualifier);
			if (Enumerable.Count<IEdmVocabularyAnnotation>(enumerable) != 1)
			{
				throw new InvalidOperationException(Strings.Edm_Evaluator_NoValueAnnotationOnElement(term.ToTraceString()));
			}
			return evaluator.Invoke(Enumerable.Single<IEdmVocabularyAnnotation>(enumerable).Value, null, term.Type);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00006718 File Offset: 0x00004918
		private static T GetTermValue<T>(this IEdmModel model, IEdmVocabularyAnnotatable element, string termName, string qualifier, Func<IEdmExpression, IEdmStructuredValue, IEdmTypeReference, T> evaluator)
		{
			IEnumerable<IEdmVocabularyAnnotation> enumerable = model.FindVocabularyAnnotations(element, termName, qualifier);
			if (Enumerable.Count<IEdmVocabularyAnnotation>(enumerable) != 1)
			{
				throw new InvalidOperationException(Strings.Edm_Evaluator_NoValueAnnotationOnElement(termName));
			}
			IEdmVocabularyAnnotation edmVocabularyAnnotation = Enumerable.Single<IEdmVocabularyAnnotation>(enumerable);
			return evaluator.Invoke(edmVocabularyAnnotation.Value, null, edmVocabularyAnnotation.Term().Type);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00006764 File Offset: 0x00004964
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

		// Token: 0x06000151 RID: 337 RVA: 0x000067E4 File Offset: 0x000049E4
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

		// Token: 0x06000152 RID: 338 RVA: 0x00006844 File Offset: 0x00004A44
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

		// Token: 0x06000153 RID: 339 RVA: 0x00006948 File Offset: 0x00004B48
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
			IEdmTerm changeTrackingTerm = CapabilitiesVocabularyModel.ChangeTrackingTerm;
			EdmVocabularyAnnotation edmVocabularyAnnotation = new EdmVocabularyAnnotation(target, changeTrackingTerm, edmRecordExpression);
			edmVocabularyAnnotation.SetSerializationLocation(model, new EdmVocabularyAnnotationSerializationLocation?(EdmVocabularyAnnotationSerializationLocation.Inline));
			model.SetVocabularyAnnotation(edmVocabularyAnnotation);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006A48 File Offset: 0x00004C48
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

		// Token: 0x0400001A RID: 26
		private const int ContainerExtendsMaxDepth = 100;

		// Token: 0x0400001B RID: 27
		private const string CollectionTypeFormat = "Collection({0})";

		// Token: 0x0400001C RID: 28
		private static readonly IEnumerable<IEdmStructuralProperty> EmptyStructuralProperties = new Collection<IEdmStructuralProperty>();

		// Token: 0x0400001D RID: 29
		private static readonly IEnumerable<IEdmNavigationProperty> EmptyNavigationProperties = new Collection<IEdmNavigationProperty>();

		// Token: 0x0400001E RID: 30
		private static readonly Func<IEdmModel, string, IEdmSchemaType> findType = (IEdmModel model, string qualifiedName) => model.FindDeclaredType(qualifiedName);

		// Token: 0x0400001F RID: 31
		private static readonly Func<IEdmModel, IEdmType, IEnumerable<IEdmOperation>> findBoundOperations = (IEdmModel model, IEdmType bindingType) => model.FindDeclaredBoundOperations(bindingType);

		// Token: 0x04000020 RID: 32
		private static readonly Func<IEdmModel, string, IEdmTerm> findTerm = (IEdmModel model, string qualifiedName) => model.FindDeclaredTerm(qualifiedName);

		// Token: 0x04000021 RID: 33
		private static readonly Func<IEdmModel, string, IEnumerable<IEdmOperation>> findOperations = (IEdmModel model, string qualifiedName) => model.FindDeclaredOperations(qualifiedName);

		// Token: 0x04000022 RID: 34
		private static readonly Func<IEdmModel, string, IEdmEntityContainer> findEntityContainer = delegate(IEdmModel model, string qualifiedName)
		{
			if (!model.ExistsContainer(qualifiedName))
			{
				return null;
			}
			return model.EntityContainer;
		};

		// Token: 0x04000023 RID: 35
		private static readonly Func<IEnumerable<IEdmOperation>, IEnumerable<IEdmOperation>, IEnumerable<IEdmOperation>> mergeFunctions = (IEnumerable<IEdmOperation> f1, IEnumerable<IEdmOperation> f2) => Enumerable.Concat<IEdmOperation>(f1, f2);

		// Token: 0x0200020E RID: 526
		internal static class TypeName<T>
		{
			// Token: 0x0400075C RID: 1884
			public static readonly string LocalName = typeof(T).ToString().Replace("_", "_____").Replace('.', '_')
				.Replace("[", "")
				.Replace("]", "")
				.Replace(",", "__")
				.Replace("`", "___")
				.Replace("+", "____");
		}
	}
}
