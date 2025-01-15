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
	// Token: 0x02000010 RID: 16
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class GatewayClientErrorResponseException : GatewayPipelineException
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002DFC File Offset: 0x00000FFC
		public override bool IsBenign()
		{
			GatewayPipelineException ex = base.InnerException as GatewayPipelineException;
			if (ex == null)
			{
				return base.IsBenign();
			}
			return ex.IsBenign();
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002E25 File Offset: 0x00001025
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002E2D File Offset: 0x0000102D
		public string GatewayExceptionMessage
		{
			get
			{
				return this.m_gatewayExceptionMessage;
			}
			protected set
			{
				this.m_gatewayExceptionMessage = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002E36 File Offset: 0x00001036
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002E3E File Offset: 0x0000103E
		public string GatewayId
		{
			get
			{
				return this.m_gatewayId;
			}
			protected set
			{
				this.m_gatewayId = value;
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002E47 File Offset: 0x00001047
		public GatewayClientErrorResponseException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002E60 File Offset: 0x00001060
		public GatewayClientErrorResponseException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002E76 File Offset: 0x00001076
		public GatewayClientErrorResponseException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002E99 File Offset: 0x00001099
		public GatewayClientErrorResponseException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002EC0 File Offset: 0x000010C0
		protected GatewayClientErrorResponseException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("GatewayClientErrorResponseException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.GatewayExceptionMessage = (string)info.GetValue("GatewayClientErrorResponseException_GatewayExceptionMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.GatewayExceptionMessage = null;
			}
			try
			{
				this.GatewayId = (string)info.GetValue("GatewayClientErrorResponseException_GatewayId", typeof(string));
			}
			catch (SerializationException)
			{
				this.GatewayId = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002F98 File Offset: 0x00001198
		public GatewayClientErrorResponseException(string gatewayExceptionMessage, string gatewayId, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.GatewayExceptionMessage = gatewayExceptionMessage;
			this.GatewayId = gatewayId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002FBC File Offset: 0x000011BC
		public GatewayClientErrorResponseException(string gatewayExceptionMessage, string gatewayId, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.GatewayExceptionMessage = gatewayExceptionMessage;
			this.GatewayId = gatewayId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002FEE File Offset: 0x000011EE
		public GatewayClientErrorResponseException(string gatewayExceptionMessage, string gatewayId, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.GatewayExceptionMessage = gatewayExceptionMessage;
			this.GatewayId = gatewayId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003022 File Offset: 0x00001222
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000302B File Offset: 0x0000122B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003034 File Offset: 0x00001234
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(GatewayClientErrorResponseException))
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

		// Token: 0x06000068 RID: 104 RVA: 0x000030E8 File Offset: 0x000012E8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("GatewayClientErrorResponseException_creationMessage", this.creationMessage, typeof(string));
			if (this.GatewayExceptionMessage != null)
			{
				info.AddValue("GatewayClientErrorResponseException_GatewayExceptionMessage", this.GatewayExceptionMessage, typeof(string));
			}
			if (this.GatewayId != null)
			{
				info.AddValue("GatewayClientErrorResponseException_GatewayId", this.GatewayId, typeof(string));
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000316C File Offset: 0x0000136C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Received error payload from gateway service with ID {0}: {1}.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.GatewayId != null) ? this.GatewayId.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.GatewayId != null) ? this.GatewayId.ToString() : string.Empty) : ((this.GatewayId != null) ? this.GatewayId.ToString() : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.GatewayExceptionMessage != null) ? this.GatewayExceptionMessage.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.GatewayExceptionMessage != null) ? this.GatewayExceptionMessage.ToString() : string.Empty) : ((this.GatewayExceptionMessage != null) ? this.GatewayExceptionMessage.ToString() : string.Empty)));
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000323A File Offset: 0x0000143A
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

		// Token: 0x0600006B RID: 107 RVA: 0x00003257 File Offset: 0x00001457
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003274 File Offset: 0x00001474
		protected override void PopulateExceptionErrorShortNameComponents(List<string> components)
		{
			base.PopulateExceptionErrorShortNameComponents(components);
			components.Add("GatewayId");
			string text = ((this.GatewayId == null) ? string.Empty : this.GatewayId.ToString());
			components.Add(text);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000032B8 File Offset: 0x000014B8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "GatewayExceptionMessage={0}", (this.GatewayExceptionMessage != null) ? this.GatewayExceptionMessage.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "GatewayExceptionMessage={0}", (this.GatewayExceptionMessage != null) ? this.GatewayExceptionMessage.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "GatewayExceptionMessage={0}", (this.GatewayExceptionMessage != null) ? this.GatewayExceptionMessage.ToString() : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "GatewayId={0}", (this.GatewayId != null) ? this.GatewayId.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "GatewayId={0}", (this.GatewayId != null) ? this.GatewayId.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "GatewayId={0}", (this.GatewayId != null) ? this.GatewayId.ToString() : string.Empty)));
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003418 File Offset: 0x00001618
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003421 File Offset: 0x00001621
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000342A File Offset: 0x0000162A
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003434 File Offset: 0x00001634
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

		// Token: 0x06000072 RID: 114 RVA: 0x000035F8 File Offset: 0x000017F8
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003604 File Offset: 0x00001804
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

		// Token: 0x06000074 RID: 116 RVA: 0x00003688 File Offset: 0x00001888
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			GatewayPipelineException ex = base.InnerException as GatewayPipelineException;
			if (ex != null)
			{
				list.AddRange(ex.GetErrorDetails());
			}
			return list;
		}

		// Token: 0x0400003A RID: 58
		private string creationMessage;

		// Token: 0x0400003B RID: 59
		private string m_gatewayExceptionMessage;

		// Token: 0x0400003C RID: 60
		private string m_gatewayId;
	}
}
