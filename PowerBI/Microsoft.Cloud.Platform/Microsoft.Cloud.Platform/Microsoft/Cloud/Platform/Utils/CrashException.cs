using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002FF RID: 767
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CrashException : MonitoredException
	{
		// Token: 0x060014A3 RID: 5283 RVA: 0x000487B8 File Offset: 0x000469B8
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x000487C0 File Offset: 0x000469C0
		public CrashException()
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x000487CF File Offset: 0x000469CF
		public CrashException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x000487E6 File Offset: 0x000469E6
		public CrashException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x00048804 File Offset: 0x00046A04
		protected CrashException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CrashException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CrashException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x000488A0 File Offset: 0x00046AA0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x000488D7 File Offset: 0x00046AD7
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x000034FD File Offset: 0x000016FD
		public override bool IsFatal()
		{
			return true;
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x000488E0 File Offset: 0x00046AE0
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CrashException))
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

		// Token: 0x060014AC RID: 5292 RVA: 0x000489B0 File Offset: 0x00046BB0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CrashException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CrashException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x00048A0C File Offset: 0x00046C0C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Process has crashed (exception thrown instead of calling FailFast/Slow): {0}", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((base.InnerException != null && base.InnerException.Message != null) ? base.InnerException.Message : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((base.InnerException != null && base.InnerException.Message != null) ? base.InnerException.Message : string.Empty) : ((base.InnerException != null && base.InnerException.Message != null) ? base.InnerException.Message : string.Empty)) });
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x00048AB1 File Offset: 0x00046CB1
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

		// Token: 0x060014AF RID: 5295 RVA: 0x00003A57 File Offset: 0x00001C57
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string newLine = Environment.NewLine;
			return base.GetPropertiesString(markupKind);
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x00048ACE File Offset: 0x00046CCE
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x00048AD7 File Offset: 0x00046CD7
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x00048AE0 File Offset: 0x00046CE0
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x00048ACE File Offset: 0x00046CCE
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x00048AEC File Offset: 0x00046CEC
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

		// Token: 0x040007C2 RID: 1986
		private string creationMessage;

		// Token: 0x040007C3 RID: 1987
		private ExceptionCulprit exceptionCulprit;
	}
}
