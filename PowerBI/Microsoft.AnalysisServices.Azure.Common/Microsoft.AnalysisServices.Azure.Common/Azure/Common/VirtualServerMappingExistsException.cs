using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000C1 RID: 193
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class VirtualServerMappingExistsException : StateManagerBaseException
	{
		// Token: 0x06000726 RID: 1830 RVA: 0x0001597C File Offset: 0x00013B7C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x00015984 File Offset: 0x00013B84
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x0001598C File Offset: 0x00013B8C
		public string VirtualServer
		{
			get
			{
				return this.m_virtualServer;
			}
			protected set
			{
				this.m_virtualServer = value;
			}
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00015995 File Offset: 0x00013B95
		public VirtualServerMappingExistsException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x000159A9 File Offset: 0x00013BA9
		public VirtualServerMappingExistsException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x000159C0 File Offset: 0x00013BC0
		public VirtualServerMappingExistsException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x000159E0 File Offset: 0x00013BE0
		protected VirtualServerMappingExistsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("VirtualServerMappingExistsException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.VirtualServer = (string)info.GetValue("VirtualServerMappingExistsException_VirtualServer", typeof(string));
			}
			catch (SerializationException)
			{
				this.VirtualServer = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("VirtualServerMappingExistsException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00015AB4 File Offset: 0x00013CB4
		public VirtualServerMappingExistsException(string virtualServer, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.VirtualServer = virtualServer;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00015AD2 File Offset: 0x00013CD2
		public VirtualServerMappingExistsException(string virtualServer, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.VirtualServer = virtualServer;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00015AF8 File Offset: 0x00013CF8
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00015B30 File Offset: 0x00013D30
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("VirtualServerMappingExistsException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("VirtualServerMappingExistsException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.VirtualServer != null)
			{
				info.AddValue("VirtualServerMappingExistsException_VirtualServer", this.VirtualServer, typeof(string));
			}
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00015BB0 File Offset: 0x00013DB0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Virtual Server {0} already exists in the State Manager Service KVS", (markupKind == PrivateInformationMarkupKind.None) ? ((this.VirtualServer != null) ? this.VirtualServer.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.VirtualServer != null) ? this.VirtualServer.MarkIfInternal() : string.Empty) : ((this.VirtualServer != null) ? this.VirtualServer.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x00015C2B File Offset: 0x00013E2B
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

		// Token: 0x06000734 RID: 1844 RVA: 0x00015C48 File Offset: 0x00013E48
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "VirtualServer={0}", (this.VirtualServer != null) ? this.VirtualServer.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "VirtualServer={0}", (this.VirtualServer != null) ? this.VirtualServer.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "VirtualServer={0}", (this.VirtualServer != null) ? this.VirtualServer.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00015D0C File Offset: 0x00013F0C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00015D15 File Offset: 0x00013F15
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00015D1E File Offset: 0x00013F1E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00015D0C File Offset: 0x00013F0C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00015D28 File Offset: 0x00013F28
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

		// Token: 0x04000250 RID: 592
		private string creationMessage;

		// Token: 0x04000251 RID: 593
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000252 RID: 594
		private string m_virtualServer;
	}
}
