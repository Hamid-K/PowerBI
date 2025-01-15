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
	// Token: 0x020000C1 RID: 193
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class OleDbInvalidGatewayResolutionParametersException : GatewayPipelineException
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x00040539 File Offset: 0x0003E739
		// (set) Token: 0x06000F4A RID: 3914 RVA: 0x00040541 File Offset: 0x0003E741
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

		// Token: 0x06000F4B RID: 3915 RVA: 0x0004054A File Offset: 0x0003E74A
		public OleDbInvalidGatewayResolutionParametersException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0004055E File Offset: 0x0003E75E
		public OleDbInvalidGatewayResolutionParametersException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x00040574 File Offset: 0x0003E774
		public OleDbInvalidGatewayResolutionParametersException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x00040597 File Offset: 0x0003E797
		public OleDbInvalidGatewayResolutionParametersException(string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x000405C0 File Offset: 0x0003E7C0
		protected OleDbInvalidGatewayResolutionParametersException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("OleDbInvalidGatewayResolutionParametersException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Parameter = (string)info.GetValue("OleDbInvalidGatewayResolutionParametersException_Parameter", typeof(string));
			}
			catch (SerializationException)
			{
				this.Parameter = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0004065C File Offset: 0x0003E85C
		public OleDbInvalidGatewayResolutionParametersException(string parameter, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.Parameter = parameter;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x00040686 File Offset: 0x0003E886
		public OleDbInvalidGatewayResolutionParametersException(string parameter, string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.Parameter = parameter;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x000406B7 File Offset: 0x0003E8B7
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_OleDbInvalidGatewayResolutionParametersError";
			}
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x000406CE File Offset: 0x0003E8CE
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x000406D8 File Offset: 0x0003E8D8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(OleDbInvalidGatewayResolutionParametersException))
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

		// Token: 0x06000F55 RID: 3925 RVA: 0x0004078C File Offset: 0x0003E98C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("OleDbInvalidGatewayResolutionParametersException_creationMessage", this.creationMessage, typeof(string));
			if (this.Parameter != null)
			{
				info.AddValue("OleDbInvalidGatewayResolutionParametersException_Parameter", this.Parameter, typeof(string));
			}
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x000407EC File Offset: 0x0003E9EC
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "'{0}' in the 'Gateway' property is missing or its value is invalid.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Parameter != null) ? this.Parameter.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Parameter != null) ? this.Parameter.ToString() : string.Empty) : ((this.Parameter != null) ? this.Parameter.ToString() : string.Empty)));
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x00040861 File Offset: 0x0003EA61
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

		// Token: 0x06000F58 RID: 3928 RVA: 0x0004087E File Offset: 0x0003EA7E
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0004089C File Offset: 0x0003EA9C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Parameter={0}", (this.Parameter != null) ? this.Parameter.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Parameter={0}", (this.Parameter != null) ? this.Parameter.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Parameter={0}", (this.Parameter != null) ? this.Parameter.ToString() : string.Empty)));
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0004095A File Offset: 0x0003EB5A
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x00040963 File Offset: 0x0003EB63
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0004096C File Offset: 0x0003EB6C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00040978 File Offset: 0x0003EB78
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

		// Token: 0x06000F5E RID: 3934 RVA: 0x00040B3C File Offset: 0x0003ED3C
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x00040B48 File Offset: 0x0003ED48
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

		// Token: 0x06000F60 RID: 3936 RVA: 0x00040BCC File Offset: 0x0003EDCC
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000330 RID: 816
		private string creationMessage;

		// Token: 0x04000331 RID: 817
		private string m_parameter;
	}
}
