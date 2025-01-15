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
	// Token: 0x02000378 RID: 888
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class EventFilesDeletionException : MonitoredException
	{
		// Token: 0x06001AAD RID: 6829 RVA: 0x00063810 File Offset: 0x00061A10
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001AAE RID: 6830 RVA: 0x00063818 File Offset: 0x00061A18
		// (set) Token: 0x06001AAF RID: 6831 RVA: 0x00063820 File Offset: 0x00061A20
		public string FileToDelete
		{
			get
			{
				return this.m_fileToDelete;
			}
			protected set
			{
				this.m_fileToDelete = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x00063829 File Offset: 0x00061A29
		// (set) Token: 0x06001AB1 RID: 6833 RVA: 0x00063831 File Offset: 0x00061A31
		public int DirectorySizeInMb
		{
			get
			{
				return this.m_directorySizeInMb;
			}
			protected set
			{
				this.m_directorySizeInMb = value;
			}
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x0006383A File Offset: 0x00061A3A
		public EventFilesDeletionException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x0006384E File Offset: 0x00061A4E
		public EventFilesDeletionException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x00063865 File Offset: 0x00061A65
		public EventFilesDeletionException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x00063884 File Offset: 0x00061A84
		protected EventFilesDeletionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("EventFilesDeletionException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.FileToDelete = (string)info.GetValue("EventFilesDeletionException_FileToDelete", typeof(string));
			}
			catch (SerializationException)
			{
				this.FileToDelete = null;
			}
			this.DirectorySizeInMb = (int)info.GetValue("EventFilesDeletionException_DirectorySizeInMb", typeof(int));
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("EventFilesDeletionException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x00063978 File Offset: 0x00061B78
		public EventFilesDeletionException(string fileToDelete, int directorySizeInMb)
		{
			this.FileToDelete = fileToDelete;
			this.DirectorySizeInMb = directorySizeInMb;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x00063995 File Offset: 0x00061B95
		public EventFilesDeletionException(string fileToDelete, int directorySizeInMb, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.FileToDelete = fileToDelete;
			this.DirectorySizeInMb = directorySizeInMb;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x000639BA File Offset: 0x00061BBA
		public EventFilesDeletionException(string fileToDelete, int directorySizeInMb, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.FileToDelete = fileToDelete;
			this.DirectorySizeInMb = directorySizeInMb;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x000639E8 File Offset: 0x00061BE8
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x00063A1F File Offset: 0x00061C1F
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x00063A28 File Offset: 0x00061C28
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(EventFilesDeletionException))
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

		// Token: 0x06001ABC RID: 6844 RVA: 0x00063AF8 File Offset: 0x00061CF8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("EventFilesDeletionException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("EventFilesDeletionException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.FileToDelete != null)
			{
				info.AddValue("EventFilesDeletionException_FileToDelete", this.FileToDelete, typeof(string));
			}
			info.AddValue("EventFilesDeletionException_DirectorySizeInMb", this.DirectorySizeInMb, typeof(int));
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x00063B98 File Offset: 0x00061D98
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed deleting event file: '{0}'. Current directory size: {1} MB - see inner exception for details", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.FileToDelete != null) ? this.FileToDelete.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.FileToDelete != null) ? this.FileToDelete.MarkIfInternal() : string.Empty) : ((this.FileToDelete != null) ? this.FileToDelete.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? this.DirectorySizeInMb.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.DirectorySizeInMb.ToString(CultureInfo.InvariantCulture) : this.DirectorySizeInMb.ToString(CultureInfo.InvariantCulture))
			});
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001ABE RID: 6846 RVA: 0x00063C63 File Offset: 0x00061E63
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

		// Token: 0x06001ABF RID: 6847 RVA: 0x00063C80 File Offset: 0x00061E80
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "FileToDelete={0}", new object[] { (this.FileToDelete != null) ? this.FileToDelete.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "FileToDelete={0}", new object[] { (this.FileToDelete != null) ? this.FileToDelete.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "FileToDelete={0}", new object[] { (this.FileToDelete != null) ? this.FileToDelete.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DirectorySizeInMb={0}", new object[] { this.DirectorySizeInMb.ToString(CultureInfo.InvariantCulture) }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DirectorySizeInMb={0}", new object[] { this.DirectorySizeInMb.ToString(CultureInfo.InvariantCulture) }) : string.Format(CultureInfo.CurrentCulture, "DirectorySizeInMb={0}", new object[] { this.DirectorySizeInMb.ToString(CultureInfo.InvariantCulture) })));
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00063E07 File Offset: 0x00062007
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x00063E10 File Offset: 0x00062010
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x00063E19 File Offset: 0x00062019
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x00063E07 File Offset: 0x00062007
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x00063E24 File Offset: 0x00062024
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

		// Token: 0x04000924 RID: 2340
		private string creationMessage;

		// Token: 0x04000925 RID: 2341
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000926 RID: 2342
		private string m_fileToDelete;

		// Token: 0x04000927 RID: 2343
		private int m_directorySizeInMb;
	}
}
