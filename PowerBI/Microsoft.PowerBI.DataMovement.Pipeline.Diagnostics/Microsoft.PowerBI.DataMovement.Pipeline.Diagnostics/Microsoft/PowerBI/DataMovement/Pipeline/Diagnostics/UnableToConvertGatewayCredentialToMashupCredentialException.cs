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
	// Token: 0x020000B3 RID: 179
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class UnableToConvertGatewayCredentialToMashupCredentialException : GatewayPipelineException
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x0003B2A4 File Offset: 0x000394A4
		// (set) Token: 0x06000E12 RID: 3602 RVA: 0x0003B2AC File Offset: 0x000394AC
		public string DSR
		{
			get
			{
				return this.m_dSR;
			}
			protected set
			{
				this.m_dSR = value;
			}
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0003B2B5 File Offset: 0x000394B5
		public UnableToConvertGatewayCredentialToMashupCredentialException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0003B2C9 File Offset: 0x000394C9
		public UnableToConvertGatewayCredentialToMashupCredentialException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0003B2DF File Offset: 0x000394DF
		public UnableToConvertGatewayCredentialToMashupCredentialException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0003B302 File Offset: 0x00039502
		public UnableToConvertGatewayCredentialToMashupCredentialException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0003B328 File Offset: 0x00039528
		protected UnableToConvertGatewayCredentialToMashupCredentialException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("UnableToConvertGatewayCredentialToMashupCredentialException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DSR = (string)info.GetValue("UnableToConvertGatewayCredentialToMashupCredentialException_DSR", typeof(string));
			}
			catch (SerializationException)
			{
				this.DSR = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0003B3C4 File Offset: 0x000395C4
		public UnableToConvertGatewayCredentialToMashupCredentialException(string dSR, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.DSR = dSR;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0003B3EE File Offset: 0x000395EE
		public UnableToConvertGatewayCredentialToMashupCredentialException(string dSR, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.DSR = dSR;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0003B41A File Offset: 0x0003961A
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_UnableToConvertGatewayCredentialToMashupCredentialError";
			}
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0003B431 File Offset: 0x00039631
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0003B43C File Offset: 0x0003963C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(UnableToConvertGatewayCredentialToMashupCredentialException))
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

		// Token: 0x06000E1D RID: 3613 RVA: 0x0003B4F0 File Offset: 0x000396F0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("UnableToConvertGatewayCredentialToMashupCredentialException_creationMessage", this.creationMessage, typeof(string));
			if (this.DSR != null)
			{
				info.AddValue("UnableToConvertGatewayCredentialToMashupCredentialException_DSR", this.DSR, typeof(string));
			}
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0003B550 File Offset: 0x00039750
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to convert gateway credential to mashup credential for this datasource reference '{0}'.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DSR != null) ? this.DSR.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DSR != null) ? this.DSR.ToString().MarkAsCustomerContent() : string.Empty) : ((this.DSR != null) ? this.DSR.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0003B5CF File Offset: 0x000397CF
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

		// Token: 0x06000E20 RID: 3616 RVA: 0x0003B5EC File Offset: 0x000397EC
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0003B60C File Offset: 0x0003980C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DSR={0}", (this.DSR != null) ? this.DSR.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DSR={0}", (this.DSR != null) ? this.DSR.ToString().MarkAsCustomerContent() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DSR={0}", (this.DSR != null) ? this.DSR.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0003B6D4 File Offset: 0x000398D4
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0003B6DD File Offset: 0x000398DD
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0003B6E6 File Offset: 0x000398E6
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0003B6F0 File Offset: 0x000398F0
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

		// Token: 0x06000E26 RID: 3622 RVA: 0x0003B8B4 File Offset: 0x00039AB4
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0003B8C0 File Offset: 0x00039AC0
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

		// Token: 0x06000E28 RID: 3624 RVA: 0x0003B944 File Offset: 0x00039B44
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x0400031A RID: 794
		private string creationMessage;

		// Token: 0x0400031B RID: 795
		private string m_dSR;
	}
}
