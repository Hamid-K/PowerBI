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
	// Token: 0x0200008B RID: 139
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class MashupDataAccessHostingException : MashupDataAccessException
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0002C645 File Offset: 0x0002A845
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x0002C64D File Offset: 0x0002A84D
		public string ErrorCode
		{
			get
			{
				return this.m_errorCode;
			}
			protected set
			{
				this.m_errorCode = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0002C656 File Offset: 0x0002A856
		// (set) Token: 0x06000A80 RID: 2688 RVA: 0x0002C65E File Offset: 0x0002A85E
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

		// Token: 0x06000A81 RID: 2689 RVA: 0x0002C667 File Offset: 0x0002A867
		public MashupDataAccessHostingException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0002C680 File Offset: 0x0002A880
		public MashupDataAccessHostingException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0002C696 File Offset: 0x0002A896
		public MashupDataAccessHostingException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0002C6B9 File Offset: 0x0002A8B9
		public MashupDataAccessHostingException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0002C6E0 File Offset: 0x0002A8E0
		protected MashupDataAccessHostingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("MashupDataAccessHostingException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ErrorCode = (string)info.GetValue("MashupDataAccessHostingException_ErrorCode", typeof(string));
			}
			catch (SerializationException)
			{
				this.ErrorCode = null;
			}
			try
			{
				this.Reason = (string)info.GetValue("MashupDataAccessHostingException_Reason", typeof(string));
			}
			catch (SerializationException)
			{
				this.Reason = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0002C7B8 File Offset: 0x0002A9B8
		public MashupDataAccessHostingException(string errorCode, string reason, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ErrorCode = errorCode;
			this.Reason = reason;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0002C7DC File Offset: 0x0002A9DC
		public MashupDataAccessHostingException(string errorCode, string reason, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ErrorCode = errorCode;
			this.Reason = reason;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0002C80E File Offset: 0x0002AA0E
		public MashupDataAccessHostingException(string errorCode, string reason, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ErrorCode = errorCode;
			this.Reason = reason;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0002C842 File Offset: 0x0002AA42
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0002C84B File Offset: 0x0002AA4B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0002C853 File Offset: 0x0002AA53
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002C858 File Offset: 0x0002AA58
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(MashupDataAccessHostingException))
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

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002C96C File Offset: 0x0002AB6C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("MashupDataAccessHostingException_creationMessage", this.creationMessage, typeof(string));
			if (this.ErrorCode != null)
			{
				info.AddValue("MashupDataAccessHostingException_ErrorCode", this.ErrorCode, typeof(string));
			}
			if (this.Reason != null)
			{
				info.AddValue("MashupDataAccessHostingException_Reason", this.Reason, typeof(string));
			}
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002C9F0 File Offset: 0x0002ABF0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "A problem hosting the mashup engine was detected. Reason: {0}. Error code: {1}.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Reason != null) ? this.Reason.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Reason != null) ? this.Reason.ToString() : string.Empty) : ((this.Reason != null) ? this.Reason.ToString() : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.ErrorCode != null) ? this.ErrorCode.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ErrorCode != null) ? this.ErrorCode.ToString() : string.Empty) : ((this.ErrorCode != null) ? this.ErrorCode.ToString() : string.Empty)));
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x0002CABE File Offset: 0x0002ACBE
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

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002CADB File Offset: 0x0002ACDB
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002CAF8 File Offset: 0x0002ACF8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ErrorCode={0}", (this.ErrorCode != null) ? this.ErrorCode.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ErrorCode={0}", (this.ErrorCode != null) ? this.ErrorCode.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ErrorCode={0}", (this.ErrorCode != null) ? this.ErrorCode.ToString() : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Reason={0}", (this.Reason != null) ? this.Reason.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Reason={0}", (this.Reason != null) ? this.Reason.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Reason={0}", (this.Reason != null) ? this.Reason.ToString() : string.Empty)));
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0002CC58 File Offset: 0x0002AE58
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0002CC61 File Offset: 0x0002AE61
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0002CC6A File Offset: 0x0002AE6A
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0002CC74 File Offset: 0x0002AE74
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

		// Token: 0x06000A96 RID: 2710 RVA: 0x0002CE38 File Offset: 0x0002B038
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0002CE44 File Offset: 0x0002B044
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

		// Token: 0x06000A98 RID: 2712 RVA: 0x0002CEC8 File Offset: 0x0002B0C8
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x040002DB RID: 731
		private string creationMessage;

		// Token: 0x040002DC RID: 732
		private string m_errorCode;

		// Token: 0x040002DD RID: 733
		private string m_reason;
	}
}
