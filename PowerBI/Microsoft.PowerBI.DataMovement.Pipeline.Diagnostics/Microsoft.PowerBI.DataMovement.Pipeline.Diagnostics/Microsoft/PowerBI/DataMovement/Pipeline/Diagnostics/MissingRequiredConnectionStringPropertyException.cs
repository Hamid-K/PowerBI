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
	// Token: 0x02000030 RID: 48
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class MissingRequiredConnectionStringPropertyException : GatewayPipelineException
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000C0F1 File Offset: 0x0000A2F1
		// (set) Token: 0x06000280 RID: 640 RVA: 0x0000C0F9 File Offset: 0x0000A2F9
		public string Keyword
		{
			get
			{
				return this.m_keyword;
			}
			protected set
			{
				this.m_keyword = value;
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000C102 File Offset: 0x0000A302
		public MissingRequiredConnectionStringPropertyException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000C116 File Offset: 0x0000A316
		public MissingRequiredConnectionStringPropertyException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000C12C File Offset: 0x0000A32C
		public MissingRequiredConnectionStringPropertyException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000C14F File Offset: 0x0000A34F
		public MissingRequiredConnectionStringPropertyException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000C174 File Offset: 0x0000A374
		protected MissingRequiredConnectionStringPropertyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("MissingRequiredConnectionStringPropertyException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Keyword = (string)info.GetValue("MissingRequiredConnectionStringPropertyException_Keyword", typeof(string));
			}
			catch (SerializationException)
			{
				this.Keyword = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000C210 File Offset: 0x0000A410
		public MissingRequiredConnectionStringPropertyException(string keyword, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.Keyword = keyword;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000C23A File Offset: 0x0000A43A
		public MissingRequiredConnectionStringPropertyException(string keyword, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.Keyword = keyword;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000C266 File Offset: 0x0000A466
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_MissingRequiredConnectionStringPropertyError";
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000C27D File Offset: 0x0000A47D
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000C288 File Offset: 0x0000A488
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(MissingRequiredConnectionStringPropertyException))
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

		// Token: 0x0600028B RID: 651 RVA: 0x0000C33C File Offset: 0x0000A53C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("MissingRequiredConnectionStringPropertyException_creationMessage", this.creationMessage, typeof(string));
			if (this.Keyword != null)
			{
				info.AddValue("MissingRequiredConnectionStringPropertyException_Keyword", this.Keyword, typeof(string));
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000C39C File Offset: 0x0000A59C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Property '{0}' is missing from the connection string.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Keyword != null) ? this.Keyword.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Keyword != null) ? this.Keyword.ToString() : string.Empty) : ((this.Keyword != null) ? this.Keyword.ToString() : string.Empty)));
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000C411 File Offset: 0x0000A611
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

		// Token: 0x0600028E RID: 654 RVA: 0x0000C42E File Offset: 0x0000A62E
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000C44C File Offset: 0x0000A64C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Keyword={0}", (this.Keyword != null) ? this.Keyword.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Keyword={0}", (this.Keyword != null) ? this.Keyword.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Keyword={0}", (this.Keyword != null) ? this.Keyword.ToString() : string.Empty)));
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000C50A File Offset: 0x0000A70A
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000C513 File Offset: 0x0000A713
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000C51C File Offset: 0x0000A71C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000C528 File Offset: 0x0000A728
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

		// Token: 0x06000294 RID: 660 RVA: 0x0000C6EC File Offset: 0x0000A8EC
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000C6F8 File Offset: 0x0000A8F8
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

		// Token: 0x06000296 RID: 662 RVA: 0x0000C77C File Offset: 0x0000A97C
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000256 RID: 598
		private string creationMessage;

		// Token: 0x04000257 RID: 599
		private string m_keyword;
	}
}
