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
	// Token: 0x020000C5 RID: 197
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class GatewayUnreachableException : GatewayPipelineException
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x00041DC1 File Offset: 0x0003FFC1
		// (set) Token: 0x06000FA8 RID: 4008 RVA: 0x00041DC9 File Offset: 0x0003FFC9
		public string ServiceBusEndpoint
		{
			get
			{
				return this.m_serviceBusEndpoint;
			}
			protected set
			{
				this.m_serviceBusEndpoint = value;
			}
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00041DD2 File Offset: 0x0003FFD2
		public GatewayUnreachableException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00041DE6 File Offset: 0x0003FFE6
		public GatewayUnreachableException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00041DFC File Offset: 0x0003FFFC
		public GatewayUnreachableException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x00041E1F File Offset: 0x0004001F
		public GatewayUnreachableException(string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x00041E48 File Offset: 0x00040048
		protected GatewayUnreachableException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("GatewayUnreachableException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ServiceBusEndpoint = (string)info.GetValue("GatewayUnreachableException_ServiceBusEndpoint", typeof(string));
			}
			catch (SerializationException)
			{
				this.ServiceBusEndpoint = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x00041EE4 File Offset: 0x000400E4
		public GatewayUnreachableException(string serviceBusEndpoint, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ServiceBusEndpoint = serviceBusEndpoint;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00041F0E File Offset: 0x0004010E
		public GatewayUnreachableException(string serviceBusEndpoint, string message, Exception innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, GatewayExceptionUtils.InnerExceptionCreator.GetInnerException(innerException), Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ServiceBusEndpoint = serviceBusEndpoint;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x00041F3F File Offset: 0x0004013F
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Client_GatewayUnreachable";
			}
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00041F56 File Offset: 0x00040156
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00041F5E File Offset: 0x0004015E
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x00041F64 File Offset: 0x00040164
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(GatewayUnreachableException))
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

		// Token: 0x06000FB4 RID: 4020 RVA: 0x00042018 File Offset: 0x00040218
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("GatewayUnreachableException_creationMessage", this.creationMessage, typeof(string));
			if (this.ServiceBusEndpoint != null)
			{
				info.AddValue("GatewayUnreachableException_ServiceBusEndpoint", this.ServiceBusEndpoint, typeof(string));
			}
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00042078 File Offset: 0x00040278
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Gateway on endpoint '{0}' is unreachable.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ServiceBusEndpoint != null) ? this.ServiceBusEndpoint.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ServiceBusEndpoint != null) ? this.ServiceBusEndpoint.ToString() : string.Empty) : ((this.ServiceBusEndpoint != null) ? this.ServiceBusEndpoint.ToString() : string.Empty)));
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x000420ED File Offset: 0x000402ED
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

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0004210A File Offset: 0x0004030A
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00042128 File Offset: 0x00040328
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ServiceBusEndpoint={0}", (this.ServiceBusEndpoint != null) ? this.ServiceBusEndpoint.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ServiceBusEndpoint={0}", (this.ServiceBusEndpoint != null) ? this.ServiceBusEndpoint.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ServiceBusEndpoint={0}", (this.ServiceBusEndpoint != null) ? this.ServiceBusEndpoint.ToString() : string.Empty)));
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x000421E6 File Offset: 0x000403E6
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x000421EF File Offset: 0x000403EF
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x000421F8 File Offset: 0x000403F8
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00042204 File Offset: 0x00040404
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

		// Token: 0x06000FBD RID: 4029 RVA: 0x000423C8 File Offset: 0x000405C8
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x000423D4 File Offset: 0x000405D4
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

		// Token: 0x06000FBF RID: 4031 RVA: 0x00042458 File Offset: 0x00040658
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000336 RID: 822
		private string creationMessage;

		// Token: 0x04000337 RID: 823
		private string m_serviceBusEndpoint;
	}
}
