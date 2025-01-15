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
	// Token: 0x02000086 RID: 134
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class MashupDataAccessPrivacySettingException : MashupDataAccessPrivacyException
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0002A4B1 File Offset: 0x000286B1
		// (set) Token: 0x06000A05 RID: 2565 RVA: 0x0002A4B9 File Offset: 0x000286B9
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

		// Token: 0x06000A06 RID: 2566 RVA: 0x0002A4C2 File Offset: 0x000286C2
		public MashupDataAccessPrivacySettingException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0002A4D6 File Offset: 0x000286D6
		public MashupDataAccessPrivacySettingException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0002A4EC File Offset: 0x000286EC
		public MashupDataAccessPrivacySettingException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0002A50F File Offset: 0x0002870F
		public MashupDataAccessPrivacySettingException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0002A534 File Offset: 0x00028734
		protected MashupDataAccessPrivacySettingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("MashupDataAccessPrivacySettingException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DataSources = (string)info.GetValue("MashupDataAccessPrivacySettingException_DataSources", typeof(string));
			}
			catch (SerializationException)
			{
				this.DataSources = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0002A5D0 File Offset: 0x000287D0
		public MashupDataAccessPrivacySettingException(string dataSources, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.DataSources = dataSources;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0002A5FA File Offset: 0x000287FA
		public MashupDataAccessPrivacySettingException(string dataSources, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.DataSources = dataSources;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0002A626 File Offset: 0x00028826
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0002A62F File Offset: 0x0002882F
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0002A637 File Offset: 0x00028837
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0002A63C File Offset: 0x0002883C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(MashupDataAccessPrivacySettingException))
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

		// Token: 0x06000A11 RID: 2577 RVA: 0x0002A750 File Offset: 0x00028950
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("MashupDataAccessPrivacySettingException_creationMessage", this.creationMessage, typeof(string));
			if (this.DataSources != null)
			{
				info.AddValue("MashupDataAccessPrivacySettingException_DataSources", this.DataSources, typeof(string));
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002A7B0 File Offset: 0x000289B0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "An error due to missing privacy settings necessary for enforcement occurred. Data sources: {0}.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DataSources != null) ? this.DataSources.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DataSources != null) ? this.DataSources.ToString().MarkAsCustomerContent() : string.Empty) : ((this.DataSources != null) ? this.DataSources.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0002A82F File Offset: 0x00028A2F
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

		// Token: 0x06000A14 RID: 2580 RVA: 0x0002A84C File Offset: 0x00028A4C
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0002A86C File Offset: 0x00028A6C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DataSources={0}", (this.DataSources != null) ? this.DataSources.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DataSources={0}", (this.DataSources != null) ? this.DataSources.ToString().MarkAsCustomerContent() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DataSources={0}", (this.DataSources != null) ? this.DataSources.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0002A934 File Offset: 0x00028B34
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0002A93D File Offset: 0x00028B3D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0002A946 File Offset: 0x00028B46
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0002A950 File Offset: 0x00028B50
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

		// Token: 0x06000A1A RID: 2586 RVA: 0x0002AB14 File Offset: 0x00028D14
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0002AB20 File Offset: 0x00028D20
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

		// Token: 0x06000A1C RID: 2588 RVA: 0x0002ABA4 File Offset: 0x00028DA4
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x040002D2 RID: 722
		private string creationMessage;

		// Token: 0x040002D3 RID: 723
		private string m_dataSources;
	}
}
