using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Privacy;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics
{
	// Token: 0x02000011 RID: 17
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class GatewayPipelineWrapperException : GatewayPipelineException
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000036D4 File Offset: 0x000018D4
		[Nullable(1)]
		public override string StackTrace
		{
			[NullableContext(1)]
			get
			{
				return this.InnerCallStack;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000036DC File Offset: 0x000018DC
		[Nullable(1)]
		public override string Message
		{
			[NullableContext(1)]
			get
			{
				return this.InnerMessage;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000036E4 File Offset: 0x000018E4
		[Nullable(1)]
		protected override string ExceptionClassName
		{
			[NullableContext(1)]
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "Wrapped({0})", this.m_innerType);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000036FB File Offset: 0x000018FB
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003703 File Offset: 0x00001903
		public string InnerType
		{
			get
			{
				return this.m_innerType;
			}
			protected set
			{
				this.m_innerType = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000370C File Offset: 0x0000190C
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003714 File Offset: 0x00001914
		public string InnerMessage
		{
			get
			{
				return this.m_innerMessage;
			}
			protected set
			{
				this.m_innerMessage = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000371D File Offset: 0x0000191D
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003725 File Offset: 0x00001925
		public string InnerToString
		{
			get
			{
				return this.m_innerToString;
			}
			protected set
			{
				this.m_innerToString = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000372E File Offset: 0x0000192E
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003736 File Offset: 0x00001936
		public string InnerCallStack
		{
			get
			{
				return this.m_innerCallStack;
			}
			protected set
			{
				this.m_innerCallStack = value;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000373F File Offset: 0x0000193F
		public GatewayPipelineWrapperException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003762 File Offset: 0x00001962
		public GatewayPipelineWrapperException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003778 File Offset: 0x00001978
		public GatewayPipelineWrapperException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000379B File Offset: 0x0000199B
		public GatewayPipelineWrapperException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000037C0 File Offset: 0x000019C0
		protected GatewayPipelineWrapperException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("GatewayPipelineWrapperException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.InnerType = (string)info.GetValue("GatewayPipelineWrapperException_InnerType", typeof(string));
			}
			catch (SerializationException)
			{
				this.InnerType = null;
			}
			try
			{
				this.InnerMessage = (string)info.GetValue("GatewayPipelineWrapperException_InnerMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.InnerMessage = null;
			}
			try
			{
				this.InnerToString = (string)info.GetValue("GatewayPipelineWrapperException_InnerToString", typeof(string));
			}
			catch (SerializationException)
			{
				this.InnerToString = null;
			}
			try
			{
				this.InnerCallStack = (string)info.GetValue("GatewayPipelineWrapperException_InnerCallStack", typeof(string));
			}
			catch (SerializationException)
			{
				this.InnerCallStack = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003908 File Offset: 0x00001B08
		public GatewayPipelineWrapperException(string innerType, string innerMessage, string innerToString, string innerCallStack, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.InnerType = innerType;
			this.InnerMessage = innerMessage;
			this.InnerToString = innerToString;
			this.InnerCallStack = innerCallStack;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000393C File Offset: 0x00001B3C
		public GatewayPipelineWrapperException(string innerType, string innerMessage, string innerToString, string innerCallStack, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.InnerType = innerType;
			this.InnerMessage = innerMessage;
			this.InnerToString = innerToString;
			this.InnerCallStack = innerCallStack;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000398C File Offset: 0x00001B8C
		public GatewayPipelineWrapperException(string innerType, string innerMessage, string innerToString, string innerCallStack, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.InnerType = innerType;
			this.InnerMessage = innerMessage;
			this.InnerToString = innerToString;
			this.InnerCallStack = innerCallStack;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000039DC File Offset: 0x00001BDC
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000039E5 File Offset: 0x00001BE5
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000039E8 File Offset: 0x00001BE8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("GatewayPipelineWrapperException_creationMessage", this.creationMessage, typeof(string));
			if (this.InnerType != null)
			{
				info.AddValue("GatewayPipelineWrapperException_InnerType", this.InnerType, typeof(string));
			}
			if (this.InnerMessage != null)
			{
				info.AddValue("GatewayPipelineWrapperException_InnerMessage", this.InnerMessage, typeof(string));
			}
			if (this.InnerToString != null)
			{
				info.AddValue("GatewayPipelineWrapperException_InnerToString", this.InnerToString, typeof(string));
			}
			if (this.InnerCallStack != null)
			{
				info.AddValue("GatewayPipelineWrapperException_InnerCallStack", this.InnerCallStack, typeof(string));
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003AB0 File Offset: 0x00001CB0
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Substituted: {0}:{1}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.InnerType != null) ? this.InnerType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.InnerType != null) ? this.InnerType.ToString() : string.Empty) : ((this.InnerType != null) ? this.InnerType.ToString() : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.InnerToString != null) ? this.InnerToString.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.InnerToString != null) ? this.InnerToString.ToString().MarkAsCustomerContent() : string.Empty) : ((this.InnerToString != null) ? this.InnerToString.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003B88 File Offset: 0x00001D88
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003BA8 File Offset: 0x00001DA8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "InnerType={0}", (this.InnerType != null) ? this.InnerType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "InnerType={0}", (this.InnerType != null) ? this.InnerType.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "InnerType={0}", (this.InnerType != null) ? this.InnerType.ToString() : string.Empty)));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "InnerMessage={0}", (this.InnerMessage != null) ? this.InnerMessage.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "InnerMessage={0}", (this.InnerMessage != null) ? this.InnerMessage.ToString().MarkAsCustomerContent() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "InnerMessage={0}", (this.InnerMessage != null) ? this.InnerMessage.ToString().MarkAsCustomerContent() : string.Empty)));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "InnerToString={0}", (this.InnerToString != null) ? this.InnerToString.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "InnerToString={0}", (this.InnerToString != null) ? this.InnerToString.ToString().MarkAsCustomerContent() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "InnerToString={0}", (this.InnerToString != null) ? this.InnerToString.ToString().MarkAsCustomerContent() : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "InnerCallStack={0}", (this.InnerCallStack != null) ? this.InnerCallStack.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "InnerCallStack={0}", (this.InnerCallStack != null) ? this.InnerCallStack.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "InnerCallStack={0}", (this.InnerCallStack != null) ? this.InnerCallStack.ToString() : string.Empty)));
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003E60 File Offset: 0x00002060
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003E69 File Offset: 0x00002069
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003E72 File Offset: 0x00002072
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003E7C File Offset: 0x0000207C
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

		// Token: 0x06000092 RID: 146 RVA: 0x00004040 File Offset: 0x00002240
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000404C File Offset: 0x0000224C
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

		// Token: 0x06000094 RID: 148 RVA: 0x000040D0 File Offset: 0x000022D0
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x0400003D RID: 61
		private string creationMessage;

		// Token: 0x0400003E RID: 62
		private string m_innerType;

		// Token: 0x0400003F RID: 63
		private string m_innerMessage;

		// Token: 0x04000040 RID: 64
		private string m_innerToString;

		// Token: 0x04000041 RID: 65
		private string m_innerCallStack;
	}
}
