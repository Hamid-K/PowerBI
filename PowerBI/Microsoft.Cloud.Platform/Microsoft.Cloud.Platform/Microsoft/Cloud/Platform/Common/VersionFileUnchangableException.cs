using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x0200054C RID: 1356
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class VersionFileUnchangableException : MonitoredException
	{
		// Token: 0x06002910 RID: 10512 RVA: 0x00092F54 File Offset: 0x00091154
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06002911 RID: 10513 RVA: 0x00092F5C File Offset: 0x0009115C
		// (set) Token: 0x06002912 RID: 10514 RVA: 0x00092F64 File Offset: 0x00091164
		public string Path
		{
			get
			{
				return this.m_path;
			}
			protected set
			{
				this.m_path = value;
			}
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x00092F6D File Offset: 0x0009116D
		public VersionFileUnchangableException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x00092F81 File Offset: 0x00091181
		public VersionFileUnchangableException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x00092F98 File Offset: 0x00091198
		public VersionFileUnchangableException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x00092FB8 File Offset: 0x000911B8
		protected VersionFileUnchangableException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("VersionFileUnchangableException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Path = (string)info.GetValue("VersionFileUnchangableException_Path", typeof(string));
			}
			catch (SerializationException)
			{
				this.Path = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("VersionFileUnchangableException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x0009308C File Offset: 0x0009128C
		public VersionFileUnchangableException(string path, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Path = path;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x000930AA File Offset: 0x000912AA
		public VersionFileUnchangableException(string path, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Path = path;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x000930D0 File Offset: 0x000912D0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x00093107 File Offset: 0x00091307
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x00093110 File Offset: 0x00091310
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(VersionFileUnchangableException))
			{
				TraceSourceBase<CommonTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<CommonTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<CommonTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x000931E0 File Offset: 0x000913E0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("VersionFileUnchangableException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("VersionFileUnchangableException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Path != null)
			{
				info.AddValue("VersionFileUnchangableException_Path", this.Path, typeof(string));
			}
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x00093260 File Offset: 0x00091460
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Version in '{0}' is non-existent, inaccessible or unchangable", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.Path != null) ? this.Path.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Path != null) ? this.Path.MarkIfInternal() : string.Empty) : ((this.Path != null) ? this.Path.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x0600291E RID: 10526 RVA: 0x000932E4 File Offset: 0x000914E4
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

		// Token: 0x0600291F RID: 10527 RVA: 0x00093304 File Offset: 0x00091504
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Path={0}", new object[] { (this.Path != null) ? this.Path.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Path={0}", new object[] { (this.Path != null) ? this.Path.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Path={0}", new object[] { (this.Path != null) ? this.Path.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x000933E3 File Offset: 0x000915E3
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x000933EC File Offset: 0x000915EC
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x000933F5 File Offset: 0x000915F5
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x000933E3 File Offset: 0x000915E3
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x00093400 File Offset: 0x00091600
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

		// Token: 0x04000EA9 RID: 3753
		private string creationMessage;

		// Token: 0x04000EAA RID: 3754
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000EAB RID: 3755
		private string m_path;
	}
}
