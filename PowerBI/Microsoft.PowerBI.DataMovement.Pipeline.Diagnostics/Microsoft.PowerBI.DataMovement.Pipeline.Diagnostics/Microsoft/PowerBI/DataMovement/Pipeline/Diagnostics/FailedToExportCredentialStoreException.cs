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
	// Token: 0x0200004C RID: 76
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class FailedToExportCredentialStoreException : GatewayPipelineException
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00016329 File Offset: 0x00014529
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x00016331 File Offset: 0x00014531
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

		// Token: 0x06000502 RID: 1282 RVA: 0x0001633A File Offset: 0x0001453A
		public FailedToExportCredentialStoreException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001634E File Offset: 0x0001454E
		public FailedToExportCredentialStoreException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00016364 File Offset: 0x00014564
		public FailedToExportCredentialStoreException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00016387 File Offset: 0x00014587
		public FailedToExportCredentialStoreException(string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000163B0 File Offset: 0x000145B0
		protected FailedToExportCredentialStoreException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("FailedToExportCredentialStoreException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.CredentialFilePath = (string)info.GetValue("FailedToExportCredentialStoreException_CredentialFilePath", typeof(string));
			}
			catch (SerializationException)
			{
				this.CredentialFilePath = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001644C File Offset: 0x0001464C
		public FailedToExportCredentialStoreException(string credentialFilePath, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.CredentialFilePath = credentialFilePath;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00016476 File Offset: 0x00014676
		public FailedToExportCredentialStoreException(string credentialFilePath, string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.CredentialFilePath = credentialFilePath;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x000164A7 File Offset: 0x000146A7
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_FailedToExportCredentialStoreError";
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000164BE File Offset: 0x000146BE
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000164C8 File Offset: 0x000146C8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(FailedToExportCredentialStoreException))
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

		// Token: 0x0600050C RID: 1292 RVA: 0x0001657C File Offset: 0x0001477C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("FailedToExportCredentialStoreException_creationMessage", this.creationMessage, typeof(string));
			if (this.CredentialFilePath != null)
			{
				info.AddValue("FailedToExportCredentialStoreException_CredentialFilePath", this.CredentialFilePath, typeof(string));
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000165DC File Offset: 0x000147DC
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Could not save credential store to {0}.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.CredentialFilePath != null) ? this.CredentialFilePath.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.CredentialFilePath != null) ? this.CredentialFilePath.ToString().MarkAsCustomerContent() : string.Empty) : ((this.CredentialFilePath != null) ? this.CredentialFilePath.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0001665B File Offset: 0x0001485B
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

		// Token: 0x0600050F RID: 1295 RVA: 0x00016678 File Offset: 0x00014878
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00016698 File Offset: 0x00014898
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "CredentialFilePath={0}", (this.CredentialFilePath != null) ? this.CredentialFilePath.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "CredentialFilePath={0}", (this.CredentialFilePath != null) ? this.CredentialFilePath.ToString().MarkAsCustomerContent() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "CredentialFilePath={0}", (this.CredentialFilePath != null) ? this.CredentialFilePath.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00016760 File Offset: 0x00014960
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00016769 File Offset: 0x00014969
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00016772 File Offset: 0x00014972
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001677C File Offset: 0x0001497C
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

		// Token: 0x06000515 RID: 1301 RVA: 0x00016940 File Offset: 0x00014B40
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001694C File Offset: 0x00014B4C
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

		// Token: 0x06000517 RID: 1303 RVA: 0x000169D0 File Offset: 0x00014BD0
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000280 RID: 640
		private string creationMessage;

		// Token: 0x04000281 RID: 641
		private string m_credentialFilePath;
	}
}
