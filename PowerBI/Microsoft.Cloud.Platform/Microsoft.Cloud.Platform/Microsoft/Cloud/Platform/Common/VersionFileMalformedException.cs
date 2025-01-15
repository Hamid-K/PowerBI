using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x0200054D RID: 1357
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class VersionFileMalformedException : MonitoredException
	{
		// Token: 0x06002925 RID: 10533 RVA: 0x000935EC File Offset: 0x000917EC
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06002926 RID: 10534 RVA: 0x000935F4 File Offset: 0x000917F4
		// (set) Token: 0x06002927 RID: 10535 RVA: 0x000935FC File Offset: 0x000917FC
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

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06002928 RID: 10536 RVA: 0x00093605 File Offset: 0x00091805
		// (set) Token: 0x06002929 RID: 10537 RVA: 0x0009360D File Offset: 0x0009180D
		public string VersionString
		{
			get
			{
				return this.m_versionString;
			}
			protected set
			{
				this.m_versionString = value;
			}
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x00093616 File Offset: 0x00091816
		public VersionFileMalformedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x0009362F File Offset: 0x0009182F
		public VersionFileMalformedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x00093646 File Offset: 0x00091846
		public VersionFileMalformedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x00093664 File Offset: 0x00091864
		protected VersionFileMalformedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("VersionFileMalformedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Path = (string)info.GetValue("VersionFileMalformedException_Path", typeof(string));
			}
			catch (SerializationException)
			{
				this.Path = null;
			}
			try
			{
				this.VersionString = (string)info.GetValue("VersionFileMalformedException_VersionString", typeof(string));
			}
			catch (SerializationException)
			{
				this.VersionString = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("VersionFileMalformedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x00093774 File Offset: 0x00091974
		public VersionFileMalformedException(string path, string versionString)
		{
			this.Path = path;
			this.VersionString = versionString;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x00093791 File Offset: 0x00091991
		public VersionFileMalformedException(string path, string versionString, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Path = path;
			this.VersionString = versionString;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x000937B6 File Offset: 0x000919B6
		public VersionFileMalformedException(string path, string versionString, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Path = path;
			this.VersionString = versionString;
			this.ConstructorInternal(false);
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x000937E4 File Offset: 0x000919E4
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x0009381B File Offset: 0x00091A1B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x00093824 File Offset: 0x00091A24
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(VersionFileMalformedException))
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

		// Token: 0x06002934 RID: 10548 RVA: 0x000938F4 File Offset: 0x00091AF4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("VersionFileMalformedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("VersionFileMalformedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Path != null)
			{
				info.AddValue("VersionFileMalformedException_Path", this.Path, typeof(string));
			}
			if (this.VersionString != null)
			{
				info.AddValue("VersionFileMalformedException_VersionString", this.VersionString, typeof(string));
			}
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x00093998 File Offset: 0x00091B98
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Version malformed in folder '{0}'. Malformed text: '{1}'", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Path != null) ? this.Path.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Path != null) ? this.Path.MarkIfInternal() : string.Empty) : ((this.Path != null) ? this.Path.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.VersionString != null) ? this.VersionString.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.VersionString != null) ? this.VersionString.MarkIfInternal() : string.Empty) : ((this.VersionString != null) ? this.VersionString.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06002936 RID: 10550 RVA: 0x00093A7E File Offset: 0x00091C7E
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

		// Token: 0x06002937 RID: 10551 RVA: 0x00093A9C File Offset: 0x00091C9C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Path={0}", new object[] { (this.Path != null) ? this.Path.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Path={0}", new object[] { (this.Path != null) ? this.Path.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Path={0}", new object[] { (this.Path != null) ? this.Path.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "VersionString={0}", new object[] { (this.VersionString != null) ? this.VersionString.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "VersionString={0}", new object[] { (this.VersionString != null) ? this.VersionString.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "VersionString={0}", new object[] { (this.VersionString != null) ? this.VersionString.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x00093C3E File Offset: 0x00091E3E
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x00093C47 File Offset: 0x00091E47
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x00093C50 File Offset: 0x00091E50
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x00093C3E File Offset: 0x00091E3E
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x00093C5C File Offset: 0x00091E5C
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

		// Token: 0x04000EAC RID: 3756
		private string creationMessage;

		// Token: 0x04000EAD RID: 3757
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000EAE RID: 3758
		private string m_path;

		// Token: 0x04000EAF RID: 3759
		private string m_versionString;
	}
}
