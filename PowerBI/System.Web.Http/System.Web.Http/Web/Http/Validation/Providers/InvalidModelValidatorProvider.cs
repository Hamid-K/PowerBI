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
	// Token: 0x020000A2 RID: 162
	public class InvalidModelValidatorProvider : AssociatedValidatorProvider
	{
		// Token: 0x060003EE RID: 1006 RVA: 0x0000B66E File Offset: 0x0000986E
		protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, IEnumerable<ModelValidatorProvider> validatorProviders, IEnumerable<Attribute> attributes)
		{
			if (metadata.ContainerType == null || string.IsNullOrEmpty(metadata.PropertyName))
			{
				Type type = metadata.ModelType;
				PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic);
				foreach (PropertyInfo propertyInfo in properties)
				{
					if (propertyInfo.GetCustomAttributes(typeof(ValidationAttribute), true).Length != 0)
					{
						yield return new ErrorModelValidator(validatorProviders, Error.Format(SRResources.ValidationAttributeOnNonPublicProperty, new object[] { propertyInfo.Name, type }));
					}
				}
				PropertyInfo[] array = null;
				FieldInfo[] fields = metadata.ModelType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				foreach (FieldInfo fieldInfo in fields)
				{
					if (fieldInfo.GetCustomAttributes(typeof(ValidationAttribute), true).Length != 0)
					{
						yield return new ErrorModelValidator(validatorProviders, Error.Format(SRResources.ValidationAttributeOnField, new object[] { fieldInfo.Name, type }));
					}
				}
				FieldInfo[] array2 = null;
				type = null;
			}
			else if (metadata.ModelType.IsValueType)
			{
				if (attributes.Any((Attribute attribute) => attribute is RequiredAttribute) && !DataMemberModelValidatorProvider.IsRequiredDataMember(metadata.ContainerType, attributes))
				{
					yield return new ErrorModelValidator(validatorProviders, Error.Format(SRResources.MissingDataMemberIsRequired, new object[] { metadata.PropertyName, metadata.ContainerType }));
				}
			}
			yield break;
		}
	}
}
