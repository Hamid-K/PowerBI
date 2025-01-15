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
	// Token: 0x0200009C RID: 156
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class RequiredCredentailDetailsParameterMissing : GatewayPipelineException
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x00032F48 File Offset: 0x00031148
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x00032F50 File Offset: 0x00031150
		public string Parameter
		{
			get
			{
				return this.m_parameter;
			}
			protected set
			{
				this.m_parameter = value;
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00032F59 File Offset: 0x00031159
		public RequiredCredentailDetailsParameterMissing()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00032F6D File Offset: 0x0003116D
		public RequiredCredentailDetailsParameterMissing(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00032F83 File Offset: 0x00031183
		public RequiredCredentailDetailsParameterMissing(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00032FA6 File Offset: 0x000311A6
		public RequiredCredentailDetailsParameterMissing(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00032FCC File Offset: 0x000311CC
		protected RequiredCredentailDetailsParameterMissing(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("RequiredCredentailDetailsParameterMissing_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Parameter = (string)info.GetValue("RequiredCredentailDetailsParameterMissing_Parameter", typeof(string));
			}
			catch (SerializationException)
			{
				this.Parameter = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00033068 File Offset: 0x00031268
		public RequiredCredentailDetailsParameterMissing(string parameter, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.Parameter = parameter;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00033092 File Offset: 0x00031292
		public RequiredCredentailDetailsParameterMissing(string parameter, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.Parameter = parameter;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x000330BE File Offset: 0x000312BE
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_RequiredCredentailDetailsParameterMissingError";
			}
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x000330D5 File Offset: 0x000312D5
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x000330DD File Offset: 0x000312DD
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x000330E0 File Offset: 0x000312E0
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(RequiredCredentailDetailsParameterMissing))
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

		// Token: 0x06000C1D RID: 3101 RVA: 0x00033194 File Offset: 0x00031394
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("RequiredCredentailDetailsParameterMissing_creationMessage", this.creationMessage, typeof(string));
			if (this.Parameter != null)
			{
				info.AddValue("RequiredCredentailDetailsParameterMissing_Parameter", this.Parameter, typeof(string));
			}
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x000331F4 File Offset: 0x000313F4
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Required parameter {0} is missing from CredentialDetails", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Parameter != null) ? this.Parameter.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Parameter != null) ? this.Parameter.ToString() : string.Empty) : ((this.Parameter != null) ? this.Parameter.ToString() : string.Empty)));
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x00033269 File Offset: 0x00031469
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

		// Token: 0x06000C20 RID: 3104 RVA: 0x00033286 File Offset: 0x00031486
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x000332A4 File Offset: 0x000314A4
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Parameter={0}", (this.Parameter != null) ? this.Parameter.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Parameter={0}", (this.Parameter != null) ? this.Parameter.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Parameter={0}", (this.Parameter != null) ? this.Parameter.ToString() : string.Empty)));
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00033362 File Offset: 0x00031562
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0003336B File Offset: 0x0003156B
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00033374 File Offset: 0x00031574
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00033380 File Offset: 0x00031580
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

		// Token: 0x06000C26 RID: 3110 RVA: 0x00033544 File Offset: 0x00031744
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00033550 File Offset: 0x00031750
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

		// Token: 0x06000C28 RID: 3112 RVA: 0x000335D4 File Offset: 0x000317D4
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x040002F9 RID: 761
		private string creationMessage;

		// Token: 0x040002FA RID: 762
		private string m_parameter;
	}
}
