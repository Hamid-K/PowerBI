using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000322 RID: 802
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class AsyncRetriableCommandFailedException : MonitoredException
	{
		// Token: 0x0600175F RID: 5983 RVA: 0x00055AE0 File Offset: 0x00053CE0
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x00055AE8 File Offset: 0x00053CE8
		// (set) Token: 0x06001761 RID: 5985 RVA: 0x00055AF0 File Offset: 0x00053CF0
		public string CommandDescription
		{
			get
			{
				return this.m_commandDescription;
			}
			protected set
			{
				this.m_commandDescription = value;
			}
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x00055AF9 File Offset: 0x00053CF9
		public AsyncRetriableCommandFailedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x00055B0D File Offset: 0x00053D0D
		public AsyncRetriableCommandFailedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x00055B24 File Offset: 0x00053D24
		public AsyncRetriableCommandFailedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00055B44 File Offset: 0x00053D44
		protected AsyncRetriableCommandFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("AsyncRetriableCommandFailedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.CommandDescription = (string)info.GetValue("AsyncRetriableCommandFailedException_CommandDescription", typeof(string));
			}
			catch (SerializationException)
			{
				this.CommandDescription = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("AsyncRetriableCommandFailedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x00055C18 File Offset: 0x00053E18
		public AsyncRetriableCommandFailedException(string commandDescription, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.CommandDescription = commandDescription;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x00055C36 File Offset: 0x00053E36
		public AsyncRetriableCommandFailedException(string commandDescription, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.CommandDescription = commandDescription;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x00055C5C File Offset: 0x00053E5C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00009B3B File Offset: 0x00007D3B
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x00055C94 File Offset: 0x00053E94
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("AsyncRetriableCommandFailedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("AsyncRetriableCommandFailedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.CommandDescription != null)
			{
				info.AddValue("AsyncRetriableCommandFailedException_CommandDescription", this.CommandDescription, typeof(string));
			}
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x00055D14 File Offset: 0x00053F14
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "AsyncRetriableCommand {0} failed.", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((this.CommandDescription != null) ? this.CommandDescription.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.CommandDescription != null) ? this.CommandDescription.MarkIfInternal() : string.Empty) : ((this.CommandDescription != null) ? this.CommandDescription.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)) });
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00055D98 File Offset: 0x00053F98
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "CommandDescription={0}", new object[] { (this.CommandDescription != null) ? this.CommandDescription.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "CommandDescription={0}", new object[] { (this.CommandDescription != null) ? this.CommandDescription.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "CommandDescription={0}", new object[] { (this.CommandDescription != null) ? this.CommandDescription.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00055E77 File Offset: 0x00054077
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00055E80 File Offset: 0x00054080
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x00055E89 File Offset: 0x00054089
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x00055E77 File Offset: 0x00054077
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00055E94 File Offset: 0x00054094
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

		// Token: 0x04000824 RID: 2084
		private string creationMessage;

		// Token: 0x04000825 RID: 2085
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000826 RID: 2086
		private string m_commandDescription;
	}
}
