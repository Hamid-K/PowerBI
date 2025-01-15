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
	// Token: 0x02000065 RID: 101
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class InvalidOleDbProviderSetupException : OleDbException
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0001E7F5 File Offset: 0x0001C9F5
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x0001E7FD File Offset: 0x0001C9FD
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

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0001E806 File Offset: 0x0001CA06
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x0001E80E File Offset: 0x0001CA0E
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

		// Token: 0x06000722 RID: 1826 RVA: 0x0001E817 File Offset: 0x0001CA17
		public InvalidOleDbProviderSetupException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001E830 File Offset: 0x0001CA30
		public InvalidOleDbProviderSetupException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001E846 File Offset: 0x0001CA46
		public InvalidOleDbProviderSetupException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001E869 File Offset: 0x0001CA69
		public InvalidOleDbProviderSetupException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001E890 File Offset: 0x0001CA90
		protected InvalidOleDbProviderSetupException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("InvalidOleDbProviderSetupException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ProviderName = (string)info.GetValue("InvalidOleDbProviderSetupException_ProviderName", typeof(string));
			}
			catch (SerializationException)
			{
				this.ProviderName = null;
			}
			try
			{
				this.Reason = (string)info.GetValue("InvalidOleDbProviderSetupException_Reason", typeof(string));
			}
			catch (SerializationException)
			{
				this.Reason = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001E968 File Offset: 0x0001CB68
		public InvalidOleDbProviderSetupException(string providerName, string reason, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ProviderName = providerName;
			this.Reason = reason;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001E98C File Offset: 0x0001CB8C
		public InvalidOleDbProviderSetupException(string providerName, string reason, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ProviderName = providerName;
			this.Reason = reason;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001E9BE File Offset: 0x0001CBBE
		public InvalidOleDbProviderSetupException(string providerName, string reason, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ProviderName = providerName;
			this.Reason = reason;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001E9F2 File Offset: 0x0001CBF2
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_InvalidOleDbProviderSetupError";
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001EA09 File Offset: 0x0001CC09
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001EA14 File Offset: 0x0001CC14
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(InvalidOleDbProviderSetupException))
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

		// Token: 0x0600072D RID: 1837 RVA: 0x0001EAC8 File Offset: 0x0001CCC8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("InvalidOleDbProviderSetupException_creationMessage", this.creationMessage, typeof(string));
			if (this.ProviderName != null)
			{
				info.AddValue("InvalidOleDbProviderSetupException_ProviderName", this.ProviderName, typeof(string));
			}
			if (this.Reason != null)
			{
				info.AddValue("InvalidOleDbProviderSetupException_Reason", this.Reason, typeof(string));
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001EB4C File Offset: 0x0001CD4C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} OLE DB provider setup is not valid. Reason: {1}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty) : ((this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.Reason != null) ? this.Reason.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Reason != null) ? this.Reason.ToString() : string.Empty) : ((this.Reason != null) ? this.Reason.ToString() : string.Empty)));
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0001EC1A File Offset: 0x0001CE1A
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

		// Token: 0x06000730 RID: 1840 RVA: 0x0001EC37 File Offset: 0x0001CE37
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001EC54 File Offset: 0x0001CE54
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ProviderName={0}", (this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ProviderName={0}", (this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ProviderName={0}", (this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Reason={0}", (this.Reason != null) ? this.Reason.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Reason={0}", (this.Reason != null) ? this.Reason.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Reason={0}", (this.Reason != null) ? this.Reason.ToString() : string.Empty)));
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001EDB4 File Offset: 0x0001CFB4
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001EDBD File Offset: 0x0001CFBD
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001EDC6 File Offset: 0x0001CFC6
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001EDD0 File Offset: 0x0001CFD0
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

		// Token: 0x06000736 RID: 1846 RVA: 0x0001EF94 File Offset: 0x0001D194
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001EFA0 File Offset: 0x0001D1A0
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

		// Token: 0x06000738 RID: 1848 RVA: 0x0001F024 File Offset: 0x0001D224
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x040002A0 RID: 672
		private string creationMessage;

		// Token: 0x040002A1 RID: 673
		private string m_providerName;

		// Token: 0x040002A2 RID: 674
		private string m_reason;
	}
}
