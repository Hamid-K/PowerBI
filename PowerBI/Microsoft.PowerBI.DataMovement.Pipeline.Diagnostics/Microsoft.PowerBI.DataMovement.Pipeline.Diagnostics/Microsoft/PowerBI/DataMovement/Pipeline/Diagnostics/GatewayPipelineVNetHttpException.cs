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
	// Token: 0x020000A9 RID: 169
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class GatewayPipelineVNetHttpException : GatewayPipelineException
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x000373F0 File Offset: 0x000355F0
		// (set) Token: 0x06000D2B RID: 3371 RVA: 0x000373F8 File Offset: 0x000355F8
		public string StatusCode
		{
			get
			{
				return this.m_statusCode;
			}
			protected set
			{
				this.m_statusCode = value;
			}
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00037401 File Offset: 0x00035601
		public GatewayPipelineVNetHttpException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00037415 File Offset: 0x00035615
		public GatewayPipelineVNetHttpException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0003742B File Offset: 0x0003562B
		public GatewayPipelineVNetHttpException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0003744E File Offset: 0x0003564E
		public GatewayPipelineVNetHttpException(string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00037478 File Offset: 0x00035678
		protected GatewayPipelineVNetHttpException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("GatewayPipelineVNetHttpException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.StatusCode = (string)info.GetValue("GatewayPipelineVNetHttpException_StatusCode", typeof(string));
			}
			catch (SerializationException)
			{
				this.StatusCode = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00037514 File Offset: 0x00035714
		public GatewayPipelineVNetHttpException(string statusCode, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.StatusCode = statusCode;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0003753E File Offset: 0x0003573E
		public GatewayPipelineVNetHttpException(string statusCode, string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.StatusCode = statusCode;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0003756F File Offset: 0x0003576F
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_VNetHttpRequestFailedError";
			}
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00037586 File Offset: 0x00035786
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00037590 File Offset: 0x00035790
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(GatewayPipelineVNetHttpException))
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

		// Token: 0x06000D36 RID: 3382 RVA: 0x00037644 File Offset: 0x00035844
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("GatewayPipelineVNetHttpException_creationMessage", this.creationMessage, typeof(string));
			if (this.StatusCode != null)
			{
				info.AddValue("GatewayPipelineVNetHttpException_StatusCode", this.StatusCode, typeof(string));
			}
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x000376A4 File Offset: 0x000358A4
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to use virtual network (VNet) data gateway. Please try again. Received http status code of {0}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.StatusCode != null) ? this.StatusCode.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.StatusCode != null) ? this.StatusCode.ToString() : string.Empty) : ((this.StatusCode != null) ? this.StatusCode.ToString() : string.Empty)));
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00037719 File Offset: 0x00035919
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

		// Token: 0x06000D39 RID: 3385 RVA: 0x00037736 File Offset: 0x00035936
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00037754 File Offset: 0x00035954
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "StatusCode={0}", (this.StatusCode != null) ? this.StatusCode.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "StatusCode={0}", (this.StatusCode != null) ? this.StatusCode.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "StatusCode={0}", (this.StatusCode != null) ? this.StatusCode.ToString() : string.Empty)));
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00037812 File Offset: 0x00035A12
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0003781B File Offset: 0x00035A1B
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00037824 File Offset: 0x00035A24
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00037830 File Offset: 0x00035A30
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

		// Token: 0x06000D3F RID: 3391 RVA: 0x000379F4 File Offset: 0x00035BF4
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00037A00 File Offset: 0x00035C00
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

		// Token: 0x06000D41 RID: 3393 RVA: 0x00037A84 File Offset: 0x00035C84
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

		// Token: 0x04000309 RID: 777
		private string creationMessage;

		// Token: 0x0400030A RID: 778
		private string m_statusCode;
	}
}
