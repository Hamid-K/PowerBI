using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200030E RID: 782
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ThrottlerCancelOperationException : MonitoredException
	{
		// Token: 0x060015CB RID: 5579 RVA: 0x0004DF0C File Offset: 0x0004C10C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x0004DF14 File Offset: 0x0004C114
		public ThrottlerCancelOperationException()
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x0004DF23 File Offset: 0x0004C123
		public ThrottlerCancelOperationException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0004DF3A File Offset: 0x0004C13A
		public ThrottlerCancelOperationException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x0004DF58 File Offset: 0x0004C158
		protected ThrottlerCancelOperationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ThrottlerCancelOperationException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ThrottlerCancelOperationException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x0004DFF4 File Offset: 0x0004C1F4
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x0004E02B File Offset: 0x0004C22B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x0004E034 File Offset: 0x0004C234
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(ThrottlerCancelOperationException))
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

		// Token: 0x060015D3 RID: 5587 RVA: 0x0004E104 File Offset: 0x0004C304
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ThrottlerCancelOperationException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ThrottlerCancelOperationException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0004E15F File Offset: 0x0004C35F
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "A pending operation has been canceled.", new object[0]);
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x0004E176 File Offset: 0x0004C376
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

		// Token: 0x060015D6 RID: 5590 RVA: 0x00003A57 File Offset: 0x00001C57
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string newLine = Environment.NewLine;
			return base.GetPropertiesString(markupKind);
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x0004E193 File Offset: 0x0004C393
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x0004E19C File Offset: 0x0004C39C
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x0004E1A5 File Offset: 0x0004C3A5
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0004E193 File Offset: 0x0004C393
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0004E1B0 File Offset: 0x0004C3B0
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

		// Token: 0x040007EA RID: 2026
		private string creationMessage;

		// Token: 0x040007EB RID: 2027
		private ExceptionCulprit exceptionCulprit;
	}
}
