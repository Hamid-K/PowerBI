using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Security
{
	// Token: 0x0200006C RID: 108
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CertificateKeyNotSupportedException : SecretManagerException
	{
		// Token: 0x06000321 RID: 801 RVA: 0x0000BD78 File Offset: 0x00009F78
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000BD80 File Offset: 0x00009F80
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0000BD88 File Offset: 0x00009F88
		public string Key
		{
			get
			{
				return this.m_key;
			}
			protected set
			{
				this.m_key = value;
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000BD91 File Offset: 0x00009F91
		public CertificateKeyNotSupportedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000BDA5 File Offset: 0x00009FA5
		public CertificateKeyNotSupportedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000BDBC File Offset: 0x00009FBC
		public CertificateKeyNotSupportedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000BDDC File Offset: 0x00009FDC
		protected CertificateKeyNotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CertificateKeyNotSupportedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Key = (string)info.GetValue("CertificateKeyNotSupportedException_Key", typeof(string));
			}
			catch (SerializationException)
			{
				this.Key = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CertificateKeyNotSupportedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000BEB0 File Offset: 0x0000A0B0
		public CertificateKeyNotSupportedException(string key, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Key = key;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000BECE File Offset: 0x0000A0CE
		public CertificateKeyNotSupportedException(string key, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Key = key;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000BEF4 File Offset: 0x0000A0F4
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000BF2B File Offset: 0x0000A12B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000BF34 File Offset: 0x0000A134
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CertificateKeyNotSupportedException))
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

		// Token: 0x0600032D RID: 813 RVA: 0x0000C004 File Offset: 0x0000A204
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CertificateKeyNotSupportedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CertificateKeyNotSupportedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Key != null)
			{
				info.AddValue("CertificateKeyNotSupportedException_Key", this.Key, typeof(string));
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000C084 File Offset: 0x0000A284
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The certificate key '{0}' is not supported.", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.Key != null) ? this.Key.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Key != null) ? this.Key.MarkIfInternal() : string.Empty) : ((this.Key != null) ? this.Key.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000C108 File Offset: 0x0000A308
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

		// Token: 0x06000330 RID: 816 RVA: 0x0000C128 File Offset: 0x0000A328
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Key={0}", new object[] { (this.Key != null) ? this.Key.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Key={0}", new object[] { (this.Key != null) ? this.Key.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Key={0}", new object[] { (this.Key != null) ? this.Key.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000C207 File Offset: 0x0000A407
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000C210 File Offset: 0x0000A410
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000C219 File Offset: 0x0000A419
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000C207 File Offset: 0x0000A407
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000C224 File Offset: 0x0000A424
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

		// Token: 0x04000115 RID: 277
		private string creationMessage;

		// Token: 0x04000116 RID: 278
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000117 RID: 279
		private string m_key;
	}
}
