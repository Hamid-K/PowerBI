using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x0200054B RID: 1355
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class VersionFileUnavailableException : MonitoredException
	{
		// Token: 0x060028FB RID: 10491 RVA: 0x000928BD File Offset: 0x00090ABD
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060028FC RID: 10492 RVA: 0x000928C5 File Offset: 0x00090AC5
		// (set) Token: 0x060028FD RID: 10493 RVA: 0x000928CD File Offset: 0x00090ACD
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

		// Token: 0x060028FE RID: 10494 RVA: 0x000928D6 File Offset: 0x00090AD6
		public VersionFileUnavailableException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x000928EA File Offset: 0x00090AEA
		public VersionFileUnavailableException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x00092901 File Offset: 0x00090B01
		public VersionFileUnavailableException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x00092920 File Offset: 0x00090B20
		protected VersionFileUnavailableException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("VersionFileUnavailableException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Path = (string)info.GetValue("VersionFileUnavailableException_Path", typeof(string));
			}
			catch (SerializationException)
			{
				this.Path = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("VersionFileUnavailableException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x000929F4 File Offset: 0x00090BF4
		public VersionFileUnavailableException(string path, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Path = path;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x00092A12 File Offset: 0x00090C12
		public VersionFileUnavailableException(string path, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Path = path;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x00092A38 File Offset: 0x00090C38
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x00092A6F File Offset: 0x00090C6F
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x00092A78 File Offset: 0x00090C78
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(VersionFileUnavailableException))
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

		// Token: 0x06002907 RID: 10503 RVA: 0x00092B48 File Offset: 0x00090D48
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("VersionFileUnavailableException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("VersionFileUnavailableException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Path != null)
			{
				info.AddValue("VersionFileUnavailableException_Path", this.Path, typeof(string));
			}
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x00092BC8 File Offset: 0x00090DC8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Version unavailable from folder '{0}'", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.Path != null) ? this.Path.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Path != null) ? this.Path.MarkIfInternal() : string.Empty) : ((this.Path != null) ? this.Path.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06002909 RID: 10505 RVA: 0x00092C4C File Offset: 0x00090E4C
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

		// Token: 0x0600290A RID: 10506 RVA: 0x00092C6C File Offset: 0x00090E6C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Path={0}", new object[] { (this.Path != null) ? this.Path.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Path={0}", new object[] { (this.Path != null) ? this.Path.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Path={0}", new object[] { (this.Path != null) ? this.Path.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x00092D4B File Offset: 0x00090F4B
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x00092D54 File Offset: 0x00090F54
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x00092D5D File Offset: 0x00090F5D
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x00092D4B File Offset: 0x00090F4B
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x00092D68 File Offset: 0x00090F68
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

		// Token: 0x04000EA6 RID: 3750
		private string creationMessage;

		// Token: 0x04000EA7 RID: 3751
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000EA8 RID: 3752
		private string m_path;
	}
}
