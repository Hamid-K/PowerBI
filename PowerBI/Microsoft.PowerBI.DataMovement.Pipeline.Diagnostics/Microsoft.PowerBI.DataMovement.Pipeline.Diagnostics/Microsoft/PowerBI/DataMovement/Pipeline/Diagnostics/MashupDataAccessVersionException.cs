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
	// Token: 0x02000088 RID: 136
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class MashupDataAccessVersionException : MashupDataAccessException
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x0002B2F9 File Offset: 0x000294F9
		// (set) Token: 0x06000A37 RID: 2615 RVA: 0x0002B301 File Offset: 0x00029501
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

		// Token: 0x06000A38 RID: 2616 RVA: 0x0002B30A File Offset: 0x0002950A
		public MashupDataAccessVersionException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0002B31E File Offset: 0x0002951E
		public MashupDataAccessVersionException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0002B334 File Offset: 0x00029534
		public MashupDataAccessVersionException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0002B357 File Offset: 0x00029557
		public MashupDataAccessVersionException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0002B37C File Offset: 0x0002957C
		protected MashupDataAccessVersionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("MashupDataAccessVersionException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ErrorCode = (string)info.GetValue("MashupDataAccessVersionException_ErrorCode", typeof(string));
			}
			catch (SerializationException)
			{
				this.ErrorCode = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0002B418 File Offset: 0x00029618
		public MashupDataAccessVersionException(string errorCode, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ErrorCode = errorCode;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002B442 File Offset: 0x00029642
		public MashupDataAccessVersionException(string errorCode, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ErrorCode = errorCode;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002B46E File Offset: 0x0002966E
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002B477 File Offset: 0x00029677
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002B47F File Offset: 0x0002967F
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0002B484 File Offset: 0x00029684
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(MashupDataAccessVersionException))
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

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002B598 File Offset: 0x00029798
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("MashupDataAccessVersionException_creationMessage", this.creationMessage, typeof(string));
			if (this.ErrorCode != null)
			{
				info.AddValue("MashupDataAccessVersionException_ErrorCode", this.ErrorCode, typeof(string));
			}
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002B5F8 File Offset: 0x000297F8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "A package with an unsupported version was found. Error code: {0}.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ErrorCode != null) ? this.ErrorCode.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ErrorCode != null) ? this.ErrorCode.ToString() : string.Empty) : ((this.ErrorCode != null) ? this.ErrorCode.ToString() : string.Empty)));
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0002B66D File Offset: 0x0002986D
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

		// Token: 0x06000A46 RID: 2630 RVA: 0x0002B68A File Offset: 0x0002988A
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0002B6A8 File Offset: 0x000298A8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ErrorCode={0}", (this.ErrorCode != null) ? this.ErrorCode.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ErrorCode={0}", (this.ErrorCode != null) ? this.ErrorCode.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ErrorCode={0}", (this.ErrorCode != null) ? this.ErrorCode.ToString() : string.Empty)));
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0002B766 File Offset: 0x00029966
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0002B76F File Offset: 0x0002996F
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0002B778 File Offset: 0x00029978
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0002B784 File Offset: 0x00029984
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

		// Token: 0x06000A4C RID: 2636 RVA: 0x0002B948 File Offset: 0x00029B48
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0002B954 File Offset: 0x00029B54
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

		// Token: 0x06000A4E RID: 2638 RVA: 0x0002B9D8 File Offset: 0x00029BD8
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x040002D6 RID: 726
		private string creationMessage;

		// Token: 0x040002D7 RID: 727
		private string m_errorCode;
	}
}
