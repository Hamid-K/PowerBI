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
	// Token: 0x02000379 RID: 889
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class EventsAlreadyExistsException : MonitoredException
	{
		// Token: 0x06001AC5 RID: 6853 RVA: 0x00064010 File Offset: 0x00062210
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x00064018 File Offset: 0x00062218
		// (set) Token: 0x06001AC7 RID: 6855 RVA: 0x00064020 File Offset: 0x00062220
		public string EventsKitType
		{
			get
			{
				return this.m_eventsKitType;
			}
			protected set
			{
				this.m_eventsKitType = value;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001AC8 RID: 6856 RVA: 0x00064029 File Offset: 0x00062229
		// (set) Token: 0x06001AC9 RID: 6857 RVA: 0x00064031 File Offset: 0x00062231
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

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x0006403A File Offset: 0x0006223A
		// (set) Token: 0x06001ACB RID: 6859 RVA: 0x00064042 File Offset: 0x00062242
		public string AttemptedEventsKitCodeBase
		{
			get
			{
				return this.m_attemptedEventsKitCodeBase;
			}
			protected set
			{
				this.m_attemptedEventsKitCodeBase = value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001ACC RID: 6860 RVA: 0x0006404B File Offset: 0x0006224B
		// (set) Token: 0x06001ACD RID: 6861 RVA: 0x00064053 File Offset: 0x00062253
		public string ExistingEventsKitCodeBase
		{
			get
			{
				return this.m_existingEventsKitCodeBase;
			}
			protected set
			{
				this.m_existingEventsKitCodeBase = value;
			}
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x0006405C File Offset: 0x0006225C
		public EventsAlreadyExistsException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidValueField<long>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x0006407F File Offset: 0x0006227F
		public EventsAlreadyExistsException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x00064096 File Offset: 0x00062296
		public EventsAlreadyExistsException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x000640B4 File Offset: 0x000622B4
		protected EventsAlreadyExistsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("EventsAlreadyExistsException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.EventsKitType = (string)info.GetValue("EventsAlreadyExistsException_EventsKitType", typeof(string));
			}
			catch (SerializationException)
			{
				this.EventsKitType = null;
			}
			this.EventsKitId = (long)info.GetValue("EventsAlreadyExistsException_EventsKitId", typeof(long));
			try
			{
				this.AttemptedEventsKitCodeBase = (string)info.GetValue("EventsAlreadyExistsException_AttemptedEventsKitCodeBase", typeof(string));
			}
			catch (SerializationException)
			{
				this.AttemptedEventsKitCodeBase = null;
			}
			try
			{
				this.ExistingEventsKitCodeBase = (string)info.GetValue("EventsAlreadyExistsException_ExistingEventsKitCodeBase", typeof(string));
			}
			catch (SerializationException)
			{
				this.ExistingEventsKitCodeBase = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("EventsAlreadyExistsException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x0006421C File Offset: 0x0006241C
		public EventsAlreadyExistsException(string eventsKitType, long eventsKitId, string attemptedEventsKitCodeBase, string existingEventsKitCodeBase)
		{
			this.EventsKitType = eventsKitType;
			this.EventsKitId = eventsKitId;
			this.AttemptedEventsKitCodeBase = attemptedEventsKitCodeBase;
			this.ExistingEventsKitCodeBase = existingEventsKitCodeBase;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00064248 File Offset: 0x00062448
		public EventsAlreadyExistsException(string eventsKitType, long eventsKitId, string attemptedEventsKitCodeBase, string existingEventsKitCodeBase, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.EventsKitType = eventsKitType;
			this.EventsKitId = eventsKitId;
			this.AttemptedEventsKitCodeBase = attemptedEventsKitCodeBase;
			this.ExistingEventsKitCodeBase = existingEventsKitCodeBase;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0006427E File Offset: 0x0006247E
		public EventsAlreadyExistsException(string eventsKitType, long eventsKitId, string attemptedEventsKitCodeBase, string existingEventsKitCodeBase, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.EventsKitType = eventsKitType;
			this.EventsKitId = eventsKitId;
			this.AttemptedEventsKitCodeBase = attemptedEventsKitCodeBase;
			this.ExistingEventsKitCodeBase = existingEventsKitCodeBase;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x000642BC File Offset: 0x000624BC
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x000642F3 File Offset: 0x000624F3
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x000642FC File Offset: 0x000624FC
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(EventsAlreadyExistsException))
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

		// Token: 0x06001AD8 RID: 6872 RVA: 0x000643CC File Offset: 0x000625CC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("EventsAlreadyExistsException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("EventsAlreadyExistsException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.EventsKitType != null)
			{
				info.AddValue("EventsAlreadyExistsException_EventsKitType", this.EventsKitType, typeof(string));
			}
			info.AddValue("EventsAlreadyExistsException_EventsKitId", this.EventsKitId, typeof(long));
			if (this.AttemptedEventsKitCodeBase != null)
			{
				info.AddValue("EventsAlreadyExistsException_AttemptedEventsKitCodeBase", this.AttemptedEventsKitCodeBase, typeof(string));
			}
			if (this.ExistingEventsKitCodeBase != null)
			{
				info.AddValue("EventsAlreadyExistsException_ExistingEventsKitCodeBase", this.ExistingEventsKitCodeBase, typeof(string));
			}
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x000644B0 File Offset: 0x000626B0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Event id {0}:{1} already exists.", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.EventsKitType != null) ? this.EventsKitType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.EventsKitType != null) ? this.EventsKitType.MarkIfInternal() : string.Empty) : ((this.EventsKitType != null) ? this.EventsKitType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? this.EventsKitId.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.EventsKitId.ToString(CultureInfo.InvariantCulture) : this.EventsKitId.ToString(CultureInfo.InvariantCulture))
			});
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x0006457B File Offset: 0x0006277B
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

		// Token: 0x06001ADB RID: 6875 RVA: 0x00064598 File Offset: 0x00062798
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "EventsKitType={0}", new object[] { (this.EventsKitType != null) ? this.EventsKitType.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "EventsKitType={0}", new object[] { (this.EventsKitType != null) ? this.EventsKitType.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "EventsKitType={0}", new object[] { (this.EventsKitType != null) ? this.EventsKitType.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "EventsKitId={0}", new object[] { this.EventsKitId.ToString(CultureInfo.InvariantCulture) }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "EventsKitId={0}", new object[] { this.EventsKitId.ToString(CultureInfo.InvariantCulture) }) : string.Format(CultureInfo.CurrentCulture, "EventsKitId={0}", new object[] { this.EventsKitId.ToString(CultureInfo.InvariantCulture) })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "AttemptedEventsKitCodeBase={0}", new object[] { (this.AttemptedEventsKitCodeBase != null) ? this.AttemptedEventsKitCodeBase.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "AttemptedEventsKitCodeBase={0}", new object[] { (this.AttemptedEventsKitCodeBase != null) ? this.AttemptedEventsKitCodeBase.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "AttemptedEventsKitCodeBase={0}", new object[] { (this.AttemptedEventsKitCodeBase != null) ? this.AttemptedEventsKitCodeBase.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ExistingEventsKitCodeBase={0}", new object[] { (this.ExistingEventsKitCodeBase != null) ? this.ExistingEventsKitCodeBase.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ExistingEventsKitCodeBase={0}", new object[] { (this.ExistingEventsKitCodeBase != null) ? this.ExistingEventsKitCodeBase.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "ExistingEventsKitCodeBase={0}", new object[] { (this.ExistingEventsKitCodeBase != null) ? this.ExistingEventsKitCodeBase.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x000648A5 File Offset: 0x00062AA5
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x000648AE File Offset: 0x00062AAE
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x000648B7 File Offset: 0x00062AB7
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x000648A5 File Offset: 0x00062AA5
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x000648C0 File Offset: 0x00062AC0
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

		// Token: 0x04000928 RID: 2344
		private string creationMessage;

		// Token: 0x04000929 RID: 2345
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400092A RID: 2346
		private string m_eventsKitType;

		// Token: 0x0400092B RID: 2347
		private long m_eventsKitId;

		// Token: 0x0400092C RID: 2348
		private string m_attemptedEventsKitCodeBase;

		// Token: 0x0400092D RID: 2349
		private string m_existingEventsKitCodeBase;
	}
}
