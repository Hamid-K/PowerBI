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
	// Token: 0x02000374 RID: 884
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class EventsQueryStoppedException : MonitoredException
	{
		// Token: 0x06001A48 RID: 6728 RVA: 0x0006141C File Offset: 0x0005F61C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001A49 RID: 6729 RVA: 0x00061424 File Offset: 0x0005F624
		// (set) Token: 0x06001A4A RID: 6730 RVA: 0x0006142C File Offset: 0x0005F62C
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

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001A4B RID: 6731 RVA: 0x00061435 File Offset: 0x0005F635
		// (set) Token: 0x06001A4C RID: 6732 RVA: 0x0006143D File Offset: 0x0005F63D
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

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001A4D RID: 6733 RVA: 0x00061446 File Offset: 0x0005F646
		// (set) Token: 0x06001A4E RID: 6734 RVA: 0x0006144E File Offset: 0x0005F64E
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

		// Token: 0x06001A4F RID: 6735 RVA: 0x00061457 File Offset: 0x0005F657
		public EventsQueryStoppedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<IEnumerable<WireEventBase>>();
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x0006146B File Offset: 0x0005F66B
		public EventsQueryStoppedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00061482 File Offset: 0x0005F682
		public EventsQueryStoppedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x000614A0 File Offset: 0x0005F6A0
		protected EventsQueryStoppedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("EventsQueryStoppedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Events = (IEnumerable<WireEventBase>)info.GetValue("EventsQueryStoppedException_Events", typeof(IEnumerable<WireEventBase>));
			}
			catch (SerializationException)
			{
				this.Events = null;
			}
			this.NumOfCollectedEvents = (int?)info.GetValue("EventsQueryStoppedException_NumOfCollectedEvents", typeof(int?));
			this.ExpectedOccurrences = (int?)info.GetValue("EventsQueryStoppedException_ExpectedOccurrences", typeof(int?));
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("EventsQueryStoppedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x000615B4 File Offset: 0x0005F7B4
		public EventsQueryStoppedException(IEnumerable<WireEventBase> events, int? numOfCollectedEvents, int? expectedOccurrences)
		{
			this.Events = events;
			this.NumOfCollectedEvents = numOfCollectedEvents;
			this.ExpectedOccurrences = expectedOccurrences;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x000615D8 File Offset: 0x0005F7D8
		public EventsQueryStoppedException(IEnumerable<WireEventBase> events, int? numOfCollectedEvents, int? expectedOccurrences, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Events = events;
			this.NumOfCollectedEvents = numOfCollectedEvents;
			this.ExpectedOccurrences = expectedOccurrences;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x00061606 File Offset: 0x0005F806
		public EventsQueryStoppedException(IEnumerable<WireEventBase> events, int? numOfCollectedEvents, int? expectedOccurrences, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Events = events;
			this.NumOfCollectedEvents = numOfCollectedEvents;
			this.ExpectedOccurrences = expectedOccurrences;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x0006163C File Offset: 0x0005F83C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x00061673 File Offset: 0x0005F873
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x0006167C File Offset: 0x0005F87C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(EventsQueryStoppedException))
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

		// Token: 0x06001A59 RID: 6745 RVA: 0x0006174C File Offset: 0x0005F94C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("EventsQueryStoppedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("EventsQueryStoppedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Events != null)
			{
				info.AddValue("EventsQueryStoppedException_Events", this.Events, typeof(IEnumerable<WireEventBase>));
			}
			info.AddValue("EventsQueryStoppedException_NumOfCollectedEvents", this.NumOfCollectedEvents, typeof(int?));
			info.AddValue("EventsQueryStoppedException_ExpectedOccurrences", this.ExpectedOccurrences, typeof(int?));
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x0006180C File Offset: 0x0005FA0C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Stop was called after collecting {0} out of {1} events.", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? this.NumOfCollectedEvents.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.NumOfCollectedEvents.ToString() : this.NumOfCollectedEvents.ToString()),
				(markupKind == PrivateInformationMarkupKind.None) ? this.ExpectedOccurrences.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.ExpectedOccurrences.ToString() : this.ExpectedOccurrences.ToString())
			});
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001A5B RID: 6747 RVA: 0x000618C2 File Offset: 0x0005FAC2
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

		// Token: 0x06001A5C RID: 6748 RVA: 0x000618E0 File Offset: 0x0005FAE0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Events={0}", new object[] { (this.Events != null) ? this.Events.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Events={0}", new object[] { (this.Events != null) ? this.Events.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Events={0}", new object[] { (this.Events != null) ? this.Events.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "NumOfCollectedEvents={0}", new object[] { this.NumOfCollectedEvents.ToString() }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "NumOfCollectedEvents={0}", new object[] { this.NumOfCollectedEvents.ToString() }) : string.Format(CultureInfo.CurrentCulture, "NumOfCollectedEvents={0}", new object[] { this.NumOfCollectedEvents.ToString() })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ExpectedOccurrences={0}", new object[] { this.ExpectedOccurrences.ToString() }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ExpectedOccurrences={0}", new object[] { this.ExpectedOccurrences.ToString() }) : string.Format(CultureInfo.CurrentCulture, "ExpectedOccurrences={0}", new object[] { this.ExpectedOccurrences.ToString() })));
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00061B15 File Offset: 0x0005FD15
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00061B1E File Offset: 0x0005FD1E
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00061B27 File Offset: 0x0005FD27
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00061B15 File Offset: 0x0005FD15
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00061B30 File Offset: 0x0005FD30
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

		// Token: 0x04000911 RID: 2321
		private string creationMessage;

		// Token: 0x04000912 RID: 2322
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000913 RID: 2323
		private IEnumerable<WireEventBase> m_events;

		// Token: 0x04000914 RID: 2324
		private int? m_numOfCollectedEvents;

		// Token: 0x04000915 RID: 2325
		private int? m_expectedOccurrences;
	}
}
