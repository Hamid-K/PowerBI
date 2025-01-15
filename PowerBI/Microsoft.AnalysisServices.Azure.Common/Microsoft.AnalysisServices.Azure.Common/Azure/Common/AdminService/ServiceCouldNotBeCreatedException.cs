using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.AdminService
{
	// Token: 0x02000124 RID: 292
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ServiceCouldNotBeCreatedException : AdminProvisioningServiceException
	{
		// Token: 0x06000F3A RID: 3898 RVA: 0x0003CBAC File Offset: 0x0003ADAC
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000F3B RID: 3899 RVA: 0x0003CBB4 File Offset: 0x0003ADB4
		// (set) Token: 0x06000F3C RID: 3900 RVA: 0x0003CBBC File Offset: 0x0003ADBC
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

		// Token: 0x06000F3D RID: 3901 RVA: 0x0003CBC5 File Offset: 0x0003ADC5
		public ServiceCouldNotBeCreatedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0003CBD9 File Offset: 0x0003ADD9
		public ServiceCouldNotBeCreatedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0003CBF0 File Offset: 0x0003ADF0
		public ServiceCouldNotBeCreatedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0003CC10 File Offset: 0x0003AE10
		protected ServiceCouldNotBeCreatedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ServiceCouldNotBeCreatedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ServiceName = (string)info.GetValue("ServiceCouldNotBeCreatedException_ServiceName", typeof(string));
			}
			catch (SerializationException)
			{
				this.ServiceName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ServiceCouldNotBeCreatedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0003CCE4 File Offset: 0x0003AEE4
		public ServiceCouldNotBeCreatedException(string serviceName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ServiceName = serviceName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0003CD02 File Offset: 0x0003AF02
		public ServiceCouldNotBeCreatedException(string serviceName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ServiceName = serviceName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0003CD28 File Offset: 0x0003AF28
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0003CD5F File Offset: 0x0003AF5F
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0003CD68 File Offset: 0x0003AF68
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(ServiceCouldNotBeCreatedException))
			{
				TraceSourceBase<ANCommonTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<ANCommonTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<ANCommonTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0003CE38 File Offset: 0x0003B038
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ServiceCouldNotBeCreatedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ServiceCouldNotBeCreatedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ServiceName != null)
			{
				info.AddValue("ServiceCouldNotBeCreatedException_ServiceName", this.ServiceName, typeof(string));
			}
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0003CEB8 File Offset: 0x0003B0B8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "WF Service '{0}' could not be created", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ServiceName != null) ? this.ServiceName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ServiceName != null) ? this.ServiceName.MarkIfInternal() : string.Empty) : ((this.ServiceName != null) ? this.ServiceName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x0003CF33 File Offset: 0x0003B133
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

		// Token: 0x06000F49 RID: 3913 RVA: 0x0003CF50 File Offset: 0x0003B150
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ServiceName={0}", (this.ServiceName != null) ? this.ServiceName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ServiceName={0}", (this.ServiceName != null) ? this.ServiceName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ServiceName={0}", (this.ServiceName != null) ? this.ServiceName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0003D014 File Offset: 0x0003B214
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0003D01D File Offset: 0x0003B21D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0003D026 File Offset: 0x0003B226
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0003D014 File Offset: 0x0003B214
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0003D030 File Offset: 0x0003B230
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

		// Token: 0x04000390 RID: 912
		private string creationMessage;

		// Token: 0x04000391 RID: 913
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000392 RID: 914
		private string m_serviceName;
	}
}
