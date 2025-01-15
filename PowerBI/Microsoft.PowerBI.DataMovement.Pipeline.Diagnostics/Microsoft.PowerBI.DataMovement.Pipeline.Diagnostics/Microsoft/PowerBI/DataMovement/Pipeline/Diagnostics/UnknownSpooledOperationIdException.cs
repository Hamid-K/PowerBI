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
	// Token: 0x0200008C RID: 140
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class UnknownSpooledOperationIdException : GatewayPipelineException
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0002CEF9 File Offset: 0x0002B0F9
		// (set) Token: 0x06000A9A RID: 2714 RVA: 0x0002CF01 File Offset: 0x0002B101
		public Guid SpooledOperationId
		{
			get
			{
				return this.m_spooledOperationId;
			}
			protected set
			{
				this.m_spooledOperationId = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0002CF0A File Offset: 0x0002B10A
		// (set) Token: 0x06000A9C RID: 2716 RVA: 0x0002CF12 File Offset: 0x0002B112
		public int? SpooledOperationStatus
		{
			get
			{
				return this.m_spooledOperationStatus;
			}
			protected set
			{
				this.m_spooledOperationStatus = value;
			}
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002CF1B File Offset: 0x0002B11B
		public UnknownSpooledOperationIdException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidValueField<Guid>();
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0002CF2F File Offset: 0x0002B12F
		public UnknownSpooledOperationIdException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0002CF45 File Offset: 0x0002B145
		public UnknownSpooledOperationIdException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0002CF68 File Offset: 0x0002B168
		public UnknownSpooledOperationIdException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0002CF8C File Offset: 0x0002B18C
		protected UnknownSpooledOperationIdException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("UnknownSpooledOperationIdException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.SpooledOperationId = (Guid)info.GetValue("UnknownSpooledOperationIdException_SpooledOperationId", typeof(Guid));
			try
			{
				this.SpooledOperationStatus = (int?)info.GetValue("UnknownSpooledOperationIdException_SpooledOperationStatus", typeof(int?));
			}
			catch (SerializationException)
			{
				this.SpooledOperationStatus = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002D050 File Offset: 0x0002B250
		public UnknownSpooledOperationIdException(Guid spooledOperationId, int? spooledOperationStatus, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.SpooledOperationId = spooledOperationId;
			this.SpooledOperationStatus = spooledOperationStatus;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0002D074 File Offset: 0x0002B274
		public UnknownSpooledOperationIdException(Guid spooledOperationId, int? spooledOperationStatus, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.SpooledOperationId = spooledOperationId;
			this.SpooledOperationStatus = spooledOperationStatus;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002D0A6 File Offset: 0x0002B2A6
		public UnknownSpooledOperationIdException(Guid spooledOperationId, int? spooledOperationStatus, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.SpooledOperationId = spooledOperationId;
			this.SpooledOperationStatus = spooledOperationStatus;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0002D0DA File Offset: 0x0002B2DA
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_SpooledOperationMissing";
			}
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002D0F1 File Offset: 0x0002B2F1
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002D0F9 File Offset: 0x0002B2F9
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002D0FC File Offset: 0x0002B2FC
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(UnknownSpooledOperationIdException))
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

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0002D210 File Offset: 0x0002B410
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("UnknownSpooledOperationIdException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("UnknownSpooledOperationIdException_SpooledOperationId", this.SpooledOperationId, typeof(Guid));
			info.AddValue("UnknownSpooledOperationIdException_SpooledOperationStatus", this.SpooledOperationStatus, typeof(int?));
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0002D28C File Offset: 0x0002B48C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The spooled operation id {0} does not exist. Evict code: {1}.", (markupKind == PrivateInformationMarkupKind.None) ? this.SpooledOperationId.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.SpooledOperationId.ToString() : this.SpooledOperationId.ToString()), (markupKind == PrivateInformationMarkupKind.None) ? this.SpooledOperationStatus.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.SpooledOperationStatus.ToString() : this.SpooledOperationStatus.ToString()));
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x0002D336 File Offset: 0x0002B536
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

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002D353 File Offset: 0x0002B553
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002D370 File Offset: 0x0002B570
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "SpooledOperationId={0}", this.SpooledOperationId.ToString()) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "SpooledOperationId={0}", this.SpooledOperationId.ToString()) : string.Format(CultureInfo.CurrentCulture, "SpooledOperationId={0}", this.SpooledOperationId.ToString())));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "SpooledOperationStatus={0}", this.SpooledOperationStatus.ToString()) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "SpooledOperationStatus={0}", this.SpooledOperationStatus.ToString()) : string.Format(CultureInfo.CurrentCulture, "SpooledOperationStatus={0}", this.SpooledOperationStatus.ToString())));
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002D4AC File Offset: 0x0002B6AC
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002D4B5 File Offset: 0x0002B6B5
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002D4BE File Offset: 0x0002B6BE
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002D4C8 File Offset: 0x0002B6C8
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

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0002D68C File Offset: 0x0002B88C
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0002D698 File Offset: 0x0002B898
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

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002D71C File Offset: 0x0002B91C
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x040002DE RID: 734
		private string creationMessage;

		// Token: 0x040002DF RID: 735
		private Guid m_spooledOperationId;

		// Token: 0x040002E0 RID: 736
		private int? m_spooledOperationStatus;
	}
}
