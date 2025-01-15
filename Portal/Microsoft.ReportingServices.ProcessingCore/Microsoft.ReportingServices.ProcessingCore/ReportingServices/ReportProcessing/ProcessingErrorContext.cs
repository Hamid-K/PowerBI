using System;
using System.Collections;
using System.Globalization;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000625 RID: 1573
	internal sealed class ProcessingErrorContext : ErrorContext
	{
		// Token: 0x060056AC RID: 22188 RVA: 0x0016DFC2 File Offset: 0x0016C1C2
		internal override ProcessingMessage Register(ProcessingErrorCode code, Severity severity, ObjectType objectType, string objectName, string propertyName, params string[] arguments)
		{
			return this.Register(code, severity, objectType, objectName, propertyName, null, arguments);
		}

		// Token: 0x060056AD RID: 22189 RVA: 0x0016DFD4 File Offset: 0x0016C1D4
		internal override ProcessingMessage Register(ProcessingErrorCode code, Severity severity, ObjectType objectType, string objectName, string propertyName, ProcessingMessageList innerMessages, params string[] arguments)
		{
			return this.Register(null, code, severity, objectType, objectName, propertyName, null, arguments);
		}

		// Token: 0x060056AE RID: 22190 RVA: 0x0016DFF4 File Offset: 0x0016C1F4
		internal override ProcessingMessage Register(string diagnosticDetails, ProcessingErrorCode code, Severity severity, ObjectType objectType, string objectName, string propertyName, ProcessingMessageList innerMessages, params string[] arguments)
		{
			ProcessingMessage processingMessage2;
			try
			{
				Monitor.Enter(this);
				if (severity == Severity.Error)
				{
					this.m_hasError = true;
				}
				if (this.RegisterItem(severity, code, objectType, objectName))
				{
					if (this.m_messages == null)
					{
						this.m_messages = new ProcessingMessageList();
					}
					ProcessingMessage processingMessage = ErrorContext.CreateProcessingMessage(code, severity, objectType, objectName, propertyName, diagnosticDetails, innerMessages, arguments);
					this.m_messages.Add(processingMessage);
					processingMessage2 = processingMessage;
				}
				else
				{
					processingMessage2 = null;
				}
			}
			finally
			{
				Monitor.Exit(this);
			}
			return processingMessage2;
		}

		// Token: 0x060056AF RID: 22191 RVA: 0x0016E074 File Offset: 0x0016C274
		internal override void Register(RSException rsException, ObjectType objectType)
		{
			try
			{
				Monitor.Enter(this);
				base.Register(rsException, objectType);
			}
			finally
			{
				Monitor.Exit(this);
			}
		}

		// Token: 0x060056B0 RID: 22192 RVA: 0x0016E0A8 File Offset: 0x0016C2A8
		private bool RegisterItem(Severity severity, ProcessingErrorCode code, ObjectType objectType, string objectName)
		{
			if (this.m_itemsRegistered == null)
			{
				this.m_itemsRegistered = new Hashtable();
			}
			if (ObjectType.DataSet == objectType && (ProcessingErrorCode.rsErrorReadingDataSetField == code || ProcessingErrorCode.rsDataSetFieldTypeNotSupported == code || ProcessingErrorCode.rsMissingFieldInDataSet == code || ProcessingErrorCode.rsErrorReadingFieldProperty == code))
			{
				return true;
			}
			bool flag = false;
			int num = (int)code;
			string text = num.ToString(CultureInfo.InvariantCulture);
			if (objectType == ObjectType.Report || ObjectType.PageHeader == objectType || ObjectType.PageFooter == objectType)
			{
				string text2 = text + objectType.ToString();
				if (!this.m_itemsRegistered.ContainsKey(text2))
				{
					flag = true;
					this.m_itemsRegistered.Add(text2, null);
				}
			}
			else
			{
				Hashtable hashtable = (Hashtable)this.m_itemsRegistered[objectType];
				if (hashtable == null)
				{
					hashtable = new Hashtable();
					this.m_itemsRegistered[objectType] = hashtable;
				}
				Global.Tracer.Assert(objectName != null, "(null != objectName)");
				string text3 = severity.ToString() + text + objectName;
				if (!hashtable.ContainsKey(text3))
				{
					flag = true;
					hashtable.Add(text3, null);
				}
			}
			return flag;
		}

		// Token: 0x060056B1 RID: 22193 RVA: 0x0016E1BC File Offset: 0x0016C3BC
		internal void Combine(ProcessingMessageList messages)
		{
			if (messages != null)
			{
				for (int i = 0; i < messages.Count; i++)
				{
					ProcessingMessage processingMessage = messages[i];
					if (processingMessage.Severity == Severity.Error)
					{
						this.m_hasError = true;
					}
					if (this.RegisterItem(processingMessage.Severity, processingMessage.Code, processingMessage.ObjectType, processingMessage.ObjectName))
					{
						if (this.m_messages == null)
						{
							this.m_messages = new ProcessingMessageList();
						}
						this.m_messages.Add(processingMessage);
					}
				}
			}
		}

		// Token: 0x04002DCC RID: 11724
		private Hashtable m_itemsRegistered;
	}
}
