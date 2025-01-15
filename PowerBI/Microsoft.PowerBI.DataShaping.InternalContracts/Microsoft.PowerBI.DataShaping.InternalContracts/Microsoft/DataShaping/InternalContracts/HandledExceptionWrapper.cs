using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.DataExtension.Contracts;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x0200001A RID: 26
	internal struct HandledExceptionWrapper
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002F13 File Offset: 0x00001113
		internal HandledExceptionWrapper(DataExtensionException dataExtensionException, DataShapeEngineException engineException)
		{
			this.DataExtensionException = dataExtensionException;
			this.EngineException = engineException;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002F23 File Offset: 0x00001123
		public readonly DataExtensionException DataExtensionException { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002F2B File Offset: 0x0000112B
		public readonly DataShapeEngineException EngineException { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002F33 File Offset: 0x00001133
		public string ErrorCode
		{
			get
			{
				DataExtensionException dataExtensionException = this.DataExtensionException;
				string text;
				if ((text = ((dataExtensionException != null) ? dataExtensionException.ErrorCode : null)) == null)
				{
					DataShapeEngineException engineException = this.EngineException;
					if (engineException == null)
					{
						return null;
					}
					text = engineException.ErrorCode;
				}
				return text;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002F5C File Offset: 0x0000115C
		public ErrorSource ErrorSource
		{
			get
			{
				DataExtensionException dataExtensionException = this.DataExtensionException;
				if (dataExtensionException == null)
				{
					return this.EngineException.ErrorSource;
				}
				return dataExtensionException.ErrorSource;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002F79 File Offset: 0x00001179
		public string Message
		{
			get
			{
				DataExtensionException dataExtensionException = this.DataExtensionException;
				string text;
				if ((text = ((dataExtensionException != null) ? dataExtensionException.Message : null)) == null)
				{
					DataShapeEngineException engineException = this.EngineException;
					if (engineException == null)
					{
						return null;
					}
					text = engineException.Message;
				}
				return text;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002FA2 File Offset: 0x000011A2
		public string Language
		{
			get
			{
				DataExtensionException dataExtensionException = this.DataExtensionException;
				string text;
				if ((text = ((dataExtensionException != null) ? dataExtensionException.Language : null)) == null)
				{
					DataShapeEngineException engineException = this.EngineException;
					if (engineException == null)
					{
						return null;
					}
					text = engineException.Language;
				}
				return text;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002FCB File Offset: 0x000011CB
		public string Source
		{
			get
			{
				DataExtensionException dataExtensionException = this.DataExtensionException;
				string text;
				if ((text = ((dataExtensionException != null) ? dataExtensionException.Source : null)) == null)
				{
					DataShapeEngineException engineException = this.EngineException;
					if (engineException == null)
					{
						return null;
					}
					text = engineException.Source;
				}
				return text;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002FF4 File Offset: 0x000011F4
		public string ErrorDetails
		{
			get
			{
				DataExtensionException dataExtensionException = this.DataExtensionException;
				string text;
				if ((text = ((dataExtensionException != null) ? dataExtensionException.GetErrorDetails() : null)) == null)
				{
					DataShapeEngineException engineException = this.EngineException;
					if (engineException == null)
					{
						return null;
					}
					text = engineException.GetErrorDetails();
				}
				return text;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000301D File Offset: 0x0000121D
		public Exception InnerException
		{
			get
			{
				DataExtensionException dataExtensionException = this.DataExtensionException;
				Exception ex;
				if ((ex = ((dataExtensionException != null) ? dataExtensionException.InnerException : null)) == null)
				{
					DataShapeEngineException engineException = this.EngineException;
					if (engineException == null)
					{
						return null;
					}
					ex = engineException.InnerException;
				}
				return ex;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003046 File Offset: 0x00001246
		public DataShapeEngineErrorInfo ToErrorInfo()
		{
			return new DataShapeEngineErrorInfo(this.ErrorCode, this.ErrorSource);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003059 File Offset: 0x00001259
		public static implicit operator HandledExceptionWrapper(DataExtensionException dataExtensionException)
		{
			return new HandledExceptionWrapper(dataExtensionException, null);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003062 File Offset: 0x00001262
		public static implicit operator HandledExceptionWrapper(DataShapeEngineException engineException)
		{
			return new HandledExceptionWrapper(null, engineException);
		}
	}
}
