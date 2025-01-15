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
	// Token: 0x020000CD RID: 205
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class CompressedDataSizeForPacketExceeded : GatewayPipelineException
	{
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x00044FE5 File Offset: 0x000431E5
		// (set) Token: 0x06001063 RID: 4195 RVA: 0x00044FED File Offset: 0x000431ED
		public string Reason
		{
			get
			{
				return this.m_reason;
			}
			protected set
			{
				this.m_reason = value;
			}
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x00044FF6 File Offset: 0x000431F6
		public CompressedDataSizeForPacketExceeded()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0004500A File Offset: 0x0004320A
		public CompressedDataSizeForPacketExceeded(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00045020 File Offset: 0x00043220
		public CompressedDataSizeForPacketExceeded(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00045043 File Offset: 0x00043243
		public CompressedDataSizeForPacketExceeded(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00045068 File Offset: 0x00043268
		protected CompressedDataSizeForPacketExceeded(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CompressedDataSizeForPacketExceeded_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Reason = (string)info.GetValue("CompressedDataSizeForPacketExceeded_Reason", typeof(string));
			}
			catch (SerializationException)
			{
				this.Reason = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00045104 File Offset: 0x00043304
		public CompressedDataSizeForPacketExceeded(string reason, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.Reason = reason;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0004512E File Offset: 0x0004332E
		public CompressedDataSizeForPacketExceeded(string reason, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.Reason = reason;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0004515A File Offset: 0x0004335A
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_CompressedDataSizeForPacketExceededError";
			}
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00045171 File Offset: 0x00043371
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00045179 File Offset: 0x00043379
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0004517C File Offset: 0x0004337C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CompressedDataSizeForPacketExceeded))
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

		// Token: 0x0600106F RID: 4207 RVA: 0x00045230 File Offset: 0x00043430
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CompressedDataSizeForPacketExceeded_creationMessage", this.creationMessage, typeof(string));
			if (this.Reason != null)
			{
				info.AddValue("CompressedDataSizeForPacketExceeded_Reason", this.Reason, typeof(string));
			}
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x00045290 File Offset: 0x00043490
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "With compression algorithm, the compressed data size in a packet exceeds the max ServiceBus limit: {0}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Reason != null) ? this.Reason.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Reason != null) ? this.Reason.ToString() : string.Empty) : ((this.Reason != null) ? this.Reason.ToString() : string.Empty)));
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x00045305 File Offset: 0x00043505
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

		// Token: 0x06001072 RID: 4210 RVA: 0x00045322 File Offset: 0x00043522
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x00045340 File Offset: 0x00043540
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Reason={0}", (this.Reason != null) ? this.Reason.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Reason={0}", (this.Reason != null) ? this.Reason.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Reason={0}", (this.Reason != null) ? this.Reason.ToString() : string.Empty)));
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x000453FE File Offset: 0x000435FE
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x00045407 File Offset: 0x00043607
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x00045410 File Offset: 0x00043610
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0004541C File Offset: 0x0004361C
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

		// Token: 0x06001078 RID: 4216 RVA: 0x000455E0 File Offset: 0x000437E0
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x000455EC File Offset: 0x000437EC
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

		// Token: 0x0600107A RID: 4218 RVA: 0x00045670 File Offset: 0x00043870
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000344 RID: 836
		private string creationMessage;

		// Token: 0x04000345 RID: 837
		private string m_reason;
	}
}
