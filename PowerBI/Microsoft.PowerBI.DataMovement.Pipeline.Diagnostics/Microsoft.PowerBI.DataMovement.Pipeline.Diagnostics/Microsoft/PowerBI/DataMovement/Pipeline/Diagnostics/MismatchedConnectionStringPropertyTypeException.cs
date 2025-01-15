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
	// Token: 0x02000031 RID: 49
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class MismatchedConnectionStringPropertyTypeException : GatewayPipelineException
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000C7AD File Offset: 0x0000A9AD
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000C7B5 File Offset: 0x0000A9B5
		public string Keyword
		{
			get
			{
				return this.m_keyword;
			}
			protected set
			{
				this.m_keyword = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000C7BE File Offset: 0x0000A9BE
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000C7C6 File Offset: 0x0000A9C6
		public string RequestedType
		{
			get
			{
				return this.m_requestedType;
			}
			protected set
			{
				this.m_requestedType = value;
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000C7CF File Offset: 0x0000A9CF
		public MismatchedConnectionStringPropertyTypeException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		public MismatchedConnectionStringPropertyTypeException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000C7FE File Offset: 0x0000A9FE
		public MismatchedConnectionStringPropertyTypeException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000C821 File Offset: 0x0000AA21
		public MismatchedConnectionStringPropertyTypeException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000C848 File Offset: 0x0000AA48
		protected MismatchedConnectionStringPropertyTypeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("MismatchedConnectionStringPropertyTypeException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Keyword = (string)info.GetValue("MismatchedConnectionStringPropertyTypeException_Keyword", typeof(string));
			}
			catch (SerializationException)
			{
				this.Keyword = null;
			}
			try
			{
				this.RequestedType = (string)info.GetValue("MismatchedConnectionStringPropertyTypeException_RequestedType", typeof(string));
			}
			catch (SerializationException)
			{
				this.RequestedType = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000C920 File Offset: 0x0000AB20
		public MismatchedConnectionStringPropertyTypeException(string keyword, string requestedType, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.Keyword = keyword;
			this.RequestedType = requestedType;
			this.ConstructorInternal(false);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000C944 File Offset: 0x0000AB44
		public MismatchedConnectionStringPropertyTypeException(string keyword, string requestedType, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.Keyword = keyword;
			this.RequestedType = requestedType;
			this.ConstructorInternal(false);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000C976 File Offset: 0x0000AB76
		public MismatchedConnectionStringPropertyTypeException(string keyword, string requestedType, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.Keyword = keyword;
			this.RequestedType = requestedType;
			this.ConstructorInternal(false);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000C9AA File Offset: 0x0000ABAA
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_MismatchedConnectionStringPropertyTypeError";
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000C9C1 File Offset: 0x0000ABC1
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000C9CC File Offset: 0x0000ABCC
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(MismatchedConnectionStringPropertyTypeException))
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

		// Token: 0x060002A6 RID: 678 RVA: 0x0000CA80 File Offset: 0x0000AC80
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("MismatchedConnectionStringPropertyTypeException_creationMessage", this.creationMessage, typeof(string));
			if (this.Keyword != null)
			{
				info.AddValue("MismatchedConnectionStringPropertyTypeException_Keyword", this.Keyword, typeof(string));
			}
			if (this.RequestedType != null)
			{
				info.AddValue("MismatchedConnectionStringPropertyTypeException_RequestedType", this.RequestedType, typeof(string));
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000CB04 File Offset: 0x0000AD04
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Property '{0}' cannot be converted to '{1}'.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.Keyword != null) ? this.Keyword.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Keyword != null) ? this.Keyword.ToString() : string.Empty) : ((this.Keyword != null) ? this.Keyword.ToString() : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.RequestedType != null) ? this.RequestedType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.RequestedType != null) ? this.RequestedType.ToString() : string.Empty) : ((this.RequestedType != null) ? this.RequestedType.ToString() : string.Empty)));
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000CBD2 File Offset: 0x0000ADD2
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

		// Token: 0x060002A9 RID: 681 RVA: 0x0000CBEF File Offset: 0x0000ADEF
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000CC0C File Offset: 0x0000AE0C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Keyword={0}", (this.Keyword != null) ? this.Keyword.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Keyword={0}", (this.Keyword != null) ? this.Keyword.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "Keyword={0}", (this.Keyword != null) ? this.Keyword.ToString() : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "RequestedType={0}", (this.RequestedType != null) ? this.RequestedType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "RequestedType={0}", (this.RequestedType != null) ? this.RequestedType.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "RequestedType={0}", (this.RequestedType != null) ? this.RequestedType.ToString() : string.Empty)));
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000CD6C File Offset: 0x0000AF6C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000CD75 File Offset: 0x0000AF75
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000CD7E File Offset: 0x0000AF7E
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000CD88 File Offset: 0x0000AF88
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

		// Token: 0x060002AF RID: 687 RVA: 0x0000CF4C File Offset: 0x0000B14C
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000CF58 File Offset: 0x0000B158
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

		// Token: 0x060002B1 RID: 689 RVA: 0x0000CFDC File Offset: 0x0000B1DC
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000258 RID: 600
		private string creationMessage;

		// Token: 0x04000259 RID: 601
		private string m_keyword;

		// Token: 0x0400025A RID: 602
		private string m_requestedType;
	}
}
