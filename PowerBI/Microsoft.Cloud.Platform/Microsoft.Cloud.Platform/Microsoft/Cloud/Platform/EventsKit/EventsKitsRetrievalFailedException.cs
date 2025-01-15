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
	// Token: 0x0200037E RID: 894
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class EventsKitsRetrievalFailedException : MonitoredException
	{
		// Token: 0x06001B3C RID: 6972 RVA: 0x00066680 File Offset: 0x00064880
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x00066688 File Offset: 0x00064888
		// (set) Token: 0x06001B3E RID: 6974 RVA: 0x00066690 File Offset: 0x00064890
		public string Folders
		{
			get
			{
				return this.m_folders;
			}
			protected set
			{
				this.m_folders = value;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x00066699 File Offset: 0x00064899
		// (set) Token: 0x06001B40 RID: 6976 RVA: 0x000666A1 File Offset: 0x000648A1
		public string Exceptions
		{
			get
			{
				return this.m_exceptions;
			}
			protected set
			{
				this.m_exceptions = value;
			}
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x000666AA File Offset: 0x000648AA
		public EventsKitsRetrievalFailedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x000666C3 File Offset: 0x000648C3
		public EventsKitsRetrievalFailedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x000666DA File Offset: 0x000648DA
		public EventsKitsRetrievalFailedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x000666F8 File Offset: 0x000648F8
		protected EventsKitsRetrievalFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("EventsKitsRetrievalFailedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Folders = (string)info.GetValue("EventsKitsRetrievalFailedException_Folders", typeof(string));
			}
			catch (SerializationException)
			{
				this.Folders = null;
			}
			try
			{
				this.Exceptions = (string)info.GetValue("EventsKitsRetrievalFailedException_Exceptions", typeof(string));
			}
			catch (SerializationException)
			{
				this.Exceptions = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("EventsKitsRetrievalFailedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x00066808 File Offset: 0x00064A08
		public EventsKitsRetrievalFailedException(string folders, string exceptions)
		{
			this.Folders = folders;
			this.Exceptions = exceptions;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x00066825 File Offset: 0x00064A25
		public EventsKitsRetrievalFailedException(string folders, string exceptions, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Folders = folders;
			this.Exceptions = exceptions;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x0006684A File Offset: 0x00064A4A
		public EventsKitsRetrievalFailedException(string folders, string exceptions, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Folders = folders;
			this.Exceptions = exceptions;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x00066878 File Offset: 0x00064A78
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x000668AF File Offset: 0x00064AAF
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x000668B8 File Offset: 0x00064AB8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(EventsKitsRetrievalFailedException))
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

		// Token: 0x06001B4B RID: 6987 RVA: 0x00066988 File Offset: 0x00064B88
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("EventsKitsRetrievalFailedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("EventsKitsRetrievalFailedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Folders != null)
			{
				info.AddValue("EventsKitsRetrievalFailedException_Folders", this.Folders, typeof(string));
			}
			if (this.Exceptions != null)
			{
				info.AddValue("EventsKitsRetrievalFailedException_Exceptions", this.Exceptions, typeof(string));
			}
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x00066A2C File Offset: 0x00064C2C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to get events kits from assemblies in {0} folders. Exception details: {1}", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Folders != null) ? this.Folders.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Folders != null) ? this.Folders.MarkIfInternal() : string.Empty) : ((this.Folders != null) ? this.Folders.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Exceptions != null) ? this.Exceptions.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Exceptions != null) ? this.Exceptions.MarkIfInternal() : string.Empty) : ((this.Exceptions != null) ? this.Exceptions.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x00066B12 File Offset: 0x00064D12
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

		// Token: 0x06001B4E RID: 6990 RVA: 0x00066B30 File Offset: 0x00064D30
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Folders={0}", new object[] { (this.Folders != null) ? this.Folders.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Folders={0}", new object[] { (this.Folders != null) ? this.Folders.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Folders={0}", new object[] { (this.Folders != null) ? this.Folders.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Exceptions={0}", new object[] { (this.Exceptions != null) ? this.Exceptions.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Exceptions={0}", new object[] { (this.Exceptions != null) ? this.Exceptions.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Exceptions={0}", new object[] { (this.Exceptions != null) ? this.Exceptions.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x00066CD2 File Offset: 0x00064ED2
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x00066CDB File Offset: 0x00064EDB
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x00066CE4 File Offset: 0x00064EE4
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x00066CD2 File Offset: 0x00064ED2
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00066CF0 File Offset: 0x00064EF0
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

		// Token: 0x0400093C RID: 2364
		private string creationMessage;

		// Token: 0x0400093D RID: 2365
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400093E RID: 2366
		private string m_folders;

		// Token: 0x0400093F RID: 2367
		private string m_exceptions;
	}
}
