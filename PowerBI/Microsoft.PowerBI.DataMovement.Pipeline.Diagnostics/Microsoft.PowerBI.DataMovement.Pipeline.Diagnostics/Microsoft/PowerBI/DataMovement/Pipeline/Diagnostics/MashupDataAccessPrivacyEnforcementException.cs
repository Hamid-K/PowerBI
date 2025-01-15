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
	// Token: 0x02000087 RID: 135
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class MashupDataAccessPrivacyEnforcementException : MashupDataAccessPrivacyException
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0002ABD5 File Offset: 0x00028DD5
		// (set) Token: 0x06000A1E RID: 2590 RVA: 0x0002ABDD File Offset: 0x00028DDD
		public string DataSources
		{
			get
			{
				return this.m_dataSources;
			}
			protected set
			{
				this.m_dataSources = value;
			}
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0002ABE6 File Offset: 0x00028DE6
		public MashupDataAccessPrivacyEnforcementException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0002ABFA File Offset: 0x00028DFA
		public MashupDataAccessPrivacyEnforcementException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0002AC10 File Offset: 0x00028E10
		public MashupDataAccessPrivacyEnforcementException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0002AC33 File Offset: 0x00028E33
		public MashupDataAccessPrivacyEnforcementException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0002AC58 File Offset: 0x00028E58
		protected MashupDataAccessPrivacyEnforcementException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("MashupDataAccessPrivacyEnforcementException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DataSources = (string)info.GetValue("MashupDataAccessPrivacyEnforcementException_DataSources", typeof(string));
			}
			catch (SerializationException)
			{
				this.DataSources = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0002ACF4 File Offset: 0x00028EF4
		public MashupDataAccessPrivacyEnforcementException(string dataSources, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.DataSources = dataSources;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0002AD1E File Offset: 0x00028F1E
		public MashupDataAccessPrivacyEnforcementException(string dataSources, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.DataSources = dataSources;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002AD4A File Offset: 0x00028F4A
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0002AD53 File Offset: 0x00028F53
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0002AD5B File Offset: 0x00028F5B
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002AD60 File Offset: 0x00028F60
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(MashupDataAccessPrivacyEnforcementException))
			{
				TraceSourceBase<DiagnosticsTraceSource>.Tracer.TraceError("Exception object created [IsBenign={0}]: {1}: {2}; ErrorShortName: {3}", new object[]
				{
					this.IsBenign(),
					type,
					this.GetMarkedUpMessage(),
					this.ErrorShortName
				});
				IList<PowerBIErrorDetail> errorDetails = this.GetErrorDetails();
				if (errorDetails != null && errorDetails.Count > 0)
				{
					for (int i = 0; i < errorDetails.Count; i++)
					{
						PowerBIErrorDetail powerBIErrorDetail = errorDetails[i];
						TraceSourceBase<DiagnosticsTraceSource>.Tracer.TraceError("Exception data: {0} = {1}", new object[]
						{
							powerBIErrorDetail.NameCode,
							powerBIErrorDetail.Value.ResourceValue.MarkAsCustomerContent()
						});
					}
				}
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

		// Token: 0x06000A2A RID: 2602 RVA: 0x0002AE74 File Offset: 0x00029074
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("MashupDataAccessPrivacyEnforcementException_creationMessage", this.creationMessage, typeof(string));
			if (this.DataSources != null)
			{
				info.AddValue("MashupDataAccessPrivacyEnforcementException_DataSources", this.DataSources, typeof(string));
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0002AED4 File Offset: 0x000290D4
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "An error due to incompatible privacy levels on data sources occurred. Data sources: {0}.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DataSources != null) ? this.DataSources.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DataSources != null) ? this.DataSources.ToString().MarkAsCustomerContent() : string.Empty) : ((this.DataSources != null) ? this.DataSources.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0002AF53 File Offset: 0x00029153
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

		// Token: 0x06000A2D RID: 2605 RVA: 0x0002AF70 File Offset: 0x00029170
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0002AF90 File Offset: 0x00029190
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DataSources={0}", (this.DataSources != null) ? this.DataSources.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DataSources={0}", (this.DataSources != null) ? this.DataSources.ToString().MarkAsCustomerContent() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DataSources={0}", (this.DataSources != null) ? this.DataSources.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0002B058 File Offset: 0x00029258
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0002B061 File Offset: 0x00029261
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0002B06A File Offset: 0x0002926A
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0002B074 File Offset: 0x00029274
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

		// Token: 0x06000A33 RID: 2611 RVA: 0x0002B238 File Offset: 0x00029438
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002B244 File Offset: 0x00029444
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

		// Token: 0x06000A35 RID: 2613 RVA: 0x0002B2C8 File Offset: 0x000294C8
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x040002D4 RID: 724
		private string creationMessage;

		// Token: 0x040002D5 RID: 725
		private string m_dataSources;
	}
}
