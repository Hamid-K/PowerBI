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
	// Token: 0x02000021 RID: 33
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class InvalidParameterCollectionItemException : GatewayPipelineException
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00005E61 File Offset: 0x00004061
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00005E69 File Offset: 0x00004069
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

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00005E72 File Offset: 0x00004072
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00005E7A File Offset: 0x0000407A
		public string ExpectedType
		{
			get
			{
				return this.m_expectedType;
			}
			protected set
			{
				this.m_expectedType = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00005E83 File Offset: 0x00004083
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00005E8B File Offset: 0x0000408B
		public string ActualType
		{
			get
			{
				return this.m_actualType;
			}
			protected set
			{
				this.m_actualType = value;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005E94 File Offset: 0x00004094
		public InvalidParameterCollectionItemException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidValueField<bool>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005EB2 File Offset: 0x000040B2
		public InvalidParameterCollectionItemException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005EC8 File Offset: 0x000040C8
		public InvalidParameterCollectionItemException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005EEB File Offset: 0x000040EB
		public InvalidParameterCollectionItemException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005F10 File Offset: 0x00004110
		protected InvalidParameterCollectionItemException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("InvalidParameterCollectionItemException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.IsNull = (bool)info.GetValue("InvalidParameterCollectionItemException_IsNull", typeof(bool));
			try
			{
				this.ExpectedType = (string)info.GetValue("InvalidParameterCollectionItemException_ExpectedType", typeof(string));
			}
			catch (SerializationException)
			{
				this.ExpectedType = null;
			}
			try
			{
				this.ActualType = (string)info.GetValue("InvalidParameterCollectionItemException_ActualType", typeof(string));
			}
			catch (SerializationException)
			{
				this.ActualType = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006004 File Offset: 0x00004204
		public InvalidParameterCollectionItemException(bool isNull, string expectedType, string actualType, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.IsNull = isNull;
			this.ExpectedType = expectedType;
			this.ActualType = actualType;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006030 File Offset: 0x00004230
		public InvalidParameterCollectionItemException(bool isNull, string expectedType, string actualType, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.IsNull = isNull;
			this.ExpectedType = expectedType;
			this.ActualType = actualType;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000606B File Offset: 0x0000426B
		public InvalidParameterCollectionItemException(bool isNull, string expectedType, string actualType, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.IsNull = isNull;
			this.ExpectedType = expectedType;
			this.ActualType = actualType;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000060A8 File Offset: 0x000042A8
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_InvalidParameterCollectionItemError";
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000060BF File Offset: 0x000042BF
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000060C8 File Offset: 0x000042C8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(InvalidParameterCollectionItemException))
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

		// Token: 0x0600012E RID: 302 RVA: 0x0000617C File Offset: 0x0000437C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("InvalidParameterCollectionItemException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("InvalidParameterCollectionItemException_IsNull", this.IsNull, typeof(bool));
			if (this.ExpectedType != null)
			{
				info.AddValue("InvalidParameterCollectionItemException_ExpectedType", this.ExpectedType, typeof(string));
			}
			if (this.ActualType != null)
			{
				info.AddValue("InvalidParameterCollectionItemException_ActualType", this.ActualType, typeof(string));
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006220 File Offset: 0x00004420
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The parameter is either null or not of the expected type. IsNull: {0}, Expected base type: {1}, Actual type: {2}", (markupKind == PrivateInformationMarkupKind.None) ? this.IsNull.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.IsNull.ToString() : this.IsNull.ToString()), (markupKind == PrivateInformationMarkupKind.None) ? ((this.ExpectedType != null) ? this.ExpectedType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ExpectedType != null) ? this.ExpectedType.ToString() : string.Empty) : ((this.ExpectedType != null) ? this.ExpectedType.ToString() : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.ActualType != null) ? this.ActualType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ActualType != null) ? this.ActualType.ToString() : string.Empty) : ((this.ActualType != null) ? this.ActualType.ToString() : string.Empty)));
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00006323 File Offset: 0x00004523
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

		// Token: 0x06000131 RID: 305 RVA: 0x00006340 File Offset: 0x00004540
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006360 File Offset: 0x00004560
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "IsNull={0}", this.IsNull.ToString()) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "IsNull={0}", this.IsNull.ToString()) : string.Format(CultureInfo.CurrentCulture, "IsNull={0}", this.IsNull.ToString())));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ExpectedType={0}", (this.ExpectedType != null) ? this.ExpectedType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ExpectedType={0}", (this.ExpectedType != null) ? this.ExpectedType.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ExpectedType={0}", (this.ExpectedType != null) ? this.ExpectedType.ToString() : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ActualType={0}", (this.ActualType != null) ? this.ActualType.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ActualType={0}", (this.ActualType != null) ? this.ActualType.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ActualType={0}", (this.ActualType != null) ? this.ActualType.ToString() : string.Empty)));
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000653E File Offset: 0x0000473E
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006547 File Offset: 0x00004747
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006550 File Offset: 0x00004750
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000655C File Offset: 0x0000475C
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

		// Token: 0x06000137 RID: 311 RVA: 0x00006720 File Offset: 0x00004920
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000672C File Offset: 0x0000492C
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

		// Token: 0x06000139 RID: 313 RVA: 0x000067B0 File Offset: 0x000049B0
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x04000238 RID: 568
		private string creationMessage;

		// Token: 0x04000239 RID: 569
		private bool m_isNull;

		// Token: 0x0400023A RID: 570
		private string m_expectedType;

		// Token: 0x0400023B RID: 571
		private string m_actualType;
	}
}
