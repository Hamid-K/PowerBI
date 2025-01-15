using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x02000500 RID: 1280
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CommunicationFrameworkCommunicationException : CommunicationFrameworkException
	{
		// Token: 0x0600271E RID: 10014 RVA: 0x0008C180 File Offset: 0x0008A380
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x0600271F RID: 10015 RVA: 0x0008C188 File Offset: 0x0008A388
		// (set) Token: 0x06002720 RID: 10016 RVA: 0x0008C190 File Offset: 0x0008A390
		public ServiceDetails ServiceDetails
		{
			get
			{
				return this.m_serviceDetails;
			}
			protected set
			{
				this.m_serviceDetails = value;
			}
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x0008C199 File Offset: 0x0008A399
		public CommunicationFrameworkCommunicationException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<ServiceDetails>();
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x0008C1AD File Offset: 0x0008A3AD
		public CommunicationFrameworkCommunicationException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x0008C1C4 File Offset: 0x0008A3C4
		public CommunicationFrameworkCommunicationException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x0008C1E4 File Offset: 0x0008A3E4
		protected CommunicationFrameworkCommunicationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CommunicationFrameworkCommunicationException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ServiceDetails = (ServiceDetails)info.GetValue("CommunicationFrameworkCommunicationException_ServiceDetails", typeof(ServiceDetails));
			}
			catch (SerializationException)
			{
				this.ServiceDetails = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CommunicationFrameworkCommunicationException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x0008C2B8 File Offset: 0x0008A4B8
		public CommunicationFrameworkCommunicationException(ServiceDetails serviceDetails)
		{
			this.ServiceDetails = serviceDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x0008C2CE File Offset: 0x0008A4CE
		public CommunicationFrameworkCommunicationException(ServiceDetails serviceDetails, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ServiceDetails = serviceDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x0008C2EC File Offset: 0x0008A4EC
		public CommunicationFrameworkCommunicationException(ServiceDetails serviceDetails, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ServiceDetails = serviceDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x0008C310 File Offset: 0x0008A510
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x0008C347 File Offset: 0x0008A547
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x0008C350 File Offset: 0x0008A550
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CommunicationFrameworkCommunicationException))
			{
				TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<CommunicationFrameworkTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x0008C420 File Offset: 0x0008A620
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CommunicationFrameworkCommunicationException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CommunicationFrameworkCommunicationException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ServiceDetails != null)
			{
				info.AddValue("CommunicationFrameworkCommunicationException_ServiceDetails", this.ServiceDetails, typeof(ServiceDetails));
			}
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x0008C49E File Offset: 0x0008A69E
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Communication error", new object[0]);
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x0600272D RID: 10029 RVA: 0x0008C4B5 File Offset: 0x0008A6B5
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

		// Token: 0x0600272E RID: 10030 RVA: 0x0008C4D4 File Offset: 0x0008A6D4
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ServiceDetails={0}", new object[] { (this.ServiceDetails != null) ? this.ServiceDetails.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ServiceDetails={0}", new object[] { (this.ServiceDetails != null) ? this.ServiceDetails.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "ServiceDetails={0}", new object[] { (this.ServiceDetails != null) ? this.ServiceDetails.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x0008C5B3 File Offset: 0x0008A7B3
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x0008C5BC File Offset: 0x0008A7BC
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x0008C5C5 File Offset: 0x0008A7C5
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x0008C5B3 File Offset: 0x0008A7B3
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x0008C5D0 File Offset: 0x0008A7D0
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

		// Token: 0x04000DCA RID: 3530
		private string creationMessage;

		// Token: 0x04000DCB RID: 3531
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000DCC RID: 3532
		private ServiceDetails m_serviceDetails;
	}
}
