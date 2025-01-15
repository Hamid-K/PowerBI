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
	// Token: 0x02000045 RID: 69
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class ClientAsyncOperationExpiredException : GatewayPipelineException
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0001398C File Offset: 0x00011B8C
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x00013994 File Offset: 0x00011B94
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

		// Token: 0x06000464 RID: 1124 RVA: 0x0001399D File Offset: 0x00011B9D
		public ClientAsyncOperationExpiredException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidValueField<Guid>();
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000139B1 File Offset: 0x00011BB1
		public ClientAsyncOperationExpiredException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000139C7 File Offset: 0x00011BC7
		public ClientAsyncOperationExpiredException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000139EA File Offset: 0x00011BEA
		public ClientAsyncOperationExpiredException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00013A10 File Offset: 0x00011C10
		protected ClientAsyncOperationExpiredException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ClientAsyncOperationExpiredException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.AsyncOperationId = (Guid)info.GetValue("ClientAsyncOperationExpiredException_AsyncOperationId", typeof(Guid));
			this.ConstructorInternal(true);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00013A94 File Offset: 0x00011C94
		public ClientAsyncOperationExpiredException(Guid asyncOperationId, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.AsyncOperationId = asyncOperationId;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00013AB1 File Offset: 0x00011CB1
		public ClientAsyncOperationExpiredException(Guid asyncOperationId, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.AsyncOperationId = asyncOperationId;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00013ADB File Offset: 0x00011CDB
		public ClientAsyncOperationExpiredException(Guid asyncOperationId, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.AsyncOperationId = asyncOperationId;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00013B07 File Offset: 0x00011D07
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Client_AsyncOperationExpired";
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00013B1E File Offset: 0x00011D1E
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00013B26 File Offset: 0x00011D26
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00013B2C File Offset: 0x00011D2C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(ClientAsyncOperationExpiredException))
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

		// Token: 0x06000470 RID: 1136 RVA: 0x00013BE0 File Offset: 0x00011DE0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ClientAsyncOperationExpiredException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ClientAsyncOperationExpiredException_AsyncOperationId", this.AsyncOperationId, typeof(Guid));
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00013C3C File Offset: 0x00011E3C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The async operation '{0}' for streaming result is expired.", (markupKind == PrivateInformationMarkupKind.None) ? this.AsyncOperationId.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.AsyncOperationId.ToString() : this.AsyncOperationId.ToString()));
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00013C9F File Offset: 0x00011E9F
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

		// Token: 0x06000473 RID: 1139 RVA: 0x00013CBC File Offset: 0x00011EBC
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00013CDC File Offset: 0x00011EDC
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "AsyncOperationId={0}", this.AsyncOperationId.ToString()) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "AsyncOperationId={0}", this.AsyncOperationId.ToString()) : string.Format(CultureInfo.CurrentCulture, "AsyncOperationId={0}", this.AsyncOperationId.ToString())));
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00013D88 File Offset: 0x00011F88
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00013D91 File Offset: 0x00011F91
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00013D9A File Offset: 0x00011F9A
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00013DA4 File Offset: 0x00011FA4
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

		// Token: 0x06000479 RID: 1145 RVA: 0x00013F68 File Offset: 0x00012168
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00013F74 File Offset: 0x00012174
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

		// Token: 0x0600047B RID: 1147 RVA: 0x00013FF8 File Offset: 0x000121F8
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

		// Token: 0x04000275 RID: 629
		private string creationMessage;

		// Token: 0x04000276 RID: 630
		private Guid m_asyncOperationId;
	}
}
