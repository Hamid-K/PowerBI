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
	// Token: 0x02000071 RID: 113
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class AdomdHostNotFoundException : AdomdConnectException
	{
		// Token: 0x06000829 RID: 2089 RVA: 0x00022825 File Offset: 0x00020A25
		public AdomdHostNotFoundException()
		{
			this.ConstructorInternal(false);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00022834 File Offset: 0x00020A34
		public AdomdHostNotFoundException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0002284A File Offset: 0x00020A4A
		public AdomdHostNotFoundException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0002286D File Offset: 0x00020A6D
		public AdomdHostNotFoundException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00022894 File Offset: 0x00020A94
		protected AdomdHostNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("AdomdHostNotFoundException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x000228F8 File Offset: 0x00020AF8
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00022901 File Offset: 0x00020B01
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00022909 File Offset: 0x00020B09
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0002290C File Offset: 0x00020B0C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(AdomdHostNotFoundException))
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

		// Token: 0x06000832 RID: 2098 RVA: 0x000229BF File Offset: 0x00020BBF
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("AdomdHostNotFoundException_creationMessage", this.creationMessage, typeof(string));
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x000229EF File Offset: 0x00020BEF
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to open an ADOMD connection; the destination host is not known.", Array.Empty<object>());
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x00022A05 File Offset: 0x00020C05
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

		// Token: 0x06000835 RID: 2101 RVA: 0x00022A22 File Offset: 0x00020C22
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00022A3F File Offset: 0x00020C3F
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string newLine = Environment.NewLine;
			return base.GetPropertiesString(markupKind);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00022A4E File Offset: 0x00020C4E
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00022A57 File Offset: 0x00020C57
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00022A60 File Offset: 0x00020C60
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00022A6C File Offset: 0x00020C6C
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

		// Token: 0x0600083B RID: 2107 RVA: 0x00022C30 File Offset: 0x00020E30
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00022C3C File Offset: 0x00020E3C
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

		// Token: 0x0600083D RID: 2109 RVA: 0x00022CC0 File Offset: 0x00020EC0
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x040002B0 RID: 688
		private string creationMessage;
	}
}
