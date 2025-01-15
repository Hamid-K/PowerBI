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
	// Token: 0x020000B0 RID: 176
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class DiscoverCustomConnectorsOnGatewayExeption : GatewayPipelineException
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x00039FF8 File Offset: 0x000381F8
		// (set) Token: 0x06000DCD RID: 3533 RVA: 0x0003A000 File Offset: 0x00038200
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

		// Token: 0x06000DCE RID: 3534 RVA: 0x0003A009 File Offset: 0x00038209
		public DiscoverCustomConnectorsOnGatewayExeption()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0003A01D File Offset: 0x0003821D
		public DiscoverCustomConnectorsOnGatewayExeption(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0003A033 File Offset: 0x00038233
		public DiscoverCustomConnectorsOnGatewayExeption(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0003A056 File Offset: 0x00038256
		public DiscoverCustomConnectorsOnGatewayExeption(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0003A07C File Offset: 0x0003827C
		protected DiscoverCustomConnectorsOnGatewayExeption(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("DiscoverCustomConnectorsOnGatewayExeption_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.FailureMessage = (string)info.GetValue("DiscoverCustomConnectorsOnGatewayExeption_FailureMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.FailureMessage = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0003A118 File Offset: 0x00038318
		public DiscoverCustomConnectorsOnGatewayExeption(string failureMessage, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.FailureMessage = failureMessage;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0003A142 File Offset: 0x00038342
		public DiscoverCustomConnectorsOnGatewayExeption(string failureMessage, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.FailureMessage = failureMessage;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0003A16E File Offset: 0x0003836E
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Client_DiscoverCustomConnectorsOnGatewayError";
			}
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0003A185 File Offset: 0x00038385
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0003A190 File Offset: 0x00038390
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(DiscoverCustomConnectorsOnGatewayExeption))
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

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0003A244 File Offset: 0x00038444
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("DiscoverCustomConnectorsOnGatewayExeption_creationMessage", this.creationMessage, typeof(string));
			if (this.FailureMessage != null)
			{
				info.AddValue("DiscoverCustomConnectorsOnGatewayExeption_FailureMessage", this.FailureMessage, typeof(string));
			}
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0003A2A4 File Offset: 0x000384A4
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to discover custom connectors on gateway, exception: {0}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty) : ((this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty)));
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x0003A319 File Offset: 0x00038519
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

		// Token: 0x06000DDB RID: 3547 RVA: 0x0003A336 File Offset: 0x00038536
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0003A354 File Offset: 0x00038554
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "FailureMessage={0}", (this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "FailureMessage={0}", (this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "FailureMessage={0}", (this.FailureMessage != null) ? this.FailureMessage.ToString() : string.Empty)));
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0003A412 File Offset: 0x00038612
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0003A41B File Offset: 0x0003861B
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0003A424 File Offset: 0x00038624
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0003A430 File Offset: 0x00038630
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

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0003A5F4 File Offset: 0x000387F4
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0003A600 File Offset: 0x00038800
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

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0003A684 File Offset: 0x00038884
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			GatewayPipelineException ex = base.InnerException as GatewayPipelineException;
			if (ex != null)
			{
				list.AddRange(ex.GetErrorDetails());
			}
			return list;
		}

		// Token: 0x04000315 RID: 789
		private string creationMessage;

		// Token: 0x04000316 RID: 790
		private string m_failureMessage;
	}
}
