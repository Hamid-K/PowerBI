using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000300 RID: 768
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class NonMonitoredExceptionWrapperException : MonitoredException
	{
		// Token: 0x060014B5 RID: 5301 RVA: 0x00048CD8 File Offset: 0x00046ED8
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x00048CE0 File Offset: 0x00046EE0
		public NonMonitoredExceptionWrapperException()
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x00048CEF File Offset: 0x00046EEF
		public NonMonitoredExceptionWrapperException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x00048D06 File Offset: 0x00046F06
		public NonMonitoredExceptionWrapperException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x00048D24 File Offset: 0x00046F24
		protected NonMonitoredExceptionWrapperException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("NonMonitoredExceptionWrapperException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("NonMonitoredExceptionWrapperException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x00048DC0 File Offset: 0x00046FC0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x00048DF7 File Offset: 0x00046FF7
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x00048E00 File Offset: 0x00047000
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(NonMonitoredExceptionWrapperException))
			{
				TraceSourceBase<UtilsTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<UtilsTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<UtilsTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x00048ED0 File Offset: 0x000470D0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("NonMonitoredExceptionWrapperException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("NonMonitoredExceptionWrapperException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x00048F2C File Offset: 0x0004712C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Non-monitored exception wrapper: {0}", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((base.InnerException != null && base.InnerException.Message != null) ? base.InnerException.Message : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((base.InnerException != null && base.InnerException.Message != null) ? base.InnerException.Message : string.Empty) : ((base.InnerException != null && base.InnerException.Message != null) ? base.InnerException.Message : string.Empty)) });
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x00048FD1 File Offset: 0x000471D1
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

		// Token: 0x060014C0 RID: 5312 RVA: 0x00003A57 File Offset: 0x00001C57
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string newLine = Environment.NewLine;
			return base.GetPropertiesString(markupKind);
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x00048FEE File Offset: 0x000471EE
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x00048FF7 File Offset: 0x000471F7
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x00049000 File Offset: 0x00047200
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x00048FEE File Offset: 0x000471EE
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0004900C File Offset: 0x0004720C
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

		// Token: 0x040007C4 RID: 1988
		private string creationMessage;

		// Token: 0x040007C5 RID: 1989
		private ExceptionCulprit exceptionCulprit;
	}
}
