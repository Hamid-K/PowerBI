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
	// Token: 0x02000047 RID: 71
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class DbConnectionProviderFactoryNotRegisteredException : GatewayPipelineException
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00014701 File Offset: 0x00012901
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x00014709 File Offset: 0x00012909
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

		// Token: 0x06000496 RID: 1174 RVA: 0x00014712 File Offset: 0x00012912
		public DbConnectionProviderFactoryNotRegisteredException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00014726 File Offset: 0x00012926
		public DbConnectionProviderFactoryNotRegisteredException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001473C File Offset: 0x0001293C
		public DbConnectionProviderFactoryNotRegisteredException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001475F File Offset: 0x0001295F
		public DbConnectionProviderFactoryNotRegisteredException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00014784 File Offset: 0x00012984
		protected DbConnectionProviderFactoryNotRegisteredException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("DbConnectionProviderFactoryNotRegisteredException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ProviderName = (string)info.GetValue("DbConnectionProviderFactoryNotRegisteredException_ProviderName", typeof(string));
			}
			catch (SerializationException)
			{
				this.ProviderName = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00014820 File Offset: 0x00012A20
		public DbConnectionProviderFactoryNotRegisteredException(string providerName, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ProviderName = providerName;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001484A File Offset: 0x00012A4A
		public DbConnectionProviderFactoryNotRegisteredException(string providerName, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ProviderName = providerName;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00014876 File Offset: 0x00012A76
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_DbConnectionProviderFactoryNotRegisteredError";
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001488D File Offset: 0x00012A8D
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00014898 File Offset: 0x00012A98
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(DbConnectionProviderFactoryNotRegisteredException))
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

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001494C File Offset: 0x00012B4C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("DbConnectionProviderFactoryNotRegisteredException_creationMessage", this.creationMessage, typeof(string));
			if (this.ProviderName != null)
			{
				info.AddValue("DbConnectionProviderFactoryNotRegisteredException_ProviderName", this.ProviderName, typeof(string));
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000149AC File Offset: 0x00012BAC
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "No DB connection provider factory for provider '{0}' has been registered.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty) : ((this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty)));
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00014A21 File Offset: 0x00012C21
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

		// Token: 0x060004A3 RID: 1187 RVA: 0x00014A3E File Offset: 0x00012C3E
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00014A5C File Offset: 0x00012C5C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ProviderName={0}", (this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ProviderName={0}", (this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ProviderName={0}", (this.ProviderName != null) ? this.ProviderName.ToString() : string.Empty)));
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00014B1A File Offset: 0x00012D1A
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00014B23 File Offset: 0x00012D23
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00014B2C File Offset: 0x00012D2C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00014B38 File Offset: 0x00012D38
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

		// Token: 0x060004A9 RID: 1193 RVA: 0x00014CFC File Offset: 0x00012EFC
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00014D08 File Offset: 0x00012F08
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

		// Token: 0x060004AB RID: 1195 RVA: 0x00014D8C File Offset: 0x00012F8C
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000279 RID: 633
		private string creationMessage;

		// Token: 0x0400027A RID: 634
		private string m_providerName;
	}
}
