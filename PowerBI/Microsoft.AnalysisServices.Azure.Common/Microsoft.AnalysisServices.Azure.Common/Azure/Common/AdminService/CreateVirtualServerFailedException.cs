using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.AdminService
{
	// Token: 0x0200012A RID: 298
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CreateVirtualServerFailedException : AdminProvisioningServiceException
	{
		// Token: 0x06000FBB RID: 4027 RVA: 0x0003F314 File Offset: 0x0003D514
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x0003F31C File Offset: 0x0003D51C
		// (set) Token: 0x06000FBD RID: 4029 RVA: 0x0003F324 File Offset: 0x0003D524
		public string VirtualServer
		{
			get
			{
				return this.m_virtualServer;
			}
			protected set
			{
				this.m_virtualServer = value;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x0003F32D File Offset: 0x0003D52D
		// (set) Token: 0x06000FBF RID: 4031 RVA: 0x0003F335 File Offset: 0x0003D535
		public string Step
		{
			get
			{
				return this.m_step;
			}
			protected set
			{
				this.m_step = value;
			}
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0003F33E File Offset: 0x0003D53E
		public CreateVirtualServerFailedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0003F357 File Offset: 0x0003D557
		public CreateVirtualServerFailedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0003F36E File Offset: 0x0003D56E
		public CreateVirtualServerFailedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0003F38C File Offset: 0x0003D58C
		protected CreateVirtualServerFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CreateVirtualServerFailedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.VirtualServer = (string)info.GetValue("CreateVirtualServerFailedException_VirtualServer", typeof(string));
			}
			catch (SerializationException)
			{
				this.VirtualServer = null;
			}
			try
			{
				this.Step = (string)info.GetValue("CreateVirtualServerFailedException_Step", typeof(string));
			}
			catch (SerializationException)
			{
				this.Step = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CreateVirtualServerFailedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0003F49C File Offset: 0x0003D69C
		public CreateVirtualServerFailedException(string virtualServer, string step)
		{
			this.VirtualServer = virtualServer;
			this.Step = step;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0003F4B9 File Offset: 0x0003D6B9
		public CreateVirtualServerFailedException(string virtualServer, string step, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.VirtualServer = virtualServer;
			this.Step = step;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0003F4DE File Offset: 0x0003D6DE
		public CreateVirtualServerFailedException(string virtualServer, string step, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.VirtualServer = virtualServer;
			this.Step = step;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0003F50C File Offset: 0x0003D70C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0003F544 File Offset: 0x0003D744
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CreateVirtualServerFailedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CreateVirtualServerFailedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.VirtualServer != null)
			{
				info.AddValue("CreateVirtualServerFailedException_VirtualServer", this.VirtualServer, typeof(string));
			}
			if (this.Step != null)
			{
				info.AddValue("CreateVirtualServerFailedException_Step", this.Step, typeof(string));
			}
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0003F5E8 File Offset: 0x0003D7E8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "CreateVirtualServer for virtual server '{0}' failed at {1}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.VirtualServer != null) ? this.VirtualServer.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.VirtualServer != null) ? this.VirtualServer.MarkIfInternal() : string.Empty) : ((this.VirtualServer != null) ? this.VirtualServer.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.Step != null) ? this.Step.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Step != null) ? this.Step.MarkIfInternal() : string.Empty) : ((this.Step != null) ? this.Step.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x0003F6C2 File Offset: 0x0003D8C2
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

		// Token: 0x06000FCC RID: 4044 RVA: 0x0003F6E0 File Offset: 0x0003D8E0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "VirtualServer={0}", (this.VirtualServer != null) ? this.VirtualServer.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "VirtualServer={0}", (this.VirtualServer != null) ? this.VirtualServer.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "VirtualServer={0}", (this.VirtualServer != null) ? this.VirtualServer.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Step={0}", (this.Step != null) ? this.Step.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Step={0}", (this.Step != null) ? this.Step.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Step={0}", (this.Step != null) ? this.Step.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0003F84C File Offset: 0x0003DA4C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0003F855 File Offset: 0x0003DA55
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0003F85E File Offset: 0x0003DA5E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0003F84C File Offset: 0x0003DA4C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0003F868 File Offset: 0x0003DA68
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

		// Token: 0x040003A4 RID: 932
		private string creationMessage;

		// Token: 0x040003A5 RID: 933
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040003A6 RID: 934
		private string m_virtualServer;

		// Token: 0x040003A7 RID: 935
		private string m_step;
	}
}
