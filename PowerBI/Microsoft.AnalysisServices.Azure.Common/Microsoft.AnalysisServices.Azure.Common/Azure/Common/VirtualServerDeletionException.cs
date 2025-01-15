using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000CE RID: 206
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class VirtualServerDeletionException : StateManagerBaseException
	{
		// Token: 0x06000848 RID: 2120 RVA: 0x0001B544 File Offset: 0x00019744
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x0001B54C File Offset: 0x0001974C
		// (set) Token: 0x0600084A RID: 2122 RVA: 0x0001B554 File Offset: 0x00019754
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

		// Token: 0x0600084B RID: 2123 RVA: 0x0001B55D File Offset: 0x0001975D
		public VirtualServerDeletionException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001B571 File Offset: 0x00019771
		public VirtualServerDeletionException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001B588 File Offset: 0x00019788
		public VirtualServerDeletionException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001B5A8 File Offset: 0x000197A8
		protected VirtualServerDeletionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("VirtualServerDeletionException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.VirtualServer = (string)info.GetValue("VirtualServerDeletionException_VirtualServer", typeof(string));
			}
			catch (SerializationException)
			{
				this.VirtualServer = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("VirtualServerDeletionException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001B67C File Offset: 0x0001987C
		public VirtualServerDeletionException(string virtualServer, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.VirtualServer = virtualServer;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001B69A File Offset: 0x0001989A
		public VirtualServerDeletionException(string virtualServer, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.VirtualServer = virtualServer;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001B6C0 File Offset: 0x000198C0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001B6F7 File Offset: 0x000198F7
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001B700 File Offset: 0x00019900
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(VirtualServerDeletionException))
			{
				TraceSourceBase<ANCommonTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<ANCommonTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<ANCommonTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001B7D0 File Offset: 0x000199D0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("VirtualServerDeletionException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("VirtualServerDeletionException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.VirtualServer != null)
			{
				info.AddValue("VirtualServerDeletionException_VirtualServer", this.VirtualServer, typeof(string));
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001B850 File Offset: 0x00019A50
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Virtual Server {0} was not deleted", (markupKind == PrivateInformationMarkupKind.None) ? ((this.VirtualServer != null) ? this.VirtualServer.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.VirtualServer != null) ? this.VirtualServer.MarkIfInternal() : string.Empty) : ((this.VirtualServer != null) ? this.VirtualServer.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x0001B8CB File Offset: 0x00019ACB
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

		// Token: 0x06000857 RID: 2135 RVA: 0x0001B8E8 File Offset: 0x00019AE8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "VirtualServer={0}", (this.VirtualServer != null) ? this.VirtualServer.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "VirtualServer={0}", (this.VirtualServer != null) ? this.VirtualServer.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "VirtualServer={0}", (this.VirtualServer != null) ? this.VirtualServer.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001B9AC File Offset: 0x00019BAC
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001B9B5 File Offset: 0x00019BB5
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001B9BE File Offset: 0x00019BBE
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001B9AC File Offset: 0x00019BAC
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001B9C8 File Offset: 0x00019BC8
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

		// Token: 0x0400027F RID: 639
		private string creationMessage;

		// Token: 0x04000280 RID: 640
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000281 RID: 641
		private string m_virtualServer;
	}
}
