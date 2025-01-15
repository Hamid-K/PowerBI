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
	// Token: 0x0200003A RID: 58
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class GatewayClientLoadBalancerNoCandidateAvailableException : GatewayPipelineException
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000FE3D File Offset: 0x0000E03D
		// (set) Token: 0x0600036D RID: 877 RVA: 0x0000FE45 File Offset: 0x0000E045
		public long AnchorGatewayId
		{
			get
			{
				return this.m_anchorGatewayId;
			}
			protected set
			{
				this.m_anchorGatewayId = value;
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000FE4E File Offset: 0x0000E04E
		public GatewayClientLoadBalancerNoCandidateAvailableException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidValueField<long>();
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000FE62 File Offset: 0x0000E062
		public GatewayClientLoadBalancerNoCandidateAvailableException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000FE78 File Offset: 0x0000E078
		public GatewayClientLoadBalancerNoCandidateAvailableException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000FE9B File Offset: 0x0000E09B
		public GatewayClientLoadBalancerNoCandidateAvailableException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000FEC0 File Offset: 0x0000E0C0
		protected GatewayClientLoadBalancerNoCandidateAvailableException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("GatewayClientLoadBalancerNoCandidateAvailableException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.AnchorGatewayId = (long)info.GetValue("GatewayClientLoadBalancerNoCandidateAvailableException_AnchorGatewayId", typeof(long));
			this.ConstructorInternal(true);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000FF44 File Offset: 0x0000E144
		public GatewayClientLoadBalancerNoCandidateAvailableException(long anchorGatewayId, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.AnchorGatewayId = anchorGatewayId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000FF61 File Offset: 0x0000E161
		public GatewayClientLoadBalancerNoCandidateAvailableException(long anchorGatewayId, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.AnchorGatewayId = anchorGatewayId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000FF8B File Offset: 0x0000E18B
		public GatewayClientLoadBalancerNoCandidateAvailableException(long anchorGatewayId, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.AnchorGatewayId = anchorGatewayId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000FFB7 File Offset: 0x0000E1B7
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Client_LoadBalancer_NoCandidateAvailable";
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000FFCE File Offset: 0x0000E1CE
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000FFD6 File Offset: 0x0000E1D6
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000FFDC File Offset: 0x0000E1DC
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(GatewayClientLoadBalancerNoCandidateAvailableException))
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

		// Token: 0x0600037A RID: 890 RVA: 0x00010090 File Offset: 0x0000E290
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("GatewayClientLoadBalancerNoCandidateAvailableException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("GatewayClientLoadBalancerNoCandidateAvailableException_AnchorGatewayId", this.AnchorGatewayId, typeof(long));
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000100EC File Offset: 0x0000E2EC
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Load balancer found no candidate available for anchor gateway '{0}'.", (markupKind == PrivateInformationMarkupKind.None) ? this.AnchorGatewayId.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.AnchorGatewayId.ToString(CultureInfo.InvariantCulture) : this.AnchorGatewayId.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0001014C File Offset: 0x0000E34C
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

		// Token: 0x0600037D RID: 893 RVA: 0x00010169 File Offset: 0x0000E369
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00010188 File Offset: 0x0000E388
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "AnchorGatewayId={0}", this.AnchorGatewayId.ToString(CultureInfo.InvariantCulture)) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "AnchorGatewayId={0}", this.AnchorGatewayId.ToString(CultureInfo.InvariantCulture)) : string.Format(CultureInfo.CurrentCulture, "AnchorGatewayId={0}", this.AnchorGatewayId.ToString(CultureInfo.InvariantCulture))));
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00010231 File Offset: 0x0000E431
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0001023A File Offset: 0x0000E43A
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00010243 File Offset: 0x0000E443
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001024C File Offset: 0x0000E44C
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

		// Token: 0x06000383 RID: 899 RVA: 0x00010410 File Offset: 0x0000E610
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001041C File Offset: 0x0000E61C
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

		// Token: 0x06000385 RID: 901 RVA: 0x000104A0 File Offset: 0x0000E6A0
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000267 RID: 615
		private string creationMessage;

		// Token: 0x04000268 RID: 616
		private long m_anchorGatewayId;
	}
}
