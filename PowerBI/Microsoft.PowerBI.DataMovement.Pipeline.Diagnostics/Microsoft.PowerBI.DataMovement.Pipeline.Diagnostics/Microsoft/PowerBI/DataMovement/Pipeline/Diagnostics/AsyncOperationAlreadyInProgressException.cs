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
	// Token: 0x0200008D RID: 141
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class AsyncOperationAlreadyInProgressException : GatewayPipelineException
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x0002D74D File Offset: 0x0002B94D
		// (set) Token: 0x06000AB6 RID: 2742 RVA: 0x0002D755 File Offset: 0x0002B955
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

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002D75E File Offset: 0x0002B95E
		public AsyncOperationAlreadyInProgressException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidValueField<Guid>();
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x0002D772 File Offset: 0x0002B972
		public AsyncOperationAlreadyInProgressException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0002D788 File Offset: 0x0002B988
		public AsyncOperationAlreadyInProgressException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0002D7AB File Offset: 0x0002B9AB
		public AsyncOperationAlreadyInProgressException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0002D7D0 File Offset: 0x0002B9D0
		protected AsyncOperationAlreadyInProgressException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("AsyncOperationAlreadyInProgressException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.AsyncOperationId = (Guid)info.GetValue("AsyncOperationAlreadyInProgressException_AsyncOperationId", typeof(Guid));
			this.ConstructorInternal(true);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0002D854 File Offset: 0x0002BA54
		public AsyncOperationAlreadyInProgressException(Guid asyncOperationId, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.AsyncOperationId = asyncOperationId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0002D871 File Offset: 0x0002BA71
		public AsyncOperationAlreadyInProgressException(Guid asyncOperationId, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.AsyncOperationId = asyncOperationId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0002D89B File Offset: 0x0002BA9B
		public AsyncOperationAlreadyInProgressException(Guid asyncOperationId, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.AsyncOperationId = asyncOperationId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0002D8C7 File Offset: 0x0002BAC7
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_AsyncOperationAlreadyInProgress";
			}
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0002D8DE File Offset: 0x0002BADE
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0002D8E8 File Offset: 0x0002BAE8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(AsyncOperationAlreadyInProgressException))
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

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0002D99C File Offset: 0x0002BB9C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("AsyncOperationAlreadyInProgressException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("AsyncOperationAlreadyInProgressException_AsyncOperationId", this.AsyncOperationId, typeof(Guid));
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002D9F8 File Offset: 0x0002BBF8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The async operation id {0} is already running.", (markupKind == PrivateInformationMarkupKind.None) ? this.AsyncOperationId.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.AsyncOperationId.ToString() : this.AsyncOperationId.ToString()));
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0002DA5B File Offset: 0x0002BC5B
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

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002DA78 File Offset: 0x0002BC78
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0002DA98 File Offset: 0x0002BC98
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "AsyncOperationId={0}", this.AsyncOperationId.ToString()) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "AsyncOperationId={0}", this.AsyncOperationId.ToString()) : string.Format(CultureInfo.CurrentCulture, "AsyncOperationId={0}", this.AsyncOperationId.ToString())));
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0002DB44 File Offset: 0x0002BD44
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0002DB4D File Offset: 0x0002BD4D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0002DB56 File Offset: 0x0002BD56
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0002DB60 File Offset: 0x0002BD60
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

		// Token: 0x06000ACB RID: 2763 RVA: 0x0002DD24 File Offset: 0x0002BF24
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0002DD30 File Offset: 0x0002BF30
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

		// Token: 0x06000ACD RID: 2765 RVA: 0x0002DDB4 File Offset: 0x0002BFB4
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x040002E1 RID: 737
		private string creationMessage;

		// Token: 0x040002E2 RID: 738
		private Guid m_asyncOperationId;
	}
}
