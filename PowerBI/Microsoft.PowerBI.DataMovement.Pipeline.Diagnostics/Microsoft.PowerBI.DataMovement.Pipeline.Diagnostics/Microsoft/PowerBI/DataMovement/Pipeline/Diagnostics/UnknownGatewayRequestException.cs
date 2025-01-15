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
	// Token: 0x02000046 RID: 70
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class UnknownGatewayRequestException : GatewayPipelineException
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00014044 File Offset: 0x00012244
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x0001404C File Offset: 0x0001224C
		public string GatewayRequestTypeName
		{
			get
			{
				return this.m_gatewayRequestTypeName;
			}
			protected set
			{
				this.m_gatewayRequestTypeName = value;
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00014055 File Offset: 0x00012255
		public UnknownGatewayRequestException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00014069 File Offset: 0x00012269
		public UnknownGatewayRequestException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0001407F File Offset: 0x0001227F
		public UnknownGatewayRequestException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000140A2 File Offset: 0x000122A2
		public UnknownGatewayRequestException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000140C8 File Offset: 0x000122C8
		protected UnknownGatewayRequestException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("UnknownGatewayRequestException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.GatewayRequestTypeName = (string)info.GetValue("UnknownGatewayRequestException_GatewayRequestTypeName", typeof(string));
			}
			catch (SerializationException)
			{
				this.GatewayRequestTypeName = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00014164 File Offset: 0x00012364
		public UnknownGatewayRequestException(string gatewayRequestTypeName, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.GatewayRequestTypeName = gatewayRequestTypeName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001418E File Offset: 0x0001238E
		public UnknownGatewayRequestException(string gatewayRequestTypeName, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.GatewayRequestTypeName = gatewayRequestTypeName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000141BA File Offset: 0x000123BA
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_UnknownGatewayRequestError";
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000141D1 File Offset: 0x000123D1
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x000141DC File Offset: 0x000123DC
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(UnknownGatewayRequestException))
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

		// Token: 0x06000488 RID: 1160 RVA: 0x00014290 File Offset: 0x00012490
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("UnknownGatewayRequestException_creationMessage", this.creationMessage, typeof(string));
			if (this.GatewayRequestTypeName != null)
			{
				info.AddValue("UnknownGatewayRequestException_GatewayRequestTypeName", this.GatewayRequestTypeName, typeof(string));
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000142F0 File Offset: 0x000124F0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} is not recognized.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.GatewayRequestTypeName != null) ? this.GatewayRequestTypeName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.GatewayRequestTypeName != null) ? this.GatewayRequestTypeName.ToString() : string.Empty) : ((this.GatewayRequestTypeName != null) ? this.GatewayRequestTypeName.ToString() : string.Empty)));
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00014365 File Offset: 0x00012565
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

		// Token: 0x0600048B RID: 1163 RVA: 0x00014382 File Offset: 0x00012582
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x000143A0 File Offset: 0x000125A0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "GatewayRequestTypeName={0}", (this.GatewayRequestTypeName != null) ? this.GatewayRequestTypeName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "GatewayRequestTypeName={0}", (this.GatewayRequestTypeName != null) ? this.GatewayRequestTypeName.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "GatewayRequestTypeName={0}", (this.GatewayRequestTypeName != null) ? this.GatewayRequestTypeName.ToString() : string.Empty)));
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001445E File Offset: 0x0001265E
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00014467 File Offset: 0x00012667
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00014470 File Offset: 0x00012670
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001447C File Offset: 0x0001267C
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

		// Token: 0x06000491 RID: 1169 RVA: 0x00014640 File Offset: 0x00012840
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001464C File Offset: 0x0001284C
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

		// Token: 0x06000493 RID: 1171 RVA: 0x000146D0 File Offset: 0x000128D0
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000277 RID: 631
		private string creationMessage;

		// Token: 0x04000278 RID: 632
		private string m_gatewayRequestTypeName;
	}
}
