using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000108 RID: 264
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class FabricIntegratorDeleteServiceFailedException : MonitoredException
	{
		// Token: 0x06000D0A RID: 3338 RVA: 0x000323BC File Offset: 0x000305BC
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x000323C4 File Offset: 0x000305C4
		// (set) Token: 0x06000D0C RID: 3340 RVA: 0x000323CC File Offset: 0x000305CC
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

		// Token: 0x06000D0D RID: 3341 RVA: 0x000323D5 File Offset: 0x000305D5
		public FabricIntegratorDeleteServiceFailedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x000323E9 File Offset: 0x000305E9
		public FabricIntegratorDeleteServiceFailedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00032400 File Offset: 0x00030600
		public FabricIntegratorDeleteServiceFailedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00032420 File Offset: 0x00030620
		protected FabricIntegratorDeleteServiceFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("FabricIntegratorDeleteServiceFailedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ServiceUri = (string)info.GetValue("FabricIntegratorDeleteServiceFailedException_ServiceUri", typeof(string));
			}
			catch (SerializationException)
			{
				this.ServiceUri = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("FabricIntegratorDeleteServiceFailedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x000324F4 File Offset: 0x000306F4
		public FabricIntegratorDeleteServiceFailedException(string serviceUri, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ServiceUri = serviceUri;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00032512 File Offset: 0x00030712
		public FabricIntegratorDeleteServiceFailedException(string serviceUri, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ServiceUri = serviceUri;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00032538 File Offset: 0x00030738
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0003256F File Offset: 0x0003076F
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00032578 File Offset: 0x00030778
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(FabricIntegratorDeleteServiceFailedException))
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

		// Token: 0x06000D16 RID: 3350 RVA: 0x00032648 File Offset: 0x00030848
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("FabricIntegratorDeleteServiceFailedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("FabricIntegratorDeleteServiceFailedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.ServiceUri != null)
			{
				info.AddValue("FabricIntegratorDeleteServiceFailedException_ServiceUri", this.ServiceUri, typeof(string));
			}
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x000326C8 File Offset: 0x000308C8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The Fabric Integrator failed to delete service '{0}'.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ServiceUri != null) ? this.ServiceUri.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ServiceUri != null) ? this.ServiceUri.MarkIfInternal() : string.Empty) : ((this.ServiceUri != null) ? this.ServiceUri.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x00032743 File Offset: 0x00030943
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

		// Token: 0x06000D19 RID: 3353 RVA: 0x00032760 File Offset: 0x00030960
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ServiceUri={0}", (this.ServiceUri != null) ? this.ServiceUri.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ServiceUri={0}", (this.ServiceUri != null) ? this.ServiceUri.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ServiceUri={0}", (this.ServiceUri != null) ? this.ServiceUri.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00032824 File Offset: 0x00030A24
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0003282D File Offset: 0x00030A2D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00032836 File Offset: 0x00030A36
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00032824 File Offset: 0x00030A24
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00032840 File Offset: 0x00030A40
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

		// Token: 0x04000340 RID: 832
		private string creationMessage;

		// Token: 0x04000341 RID: 833
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000342 RID: 834
		private string m_serviceUri;
	}
}
