using System;

namespace DocumentFormat.OpenXml.Packaging
{
	// Token: 0x020030BB RID: 12475
	internal static class MailMergeRecipientDataPartTypeInfo
	{
		// Token: 0x0601B1D5 RID: 111061 RVA: 0x0036C17C File Offset: 0x0036A37C
		internal static string GetContentType(MailMergeRecipientDataPartType mailMergeRecipientDataPartType)
		{
			switch (mailMergeRecipientDataPartType)
			{
			case MailMergeRecipientDataPartType.OpenXmlMailMergeRecipientData:
				return "application/vnd.openxmlformats-officedocument.wordprocessingml.mailMergeRecipientData+xml";
			case MailMergeRecipientDataPartType.MsWordMailMergeRecipientData:
				return "application/vnd.ms-word.mailMergeRecipientData+xml";
			default:
				throw new ArgumentOutOfRangeException("mailMergeRecipientDataPartType");
			}
		}

		// Token: 0x0601B1D6 RID: 111062 RVA: 0x00299794 File Offset: 0x00297994
		internal static string GetTargetExtension(MailMergeRecipientDataPartType mailMergeRecipientDataPartType)
		{
			return ".xml";
		}
	}
}
