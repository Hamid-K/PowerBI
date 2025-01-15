using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000DF RID: 223
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class InitializationErrorException : MonitoredException
	{
		// Token: 0x06000649 RID: 1609 RVA: 0x000163E8 File Offset: 0x000145E8
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x000163F0 File Offset: 0x000145F0
		public InitializationErrorException()
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x000163FF File Offset: 0x000145FF
		public InitializationErrorException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00016416 File Offset: 0x00014616
		public InitializationErrorException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00016434 File Offset: 0x00014634
		protected InitializationErrorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("InitializationErrorException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("InitializationErrorException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x000164D0 File Offset: 0x000146D0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00016507 File Offset: 0x00014707
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00016510 File Offset: 0x00014710
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(InitializationErrorException))
			{
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<ModularizationFrameworkTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<ModularizationFrameworkTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x000165E0 File Offset: 0x000147E0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("InitializationErrorException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("InitializationErrorException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001663C File Offset: 0x0001483C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? (base.Message ?? string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? (base.Message ?? string.Empty) : (base.Message ?? string.Empty)) });
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00016699 File Offset: 0x00014899
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

		// Token: 0x06000654 RID: 1620 RVA: 0x00003A57 File Offset: 0x00001C57
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string newLine = Environment.NewLine;
			return base.GetPropertiesString(markupKind);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000166B6 File Offset: 0x000148B6
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x000166BF File Offset: 0x000148BF
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x000166C8 File Offset: 0x000148C8
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x000166B6 File Offset: 0x000148B6
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x000166D4 File Offset: 0x000148D4
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

		// Token: 0x0400022E RID: 558
		private string creationMessage;

		// Token: 0x0400022F RID: 559
		private ExceptionCulprit exceptionCulprit;
	}
}
