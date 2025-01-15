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
	// Token: 0x02000376 RID: 886
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class EtwProviderSynchronizerException : MonitoredException
	{
		// Token: 0x06001A7C RID: 6780 RVA: 0x00062614 File Offset: 0x00060814
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x0006261C File Offset: 0x0006081C
		// (set) Token: 0x06001A7E RID: 6782 RVA: 0x00062624 File Offset: 0x00060824
		public string OperationName
		{
			get
			{
				return this.m_operationName;
			}
			protected set
			{
				this.m_operationName = value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x0006262D File Offset: 0x0006082D
		// (set) Token: 0x06001A80 RID: 6784 RVA: 0x00062635 File Offset: 0x00060835
		public string EventSourceName
		{
			get
			{
				return this.m_eventSourceName;
			}
			protected set
			{
				this.m_eventSourceName = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x0006263E File Offset: 0x0006083E
		// (set) Token: 0x06001A82 RID: 6786 RVA: 0x00062646 File Offset: 0x00060846
		public Guid EventSourceGuid
		{
			get
			{
				return this.m_eventSourceGuid;
			}
			protected set
			{
				this.m_eventSourceGuid = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x0006264F File Offset: 0x0006084F
		// (set) Token: 0x06001A84 RID: 6788 RVA: 0x00062657 File Offset: 0x00060857
		public string SessionName
		{
			get
			{
				return this.m_sessionName;
			}
			protected set
			{
				this.m_sessionName = value;
			}
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x00062660 File Offset: 0x00060860
		public EtwProviderSynchronizerException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x0006267E File Offset: 0x0006087E
		public EtwProviderSynchronizerException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x00062695 File Offset: 0x00060895
		public EtwProviderSynchronizerException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x000626B4 File Offset: 0x000608B4
		protected EtwProviderSynchronizerException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("EtwProviderSynchronizerException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.OperationName = (string)info.GetValue("EtwProviderSynchronizerException_OperationName", typeof(string));
			}
			catch (SerializationException)
			{
				this.OperationName = null;
			}
			try
			{
				this.EventSourceName = (string)info.GetValue("EtwProviderSynchronizerException_EventSourceName", typeof(string));
			}
			catch (SerializationException)
			{
				this.EventSourceName = null;
			}
			this.EventSourceGuid = (Guid)info.GetValue("EtwProviderSynchronizerException_EventSourceGuid", typeof(Guid));
			try
			{
				this.SessionName = (string)info.GetValue("EtwProviderSynchronizerException_SessionName", typeof(string));
			}
			catch (SerializationException)
			{
				this.SessionName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("EtwProviderSynchronizerException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x0006281C File Offset: 0x00060A1C
		public EtwProviderSynchronizerException(string operationName, string eventSourceName, Guid eventSourceGuid, string sessionName)
		{
			this.OperationName = operationName;
			this.EventSourceName = eventSourceName;
			this.EventSourceGuid = eventSourceGuid;
			this.SessionName = sessionName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x00062848 File Offset: 0x00060A48
		public EtwProviderSynchronizerException(string operationName, string eventSourceName, Guid eventSourceGuid, string sessionName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.OperationName = operationName;
			this.EventSourceName = eventSourceName;
			this.EventSourceGuid = eventSourceGuid;
			this.SessionName = sessionName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x0006287E File Offset: 0x00060A7E
		public EtwProviderSynchronizerException(string operationName, string eventSourceName, Guid eventSourceGuid, string sessionName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.OperationName = operationName;
			this.EventSourceName = eventSourceName;
			this.EventSourceGuid = eventSourceGuid;
			this.SessionName = sessionName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x000628BC File Offset: 0x00060ABC
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x000628F3 File Offset: 0x00060AF3
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x000628FC File Offset: 0x00060AFC
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(EtwProviderSynchronizerException))
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

		// Token: 0x06001A8F RID: 6799 RVA: 0x000629CC File Offset: 0x00060BCC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("EtwProviderSynchronizerException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("EtwProviderSynchronizerException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.OperationName != null)
			{
				info.AddValue("EtwProviderSynchronizerException_OperationName", this.OperationName, typeof(string));
			}
			if (this.EventSourceName != null)
			{
				info.AddValue("EtwProviderSynchronizerException_EventSourceName", this.EventSourceName, typeof(string));
			}
			info.AddValue("EtwProviderSynchronizerException_EventSourceGuid", this.EventSourceGuid, typeof(Guid));
			if (this.SessionName != null)
			{
				info.AddValue("EtwProviderSynchronizerException_SessionName", this.SessionName, typeof(string));
			}
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x00062AB0 File Offset: 0x00060CB0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed operation '{0}' event source '{1}:{2}' on ETW session '{3}'", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.OperationName != null) ? this.OperationName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.OperationName != null) ? this.OperationName.MarkIfInternal() : string.Empty) : ((this.OperationName != null) ? this.OperationName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.EventSourceName != null) ? this.EventSourceName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.EventSourceName != null) ? this.EventSourceName.MarkIfInternal() : string.Empty) : ((this.EventSourceName != null) ? this.EventSourceName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? this.EventSourceGuid.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.EventSourceGuid.ToString() : this.EventSourceGuid.ToString()),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.SessionName != null) ? this.SessionName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.SessionName != null) ? this.SessionName.MarkIfInternal() : string.Empty) : ((this.SessionName != null) ? this.SessionName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001A91 RID: 6801 RVA: 0x00062C42 File Offset: 0x00060E42
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

		// Token: 0x06001A92 RID: 6802 RVA: 0x00062C60 File Offset: 0x00060E60
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "OperationName={0}", new object[] { (this.OperationName != null) ? this.OperationName.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "OperationName={0}", new object[] { (this.OperationName != null) ? this.OperationName.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "OperationName={0}", new object[] { (this.OperationName != null) ? this.OperationName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "EventSourceName={0}", new object[] { (this.EventSourceName != null) ? this.EventSourceName.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "EventSourceName={0}", new object[] { (this.EventSourceName != null) ? this.EventSourceName.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "EventSourceName={0}", new object[] { (this.EventSourceName != null) ? this.EventSourceName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "EventSourceGuid={0}", new object[] { this.EventSourceGuid.ToString() }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "EventSourceGuid={0}", new object[] { this.EventSourceGuid.ToString() }) : string.Format(CultureInfo.CurrentCulture, "EventSourceGuid={0}", new object[] { this.EventSourceGuid.ToString() })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "SessionName={0}", new object[] { (this.SessionName != null) ? this.SessionName.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "SessionName={0}", new object[] { (this.SessionName != null) ? this.SessionName.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "SessionName={0}", new object[] { (this.SessionName != null) ? this.SessionName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x00062F70 File Offset: 0x00061170
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x00062F79 File Offset: 0x00061179
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x00062F82 File Offset: 0x00061182
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x00062F70 File Offset: 0x00061170
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00062F8C File Offset: 0x0006118C
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

		// Token: 0x0400091B RID: 2331
		private string creationMessage;

		// Token: 0x0400091C RID: 2332
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400091D RID: 2333
		private string m_operationName;

		// Token: 0x0400091E RID: 2334
		private string m_eventSourceName;

		// Token: 0x0400091F RID: 2335
		private Guid m_eventSourceGuid;

		// Token: 0x04000920 RID: 2336
		private string m_sessionName;
	}
}
