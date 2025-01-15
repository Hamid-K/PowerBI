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
	// Token: 0x020000AE RID: 174
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class OAuthTokenLoginFailedException : GatewayPipelineException
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x00039234 File Offset: 0x00037434
		// (set) Token: 0x06000D9C RID: 3484 RVA: 0x0003923C File Offset: 0x0003743C
		public string FailureMessage
		{
			get
			{
				return this.m_failureMessage;
			}
			protected set
			{
				this.m_failureMessage = value;
			}
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00039245 File Offset: 0x00037445
		public OAuthTokenLoginFailedException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00039259 File Offset: 0x00037459
		public OAuthTokenLoginFailedException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0003926F File Offset: 0x0003746F
		public OAuthTokenLoginFailedException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00039292 File Offset: 0x00037492
		public OAuthTokenLoginFailedException(string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x000392BC File Offset: 0x000374BC
		protected OAuthTokenLoginFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("OAuthTokenLoginFailedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.FailureMessage = (string)info.GetValue("OAuthTokenLoginFailedException_FailureMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.FailureMessage = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00039358 File Offset: 0x00037558
		public OAuthTokenLoginFailedException(string failureMessage, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.FailureMessage = failureMessage;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00039382 File Offset: 0x00037582
		public OAuthTokenLoginFailedException(string failureMessage, string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.FailureMessage = failureMessage;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x000393B3 File Offset: 0x000375B3
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Client_OAuthTokenLoginFailedError";
			}
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x000393CA File Offset: 0x000375CA
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x000393D4 File Offset: 0x000375D4
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(OAuthTokenLoginFailedException))
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

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00039488 File Offset: 0x00037688
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("OAuthTokenLoginFailedException_creationMessage", this.creationMessage, typeof(string));
			if (this.FailureMessage != null)
			{
				info.AddValue("OAuthTokenLoginFailedException_FailureMessage", this.FailureMessage, typeof(string));
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x000394E8 File Offset: 0x000376E8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to call FinishLogin with OAuth token or failed with StartLogin, exception: {0}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty) : ((this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty)));
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x0003955D File Offset: 0x0003775D
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

		// Token: 0x06000DAA RID: 3498 RVA: 0x0003957A File Offset: 0x0003777A
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00039598 File Offset: 0x00037798
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "FailureMessage={0}", (this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "FailureMessage={0}", (this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "FailureMessage={0}", (this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty)));
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00039656 File Offset: 0x00037856
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0003965F File Offset: 0x0003785F
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00039668 File Offset: 0x00037868
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00039674 File Offset: 0x00037874
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

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00039838 File Offset: 0x00037A38
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00039844 File Offset: 0x00037A44
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

		// Token: 0x06000DB2 RID: 3506 RVA: 0x000398C8 File Offset: 0x00037AC8
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

		// Token: 0x04000311 RID: 785
		private string creationMessage;

		// Token: 0x04000312 RID: 786
		private string m_failureMessage;
	}
}
