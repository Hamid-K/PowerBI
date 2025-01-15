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
	// Token: 0x02000093 RID: 147
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class InvalidAsyncPacketAcknowledgementException : GatewayPipelineException
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x0002FEE5 File Offset: 0x0002E0E5
		// (set) Token: 0x06000B4C RID: 2892 RVA: 0x0002FEED File Offset: 0x0002E0ED
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

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x0002FEF6 File Offset: 0x0002E0F6
		// (set) Token: 0x06000B4E RID: 2894 RVA: 0x0002FEFE File Offset: 0x0002E0FE
		public int PacketIndex
		{
			get
			{
				return this.m_packetIndex;
			}
			protected set
			{
				this.m_packetIndex = value;
			}
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0002FF07 File Offset: 0x0002E107
		public InvalidAsyncPacketAcknowledgementException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidValueField<Guid>();
			GatewayExceptionUtils.CompileCheck.IsValidValueField<int>();
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0002FF20 File Offset: 0x0002E120
		public InvalidAsyncPacketAcknowledgementException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0002FF36 File Offset: 0x0002E136
		public InvalidAsyncPacketAcknowledgementException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0002FF59 File Offset: 0x0002E159
		public InvalidAsyncPacketAcknowledgementException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0002FF80 File Offset: 0x0002E180
		protected InvalidAsyncPacketAcknowledgementException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("InvalidAsyncPacketAcknowledgementException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.AsyncOperationId = (Guid)info.GetValue("InvalidAsyncPacketAcknowledgementException_AsyncOperationId", typeof(Guid));
			this.PacketIndex = (int)info.GetValue("InvalidAsyncPacketAcknowledgementException_PacketIndex", typeof(int));
			this.ConstructorInternal(true);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00030024 File Offset: 0x0002E224
		public InvalidAsyncPacketAcknowledgementException(Guid asyncOperationId, int packetIndex, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.AsyncOperationId = asyncOperationId;
			this.PacketIndex = packetIndex;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00030048 File Offset: 0x0002E248
		public InvalidAsyncPacketAcknowledgementException(Guid asyncOperationId, int packetIndex, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.AsyncOperationId = asyncOperationId;
			this.PacketIndex = packetIndex;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0003007A File Offset: 0x0002E27A
		public InvalidAsyncPacketAcknowledgementException(Guid asyncOperationId, int packetIndex, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.AsyncOperationId = asyncOperationId;
			this.PacketIndex = packetIndex;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x000300AE File Offset: 0x0002E2AE
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_InvalidAsyncPacketAcknowledgementError";
			}
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x000300C5 File Offset: 0x0002E2C5
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x000300D0 File Offset: 0x0002E2D0
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(InvalidAsyncPacketAcknowledgementException))
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

		// Token: 0x06000B5A RID: 2906 RVA: 0x00030184 File Offset: 0x0002E384
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("InvalidAsyncPacketAcknowledgementException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("InvalidAsyncPacketAcknowledgementException_AsyncOperationId", this.AsyncOperationId, typeof(Guid));
			info.AddValue("InvalidAsyncPacketAcknowledgementException_PacketIndex", this.PacketIndex, typeof(int));
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00030200 File Offset: 0x0002E400
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Packet index {0} cannot be acknowledged for async operation {1}.", (markupKind == PrivateInformationMarkupKind.None) ? this.PacketIndex.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.PacketIndex.ToString(CultureInfo.InvariantCulture) : this.PacketIndex.ToString(CultureInfo.InvariantCulture)), (markupKind == PrivateInformationMarkupKind.None) ? this.AsyncOperationId.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.AsyncOperationId.ToString() : this.AsyncOperationId.ToString()));
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x000302A7 File Offset: 0x0002E4A7
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

		// Token: 0x06000B5D RID: 2909 RVA: 0x000302C4 File Offset: 0x0002E4C4
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x000302E4 File Offset: 0x0002E4E4
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "AsyncOperationId={0}", this.AsyncOperationId.ToString()) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "AsyncOperationId={0}", this.AsyncOperationId.ToString()) : string.Format(CultureInfo.CurrentCulture, "AsyncOperationId={0}", this.AsyncOperationId.ToString())));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "PacketIndex={0}", this.PacketIndex.ToString(CultureInfo.InvariantCulture)) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "PacketIndex={0}", this.PacketIndex.ToString(CultureInfo.InvariantCulture)) : string.Format(CultureInfo.CurrentCulture, "PacketIndex={0}", this.PacketIndex.ToString(CultureInfo.InvariantCulture))));
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0003041D File Offset: 0x0002E61D
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00030426 File Offset: 0x0002E626
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0003042F File Offset: 0x0002E62F
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00030438 File Offset: 0x0002E638
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

		// Token: 0x06000B63 RID: 2915 RVA: 0x000305FC File Offset: 0x0002E7FC
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00030608 File Offset: 0x0002E808
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

		// Token: 0x06000B65 RID: 2917 RVA: 0x0003068C File Offset: 0x0002E88C
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x040002ED RID: 749
		private string creationMessage;

		// Token: 0x040002EE RID: 750
		private Guid m_asyncOperationId;

		// Token: 0x040002EF RID: 751
		private int m_packetIndex;
	}
}
