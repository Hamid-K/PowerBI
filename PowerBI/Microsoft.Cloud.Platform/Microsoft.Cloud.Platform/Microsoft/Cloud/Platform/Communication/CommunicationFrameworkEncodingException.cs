using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x02000503 RID: 1283
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CommunicationFrameworkEncodingException : CommunicationFrameworkException
	{
		// Token: 0x0600275B RID: 10075 RVA: 0x0008D288 File Offset: 0x0008B488
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x0600275C RID: 10076 RVA: 0x0008D290 File Offset: 0x0008B490
		// (set) Token: 0x0600275D RID: 10077 RVA: 0x0008D298 File Offset: 0x0008B498
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

		// Token: 0x0600275E RID: 10078 RVA: 0x0008D2A1 File Offset: 0x0008B4A1
		public CommunicationFrameworkEncodingException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<ServiceDetails>();
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x0008D2B5 File Offset: 0x0008B4B5
		public CommunicationFrameworkEncodingException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x0008D2CC File Offset: 0x0008B4CC
		public CommunicationFrameworkEncodingException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x0008D2EC File Offset: 0x0008B4EC
		protected CommunicationFrameworkEncodingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CommunicationFrameworkEncodingException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ServiceDetails = (ServiceDetails)info.GetValue("CommunicationFrameworkEncodingException_ServiceDetails", typeof(ServiceDetails));
			}
			catch (SerializationException)
			{
				this.ServiceDetails = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CommunicationFrameworkEncodingException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x0008D3C0 File Offset: 0x0008B5C0
		public CommunicationFrameworkEncodingException(ServiceDetails serviceDetails)
		{
			this.ServiceDetails = serviceDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x0008D3D6 File Offset: 0x0008B5D6
		public CommunicationFrameworkEncodingException(ServiceDetails serviceDetails, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ServiceDetails = serviceDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x0008D3F4 File Offset: 0x0008B5F4
		public CommunicationFrameworkEncodingException(ServiceDetails serviceDetails, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ServiceDetails = serviceDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x0008D418 File Offset: 0x0008B618
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x0008D44F File Offset: 0x0008B64F
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x0008D458 File Offset: 0x0008B658
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CommunicationFrameworkEncodingException))
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

		// Token: 0x06002768 RID: 10088 RVA: 0x0008D528 File Offset: 0x0008B728
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CommunicationFrameworkEncodingException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CommunicationFrameworkEncodingException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ServiceDetails != null)
			{
				info.AddValue("CommunicationFrameworkEncodingException_ServiceDetails", this.ServiceDetails, typeof(ServiceDetails));
			}
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x0008D5A6 File Offset: 0x0008B7A6
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Error encoding the message", new object[0]);
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x0600276A RID: 10090 RVA: 0x0008D5BD File Offset: 0x0008B7BD
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

		// Token: 0x0600276B RID: 10091 RVA: 0x0008D5DC File Offset: 0x0008B7DC
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ServiceDetails={0}", new object[] { (this.ServiceDetails != null) ? this.ServiceDetails.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ServiceDetails={0}", new object[] { (this.ServiceDetails != null) ? this.ServiceDetails.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "ServiceDetails={0}", new object[] { (this.ServiceDetails != null) ? this.ServiceDetails.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x0008D6BB File Offset: 0x0008B8BB
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x0008D6C4 File Offset: 0x0008B8C4
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x0008D6CD File Offset: 0x0008B8CD
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x0008D6BB File Offset: 0x0008B8BB
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x0008D6D8 File Offset: 0x0008B8D8
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

		// Token: 0x04000DD2 RID: 3538
		private string creationMessage;

		// Token: 0x04000DD3 RID: 3539
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000DD4 RID: 3540
		private ServiceDetails m_serviceDetails;
	}
}
