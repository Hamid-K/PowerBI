using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Configuration;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x0200041D RID: 1053
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CcsMissingCertificateException : ConfigurationException
	{
		// Token: 0x0600206E RID: 8302 RVA: 0x0007A368 File Offset: 0x00078568
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600206F RID: 8303 RVA: 0x0007A370 File Offset: 0x00078570
		// (set) Token: 0x06002070 RID: 8304 RVA: 0x0007A378 File Offset: 0x00078578
		public string Thumbprint
		{
			get
			{
				return this.m_thumbprint;
			}
			protected set
			{
				this.m_thumbprint = value;
			}
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x0007A381 File Offset: 0x00078581
		public CcsMissingCertificateException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x0007A395 File Offset: 0x00078595
		public CcsMissingCertificateException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x0007A3AC File Offset: 0x000785AC
		public CcsMissingCertificateException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x0007A3CC File Offset: 0x000785CC
		protected CcsMissingCertificateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CcsMissingCertificateException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Thumbprint = (string)info.GetValue("CcsMissingCertificateException_Thumbprint", typeof(string));
			}
			catch (SerializationException)
			{
				this.Thumbprint = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CcsMissingCertificateException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x0007A4A0 File Offset: 0x000786A0
		public CcsMissingCertificateException(string thumbprint, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Thumbprint = thumbprint;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x0007A4BE File Offset: 0x000786BE
		public CcsMissingCertificateException(string thumbprint, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Thumbprint = thumbprint;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x0007A4E4 File Offset: 0x000786E4
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x0007A51B File Offset: 0x0007871B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x0007A524 File Offset: 0x00078724
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CcsMissingCertificateException))
			{
				TraceSourceBase<ConfigurationTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<ConfigurationTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<ConfigurationTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x0007A5F4 File Offset: 0x000787F4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CcsMissingCertificateException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CcsMissingCertificateException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Thumbprint != null)
			{
				info.AddValue("CcsMissingCertificateException_Thumbprint", this.Thumbprint, typeof(string));
			}
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x0007A674 File Offset: 0x00078874
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The certificate used to encrypt/decrypt configuration properties (thumbprint is {0}) was not found", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.Thumbprint != null) ? this.Thumbprint.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Thumbprint != null) ? this.Thumbprint.MarkIfInternal() : string.Empty) : ((this.Thumbprint != null) ? this.Thumbprint.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x0600207C RID: 8316 RVA: 0x0007A6F8 File Offset: 0x000788F8
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

		// Token: 0x0600207D RID: 8317 RVA: 0x0007A718 File Offset: 0x00078918
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Thumbprint={0}", new object[] { (this.Thumbprint != null) ? this.Thumbprint.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Thumbprint={0}", new object[] { (this.Thumbprint != null) ? this.Thumbprint.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Thumbprint={0}", new object[] { (this.Thumbprint != null) ? this.Thumbprint.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x0007A7F7 File Offset: 0x000789F7
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x0007A800 File Offset: 0x00078A00
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x0007A809 File Offset: 0x00078A09
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x0007A7F7 File Offset: 0x000789F7
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x0007A814 File Offset: 0x00078A14
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

		// Token: 0x04000B2B RID: 2859
		private string creationMessage;

		// Token: 0x04000B2C RID: 2860
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000B2D RID: 2861
		private string m_thumbprint;
	}
}
