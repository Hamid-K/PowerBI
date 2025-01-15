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
	// Token: 0x020000AC RID: 172
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class RuntimeCheckFailedException : GatewayPipelineException
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x00038495 File Offset: 0x00036695
		// (set) Token: 0x06000D6B RID: 3435 RVA: 0x0003849D File Offset: 0x0003669D
		public string FailureMessage
		{
			get
			{
				return this.m_failureMessage;
			}
			protected set
			{
				this.m_failureMessage = value;
			}
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x000384A6 File Offset: 0x000366A6
		public RuntimeCheckFailedException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x000384BA File Offset: 0x000366BA
		public RuntimeCheckFailedException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x000384D0 File Offset: 0x000366D0
		public RuntimeCheckFailedException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x000384F3 File Offset: 0x000366F3
		public RuntimeCheckFailedException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00038518 File Offset: 0x00036718
		protected RuntimeCheckFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("RuntimeCheckFailedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.FailureMessage = (string)info.GetValue("RuntimeCheckFailedException_FailureMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.FailureMessage = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x000385B4 File Offset: 0x000367B4
		public RuntimeCheckFailedException(string failureMessage, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.FailureMessage = failureMessage;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x000385DE File Offset: 0x000367DE
		public RuntimeCheckFailedException(string failureMessage, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.FailureMessage = failureMessage;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0003860A File Offset: 0x0003680A
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_RuntimeCheckFailedError";
			}
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00038621 File Offset: 0x00036821
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0003862C File Offset: 0x0003682C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(RuntimeCheckFailedException))
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

		// Token: 0x06000D76 RID: 3446 RVA: 0x000386E0 File Offset: 0x000368E0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("RuntimeCheckFailedException_creationMessage", this.creationMessage, typeof(string));
			if (this.FailureMessage != null)
			{
				info.AddValue("RuntimeCheckFailedException_FailureMessage", this.FailureMessage, typeof(string));
			}
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00038740 File Offset: 0x00036940
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "A runtime check error occurred: {0}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty) : ((this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty)));
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x000387B5 File Offset: 0x000369B5
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

		// Token: 0x06000D79 RID: 3449 RVA: 0x000387D2 File Offset: 0x000369D2
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x000387F0 File Offset: 0x000369F0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "FailureMessage={0}", (this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "FailureMessage={0}", (this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "FailureMessage={0}", (this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty)));
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x000388AE File Offset: 0x00036AAE
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x000388B7 File Offset: 0x00036AB7
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x000388C0 File Offset: 0x00036AC0
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x000388CC File Offset: 0x00036ACC
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

		// Token: 0x06000D7F RID: 3455 RVA: 0x00038A90 File Offset: 0x00036C90
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00038A9C File Offset: 0x00036C9C
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

		// Token: 0x06000D81 RID: 3457 RVA: 0x00038B20 File Offset: 0x00036D20
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x0400030D RID: 781
		private string creationMessage;

		// Token: 0x0400030E RID: 782
		private string m_failureMessage;
	}
}
