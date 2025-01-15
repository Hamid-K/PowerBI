using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000F0 RID: 240
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class MonitoredAmoException : MonitoredException
	{
		// Token: 0x06000B10 RID: 2832 RVA: 0x00028A04 File Offset: 0x00026C04
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00028A0C File Offset: 0x00026C0C
		public MonitoredAmoException()
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00028A1B File Offset: 0x00026C1B
		public MonitoredAmoException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00028A32 File Offset: 0x00026C32
		public MonitoredAmoException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00028A50 File Offset: 0x00026C50
		protected MonitoredAmoException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("MonitoredAmoException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("MonitoredAmoException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00028AEC File Offset: 0x00026CEC
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00028B24 File Offset: 0x00026D24
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("MonitoredAmoException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("MonitoredAmoException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00028B7F File Offset: 0x00026D7F
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "An AmoException or its subclass is thrown. Please check inner exceptions.", Array.Empty<object>());
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00028B95 File Offset: 0x00026D95
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

		// Token: 0x06000B1A RID: 2842 RVA: 0x00002C02 File Offset: 0x00000E02
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string newLine = Environment.NewLine;
			return base.GetPropertiesString(markupKind);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00028BB2 File Offset: 0x00026DB2
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00028BBB File Offset: 0x00026DBB
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00028BC4 File Offset: 0x00026DC4
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00028BB2 File Offset: 0x00026DB2
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00028BD0 File Offset: 0x00026DD0
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

		// Token: 0x040002F1 RID: 753
		private string creationMessage;

		// Token: 0x040002F2 RID: 754
		private ExceptionCulprit exceptionCulprit;
	}
}
