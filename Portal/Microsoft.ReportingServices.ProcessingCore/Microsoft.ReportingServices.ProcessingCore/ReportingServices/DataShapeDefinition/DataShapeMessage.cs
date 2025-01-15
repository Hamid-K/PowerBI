using System;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x0200058F RID: 1423
	[DataContract]
	internal sealed class DataShapeMessage
	{
		// Token: 0x060051B1 RID: 20913 RVA: 0x00159F88 File Offset: 0x00158188
		internal DataShapeMessage(ErrorCode commonCode, string errorCode, Severity severity, string message, string objectType, string objectName, string propertyName)
		{
			this.m_commonCode = commonCode;
			this.m_code = errorCode;
			this.m_severity = severity;
			this.m_message = message;
			this.m_objectType = objectType;
			this.m_objectName = objectName;
			this.m_propertyName = propertyName;
		}

		// Token: 0x17001E58 RID: 7768
		// (get) Token: 0x060051B2 RID: 20914 RVA: 0x00159FC5 File Offset: 0x001581C5
		public string ErrorCode
		{
			get
			{
				return this.m_code;
			}
		}

		// Token: 0x17001E59 RID: 7769
		// (get) Token: 0x060051B3 RID: 20915 RVA: 0x00159FCD File Offset: 0x001581CD
		public ErrorCode CommonCode
		{
			get
			{
				return this.m_commonCode;
			}
		}

		// Token: 0x17001E5A RID: 7770
		// (get) Token: 0x060051B4 RID: 20916 RVA: 0x00159FD5 File Offset: 0x001581D5
		public Severity Severity
		{
			get
			{
				return this.m_severity;
			}
		}

		// Token: 0x17001E5B RID: 7771
		// (get) Token: 0x060051B5 RID: 20917 RVA: 0x00159FDD File Offset: 0x001581DD
		public string Message
		{
			get
			{
				return this.m_message;
			}
		}

		// Token: 0x17001E5C RID: 7772
		// (get) Token: 0x060051B6 RID: 20918 RVA: 0x00159FE5 File Offset: 0x001581E5
		public string ObjectType
		{
			get
			{
				return this.m_objectType;
			}
		}

		// Token: 0x17001E5D RID: 7773
		// (get) Token: 0x060051B7 RID: 20919 RVA: 0x00159FED File Offset: 0x001581ED
		public string ObjectName
		{
			get
			{
				return this.m_objectName;
			}
		}

		// Token: 0x17001E5E RID: 7774
		// (get) Token: 0x060051B8 RID: 20920 RVA: 0x00159FF5 File Offset: 0x001581F5
		public string PropertyName
		{
			get
			{
				return this.m_propertyName;
			}
		}

		// Token: 0x060051B9 RID: 20921 RVA: 0x00159FFD File Offset: 0x001581FD
		public string GetErrorCodeAsString()
		{
			return this.ErrorCode;
		}

		// Token: 0x060051BA RID: 20922 RVA: 0x0015A008 File Offset: 0x00158208
		public string FormatMessage()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} ({1}.{2}) : {3} [{4}]", new object[]
			{
				(this.m_severity == Severity.Warning) ? "Warning" : "Error",
				this.m_objectName,
				this.m_propertyName,
				this.m_message,
				this.m_code
			});
		}

		// Token: 0x0400293D RID: 10557
		[DataMember(Name = "ErrorCode")]
		private string m_code;

		// Token: 0x0400293E RID: 10558
		[DataMember(Name = "Code")]
		private ErrorCode m_commonCode;

		// Token: 0x0400293F RID: 10559
		[DataMember(Name = "Severity")]
		private Severity m_severity;

		// Token: 0x04002940 RID: 10560
		[DataMember(Name = "Message")]
		private string m_message;

		// Token: 0x04002941 RID: 10561
		[DataMember(Name = "ObjectType")]
		private string m_objectType;

		// Token: 0x04002942 RID: 10562
		[DataMember(Name = "ObjectName")]
		private string m_objectName;

		// Token: 0x04002943 RID: 10563
		[DataMember(Name = "PropertyName")]
		private string m_propertyName;
	}
}
