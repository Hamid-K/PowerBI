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
	// Token: 0x0200001D RID: 29
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class InvalidMonikerSystemResponse : GatewayPipelineException
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000041A1 File Offset: 0x000023A1
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000041A9 File Offset: 0x000023A9
		public string ProviderName
		{
			get
			{
				return this.m_providerName;
			}
			protected set
			{
				this.m_providerName = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000041B2 File Offset: 0x000023B2
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000041BA File Offset: 0x000023BA
		public string ConnectionString
		{
			get
			{
				return this.m_connectionString;
			}
			protected set
			{
				this.m_connectionString = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000041C3 File Offset: 0x000023C3
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x000041CB File Offset: 0x000023CB
		public string IssueDetails
		{
			get
			{
				return this.m_issueDetails;
			}
			protected set
			{
				this.m_issueDetails = value;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000041D4 File Offset: 0x000023D4
		public InvalidMonikerSystemResponse()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000041F2 File Offset: 0x000023F2
		public InvalidMonikerSystemResponse(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004208 File Offset: 0x00002408
		public InvalidMonikerSystemResponse(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000422B File Offset: 0x0000242B
		public InvalidMonikerSystemResponse(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004250 File Offset: 0x00002450
		protected InvalidMonikerSystemResponse(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("InvalidMonikerSystemResponse_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ProviderName = (string)info.GetValue("InvalidMonikerSystemResponse_ProviderName", typeof(string));
			}
			catch (SerializationException)
			{
				this.ProviderName = null;
			}
			try
			{
				this.ConnectionString = (string)info.GetValue("InvalidMonikerSystemResponse_ConnectionString", typeof(string));
			}
			catch (SerializationException)
			{
				this.ConnectionString = null;
			}
			try
			{
				this.IssueDetails = (string)info.GetValue("InvalidMonikerSystemResponse_IssueDetails", typeof(string));
			}
			catch (SerializationException)
			{
				this.IssueDetails = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004360 File Offset: 0x00002560
		public InvalidMonikerSystemResponse(string providerName, string connectionString, string issueDetails, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ProviderName = providerName;
			this.ConnectionString = connectionString;
			this.IssueDetails = issueDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000438C File Offset: 0x0000258C
		public InvalidMonikerSystemResponse(string providerName, string connectionString, string issueDetails, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ProviderName = providerName;
			this.ConnectionString = connectionString;
			this.IssueDetails = issueDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000043C7 File Offset: 0x000025C7
		public InvalidMonikerSystemResponse(string providerName, string connectionString, string issueDetails, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ProviderName = providerName;
			this.ConnectionString = connectionString;
			this.IssueDetails = issueDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004404 File Offset: 0x00002604
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_InvalidMonikerSystemResponseError";
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000441B File Offset: 0x0000261B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004424 File Offset: 0x00002624
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(InvalidMonikerSystemResponse))
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

		// Token: 0x060000CC RID: 204 RVA: 0x000044D8 File Offset: 0x000026D8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("InvalidMonikerSystemResponse_creationMessage", this.creationMessage, typeof(string));
			if (this.ProviderName != null)
			{
				info.AddValue("InvalidMonikerSystemResponse_ProviderName", this.ProviderName, typeof(string));
			}
			if (this.ConnectionString != null)
			{
				info.AddValue("InvalidMonikerSystemResponse_ConnectionString", this.ConnectionString, typeof(string));
			}
			if (this.IssueDetails != null)
			{
				info.AddValue("InvalidMonikerSystemResponse_IssueDetails", this.IssueDetails, typeof(string));
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000457C File Offset: 0x0000277C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The resolved moniker details are invalid for {0}: Details: {1}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty) : ((this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.IssueDetails != null) ? this.IssueDetails.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.IssueDetails != null) ? this.IssueDetails.ToString() : string.Empty) : ((this.IssueDetails != null) ? this.IssueDetails.ToString() : string.Empty)));
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000464A File Offset: 0x0000284A
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

		// Token: 0x060000CF RID: 207 RVA: 0x00004667 File Offset: 0x00002867
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004684 File Offset: 0x00002884
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ProviderName={0}", (this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ProviderName={0}", (this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ProviderName={0}", (this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty)));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ConnectionString={0}", (this.ConnectionString != null) ? this.ConnectionString.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ConnectionString={0}", (this.ConnectionString != null) ? this.ConnectionString.ToString().MarkAsCustomerContent() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ConnectionString={0}", (this.ConnectionString != null) ? this.ConnectionString.ToString().MarkAsCustomerContent() : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "IssueDetails={0}", (this.IssueDetails != null) ? this.IssueDetails.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "IssueDetails={0}", (this.IssueDetails != null) ? this.IssueDetails.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "IssueDetails={0}", (this.IssueDetails != null) ? this.IssueDetails.ToString() : string.Empty)));
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004890 File Offset: 0x00002A90
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004899 File Offset: 0x00002A99
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000048A2 File Offset: 0x00002AA2
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000048AC File Offset: 0x00002AAC
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

		// Token: 0x060000D5 RID: 213 RVA: 0x00004A70 File Offset: 0x00002C70
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004A7C File Offset: 0x00002C7C
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

		// Token: 0x060000D7 RID: 215 RVA: 0x00004B00 File Offset: 0x00002D00
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x0400022E RID: 558
		private string creationMessage;

		// Token: 0x0400022F RID: 559
		private string m_providerName;

		// Token: 0x04000230 RID: 560
		private string m_connectionString;

		// Token: 0x04000231 RID: 561
		private string m_issueDetails;
	}
}
