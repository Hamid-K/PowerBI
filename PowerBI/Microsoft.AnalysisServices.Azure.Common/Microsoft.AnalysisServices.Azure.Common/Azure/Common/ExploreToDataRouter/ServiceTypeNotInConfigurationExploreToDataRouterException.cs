using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.ExploreToDataRouter
{
	// Token: 0x02000152 RID: 338
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ServiceTypeNotInConfigurationExploreToDataRouterException : ExploreToDataRouterException
	{
		// Token: 0x060011B9 RID: 4537 RVA: 0x0004899C File Offset: 0x00046B9C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x000489A4 File Offset: 0x00046BA4
		// (set) Token: 0x060011BB RID: 4539 RVA: 0x000489AC File Offset: 0x00046BAC
		public string ServiceType
		{
			get
			{
				return this.m_serviceType;
			}
			protected set
			{
				this.m_serviceType = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x000489B5 File Offset: 0x00046BB5
		// (set) Token: 0x060011BD RID: 4541 RVA: 0x000489BD File Offset: 0x00046BBD
		public string ConfiguredServiceTypes
		{
			get
			{
				return this.m_configuredServiceTypes;
			}
			protected set
			{
				this.m_configuredServiceTypes = value;
			}
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x000489C6 File Offset: 0x00046BC6
		public ServiceTypeNotInConfigurationExploreToDataRouterException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x000489DF File Offset: 0x00046BDF
		public ServiceTypeNotInConfigurationExploreToDataRouterException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x000489F6 File Offset: 0x00046BF6
		public ServiceTypeNotInConfigurationExploreToDataRouterException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00048A14 File Offset: 0x00046C14
		protected ServiceTypeNotInConfigurationExploreToDataRouterException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ServiceTypeNotInConfigurationExploreToDataRouterException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ServiceType = (string)info.GetValue("ServiceTypeNotInConfigurationExploreToDataRouterException_ServiceType", typeof(string));
			}
			catch (SerializationException)
			{
				this.ServiceType = null;
			}
			try
			{
				this.ConfiguredServiceTypes = (string)info.GetValue("ServiceTypeNotInConfigurationExploreToDataRouterException_ConfiguredServiceTypes", typeof(string));
			}
			catch (SerializationException)
			{
				this.ConfiguredServiceTypes = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ServiceTypeNotInConfigurationExploreToDataRouterException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00048B24 File Offset: 0x00046D24
		public ServiceTypeNotInConfigurationExploreToDataRouterException(string serviceType, string configuredServiceTypes)
		{
			this.ServiceType = serviceType;
			this.ConfiguredServiceTypes = configuredServiceTypes;
			this.ConstructorInternal(false);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00048B41 File Offset: 0x00046D41
		public ServiceTypeNotInConfigurationExploreToDataRouterException(string serviceType, string configuredServiceTypes, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ServiceType = serviceType;
			this.ConfiguredServiceTypes = configuredServiceTypes;
			this.ConstructorInternal(false);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00048B66 File Offset: 0x00046D66
		public ServiceTypeNotInConfigurationExploreToDataRouterException(string serviceType, string configuredServiceTypes, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ServiceType = serviceType;
			this.ConfiguredServiceTypes = configuredServiceTypes;
			this.ConstructorInternal(false);
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00048B94 File Offset: 0x00046D94
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00048BCC File Offset: 0x00046DCC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ServiceTypeNotInConfigurationExploreToDataRouterException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ServiceTypeNotInConfigurationExploreToDataRouterException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ServiceType != null)
			{
				info.AddValue("ServiceTypeNotInConfigurationExploreToDataRouterException_ServiceType", this.ServiceType, typeof(string));
			}
			if (this.ConfiguredServiceTypes != null)
			{
				info.AddValue("ServiceTypeNotInConfigurationExploreToDataRouterException_ConfiguredServiceTypes", this.ConfiguredServiceTypes, typeof(string));
			}
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00048C70 File Offset: 0x00046E70
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Service type {0} not found among types: '{1}'! Use different way to obtain a router or add Service Type to configuration.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ServiceType != null) ? this.ServiceType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ServiceType != null) ? this.ServiceType.MarkIfInternal() : string.Empty) : ((this.ServiceType != null) ? this.ServiceType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.ConfiguredServiceTypes != null) ? this.ConfiguredServiceTypes.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ConfiguredServiceTypes != null) ? this.ConfiguredServiceTypes.MarkIfInternal() : string.Empty) : ((this.ConfiguredServiceTypes != null) ? this.ConfiguredServiceTypes.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x00048D4A File Offset: 0x00046F4A
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

		// Token: 0x060011CA RID: 4554 RVA: 0x00048D68 File Offset: 0x00046F68
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ServiceType={0}", (this.ServiceType != null) ? this.ServiceType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ServiceType={0}", (this.ServiceType != null) ? this.ServiceType.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ServiceType={0}", (this.ServiceType != null) ? this.ServiceType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ConfiguredServiceTypes={0}", (this.ConfiguredServiceTypes != null) ? this.ConfiguredServiceTypes.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ConfiguredServiceTypes={0}", (this.ConfiguredServiceTypes != null) ? this.ConfiguredServiceTypes.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ConfiguredServiceTypes={0}", (this.ConfiguredServiceTypes != null) ? this.ConfiguredServiceTypes.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00048ED4 File Offset: 0x000470D4
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00048EDD File Offset: 0x000470DD
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00048EE6 File Offset: 0x000470E6
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00048ED4 File Offset: 0x000470D4
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00048EF0 File Offset: 0x000470F0
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

		// Token: 0x04000422 RID: 1058
		private string creationMessage;

		// Token: 0x04000423 RID: 1059
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000424 RID: 1060
		private string m_serviceType;

		// Token: 0x04000425 RID: 1061
		private string m_configuredServiceTypes;
	}
}
