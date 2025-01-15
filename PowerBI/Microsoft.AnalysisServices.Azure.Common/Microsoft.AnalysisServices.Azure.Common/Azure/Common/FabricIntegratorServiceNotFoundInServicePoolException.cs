using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000F9 RID: 249
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class FabricIntegratorServiceNotFoundInServicePoolException : MonitoredException
	{
		// Token: 0x06000BCC RID: 3020 RVA: 0x0002C1CC File Offset: 0x0002A3CC
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x0002C1D4 File Offset: 0x0002A3D4
		// (set) Token: 0x06000BCE RID: 3022 RVA: 0x0002C1DC File Offset: 0x0002A3DC
		public string ServiceUri
		{
			get
			{
				return this.m_serviceUri;
			}
			protected set
			{
				this.m_serviceUri = value;
			}
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0002C1E5 File Offset: 0x0002A3E5
		public FabricIntegratorServiceNotFoundInServicePoolException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0002C1F9 File Offset: 0x0002A3F9
		public FabricIntegratorServiceNotFoundInServicePoolException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0002C210 File Offset: 0x0002A410
		public FabricIntegratorServiceNotFoundInServicePoolException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0002C230 File Offset: 0x0002A430
		protected FabricIntegratorServiceNotFoundInServicePoolException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("FabricIntegratorServiceNotFoundInServicePoolException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ServiceUri = (string)info.GetValue("FabricIntegratorServiceNotFoundInServicePoolException_ServiceUri", typeof(string));
			}
			catch (SerializationException)
			{
				this.ServiceUri = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("FabricIntegratorServiceNotFoundInServicePoolException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0002C304 File Offset: 0x0002A504
		public FabricIntegratorServiceNotFoundInServicePoolException(string serviceUri, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ServiceUri = serviceUri;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0002C322 File Offset: 0x0002A522
		public FabricIntegratorServiceNotFoundInServicePoolException(string serviceUri, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ServiceUri = serviceUri;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0002C348 File Offset: 0x0002A548
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0002C37F File Offset: 0x0002A57F
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0002C388 File Offset: 0x0002A588
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(FabricIntegratorServiceNotFoundInServicePoolException))
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

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0002C458 File Offset: 0x0002A658
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("FabricIntegratorServiceNotFoundInServicePoolException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("FabricIntegratorServiceNotFoundInServicePoolException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ServiceUri != null)
			{
				info.AddValue("FabricIntegratorServiceNotFoundInServicePoolException_ServiceUri", this.ServiceUri, typeof(string));
			}
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0002C4D8 File Offset: 0x0002A6D8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The Fabric Integrator failed to find serice '{0}' in local service pool.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ServiceUri != null) ? this.ServiceUri.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ServiceUri != null) ? this.ServiceUri.MarkIfInternal() : string.Empty) : ((this.ServiceUri != null) ? this.ServiceUri.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x0002C553 File Offset: 0x0002A753
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

		// Token: 0x06000BDB RID: 3035 RVA: 0x0002C570 File Offset: 0x0002A770
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ServiceUri={0}", (this.ServiceUri != null) ? this.ServiceUri.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ServiceUri={0}", (this.ServiceUri != null) ? this.ServiceUri.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ServiceUri={0}", (this.ServiceUri != null) ? this.ServiceUri.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0002C634 File Offset: 0x0002A834
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0002C63D File Offset: 0x0002A83D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0002C646 File Offset: 0x0002A846
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0002C634 File Offset: 0x0002A834
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0002C650 File Offset: 0x0002A850
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

		// Token: 0x0400030E RID: 782
		private string creationMessage;

		// Token: 0x0400030F RID: 783
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000310 RID: 784
		private string m_serviceUri;
	}
}
