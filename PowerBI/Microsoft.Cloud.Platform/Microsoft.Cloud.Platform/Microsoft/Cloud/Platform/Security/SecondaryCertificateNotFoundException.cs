using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Security
{
	// Token: 0x0200006D RID: 109
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class SecondaryCertificateNotFoundException : SecretManagerException
	{
		// Token: 0x06000336 RID: 822 RVA: 0x0000C410 File Offset: 0x0000A610
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000C418 File Offset: 0x0000A618
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0000C420 File Offset: 0x0000A620
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

		// Token: 0x06000339 RID: 825 RVA: 0x0000C429 File Offset: 0x0000A629
		public SecondaryCertificateNotFoundException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000C43D File Offset: 0x0000A63D
		public SecondaryCertificateNotFoundException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000C454 File Offset: 0x0000A654
		public SecondaryCertificateNotFoundException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000C474 File Offset: 0x0000A674
		protected SecondaryCertificateNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("SecondaryCertificateNotFoundException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.CN = (string)info.GetValue("SecondaryCertificateNotFoundException_CN", typeof(string));
			}
			catch (SerializationException)
			{
				this.CN = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("SecondaryCertificateNotFoundException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000C548 File Offset: 0x0000A748
		public SecondaryCertificateNotFoundException(string cN, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.CN = cN;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000C566 File Offset: 0x0000A766
		public SecondaryCertificateNotFoundException(string cN, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.CN = cN;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000C58C File Offset: 0x0000A78C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000C5C3 File Offset: 0x0000A7C3
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000C5CC File Offset: 0x0000A7CC
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(SecondaryCertificateNotFoundException))
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

		// Token: 0x06000342 RID: 834 RVA: 0x0000C69C File Offset: 0x0000A89C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("SecondaryCertificateNotFoundException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("SecondaryCertificateNotFoundException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.CN != null)
			{
				info.AddValue("SecondaryCertificateNotFoundException_CN", this.CN, typeof(string));
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000C71C File Offset: 0x0000A91C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "A secondary certificate for CN '{0}' not found", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.CN != null) ? this.CN.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.CN != null) ? this.CN.MarkIfInternal() : string.Empty) : ((this.CN != null) ? this.CN.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
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

		// Token: 0x06000345 RID: 837 RVA: 0x0000C7C0 File Offset: 0x0000A9C0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "CN={0}", new object[] { (this.CN != null) ? this.CN.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "CN={0}", new object[] { (this.CN != null) ? this.CN.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "CN={0}", new object[] { (this.CN != null) ? this.CN.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000C89F File Offset: 0x0000AA9F
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000C8A8 File Offset: 0x0000AAA8
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000C8B1 File Offset: 0x0000AAB1
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000C89F File Offset: 0x0000AA9F
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000C8BC File Offset: 0x0000AABC
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

		// Token: 0x04000118 RID: 280
		private string creationMessage;

		// Token: 0x04000119 RID: 281
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400011A RID: 282
		private string m_cN;
	}
}
