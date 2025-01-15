using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Http.Metadata;
using System.Web.Http.Properties;
using System.Web.Http.Validation.Validators;

namespace System.Web.Http.Validation.Providers
{
	// Token: 0x020000A0 RID: 160
	public class DataAnnotationsModelValidatorProvider : AssociatedValidatorProvider
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x0000B098 File Offset: 0x00009298
		protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders, IEnumerable<Attribute> attributes)
		{
			List<ModelValidator> list = new List<ModelValidator>();
			foreach (ValidationAttribute validationAttribute in attributes.OfType<ValidationAttribute>())
			{
				DataAnnotationsModelValidationFactory defaultAttributeFactory;
				if (!this.AttributeFactories.TryGetValue(validationAttribute.GetType(), out defaultAttributeFactory))
				{
					defaultAttributeFactory = this.DefaultAttributeFactory;
				}
				list.Add(defaultAttributeFactory(validatorProviders, validationAttribute));
			}
			if (typeof(IValidatableObject).IsAssignableFrom(metadata.ModelType))
			{
				DataAnnotationsValidatableObjectAdapterFactory defaultValidatableFactory;
				if (!this.ValidatableFactories.TryGetValue(metadata.ModelType, out defaultValidatableFactory))
				{
					defaultValidatableFactory = this.DefaultValidatableFactory;
				}
				list.Add(defaultValidatableFactory(validatorProviders));
			}
			return list;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000B154 File Offset: 0x00009354
		public void RegisterAdapter(Type attributeType, Type adapterType)
		{
			DataAnnotationsModelValidatorProvider.ValidateAttributeType(attributeType);
			DataAnnotationsModelValidatorProvider.ValidateAttributeAdapterType(adapterType);
			ConstructorInfo constructor = DataAnnotationsModelValidatorProvider.GetAttributeAdapterConstructor(attributeType, adapterType);
			this.AttributeFactories[attributeType] = (IEnumerable<ModelValidatorProvider> context, ValidationAttribute attribute) => (ModelValidator)constructor.Invoke(new object[] { context, attribute });
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000B198 File Offset: 0x00009398
		public void RegisterAdapterFactory(Type attributeType, DataAnnotationsModelValidationFactory factory)
		{
			DataAnnotationsModelValidatorProvider.ValidateAttributeType(attributeType);
			DataAnnotationsModelValidatorProvider.ValidateAttributeFactory(factory);
			this.AttributeFactories[attributeType] = factory;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000B1B4 File Offset: 0x000093B4
		public void RegisterDefaultAdapter(Type adapterType)
		{
			DataAnnotationsModelValidatorProvider.ValidateAttributeAdapterType(adapterType);
			ConstructorInfo constructor = DataAnnotationsModelValidatorProvider.GetAttributeAdapterConstructor(typeof(ValidationAttribute), adapterType);
			this.DefaultAttributeFactory = (IEnumerable<ModelValidatorProvider> context, ValidationAttribute attribute) => (ModelValidator)constructor.Invoke(new object[] { context, attribute });
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000B1F5 File Offset: 0x000093F5
		public void RegisterDefaultAdapterFactory(DataAnnotationsModelValidationFactory factory)
		{
			DataAnnotationsModelValidatorProvider.ValidateAttributeFactory(factory);
			this.DefaultAttributeFactory = factory;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000B204 File Offset: 0x00009404
		private static ConstructorInfo GetAttributeAdapterConstructor(Type attributeType, Type adapterType)
		{
			ConstructorInfo constructor = adapterType.GetConstructor(new Type[]
			{
				typeof(IEnumerable<ModelValidatorProvider>),
				attributeType
			});
			if (constructor == null)
			{
				throw Error.Argument("adapterType", SRResources.DataAnnotationsModelValidatorProvider_ConstructorRequirements, new object[]
				{
					adapterType.Name,
					typeof(ModelMetadata).Name,
					"IEnumerable<" + typeof(ModelValidatorProvider).Name + ">",
					attributeType.Name
				});
			}
			return constructor;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000B294 File Offset: 0x00009494
		private static void ValidateAttributeAdapterType(Type adapterType)
		{
			if (adapterType == null)
			{
				throw Error.ArgumentNull("adapterType");
			}
			if (!typeof(ModelValidator).IsAssignableFrom(adapterType))
			{
				throw Error.Argument("adapterType", SRResources.Common_TypeMustDriveFromType, new object[]
				{
					adapterType.Name,
					typeof(ModelValidator).Name
				});
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000B2F8 File Offset: 0x000094F8
		private static void ValidateAttributeType(Type attributeType)
		{
			if (attributeType == null)
			{
				throw Error.ArgumentNull("attributeType");
			}
			if (!typeof(ValidationAttribute).IsAssignableFrom(attributeType))
			{
				throw Error.Argument("attributeType", SRResources.Common_TypeMustDriveFromType, new object[]
				{
					attributeType.Name,
					typeof(ValidationAttribute).Name
				});
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000B35C File Offset: 0x0000955C
		private static void ValidateAttributeFactory(DataAnnotationsModelValidationFactory factory)
		{
			if (factory == null)
			{
				throw Error.ArgumentNull("factory");
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000B36C File Offset: 0x0000956C
		public void RegisterValidatableObjectAdapter(Type modelType, Type adapterType)
		{
			DataAnnotationsModelValidatorProvider.ValidateValidatableModelType(modelType);
			DataAnnotationsModelValidatorProvider.ValidateValidatableAdapterType(adapterType);
			ConstructorInfo constructor = DataAnnotationsModelValidatorProvider.GetValidatableAdapterConstructor(adapterType);
			this.ValidatableFactories[modelType] = (IEnumerable<ModelValidatorProvider> context) => (ModelValidator)constructor.Invoke(new object[] { context });
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000B3AF File Offset: 0x000095AF
		public void RegisterValidatableObjectAdapterFactory(Type modelType, DataAnnotationsValidatableObjectAdapterFactory factory)
		{
			DataAnnotationsModelValidatorProvider.ValidateValidatableModelType(modelType);
			DataAnnotationsModelValidatorProvider.ValidateValidatableFactory(factory);
			this.ValidatableFactories[modelType] = factory;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000B3CC File Offset: 0x000095CC
		public void RegisterDefaultValidatableObjectAdapter(Type adapterType)
		{
			DataAnnotationsModelValidatorProvider.ValidateValidatableAdapterType(adapterType);
			ConstructorInfo constructor = DataAnnotationsModelValidatorProvider.GetValidatableAdapterConstructor(adapterType);
			this.DefaultValidatableFactory = (IEnumerable<ModelValidatorProvider> context) => (ModelValidator)constructor.Invoke(new object[] { context });
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000B403 File Offset: 0x00009603
		public void RegisterDefaultValidatableObjectAdapterFactory(DataAnnotationsValidatableObjectAdapterFactory factory)
		{
			DataAnnotationsModelValidatorProvider.ValidateValidatableFactory(factory);
			this.DefaultValidatableFactory = factory;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000B414 File Offset: 0x00009614
		private static ConstructorInfo GetValidatableAdapterConstructor(Type adapterType)
		{
			ConstructorInfo constructor = adapterType.GetConstructor(new Type[] { typeof(IEnumerable<ModelValidatorProvider>) });
			if (constructor == null)
			{
				throw Error.Argument("adapterType", SRResources.DataAnnotationsModelValidatorProvider_ValidatableConstructorRequirements, new object[]
				{
					adapterType.Name,
					typeof(ModelMetadata).Name,
					"IEnumerable<" + typeof(ModelValidatorProvider).Name + ">"
				});
			}
			return constructor;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000B498 File Offset: 0x00009698
		private static void ValidateValidatableAdapterType(Type adapterType)
		{
			if (adapterType == null)
			{
				throw Error.ArgumentNull("adapterType");
			}
			if (!typeof(ModelValidator).IsAssignableFrom(adapterType))
			{
				throw Error.Argument("adapterType", SRResources.Common_TypeMustDriveFromType, new object[]
				{
					adapterType.Name,
					typeof(ModelValidator).Name
				});
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000B4FC File Offset: 0x000096FC
		private static void ValidateValidatableModelType(Type modelType)
		{
			if (modelType == null)
			{
				throw Error.ArgumentNull("modelType");
			}
			if (!typeof(IValidatableObject).IsAssignableFrom(modelType))
			{
				throw Error.Argument("modelType", SRResources.Common_TypeMustDriveFromType, new object[]
				{
					modelType.Name,
					typeof(IValidatableObject).Name
				});
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000B35C File Offset: 0x0000955C
		private static void ValidateValidatableFactory(DataAnnotationsValidatableObjectAdapterFactory factory)
		{
			if (factory == null)
			{
				throw Error.ArgumentNull("factory");
			}
		}

		// Token: 0x040000E1 RID: 225
		internal DataAnnotationsModelValidationFactory DefaultAttributeFactory = (IEnumerable<ModelValidatorProvider> validationProviders, ValidationAttribute attribute) => new DataAnnotationsModelValidator(validationProviders, attribute);

		// Token: 0x040000E2 RID: 226
		internal Dictionary<Type, DataAnnotationsModelValidationFactory> AttributeFactories = new Dictionary<Type, DataAnnotationsModelValidationFactory>();

		// Token: 0x040000E3 RID: 227
		internal DataAnnotationsValidatableObjectAdapterFactory DefaultValidatableFactory = (IEnumerable<ModelValidatorProvider> validationProviders) => new ValidatableObjectAdapter(validationProviders);

		// Token: 0x040000E4 RID: 228
		internal Dictionary<Type, DataAnnotationsValidatableObjectAdapterFactory> ValidatableFactories = new Dictionary<Type, DataAnnotationsValidatableObjectAdapterFactory>();
	}
}
