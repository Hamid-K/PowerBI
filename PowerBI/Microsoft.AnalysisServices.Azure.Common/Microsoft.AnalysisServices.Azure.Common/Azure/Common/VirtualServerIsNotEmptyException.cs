using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000D0 RID: 208
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class VirtualServerIsNotEmptyException : StateManagerBaseException
	{
		// Token: 0x06000871 RID: 2161 RVA: 0x0001C14C File Offset: 0x0001A34C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x0001C154 File Offset: 0x0001A354
		// (set) Token: 0x06000873 RID: 2163 RVA: 0x0001C15C File Offset: 0x0001A35C
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

		// Token: 0x06000874 RID: 2164 RVA: 0x0001C165 File Offset: 0x0001A365
		public VirtualServerIsNotEmptyException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001C179 File Offset: 0x0001A379
		public VirtualServerIsNotEmptyException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0001C190 File Offset: 0x0001A390
		public VirtualServerIsNotEmptyException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001C1B0 File Offset: 0x0001A3B0
		protected VirtualServerIsNotEmptyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("VirtualServerIsNotEmptyException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.VirtualServer = (string)info.GetValue("VirtualServerIsNotEmptyException_VirtualServer", typeof(string));
			}
			catch (SerializationException)
			{
				this.VirtualServer = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("VirtualServerIsNotEmptyException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0001C284 File Offset: 0x0001A484
		public VirtualServerIsNotEmptyException(string virtualServer, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.VirtualServer = virtualServer;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0001C2A2 File Offset: 0x0001A4A2
		public VirtualServerIsNotEmptyException(string virtualServer, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.VirtualServer = virtualServer;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001C2C8 File Offset: 0x0001A4C8
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001C2FF File Offset: 0x0001A4FF
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001C308 File Offset: 0x0001A508
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(VirtualServerIsNotEmptyException))
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

		// Token: 0x0600087D RID: 2173 RVA: 0x0001C3D8 File Offset: 0x0001A5D8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("VirtualServerIsNotEmptyException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("VirtualServerIsNotEmptyException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.VirtualServer != null)
			{
				info.AddValue("VirtualServerIsNotEmptyException_VirtualServer", this.VirtualServer, typeof(string));
			}
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001C458 File Offset: 0x0001A658
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Virtual Server {0} is not empty", (markupKind == PrivateInformationMarkupKind.None) ? ((this.VirtualServer != null) ? this.VirtualServer.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.VirtualServer != null) ? this.VirtualServer.MarkIfInternal() : string.Empty) : ((this.VirtualServer != null) ? this.VirtualServer.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x0001C4D3 File Offset: 0x0001A6D3
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

		// Token: 0x06000880 RID: 2176 RVA: 0x0001C4F0 File Offset: 0x0001A6F0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "VirtualServer={0}", (this.VirtualServer != null) ? this.VirtualServer.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "VirtualServer={0}", (this.VirtualServer != null) ? this.VirtualServer.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "VirtualServer={0}", (this.VirtualServer != null) ? this.VirtualServer.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001C5B4 File Offset: 0x0001A7B4
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001C5BD File Offset: 0x0001A7BD
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0001C5C6 File Offset: 0x0001A7C6
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001C5B4 File Offset: 0x0001A7B4
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001C5D0 File Offset: 0x0001A7D0
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

		// Token: 0x04000285 RID: 645
		private string creationMessage;

		// Token: 0x04000286 RID: 646
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000287 RID: 647
		private string m_virtualServer;
	}
}
