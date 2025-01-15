using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Internal;
using System.Web.Http.Metadata;
using System.Web.Http.Properties;
using System.Web.Http.Validation;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000075 RID: 117
	public class MutableObjectModelBinder : IModelBinder
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000895F File Offset: 0x00006B5F
		// (set) Token: 0x06000309 RID: 777 RVA: 0x00008967 File Offset: 0x00006B67
		internal ModelMetadataProvider MetadataProvider { private get; set; }

		// Token: 0x0600030A RID: 778 RVA: 0x00008970 File Offset: 0x00006B70
		internal static bool CanBindType(Type modelType)
		{
			return !TypeHelper.HasStringConverter(modelType) && !(modelType == typeof(ComplexModelDto));
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00008994 File Offset: 0x00006B94
		public virtual bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			ModelBindingHelper.ValidateBindingContext(bindingContext);
			if (!bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName))
			{
				return false;
			}
			if (!MutableObjectModelBinder.CanBindType(bindingContext.ModelType))
			{
				return false;
			}
			this.EnsureModel(actionContext, bindingContext);
			IEnumerable<ModelMetadata> metadataForProperties = this.GetMetadataForProperties(actionContext, bindingContext);
			ComplexModelDto complexModelDto = this.CreateAndPopulateDto(actionContext, bindingContext, metadataForProperties);
			this.ProcessDto(actionContext, bindingContext, complexModelDto);
			bindingContext.ValidationNode.ValidateAllProperties = true;
			return true;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000089FC File Offset: 0x00006BFC
		protected virtual bool CanUpdateProperty(ModelMetadata propertyMetadata)
		{
			return MutableObjectModelBinder.CanUpdatePropertyInternal(propertyMetadata);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00008A04 File Offset: 0x00006C04
		internal static bool CanUpdatePropertyInternal(ModelMetadata propertyMetadata)
		{
			return !propertyMetadata.IsReadOnly || MutableObjectModelBinder.CanUpdateReadOnlyProperty(propertyMetadata.ModelType);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00008A1B File Offset: 0x00006C1B
		private static bool CanUpdateReadOnlyProperty(Type propertyType)
		{
			return !propertyType.IsValueType && !propertyType.IsArray && !(propertyType == typeof(string));
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00008A48 File Offset: 0x00006C48
		private ComplexModelDto CreateAndPopulateDto(HttpActionContext actionContext, ModelBindingContext bindingContext, IEnumerable<ModelMetadata> propertyMetadatas)
		{
			ModelMetadataProvider modelMetadataProvider = this.MetadataProvider ?? actionContext.GetMetadataProvider();
			ComplexModelDto originalDto = new ComplexModelDto(bindingContext.ModelMetadata, propertyMetadatas);
			ModelBindingContext modelBindingContext = new ModelBindingContext(bindingContext)
			{
				ModelMetadata = modelMetadataProvider.GetMetadataForType(() => originalDto, typeof(ComplexModelDto)),
				ModelName = bindingContext.ModelName
			};
			actionContext.Bind(modelBindingContext);
			return (ComplexModelDto)modelBindingContext.Model;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00008AC6 File Offset: 0x00006CC6
		protected virtual object CreateModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			return Activator.CreateInstance(bindingContext.ModelType);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00008AD3 File Offset: 0x00006CD3
		internal static EventHandler<ModelValidatedEventArgs> CreateNullCheckFailedHandler(ModelMetadata modelMetadata, object incomingValue)
		{
			return delegate(object sender, ModelValidatedEventArgs e)
			{
				ModelValidationNode modelValidationNode = (ModelValidationNode)sender;
				ModelStateDictionary modelState = e.ActionContext.ModelState;
				if (modelState.IsValidField(modelValidationNode.ModelStateKey))
				{
					string text = ModelBinderConfig.ValueRequiredErrorMessageProvider(e.ActionContext, modelMetadata, incomingValue);
					if (text != null)
					{
						modelState.AddModelError(modelValidationNode.ModelStateKey, text);
					}
				}
			};
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00008AF3 File Offset: 0x00006CF3
		protected virtual void EnsureModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			if (bindingContext.Model == null)
			{
				bindingContext.ModelMetadata.Model = this.CreateModel(actionContext, bindingContext);
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00008B10 File Offset: 0x00006D10
		protected virtual IEnumerable<ModelMetadata> GetMetadataForProperties(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			HashSet<string> requiredProperties;
			HashSet<string> skipProperties;
			Dictionary<string, ModelValidator> dictionary;
			MutableObjectModelBinder.GetRequiredPropertiesCollection(actionContext, bindingContext, out requiredProperties, out dictionary, out skipProperties);
			return from propertyMetadata in bindingContext.ModelMetadata.Properties
				let propertyName = propertyMetadata.PropertyName
				let shouldUpdateProperty = requiredProperties.Contains(propertyName) || !skipProperties.Contains(propertyName)
				where shouldUpdateProperty && this.CanUpdateProperty(propertyMetadata)
				select propertyMetadata;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00008BB4 File Offset: 0x00006DB4
		private static object GetPropertyDefaultValue(PropertyDescriptor propertyDescriptor)
		{
			DefaultValueAttribute defaultValueAttribute = propertyDescriptor.Attributes.OfType<DefaultValueAttribute>().FirstOrDefault<DefaultValueAttribute>();
			if (defaultValueAttribute == null)
			{
				return null;
			}
			return defaultValueAttribute.Value;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00008BE0 File Offset: 0x00006DE0
		internal static void GetRequiredPropertiesCollection(HttpActionContext actionContext, ModelBindingContext bindingContext, out HashSet<string> requiredProperties, out Dictionary<string, ModelValidator> requiredValidators, out HashSet<string> skipProperties)
		{
			requiredProperties = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			requiredValidators = new Dictionary<string, ModelValidator>(StringComparer.OrdinalIgnoreCase);
			skipProperties = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			ICustomTypeDescriptor customTypeDescriptor = TypeDescriptorHelper.Get(bindingContext.ModelType);
			PropertyDescriptorCollection properties = customTypeDescriptor.GetProperties();
			HttpBindingBehaviorAttribute httpBindingBehaviorAttribute = customTypeDescriptor.GetAttributes().OfType<HttpBindingBehaviorAttribute>().SingleOrDefault<HttpBindingBehaviorAttribute>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				string name = propertyDescriptor.Name;
				ModelMetadata modelMetadata = bindingContext.PropertyMetadata[name];
				ModelValidator modelValidator = (from v in actionContext.GetValidators(modelMetadata)
					where v.IsRequired
					select v).FirstOrDefault<ModelValidator>();
				requiredValidators[name] = modelValidator;
				HttpBindingBehaviorAttribute httpBindingBehaviorAttribute2 = propertyDescriptor.Attributes.OfType<HttpBindingBehaviorAttribute>().SingleOrDefault<HttpBindingBehaviorAttribute>() ?? httpBindingBehaviorAttribute;
				if (httpBindingBehaviorAttribute2 != null)
				{
					HttpBindingBehavior behavior = httpBindingBehaviorAttribute2.Behavior;
					if (behavior != HttpBindingBehavior.Never)
					{
						if (behavior == HttpBindingBehavior.Required)
						{
							requiredProperties.Add(name);
						}
					}
					else
					{
						skipProperties.Add(name);
					}
				}
				else if (modelValidator != null)
				{
					requiredProperties.Add(name);
				}
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00008D20 File Offset: 0x00006F20
		internal void ProcessDto(HttpActionContext actionContext, ModelBindingContext bindingContext, ComplexModelDto dto)
		{
			HashSet<string> hashSet;
			Dictionary<string, ModelValidator> dictionary;
			HashSet<string> hashSet2;
			MutableObjectModelBinder.GetRequiredPropertiesCollection(actionContext, bindingContext, out hashSet, out dictionary, out hashSet2);
			hashSet.ExceptWith(dto.Results.Select((KeyValuePair<ModelMetadata, ComplexModelDtoResult> r) => r.Key.PropertyName));
			foreach (string text in hashSet)
			{
				string text2 = ModelBindingHelper.CreatePropertyModelName(bindingContext.ValidationNode.ModelStateKey, text);
				ModelMetadata modelMetadata = bindingContext.PropertyMetadata[text];
				modelMetadata.Model = null;
				if (!MutableObjectModelBinder.RunValidator(dictionary[text], bindingContext, modelMetadata, text2))
				{
					bindingContext.ModelState.AddModelError(text2, Error.Format(SRResources.MissingRequiredMember, new object[] { text }));
				}
			}
			foreach (KeyValuePair<ModelMetadata, ComplexModelDtoResult> keyValuePair in dto.Results)
			{
				ModelMetadata key = keyValuePair.Key;
				ComplexModelDtoResult value = keyValuePair.Value;
				if (value != null)
				{
					this.SetProperty(actionContext, bindingContext, key, value, dictionary[key.PropertyName]);
					bindingContext.ValidationNode.ChildNodes.Add(value.ValidationNode);
				}
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00008E88 File Offset: 0x00007088
		protected virtual void SetProperty(HttpActionContext actionContext, ModelBindingContext bindingContext, ModelMetadata propertyMetadata, ComplexModelDtoResult dtoResult, ModelValidator requiredValidator)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptorHelper.Get(bindingContext.ModelType).GetProperties().Find(propertyMetadata.PropertyName, true);
			if (propertyDescriptor == null || propertyDescriptor.IsReadOnly)
			{
				return;
			}
			object obj = dtoResult.Model ?? MutableObjectModelBinder.GetPropertyDefaultValue(propertyDescriptor);
			propertyMetadata.Model = obj;
			if (obj == null)
			{
				string modelStateKey = dtoResult.ValidationNode.ModelStateKey;
				if (bindingContext.ModelState.IsValidField(modelStateKey))
				{
					MutableObjectModelBinder.RunValidator(requiredValidator, bindingContext, propertyMetadata, modelStateKey);
				}
			}
			if (obj != null || TypeHelper.TypeAllowsNullValue(propertyDescriptor.PropertyType))
			{
				try
				{
					propertyDescriptor.SetValue(bindingContext.Model, obj);
					return;
				}
				catch (Exception ex)
				{
					string modelStateKey2 = dtoResult.ValidationNode.ModelStateKey;
					if (bindingContext.ModelState.IsValidField(modelStateKey2))
					{
						bindingContext.ModelState.AddModelError(modelStateKey2, ex);
					}
					return;
				}
			}
			string modelStateKey3 = dtoResult.ValidationNode.ModelStateKey;
			if (bindingContext.ModelState.IsValidField(modelStateKey3))
			{
				dtoResult.ValidationNode.Validated += MutableObjectModelBinder.CreateNullCheckFailedHandler(propertyMetadata, obj);
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00008F90 File Offset: 0x00007190
		private static bool RunValidator(ModelValidator validator, ModelBindingContext bindingContext, ModelMetadata propertyMetadata, string modelStateKey)
		{
			bool flag = false;
			if (validator != null)
			{
				foreach (ModelValidationResult modelValidationResult in validator.Validate(propertyMetadata, bindingContext.Model))
				{
					bindingContext.ModelState.AddModelError(modelStateKey, modelValidationResult.Message);
					flag = true;
				}
			}
			return flag;
		}
	}
}
