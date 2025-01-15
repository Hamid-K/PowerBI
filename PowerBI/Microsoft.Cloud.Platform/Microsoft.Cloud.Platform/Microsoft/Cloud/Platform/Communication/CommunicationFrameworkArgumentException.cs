using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x02000501 RID: 1281
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CommunicationFrameworkArgumentException : CommunicationFrameworkException
	{
		// Token: 0x06002734 RID: 10036 RVA: 0x0008C7BC File Offset: 0x0008A9BC
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06002735 RID: 10037 RVA: 0x0008C7C4 File Offset: 0x0008A9C4
		// (set) Token: 0x06002736 RID: 10038 RVA: 0x0008C7CC File Offset: 0x0008A9CC
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

		// Token: 0x06002737 RID: 10039 RVA: 0x0008C7D5 File Offset: 0x0008A9D5
		public CommunicationFrameworkArgumentException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<ServiceDetails>();
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x0008C7E9 File Offset: 0x0008A9E9
		public CommunicationFrameworkArgumentException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x0008C800 File Offset: 0x0008AA00
		public CommunicationFrameworkArgumentException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x0008C820 File Offset: 0x0008AA20
		protected CommunicationFrameworkArgumentException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CommunicationFrameworkArgumentException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ServiceDetails = (ServiceDetails)info.GetValue("CommunicationFrameworkArgumentException_ServiceDetails", typeof(ServiceDetails));
			}
			catch (SerializationException)
			{
				this.ServiceDetails = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CommunicationFrameworkArgumentException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x0008C8F4 File Offset: 0x0008AAF4
		public CommunicationFrameworkArgumentException(ServiceDetails serviceDetails)
		{
			this.ServiceDetails = serviceDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x0008C90A File Offset: 0x0008AB0A
		public CommunicationFrameworkArgumentException(ServiceDetails serviceDetails, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ServiceDetails = serviceDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x0008C928 File Offset: 0x0008AB28
		public CommunicationFrameworkArgumentException(ServiceDetails serviceDetails, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ServiceDetails = serviceDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x0008C94C File Offset: 0x0008AB4C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x0008C983 File Offset: 0x0008AB83
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x0008C98C File Offset: 0x0008AB8C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CommunicationFrameworkArgumentException))
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

		// Token: 0x06002741 RID: 10049 RVA: 0x0008CA5C File Offset: 0x0008AC5C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CommunicationFrameworkArgumentException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CommunicationFrameworkArgumentException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ServiceDetails != null)
			{
				info.AddValue("CommunicationFrameworkArgumentException_ServiceDetails", this.ServiceDetails, typeof(ServiceDetails));
			}
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x0008CADA File Offset: 0x0008ACDA
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Invalid Argument", new object[0]);
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06002743 RID: 10051 RVA: 0x0008CAF1 File Offset: 0x0008ACF1
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

		// Token: 0x06002744 RID: 10052 RVA: 0x0008CB10 File Offset: 0x0008AD10
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ServiceDetails={0}", new object[] { (this.ServiceDetails != null) ? this.ServiceDetails.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ServiceDetails={0}", new object[] { (this.ServiceDetails != null) ? this.ServiceDetails.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "ServiceDetails={0}", new object[] { (this.ServiceDetails != null) ? this.ServiceDetails.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x0008CBEF File Offset: 0x0008ADEF
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x0008CBF8 File Offset: 0x0008ADF8
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x0008CC01 File Offset: 0x0008AE01
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x0008CBEF File Offset: 0x0008ADEF
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x0008CC0C File Offset: 0x0008AE0C
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

		// Token: 0x04000DCD RID: 3533
		private string creationMessage;

		// Token: 0x04000DCE RID: 3534
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000DCF RID: 3535
		private ServiceDetails m_serviceDetails;
	}
}
