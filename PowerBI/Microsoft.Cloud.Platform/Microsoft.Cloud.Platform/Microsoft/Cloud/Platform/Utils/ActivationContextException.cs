using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000320 RID: 800
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ActivationContextException : Win32Exception
	{
		// Token: 0x06001737 RID: 5943 RVA: 0x00054F0C File Offset: 0x0005310C
		public virtual ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x00054F14 File Offset: 0x00053114
		public ActivationContextException()
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x00054F23 File Offset: 0x00053123
		public ActivationContextException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x00054F3A File Offset: 0x0005313A
		public ActivationContextException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x00054F58 File Offset: 0x00053158
		protected ActivationContextException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ActivationContextException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ActivationContextException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00054FF4 File Offset: 0x000531F4
		public ActivationContextException(int errorCode)
			: base(errorCode)
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00055004 File Offset: 0x00053204
		public ActivationContextException(int errorCode, string message)
			: base(errorCode, message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x0005501C File Offset: 0x0005321C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00009B3B File Offset: 0x00007D3B
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x00055054 File Offset: 0x00053254
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ActivationContextException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ActivationContextException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x000550B0 File Offset: 0x000532B0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to perform activation context operation. Error code: '{0}' Error message: ({1})", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? this.ErrorCode.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.ErrorCode.ToString(CultureInfo.InvariantCulture) : this.ErrorCode.ToString(CultureInfo.InvariantCulture)),
				(markupKind == PrivateInformationMarkupKind.None) ? (base.Message ?? string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? (base.Message ?? string.Empty) : (base.Message ?? string.Empty))
			});
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x00055154 File Offset: 0x00053354
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

		// Token: 0x06001743 RID: 5955 RVA: 0x0004CDF7 File Offset: 0x0004AFF7
		protected virtual string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			return Environment.NewLine;
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x00055171 File Offset: 0x00053371
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0005517A File Offset: 0x0005337A
		public virtual string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00055183 File Offset: 0x00053383
		public virtual string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x00055171 File Offset: 0x00053371
		public virtual string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x0005518C File Offset: 0x0005338C
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

		// Token: 0x0400081E RID: 2078
		private string creationMessage;

		// Token: 0x0400081F RID: 2079
		private ExceptionCulprit exceptionCulprit;
	}
}
