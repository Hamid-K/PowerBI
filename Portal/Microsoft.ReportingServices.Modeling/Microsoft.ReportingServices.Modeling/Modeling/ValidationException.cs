using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200004C RID: 76
	[Serializable]
	internal class ValidationException : RSException
	{
		// Token: 0x06000315 RID: 789 RVA: 0x0000ACB1 File Offset: 0x00008EB1
		internal ValidationException(ValidationMessageCollection messages)
			: base(ErrorCode.rsModelingError, ValidationException.GetMessage(messages), null, Internal.GetTracer(), null, Array.Empty<object>())
		{
			if (messages == null || messages.Count == 0)
			{
				throw new InternalModelingException("messages is null/empty");
			}
			this.m_messages = messages;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000ACEA File Offset: 0x00008EEA
		internal ValidationException(ValidationMessage message)
			: this(new ValidationMessageCollection(message))
		{
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000ACF8 File Offset: 0x00008EF8
		internal ValidationException(ModelingErrorCode code, string message)
			: this(code, null, message, null)
		{
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000AD04 File Offset: 0x00008F04
		internal ValidationException(ModelingErrorCode code, string message, Exception inner)
			: this(code, null, message, inner)
		{
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000AD10 File Offset: 0x00008F10
		internal ValidationException(ModelingErrorCode code, IValidationScope scope, string message)
			: this(code, scope, message, null)
		{
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000AD1C File Offset: 0x00008F1C
		internal ValidationException(ModelingErrorCode code, IValidationScope scope, string message, Exception inner)
			: base(ErrorCode.rsModelingError, message, inner, Internal.GetTracer(), null, Array.Empty<object>())
		{
			this.m_messages = new ValidationMessageCollection(new ValidationMessage(code, Severity.Error, scope, message));
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000AD48 File Offset: 0x00008F48
		protected ValidationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ValidationMessage[] array = (ValidationMessage[])info.GetValue("Messages", typeof(ValidationMessage[]));
			this.m_messages = new ValidationMessageCollection(array);
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000AD84 File Offset: 0x00008F84
		public ValidationMessageCollection Messages
		{
			get
			{
				return this.m_messages;
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000AD8C File Offset: 0x00008F8C
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Messages", ArrayUtil.ToArray<ValidationMessage>(this.m_messages));
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000ADAC File Offset: 0x00008FAC
		private static string GetMessage(ValidationMessageCollection messages)
		{
			if (messages == null || messages.Count == 0)
			{
				throw new InternalModelingException("messages is null/empty");
			}
			ValidationMessage validationMessage = ((messages.FirstError != null) ? messages.FirstError : messages[0]);
			if (messages.Count > 1)
			{
				return SRErrors.ValidationException_MessageWithAdditionalMessages(validationMessage.Message, messages.Count - 1);
			}
			return validationMessage.Message;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000AE0C File Offset: 0x0000900C
		protected override XmlNode AddMoreInformationForThis(XmlDocument doc, XmlNode parent, StringBuilder errorMsgBuilder)
		{
			if (!this.m_messages.HasErrors)
			{
				return base.AddMoreInformationForThis(doc, parent, errorMsgBuilder);
			}
			XmlNode xmlNode = RSException.CreateMoreInfoNode(this.Source, doc, parent);
			foreach (ValidationMessage validationMessage in this.m_messages.GetErrors())
			{
				string text = validationMessage.Code.ToString();
				string text2 = base.CreateHelpLink(typeof(SRErrors).FullName, text);
				RSException.AddMessageToMoreInfoNode(doc, xmlNode, text, validationMessage.Message, text2);
			}
			return xmlNode;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000AEBC File Offset: 0x000090BC
		protected override void AddWarnings(XmlDocument doc, XmlNode parent)
		{
			foreach (ValidationMessage validationMessage in this.m_messages.GetWarnings())
			{
				RSException.AddWarningNode(doc, parent, validationMessage.Code.ToString(), validationMessage.Severity.ToString(), validationMessage.ObjectID, validationMessage.ObjectType, validationMessage.Message);
			}
		}

		// Token: 0x040001F6 RID: 502
		private readonly ValidationMessageCollection m_messages;
	}
}
