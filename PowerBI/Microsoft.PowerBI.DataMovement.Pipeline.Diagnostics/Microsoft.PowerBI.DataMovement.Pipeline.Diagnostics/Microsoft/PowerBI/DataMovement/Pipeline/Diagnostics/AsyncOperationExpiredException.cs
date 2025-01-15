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
	// Token: 0x02000090 RID: 144
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class AsyncOperationExpiredException : GatewayPipelineException
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x0002EB1D File Offset: 0x0002CD1D
		// (set) Token: 0x06000B01 RID: 2817 RVA: 0x0002EB25 File Offset: 0x0002CD25
		public Guid AsyncOperationId
		{
			get
			{
				return this.m_asyncOperationId;
			}
			protected set
			{
				this.m_asyncOperationId = value;
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0002EB2E File Offset: 0x0002CD2E
		public AsyncOperationExpiredException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidValueField<Guid>();
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0002EB42 File Offset: 0x0002CD42
		public AsyncOperationExpiredException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0002EB58 File Offset: 0x0002CD58
		public AsyncOperationExpiredException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0002EB7B File Offset: 0x0002CD7B
		public AsyncOperationExpiredException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0002EBA0 File Offset: 0x0002CDA0
		protected AsyncOperationExpiredException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("AsyncOperationExpiredException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.AsyncOperationId = (Guid)info.GetValue("AsyncOperationExpiredException_AsyncOperationId", typeof(Guid));
			this.ConstructorInternal(true);
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0002EC24 File Offset: 0x0002CE24
		public AsyncOperationExpiredException(Guid asyncOperationId, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.AsyncOperationId = asyncOperationId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0002EC41 File Offset: 0x0002CE41
		public AsyncOperationExpiredException(Guid asyncOperationId, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.AsyncOperationId = asyncOperationId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0002EC6B File Offset: 0x0002CE6B
		public AsyncOperationExpiredException(Guid asyncOperationId, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.AsyncOperationId = asyncOperationId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0002EC97 File Offset: 0x0002CE97
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_AsyncOperationExpired";
			}
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0002ECAE File Offset: 0x0002CEAE
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0002ECB8 File Offset: 0x0002CEB8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(AsyncOperationExpiredException))
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

		// Token: 0x06000B0D RID: 2829 RVA: 0x0002ED6C File Offset: 0x0002CF6C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("AsyncOperationExpiredException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("AsyncOperationExpiredException_AsyncOperationId", this.AsyncOperationId, typeof(Guid));
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0002EDC8 File Offset: 0x0002CFC8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The async operation id {0} was already expired.", (markupKind == PrivateInformationMarkupKind.None) ? this.AsyncOperationId.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.AsyncOperationId.ToString() : this.AsyncOperationId.ToString()));
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0002EE2B File Offset: 0x0002D02B
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

		// Token: 0x06000B10 RID: 2832 RVA: 0x0002EE48 File Offset: 0x0002D048
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0002EE68 File Offset: 0x0002D068
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "AsyncOperationId={0}", this.AsyncOperationId.ToString()) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "AsyncOperationId={0}", this.AsyncOperationId.ToString()) : string.Format(CultureInfo.CurrentCulture, "AsyncOperationId={0}", this.AsyncOperationId.ToString())));
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0002EF14 File Offset: 0x0002D114
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0002EF1D File Offset: 0x0002D11D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0002EF26 File Offset: 0x0002D126
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0002EF30 File Offset: 0x0002D130
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

		// Token: 0x06000B16 RID: 2838 RVA: 0x0002F0F4 File Offset: 0x0002D2F4
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0002F100 File Offset: 0x0002D300
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

		// Token: 0x06000B18 RID: 2840 RVA: 0x0002F184 File Offset: 0x0002D384
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x040002E7 RID: 743
		private string creationMessage;

		// Token: 0x040002E8 RID: 744
		private Guid m_asyncOperationId;
	}
}
