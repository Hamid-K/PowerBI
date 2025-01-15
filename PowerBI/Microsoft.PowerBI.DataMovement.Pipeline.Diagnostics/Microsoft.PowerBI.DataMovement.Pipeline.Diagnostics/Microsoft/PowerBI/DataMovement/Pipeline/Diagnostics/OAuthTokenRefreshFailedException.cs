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
	// Token: 0x020000AD RID: 173
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class OAuthTokenRefreshFailedException : GatewayPipelineException
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x00038B51 File Offset: 0x00036D51
		// (set) Token: 0x06000D83 RID: 3459 RVA: 0x00038B59 File Offset: 0x00036D59
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

		// Token: 0x06000D84 RID: 3460 RVA: 0x00038B62 File Offset: 0x00036D62
		public OAuthTokenRefreshFailedException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00038B76 File Offset: 0x00036D76
		public OAuthTokenRefreshFailedException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00038B8C File Offset: 0x00036D8C
		public OAuthTokenRefreshFailedException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00038BAF File Offset: 0x00036DAF
		public OAuthTokenRefreshFailedException(string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00038BD8 File Offset: 0x00036DD8
		protected OAuthTokenRefreshFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("OAuthTokenRefreshFailedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.FailureMessage = (string)info.GetValue("OAuthTokenRefreshFailedException_FailureMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.FailureMessage = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00038C74 File Offset: 0x00036E74
		public OAuthTokenRefreshFailedException(string failureMessage, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.FailureMessage = failureMessage;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00038C9E File Offset: 0x00036E9E
		public OAuthTokenRefreshFailedException(string failureMessage, string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.FailureMessage = failureMessage;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00038CCF File Offset: 0x00036ECF
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Client_OAuthTokenRefreshFailedError";
			}
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00038CE6 File Offset: 0x00036EE6
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00038CEE File Offset: 0x00036EEE
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00038CF4 File Offset: 0x00036EF4
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(OAuthTokenRefreshFailedException))
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

		// Token: 0x06000D8F RID: 3471 RVA: 0x00038DA8 File Offset: 0x00036FA8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("OAuthTokenRefreshFailedException_creationMessage", this.creationMessage, typeof(string));
			if (this.FailureMessage != null)
			{
				info.AddValue("OAuthTokenRefreshFailedException_FailureMessage", this.FailureMessage, typeof(string));
			}
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00038E08 File Offset: 0x00037008
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to refresh OAuth token, exception: {0}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty) : ((this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty)));
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x00038E7D File Offset: 0x0003707D
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

		// Token: 0x06000D92 RID: 3474 RVA: 0x00038E9A File Offset: 0x0003709A
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00038EB8 File Offset: 0x000370B8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "FailureMessage={0}", (this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "FailureMessage={0}", (this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "FailureMessage={0}", (this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty)));
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00038F76 File Offset: 0x00037176
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00038F7F File Offset: 0x0003717F
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00038F88 File Offset: 0x00037188
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00038F94 File Offset: 0x00037194
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

		// Token: 0x06000D98 RID: 3480 RVA: 0x00039158 File Offset: 0x00037358
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00039164 File Offset: 0x00037364
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

		// Token: 0x06000D9A RID: 3482 RVA: 0x000391E8 File Offset: 0x000373E8
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

		// Token: 0x0400030F RID: 783
		private string creationMessage;

		// Token: 0x04000310 RID: 784
		private string m_failureMessage;
	}
}
