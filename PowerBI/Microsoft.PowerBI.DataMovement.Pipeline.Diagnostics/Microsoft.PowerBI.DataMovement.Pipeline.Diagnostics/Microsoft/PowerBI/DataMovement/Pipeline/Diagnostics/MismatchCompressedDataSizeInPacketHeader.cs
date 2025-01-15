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
	// Token: 0x020000CB RID: 203
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class MismatchCompressedDataSizeInPacketHeader : GatewayPipelineException
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x0004426D File Offset: 0x0004246D
		// (set) Token: 0x06001032 RID: 4146 RVA: 0x00044275 File Offset: 0x00042475
		public string Reason
		{
			get
			{
				return this.m_reason;
			}
			protected set
			{
				this.m_reason = value;
			}
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0004427E File Offset: 0x0004247E
		public MismatchCompressedDataSizeInPacketHeader()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00044292 File Offset: 0x00042492
		public MismatchCompressedDataSizeInPacketHeader(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x000442A8 File Offset: 0x000424A8
		public MismatchCompressedDataSizeInPacketHeader(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x000442CB File Offset: 0x000424CB
		public MismatchCompressedDataSizeInPacketHeader(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x000442F0 File Offset: 0x000424F0
		protected MismatchCompressedDataSizeInPacketHeader(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("MismatchCompressedDataSizeInPacketHeader_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Reason = (string)info.GetValue("MismatchCompressedDataSizeInPacketHeader_Reason", typeof(string));
			}
			catch (SerializationException)
			{
				this.Reason = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0004438C File Offset: 0x0004258C
		public MismatchCompressedDataSizeInPacketHeader(string reason, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.Reason = reason;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x000443B6 File Offset: 0x000425B6
		public MismatchCompressedDataSizeInPacketHeader(string reason, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.Reason = reason;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x000443E2 File Offset: 0x000425E2
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_MismatchCompressedDataSizeInPacketHeaderError";
			}
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x000443F9 File Offset: 0x000425F9
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00044404 File Offset: 0x00042604
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(MismatchCompressedDataSizeInPacketHeader))
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

		// Token: 0x0600103D RID: 4157 RVA: 0x000444B8 File Offset: 0x000426B8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("MismatchCompressedDataSizeInPacketHeader_creationMessage", this.creationMessage, typeof(string));
			if (this.Reason != null)
			{
				info.AddValue("MismatchCompressedDataSizeInPacketHeader_Reason", this.Reason, typeof(string));
			}
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00044518 File Offset: 0x00042718
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The compressed data size in a packet header does not match actual size: {0}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Reason != null) ? this.Reason.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Reason != null) ? this.Reason.ToString() : string.Empty) : ((this.Reason != null) ? this.Reason.ToString() : string.Empty)));
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x0004458D File Offset: 0x0004278D
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

		// Token: 0x06001040 RID: 4160 RVA: 0x000445AA File Offset: 0x000427AA
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x000445C8 File Offset: 0x000427C8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Reason={0}", (this.Reason != null) ? this.Reason.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Reason={0}", (this.Reason != null) ? this.Reason.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Reason={0}", (this.Reason != null) ? this.Reason.ToString() : string.Empty)));
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00044686 File Offset: 0x00042886
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0004468F File Offset: 0x0004288F
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00044698 File Offset: 0x00042898
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x000446A4 File Offset: 0x000428A4
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

		// Token: 0x06001046 RID: 4166 RVA: 0x00044868 File Offset: 0x00042A68
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x00044874 File Offset: 0x00042A74
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

		// Token: 0x06001048 RID: 4168 RVA: 0x000448F8 File Offset: 0x00042AF8
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000340 RID: 832
		private string creationMessage;

		// Token: 0x04000341 RID: 833
		private string m_reason;
	}
}
