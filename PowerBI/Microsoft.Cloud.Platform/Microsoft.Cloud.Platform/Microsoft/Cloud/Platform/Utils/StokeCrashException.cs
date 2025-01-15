using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001F6 RID: 502
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class StokeCrashException : CrashException
	{
		// Token: 0x06000D37 RID: 3383 RVA: 0x00009B3B File Offset: 0x00007D3B
		private void TraceConstructor()
		{
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0002E268 File Offset: 0x0002C468
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0002E270 File Offset: 0x0002C470
		public StokeCrashException()
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0002E27F File Offset: 0x0002C47F
		public StokeCrashException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0002E296 File Offset: 0x0002C496
		public StokeCrashException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0002E2B4 File Offset: 0x0002C4B4
		protected StokeCrashException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("StokeCrashException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("StokeCrashException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0002E350 File Offset: 0x0002C550
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00009B3B File Offset: 0x00007D3B
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x000034FD File Offset: 0x000016FD
		public override bool IsFatal()
		{
			return true;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0002E388 File Offset: 0x0002C588
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("StokeCrashException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("StokeCrashException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0002E3E4 File Offset: 0x0002C5E4
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Process has crashed (exception thrown instead of calling FailFast/Slow): {0}", new object[] { (markupKind == PrivateInformationMarkupKind.None) ? ((base.InnerException != null && base.InnerException.Message != null) ? base.InnerException.Message : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((base.InnerException != null && base.InnerException.Message != null) ? base.InnerException.Message : string.Empty) : ((base.InnerException != null && base.InnerException.Message != null) ? base.InnerException.Message : string.Empty)) });
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x0002E489 File Offset: 0x0002C689
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

		// Token: 0x06000D43 RID: 3395 RVA: 0x0002E4A6 File Offset: 0x0002C6A6
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string newLine = Environment.NewLine;
			return base.GetPropertiesString(markupKind);
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0002E4B5 File Offset: 0x0002C6B5
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0002E4BE File Offset: 0x0002C6BE
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0002E4C7 File Offset: 0x0002C6C7
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0002E4B5 File Offset: 0x0002C6B5
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0002E4D0 File Offset: 0x0002C6D0
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

		// Token: 0x04000521 RID: 1313
		private string creationMessage;

		// Token: 0x04000522 RID: 1314
		private ExceptionCulprit exceptionCulprit;
	}
}
