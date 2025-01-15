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
	// Token: 0x02000373 RID: 883
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class EventsKitCreationFailedException : MonitoredException
	{
		// Token: 0x06001A30 RID: 6704 RVA: 0x00060BC0 File Offset: 0x0005EDC0
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001A31 RID: 6705 RVA: 0x00060BC8 File Offset: 0x0005EDC8
		// (set) Token: 0x06001A32 RID: 6706 RVA: 0x00060BD0 File Offset: 0x0005EDD0
		public string EventsKitName
		{
			get
			{
				return this.m_eventsKitName;
			}
			protected set
			{
				this.m_eventsKitName = value;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x00060BD9 File Offset: 0x0005EDD9
		// (set) Token: 0x06001A34 RID: 6708 RVA: 0x00060BE1 File Offset: 0x0005EDE1
		public string AssemblyName
		{
			get
			{
				return this.m_assemblyName;
			}
			protected set
			{
				this.m_assemblyName = value;
			}
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x00060BEA File Offset: 0x0005EDEA
		public EventsKitCreationFailedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x00060C03 File Offset: 0x0005EE03
		public EventsKitCreationFailedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x00060C1A File Offset: 0x0005EE1A
		public EventsKitCreationFailedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x00060C38 File Offset: 0x0005EE38
		protected EventsKitCreationFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("EventsKitCreationFailedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.EventsKitName = (string)info.GetValue("EventsKitCreationFailedException_EventsKitName", typeof(string));
			}
			catch (SerializationException)
			{
				this.EventsKitName = null;
			}
			try
			{
				this.AssemblyName = (string)info.GetValue("EventsKitCreationFailedException_AssemblyName", typeof(string));
			}
			catch (SerializationException)
			{
				this.AssemblyName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("EventsKitCreationFailedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x00060D48 File Offset: 0x0005EF48
		public EventsKitCreationFailedException(string eventsKitName, string assemblyName)
		{
			this.EventsKitName = eventsKitName;
			this.AssemblyName = assemblyName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x00060D65 File Offset: 0x0005EF65
		public EventsKitCreationFailedException(string eventsKitName, string assemblyName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.EventsKitName = eventsKitName;
			this.AssemblyName = assemblyName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x00060D8A File Offset: 0x0005EF8A
		public EventsKitCreationFailedException(string eventsKitName, string assemblyName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.EventsKitName = eventsKitName;
			this.AssemblyName = assemblyName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x00060DB8 File Offset: 0x0005EFB8
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x00060DEF File Offset: 0x0005EFEF
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x00060DF8 File Offset: 0x0005EFF8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(EventsKitCreationFailedException))
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

		// Token: 0x06001A3F RID: 6719 RVA: 0x00060EC8 File Offset: 0x0005F0C8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("EventsKitCreationFailedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("EventsKitCreationFailedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.EventsKitName != null)
			{
				info.AddValue("EventsKitCreationFailedException_EventsKitName", this.EventsKitName, typeof(string));
			}
			if (this.AssemblyName != null)
			{
				info.AddValue("EventsKitCreationFailedException_AssemblyName", this.AssemblyName, typeof(string));
			}
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x00060F6C File Offset: 0x0005F16C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed creating or locating events kit '{0}' in assembly '{1}'", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.EventsKitName != null) ? this.EventsKitName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.EventsKitName != null) ? this.EventsKitName.MarkIfInternal() : string.Empty) : ((this.EventsKitName != null) ? this.EventsKitName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.AssemblyName != null) ? this.AssemblyName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.AssemblyName != null) ? this.AssemblyName.MarkIfInternal() : string.Empty) : ((this.AssemblyName != null) ? this.AssemblyName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x00061052 File Offset: 0x0005F252
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

		// Token: 0x06001A42 RID: 6722 RVA: 0x00061070 File Offset: 0x0005F270
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "EventsKitName={0}", new object[] { (this.EventsKitName != null) ? this.EventsKitName.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "EventsKitName={0}", new object[] { (this.EventsKitName != null) ? this.EventsKitName.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "EventsKitName={0}", new object[] { (this.EventsKitName != null) ? this.EventsKitName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "AssemblyName={0}", new object[] { (this.AssemblyName != null) ? this.AssemblyName.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "AssemblyName={0}", new object[] { (this.AssemblyName != null) ? this.AssemblyName.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "AssemblyName={0}", new object[] { (this.AssemblyName != null) ? this.AssemblyName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x00061212 File Offset: 0x0005F412
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x0006121B File Offset: 0x0005F41B
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00061224 File Offset: 0x0005F424
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x00061212 File Offset: 0x0005F412
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x00061230 File Offset: 0x0005F430
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

		// Token: 0x0400090D RID: 2317
		private string creationMessage;

		// Token: 0x0400090E RID: 2318
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400090F RID: 2319
		private string m_eventsKitName;

		// Token: 0x04000910 RID: 2320
		private string m_assemblyName;
	}
}
