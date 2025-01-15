using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000103 RID: 259
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class AnalyticsWriteServiceInconsistentStateException : MonitoredException
	{
		// Token: 0x06000CAC RID: 3244 RVA: 0x000309D4 File Offset: 0x0002EBD4
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x000309DC File Offset: 0x0002EBDC
		// (set) Token: 0x06000CAE RID: 3246 RVA: 0x000309E4 File Offset: 0x0002EBE4
		public string ServiceUri
		{
			get
			{
				return this.m_serviceUri;
			}
			protected set
			{
				this.m_serviceUri = value;
			}
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x000309ED File Offset: 0x0002EBED
		public AnalyticsWriteServiceInconsistentStateException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00030A01 File Offset: 0x0002EC01
		public AnalyticsWriteServiceInconsistentStateException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00030A18 File Offset: 0x0002EC18
		public AnalyticsWriteServiceInconsistentStateException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00030A38 File Offset: 0x0002EC38
		protected AnalyticsWriteServiceInconsistentStateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("AnalyticsWriteServiceInconsistentStateException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ServiceUri = (string)info.GetValue("AnalyticsWriteServiceInconsistentStateException_ServiceUri", typeof(string));
			}
			catch (SerializationException)
			{
				this.ServiceUri = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("AnalyticsWriteServiceInconsistentStateException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00030B0C File Offset: 0x0002ED0C
		public AnalyticsWriteServiceInconsistentStateException(string serviceUri, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ServiceUri = serviceUri;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00030B2A File Offset: 0x0002ED2A
		public AnalyticsWriteServiceInconsistentStateException(string serviceUri, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ServiceUri = serviceUri;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00030B50 File Offset: 0x0002ED50
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x00030B88 File Offset: 0x0002ED88
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("AnalyticsWriteServiceInconsistentStateException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("AnalyticsWriteServiceInconsistentStateException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ServiceUri != null)
			{
				info.AddValue("AnalyticsWriteServiceInconsistentStateException_ServiceUri", this.ServiceUri, typeof(string));
			}
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00030C08 File Offset: 0x0002EE08
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Service {0} is in inconsistent state", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ServiceUri != null) ? this.ServiceUri.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ServiceUri != null) ? this.ServiceUri.MarkIfInternal() : string.Empty) : ((this.ServiceUri != null) ? this.ServiceUri.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x00030C83 File Offset: 0x0002EE83
		public override string Message
		{
			get
			{
				if (!string.IsNullOrEmpty(this.creationMessage))
				{
					return this.creationMessage;
				}
				return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.None);
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00030CA0 File Offset: 0x0002EEA0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ServiceUri={0}", (this.ServiceUri != null) ? this.ServiceUri.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ServiceUri={0}", (this.ServiceUri != null) ? this.ServiceUri.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ServiceUri={0}", (this.ServiceUri != null) ? this.ServiceUri.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00030D64 File Offset: 0x0002EF64
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00030D6D File Offset: 0x0002EF6D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00030D76 File Offset: 0x0002EF76
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00030D64 File Offset: 0x0002EF64
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00030D80 File Offset: 0x0002EF80
		private string ToString(PrivateInformationMarkupKind markupKind)
		{
			string text = "[" + ExceptionsTemplateHelper.MagicLevel.ToString(CultureInfo.CurrentCulture) + "]" + base.GetType().FullName;
			string text2 = this.CreateMessageFromTemplate(markupKind);
			string text3 = text + ": ";
			if (string.IsNullOrEmpty(this.creationMessage))
			{
				text3 += text2;
			}
			else
			{
				if (markupKind == PrivateInformationMarkupKind.Private || markupKind == PrivateInformationMarkupKind.Internal)
				{
					text3 += this.creationMessage.ObfuscatePrivateValue(true);
				}
				else
				{
					text3 += this.creationMessage;
				}
				if (!string.Equals(this.creationMessage, text2))
				{
					text3 = text3 + Environment.NewLine + "  TemplateMessage: " + text2;
				}
			}
			text3 += this.GetPropertiesString(markupKind);
			text3 = text3 + Environment.NewLine + "ExceptionCulprit=" + this.exceptionCulprit.ToString();
			if (base.InnerException != null)
			{
				try
				{
					ExceptionsTemplateHelper.IncrementMagicLevel();
					IMonitoredError monitoredError = base.InnerException as MonitoredException;
					string text4;
					if (markupKind != PrivateInformationMarkupKind.None)
					{
						if (markupKind != PrivateInformationMarkupKind.Internal)
						{
							text4 = ((monitoredError == null) ? base.InnerException.MarkIfPrivate() : monitoredError.ToPrivateString());
							text4 = text4.ObfuscatePrivateValue(true);
						}
						else
						{
							text4 = ((monitoredError == null) ? base.InnerException.MarkIfInternal() : monitoredError.ToInternalString());
						}
					}
					else
					{
						text4 = ((monitoredError == null) ? base.InnerException.ToString() : monitoredError.ToOriginalString());
					}
					text3 = string.Concat(new string[]
					{
						text3,
						" --->",
						Environment.NewLine,
						text4,
						Environment.NewLine,
						"   --- End of inner exception stack trace ---",
						Environment.NewLine,
						"  (",
						text,
						".StackTrace:)"
					});
				}
				finally
				{
					ExceptionsTemplateHelper.DecrementMagicLevel();
				}
			}
			if (this.StackTrace != null)
			{
				text3 = text3 + Environment.NewLine + this.StackTrace;
			}
			return text3;
		}

		// Token: 0x04000333 RID: 819
		private string creationMessage;

		// Token: 0x04000334 RID: 820
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000335 RID: 821
		private string m_serviceUri;
	}
}
