using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x02000509 RID: 1289
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CommunicationFrameworkMissingCertificateException : CommunicationFrameworkException
	{
		// Token: 0x060027C6 RID: 10182 RVA: 0x0008EFA4 File Offset: 0x0008D1A4
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060027C7 RID: 10183 RVA: 0x0008EFAC File Offset: 0x0008D1AC
		// (set) Token: 0x060027C8 RID: 10184 RVA: 0x0008EFB4 File Offset: 0x0008D1B4
		public string CertificateKey
		{
			get
			{
				return this.m_certificateKey;
			}
			protected set
			{
				this.m_certificateKey = value;
			}
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x0008EFBD File Offset: 0x0008D1BD
		public CommunicationFrameworkMissingCertificateException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x0008EFD1 File Offset: 0x0008D1D1
		public CommunicationFrameworkMissingCertificateException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x0008EFE8 File Offset: 0x0008D1E8
		public CommunicationFrameworkMissingCertificateException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x0008F008 File Offset: 0x0008D208
		protected CommunicationFrameworkMissingCertificateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CommunicationFrameworkMissingCertificateException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.CertificateKey = (string)info.GetValue("CommunicationFrameworkMissingCertificateException_CertificateKey", typeof(string));
			}
			catch (SerializationException)
			{
				this.CertificateKey = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CommunicationFrameworkMissingCertificateException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x0008F0DC File Offset: 0x0008D2DC
		public CommunicationFrameworkMissingCertificateException(string certificateKey, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.CertificateKey = certificateKey;
			this.ConstructorInternal(false);
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x0008F0FA File Offset: 0x0008D2FA
		public CommunicationFrameworkMissingCertificateException(string certificateKey, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.CertificateKey = certificateKey;
			this.ConstructorInternal(false);
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x0008F120 File Offset: 0x0008D320
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x0008F157 File Offset: 0x0008D357
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x0008F160 File Offset: 0x0008D360
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CommunicationFrameworkMissingCertificateException))
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

		// Token: 0x060027D2 RID: 10194 RVA: 0x0008F230 File Offset: 0x0008D430
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CommunicationFrameworkMissingCertificateException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CommunicationFrameworkMissingCertificateException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.CertificateKey != null)
			{
				info.AddValue("CommunicationFrameworkMissingCertificateException_CertificateKey", this.CertificateKey, typeof(string));
			}
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x0008F2B0 File Offset: 0x0008D4B0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Could not retrieve certificate with key '{0}' from certificate provider", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.CertificateKey != null) ? this.CertificateKey.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.CertificateKey != null) ? this.CertificateKey.MarkIfInternal() : string.Empty) : ((this.CertificateKey != null) ? this.CertificateKey.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060027D4 RID: 10196 RVA: 0x0008F334 File Offset: 0x0008D534
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

		// Token: 0x060027D5 RID: 10197 RVA: 0x0008F354 File Offset: 0x0008D554
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "CertificateKey={0}", new object[] { (this.CertificateKey != null) ? this.CertificateKey.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "CertificateKey={0}", new object[] { (this.CertificateKey != null) ? this.CertificateKey.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "CertificateKey={0}", new object[] { (this.CertificateKey != null) ? this.CertificateKey.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x0008F433 File Offset: 0x0008D633
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x0008F43C File Offset: 0x0008D63C
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x0008F445 File Offset: 0x0008D645
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x0008F433 File Offset: 0x0008D633
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x0008F450 File Offset: 0x0008D650
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

		// Token: 0x04000DDF RID: 3551
		private string creationMessage;

		// Token: 0x04000DE0 RID: 3552
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000DE1 RID: 3553
		private string m_certificateKey;
	}
}
