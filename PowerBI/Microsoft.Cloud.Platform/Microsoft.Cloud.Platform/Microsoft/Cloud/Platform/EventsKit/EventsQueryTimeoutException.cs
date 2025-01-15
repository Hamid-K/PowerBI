using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000375 RID: 885
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class EventsQueryTimeoutException : TimeoutException
	{
		// Token: 0x06001A62 RID: 6754 RVA: 0x00061D1C File Offset: 0x0005FF1C
		public virtual ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001A63 RID: 6755 RVA: 0x00061D24 File Offset: 0x0005FF24
		// (set) Token: 0x06001A64 RID: 6756 RVA: 0x00061D2C File Offset: 0x0005FF2C
		public IEnumerable<WireEventBase> Events
		{
			get
			{
				return this.m_events;
			}
			protected set
			{
				this.m_events = value;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x00061D35 File Offset: 0x0005FF35
		// (set) Token: 0x06001A66 RID: 6758 RVA: 0x00061D3D File Offset: 0x0005FF3D
		public int? NumOfCollectedEvents
		{
			get
			{
				return this.m_numOfCollectedEvents;
			}
			protected set
			{
				this.m_numOfCollectedEvents = value;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001A67 RID: 6759 RVA: 0x00061D46 File Offset: 0x0005FF46
		// (set) Token: 0x06001A68 RID: 6760 RVA: 0x00061D4E File Offset: 0x0005FF4E
		public int? ExpectedOccurrences
		{
			get
			{
				return this.m_expectedOccurrences;
			}
			protected set
			{
				this.m_expectedOccurrences = value;
			}
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00061D57 File Offset: 0x0005FF57
		public EventsQueryTimeoutException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<IEnumerable<WireEventBase>>();
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x00061D6B File Offset: 0x0005FF6B
		public EventsQueryTimeoutException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x00061D82 File Offset: 0x0005FF82
		public EventsQueryTimeoutException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00061DA0 File Offset: 0x0005FFA0
		protected EventsQueryTimeoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("EventsQueryTimeoutException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Events = (IEnumerable<WireEventBase>)info.GetValue("EventsQueryTimeoutException_Events", typeof(IEnumerable<WireEventBase>));
			}
			catch (SerializationException)
			{
				this.Events = null;
			}
			this.NumOfCollectedEvents = (int?)info.GetValue("EventsQueryTimeoutException_NumOfCollectedEvents", typeof(int?));
			this.ExpectedOccurrences = (int?)info.GetValue("EventsQueryTimeoutException_ExpectedOccurrences", typeof(int?));
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("EventsQueryTimeoutException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x00061EB4 File Offset: 0x000600B4
		public EventsQueryTimeoutException(IEnumerable<WireEventBase> events, int? numOfCollectedEvents, int? expectedOccurrences)
		{
			this.Events = events;
			this.NumOfCollectedEvents = numOfCollectedEvents;
			this.ExpectedOccurrences = expectedOccurrences;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x00061ED8 File Offset: 0x000600D8
		public EventsQueryTimeoutException(IEnumerable<WireEventBase> events, int? numOfCollectedEvents, int? expectedOccurrences, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Events = events;
			this.NumOfCollectedEvents = numOfCollectedEvents;
			this.ExpectedOccurrences = expectedOccurrences;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x00061F06 File Offset: 0x00060106
		public EventsQueryTimeoutException(IEnumerable<WireEventBase> events, int? numOfCollectedEvents, int? expectedOccurrences, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Events = events;
			this.NumOfCollectedEvents = numOfCollectedEvents;
			this.ExpectedOccurrences = expectedOccurrences;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x00061F3C File Offset: 0x0006013C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x00061F73 File Offset: 0x00060173
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x00061F7C File Offset: 0x0006017C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(EventsQueryTimeoutException))
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

		// Token: 0x06001A73 RID: 6771 RVA: 0x0006204C File Offset: 0x0006024C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("EventsQueryTimeoutException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("EventsQueryTimeoutException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Events != null)
			{
				info.AddValue("EventsQueryTimeoutException_Events", this.Events, typeof(IEnumerable<WireEventBase>));
			}
			info.AddValue("EventsQueryTimeoutException_NumOfCollectedEvents", this.NumOfCollectedEvents, typeof(int?));
			info.AddValue("EventsQueryTimeoutException_ExpectedOccurrences", this.ExpectedOccurrences, typeof(int?));
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x0006210C File Offset: 0x0006030C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Timeout reached after collecting {0} out of {1} events.", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? this.NumOfCollectedEvents.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.NumOfCollectedEvents.ToString() : this.NumOfCollectedEvents.ToString()),
				(markupKind == PrivateInformationMarkupKind.None) ? this.ExpectedOccurrences.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.ExpectedOccurrences.ToString() : this.ExpectedOccurrences.ToString())
			});
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x000621C2 File Offset: 0x000603C2
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

		// Token: 0x06001A76 RID: 6774 RVA: 0x000621E0 File Offset: 0x000603E0
		protected virtual string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Events={0}", new object[] { (this.Events != null) ? this.Events.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Events={0}", new object[] { (this.Events != null) ? this.Events.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Events={0}", new object[] { (this.Events != null) ? this.Events.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "NumOfCollectedEvents={0}", new object[] { this.NumOfCollectedEvents.ToString() }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "NumOfCollectedEvents={0}", new object[] { this.NumOfCollectedEvents.ToString() }) : string.Format(CultureInfo.CurrentCulture, "NumOfCollectedEvents={0}", new object[] { this.NumOfCollectedEvents.ToString() })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ExpectedOccurrences={0}", new object[] { this.ExpectedOccurrences.ToString() }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ExpectedOccurrences={0}", new object[] { this.ExpectedOccurrences.ToString() }) : string.Format(CultureInfo.CurrentCulture, "ExpectedOccurrences={0}", new object[] { this.ExpectedOccurrences.ToString() })));
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x0006240D File Offset: 0x0006060D
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x00062416 File Offset: 0x00060616
		public virtual string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x0006241F File Offset: 0x0006061F
		public virtual string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x0006240D File Offset: 0x0006060D
		public virtual string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x00062428 File Offset: 0x00060628
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

		// Token: 0x04000916 RID: 2326
		private string creationMessage;

		// Token: 0x04000917 RID: 2327
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000918 RID: 2328
		private IEnumerable<WireEventBase> m_events;

		// Token: 0x04000919 RID: 2329
		private int? m_numOfCollectedEvents;

		// Token: 0x0400091A RID: 2330
		private int? m_expectedOccurrences;
	}
}
