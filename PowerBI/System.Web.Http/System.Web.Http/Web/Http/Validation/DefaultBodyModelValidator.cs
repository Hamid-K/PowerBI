using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Runtime.CompilerServices;
using System.Web.Http.Controllers;
using System.Web.Http.Internal;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;

namespace System.Web.Http.Validation
{
	// Token: 0x0200008E RID: 142
	public class DefaultBodyModelValidator : IBodyModelValidator
	{
		// Token: 0x06000378 RID: 888 RVA: 0x0000A058 File Offset: 0x00008258
		public bool Validate(object model, Type type, ModelMetadataProvider metadataProvider, HttpActionContext actionContext, string keyPrefix)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (metadataProvider == null)
			{
				throw Error.ArgumentNull("metadataProvider");
			}
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			if (model != null && !this.ShouldValidateType(model.GetType()))
			{
				return true;
			}
			ModelValidatorProvider[] array = actionContext.GetValidatorProviders().ToArray<ModelValidatorProvider>();
			if (array == null || array.Length == 0)
			{
				return true;
			}
			ModelMetadata metadataForType = metadataProvider.GetMetadataForType(() => model, type);
			BodyModelValidatorContext bodyModelValidatorContext = new BodyModelValidatorContext(actionContext.ModelState)
			{
				MetadataProvider = metadataProvider,
				ActionContext = actionContext,
				ValidatorCache = actionContext.GetValidatorCache(),
				RootPrefix = keyPrefix
			};
			return this.ValidateNodeAndChildren(metadataForType, bodyModelValidatorContext, null, null);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000A125 File Offset: 0x00008325
		public virtual bool ShouldValidateType(Type type)
		{
			return !MediaTypeFormatterCollection.IsTypeExcludedFromValidation(type);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000A130 File Offset: 0x00008330
		protected virtual bool ValidateNodeAndChildren(ModelMetadata metadata, BodyModelValidatorContext validationContext, object container, IEnumerable<ModelValidator> validators)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			if (metadata == null)
			{
				throw Error.ArgumentNull("metadata");
			}
			if (validationContext == null)
			{
				throw Error.ArgumentNull("validationContext");
			}
			object obj = null;
			try
			{
				obj = metadata.Model;
			}
			catch
			{
				return true;
			}
			if (validators == null)
			{
				validators = validationContext.ActionContext.GetValidators(metadata, validationContext.ValidatorCache);
			}
			if (obj == null)
			{
				return this.ShallowValidate(metadata, validationContext, container, validators);
			}
			Type type = obj.GetType();
			if (TypeHelper.IsSimpleType(type) || !this.ShouldValidateType(type))
			{
				return this.ShallowValidate(metadata, validationContext, container, validators);
			}
			if (validationContext.Visited.Contains(obj))
			{
				return true;
			}
			validationContext.Visited.Add(obj);
			IEnumerable enumerable = obj as IEnumerable;
			bool flag;
			if (enumerable == null)
			{
				flag = this.ValidateProperties(metadata, validationContext);
			}
			else
			{
				flag = this.ValidateElements(enumerable, validationContext);
			}
			if (flag)
			{
				flag = this.ShallowValidate(metadata, validationContext, container, validators);
			}
			validationContext.Visited.Remove(obj);
			return flag;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000A22C File Offset: 0x0000842C
		protected virtual bool ValidateProperties(ModelMetadata metadata, BodyModelValidatorContext validationContext)
		{
			if (metadata == null)
			{
				throw Error.ArgumentNull("metadata");
			}
			if (validationContext == null)
			{
				throw Error.ArgumentNull("validationContext");
			}
			bool flag = true;
			DefaultBodyModelValidator.PropertyScope propertyScope = new DefaultBodyModelValidator.PropertyScope();
			validationContext.KeyBuilders.Push(propertyScope);
			foreach (ModelMetadata modelMetadata in validationContext.MetadataProvider.GetMetadataForProperties(metadata.Model, metadata.RealModelType))
			{
				propertyScope.PropertyName = modelMetadata.PropertyName;
				if (!this.ValidateNodeAndChildren(modelMetadata, validationContext, metadata.Model, null))
				{
					flag = false;
				}
			}
			validationContext.KeyBuilders.Pop();
			return flag;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000A2E0 File Offset: 0x000084E0
		protected virtual bool ValidateElements(IEnumerable model, BodyModelValidatorContext validationContext)
		{
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			if (validationContext == null)
			{
				throw Error.ArgumentNull("validationContext");
			}
			bool flag = true;
			Type elementType = DefaultBodyModelValidator.GetElementType(model.GetType());
			ModelMetadata metadataForType = validationContext.MetadataProvider.GetMetadataForType(null, elementType);
			DefaultBodyModelValidator.ElementScope elementScope = new DefaultBodyModelValidator.ElementScope
			{
				Index = 0
			};
			validationContext.KeyBuilders.Push(elementScope);
			IEnumerable<ModelValidator> validators = validationContext.ActionContext.GetValidators(metadataForType, validationContext.ValidatorCache);
			bool flag2 = validators.Any<ModelValidator>();
			foreach (object obj in model)
			{
				if (obj != null || flag2)
				{
					metadataForType.Model = obj;
					if (!this.ValidateNodeAndChildren(metadataForType, validationContext, model, validators))
					{
						flag = false;
					}
				}
				DefaultBodyModelValidator.ElementScope elementScope2 = elementScope;
				int index = elementScope2.Index;
				elementScope2.Index = index + 1;
			}
			validationContext.KeyBuilders.Pop();
			return flag;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000A3E0 File Offset: 0x000085E0
		protected virtual bool ShallowValidate(ModelMetadata metadata, BodyModelValidatorContext validationContext, object container, IEnumerable<ModelValidator> validators)
		{
			if (metadata == null)
			{
				throw Error.ArgumentNull("metadata");
			}
			if (validationContext == null)
			{
				throw Error.ArgumentNull("validationContext");
			}
			if (validators == null)
			{
				throw Error.ArgumentNull("validators");
			}
			bool flag = true;
			string text = null;
			ICollection collection = validators as ICollection;
			if (collection != null && collection.Count == 0)
			{
				return flag;
			}
			foreach (ModelValidator modelValidator in validators)
			{
				foreach (ModelValidationResult modelValidationResult in modelValidator.Validate(metadata, container))
				{
					if (text == null)
					{
						text = validationContext.RootPrefix;
						foreach (IBodyModelValidatorKeyBuilder bodyModelValidatorKeyBuilder in validationContext.KeyBuilders.Reverse<IBodyModelValidatorKeyBuilder>())
						{
							text = bodyModelValidatorKeyBuilder.AppendTo(text);
						}
					}
					string text2 = ModelBindingHelper.CreatePropertyModelName(text, modelValidationResult.MemberName);
					validationContext.ModelState.AddModelError(text2, modelValidationResult.Message);
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000A51C File Offset: 0x0000871C
		private static Type GetElementType(Type type)
		{
			if (type.IsArray)
			{
				return type.GetElementType();
			}
			foreach (Type type2 in type.GetInterfaces())
			{
				if (type2.IsGenericType && type2.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				{
					return type2.GetGenericArguments()[0];
				}
			}
			return typeof(object);
		}

		// Token: 0x020001BB RID: 443
		private class PropertyScope : IBodyModelValidatorKeyBuilder
		{
			// Token: 0x170002EE RID: 750
			// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0001BF7A File Offset: 0x0001A17A
			// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x0001BF82 File Offset: 0x0001A182
			public string PropertyName { get; set; }

			// Token: 0x06000AD4 RID: 2772 RVA: 0x0001BF8B File Offset: 0x0001A18B
			public string AppendTo(string prefix)
			{
				return ModelBindingHelper.CreatePropertyModelName(prefix, this.PropertyName);
			}
		}

		// Token: 0x020001BC RID: 444
		private class ElementScope : IBodyModelValidatorKeyBuilder
		{
			// Token: 0x170002EF RID: 751
			// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0001BF99 File Offset: 0x0001A199
			// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x0001BFA1 File Offset: 0x0001A1A1
			public int Index { get; set; }

			// Token: 0x06000AD8 RID: 2776 RVA: 0x0001BFAA File Offset: 0x0001A1AA
			public string AppendTo(string prefix)
			{
				return ModelBindingHelper.CreateIndexModelName(prefix, this.Index);
			}
		}
	}
}
