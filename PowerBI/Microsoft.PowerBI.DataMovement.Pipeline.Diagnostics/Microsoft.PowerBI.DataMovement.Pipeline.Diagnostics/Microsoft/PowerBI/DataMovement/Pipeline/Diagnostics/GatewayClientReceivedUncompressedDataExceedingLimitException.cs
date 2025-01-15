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
	// Token: 0x02000039 RID: 57
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class GatewayClientReceivedUncompressedDataExceedingLimitException : GatewayPipelineException
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000F7A9 File Offset: 0x0000D9A9
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0000F7B1 File Offset: 0x0000D9B1
		public int UncompressedDataLimitInMB
		{
			get
			{
				return this.m_uncompressedDataLimitInMB;
			}
			protected set
			{
				this.m_uncompressedDataLimitInMB = value;
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000F7BA File Offset: 0x0000D9BA
		public GatewayClientReceivedUncompressedDataExceedingLimitException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidValueField<int>();
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000F7CE File Offset: 0x0000D9CE
		public GatewayClientReceivedUncompressedDataExceedingLimitException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000F7E4 File Offset: 0x0000D9E4
		public GatewayClientReceivedUncompressedDataExceedingLimitException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000F807 File Offset: 0x0000DA07
		public GatewayClientReceivedUncompressedDataExceedingLimitException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000F82C File Offset: 0x0000DA2C
		protected GatewayClientReceivedUncompressedDataExceedingLimitException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("GatewayClientReceivedUncompressedDataExceedingLimitException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.UncompressedDataLimitInMB = (int)info.GetValue("GatewayClientReceivedUncompressedDataExceedingLimitException_UncompressedDataLimitInMB", typeof(int));
			this.ConstructorInternal(true);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000F8B0 File Offset: 0x0000DAB0
		public GatewayClientReceivedUncompressedDataExceedingLimitException(int uncompressedDataLimitInMB, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.UncompressedDataLimitInMB = uncompressedDataLimitInMB;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000F8CD File Offset: 0x0000DACD
		public GatewayClientReceivedUncompressedDataExceedingLimitException(int uncompressedDataLimitInMB, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.UncompressedDataLimitInMB = uncompressedDataLimitInMB;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000F8F7 File Offset: 0x0000DAF7
		public GatewayClientReceivedUncompressedDataExceedingLimitException(int uncompressedDataLimitInMB, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.UncompressedDataLimitInMB = uncompressedDataLimitInMB;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000F923 File Offset: 0x0000DB23
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Client_ReceivedUncompressedDataExceedingLimit";
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000F93A File Offset: 0x0000DB3A
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000F942 File Offset: 0x0000DB42
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000F948 File Offset: 0x0000DB48
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(GatewayClientReceivedUncompressedDataExceedingLimitException))
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

		// Token: 0x06000360 RID: 864 RVA: 0x0000F9FC File Offset: 0x0000DBFC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("GatewayClientReceivedUncompressedDataExceedingLimitException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("GatewayClientReceivedUncompressedDataExceedingLimitException_UncompressedDataLimitInMB", this.UncompressedDataLimitInMB, typeof(int));
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000FA58 File Offset: 0x0000DC58
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Uncompressed data received by gateway client exceedes limit '{0}' MB.", (markupKind == PrivateInformationMarkupKind.None) ? this.UncompressedDataLimitInMB.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.UncompressedDataLimitInMB.ToString(CultureInfo.InvariantCulture) : this.UncompressedDataLimitInMB.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000FAB8 File Offset: 0x0000DCB8
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

		// Token: 0x06000363 RID: 867 RVA: 0x0000FAD5 File Offset: 0x0000DCD5
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000FAF4 File Offset: 0x0000DCF4
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "UncompressedDataLimitInMB={0}", this.UncompressedDataLimitInMB.ToString(CultureInfo.InvariantCulture)) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "UncompressedDataLimitInMB={0}", this.UncompressedDataLimitInMB.ToString(CultureInfo.InvariantCulture)) : string.Format(CultureInfo.CurrentCulture, "UncompressedDataLimitInMB={0}", this.UncompressedDataLimitInMB.ToString(CultureInfo.InvariantCulture))));
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000FB9D File Offset: 0x0000DD9D
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000FBA6 File Offset: 0x0000DDA6
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000FBAF File Offset: 0x0000DDAF
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000FBB8 File Offset: 0x0000DDB8
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

		// Token: 0x06000369 RID: 873 RVA: 0x0000FD7C File Offset: 0x0000DF7C
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000FD88 File Offset: 0x0000DF88
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

		// Token: 0x0600036B RID: 875 RVA: 0x0000FE0C File Offset: 0x0000E00C
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000265 RID: 613
		private string creationMessage;

		// Token: 0x04000266 RID: 614
		private int m_uncompressedDataLimitInMB;
	}
}
