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
	// Token: 0x02000075 RID: 117
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class AdomdInvalidPropertyValueException : AdomdDataAccessException
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x00023B65 File Offset: 0x00021D65
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x00023B6D File Offset: 0x00021D6D
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

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x00023B76 File Offset: 0x00021D76
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x00023B7E File Offset: 0x00021D7E
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

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x00023B87 File Offset: 0x00021D87
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x00023B8F File Offset: 0x00021D8F
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

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x00023B98 File Offset: 0x00021D98
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x00023BA0 File Offset: 0x00021DA0
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

		// Token: 0x06000885 RID: 2181 RVA: 0x00023BA9 File Offset: 0x00021DA9
		public AdomdInvalidPropertyValueException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00023BCC File Offset: 0x00021DCC
		public AdomdInvalidPropertyValueException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00023BE2 File Offset: 0x00021DE2
		public AdomdInvalidPropertyValueException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00023C05 File Offset: 0x00021E05
		public AdomdInvalidPropertyValueException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00023C2C File Offset: 0x00021E2C
		protected AdomdInvalidPropertyValueException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("AdomdInvalidPropertyValueException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.InnerType = (string)info.GetValue("AdomdInvalidPropertyValueException_InnerType", typeof(string));
			}
			catch (SerializationException)
			{
				this.InnerType = null;
			}
			try
			{
				this.InnerMessage = (string)info.GetValue("AdomdInvalidPropertyValueException_InnerMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.InnerMessage = null;
			}
			try
			{
				this.InnerToString = (string)info.GetValue("AdomdInvalidPropertyValueException_InnerToString", typeof(string));
			}
			catch (SerializationException)
			{
				this.InnerToString = null;
			}
			try
			{
				this.InnerCallStack = (string)info.GetValue("AdomdInvalidPropertyValueException_InnerCallStack", typeof(string));
			}
			catch (SerializationException)
			{
				this.InnerCallStack = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00023D74 File Offset: 0x00021F74
		public AdomdInvalidPropertyValueException(string innerType, string innerMessage, string innerToString, string innerCallStack, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.InnerType = innerType;
			this.InnerMessage = innerMessage;
			this.InnerToString = innerToString;
			this.InnerCallStack = innerCallStack;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00023DA8 File Offset: 0x00021FA8
		public AdomdInvalidPropertyValueException(string innerType, string innerMessage, string innerToString, string innerCallStack, string message, params PowerBIErrorDetail[] errorDetails)
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

		// Token: 0x0600088C RID: 2188 RVA: 0x00023DF8 File Offset: 0x00021FF8
		public AdomdInvalidPropertyValueException(string innerType, string innerMessage, string innerToString, string innerCallStack, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
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

		// Token: 0x0600088D RID: 2189 RVA: 0x00023E48 File Offset: 0x00022048
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00023E51 File Offset: 0x00022051
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00023E59 File Offset: 0x00022059
		public override bool IsBenign()
		{
			return true;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00023E5C File Offset: 0x0002205C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(AdomdInvalidPropertyValueException))
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

		// Token: 0x06000891 RID: 2193 RVA: 0x00023F10 File Offset: 0x00022110
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("AdomdInvalidPropertyValueException_creationMessage", this.creationMessage, typeof(string));
			if (this.InnerType != null)
			{
				info.AddValue("AdomdInvalidPropertyValueException_InnerType", this.InnerType, typeof(string));
			}
			if (this.InnerMessage != null)
			{
				info.AddValue("AdomdInvalidPropertyValueException_InnerMessage", this.InnerMessage, typeof(string));
			}
			if (this.InnerToString != null)
			{
				info.AddValue("AdomdInvalidPropertyValueException_InnerToString", this.InnerToString, typeof(string));
			}
			if (this.InnerCallStack != null)
			{
				info.AddValue("AdomdInvalidPropertyValueException_InnerCallStack", this.InnerCallStack, typeof(string));
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00023FD8 File Offset: 0x000221D8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "A property in the XML for Analysis Services query is not valid. Substituted: {0}:{1}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.InnerType != null) ? this.InnerType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.InnerType != null) ? this.InnerType.ToString() : string.Empty) : ((this.InnerType != null) ? this.InnerType.ToString() : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.InnerToString != null) ? this.InnerToString.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.InnerToString != null) ? this.InnerToString.ToString().MarkAsCustomerContent() : string.Empty) : ((this.InnerToString != null) ? this.InnerToString.ToString().MarkAsCustomerContent() : string.Empty)));
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x000240B0 File Offset: 0x000222B0
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

		// Token: 0x06000894 RID: 2196 RVA: 0x000240CD File Offset: 0x000222CD
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x000240EC File Offset: 0x000222EC
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "InnerType={0}", (this.InnerType != null) ? this.InnerType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "InnerType={0}", (this.InnerType != null) ? this.InnerType.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "InnerType={0}", (this.InnerType != null) ? this.InnerType.ToString() : string.Empty)));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "InnerMessage={0}", (this.InnerMessage != null) ? this.InnerMessage.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "InnerMessage={0}", (this.InnerMessage != null) ? this.InnerMessage.ToString().MarkAsCustomerContent() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "InnerMessage={0}", (this.InnerMessage != null) ? this.InnerMessage.ToString().MarkAsCustomerContent() : string.Empty)));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "InnerToString={0}", (this.InnerToString != null) ? this.InnerToString.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "InnerToString={0}", (this.InnerToString != null) ? this.InnerToString.ToString().MarkAsCustomerContent() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "InnerToString={0}", (this.InnerToString != null) ? this.InnerToString.ToString().MarkAsCustomerContent() : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "InnerCallStack={0}", (this.InnerCallStack != null) ? this.InnerCallStack.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "InnerCallStack={0}", (this.InnerCallStack != null) ? this.InnerCallStack.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "InnerCallStack={0}", (this.InnerCallStack != null) ? this.InnerCallStack.ToString() : string.Empty)));
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x000243A4 File Offset: 0x000225A4
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x000243AD File Offset: 0x000225AD
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x000243B6 File Offset: 0x000225B6
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x000243C0 File Offset: 0x000225C0
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

		// Token: 0x0600089A RID: 2202 RVA: 0x00024584 File Offset: 0x00022784
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00024590 File Offset: 0x00022790
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

		// Token: 0x0600089C RID: 2204 RVA: 0x00024614 File Offset: 0x00022814
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x040002B4 RID: 692
		private string creationMessage;

		// Token: 0x040002B5 RID: 693
		private string m_innerType;

		// Token: 0x040002B6 RID: 694
		private string m_innerMessage;

		// Token: 0x040002B7 RID: 695
		private string m_innerToString;

		// Token: 0x040002B8 RID: 696
		private string m_innerCallStack;
	}
}
