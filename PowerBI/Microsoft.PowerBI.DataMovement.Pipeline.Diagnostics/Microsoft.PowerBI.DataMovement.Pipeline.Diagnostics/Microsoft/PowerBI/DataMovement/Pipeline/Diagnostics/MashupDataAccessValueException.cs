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
	// Token: 0x0200008A RID: 138
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class MashupDataAccessValueException : MashupDataAccessException
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0002BF35 File Offset: 0x0002A135
		// (set) Token: 0x06000A65 RID: 2661 RVA: 0x0002BF3D File Offset: 0x0002A13D
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

		// Token: 0x06000A66 RID: 2662 RVA: 0x0002BF46 File Offset: 0x0002A146
		public MashupDataAccessValueException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0002BF5A File Offset: 0x0002A15A
		public MashupDataAccessValueException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0002BF70 File Offset: 0x0002A170
		public MashupDataAccessValueException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0002BF93 File Offset: 0x0002A193
		public MashupDataAccessValueException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0002BFB8 File Offset: 0x0002A1B8
		protected MashupDataAccessValueException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("MashupDataAccessValueException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Reason = (string)info.GetValue("MashupDataAccessValueException_Reason", typeof(string));
			}
			catch (SerializationException)
			{
				this.Reason = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0002C054 File Offset: 0x0002A254
		public MashupDataAccessValueException(string reason, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.Reason = reason;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0002C07E File Offset: 0x0002A27E
		public MashupDataAccessValueException(string reason, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.Reason = reason;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0002C0AA File Offset: 0x0002A2AA
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002C0B3 File Offset: 0x0002A2B3
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0002C0BB File Offset: 0x0002A2BB
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002C0C0 File Offset: 0x0002A2C0
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(MashupDataAccessValueException))
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

		// Token: 0x06000A71 RID: 2673 RVA: 0x0002C1D4 File Offset: 0x0002A3D4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("MashupDataAccessValueException_creationMessage", this.creationMessage, typeof(string));
			if (this.Reason != null)
			{
				info.AddValue("MashupDataAccessValueException_Reason", this.Reason, typeof(string));
			}
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0002C234 File Offset: 0x0002A434
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Mashup expression evaluation error. Reason: {0}.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Reason != null) ? this.Reason.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Reason != null) ? this.Reason.ToString() : string.Empty) : ((this.Reason != null) ? this.Reason.ToString() : string.Empty)));
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0002C2A9 File Offset: 0x0002A4A9
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

		// Token: 0x06000A74 RID: 2676 RVA: 0x0002C2C6 File Offset: 0x0002A4C6
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0002C2E4 File Offset: 0x0002A4E4
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Reason={0}", (this.Reason != null) ? this.Reason.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Reason={0}", (this.Reason != null) ? this.Reason.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Reason={0}", (this.Reason != null) ? this.Reason.ToString() : string.Empty)));
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0002C3A2 File Offset: 0x0002A5A2
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0002C3AB File Offset: 0x0002A5AB
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002C3B4 File Offset: 0x0002A5B4
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0002C3C0 File Offset: 0x0002A5C0
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

		// Token: 0x06000A7A RID: 2682 RVA: 0x0002C584 File Offset: 0x0002A784
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0002C590 File Offset: 0x0002A790
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

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002C614 File Offset: 0x0002A814
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x040002D9 RID: 729
		private string creationMessage;

		// Token: 0x040002DA RID: 730
		private string m_reason;
	}
}
