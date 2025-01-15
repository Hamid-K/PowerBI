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
	// Token: 0x02000027 RID: 39
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class UnsupportedAdomdAuthenticationTypeException : GatewayPipelineException
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000089AD File Offset: 0x00006BAD
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x000089B5 File Offset: 0x00006BB5
		public string AuthType
		{
			get
			{
				return this.m_authType;
			}
			protected set
			{
				this.m_authType = value;
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000089BE File Offset: 0x00006BBE
		public UnsupportedAdomdAuthenticationTypeException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000089D2 File Offset: 0x00006BD2
		public UnsupportedAdomdAuthenticationTypeException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000089E8 File Offset: 0x00006BE8
		public UnsupportedAdomdAuthenticationTypeException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008A0B File Offset: 0x00006C0B
		public UnsupportedAdomdAuthenticationTypeException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00008A30 File Offset: 0x00006C30
		protected UnsupportedAdomdAuthenticationTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("UnsupportedAdomdAuthenticationTypeException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.AuthType = (string)info.GetValue("UnsupportedAdomdAuthenticationTypeException_AuthType", typeof(string));
			}
			catch (SerializationException)
			{
				this.AuthType = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008ACC File Offset: 0x00006CCC
		public UnsupportedAdomdAuthenticationTypeException(string authType, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.AuthType = authType;
			this.ConstructorInternal(false);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00008AF6 File Offset: 0x00006CF6
		public UnsupportedAdomdAuthenticationTypeException(string authType, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.AuthType = authType;
			this.ConstructorInternal(false);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008B22 File Offset: 0x00006D22
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_UnsupportedAdomdAuthenticationTypeError";
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008B39 File Offset: 0x00006D39
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00008B41 File Offset: 0x00006D41
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008B44 File Offset: 0x00006D44
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(UnsupportedAdomdAuthenticationTypeException))
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

		// Token: 0x060001BE RID: 446 RVA: 0x00008BF8 File Offset: 0x00006DF8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("UnsupportedAdomdAuthenticationTypeException_creationMessage", this.creationMessage, typeof(string));
			if (this.AuthType != null)
			{
				info.AddValue("UnsupportedAdomdAuthenticationTypeException_AuthType", this.AuthType, typeof(string));
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00008C58 File Offset: 0x00006E58
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The authentication type '{0}' is not supported. Only the authentication type 'Windows' is supported.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.AuthType != null) ? this.AuthType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.AuthType != null) ? this.AuthType.ToString() : string.Empty) : ((this.AuthType != null) ? this.AuthType.ToString() : string.Empty)));
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00008CCD File Offset: 0x00006ECD
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

		// Token: 0x060001C1 RID: 449 RVA: 0x00008CEA File Offset: 0x00006EEA
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00008D08 File Offset: 0x00006F08
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "AuthType={0}", (this.AuthType != null) ? this.AuthType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "AuthType={0}", (this.AuthType != null) ? this.AuthType.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "AuthType={0}", (this.AuthType != null) ? this.AuthType.ToString() : string.Empty)));
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00008DC6 File Offset: 0x00006FC6
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00008DCF File Offset: 0x00006FCF
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008DD8 File Offset: 0x00006FD8
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00008DE4 File Offset: 0x00006FE4
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

		// Token: 0x060001C7 RID: 455 RVA: 0x00008FA8 File Offset: 0x000071A8
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00008FB4 File Offset: 0x000071B4
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

		// Token: 0x060001C9 RID: 457 RVA: 0x00009038 File Offset: 0x00007238
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000247 RID: 583
		private string creationMessage;

		// Token: 0x04000248 RID: 584
		private string m_authType;
	}
}
