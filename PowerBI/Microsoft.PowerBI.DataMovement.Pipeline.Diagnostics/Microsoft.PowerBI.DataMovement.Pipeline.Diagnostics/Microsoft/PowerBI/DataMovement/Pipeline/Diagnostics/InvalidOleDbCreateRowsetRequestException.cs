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
	// Token: 0x020000B8 RID: 184
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class InvalidOleDbCreateRowsetRequestException : GatewayPipelineException
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x0003D241 File Offset: 0x0003B441
		// (set) Token: 0x06000E85 RID: 3717 RVA: 0x0003D249 File Offset: 0x0003B449
		public string Details
		{
			get
			{
				return this.m_details;
			}
			protected set
			{
				this.m_details = value;
			}
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0003D252 File Offset: 0x0003B452
		public InvalidOleDbCreateRowsetRequestException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0003D266 File Offset: 0x0003B466
		public InvalidOleDbCreateRowsetRequestException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0003D27C File Offset: 0x0003B47C
		public InvalidOleDbCreateRowsetRequestException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0003D29F File Offset: 0x0003B49F
		public InvalidOleDbCreateRowsetRequestException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0003D2C4 File Offset: 0x0003B4C4
		protected InvalidOleDbCreateRowsetRequestException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("InvalidOleDbCreateRowsetRequestException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Details = (string)info.GetValue("InvalidOleDbCreateRowsetRequestException_Details", typeof(string));
			}
			catch (SerializationException)
			{
				this.Details = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0003D360 File Offset: 0x0003B560
		public InvalidOleDbCreateRowsetRequestException(string details, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.Details = details;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0003D38A File Offset: 0x0003B58A
		public InvalidOleDbCreateRowsetRequestException(string details, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.Details = details;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0003D3B6 File Offset: 0x0003B5B6
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_InvalidOleDbCreateRowsetRequestError";
			}
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0003D3CD File Offset: 0x0003B5CD
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0003D3D8 File Offset: 0x0003B5D8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(InvalidOleDbCreateRowsetRequestException))
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

		// Token: 0x06000E90 RID: 3728 RVA: 0x0003D48C File Offset: 0x0003B68C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("InvalidOleDbCreateRowsetRequestException_creationMessage", this.creationMessage, typeof(string));
			if (this.Details != null)
			{
				info.AddValue("InvalidOleDbCreateRowsetRequestException_Details", this.Details, typeof(string));
			}
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0003D4EC File Offset: 0x0003B6EC
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The provided OLE DB create row set request is invalid. Details: {0}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Details != null) ? this.Details.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Details != null) ? this.Details.ToString().MarkAsCustomerContent() : string.Empty) : ((this.Details != null) ? this.Details.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x0003D56B File Offset: 0x0003B76B
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

		// Token: 0x06000E93 RID: 3731 RVA: 0x0003D588 File Offset: 0x0003B788
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0003D5A8 File Offset: 0x0003B7A8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Details={0}", (this.Details != null) ? this.Details.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Details={0}", (this.Details != null) ? this.Details.ToString().MarkAsCustomerContent() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Details={0}", (this.Details != null) ? this.Details.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0003D670 File Offset: 0x0003B870
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0003D679 File Offset: 0x0003B879
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0003D682 File Offset: 0x0003B882
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0003D68C File Offset: 0x0003B88C
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

		// Token: 0x06000E99 RID: 3737 RVA: 0x0003D850 File Offset: 0x0003BA50
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0003D85C File Offset: 0x0003BA5C
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

		// Token: 0x06000E9B RID: 3739 RVA: 0x0003D8E0 File Offset: 0x0003BAE0
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000323 RID: 803
		private string creationMessage;

		// Token: 0x04000324 RID: 804
		private string m_details;
	}
}
