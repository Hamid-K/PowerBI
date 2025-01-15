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
	// Token: 0x0200003B RID: 59
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class GatewayClientMaxPacketLimitReachedException : GatewayPipelineException
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000386 RID: 902 RVA: 0x000104D1 File Offset: 0x0000E6D1
		// (set) Token: 0x06000387 RID: 903 RVA: 0x000104D9 File Offset: 0x0000E6D9
		public int MaxPacketLimitReachedCount
		{
			get
			{
				return this.m_maxPacketLimitReachedCount;
			}
			protected set
			{
				this.m_maxPacketLimitReachedCount = value;
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000104E2 File Offset: 0x0000E6E2
		public GatewayClientMaxPacketLimitReachedException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidValueField<int>();
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000104F6 File Offset: 0x0000E6F6
		public GatewayClientMaxPacketLimitReachedException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001050C File Offset: 0x0000E70C
		public GatewayClientMaxPacketLimitReachedException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001052F File Offset: 0x0000E72F
		public GatewayClientMaxPacketLimitReachedException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00010554 File Offset: 0x0000E754
		protected GatewayClientMaxPacketLimitReachedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("GatewayClientMaxPacketLimitReachedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.MaxPacketLimitReachedCount = (int)info.GetValue("GatewayClientMaxPacketLimitReachedException_MaxPacketLimitReachedCount", typeof(int));
			this.ConstructorInternal(true);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000105D8 File Offset: 0x0000E7D8
		public GatewayClientMaxPacketLimitReachedException(int maxPacketLimitReachedCount, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.MaxPacketLimitReachedCount = maxPacketLimitReachedCount;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000105F5 File Offset: 0x0000E7F5
		public GatewayClientMaxPacketLimitReachedException(int maxPacketLimitReachedCount, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.MaxPacketLimitReachedCount = maxPacketLimitReachedCount;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001061F File Offset: 0x0000E81F
		public GatewayClientMaxPacketLimitReachedException(int maxPacketLimitReachedCount, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.MaxPacketLimitReachedCount = maxPacketLimitReachedCount;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001064B File Offset: 0x0000E84B
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Client_MaxPacketLimitReached";
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00010662 File Offset: 0x0000E862
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001066A File Offset: 0x0000E86A
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00010670 File Offset: 0x0000E870
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(GatewayClientMaxPacketLimitReachedException))
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

		// Token: 0x06000394 RID: 916 RVA: 0x00010724 File Offset: 0x0000E924
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("GatewayClientMaxPacketLimitReachedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("GatewayClientMaxPacketLimitReachedException_MaxPacketLimitReachedCount", this.MaxPacketLimitReachedCount, typeof(int));
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00010780 File Offset: 0x0000E980
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "packet reached the maximum limit = '{0}' at gateway client.", (markupKind == PrivateInformationMarkupKind.None) ? this.MaxPacketLimitReachedCount.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.MaxPacketLimitReachedCount.ToString(CultureInfo.InvariantCulture) : this.MaxPacketLimitReachedCount.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000396 RID: 918 RVA: 0x000107E0 File Offset: 0x0000E9E0
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

		// Token: 0x06000397 RID: 919 RVA: 0x000107FD File Offset: 0x0000E9FD
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001081C File Offset: 0x0000EA1C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "MaxPacketLimitReachedCount={0}", this.MaxPacketLimitReachedCount.ToString(CultureInfo.InvariantCulture)) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "MaxPacketLimitReachedCount={0}", this.MaxPacketLimitReachedCount.ToString(CultureInfo.InvariantCulture)) : string.Format(CultureInfo.CurrentCulture, "MaxPacketLimitReachedCount={0}", this.MaxPacketLimitReachedCount.ToString(CultureInfo.InvariantCulture))));
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000108C5 File Offset: 0x0000EAC5
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000108CE File Offset: 0x0000EACE
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000108D7 File Offset: 0x0000EAD7
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000108E0 File Offset: 0x0000EAE0
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

		// Token: 0x0600039D RID: 925 RVA: 0x00010AA4 File Offset: 0x0000ECA4
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00010AB0 File Offset: 0x0000ECB0
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

		// Token: 0x0600039F RID: 927 RVA: 0x00010B34 File Offset: 0x0000ED34
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000269 RID: 617
		private string creationMessage;

		// Token: 0x0400026A RID: 618
		private int m_maxPacketLimitReachedCount;
	}
}
