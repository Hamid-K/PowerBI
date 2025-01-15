using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x0200005D RID: 93
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class DatabaseTimeoutException : DatabaseException
	{
		// Token: 0x0600028C RID: 652 RVA: 0x000092E0 File Offset: 0x000074E0
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600028D RID: 653 RVA: 0x000092E8 File Offset: 0x000074E8
		// (set) Token: 0x0600028E RID: 654 RVA: 0x000092F0 File Offset: 0x000074F0
		public TimeSpan Timeout
		{
			get
			{
				return this.m_timeout;
			}
			protected set
			{
				this.m_timeout = value;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000092F9 File Offset: 0x000074F9
		public DatabaseTimeoutException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidValueField<TimeSpan>();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000930D File Offset: 0x0000750D
		public DatabaseTimeoutException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00009324 File Offset: 0x00007524
		public DatabaseTimeoutException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00009344 File Offset: 0x00007544
		protected DatabaseTimeoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("DatabaseTimeoutException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.Timeout = (TimeSpan)info.GetValue("DatabaseTimeoutException_Timeout", typeof(TimeSpan));
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("DatabaseTimeoutException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00009400 File Offset: 0x00007600
		public DatabaseTimeoutException(TimeSpan timeout)
		{
			this.Timeout = timeout;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00009416 File Offset: 0x00007616
		public DatabaseTimeoutException(TimeSpan timeout, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Timeout = timeout;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00009434 File Offset: 0x00007634
		public DatabaseTimeoutException(TimeSpan timeout, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Timeout = timeout;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00009458 File Offset: 0x00007658
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000948F File Offset: 0x0000768F
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00009498 File Offset: 0x00007698
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(DatabaseTimeoutException))
			{
				TraceSourceBase<StorageTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<StorageTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<StorageTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00009568 File Offset: 0x00007768
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("DatabaseTimeoutException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("DatabaseTimeoutException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			info.AddValue("DatabaseTimeoutException_Timeout", this.Timeout, typeof(TimeSpan));
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000095E4 File Offset: 0x000077E4
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The Database operation reached a timeout of '{0}'", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? this.Timeout.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.Timeout.ToString() : this.Timeout.ToString()) });
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00009650 File Offset: 0x00007850
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

		// Token: 0x0600029C RID: 668 RVA: 0x00009670 File Offset: 0x00007870
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Timeout={0}", new object[] { this.Timeout.ToString() }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Timeout={0}", new object[] { this.Timeout.ToString() }) : string.Format(CultureInfo.CurrentCulture, "Timeout={0}", new object[] { this.Timeout.ToString() })));
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00009737 File Offset: 0x00007937
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00009740 File Offset: 0x00007940
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00009749 File Offset: 0x00007949
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00009737 File Offset: 0x00007937
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00009754 File Offset: 0x00007954
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

		// Token: 0x040000F3 RID: 243
		private string creationMessage;

		// Token: 0x040000F4 RID: 244
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040000F5 RID: 245
		private TimeSpan m_timeout;
	}
}
