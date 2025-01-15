using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web.Http.Metadata;

namespace System.Web.Http.Validation.Validators
{
	// Token: 0x0200009C RID: 156
	public class DataAnnotationsModelValidator : ModelValidator
	{
		// Token: 0x060003C2 RID: 962 RVA: 0x0000AE52 File Offset: 0x00009052
		public DataAnnotationsModelValidator(IEnumerable<ModelValidatorProvider> validatorProviders, ValidationAttribute attribute)
			: base(validatorProviders)
		{
			if (attribute == null)
			{
				throw Error.ArgumentNull("attribute");
			}
			this.Attribute = attribute;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000AE70 File Offset: 0x00009070
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000AE77 File Offset: 0x00009077
		internal static bool UseLegacyValidationMemberName
		{
			get
			{
				return DataAnnotationsModelValidator._useLegacyValidationMemberName;
			}
			set
			{
				DataAnnotationsModelValidator._useLegacyValidationMemberName = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000AE7F File Offset: 0x0000907F
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x0000AE87 File Offset: 0x00009087
		protected internal ValidationAttribute Attribute { get; private set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000AE90 File Offset: 0x00009090
		public override bool IsRequired
		{
			get
			{
				return this.Attribute is RequiredAttribute;
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000AEA0 File Offset: 0x000090A0
		public override IEnumerable<ModelValidationResult> Validate(ModelMetadata metadata, object container)
		{
			string text;
			if (DataAnnotationsModelValidator._useLegacyValidationMemberName)
			{
				text = metadata.GetDisplayName();
			}
			else
			{
				text = metadata.PropertyName ?? metadata.ModelType.Name;
			}
			ValidationContext validationContext = new ValidationContext(container ?? metadata.Model)
			{
				DisplayName = metadata.GetDisplayName(),
				MemberName = text
			};
			ValidationResult validationResult = this.Attribute.GetValidationResult(metadata.Model, validationContext);
			if (validationResult != ValidationResult.Success)
			{
				string text2 = validationResult.MemberNames.FirstOrDefault<string>();
				if (string.Equals(text2, text, StringComparison.Ordinal))
				{
					text2 = null;
				}
				ModelValidationResult modelValidationResult = new ModelValidationResult
				{
					Message = validationResult.ErrorMessage,
					MemberName = text2
				};
				return new ModelValidationResult[] { modelValidationResult };
			}
			return Enumerable.Empty<ModelValidationResult>();
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000AF58 File Offset: 0x00009158
		internal static bool GetUseLegacyValidationMemberName(NameValueCollection appSettings)
		{
			string[] values = appSettings.GetValues(DataAnnotationsModelValidator.UseLegacyValidationMemberNameKey);
			bool flag;
			return values != null && values.Length != 0 && (bool.TryParse(values[0], out flag) && flag);
		}

		// Token: 0x040000DE RID: 222
		internal static readonly string UseLegacyValidationMemberNameKey = "webapi:UseLegacyValidationMemberName";

		// Token: 0x040000DF RID: 223
		private static bool _useLegacyValidationMemberName = DataAnnotationsModelValidator.GetUseLegacyValidationMemberName(ConfigurationManager.AppSettings);
	}
}
