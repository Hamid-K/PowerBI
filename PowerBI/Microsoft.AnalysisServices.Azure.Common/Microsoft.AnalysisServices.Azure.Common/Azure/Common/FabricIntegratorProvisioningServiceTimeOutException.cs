using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000FF RID: 255
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class FabricIntegratorProvisioningServiceTimeOutException : MonitoredException
	{
		// Token: 0x06000C56 RID: 3158 RVA: 0x0002EEDC File Offset: 0x0002D0DC
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x0002EEE4 File Offset: 0x0002D0E4
		// (set) Token: 0x06000C58 RID: 3160 RVA: 0x0002EEEC File Offset: 0x0002D0EC
		public string FlowName
		{
			get
			{
				return this.m_flowName;
			}
			protected set
			{
				this.m_flowName = value;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x0002EEF5 File Offset: 0x0002D0F5
		// (set) Token: 0x06000C5A RID: 3162 RVA: 0x0002EEFD File Offset: 0x0002D0FD
		public string TimeOut
		{
			get
			{
				return this.m_timeOut;
			}
			protected set
			{
				this.m_timeOut = value;
			}
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0002EF06 File Offset: 0x0002D106
		public FabricIntegratorProvisioningServiceTimeOutException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0002EF1F File Offset: 0x0002D11F
		public FabricIntegratorProvisioningServiceTimeOutException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0002EF36 File Offset: 0x0002D136
		public FabricIntegratorProvisioningServiceTimeOutException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0002EF54 File Offset: 0x0002D154
		protected FabricIntegratorProvisioningServiceTimeOutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("FabricIntegratorProvisioningServiceTimeOutException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.FlowName = (string)info.GetValue("FabricIntegratorProvisioningServiceTimeOutException_FlowName", typeof(string));
			}
			catch (SerializationException)
			{
				this.FlowName = null;
			}
			try
			{
				this.TimeOut = (string)info.GetValue("FabricIntegratorProvisioningServiceTimeOutException_TimeOut", typeof(string));
			}
			catch (SerializationException)
			{
				this.TimeOut = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("FabricIntegratorProvisioningServiceTimeOutException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0002F064 File Offset: 0x0002D264
		public FabricIntegratorProvisioningServiceTimeOutException(string flowName, string timeOut)
		{
			this.FlowName = flowName;
			this.TimeOut = timeOut;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0002F081 File Offset: 0x0002D281
		public FabricIntegratorProvisioningServiceTimeOutException(string flowName, string timeOut, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.FlowName = flowName;
			this.TimeOut = timeOut;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0002F0A6 File Offset: 0x0002D2A6
		public FabricIntegratorProvisioningServiceTimeOutException(string flowName, string timeOut, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.FlowName = flowName;
			this.TimeOut = timeOut;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002F0D4 File Offset: 0x0002D2D4
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002F10B File Offset: 0x0002D30B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0002F114 File Offset: 0x0002D314
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(FabricIntegratorProvisioningServiceTimeOutException))
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

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002F1E4 File Offset: 0x0002D3E4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("FabricIntegratorProvisioningServiceTimeOutException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("FabricIntegratorProvisioningServiceTimeOutException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.FlowName != null)
			{
				info.AddValue("FabricIntegratorProvisioningServiceTimeOutException_FlowName", this.FlowName, typeof(string));
			}
			if (this.TimeOut != null)
			{
				info.AddValue("FabricIntegratorProvisioningServiceTimeOutException_TimeOut", this.TimeOut, typeof(string));
			}
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0002F288 File Offset: 0x0002D488
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Flow {0} couldn't complete operation in {1}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.FlowName != null) ? this.FlowName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.FlowName != null) ? this.FlowName.MarkIfInternal() : string.Empty) : ((this.FlowName != null) ? this.FlowName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.TimeOut != null) ? this.TimeOut.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.TimeOut != null) ? this.TimeOut.MarkIfInternal() : string.Empty) : ((this.TimeOut != null) ? this.TimeOut.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0002F362 File Offset: 0x0002D562
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

		// Token: 0x06000C68 RID: 3176 RVA: 0x0002F380 File Offset: 0x0002D580
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "FlowName={0}", (this.FlowName != null) ? this.FlowName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "FlowName={0}", (this.FlowName != null) ? this.FlowName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "FlowName={0}", (this.FlowName != null) ? this.FlowName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "TimeOut={0}", (this.TimeOut != null) ? this.TimeOut.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "TimeOut={0}", (this.TimeOut != null) ? this.TimeOut.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "TimeOut={0}", (this.TimeOut != null) ? this.TimeOut.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0002F4EC File Offset: 0x0002D6EC
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002F4F5 File Offset: 0x0002D6F5
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0002F4FE File Offset: 0x0002D6FE
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0002F4EC File Offset: 0x0002D6EC
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0002F508 File Offset: 0x0002D708
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

		// Token: 0x04000326 RID: 806
		private string creationMessage;

		// Token: 0x04000327 RID: 807
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000328 RID: 808
		private string m_flowName;

		// Token: 0x04000329 RID: 809
		private string m_timeOut;
	}
}
