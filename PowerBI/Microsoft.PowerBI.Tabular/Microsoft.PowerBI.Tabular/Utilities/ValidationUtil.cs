using System;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular.Utilities
{
	// Token: 0x0200019B RID: 411
	internal static class ValidationUtil
	{
		// Token: 0x0600196D RID: 6509 RVA: 0x000A8E34 File Offset: 0x000A7034
		public static void ValidateLink(MetadataObject linkOwner, string linkPropName, MetadataObject targetObj, ObjectPath targetPath, ValidationResult result, bool throwOnError)
		{
			Utils.Verify(result != null || throwOnError);
			if (targetObj == null && targetPath != null)
			{
				ValidationUtil.AddLinkValidationError(result, throwOnError, new ValidationError
				{
					Message = TomSR.Validation_UnresolvedLink(linkPropName, ClientHostingManager.MarkAsRestrictedInformation(linkOwner.GetFormattedObjectPath(), InfoRestrictionType.CCON)),
					Source = linkOwner
				});
			}
			if (!linkOwner.IsRemoved && targetObj != null && targetObj.IsRemoved)
			{
				ValidationUtil.AddLinkValidationError(result, throwOnError, new ValidationError
				{
					Message = TomSR.Validation_LinkToRemovedObject(linkPropName, ClientHostingManager.MarkAsRestrictedInformation(linkOwner.GetFormattedObjectPath(), InfoRestrictionType.CCON)),
					Source = linkOwner
				});
			}
			if (linkOwner.Model != null && targetObj != null && linkOwner.Model != targetObj.Model)
			{
				ValidationUtil.AddLinkValidationError(result, throwOnError, new ValidationError
				{
					Message = TomSR.Validation_LinkToAnotherModel(linkPropName, ClientHostingManager.MarkAsRestrictedInformation(linkOwner.GetFormattedObjectPath(), InfoRestrictionType.CCON)),
					Source = linkOwner
				});
			}
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x000A8F07 File Offset: 0x000A7107
		private static void AddLinkValidationError(ValidationResult result, bool throwOnError, ValidationError error)
		{
			if (throwOnError)
			{
				throw new TomValidationException(error);
			}
			result.AddError(error);
		}
	}
}
