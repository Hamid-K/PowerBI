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
	// Token: 0x0200037C RID: 892
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class EventMetadataNotFoundException : MonitoredException
	{
		// Token: 0x06001B0C RID: 6924 RVA: 0x0006579C File Offset: 0x0006399C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x000657A4 File Offset: 0x000639A4
		// (set) Token: 0x06001B0E RID: 6926 RVA: 0x000657AC File Offset: 0x000639AC
		public Guid EventIdentifier
		{
			get
			{
				return this.m_eventIdentifier;
			}
			protected set
			{
				this.m_eventIdentifier = value;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x000657B5 File Offset: 0x000639B5
		// (set) Token: 0x06001B10 RID: 6928 RVA: 0x000657BD File Offset: 0x000639BD
		public long EventsKitId
		{
			get
			{
				return this.m_eventsKitId;
			}
			protected set
			{
				this.m_eventsKitId = value;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x000657C6 File Offset: 0x000639C6
		// (set) Token: 0x06001B12 RID: 6930 RVA: 0x000657CE File Offset: 0x000639CE
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

		// Token: 0x06001B13 RID: 6931 RVA: 0x000657D7 File Offset: 0x000639D7
		public EventMetadataNotFoundException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidValueField<Guid>();
			CompileCheck.IsValidValueField<long>();
			CompileCheck.IsValidValueField<long>();
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x000657F5 File Offset: 0x000639F5
		public EventMetadataNotFoundException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x0006580C File Offset: 0x00063A0C
		public EventMetadataNotFoundException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x0006582C File Offset: 0x00063A2C
		protected EventMetadataNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("EventMetadataNotFoundException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.EventIdentifier = (Guid)info.GetValue("EventMetadataNotFoundException_EventIdentifier", typeof(Guid));
			this.EventsKitId = (long)info.GetValue("EventMetadataNotFoundException_EventsKitId", typeof(long));
			this.EventId = (long)info.GetValue("EventMetadataNotFoundException_EventId", typeof(long));
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("EventMetadataNotFoundException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00065928 File Offset: 0x00063B28
		public EventMetadataNotFoundException(Guid eventIdentifier, long eventsKitId, long eventId)
		{
			this.EventIdentifier = eventIdentifier;
			this.EventsKitId = eventsKitId;
			this.EventId = eventId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x0006594C File Offset: 0x00063B4C
		public EventMetadataNotFoundException(Guid eventIdentifier, long eventsKitId, long eventId, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.EventIdentifier = eventIdentifier;
			this.EventsKitId = eventsKitId;
			this.EventId = eventId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x0006597A File Offset: 0x00063B7A
		public EventMetadataNotFoundException(Guid eventIdentifier, long eventsKitId, long eventId, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.EventIdentifier = eventIdentifier;
			this.EventsKitId = eventsKitId;
			this.EventId = eventId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x000659B0 File Offset: 0x00063BB0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x000659E7 File Offset: 0x00063BE7
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x000659F0 File Offset: 0x00063BF0
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(EventMetadataNotFoundException))
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

		// Token: 0x06001B1D RID: 6941 RVA: 0x00065AC0 File Offset: 0x00063CC0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("EventMetadataNotFoundException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("EventMetadataNotFoundException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			info.AddValue("EventMetadataNotFoundException_EventIdentifier", this.EventIdentifier, typeof(Guid));
			info.AddValue("EventMetadataNotFoundException_EventsKitId", this.EventsKitId, typeof(long));
			info.AddValue("EventMetadataNotFoundException_EventId", this.EventId, typeof(long));
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x00065B7C File Offset: 0x00063D7C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Metadata for event '{0}' was not found.", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? this.EventIdentifier.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.EventIdentifier.ToString() : this.EventIdentifier.ToString()) });
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x00065BE8 File Offset: 0x00063DE8
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

		// Token: 0x06001B20 RID: 6944 RVA: 0x00065C08 File Offset: 0x00063E08
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "EventIdentifier={0}", new object[] { this.EventIdentifier.ToString() }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "EventIdentifier={0}", new object[] { this.EventIdentifier.ToString() }) : string.Format(CultureInfo.CurrentCulture, "EventIdentifier={0}", new object[] { this.EventIdentifier.ToString() })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "EventsKitId={0}", new object[] { this.EventsKitId.ToString(CultureInfo.InvariantCulture) }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "EventsKitId={0}", new object[] { this.EventsKitId.ToString(CultureInfo.InvariantCulture) }) : string.Format(CultureInfo.CurrentCulture, "EventsKitId={0}", new object[] { this.EventsKitId.ToString(CultureInfo.InvariantCulture) })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "EventId={0}", new object[] { this.EventId.ToString(CultureInfo.InvariantCulture) }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "EventId={0}", new object[] { this.EventId.ToString(CultureInfo.InvariantCulture) }) : string.Format(CultureInfo.CurrentCulture, "EventId={0}", new object[] { this.EventId.ToString(CultureInfo.InvariantCulture) })));
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x00065E1F File Offset: 0x0006401F
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x00065E28 File Offset: 0x00064028
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x00065E31 File Offset: 0x00064031
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x00065E1F File Offset: 0x0006401F
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x00065E3C File Offset: 0x0006403C
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

		// Token: 0x04000934 RID: 2356
		private string creationMessage;

		// Token: 0x04000935 RID: 2357
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000936 RID: 2358
		private Guid m_eventIdentifier;

		// Token: 0x04000937 RID: 2359
		private long m_eventsKitId;

		// Token: 0x04000938 RID: 2360
		private long m_eventId;
	}
}
