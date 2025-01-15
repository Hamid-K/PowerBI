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
	// Token: 0x020000A1 RID: 161
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class SSOTestConnectionException : GatewayPipelineException
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00034971 File Offset: 0x00032B71
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x00034979 File Offset: 0x00032B79
		public string ResolvedUserPrinicpalName
		{
			get
			{
				return this.m_resolvedUserPrinicpalName;
			}
			protected set
			{
				this.m_resolvedUserPrinicpalName = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x00034982 File Offset: 0x00032B82
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x0003498A File Offset: 0x00032B8A
		public string ActiveDirectoryServer
		{
			get
			{
				return this.m_activeDirectoryServer;
			}
			protected set
			{
				this.m_activeDirectoryServer = value;
			}
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00034993 File Offset: 0x00032B93
		public SSOTestConnectionException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x000349AC File Offset: 0x00032BAC
		public SSOTestConnectionException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x000349C2 File Offset: 0x00032BC2
		public SSOTestConnectionException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x000349E5 File Offset: 0x00032BE5
		public SSOTestConnectionException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00034A0C File Offset: 0x00032C0C
		protected SSOTestConnectionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("SSOTestConnectionException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ResolvedUserPrinicpalName = (string)info.GetValue("SSOTestConnectionException_ResolvedUserPrinicpalName", typeof(string));
			}
			catch (SerializationException)
			{
				this.ResolvedUserPrinicpalName = null;
			}
			try
			{
				this.ActiveDirectoryServer = (string)info.GetValue("SSOTestConnectionException_ActiveDirectoryServer", typeof(string));
			}
			catch (SerializationException)
			{
				this.ActiveDirectoryServer = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00034AE4 File Offset: 0x00032CE4
		public SSOTestConnectionException(string resolvedUserPrinicpalName, string activeDirectoryServer, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ResolvedUserPrinicpalName = resolvedUserPrinicpalName;
			this.ActiveDirectoryServer = activeDirectoryServer;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00034B08 File Offset: 0x00032D08
		public SSOTestConnectionException(string resolvedUserPrinicpalName, string activeDirectoryServer, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ResolvedUserPrinicpalName = resolvedUserPrinicpalName;
			this.ActiveDirectoryServer = activeDirectoryServer;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00034B3A File Offset: 0x00032D3A
		public SSOTestConnectionException(string resolvedUserPrinicpalName, string activeDirectoryServer, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ResolvedUserPrinicpalName = resolvedUserPrinicpalName;
			this.ActiveDirectoryServer = activeDirectoryServer;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00034B6E File Offset: 0x00032D6E
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_SSOTestConnectionError";
			}
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00034B85 File Offset: 0x00032D85
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x00034B8D File Offset: 0x00032D8D
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00034B90 File Offset: 0x00032D90
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(SSOTestConnectionException))
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

		// Token: 0x06000C8C RID: 3212 RVA: 0x00034C44 File Offset: 0x00032E44
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("SSOTestConnectionException_creationMessage", this.creationMessage, typeof(string));
			if (this.ResolvedUserPrinicpalName != null)
			{
				info.AddValue("SSOTestConnectionException_ResolvedUserPrinicpalName", this.ResolvedUserPrinicpalName, typeof(string));
			}
			if (this.ActiveDirectoryServer != null)
			{
				info.AddValue("SSOTestConnectionException_ActiveDirectoryServer", this.ActiveDirectoryServer, typeof(string));
			}
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00034CC5 File Offset: 0x00032EC5
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "An exception encountered while testing a datasource connection using single sign on.", Array.Empty<object>());
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x00034CDB File Offset: 0x00032EDB
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

		// Token: 0x06000C8F RID: 3215 RVA: 0x00034CF8 File Offset: 0x00032EF8
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00034D18 File Offset: 0x00032F18
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ResolvedUserPrinicpalName={0}", (this.ResolvedUserPrinicpalName != null) ? this.ResolvedUserPrinicpalName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ResolvedUserPrinicpalName={0}", (this.ResolvedUserPrinicpalName != null) ? this.ResolvedUserPrinicpalName.ToString().MarkAsCustomerContent() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ResolvedUserPrinicpalName={0}", (this.ResolvedUserPrinicpalName != null) ? this.ResolvedUserPrinicpalName.ToString().MarkAsCustomerContent() : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ActiveDirectoryServer={0}", (this.ActiveDirectoryServer != null) ? this.ActiveDirectoryServer.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ActiveDirectoryServer={0}", (this.ActiveDirectoryServer != null) ? this.ActiveDirectoryServer.ToString().MarkAsCustomerContent() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ActiveDirectoryServer={0}", (this.ActiveDirectoryServer != null) ? this.ActiveDirectoryServer.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00034E8C File Offset: 0x0003308C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00034E95 File Offset: 0x00033095
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00034E9E File Offset: 0x0003309E
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00034EA8 File Offset: 0x000330A8
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

		// Token: 0x06000C95 RID: 3221 RVA: 0x0003506C File Offset: 0x0003326C
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00035078 File Offset: 0x00033278
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

		// Token: 0x06000C97 RID: 3223 RVA: 0x000350FC File Offset: 0x000332FC
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

		// Token: 0x040002FF RID: 767
		private string creationMessage;

		// Token: 0x04000300 RID: 768
		private string m_resolvedUserPrinicpalName;

		// Token: 0x04000301 RID: 769
		private string m_activeDirectoryServer;
	}
}
