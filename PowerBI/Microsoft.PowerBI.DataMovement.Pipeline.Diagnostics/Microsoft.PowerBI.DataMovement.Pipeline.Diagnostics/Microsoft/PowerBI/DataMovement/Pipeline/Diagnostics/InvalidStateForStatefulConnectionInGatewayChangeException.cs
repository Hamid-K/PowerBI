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
	// Token: 0x02000024 RID: 36
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class InvalidStateForStatefulConnectionInGatewayChangeException : GatewayPipelineException
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00007941 File Offset: 0x00005B41
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00007949 File Offset: 0x00005B49
		public string ConnectionState
		{
			get
			{
				return this.m_connectionState;
			}
			protected set
			{
				this.m_connectionState = value;
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00007952 File Offset: 0x00005B52
		public InvalidStateForStatefulConnectionInGatewayChangeException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007966 File Offset: 0x00005B66
		public InvalidStateForStatefulConnectionInGatewayChangeException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000797C File Offset: 0x00005B7C
		public InvalidStateForStatefulConnectionInGatewayChangeException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000799F File Offset: 0x00005B9F
		public InvalidStateForStatefulConnectionInGatewayChangeException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000079C4 File Offset: 0x00005BC4
		protected InvalidStateForStatefulConnectionInGatewayChangeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("InvalidStateForStatefulConnectionInGatewayChangeException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ConnectionState = (string)info.GetValue("InvalidStateForStatefulConnectionInGatewayChangeException_ConnectionState", typeof(string));
			}
			catch (SerializationException)
			{
				this.ConnectionState = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00007A60 File Offset: 0x00005C60
		public InvalidStateForStatefulConnectionInGatewayChangeException(string connectionState, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConnectionState = connectionState;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007A8A File Offset: 0x00005C8A
		public InvalidStateForStatefulConnectionInGatewayChangeException(string connectionState, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConnectionState = connectionState;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007AB6 File Offset: 0x00005CB6
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_InvalidStateForStatefulConnectionInGatewayChangeError";
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007ACD File Offset: 0x00005CCD
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007AD8 File Offset: 0x00005CD8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(InvalidStateForStatefulConnectionInGatewayChangeException))
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

		// Token: 0x0600017D RID: 381 RVA: 0x00007B8C File Offset: 0x00005D8C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("InvalidStateForStatefulConnectionInGatewayChangeException_creationMessage", this.creationMessage, typeof(string));
			if (this.ConnectionState != null)
			{
				info.AddValue("InvalidStateForStatefulConnectionInGatewayChangeException_ConnectionState", this.ConnectionState, typeof(string));
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007BEC File Offset: 0x00005DEC
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Stateful connection in gateway cannot be changed if the connection is in state '{0}'. The connection must be in state 'Closed'.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ConnectionState != null) ? this.ConnectionState.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ConnectionState != null) ? this.ConnectionState.ToString() : string.Empty) : ((this.ConnectionState != null) ? this.ConnectionState.ToString() : string.Empty)));
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00007C61 File Offset: 0x00005E61
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

		// Token: 0x06000180 RID: 384 RVA: 0x00007C7E File Offset: 0x00005E7E
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007C9C File Offset: 0x00005E9C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ConnectionState={0}", (this.ConnectionState != null) ? this.ConnectionState.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ConnectionState={0}", (this.ConnectionState != null) ? this.ConnectionState.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ConnectionState={0}", (this.ConnectionState != null) ? this.ConnectionState.ToString() : string.Empty)));
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007D5A File Offset: 0x00005F5A
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007D63 File Offset: 0x00005F63
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00007D6C File Offset: 0x00005F6C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007D78 File Offset: 0x00005F78
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

		// Token: 0x06000186 RID: 390 RVA: 0x00007F3C File Offset: 0x0000613C
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007F48 File Offset: 0x00006148
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

		// Token: 0x06000188 RID: 392 RVA: 0x00007FCC File Offset: 0x000061CC
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000243 RID: 579
		private string creationMessage;

		// Token: 0x04000244 RID: 580
		private string m_connectionState;
	}
}
