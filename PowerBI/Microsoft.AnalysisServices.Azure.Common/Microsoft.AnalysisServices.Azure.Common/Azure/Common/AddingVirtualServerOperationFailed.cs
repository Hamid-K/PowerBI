using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000C0 RID: 192
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class AddingVirtualServerOperationFailed : StateManagerBaseException
	{
		// Token: 0x06000712 RID: 1810 RVA: 0x000153E4 File Offset: 0x000135E4
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x000153EC File Offset: 0x000135EC
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x000153F4 File Offset: 0x000135F4
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

		// Token: 0x06000715 RID: 1813 RVA: 0x000153FD File Offset: 0x000135FD
		public AddingVirtualServerOperationFailed()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00015411 File Offset: 0x00013611
		public AddingVirtualServerOperationFailed(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00015428 File Offset: 0x00013628
		public AddingVirtualServerOperationFailed(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00015448 File Offset: 0x00013648
		protected AddingVirtualServerOperationFailed(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("AddingVirtualServerOperationFailed_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.VirtualServer = (string)info.GetValue("AddingVirtualServerOperationFailed_VirtualServer", typeof(string));
			}
			catch (SerializationException)
			{
				this.VirtualServer = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("AddingVirtualServerOperationFailed_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001551C File Offset: 0x0001371C
		public AddingVirtualServerOperationFailed(string virtualServer, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.VirtualServer = virtualServer;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001553A File Offset: 0x0001373A
		public AddingVirtualServerOperationFailed(string virtualServer, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.VirtualServer = virtualServer;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00015560 File Offset: 0x00013760
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00015598 File Offset: 0x00013798
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("AddingVirtualServerOperationFailed_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("AddingVirtualServerOperationFailed_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.VirtualServer != null)
			{
				info.AddValue("AddingVirtualServerOperationFailed_VirtualServer", this.VirtualServer, typeof(string));
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00015618 File Offset: 0x00013818
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to add Virtual Server {0}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.VirtualServer != null) ? this.VirtualServer.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.VirtualServer != null) ? this.VirtualServer.MarkIfInternal() : string.Empty) : ((this.VirtualServer != null) ? this.VirtualServer.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x00015693 File Offset: 0x00013893
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

		// Token: 0x06000720 RID: 1824 RVA: 0x000156B0 File Offset: 0x000138B0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "VirtualServer={0}", (this.VirtualServer != null) ? this.VirtualServer.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "VirtualServer={0}", (this.VirtualServer != null) ? this.VirtualServer.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "VirtualServer={0}", (this.VirtualServer != null) ? this.VirtualServer.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00015774 File Offset: 0x00013974
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001577D File Offset: 0x0001397D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00015786 File Offset: 0x00013986
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00015774 File Offset: 0x00013974
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00015790 File Offset: 0x00013990
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

		// Token: 0x0400024D RID: 589
		private string creationMessage;

		// Token: 0x0400024E RID: 590
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400024F RID: 591
		private string m_virtualServer;
	}
}
