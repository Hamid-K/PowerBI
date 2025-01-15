using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts
{
	// Token: 0x0200000E RID: 14
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class RequiredIfNotAnonymousAttribute : RequiredAttribute
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000026B8 File Offset: 0x000008B8
		public override bool RequiresValidationContext
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000026BC File Offset: 0x000008BC
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (validationContext == null)
			{
				throw new ArgumentNullException("validationContext");
			}
			CredentialDetails credentialDetails = validationContext.ObjectInstance as CredentialDetails;
			UnifiedGatewayCredentialDetails unifiedGatewayCredentialDetails = validationContext.ObjectInstance as UnifiedGatewayCredentialDetails;
			if (credentialDetails == null && unifiedGatewayCredentialDetails == null)
			{
				throw new ArgumentException("You can only use this attribute with an instance of CredentialDetails or UnifiedGatewayCredentialDetails");
			}
			if (!RequiredIfNotAnonymousAttribute.IsRequired((credentialDetails != null) ? credentialDetails.CredentialType : new CredentialType?(unifiedGatewayCredentialDetails.CredentialType)))
			{
				return ValidationResult.Success;
			}
			return base.IsValid(value, validationContext);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000272B File Offset: 0x0000092B
		private static bool IsRequired(CredentialType? credentialType)
		{
			return credentialType != null && !credentialType.Equals(CredentialType.Anonymous) && !credentialType.Equals(CredentialType.WindowsWithoutImpersonation);
		}
	}
}
