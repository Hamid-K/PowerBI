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
	// Token: 0x02000023 RID: 35
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class InvalidStateForConnectionStringChangeException : GatewayPipelineException
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00007285 File Offset: 0x00005485
		// (set) Token: 0x0600015A RID: 346 RVA: 0x0000728D File Offset: 0x0000548D
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

		// Token: 0x0600015B RID: 347 RVA: 0x00007296 File Offset: 0x00005496
		public InvalidStateForConnectionStringChangeException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000072AA File Offset: 0x000054AA
		public InvalidStateForConnectionStringChangeException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000072C0 File Offset: 0x000054C0
		public InvalidStateForConnectionStringChangeException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000072E3 File Offset: 0x000054E3
		public InvalidStateForConnectionStringChangeException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00007308 File Offset: 0x00005508
		protected InvalidStateForConnectionStringChangeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("InvalidStateForConnectionStringChangeException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ConnectionState = (string)info.GetValue("InvalidStateForConnectionStringChangeException_ConnectionState", typeof(string));
			}
			catch (SerializationException)
			{
				this.ConnectionState = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000073A4 File Offset: 0x000055A4
		public InvalidStateForConnectionStringChangeException(string connectionState, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConnectionState = connectionState;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000073CE File Offset: 0x000055CE
		public InvalidStateForConnectionStringChangeException(string connectionState, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConnectionState = connectionState;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000073FA File Offset: 0x000055FA
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_InvalidStateForConnectionStringChangeError";
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00007411 File Offset: 0x00005611
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000741C File Offset: 0x0000561C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(InvalidStateForConnectionStringChangeException))
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

		// Token: 0x06000165 RID: 357 RVA: 0x000074D0 File Offset: 0x000056D0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("InvalidStateForConnectionStringChangeException_creationMessage", this.creationMessage, typeof(string));
			if (this.ConnectionState != null)
			{
				info.AddValue("InvalidStateForConnectionStringChangeException_ConnectionState", this.ConnectionState, typeof(string));
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00007530 File Offset: 0x00005730
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The connection string cannot be changed if the connection is in state '{0}'. The connection must be in state 'Closed'.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ConnectionState != null) ? this.ConnectionState.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ConnectionState != null) ? this.ConnectionState.ToString() : string.Empty) : ((this.ConnectionState != null) ? this.ConnectionState.ToString() : string.Empty)));
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000075A5 File Offset: 0x000057A5
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

		// Token: 0x06000168 RID: 360 RVA: 0x000075C2 File Offset: 0x000057C2
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000075E0 File Offset: 0x000057E0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ConnectionState={0}", (this.ConnectionState != null) ? this.ConnectionState.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ConnectionState={0}", (this.ConnectionState != null) ? this.ConnectionState.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ConnectionState={0}", (this.ConnectionState != null) ? this.ConnectionState.ToString() : string.Empty)));
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000769E File Offset: 0x0000589E
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000076A7 File Offset: 0x000058A7
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000076B0 File Offset: 0x000058B0
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000076BC File Offset: 0x000058BC
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

		// Token: 0x0600016E RID: 366 RVA: 0x00007880 File Offset: 0x00005A80
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000788C File Offset: 0x00005A8C
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

		// Token: 0x06000170 RID: 368 RVA: 0x00007910 File Offset: 0x00005B10
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000241 RID: 577
		private string creationMessage;

		// Token: 0x04000242 RID: 578
		private string m_connectionState;
	}
}
