using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Security
{
	// Token: 0x0200006B RID: 107
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CertificateNotFoundInStoreException : SecretManagerException
	{
		// Token: 0x0600030C RID: 780 RVA: 0x0000B6E0 File Offset: 0x000098E0
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000B6E8 File Offset: 0x000098E8
		// (set) Token: 0x0600030E RID: 782 RVA: 0x0000B6F0 File Offset: 0x000098F0
		public string CN
		{
			get
			{
				return this.m_cN;
			}
			protected set
			{
				this.m_cN = value;
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000B6F9 File Offset: 0x000098F9
		public CertificateNotFoundInStoreException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000B70D File Offset: 0x0000990D
		public CertificateNotFoundInStoreException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000B724 File Offset: 0x00009924
		public CertificateNotFoundInStoreException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000B744 File Offset: 0x00009944
		protected CertificateNotFoundInStoreException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CertificateNotFoundInStoreException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.CN = (string)info.GetValue("CertificateNotFoundInStoreException_CN", typeof(string));
			}
			catch (SerializationException)
			{
				this.CN = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CertificateNotFoundInStoreException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000B818 File Offset: 0x00009A18
		public CertificateNotFoundInStoreException(string cN, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.CN = cN;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000B836 File Offset: 0x00009A36
		public CertificateNotFoundInStoreException(string cN, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.CN = cN;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000B85C File Offset: 0x00009A5C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000B893 File Offset: 0x00009A93
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000B89C File Offset: 0x00009A9C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CertificateNotFoundInStoreException))
			{
				TraceSourceBase<SecretManagerTracer>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<SecretManagerTracer>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<SecretManagerTracer>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000B96C File Offset: 0x00009B6C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CertificateNotFoundInStoreException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CertificateNotFoundInStoreException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.CN != null)
			{
				info.AddValue("CertificateNotFoundInStoreException_CN", this.CN, typeof(string));
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000B9EC File Offset: 0x00009BEC
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Could not find certificate '{0}' in the local certificates store", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.CN != null) ? this.CN.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.CN != null) ? this.CN.MarkIfInternal() : string.Empty) : ((this.CN != null) ? this.CN.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000BA70 File Offset: 0x00009C70
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

		// Token: 0x0600031B RID: 795 RVA: 0x0000BA90 File Offset: 0x00009C90
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "CN={0}", new object[] { (this.CN != null) ? this.CN.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "CN={0}", new object[] { (this.CN != null) ? this.CN.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "CN={0}", new object[] { (this.CN != null) ? this.CN.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000BB6F File Offset: 0x00009D6F
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000BB78 File Offset: 0x00009D78
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000BB81 File Offset: 0x00009D81
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000BB6F File Offset: 0x00009D6F
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000BB8C File Offset: 0x00009D8C
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

		// Token: 0x04000112 RID: 274
		private string creationMessage;

		// Token: 0x04000113 RID: 275
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000114 RID: 276
		private string m_cN;
	}
}
