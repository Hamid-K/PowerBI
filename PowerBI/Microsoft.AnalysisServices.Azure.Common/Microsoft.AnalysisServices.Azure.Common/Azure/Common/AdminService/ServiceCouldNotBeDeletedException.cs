using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.AdminService
{
	// Token: 0x02000123 RID: 291
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ServiceCouldNotBeDeletedException : AdminProvisioningServiceException
	{
		// Token: 0x06000F26 RID: 3878 RVA: 0x0003C614 File Offset: 0x0003A814
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x0003C61C File Offset: 0x0003A81C
		// (set) Token: 0x06000F28 RID: 3880 RVA: 0x0003C624 File Offset: 0x0003A824
		public string ServiceName
		{
			get
			{
				return this.m_serviceName;
			}
			protected set
			{
				this.m_serviceName = value;
			}
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0003C62D File Offset: 0x0003A82D
		public ServiceCouldNotBeDeletedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0003C641 File Offset: 0x0003A841
		public ServiceCouldNotBeDeletedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0003C658 File Offset: 0x0003A858
		public ServiceCouldNotBeDeletedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0003C678 File Offset: 0x0003A878
		protected ServiceCouldNotBeDeletedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ServiceCouldNotBeDeletedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ServiceName = (string)info.GetValue("ServiceCouldNotBeDeletedException_ServiceName", typeof(string));
			}
			catch (SerializationException)
			{
				this.ServiceName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ServiceCouldNotBeDeletedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0003C74C File Offset: 0x0003A94C
		public ServiceCouldNotBeDeletedException(string serviceName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ServiceName = serviceName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0003C76A File Offset: 0x0003A96A
		public ServiceCouldNotBeDeletedException(string serviceName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ServiceName = serviceName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0003C790 File Offset: 0x0003A990
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0003C7C8 File Offset: 0x0003A9C8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ServiceCouldNotBeDeletedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ServiceCouldNotBeDeletedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ServiceName != null)
			{
				info.AddValue("ServiceCouldNotBeDeletedException_ServiceName", this.ServiceName, typeof(string));
			}
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0003C848 File Offset: 0x0003AA48
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "WF Service '{0}' could not be deleted", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ServiceName != null) ? this.ServiceName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ServiceName != null) ? this.ServiceName.MarkIfInternal() : string.Empty) : ((this.ServiceName != null) ? this.ServiceName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x0003C8C3 File Offset: 0x0003AAC3
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

		// Token: 0x06000F34 RID: 3892 RVA: 0x0003C8E0 File Offset: 0x0003AAE0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ServiceName={0}", (this.ServiceName != null) ? this.ServiceName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ServiceName={0}", (this.ServiceName != null) ? this.ServiceName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ServiceName={0}", (this.ServiceName != null) ? this.ServiceName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0003C9A4 File Offset: 0x0003ABA4
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0003C9AD File Offset: 0x0003ABAD
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0003C9B6 File Offset: 0x0003ABB6
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0003C9A4 File Offset: 0x0003ABA4
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0003C9C0 File Offset: 0x0003ABC0
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

		// Token: 0x0400038D RID: 909
		private string creationMessage;

		// Token: 0x0400038E RID: 910
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400038F RID: 911
		private string m_serviceName;
	}
}
