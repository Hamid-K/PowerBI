using System;
using Microsoft.ReportingServices.DataShapeDefinition;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200009B RID: 155
	internal sealed class DataShapeErrorMessage
	{
		// Token: 0x0600095E RID: 2398 RVA: 0x0002716A File Offset: 0x0002536A
		internal DataShapeErrorMessage(string errorCode, Severity severity, string message, string objectType, string objectName, string propertyName)
		{
			this.m_code = errorCode;
			this.m_severity = severity;
			this.m_message = message;
			this.m_objectType = objectType;
			this.m_objectName = objectName;
			this.m_propertyName = propertyName;
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x000271A0 File Offset: 0x000253A0
		public static DataShapeErrorMessage Create(ProcessingMessage processingMessage)
		{
			return new DataShapeErrorMessage(processingMessage.Code.ToString(), processingMessage.Severity, processingMessage.Message, processingMessage.ObjectType.ToString(), processingMessage.ObjectName, processingMessage.PropertyName);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x000271F2 File Offset: 0x000253F2
		public static DataShapeErrorMessage Create(DataShapeMessage dataShapeMessage)
		{
			return new DataShapeErrorMessage(dataShapeMessage.ErrorCode, dataShapeMessage.Severity, dataShapeMessage.Message, dataShapeMessage.ObjectType, dataShapeMessage.ObjectName, dataShapeMessage.PropertyName);
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x0002721D File Offset: 0x0002541D
		public string ErrorCode
		{
			get
			{
				return this.m_code;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00027225 File Offset: 0x00025425
		public Severity Severity
		{
			get
			{
				return this.m_severity;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x0002722D File Offset: 0x0002542D
		public string Message
		{
			get
			{
				return this.m_message;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00027235 File Offset: 0x00025435
		public string ObjectType
		{
			get
			{
				return this.m_objectType;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x0002723D File Offset: 0x0002543D
		public string ObjectName
		{
			get
			{
				return this.m_objectName;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00027245 File Offset: 0x00025445
		public string PropertyName
		{
			get
			{
				return this.m_propertyName;
			}
		}

		// Token: 0x0400026A RID: 618
		private string m_code;

		// Token: 0x0400026B RID: 619
		private Severity m_severity;

		// Token: 0x0400026C RID: 620
		private string m_message;

		// Token: 0x0400026D RID: 621
		private string m_objectType;

		// Token: 0x0400026E RID: 622
		private string m_objectName;

		// Token: 0x0400026F RID: 623
		private string m_propertyName;
	}
}
