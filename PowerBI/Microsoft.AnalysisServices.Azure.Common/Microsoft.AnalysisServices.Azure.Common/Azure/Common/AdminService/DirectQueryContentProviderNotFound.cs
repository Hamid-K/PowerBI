using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.AdminService
{
	// Token: 0x02000130 RID: 304
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class DirectQueryContentProviderNotFound : AdminProvisioningServiceException
	{
		// Token: 0x0600103D RID: 4157 RVA: 0x00041B54 File Offset: 0x0003FD54
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x00041B5C File Offset: 0x0003FD5C
		// (set) Token: 0x0600103F RID: 4159 RVA: 0x00041B64 File Offset: 0x0003FD64
		public string ProviderId
		{
			get
			{
				return this.m_providerId;
			}
			protected set
			{
				this.m_providerId = value;
			}
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00041B6D File Offset: 0x0003FD6D
		public DirectQueryContentProviderNotFound()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x00041B81 File Offset: 0x0003FD81
		public DirectQueryContentProviderNotFound(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00041B98 File Offset: 0x0003FD98
		public DirectQueryContentProviderNotFound(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00041BB8 File Offset: 0x0003FDB8
		protected DirectQueryContentProviderNotFound(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("DirectQueryContentProviderNotFound_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ProviderId = (string)info.GetValue("DirectQueryContentProviderNotFound_ProviderId", typeof(string));
			}
			catch (SerializationException)
			{
				this.ProviderId = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("DirectQueryContentProviderNotFound_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00041C8C File Offset: 0x0003FE8C
		public DirectQueryContentProviderNotFound(string providerId, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ProviderId = providerId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00041CAA File Offset: 0x0003FEAA
		public DirectQueryContentProviderNotFound(string providerId, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ProviderId = providerId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x00041CD0 File Offset: 0x0003FED0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x00041D08 File Offset: 0x0003FF08
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("DirectQueryContentProviderNotFound_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("DirectQueryContentProviderNotFound_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ProviderId != null)
			{
				info.AddValue("DirectQueryContentProviderNotFound_ProviderId", this.ProviderId, typeof(string));
			}
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00041D88 File Offset: 0x0003FF88
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Unable to find the direct query content provider with id '{0}'", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ProviderId != null) ? this.ProviderId.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ProviderId != null) ? this.ProviderId.MarkIfInternal() : string.Empty) : ((this.ProviderId != null) ? this.ProviderId.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x00041E03 File Offset: 0x00040003
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

		// Token: 0x0600104B RID: 4171 RVA: 0x00041E20 File Offset: 0x00040020
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ProviderId={0}", (this.ProviderId != null) ? this.ProviderId.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ProviderId={0}", (this.ProviderId != null) ? this.ProviderId.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ProviderId={0}", (this.ProviderId != null) ? this.ProviderId.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x00041EE4 File Offset: 0x000400E4
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00041EED File Offset: 0x000400ED
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00041EF6 File Offset: 0x000400F6
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00041EE4 File Offset: 0x000400E4
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x00041F00 File Offset: 0x00040100
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

		// Token: 0x040003B8 RID: 952
		private string creationMessage;

		// Token: 0x040003B9 RID: 953
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040003BA RID: 954
		private string m_providerId;
	}
}
