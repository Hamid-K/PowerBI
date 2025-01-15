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
	// Token: 0x02000028 RID: 40
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class AdomdEffectiveUserNameMissingException : GatewayPipelineException
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00009069 File Offset: 0x00007269
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00009071 File Offset: 0x00007271
		public string ConnectionString
		{
			get
			{
				return this.m_connectionString;
			}
			protected set
			{
				this.m_connectionString = value;
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000907A File Offset: 0x0000727A
		public AdomdEffectiveUserNameMissingException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000908E File Offset: 0x0000728E
		public AdomdEffectiveUserNameMissingException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000090A4 File Offset: 0x000072A4
		public AdomdEffectiveUserNameMissingException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000090C7 File Offset: 0x000072C7
		public AdomdEffectiveUserNameMissingException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000090EC File Offset: 0x000072EC
		protected AdomdEffectiveUserNameMissingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("AdomdEffectiveUserNameMissingException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ConnectionString = (string)info.GetValue("AdomdEffectiveUserNameMissingException_ConnectionString", typeof(string));
			}
			catch (SerializationException)
			{
				this.ConnectionString = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00009188 File Offset: 0x00007388
		public AdomdEffectiveUserNameMissingException(string connectionString, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConnectionString = connectionString;
			this.ConstructorInternal(false);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000091B2 File Offset: 0x000073B2
		public AdomdEffectiveUserNameMissingException(string connectionString, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConnectionString = connectionString;
			this.ConstructorInternal(false);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000091DE File Offset: 0x000073DE
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_AdomdEffectiveUserNameMissingError";
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000091F5 File Offset: 0x000073F5
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00009200 File Offset: 0x00007400
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(AdomdEffectiveUserNameMissingException))
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

		// Token: 0x060001D6 RID: 470 RVA: 0x000092B4 File Offset: 0x000074B4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("AdomdEffectiveUserNameMissingException_creationMessage", this.creationMessage, typeof(string));
			if (this.ConnectionString != null)
			{
				info.AddValue("AdomdEffectiveUserNameMissingException_ConnectionString", this.ConnectionString, typeof(string));
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00009314 File Offset: 0x00007514
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Adomd transfer connection '{0}' is missing effective user name.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ConnectionString != null) ? this.ConnectionString.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ConnectionString != null) ? this.ConnectionString.ToString().MarkAsCustomerContent() : string.Empty) : ((this.ConnectionString != null) ? this.ConnectionString.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00009393 File Offset: 0x00007593
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

		// Token: 0x060001D9 RID: 473 RVA: 0x000093B0 File Offset: 0x000075B0
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000093D0 File Offset: 0x000075D0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ConnectionString={0}", (this.ConnectionString != null) ? this.ConnectionString.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ConnectionString={0}", (this.ConnectionString != null) ? this.ConnectionString.ToString().MarkAsCustomerContent() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ConnectionString={0}", (this.ConnectionString != null) ? this.ConnectionString.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00009498 File Offset: 0x00007698
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000094A1 File Offset: 0x000076A1
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000094AA File Offset: 0x000076AA
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000094B4 File Offset: 0x000076B4
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

		// Token: 0x060001DF RID: 479 RVA: 0x00009678 File Offset: 0x00007878
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00009684 File Offset: 0x00007884
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

		// Token: 0x060001E1 RID: 481 RVA: 0x00009708 File Offset: 0x00007908
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000249 RID: 585
		private string creationMessage;

		// Token: 0x0400024A RID: 586
		private string m_connectionString;
	}
}
