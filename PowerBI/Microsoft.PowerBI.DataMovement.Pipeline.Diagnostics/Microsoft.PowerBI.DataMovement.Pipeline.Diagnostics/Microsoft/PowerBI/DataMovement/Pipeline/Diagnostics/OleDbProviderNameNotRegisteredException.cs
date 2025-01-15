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
	// Token: 0x02000062 RID: 98
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class OleDbProviderNameNotRegisteredException : OleDbException
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001D3C1 File Offset: 0x0001B5C1
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x0001D3C9 File Offset: 0x0001B5C9
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

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001D3D2 File Offset: 0x0001B5D2
		public OleDbProviderNameNotRegisteredException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001D3E6 File Offset: 0x0001B5E6
		public OleDbProviderNameNotRegisteredException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0001D3FC File Offset: 0x0001B5FC
		public OleDbProviderNameNotRegisteredException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001D41F File Offset: 0x0001B61F
		public OleDbProviderNameNotRegisteredException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001D444 File Offset: 0x0001B644
		protected OleDbProviderNameNotRegisteredException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("OleDbProviderNameNotRegisteredException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ProviderName = (string)info.GetValue("OleDbProviderNameNotRegisteredException_ProviderName", typeof(string));
			}
			catch (SerializationException)
			{
				this.ProviderName = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001D4E0 File Offset: 0x0001B6E0
		public OleDbProviderNameNotRegisteredException(string providerName, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ProviderName = providerName;
			this.ConstructorInternal(false);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001D50A File Offset: 0x0001B70A
		public OleDbProviderNameNotRegisteredException(string providerName, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ProviderName = providerName;
			this.ConstructorInternal(false);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001D536 File Offset: 0x0001B736
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_OleDbProviderNameNotRegistered";
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001D54D File Offset: 0x0001B74D
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001D558 File Offset: 0x0001B758
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(OleDbProviderNameNotRegisteredException))
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

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001D60C File Offset: 0x0001B80C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("OleDbProviderNameNotRegisteredException_creationMessage", this.creationMessage, typeof(string));
			if (this.ProviderName != null)
			{
				info.AddValue("OleDbProviderNameNotRegisteredException_ProviderName", this.ProviderName, typeof(string));
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001D66C File Offset: 0x0001B86C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The provider name '{0}' is not registered as a COM ProgId in the current context.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty) : ((this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty)));
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0001D6E1 File Offset: 0x0001B8E1
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

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001D6FE File Offset: 0x0001B8FE
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001D71C File Offset: 0x0001B91C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ProviderName={0}", (this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ProviderName={0}", (this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ProviderName={0}", (this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty)));
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001D7DA File Offset: 0x0001B9DA
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001D7E3 File Offset: 0x0001B9E3
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001D7EC File Offset: 0x0001B9EC
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001D7F8 File Offset: 0x0001B9F8
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

		// Token: 0x060006EB RID: 1771 RVA: 0x0001D9BC File Offset: 0x0001BBBC
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001D9C8 File Offset: 0x0001BBC8
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

		// Token: 0x060006ED RID: 1773 RVA: 0x0001DA4C File Offset: 0x0001BC4C
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x0400029A RID: 666
		private string creationMessage;

		// Token: 0x0400029B RID: 667
		private string m_providerName;
	}
}
