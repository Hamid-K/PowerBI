using System;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000324 RID: 804
	public class Warning
	{
		// Token: 0x06001B56 RID: 6998 RVA: 0x0006F7B0 File Offset: 0x0006D9B0
		public Warning()
		{
			this.Code = null;
			this.Severity = null;
			this.ObjectName = null;
			this.ObjectType = null;
			this.Message = null;
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x0006F7DC File Offset: 0x0006D9DC
		internal static Warning[] ProcessingMessagesToWarningArray(ProcessingMessageList messages)
		{
			if (messages == null)
			{
				return null;
			}
			Warning[] array = new Warning[messages.Count];
			int num = 0;
			foreach (object obj in messages)
			{
				ProcessingMessage processingMessage = (ProcessingMessage)obj;
				array[num] = new Warning();
				array[num].Code = processingMessage.Code.ToString();
				array[num].Message = processingMessage.Message;
				array[num].ObjectName = processingMessage.ObjectName;
				array[num].ObjectType = processingMessage.ObjectType.ToString();
				array[num].Severity = processingMessage.Severity.ToString();
				num++;
			}
			return array;
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x0006F8C8 File Offset: 0x0006DAC8
		internal static Warning[] ModelingMessagesToWarningArray(ValidationMessageCollection messages)
		{
			if (messages == null)
			{
				return null;
			}
			Warning[] array = new Warning[messages.Count];
			int num = 0;
			foreach (ValidationMessage validationMessage in messages)
			{
				array[num] = new Warning();
				array[num].Code = validationMessage.Code.ToString();
				array[num].Message = validationMessage.Message;
				array[num].ObjectName = validationMessage.ObjectID;
				array[num].ObjectType = validationMessage.ObjectType.ToString();
				array[num].Severity = validationMessage.Severity.ToString();
				num++;
			}
			return array;
		}

		// Token: 0x04000AE4 RID: 2788
		public string Code;

		// Token: 0x04000AE5 RID: 2789
		public string Severity;

		// Token: 0x04000AE6 RID: 2790
		public string ObjectName;

		// Token: 0x04000AE7 RID: 2791
		public string ObjectType;

		// Token: 0x04000AE8 RID: 2792
		public string Message;
	}
}
