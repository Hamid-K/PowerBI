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
	// Token: 0x020000B2 RID: 178
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class GetOAuthResourceException : GatewayPipelineException
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0003ABC0 File Offset: 0x00038DC0
		// (set) Token: 0x06000DF9 RID: 3577 RVA: 0x0003ABC8 File Offset: 0x00038DC8
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

		// Token: 0x06000DFA RID: 3578 RVA: 0x0003ABD1 File Offset: 0x00038DD1
		public GetOAuthResourceException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0003ABE5 File Offset: 0x00038DE5
		public GetOAuthResourceException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0003ABFB File Offset: 0x00038DFB
		public GetOAuthResourceException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0003AC1E File Offset: 0x00038E1E
		public GetOAuthResourceException(string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0003AC48 File Offset: 0x00038E48
		protected GetOAuthResourceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("GetOAuthResourceException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.FailureMessage = (string)info.GetValue("GetOAuthResourceException_FailureMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.FailureMessage = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0003ACE4 File Offset: 0x00038EE4
		public GetOAuthResourceException(string failureMessage, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.FailureMessage = failureMessage;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0003AD0E File Offset: 0x00038F0E
		public GetOAuthResourceException(string failureMessage, string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.FailureMessage = failureMessage;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0003AD3F File Offset: 0x00038F3F
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Client_GetOAuthResourceError";
			}
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0003AD56 File Offset: 0x00038F56
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0003AD5E File Offset: 0x00038F5E
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0003AD64 File Offset: 0x00038F64
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(GetOAuthResourceException))
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

		// Token: 0x06000E05 RID: 3589 RVA: 0x0003AE18 File Offset: 0x00039018
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("GetOAuthResourceException_creationMessage", this.creationMessage, typeof(string));
			if (this.FailureMessage != null)
			{
				info.AddValue("GetOAuthResourceException_FailureMessage", this.FailureMessage, typeof(string));
			}
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0003AE78 File Offset: 0x00039078
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to get OAuth resource information, exception: {0}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty) : ((this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty)));
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x0003AEED File Offset: 0x000390ED
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

		// Token: 0x06000E08 RID: 3592 RVA: 0x0003AF0A File Offset: 0x0003910A
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0003AF28 File Offset: 0x00039128
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "FailureMessage={0}", (this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "FailureMessage={0}", (this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "FailureMessage={0}", (this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty)));
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0003AFE6 File Offset: 0x000391E6
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0003AFEF File Offset: 0x000391EF
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0003AFF8 File Offset: 0x000391F8
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0003B004 File Offset: 0x00039204
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

		// Token: 0x06000E0E RID: 3598 RVA: 0x0003B1C8 File Offset: 0x000393C8
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0003B1D4 File Offset: 0x000393D4
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

		// Token: 0x06000E10 RID: 3600 RVA: 0x0003B258 File Offset: 0x00039458
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

		// Token: 0x04000318 RID: 792
		private string creationMessage;

		// Token: 0x04000319 RID: 793
		private string m_failureMessage;
	}
}
