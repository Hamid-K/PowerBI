using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Security
{
	// Token: 0x02000069 RID: 105
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class SecretManagerEventAlreadySubscribedException : SecretManagerException
	{
		// Token: 0x060002EA RID: 746 RVA: 0x0000ADB0 File Offset: 0x00008FB0
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000ADB8 File Offset: 0x00008FB8
		public SecretManagerEventAlreadySubscribedException()
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000ADC7 File Offset: 0x00008FC7
		public SecretManagerEventAlreadySubscribedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000ADDE File Offset: 0x00008FDE
		public SecretManagerEventAlreadySubscribedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000ADFC File Offset: 0x00008FFC
		protected SecretManagerEventAlreadySubscribedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("SecretManagerEventAlreadySubscribedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("SecretManagerEventAlreadySubscribedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000AE98 File Offset: 0x00009098
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000AECF File Offset: 0x000090CF
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000AED8 File Offset: 0x000090D8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(SecretManagerEventAlreadySubscribedException))
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

		// Token: 0x060002F2 RID: 754 RVA: 0x0000AFA8 File Offset: 0x000091A8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("SecretManagerEventAlreadySubscribedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("SecretManagerEventAlreadySubscribedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000B003 File Offset: 0x00009203
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Registered event handler can be used only once", new object[0]);
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000B01A File Offset: 0x0000921A
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

		// Token: 0x060002F5 RID: 757 RVA: 0x0000B037 File Offset: 0x00009237
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string newLine = Environment.NewLine;
			return base.GetPropertiesString(markupKind);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000B046 File Offset: 0x00009246
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000B04F File Offset: 0x0000924F
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000B058 File Offset: 0x00009258
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000B046 File Offset: 0x00009246
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000B064 File Offset: 0x00009264
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

		// Token: 0x0400010E RID: 270
		private string creationMessage;

		// Token: 0x0400010F RID: 271
		private ExceptionCulprit exceptionCulprit;
	}
}
