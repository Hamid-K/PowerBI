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
	// Token: 0x0200001E RID: 30
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class InvalidConnectionStateException : GatewayPipelineException
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004B31 File Offset: 0x00002D31
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00004B39 File Offset: 0x00002D39
		public bool IsNull
		{
			get
			{
				return this.m_isNull;
			}
			protected set
			{
				this.m_isNull = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004B42 File Offset: 0x00002D42
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00004B4A File Offset: 0x00002D4A
		public string ExpectedState
		{
			get
			{
				return this.m_expectedState;
			}
			protected set
			{
				this.m_expectedState = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004B53 File Offset: 0x00002D53
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00004B5B File Offset: 0x00002D5B
		public string ActualState
		{
			get
			{
				return this.m_actualState;
			}
			protected set
			{
				this.m_actualState = value;
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004B64 File Offset: 0x00002D64
		public InvalidConnectionStateException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidValueField<bool>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004B82 File Offset: 0x00002D82
		public InvalidConnectionStateException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004B98 File Offset: 0x00002D98
		public InvalidConnectionStateException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004BBB File Offset: 0x00002DBB
		public InvalidConnectionStateException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004BE0 File Offset: 0x00002DE0
		protected InvalidConnectionStateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("InvalidConnectionStateException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.IsNull = (bool)info.GetValue("InvalidConnectionStateException_IsNull", typeof(bool));
			try
			{
				this.ExpectedState = (string)info.GetValue("InvalidConnectionStateException_ExpectedState", typeof(string));
			}
			catch (SerializationException)
			{
				this.ExpectedState = null;
			}
			try
			{
				this.ActualState = (string)info.GetValue("InvalidConnectionStateException_ActualState", typeof(string));
			}
			catch (SerializationException)
			{
				this.ActualState = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004CD4 File Offset: 0x00002ED4
		public InvalidConnectionStateException(bool isNull, string expectedState, string actualState, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.IsNull = isNull;
			this.ExpectedState = expectedState;
			this.ActualState = actualState;
			this.ConstructorInternal(false);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004D00 File Offset: 0x00002F00
		public InvalidConnectionStateException(bool isNull, string expectedState, string actualState, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.IsNull = isNull;
			this.ExpectedState = expectedState;
			this.ActualState = actualState;
			this.ConstructorInternal(false);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004D3B File Offset: 0x00002F3B
		public InvalidConnectionStateException(bool isNull, string expectedState, string actualState, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.IsNull = isNull;
			this.ExpectedState = expectedState;
			this.ActualState = actualState;
			this.ConstructorInternal(false);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004D78 File Offset: 0x00002F78
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_InvalidConnectionStateError";
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004D8F File Offset: 0x00002F8F
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004D98 File Offset: 0x00002F98
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(InvalidConnectionStateException))
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

		// Token: 0x060000E9 RID: 233 RVA: 0x00004E4C File Offset: 0x0000304C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("InvalidConnectionStateException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("InvalidConnectionStateException_IsNull", this.IsNull, typeof(bool));
			if (this.ExpectedState != null)
			{
				info.AddValue("InvalidConnectionStateException_ExpectedState", this.ExpectedState, typeof(string));
			}
			if (this.ActualState != null)
			{
				info.AddValue("InvalidConnectionStateException_ActualState", this.ActualState, typeof(string));
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004EF0 File Offset: 0x000030F0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The connection is either null or not in the expected state. IsNull: {0}, ExpectedState: {1}, ActualState: {2}", (markupKind == PrivateInformationMarkupKind.None) ? this.IsNull.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.IsNull.ToString() : this.IsNull.ToString()), (markupKind == PrivateInformationMarkupKind.None) ? ((this.ExpectedState != null) ? this.ExpectedState.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ExpectedState != null) ? this.ExpectedState.ToString() : string.Empty) : ((this.ExpectedState != null) ? this.ExpectedState.ToString() : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.ActualState != null) ? this.ActualState.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ActualState != null) ? this.ActualState.ToString() : string.Empty) : ((this.ActualState != null) ? this.ActualState.ToString() : string.Empty)));
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004FF3 File Offset: 0x000031F3
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

		// Token: 0x060000EC RID: 236 RVA: 0x00005010 File Offset: 0x00003210
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005030 File Offset: 0x00003230
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "IsNull={0}", this.IsNull.ToString()) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "IsNull={0}", this.IsNull.ToString()) : string.Format(CultureInfo.CurrentCulture, "IsNull={0}", this.IsNull.ToString())));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ExpectedState={0}", (this.ExpectedState != null) ? this.ExpectedState.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ExpectedState={0}", (this.ExpectedState != null) ? this.ExpectedState.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ExpectedState={0}", (this.ExpectedState != null) ? this.ExpectedState.ToString() : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ActualState={0}", (this.ActualState != null) ? this.ActualState.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ActualState={0}", (this.ActualState != null) ? this.ActualState.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ActualState={0}", (this.ActualState != null) ? this.ActualState.ToString() : string.Empty)));
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000520E File Offset: 0x0000340E
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005217 File Offset: 0x00003417
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005220 File Offset: 0x00003420
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000522C File Offset: 0x0000342C
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

		// Token: 0x060000F2 RID: 242 RVA: 0x000053F0 File Offset: 0x000035F0
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000053FC File Offset: 0x000035FC
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

		// Token: 0x060000F4 RID: 244 RVA: 0x00005480 File Offset: 0x00003680
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000232 RID: 562
		private string creationMessage;

		// Token: 0x04000233 RID: 563
		private bool m_isNull;

		// Token: 0x04000234 RID: 564
		private string m_expectedState;

		// Token: 0x04000235 RID: 565
		private string m_actualState;
	}
}
