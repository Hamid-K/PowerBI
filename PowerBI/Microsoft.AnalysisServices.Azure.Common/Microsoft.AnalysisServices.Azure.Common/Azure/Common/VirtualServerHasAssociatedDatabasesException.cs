using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000CD RID: 205
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class VirtualServerHasAssociatedDatabasesException : StateManagerBaseException
	{
		// Token: 0x06000834 RID: 2100 RVA: 0x0001AFAC File Offset: 0x000191AC
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x0001AFB4 File Offset: 0x000191B4
		// (set) Token: 0x06000836 RID: 2102 RVA: 0x0001AFBC File Offset: 0x000191BC
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

		// Token: 0x06000837 RID: 2103 RVA: 0x0001AFC5 File Offset: 0x000191C5
		public VirtualServerHasAssociatedDatabasesException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001AFD9 File Offset: 0x000191D9
		public VirtualServerHasAssociatedDatabasesException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001AFF0 File Offset: 0x000191F0
		public VirtualServerHasAssociatedDatabasesException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001B010 File Offset: 0x00019210
		protected VirtualServerHasAssociatedDatabasesException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("VirtualServerHasAssociatedDatabasesException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.VirtualServer = (string)info.GetValue("VirtualServerHasAssociatedDatabasesException_VirtualServer", typeof(string));
			}
			catch (SerializationException)
			{
				this.VirtualServer = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("VirtualServerHasAssociatedDatabasesException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001B0E4 File Offset: 0x000192E4
		public VirtualServerHasAssociatedDatabasesException(string virtualServer, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.VirtualServer = virtualServer;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001B102 File Offset: 0x00019302
		public VirtualServerHasAssociatedDatabasesException(string virtualServer, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.VirtualServer = virtualServer;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001B128 File Offset: 0x00019328
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001B160 File Offset: 0x00019360
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("VirtualServerHasAssociatedDatabasesException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("VirtualServerHasAssociatedDatabasesException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.VirtualServer != null)
			{
				info.AddValue("VirtualServerHasAssociatedDatabasesException_VirtualServer", this.VirtualServer, typeof(string));
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001B1E0 File Offset: 0x000193E0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Virtual Server {0} has databases associated with it. Cannot delete the Virtual Server", (markupKind == PrivateInformationMarkupKind.None) ? ((this.VirtualServer != null) ? this.VirtualServer.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.VirtualServer != null) ? this.VirtualServer.MarkIfInternal() : string.Empty) : ((this.VirtualServer != null) ? this.VirtualServer.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x0001B25B File Offset: 0x0001945B
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

		// Token: 0x06000842 RID: 2114 RVA: 0x0001B278 File Offset: 0x00019478
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "VirtualServer={0}", (this.VirtualServer != null) ? this.VirtualServer.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "VirtualServer={0}", (this.VirtualServer != null) ? this.VirtualServer.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "VirtualServer={0}", (this.VirtualServer != null) ? this.VirtualServer.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001B33C File Offset: 0x0001953C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001B345 File Offset: 0x00019545
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001B34E File Offset: 0x0001954E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001B33C File Offset: 0x0001953C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001B358 File Offset: 0x00019558
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

		// Token: 0x0400027C RID: 636
		private string creationMessage;

		// Token: 0x0400027D RID: 637
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400027E RID: 638
		private string m_virtualServer;
	}
}
