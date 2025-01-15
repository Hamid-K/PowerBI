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
	// Token: 0x02000091 RID: 145
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class AsyncOperationStreamingCancelledException : GatewayPipelineException
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0002F1B5 File Offset: 0x0002D3B5
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x0002F1BD File Offset: 0x0002D3BD
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

		// Token: 0x06000B1B RID: 2843 RVA: 0x0002F1C6 File Offset: 0x0002D3C6
		public AsyncOperationStreamingCancelledException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidValueField<Guid>();
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002F1DA File Offset: 0x0002D3DA
		public AsyncOperationStreamingCancelledException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0002F1F0 File Offset: 0x0002D3F0
		public AsyncOperationStreamingCancelledException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0002F213 File Offset: 0x0002D413
		public AsyncOperationStreamingCancelledException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0002F238 File Offset: 0x0002D438
		protected AsyncOperationStreamingCancelledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("AsyncOperationStreamingCancelledException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.AsyncOperationId = (Guid)info.GetValue("AsyncOperationStreamingCancelledException_AsyncOperationId", typeof(Guid));
			this.ConstructorInternal(true);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0002F2BC File Offset: 0x0002D4BC
		public AsyncOperationStreamingCancelledException(Guid asyncOperationId, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.AsyncOperationId = asyncOperationId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0002F2D9 File Offset: 0x0002D4D9
		public AsyncOperationStreamingCancelledException(Guid asyncOperationId, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.AsyncOperationId = asyncOperationId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0002F303 File Offset: 0x0002D503
		public AsyncOperationStreamingCancelledException(Guid asyncOperationId, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.AsyncOperationId = asyncOperationId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0002F32F File Offset: 0x0002D52F
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_AsyncOperationStreamingCancelled";
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0002F346 File Offset: 0x0002D546
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0002F350 File Offset: 0x0002D550
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(AsyncOperationStreamingCancelledException))
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

		// Token: 0x06000B26 RID: 2854 RVA: 0x0002F404 File Offset: 0x0002D604
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("AsyncOperationStreamingCancelledException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("AsyncOperationStreamingCancelledException_AsyncOperationId", this.AsyncOperationId, typeof(Guid));
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0002F460 File Offset: 0x0002D660
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The async operation id {0} streaming was cancelled.", (markupKind == PrivateInformationMarkupKind.None) ? this.AsyncOperationId.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.AsyncOperationId.ToString() : this.AsyncOperationId.ToString()));
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0002F4C3 File Offset: 0x0002D6C3
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

		// Token: 0x06000B29 RID: 2857 RVA: 0x0002F4E0 File Offset: 0x0002D6E0
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0002F500 File Offset: 0x0002D700
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "AsyncOperationId={0}", this.AsyncOperationId.ToString()) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "AsyncOperationId={0}", this.AsyncOperationId.ToString()) : string.Format(CultureInfo.CurrentCulture, "AsyncOperationId={0}", this.AsyncOperationId.ToString())));
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0002F5AC File Offset: 0x0002D7AC
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0002F5B5 File Offset: 0x0002D7B5
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002F5BE File Offset: 0x0002D7BE
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0002F5C8 File Offset: 0x0002D7C8
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

		// Token: 0x06000B2F RID: 2863 RVA: 0x0002F78C File Offset: 0x0002D98C
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0002F798 File Offset: 0x0002D998
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

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002F81C File Offset: 0x0002DA1C
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x040002E9 RID: 745
		private string creationMessage;

		// Token: 0x040002EA RID: 746
		private Guid m_asyncOperationId;
	}
}
