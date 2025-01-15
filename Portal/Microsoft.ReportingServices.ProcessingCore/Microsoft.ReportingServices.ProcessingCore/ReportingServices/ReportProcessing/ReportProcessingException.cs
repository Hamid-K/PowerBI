using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005F3 RID: 1523
	[Serializable]
	public class ReportProcessingException : ReportProcessingExceptionBase
	{
		// Token: 0x0600542E RID: 21550 RVA: 0x00161C04 File Offset: 0x0015FE04
		public ReportProcessingException(ProcessingMessageList processingMessages)
			: this(processingMessages, null)
		{
		}

		// Token: 0x0600542F RID: 21551 RVA: 0x00161C10 File Offset: 0x0015FE10
		internal ReportProcessingException(ProcessingMessageList processingMessages, Exception innerException)
			: base(ErrorCode.rsProcessingError, RPResWrapper.Keys.GetString(ErrorCode.rsProcessingError.ToString()), innerException, Global.Tracer, null, Array.Empty<object>())
		{
			this.m_useMessageListForExceptionMessage = true;
			this.m_processingMessages = processingMessages;
		}

		// Token: 0x06005430 RID: 21552 RVA: 0x00161C54 File Offset: 0x0015FE54
		internal ReportProcessingException(Exception innerException, ProcessingMessageList processingMessages)
			: base(ErrorCode.rsInternalError, RPResWrapper.Keys.GetString(ErrorCode.rsUnexpectedError.ToString()), innerException, Global.Tracer, null, Array.Empty<object>())
		{
			this.m_processingMessages = processingMessages;
		}

		// Token: 0x06005431 RID: 21553 RVA: 0x00161C94 File Offset: 0x0015FE94
		public ReportProcessingException(ErrorCode code, Exception innerException, params object[] arguments)
			: base(code, string.Format(CultureInfo.CurrentCulture, RPResWrapper.Keys.GetString(code.ToString()), arguments), innerException, Global.Tracer, null, Array.Empty<object>())
		{
		}

		// Token: 0x06005432 RID: 21554 RVA: 0x00161CC6 File Offset: 0x0015FEC6
		public ReportProcessingException(ErrorCode code)
			: base(code, RPResWrapper.Keys.GetString(code.ToString()), null, Global.Tracer, null, Array.Empty<object>())
		{
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x00161CED File Offset: 0x0015FEED
		internal ReportProcessingException(ErrorCode code, params object[] arguments)
			: base(code, string.Format(CultureInfo.CurrentCulture, RPResWrapper.Keys.GetString(code.ToString()), arguments), null, Global.Tracer, null, Array.Empty<object>())
		{
		}

		// Token: 0x06005434 RID: 21556 RVA: 0x00161D1F File Offset: 0x0015FF1F
		internal ReportProcessingException(string errMessage, ErrorCode code)
			: base(code, errMessage, null, Global.Tracer, null, Array.Empty<object>())
		{
		}

		// Token: 0x06005435 RID: 21557 RVA: 0x00161D35 File Offset: 0x0015FF35
		internal ReportProcessingException(string errMessage, ProcessingMessageList processingMessages, ErrorCode code)
			: base(code, errMessage, null, Global.Tracer, null, Array.Empty<object>())
		{
			this.m_processingMessages = processingMessages;
		}

		// Token: 0x06005436 RID: 21558 RVA: 0x00161D52 File Offset: 0x0015FF52
		public ReportProcessingException(string message, ErrorCode code, Exception innerException)
			: base(code, message, innerException, Global.Tracer, null, Array.Empty<object>())
		{
		}

		// Token: 0x06005437 RID: 21559 RVA: 0x00161D68 File Offset: 0x0015FF68
		protected ReportProcessingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.m_processingMessages = (ProcessingMessageList)info.GetValue("ProcessingMessages", typeof(ProcessingMessageList));
			this.m_useMessageListForExceptionMessage = info.GetBoolean("UseMessageListForExeptionMessage");
		}

		// Token: 0x17001F02 RID: 7938
		// (get) Token: 0x06005438 RID: 21560 RVA: 0x00161DA3 File Offset: 0x0015FFA3
		public ProcessingMessageList ProcessingMessages
		{
			[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
			get
			{
				return this.m_processingMessages;
			}
		}

		// Token: 0x06005439 RID: 21561 RVA: 0x00161DAB File Offset: 0x0015FFAB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ProcessingMessages", this.m_processingMessages);
			info.AddValue("UseMessageListForExeptionMessage", this.m_useMessageListForExceptionMessage);
		}

		// Token: 0x17001F03 RID: 7939
		// (get) Token: 0x0600543A RID: 21562 RVA: 0x00161DD8 File Offset: 0x0015FFD8
		public override string Message
		{
			get
			{
				if (this.m_useMessageListForExceptionMessage && this.m_processingMessages != null)
				{
					foreach (object obj in this.m_processingMessages)
					{
						ProcessingMessage processingMessage = (ProcessingMessage)obj;
						if (processingMessage.Severity == Severity.Error)
						{
							return processingMessage.Message;
						}
					}
				}
				return base.Message;
			}
		}

		// Token: 0x0600543B RID: 21563 RVA: 0x00161E54 File Offset: 0x00160054
		protected override XmlNode AddMoreInformationForThis(XmlDocument doc, XmlNode parent, StringBuilder errorMsgBuilder)
		{
			if (this.m_processingMessages == null)
			{
				return base.AddMoreInformationForThis(doc, parent, errorMsgBuilder);
			}
			bool flag = false;
			using (IEnumerator enumerator = this.m_processingMessages.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((ProcessingMessage)enumerator.Current).Severity == Severity.Error)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return base.AddMoreInformationForThis(doc, parent, errorMsgBuilder);
			}
			XmlNode xmlNode = RSException.CreateMoreInfoNode(this.Source, doc, parent);
			foreach (object obj in this.m_processingMessages)
			{
				ProcessingMessage processingMessage = (ProcessingMessage)obj;
				if (processingMessage.Severity == Severity.Error && xmlNode != null)
				{
					string text = ReportProcessingException.CodeFromMessage(processingMessage);
					string text2 = base.CreateHelpLink(typeof(RPRes).FullName, text);
					RSException.AddMessageToMoreInfoNode(doc, xmlNode, text, processingMessage.Message, text2);
				}
			}
			return xmlNode;
		}

		// Token: 0x0600543C RID: 21564 RVA: 0x00161F64 File Offset: 0x00160164
		protected override void AddWarnings(XmlDocument doc, XmlNode parent)
		{
			if (this.m_processingMessages == null)
			{
				return;
			}
			foreach (object obj in this.m_processingMessages)
			{
				ProcessingMessage processingMessage = (ProcessingMessage)obj;
				if (processingMessage.Severity == Severity.Warning)
				{
					string text = ReportProcessingException.CodeFromMessage(processingMessage);
					RSException.AddWarningNode(doc, parent, text, processingMessage.Severity.ToString(), processingMessage.ObjectName, processingMessage.ObjectType.ToString(), processingMessage.Message);
				}
			}
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x00162010 File Offset: 0x00160210
		protected override List<RSException.AdditionalMessage> GetAdditionalMessages()
		{
			if (this.m_processingMessages == null)
			{
				return null;
			}
			List<RSException.AdditionalMessage> list = new List<RSException.AdditionalMessage>(this.m_processingMessages.Count);
			foreach (object obj in this.m_processingMessages)
			{
				ProcessingMessage processingMessage = (ProcessingMessage)obj;
				list.Add(new RSException.AdditionalMessage(ReportProcessingException.CodeFromMessage(processingMessage), processingMessage.Severity.ToString(), processingMessage.Message, processingMessage.ObjectType.ToString(), processingMessage.ObjectName, processingMessage.PropertyName, null));
			}
			return list;
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x001620D0 File Offset: 0x001602D0
		private static string CodeFromMessage(ProcessingMessage message)
		{
			if (message.Code == ProcessingErrorCode.rsNone)
			{
				return message.CommonCode.ToString();
			}
			return message.Code.ToString();
		}

		// Token: 0x04002CDB RID: 11483
		private ProcessingMessageList m_processingMessages;

		// Token: 0x04002CDC RID: 11484
		private bool m_useMessageListForExceptionMessage;

		// Token: 0x04002CDD RID: 11485
		private const string ProcessingMessagesName = "ProcessingMessages";

		// Token: 0x04002CDE RID: 11486
		private const string UseMessageListForExeptionMessageName = "UseMessageListForExeptionMessage";
	}
}
