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
	// Token: 0x0200037B RID: 891
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class EventsKitNotFoundException : MonitoredException
	{
		// Token: 0x06001AF6 RID: 6902 RVA: 0x00065144 File Offset: 0x00063344
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x0006514C File Offset: 0x0006334C
		// (set) Token: 0x06001AF8 RID: 6904 RVA: 0x00065154 File Offset: 0x00063354
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

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0006515D File Offset: 0x0006335D
		public EventsKitNotFoundException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidValueField<long>();
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x00065171 File Offset: 0x00063371
		public EventsKitNotFoundException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x00065188 File Offset: 0x00063388
		public EventsKitNotFoundException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x000651A8 File Offset: 0x000633A8
		protected EventsKitNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("EventsKitNotFoundException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.EventsKitId = (long)info.GetValue("EventsKitNotFoundException_EventsKitId", typeof(long));
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("EventsKitNotFoundException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x00065264 File Offset: 0x00063464
		public EventsKitNotFoundException(long eventsKitId)
		{
			this.EventsKitId = eventsKitId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x0006527A File Offset: 0x0006347A
		public EventsKitNotFoundException(long eventsKitId, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.EventsKitId = eventsKitId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x00065298 File Offset: 0x00063498
		public EventsKitNotFoundException(long eventsKitId, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.EventsKitId = eventsKitId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x000652BC File Offset: 0x000634BC
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x000652F3 File Offset: 0x000634F3
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x000652FC File Offset: 0x000634FC
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(EventsKitNotFoundException))
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

		// Token: 0x06001B03 RID: 6915 RVA: 0x000653CC File Offset: 0x000635CC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("EventsKitNotFoundException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("EventsKitNotFoundException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			info.AddValue("EventsKitNotFoundException_EventsKitId", this.EventsKitId, typeof(long));
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x00065448 File Offset: 0x00063648
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Events kit {0} was not found.", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? this.EventsKitId.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.EventsKitId.ToString(CultureInfo.InvariantCulture) : this.EventsKitId.ToString(CultureInfo.InvariantCulture)) });
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001B05 RID: 6917 RVA: 0x000654B1 File Offset: 0x000636B1
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

		// Token: 0x06001B06 RID: 6918 RVA: 0x000654D0 File Offset: 0x000636D0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "EventsKitId={0}", new object[] { this.EventsKitId.ToString(CultureInfo.InvariantCulture) }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "EventsKitId={0}", new object[] { this.EventsKitId.ToString(CultureInfo.InvariantCulture) }) : string.Format(CultureInfo.CurrentCulture, "EventsKitId={0}", new object[] { this.EventsKitId.ToString(CultureInfo.InvariantCulture) })));
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x00065594 File Offset: 0x00063794
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x0006559D File Offset: 0x0006379D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x000655A6 File Offset: 0x000637A6
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x00065594 File Offset: 0x00063794
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x000655B0 File Offset: 0x000637B0
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

		// Token: 0x04000931 RID: 2353
		private string creationMessage;

		// Token: 0x04000932 RID: 2354
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000933 RID: 2355
		private long m_eventsKitId;
	}
}
