using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000235 RID: 565
	public enum RuleProperty
	{
		// Token: 0x04000F63 RID: 3939
		[EwsEnum("RuleId")]
		RuleId,
		// Token: 0x04000F64 RID: 3940
		[EwsEnum("DisplayName")]
		DisplayName,
		// Token: 0x04000F65 RID: 3941
		[EwsEnum("Priority")]
		Priority,
		// Token: 0x04000F66 RID: 3942
		[EwsEnum("IsNotSupported")]
		IsNotSupported,
		// Token: 0x04000F67 RID: 3943
		[EwsEnum("Actions")]
		Actions,
		// Token: 0x04000F68 RID: 3944
		[EwsEnum("Condition:Categories")]
		ConditionCategories,
		// Token: 0x04000F69 RID: 3945
		[EwsEnum("Condition:ContainsBodyStrings")]
		ConditionContainsBodyStrings,
		// Token: 0x04000F6A RID: 3946
		[EwsEnum("Condition:ContainsHeaderStrings")]
		ConditionContainsHeaderStrings,
		// Token: 0x04000F6B RID: 3947
		[EwsEnum("Condition:ContainsRecipientStrings")]
		ConditionContainsRecipientStrings,
		// Token: 0x04000F6C RID: 3948
		[EwsEnum("Condition:ContainsSenderStrings")]
		ConditionContainsSenderStrings,
		// Token: 0x04000F6D RID: 3949
		[EwsEnum("Condition:ContainsSubjectOrBodyStrings")]
		ConditionContainsSubjectOrBodyStrings,
		// Token: 0x04000F6E RID: 3950
		[EwsEnum("Condition:ContainsSubjectStrings")]
		ConditionContainsSubjectStrings,
		// Token: 0x04000F6F RID: 3951
		[EwsEnum("Condition:FlaggedForAction")]
		ConditionFlaggedForAction,
		// Token: 0x04000F70 RID: 3952
		[EwsEnum("Condition:FromAddresses")]
		ConditionFromAddresses,
		// Token: 0x04000F71 RID: 3953
		[EwsEnum("Condition:FromConnectedAccounts")]
		ConditionFromConnectedAccounts,
		// Token: 0x04000F72 RID: 3954
		[EwsEnum("Condition:HasAttachments")]
		ConditionHasAttachments,
		// Token: 0x04000F73 RID: 3955
		[EwsEnum("Condition:Importance")]
		ConditionImportance,
		// Token: 0x04000F74 RID: 3956
		[EwsEnum("Condition:IsApprovalRequest")]
		ConditionIsApprovalRequest,
		// Token: 0x04000F75 RID: 3957
		[EwsEnum("Condition:IsAutomaticForward")]
		ConditionIsAutomaticForward,
		// Token: 0x04000F76 RID: 3958
		[EwsEnum("Condition:IsAutomaticReply")]
		ConditionIsAutomaticReply,
		// Token: 0x04000F77 RID: 3959
		[EwsEnum("Condition:IsEncrypted")]
		ConditionIsEncrypted,
		// Token: 0x04000F78 RID: 3960
		[EwsEnum("Condition:IsMeetingRequest")]
		ConditionIsMeetingRequest,
		// Token: 0x04000F79 RID: 3961
		[EwsEnum("Condition:IsMeetingResponse")]
		ConditionIsMeetingResponse,
		// Token: 0x04000F7A RID: 3962
		[EwsEnum("Condition:IsNDR")]
		ConditionIsNonDeliveryReport,
		// Token: 0x04000F7B RID: 3963
		[EwsEnum("Condition:IsPermissionControlled")]
		ConditionIsPermissionControlled,
		// Token: 0x04000F7C RID: 3964
		[EwsEnum("Condition:IsRead")]
		ConditionIsRead,
		// Token: 0x04000F7D RID: 3965
		[EwsEnum("Condition:IsSigned")]
		ConditionIsSigned,
		// Token: 0x04000F7E RID: 3966
		[EwsEnum("Condition:IsVoicemail")]
		ConditionIsVoicemail,
		// Token: 0x04000F7F RID: 3967
		[EwsEnum("Condition:IsReadReceipt")]
		ConditionIsReadReceipt,
		// Token: 0x04000F80 RID: 3968
		[EwsEnum("Condition:ItemClasses")]
		ConditionItemClasses,
		// Token: 0x04000F81 RID: 3969
		[EwsEnum("Condition:MessageClassifications")]
		ConditionMessageClassifications,
		// Token: 0x04000F82 RID: 3970
		[EwsEnum("Condition:NotSentToMe")]
		ConditionNotSentToMe,
		// Token: 0x04000F83 RID: 3971
		[EwsEnum("Condition:SentCcMe")]
		ConditionSentCcMe,
		// Token: 0x04000F84 RID: 3972
		[EwsEnum("Condition:SentOnlyToMe")]
		ConditionSentOnlyToMe,
		// Token: 0x04000F85 RID: 3973
		[EwsEnum("Condition:SentToAddresses")]
		ConditionSentToAddresses,
		// Token: 0x04000F86 RID: 3974
		[EwsEnum("Condition:SentToMe")]
		ConditionSentToMe,
		// Token: 0x04000F87 RID: 3975
		[EwsEnum("Condition:SentToOrCcMe")]
		ConditionSentToOrCcMe,
		// Token: 0x04000F88 RID: 3976
		[EwsEnum("Condition:Sensitivity")]
		ConditionSensitivity,
		// Token: 0x04000F89 RID: 3977
		[EwsEnum("Condition:WithinDateRange")]
		ConditionWithinDateRange,
		// Token: 0x04000F8A RID: 3978
		[EwsEnum("Condition:WithinSizeRange")]
		ConditionWithinSizeRange,
		// Token: 0x04000F8B RID: 3979
		[EwsEnum("Exception:Categories")]
		ExceptionCategories,
		// Token: 0x04000F8C RID: 3980
		[EwsEnum("Exception:ContainsBodyStrings")]
		ExceptionContainsBodyStrings,
		// Token: 0x04000F8D RID: 3981
		[EwsEnum("Exception:ContainsHeaderStrings")]
		ExceptionContainsHeaderStrings,
		// Token: 0x04000F8E RID: 3982
		[EwsEnum("Exception:ContainsRecipientStrings")]
		ExceptionContainsRecipientStrings,
		// Token: 0x04000F8F RID: 3983
		[EwsEnum("Exception:ContainsSenderStrings")]
		ExceptionContainsSenderStrings,
		// Token: 0x04000F90 RID: 3984
		[EwsEnum("Exception:ContainsSubjectOrBodyStrings")]
		ExceptionContainsSubjectOrBodyStrings,
		// Token: 0x04000F91 RID: 3985
		[EwsEnum("Exception:ContainsSubjectStrings")]
		ExceptionContainsSubjectStrings,
		// Token: 0x04000F92 RID: 3986
		[EwsEnum("Exception:FlaggedForAction")]
		ExceptionFlaggedForAction,
		// Token: 0x04000F93 RID: 3987
		[EwsEnum("Exception:FromAddresses")]
		ExceptionFromAddresses,
		// Token: 0x04000F94 RID: 3988
		[EwsEnum("Exception:FromConnectedAccounts")]
		ExceptionFromConnectedAccounts,
		// Token: 0x04000F95 RID: 3989
		[EwsEnum("Exception:HasAttachments")]
		ExceptionHasAttachments,
		// Token: 0x04000F96 RID: 3990
		[EwsEnum("Exception:Importance")]
		ExceptionImportance,
		// Token: 0x04000F97 RID: 3991
		[EwsEnum("Exception:IsApprovalRequest")]
		ExceptionIsApprovalRequest,
		// Token: 0x04000F98 RID: 3992
		[EwsEnum("Exception:IsAutomaticForward")]
		ExceptionIsAutomaticForward,
		// Token: 0x04000F99 RID: 3993
		[EwsEnum("Exception:IsAutomaticReply")]
		ExceptionIsAutomaticReply,
		// Token: 0x04000F9A RID: 3994
		[EwsEnum("Exception:IsEncrypted")]
		ExceptionIsEncrypted,
		// Token: 0x04000F9B RID: 3995
		[EwsEnum("Exception:IsMeetingRequest")]
		ExceptionIsMeetingRequest,
		// Token: 0x04000F9C RID: 3996
		[EwsEnum("Exception:IsMeetingResponse")]
		ExceptionIsMeetingResponse,
		// Token: 0x04000F9D RID: 3997
		[EwsEnum("Exception:IsNDR")]
		ExceptionIsNonDeliveryReport,
		// Token: 0x04000F9E RID: 3998
		[EwsEnum("Exception:IsPermissionControlled")]
		ExceptionIsPermissionControlled,
		// Token: 0x04000F9F RID: 3999
		[EwsEnum("Exception:IsRead")]
		ExceptionIsRead,
		// Token: 0x04000FA0 RID: 4000
		[EwsEnum("Exception:IsSigned")]
		ExceptionIsSigned,
		// Token: 0x04000FA1 RID: 4001
		[EwsEnum("Exception:IsVoicemail")]
		ExceptionIsVoicemail,
		// Token: 0x04000FA2 RID: 4002
		[EwsEnum("Exception:ItemClasses")]
		ExceptionItemClasses,
		// Token: 0x04000FA3 RID: 4003
		[EwsEnum("Exception:MessageClassifications")]
		ExceptionMessageClassifications,
		// Token: 0x04000FA4 RID: 4004
		[EwsEnum("Exception:NotSentToMe")]
		ExceptionNotSentToMe,
		// Token: 0x04000FA5 RID: 4005
		[EwsEnum("Exception:SentCcMe")]
		ExceptionSentCcMe,
		// Token: 0x04000FA6 RID: 4006
		[EwsEnum("Exception:SentOnlyToMe")]
		ExceptionSentOnlyToMe,
		// Token: 0x04000FA7 RID: 4007
		[EwsEnum("Exception:SentToAddresses")]
		ExceptionSentToAddresses,
		// Token: 0x04000FA8 RID: 4008
		[EwsEnum("Exception:SentToMe")]
		ExceptionSentToMe,
		// Token: 0x04000FA9 RID: 4009
		[EwsEnum("Exception:SentToOrCcMe")]
		ExceptionSentToOrCcMe,
		// Token: 0x04000FAA RID: 4010
		[EwsEnum("Exception:Sensitivity")]
		ExceptionSensitivity,
		// Token: 0x04000FAB RID: 4011
		[EwsEnum("Exception:WithinDateRange")]
		ExceptionWithinDateRange,
		// Token: 0x04000FAC RID: 4012
		[EwsEnum("Exception:WithinSizeRange")]
		ExceptionWithinSizeRange,
		// Token: 0x04000FAD RID: 4013
		[EwsEnum("Action:Categories")]
		ActionCategories,
		// Token: 0x04000FAE RID: 4014
		[EwsEnum("Action:CopyToFolder")]
		ActionCopyToFolder,
		// Token: 0x04000FAF RID: 4015
		[EwsEnum("Action:Delete")]
		ActionDelete,
		// Token: 0x04000FB0 RID: 4016
		[EwsEnum("Action:ForwardAsAttachmentToRecipients")]
		ActionForwardAsAttachmentToRecipients,
		// Token: 0x04000FB1 RID: 4017
		[EwsEnum("Action:ForwardToRecipients")]
		ActionForwardToRecipients,
		// Token: 0x04000FB2 RID: 4018
		[EwsEnum("Action:Importance")]
		ActionImportance,
		// Token: 0x04000FB3 RID: 4019
		[EwsEnum("Action:MarkAsRead")]
		ActionMarkAsRead,
		// Token: 0x04000FB4 RID: 4020
		[EwsEnum("Action:MoveToFolder")]
		ActionMoveToFolder,
		// Token: 0x04000FB5 RID: 4021
		[EwsEnum("Action:PermanentDelete")]
		ActionPermanentDelete,
		// Token: 0x04000FB6 RID: 4022
		[EwsEnum("Action:RedirectToRecipients")]
		ActionRedirectToRecipients,
		// Token: 0x04000FB7 RID: 4023
		[EwsEnum("Action:SendSMSAlertToRecipients")]
		ActionSendSMSAlertToRecipients,
		// Token: 0x04000FB8 RID: 4024
		[EwsEnum("Action:ServerReplyWithMessage")]
		ActionServerReplyWithMessage,
		// Token: 0x04000FB9 RID: 4025
		[EwsEnum("Action:StopProcessingRules")]
		ActionStopProcessingRules,
		// Token: 0x04000FBA RID: 4026
		[EwsEnum("IsEnabled")]
		IsEnabled,
		// Token: 0x04000FBB RID: 4027
		[EwsEnum("IsInError")]
		IsInError,
		// Token: 0x04000FBC RID: 4028
		[EwsEnum("Conditions")]
		Conditions,
		// Token: 0x04000FBD RID: 4029
		[EwsEnum("Exceptions")]
		Exceptions
	}
}
