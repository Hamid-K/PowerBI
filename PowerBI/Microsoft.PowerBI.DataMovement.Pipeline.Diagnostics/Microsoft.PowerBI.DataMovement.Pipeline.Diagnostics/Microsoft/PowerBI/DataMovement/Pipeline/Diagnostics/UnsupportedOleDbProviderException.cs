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
	// Token: 0x02000063 RID: 99
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class UnsupportedOleDbProviderException : OleDbException
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0001DA7D File Offset: 0x0001BC7D
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x0001DA85 File Offset: 0x0001BC85
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

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001DA8E File Offset: 0x0001BC8E
		public UnsupportedOleDbProviderException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001DAA2 File Offset: 0x0001BCA2
		public UnsupportedOleDbProviderException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001DAB8 File Offset: 0x0001BCB8
		public UnsupportedOleDbProviderException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001DADB File Offset: 0x0001BCDB
		public UnsupportedOleDbProviderException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001DB00 File Offset: 0x0001BD00
		protected UnsupportedOleDbProviderException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("UnsupportedOleDbProviderException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ProviderName = (string)info.GetValue("UnsupportedOleDbProviderException_ProviderName", typeof(string));
			}
			catch (SerializationException)
			{
				this.ProviderName = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001DB9C File Offset: 0x0001BD9C
		public UnsupportedOleDbProviderException(string providerName, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ProviderName = providerName;
			this.ConstructorInternal(false);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001DBC6 File Offset: 0x0001BDC6
		public UnsupportedOleDbProviderException(string providerName, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ProviderName = providerName;
			this.ConstructorInternal(false);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001DBF2 File Offset: 0x0001BDF2
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_UnsupportedOleDbProviderError";
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001DC09 File Offset: 0x0001BE09
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001DC14 File Offset: 0x0001BE14
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(UnsupportedOleDbProviderException))
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

		// Token: 0x060006FA RID: 1786 RVA: 0x0001DCC8 File Offset: 0x0001BEC8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("UnsupportedOleDbProviderException_creationMessage", this.creationMessage, typeof(string));
			if (this.ProviderName != null)
			{
				info.AddValue("UnsupportedOleDbProviderException_ProviderName", this.ProviderName, typeof(string));
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001DD28 File Offset: 0x0001BF28
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "OLE DB provider '{0}' is not supported.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty) : ((this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty)));
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x0001DD9D File Offset: 0x0001BF9D
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

		// Token: 0x060006FD RID: 1789 RVA: 0x0001DDBA File Offset: 0x0001BFBA
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001DDD8 File Offset: 0x0001BFD8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ProviderName={0}", (this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ProviderName={0}", (this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ProviderName={0}", (this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty)));
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001DE96 File Offset: 0x0001C096
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001DE9F File Offset: 0x0001C09F
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001DEA8 File Offset: 0x0001C0A8
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001DEB4 File Offset: 0x0001C0B4
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

		// Token: 0x06000703 RID: 1795 RVA: 0x0001E078 File Offset: 0x0001C278
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001E084 File Offset: 0x0001C284
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

		// Token: 0x06000705 RID: 1797 RVA: 0x0001E108 File Offset: 0x0001C308
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x0400029C RID: 668
		private string creationMessage;

		// Token: 0x0400029D RID: 669
		private string m_providerName;
	}
}
