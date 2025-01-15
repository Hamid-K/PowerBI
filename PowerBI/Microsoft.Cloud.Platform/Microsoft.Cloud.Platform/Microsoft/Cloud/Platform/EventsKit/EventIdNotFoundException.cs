using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x0200037D RID: 893
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class EventIdNotFoundException : MonitoredException
	{
		// Token: 0x06001B26 RID: 6950 RVA: 0x00066028 File Offset: 0x00064228
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x00066030 File Offset: 0x00064230
		// (set) Token: 0x06001B28 RID: 6952 RVA: 0x00066038 File Offset: 0x00064238
		public long EventId
		{
			get
			{
				return this.m_eventId;
			}
			protected set
			{
				this.m_eventId = value;
			}
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x00066041 File Offset: 0x00064241
		public EventIdNotFoundException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidValueField<long>();
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x00066055 File Offset: 0x00064255
		public EventIdNotFoundException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x0006606C File Offset: 0x0006426C
		public EventIdNotFoundException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0006608C File Offset: 0x0006428C
		protected EventIdNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("EventIdNotFoundException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.EventId = (long)info.GetValue("EventIdNotFoundException_EventId", typeof(long));
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("EventIdNotFoundException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x00066148 File Offset: 0x00064348
		public EventIdNotFoundException(long eventId)
		{
			this.EventId = eventId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x0006615E File Offset: 0x0006435E
		public EventIdNotFoundException(long eventId, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.EventId = eventId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0006617C File Offset: 0x0006437C
		public EventIdNotFoundException(long eventId, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.EventId = eventId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x000661A0 File Offset: 0x000643A0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x000661D7 File Offset: 0x000643D7
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x000661E0 File Offset: 0x000643E0
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(EventIdNotFoundException))
			{
				TraceSourceBase<EventingTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<EventingTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<EventingTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x000662B0 File Offset: 0x000644B0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("EventIdNotFoundException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("EventIdNotFoundException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			info.AddValue("EventIdNotFoundException_EventId", this.EventId, typeof(long));
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0006632C File Offset: 0x0006452C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Metadata for event '{0}' was not found.", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? this.EventId.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.EventId.ToString(CultureInfo.InvariantCulture) : this.EventId.ToString(CultureInfo.InvariantCulture)) });
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x00066395 File Offset: 0x00064595
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

		// Token: 0x06001B36 RID: 6966 RVA: 0x000663B4 File Offset: 0x000645B4
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "EventId={0}", new object[] { this.EventId.ToString(CultureInfo.InvariantCulture) }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "EventId={0}", new object[] { this.EventId.ToString(CultureInfo.InvariantCulture) }) : string.Format(CultureInfo.CurrentCulture, "EventId={0}", new object[] { this.EventId.ToString(CultureInfo.InvariantCulture) })));
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x00066478 File Offset: 0x00064678
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x00066481 File Offset: 0x00064681
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x0006648A File Offset: 0x0006468A
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x00066478 File Offset: 0x00064678
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x00066494 File Offset: 0x00064694
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

		// Token: 0x04000939 RID: 2361
		private string creationMessage;

		// Token: 0x0400093A RID: 2362
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400093B RID: 2363
		private long m_eventId;
	}
}
