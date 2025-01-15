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
	// Token: 0x02000094 RID: 148
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class PacketsNotReadyToStreamException : GatewayPipelineException
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x000306BD File Offset: 0x0002E8BD
		// (set) Token: 0x06000B67 RID: 2919 RVA: 0x000306C5 File Offset: 0x0002E8C5
		public int NextPacketIndex
		{
			get
			{
				return this.m_nextPacketIndex;
			}
			protected set
			{
				this.m_nextPacketIndex = value;
			}
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x000306CE File Offset: 0x0002E8CE
		public PacketsNotReadyToStreamException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidValueField<int>();
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x000306E2 File Offset: 0x0002E8E2
		public PacketsNotReadyToStreamException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x000306F8 File Offset: 0x0002E8F8
		public PacketsNotReadyToStreamException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0003071B File Offset: 0x0002E91B
		public PacketsNotReadyToStreamException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00030740 File Offset: 0x0002E940
		protected PacketsNotReadyToStreamException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("PacketsNotReadyToStreamException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.NextPacketIndex = (int)info.GetValue("PacketsNotReadyToStreamException_NextPacketIndex", typeof(int));
			this.ConstructorInternal(true);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x000307C4 File Offset: 0x0002E9C4
		public PacketsNotReadyToStreamException(int nextPacketIndex, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.NextPacketIndex = nextPacketIndex;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x000307E1 File Offset: 0x0002E9E1
		public PacketsNotReadyToStreamException(int nextPacketIndex, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.NextPacketIndex = nextPacketIndex;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0003080B File Offset: 0x0002EA0B
		public PacketsNotReadyToStreamException(int nextPacketIndex, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.NextPacketIndex = nextPacketIndex;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00030837 File Offset: 0x0002EA37
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_PacketsNotReadyToStreamError";
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0003084E File Offset: 0x0002EA4E
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00030858 File Offset: 0x0002EA58
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(PacketsNotReadyToStreamException))
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

		// Token: 0x06000B73 RID: 2931 RVA: 0x0003090C File Offset: 0x0002EB0C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("PacketsNotReadyToStreamException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("PacketsNotReadyToStreamException_NextPacketIndex", this.NextPacketIndex, typeof(int));
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00030967 File Offset: 0x0002EB67
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "No more packets can be streamed at this time.", Array.Empty<object>());
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0003097D File Offset: 0x0002EB7D
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

		// Token: 0x06000B76 RID: 2934 RVA: 0x0003099A File Offset: 0x0002EB9A
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x000309B8 File Offset: 0x0002EBB8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "NextPacketIndex={0}", this.NextPacketIndex.ToString(CultureInfo.InvariantCulture)) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "NextPacketIndex={0}", this.NextPacketIndex.ToString(CultureInfo.InvariantCulture)) : string.Format(CultureInfo.CurrentCulture, "NextPacketIndex={0}", this.NextPacketIndex.ToString(CultureInfo.InvariantCulture))));
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00030A61 File Offset: 0x0002EC61
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00030A6A File Offset: 0x0002EC6A
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00030A73 File Offset: 0x0002EC73
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00030A7C File Offset: 0x0002EC7C
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

		// Token: 0x06000B7C RID: 2940 RVA: 0x00030C40 File Offset: 0x0002EE40
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00030C4C File Offset: 0x0002EE4C
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

		// Token: 0x06000B7E RID: 2942 RVA: 0x00030CD0 File Offset: 0x0002EED0
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x040002F0 RID: 752
		private string creationMessage;

		// Token: 0x040002F1 RID: 753
		private int m_nextPacketIndex;
	}
}
