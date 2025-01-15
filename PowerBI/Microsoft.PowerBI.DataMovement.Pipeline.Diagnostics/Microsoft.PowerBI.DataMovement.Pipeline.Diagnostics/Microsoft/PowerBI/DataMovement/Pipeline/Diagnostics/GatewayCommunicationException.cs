using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Privacy;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics
{
	// Token: 0x020000C2 RID: 194
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class GatewayCommunicationException : GatewayPipelineException
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x00040BFD File Offset: 0x0003EDFD
		// (set) Token: 0x06000F62 RID: 3938 RVA: 0x00040C05 File Offset: 0x0003EE05
		public string ServiceBusEndpoint
		{
			get
			{
				return this.m_serviceBusEndpoint;
			}
			protected set
			{
				this.m_serviceBusEndpoint = value;
			}
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00040C0E File Offset: 0x0003EE0E
		public GatewayCommunicationException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x00040C22 File Offset: 0x0003EE22
		public GatewayCommunicationException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x00040C38 File Offset: 0x0003EE38
		public GatewayCommunicationException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x00040C5B File Offset: 0x0003EE5B
		public GatewayCommunicationException(string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x00040C84 File Offset: 0x0003EE84
		protected GatewayCommunicationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("GatewayCommunicationException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ServiceBusEndpoint = (string)info.GetValue("GatewayCommunicationException_ServiceBusEndpoint", typeof(string));
			}
			catch (SerializationException)
			{
				this.ServiceBusEndpoint = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x00040D20 File Offset: 0x0003EF20
		public GatewayCommunicationException(string serviceBusEndpoint, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ServiceBusEndpoint = serviceBusEndpoint;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00040D4A File Offset: 0x0003EF4A
		public GatewayCommunicationException(string serviceBusEndpoint, string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ServiceBusEndpoint = serviceBusEndpoint;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x00040D7B File Offset: 0x0003EF7B
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Client_GatewayUnreachable";
			}
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00040D92 File Offset: 0x0003EF92
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00040D9A File Offset: 0x0003EF9A
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x00040DA0 File Offset: 0x0003EFA0
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(GatewayCommunicationException))
			{
				TraceSourceBase<DiagnosticsTraceSource>.Tracer.TraceError("Exception object created [IsBenign={0}]: {1}: {2}; ErrorShortName: {3}", new object[]
				{
					this.IsBenign(),
					type,
					this.GetMarkedUpMessage(),
					this.ErrorShortName
				});
				bool flag = base.InnerException != null && base.InnerException is GatewayPipelineException;
				if (TraceSourceBase<DiagnosticsTraceSource>.Tracer.ShouldTrace(PipelineTraceVerbosity.Error) && (base.InnerException == null || !flag))
				{
					TraceSourceBase<DiagnosticsTraceSource>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x00040E54 File Offset: 0x0003F054
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("GatewayCommunicationException_creationMessage", this.creationMessage, typeof(string));
			if (this.ServiceBusEndpoint != null)
			{
				info.AddValue("GatewayCommunicationException_ServiceBusEndpoint", this.ServiceBusEndpoint, typeof(string));
			}
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00040EB4 File Offset: 0x0003F0B4
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Cannot communicate with Gateway on endpoint '{0}'.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ServiceBusEndpoint != null) ? this.ServiceBusEndpoint.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ServiceBusEndpoint != null) ? this.ServiceBusEndpoint.ToString() : string.Empty) : ((this.ServiceBusEndpoint != null) ? this.ServiceBusEndpoint.ToString() : string.Empty)));
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x00040F29 File Offset: 0x0003F129
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

		// Token: 0x06000F71 RID: 3953 RVA: 0x00040F46 File Offset: 0x0003F146
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x00040F64 File Offset: 0x0003F164
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ServiceBusEndpoint={0}", (this.ServiceBusEndpoint != null) ? this.ServiceBusEndpoint.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ServiceBusEndpoint={0}", (this.ServiceBusEndpoint != null) ? this.ServiceBusEndpoint.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ServiceBusEndpoint={0}", (this.ServiceBusEndpoint != null) ? this.ServiceBusEndpoint.ToString() : string.Empty)));
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x00041022 File Offset: 0x0003F222
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0004102B File Offset: 0x0003F22B
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x00041034 File Offset: 0x0003F234
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00041040 File Offset: 0x0003F240
		private string ToString(PrivateInformationMarkupKind markupKind)
		{
			string text = "[" + GatewayExceptionUtils.ExceptionsTemplateHelper.MagicLevel.ToString(CultureInfo.CurrentCulture) + "]" + base.GetType().FullName;
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
			if (base.InnerException != null)
			{
				try
				{
					GatewayExceptionUtils.ExceptionsTemplateHelper.IncrementMagicLevel();
					IContainsPrivateInformation containsPrivateInformation = base.InnerException as IContainsPrivateInformation;
					string text4;
					if (markupKind == PrivateInformationMarkupKind.None)
					{
						text4 = ((containsPrivateInformation == null) ? base.InnerException.ToString() : containsPrivateInformation.ToOriginalString());
					}
					else
					{
						text4 = ((containsPrivateInformation == null) ? (GatewayExceptionUtils.InnerExceptionStringCreator.CreateInnerExceptionStack(base.InnerException) + base.InnerException.ToString().MarkAsCustomerContent()) : containsPrivateInformation.ToPrivateString());
					}
					text3 = string.Concat(new string[]
					{
						text3,
						" --->",
						Environment.NewLine,
						text4,
						Environment.NewLine,
						"   --- End of inner exception stack trace ---"
					});
				}
				finally
				{
					GatewayExceptionUtils.ExceptionsTemplateHelper.DecrementMagicLevel();
				}
			}
			if (this.StackTrace != null)
			{
				text3 = string.Concat(new string[]
				{
					text3,
					Environment.NewLine,
					"  (",
					text,
					".StackTrace:)"
				});
				text3 = text3 + Environment.NewLine + this.StackTrace;
			}
			return text3;
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x00041204 File Offset: 0x0003F404
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x00041210 File Offset: 0x0003F410
		internal override IDictionary<string, string> GetClientErrorParameters(bool includeInner)
		{
			IDictionary<string, string> clientErrorParameters = base.GetClientErrorParameters(true);
			if (includeInner)
			{
				GatewayPipelineException ex = base.InnerException as GatewayPipelineException;
				if (ex != null)
				{
					IDictionary<string, string> clientErrorParameters2 = ex.GetClientErrorParameters();
					foreach (string text in clientErrorParameters2.Keys)
					{
						if (!clientErrorParameters.ContainsKey(text))
						{
							clientErrorParameters[text] = clientErrorParameters2[text];
						}
					}
				}
			}
			return clientErrorParameters;
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x00041294 File Offset: 0x0003F494
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000332 RID: 818
		private string creationMessage;

		// Token: 0x04000333 RID: 819
		private string m_serviceBusEndpoint;
	}
}
