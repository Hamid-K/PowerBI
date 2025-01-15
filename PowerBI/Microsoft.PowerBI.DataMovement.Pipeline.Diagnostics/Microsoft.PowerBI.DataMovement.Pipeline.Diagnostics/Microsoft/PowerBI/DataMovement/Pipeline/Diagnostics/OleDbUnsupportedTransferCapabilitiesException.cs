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
	// Token: 0x020000BC RID: 188
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class OleDbUnsupportedTransferCapabilitiesException : GatewayPipelineException
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x0003EB61 File Offset: 0x0003CD61
		// (set) Token: 0x06000EE1 RID: 3809 RVA: 0x0003EB69 File Offset: 0x0003CD69
		public IEnumerable<string> Capabilities
		{
			get
			{
				return this.m_capabilities;
			}
			protected set
			{
				this.m_capabilities = value;
			}
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0003EB72 File Offset: 0x0003CD72
		public OleDbUnsupportedTransferCapabilitiesException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<IEnumerable<string>>();
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0003EB86 File Offset: 0x0003CD86
		public OleDbUnsupportedTransferCapabilitiesException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0003EB9C File Offset: 0x0003CD9C
		public OleDbUnsupportedTransferCapabilitiesException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0003EBBF File Offset: 0x0003CDBF
		public OleDbUnsupportedTransferCapabilitiesException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0003EBE4 File Offset: 0x0003CDE4
		protected OleDbUnsupportedTransferCapabilitiesException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("OleDbUnsupportedTransferCapabilitiesException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Capabilities = (IEnumerable<string>)info.GetValue("OleDbUnsupportedTransferCapabilitiesException_Capabilities", typeof(IEnumerable<string>));
			}
			catch (SerializationException)
			{
				this.Capabilities = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0003EC80 File Offset: 0x0003CE80
		public OleDbUnsupportedTransferCapabilitiesException(IEnumerable<string> capabilities, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.Capabilities = capabilities;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0003EC9D File Offset: 0x0003CE9D
		public OleDbUnsupportedTransferCapabilitiesException(IEnumerable<string> capabilities, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.Capabilities = capabilities;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0003ECC7 File Offset: 0x0003CEC7
		public OleDbUnsupportedTransferCapabilitiesException(IEnumerable<string> capabilities, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.Capabilities = capabilities;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0003ECF3 File Offset: 0x0003CEF3
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_OleDbUnsupportedTransferCapabilitiesError";
			}
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0003ED0A File Offset: 0x0003CF0A
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0003ED14 File Offset: 0x0003CF14
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(OleDbUnsupportedTransferCapabilitiesException))
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

		// Token: 0x06000EED RID: 3821 RVA: 0x0003EDC8 File Offset: 0x0003CFC8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("OleDbUnsupportedTransferCapabilitiesException_creationMessage", this.creationMessage, typeof(string));
			if (this.Capabilities != null)
			{
				info.AddValue("OleDbUnsupportedTransferCapabilitiesException_Capabilities", this.Capabilities, typeof(IEnumerable<string>));
			}
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0003EE26 File Offset: 0x0003D026
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "OLE DB transfer connection doesn't support required capabilities.", Array.Empty<object>());
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x0003EE3C File Offset: 0x0003D03C
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

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0003EE59 File Offset: 0x0003D059
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0003EE78 File Offset: 0x0003D078
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Capabilities={0}", (this.Capabilities != null) ? this.Capabilities.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Capabilities={0}", (this.Capabilities != null) ? this.Capabilities.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Capabilities={0}", (this.Capabilities != null) ? this.Capabilities.ToString() : string.Empty)));
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0003EF36 File Offset: 0x0003D136
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0003EF3F File Offset: 0x0003D13F
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0003EF48 File Offset: 0x0003D148
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0003EF54 File Offset: 0x0003D154
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

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0003F118 File Offset: 0x0003D318
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0003F124 File Offset: 0x0003D324
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

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0003F1A8 File Offset: 0x0003D3A8
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x0400032A RID: 810
		private string creationMessage;

		// Token: 0x0400032B RID: 811
		private IEnumerable<string> m_capabilities;
	}
}
