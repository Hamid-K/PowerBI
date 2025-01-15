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
	// Token: 0x02000037 RID: 55
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class ClientAsyncOperationCancelledException : GatewayPipelineException
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000EA70 File Offset: 0x0000CC70
		// (set) Token: 0x0600031F RID: 799 RVA: 0x0000EA78 File Offset: 0x0000CC78
		public Guid AsyncOperationId
		{
			get
			{
				return this.m_asyncOperationId;
			}
			protected set
			{
				this.m_asyncOperationId = value;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000EA81 File Offset: 0x0000CC81
		public ClientAsyncOperationCancelledException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidValueField<Guid>();
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000EA95 File Offset: 0x0000CC95
		public ClientAsyncOperationCancelledException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000EAAB File Offset: 0x0000CCAB
		public ClientAsyncOperationCancelledException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000EACE File Offset: 0x0000CCCE
		public ClientAsyncOperationCancelledException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000EAF4 File Offset: 0x0000CCF4
		protected ClientAsyncOperationCancelledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ClientAsyncOperationCancelledException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.AsyncOperationId = (Guid)info.GetValue("ClientAsyncOperationCancelledException_AsyncOperationId", typeof(Guid));
			this.ConstructorInternal(true);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000EB78 File Offset: 0x0000CD78
		public ClientAsyncOperationCancelledException(Guid asyncOperationId, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.AsyncOperationId = asyncOperationId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000EB95 File Offset: 0x0000CD95
		public ClientAsyncOperationCancelledException(Guid asyncOperationId, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.AsyncOperationId = asyncOperationId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000EBBF File Offset: 0x0000CDBF
		public ClientAsyncOperationCancelledException(Guid asyncOperationId, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.AsyncOperationId = asyncOperationId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000EBEB File Offset: 0x0000CDEB
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_ClientAsyncOperationCancelled";
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000EC02 File Offset: 0x0000CE02
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000EC0A File Offset: 0x0000CE0A
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000EC10 File Offset: 0x0000CE10
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(ClientAsyncOperationCancelledException))
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

		// Token: 0x0600032C RID: 812 RVA: 0x0000ECC4 File Offset: 0x0000CEC4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ClientAsyncOperationCancelledException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ClientAsyncOperationCancelledException_AsyncOperationId", this.AsyncOperationId, typeof(Guid));
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000ED20 File Offset: 0x0000CF20
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The async operation id {0} was already cancelled in gateway client.", (markupKind == PrivateInformationMarkupKind.None) ? this.AsyncOperationId.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.AsyncOperationId.ToString() : this.AsyncOperationId.ToString()));
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000ED83 File Offset: 0x0000CF83
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

		// Token: 0x0600032F RID: 815 RVA: 0x0000EDA0 File Offset: 0x0000CFA0
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000EDC0 File Offset: 0x0000CFC0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "AsyncOperationId={0}", this.AsyncOperationId.ToString()) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "AsyncOperationId={0}", this.AsyncOperationId.ToString()) : string.Format(CultureInfo.CurrentCulture, "AsyncOperationId={0}", this.AsyncOperationId.ToString())));
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000EE6C File Offset: 0x0000D06C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000EE75 File Offset: 0x0000D075
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000EE7E File Offset: 0x0000D07E
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000EE88 File Offset: 0x0000D088
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

		// Token: 0x06000335 RID: 821 RVA: 0x0000F04C File Offset: 0x0000D24C
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000F058 File Offset: 0x0000D258
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

		// Token: 0x06000337 RID: 823 RVA: 0x0000F0DC File Offset: 0x0000D2DC
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000261 RID: 609
		private string creationMessage;

		// Token: 0x04000262 RID: 610
		private Guid m_asyncOperationId;
	}
}
