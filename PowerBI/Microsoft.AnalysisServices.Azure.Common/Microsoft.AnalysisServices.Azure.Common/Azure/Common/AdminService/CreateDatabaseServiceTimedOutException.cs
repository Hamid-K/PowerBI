using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.AdminService
{
	// Token: 0x02000125 RID: 293
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CreateDatabaseServiceTimedOutException : AdminProvisioningServiceException
	{
		// Token: 0x06000F4F RID: 3919 RVA: 0x0003D21C File Offset: 0x0003B41C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x0003D224 File Offset: 0x0003B424
		// (set) Token: 0x06000F51 RID: 3921 RVA: 0x0003D22C File Offset: 0x0003B42C
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

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x0003D235 File Offset: 0x0003B435
		// (set) Token: 0x06000F53 RID: 3923 RVA: 0x0003D23D File Offset: 0x0003B43D
		public string Timeout
		{
			get
			{
				return this.m_timeout;
			}
			protected set
			{
				this.m_timeout = value;
			}
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0003D246 File Offset: 0x0003B446
		public CreateDatabaseServiceTimedOutException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0003D25F File Offset: 0x0003B45F
		public CreateDatabaseServiceTimedOutException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0003D276 File Offset: 0x0003B476
		public CreateDatabaseServiceTimedOutException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0003D294 File Offset: 0x0003B494
		protected CreateDatabaseServiceTimedOutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CreateDatabaseServiceTimedOutException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ServiceName = (string)info.GetValue("CreateDatabaseServiceTimedOutException_ServiceName", typeof(string));
			}
			catch (SerializationException)
			{
				this.ServiceName = null;
			}
			try
			{
				this.Timeout = (string)info.GetValue("CreateDatabaseServiceTimedOutException_Timeout", typeof(string));
			}
			catch (SerializationException)
			{
				this.Timeout = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CreateDatabaseServiceTimedOutException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0003D3A4 File Offset: 0x0003B5A4
		public CreateDatabaseServiceTimedOutException(string serviceName, string timeout)
		{
			this.ServiceName = serviceName;
			this.Timeout = timeout;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0003D3C1 File Offset: 0x0003B5C1
		public CreateDatabaseServiceTimedOutException(string serviceName, string timeout, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ServiceName = serviceName;
			this.Timeout = timeout;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0003D3E6 File Offset: 0x0003B5E6
		public CreateDatabaseServiceTimedOutException(string serviceName, string timeout, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ServiceName = serviceName;
			this.Timeout = timeout;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0003D414 File Offset: 0x0003B614
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0003D44C File Offset: 0x0003B64C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CreateDatabaseServiceTimedOutException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CreateDatabaseServiceTimedOutException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ServiceName != null)
			{
				info.AddValue("CreateDatabaseServiceTimedOutException_ServiceName", this.ServiceName, typeof(string));
			}
			if (this.Timeout != null)
			{
				info.AddValue("CreateDatabaseServiceTimedOutException_Timeout", this.Timeout, typeof(string));
			}
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0003D4F0 File Offset: 0x0003B6F0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Windows Fabric timed out on creating WF Service '{0}' - Time elapsed {1}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ServiceName != null) ? this.ServiceName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ServiceName != null) ? this.ServiceName.MarkIfInternal() : string.Empty) : ((this.ServiceName != null) ? this.ServiceName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.Timeout != null) ? this.Timeout.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Timeout != null) ? this.Timeout.MarkIfInternal() : string.Empty) : ((this.Timeout != null) ? this.Timeout.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x0003D5CA File Offset: 0x0003B7CA
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

		// Token: 0x06000F60 RID: 3936 RVA: 0x0003D5E8 File Offset: 0x0003B7E8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ServiceName={0}", (this.ServiceName != null) ? this.ServiceName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ServiceName={0}", (this.ServiceName != null) ? this.ServiceName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ServiceName={0}", (this.ServiceName != null) ? this.ServiceName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Timeout={0}", (this.Timeout != null) ? this.Timeout.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Timeout={0}", (this.Timeout != null) ? this.Timeout.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Timeout={0}", (this.Timeout != null) ? this.Timeout.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0003D754 File Offset: 0x0003B954
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0003D75D File Offset: 0x0003B95D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0003D766 File Offset: 0x0003B966
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0003D754 File Offset: 0x0003B954
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0003D770 File Offset: 0x0003B970
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

		// Token: 0x04000393 RID: 915
		private string creationMessage;

		// Token: 0x04000394 RID: 916
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000395 RID: 917
		private string m_serviceName;

		// Token: 0x04000396 RID: 918
		private string m_timeout;
	}
}
