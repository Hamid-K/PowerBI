using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000301 RID: 769
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class NonObservedExceptionWrapperException : MonitoredException
	{
		// Token: 0x060014C6 RID: 5318 RVA: 0x000491F8 File Offset: 0x000473F8
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x00049200 File Offset: 0x00047400
		public NonObservedExceptionWrapperException()
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0004920F File Offset: 0x0004740F
		public NonObservedExceptionWrapperException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x00049226 File Offset: 0x00047426
		public NonObservedExceptionWrapperException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x00049244 File Offset: 0x00047444
		protected NonObservedExceptionWrapperException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("NonObservedExceptionWrapperException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("NonObservedExceptionWrapperException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x000492E0 File Offset: 0x000474E0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x00049317 File Offset: 0x00047517
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00049320 File Offset: 0x00047520
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(NonObservedExceptionWrapperException))
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

		// Token: 0x060014CE RID: 5326 RVA: 0x000493F0 File Offset: 0x000475F0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("NonObservedExceptionWrapperException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("NonObservedExceptionWrapperException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0004944C File Offset: 0x0004764C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Non-observed exception wrapper: {0}", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((base.InnerException != null && base.InnerException.Message != null) ? base.InnerException.Message : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((base.InnerException != null && base.InnerException.Message != null) ? base.InnerException.Message : string.Empty) : ((base.InnerException != null && base.InnerException.Message != null) ? base.InnerException.Message : string.Empty)) });
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x000494F1 File Offset: 0x000476F1
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

		// Token: 0x060014D1 RID: 5329 RVA: 0x00003A57 File Offset: 0x00001C57
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string newLine = Environment.NewLine;
			return base.GetPropertiesString(markupKind);
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0004950E File Offset: 0x0004770E
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00049517 File Offset: 0x00047717
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00049520 File Offset: 0x00047720
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0004950E File Offset: 0x0004770E
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0004952C File Offset: 0x0004772C
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

		// Token: 0x040007C6 RID: 1990
		private string creationMessage;

		// Token: 0x040007C7 RID: 1991
		private ExceptionCulprit exceptionCulprit;
	}
}
