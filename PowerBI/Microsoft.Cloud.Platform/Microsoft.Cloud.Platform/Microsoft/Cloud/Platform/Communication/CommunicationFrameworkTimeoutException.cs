using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004FB RID: 1275
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CommunicationFrameworkTimeoutException : CommunicationFrameworkException
	{
		// Token: 0x060026BD RID: 9917 RVA: 0x0008A4F0 File Offset: 0x000886F0
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060026BE RID: 9918 RVA: 0x0008A4F8 File Offset: 0x000886F8
		// (set) Token: 0x060026BF RID: 9919 RVA: 0x0008A500 File Offset: 0x00088700
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

		// Token: 0x060026C0 RID: 9920 RVA: 0x0008A509 File Offset: 0x00088709
		public CommunicationFrameworkTimeoutException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<ServiceDetails>();
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x0008A51D File Offset: 0x0008871D
		public CommunicationFrameworkTimeoutException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x0008A534 File Offset: 0x00088734
		public CommunicationFrameworkTimeoutException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x0008A554 File Offset: 0x00088754
		protected CommunicationFrameworkTimeoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CommunicationFrameworkTimeoutException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ServiceDetails = (ServiceDetails)info.GetValue("CommunicationFrameworkTimeoutException_ServiceDetails", typeof(ServiceDetails));
			}
			catch (SerializationException)
			{
				this.ServiceDetails = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CommunicationFrameworkTimeoutException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x0008A628 File Offset: 0x00088828
		public CommunicationFrameworkTimeoutException(ServiceDetails serviceDetails)
		{
			this.ServiceDetails = serviceDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x0008A63E File Offset: 0x0008883E
		public CommunicationFrameworkTimeoutException(ServiceDetails serviceDetails, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ServiceDetails = serviceDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x0008A65C File Offset: 0x0008885C
		public CommunicationFrameworkTimeoutException(ServiceDetails serviceDetails, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ServiceDetails = serviceDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x0008A680 File Offset: 0x00088880
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x0008A6B7 File Offset: 0x000888B7
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x0008A6C0 File Offset: 0x000888C0
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CommunicationFrameworkTimeoutException))
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

		// Token: 0x060026CA RID: 9930 RVA: 0x0008A790 File Offset: 0x00088990
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CommunicationFrameworkTimeoutException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CommunicationFrameworkTimeoutException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ServiceDetails != null)
			{
				info.AddValue("CommunicationFrameworkTimeoutException_ServiceDetails", this.ServiceDetails, typeof(ServiceDetails));
			}
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x0008A80E File Offset: 0x00088A0E
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The operation failed to complete due to timeout", new object[0]);
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060026CC RID: 9932 RVA: 0x0008A825 File Offset: 0x00088A25
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

		// Token: 0x060026CD RID: 9933 RVA: 0x0008A844 File Offset: 0x00088A44
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ServiceDetails={0}", new object[] { (this.ServiceDetails != null) ? this.ServiceDetails.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ServiceDetails={0}", new object[] { (this.ServiceDetails != null) ? this.ServiceDetails.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "ServiceDetails={0}", new object[] { (this.ServiceDetails != null) ? this.ServiceDetails.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x0008A923 File Offset: 0x00088B23
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x0008A92C File Offset: 0x00088B2C
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x0008A935 File Offset: 0x00088B35
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x0008A923 File Offset: 0x00088B23
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x0008A940 File Offset: 0x00088B40
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

		// Token: 0x04000DBD RID: 3517
		private string creationMessage;

		// Token: 0x04000DBE RID: 3518
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000DBF RID: 3519
		private ServiceDetails m_serviceDetails;
	}
}
