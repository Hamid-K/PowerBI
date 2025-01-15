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
	// Token: 0x02000377 RID: 887
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class EventFilesCollectionException : MonitoredException
	{
		// Token: 0x06001A98 RID: 6808 RVA: 0x00063178 File Offset: 0x00061378
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x00063180 File Offset: 0x00061380
		// (set) Token: 0x06001A9A RID: 6810 RVA: 0x00063188 File Offset: 0x00061388
		public string FilesToMove
		{
			get
			{
				return this.m_filesToMove;
			}
			protected set
			{
				this.m_filesToMove = value;
			}
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x00063191 File Offset: 0x00061391
		public EventFilesCollectionException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x000631A5 File Offset: 0x000613A5
		public EventFilesCollectionException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x000631BC File Offset: 0x000613BC
		public EventFilesCollectionException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x000631DC File Offset: 0x000613DC
		protected EventFilesCollectionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("EventFilesCollectionException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.FilesToMove = (string)info.GetValue("EventFilesCollectionException_FilesToMove", typeof(string));
			}
			catch (SerializationException)
			{
				this.FilesToMove = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("EventFilesCollectionException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x000632B0 File Offset: 0x000614B0
		public EventFilesCollectionException(string filesToMove, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.FilesToMove = filesToMove;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x000632CE File Offset: 0x000614CE
		public EventFilesCollectionException(string filesToMove, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.FilesToMove = filesToMove;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x000632F4 File Offset: 0x000614F4
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x0006332B File Offset: 0x0006152B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x00063334 File Offset: 0x00061534
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(EventFilesCollectionException))
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

		// Token: 0x06001AA4 RID: 6820 RVA: 0x00063404 File Offset: 0x00061604
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("EventFilesCollectionException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("EventFilesCollectionException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.FilesToMove != null)
			{
				info.AddValue("EventFilesCollectionException_FilesToMove", this.FilesToMove, typeof(string));
			}
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x00063484 File Offset: 0x00061684
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed collecting event files: '{0}' - see inner exception for details", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.FilesToMove != null) ? this.FilesToMove.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.FilesToMove != null) ? this.FilesToMove.MarkIfInternal() : string.Empty) : ((this.FilesToMove != null) ? this.FilesToMove.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x00063508 File Offset: 0x00061708
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

		// Token: 0x06001AA7 RID: 6823 RVA: 0x00063528 File Offset: 0x00061728
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "FilesToMove={0}", new object[] { (this.FilesToMove != null) ? this.FilesToMove.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "FilesToMove={0}", new object[] { (this.FilesToMove != null) ? this.FilesToMove.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "FilesToMove={0}", new object[] { (this.FilesToMove != null) ? this.FilesToMove.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x00063607 File Offset: 0x00061807
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x00063610 File Offset: 0x00061810
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x00063619 File Offset: 0x00061819
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x00063607 File Offset: 0x00061807
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x00063624 File Offset: 0x00061824
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

		// Token: 0x04000921 RID: 2337
		private string creationMessage;

		// Token: 0x04000922 RID: 2338
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000923 RID: 2339
		private string m_filesToMove;
	}
}
