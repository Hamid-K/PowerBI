using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001AB RID: 427
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class MonitoredScopeNonMonitoredErrorWrapperException : MonitoredException
	{
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x00026784 File Offset: 0x00024984
		public override string ErrorShortName
		{
			get
			{
				if (base.InnerException != null)
				{
					return "{0}.{1}".FormatWithInvariantCulture(new object[]
					{
						base.GetType().Name,
						base.InnerException.GetType().Name
					});
				}
				return base.GetType().Name;
			}
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000267D6 File Offset: 0x000249D6
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x000267DE File Offset: 0x000249DE
		public MonitoredScopeNonMonitoredErrorWrapperException()
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x000267ED File Offset: 0x000249ED
		public MonitoredScopeNonMonitoredErrorWrapperException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00026804 File Offset: 0x00024A04
		public MonitoredScopeNonMonitoredErrorWrapperException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00026824 File Offset: 0x00024A24
		protected MonitoredScopeNonMonitoredErrorWrapperException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("MonitoredScopeNonMonitoredErrorWrapperException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("MonitoredScopeNonMonitoredErrorWrapperException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x000268C0 File Offset: 0x00024AC0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00009B3B File Offset: 0x00007D3B
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x000268F8 File Offset: 0x00024AF8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("MonitoredScopeNonMonitoredErrorWrapperException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("MonitoredScopeNonMonitoredErrorWrapperException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00026953 File Offset: 0x00024B53
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Non Monitored Error Thrown within Monitored Scope", new object[0]);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00003A57 File Offset: 0x00001C57
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string newLine = Environment.NewLine;
			return base.GetPropertiesString(markupKind);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0002696A File Offset: 0x00024B6A
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00026973 File Offset: 0x00024B73
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0002697C File Offset: 0x00024B7C
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0002696A File Offset: 0x00024B6A
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00026988 File Offset: 0x00024B88
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

		// Token: 0x0400045A RID: 1114
		private string creationMessage;

		// Token: 0x0400045B RID: 1115
		private ExceptionCulprit exceptionCulprit;
	}
}
