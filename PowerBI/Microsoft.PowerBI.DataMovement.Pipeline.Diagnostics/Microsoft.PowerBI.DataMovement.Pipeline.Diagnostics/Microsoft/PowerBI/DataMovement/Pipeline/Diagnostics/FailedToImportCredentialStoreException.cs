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
	// Token: 0x0200004B RID: 75
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class FailedToImportCredentialStoreException : GatewayPipelineException
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00015C51 File Offset: 0x00013E51
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x00015C59 File Offset: 0x00013E59
		public string CredentialFilePath
		{
			get
			{
				return this.m_credentialFilePath;
			}
			protected set
			{
				this.m_credentialFilePath = value;
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00015C62 File Offset: 0x00013E62
		public FailedToImportCredentialStoreException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00015C76 File Offset: 0x00013E76
		public FailedToImportCredentialStoreException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00015C8C File Offset: 0x00013E8C
		public FailedToImportCredentialStoreException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00015CAF File Offset: 0x00013EAF
		public FailedToImportCredentialStoreException(string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00015CD8 File Offset: 0x00013ED8
		protected FailedToImportCredentialStoreException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("FailedToImportCredentialStoreException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.CredentialFilePath = (string)info.GetValue("FailedToImportCredentialStoreException_CredentialFilePath", typeof(string));
			}
			catch (SerializationException)
			{
				this.CredentialFilePath = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00015D74 File Offset: 0x00013F74
		public FailedToImportCredentialStoreException(string credentialFilePath, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.CredentialFilePath = credentialFilePath;
			this.ConstructorInternal(false);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00015D9E File Offset: 0x00013F9E
		public FailedToImportCredentialStoreException(string credentialFilePath, string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.CredentialFilePath = credentialFilePath;
			this.ConstructorInternal(false);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00015DCF File Offset: 0x00013FCF
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_FailedToImportCredentialStoreError";
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00015DE6 File Offset: 0x00013FE6
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00015DF0 File Offset: 0x00013FF0
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(FailedToImportCredentialStoreException))
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

		// Token: 0x060004F4 RID: 1268 RVA: 0x00015EA4 File Offset: 0x000140A4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("FailedToImportCredentialStoreException_creationMessage", this.creationMessage, typeof(string));
			if (this.CredentialFilePath != null)
			{
				info.AddValue("FailedToImportCredentialStoreException_CredentialFilePath", this.CredentialFilePath, typeof(string));
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00015F04 File Offset: 0x00014104
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Could not load credential store from {0}.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.CredentialFilePath != null) ? this.CredentialFilePath.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.CredentialFilePath != null) ? this.CredentialFilePath.ToString().MarkAsCustomerContent() : string.Empty) : ((this.CredentialFilePath != null) ? this.CredentialFilePath.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00015F83 File Offset: 0x00014183
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

		// Token: 0x060004F7 RID: 1271 RVA: 0x00015FA0 File Offset: 0x000141A0
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00015FC0 File Offset: 0x000141C0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "CredentialFilePath={0}", (this.CredentialFilePath != null) ? this.CredentialFilePath.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "CredentialFilePath={0}", (this.CredentialFilePath != null) ? this.CredentialFilePath.ToString().MarkAsCustomerContent() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "CredentialFilePath={0}", (this.CredentialFilePath != null) ? this.CredentialFilePath.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00016088 File Offset: 0x00014288
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00016091 File Offset: 0x00014291
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001609A File Offset: 0x0001429A
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000160A4 File Offset: 0x000142A4
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

		// Token: 0x060004FD RID: 1277 RVA: 0x00016268 File Offset: 0x00014468
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00016274 File Offset: 0x00014474
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

		// Token: 0x060004FF RID: 1279 RVA: 0x000162F8 File Offset: 0x000144F8
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x0400027E RID: 638
		private string creationMessage;

		// Token: 0x0400027F RID: 639
		private string m_credentialFilePath;
	}
}
