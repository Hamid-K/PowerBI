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
	// Token: 0x020000B5 RID: 181
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class DataMovementExtensionVersionNotLoadedException : GatewayPipelineException
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0003C031 File Offset: 0x0003A231
		// (set) Token: 0x06000E42 RID: 3650 RVA: 0x0003C039 File Offset: 0x0003A239
		public string ExtensionKind
		{
			get
			{
				return this.m_extensionKind;
			}
			protected set
			{
				this.m_extensionKind = value;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0003C042 File Offset: 0x0003A242
		// (set) Token: 0x06000E44 RID: 3652 RVA: 0x0003C04A File Offset: 0x0003A24A
		public string ExtensionVersion
		{
			get
			{
				return this.m_extensionVersion;
			}
			protected set
			{
				this.m_extensionVersion = value;
			}
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0003C053 File Offset: 0x0003A253
		public DataMovementExtensionVersionNotLoadedException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0003C06C File Offset: 0x0003A26C
		public DataMovementExtensionVersionNotLoadedException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0003C082 File Offset: 0x0003A282
		public DataMovementExtensionVersionNotLoadedException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0003C0A5 File Offset: 0x0003A2A5
		public DataMovementExtensionVersionNotLoadedException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0003C0CC File Offset: 0x0003A2CC
		protected DataMovementExtensionVersionNotLoadedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("DataMovementExtensionVersionNotLoadedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ExtensionKind = (string)info.GetValue("DataMovementExtensionVersionNotLoadedException_ExtensionKind", typeof(string));
			}
			catch (SerializationException)
			{
				this.ExtensionKind = null;
			}
			try
			{
				this.ExtensionVersion = (string)info.GetValue("DataMovementExtensionVersionNotLoadedException_ExtensionVersion", typeof(string));
			}
			catch (SerializationException)
			{
				this.ExtensionVersion = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0003C1A4 File Offset: 0x0003A3A4
		public DataMovementExtensionVersionNotLoadedException(string extensionKind, string extensionVersion, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ExtensionKind = extensionKind;
			this.ExtensionVersion = extensionVersion;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0003C1C8 File Offset: 0x0003A3C8
		public DataMovementExtensionVersionNotLoadedException(string extensionKind, string extensionVersion, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ExtensionKind = extensionKind;
			this.ExtensionVersion = extensionVersion;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0003C1FA File Offset: 0x0003A3FA
		public DataMovementExtensionVersionNotLoadedException(string extensionKind, string extensionVersion, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ExtensionKind = extensionKind;
			this.ExtensionVersion = extensionVersion;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0003C22E File Offset: 0x0003A42E
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_DataMovementExtensionVersionNotLoadedError";
			}
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0003C245 File Offset: 0x0003A445
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0003C250 File Offset: 0x0003A450
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(DataMovementExtensionVersionNotLoadedException))
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

		// Token: 0x06000E50 RID: 3664 RVA: 0x0003C304 File Offset: 0x0003A504
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("DataMovementExtensionVersionNotLoadedException_creationMessage", this.creationMessage, typeof(string));
			if (this.ExtensionKind != null)
			{
				info.AddValue("DataMovementExtensionVersionNotLoadedException_ExtensionKind", this.ExtensionKind, typeof(string));
			}
			if (this.ExtensionVersion != null)
			{
				info.AddValue("DataMovementExtensionVersionNotLoadedException_ExtensionVersion", this.ExtensionVersion, typeof(string));
			}
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0003C388 File Offset: 0x0003A588
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Mashup extension {0} of version {1} does not load into Mashup Engine", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ExtensionKind != null) ? this.ExtensionKind.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ExtensionKind != null) ? this.ExtensionKind.ToString() : string.Empty) : ((this.ExtensionKind != null) ? this.ExtensionKind.ToString() : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.ExtensionVersion != null) ? this.ExtensionVersion.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ExtensionVersion != null) ? this.ExtensionVersion.ToString() : string.Empty) : ((this.ExtensionVersion != null) ? this.ExtensionVersion.ToString() : string.Empty)));
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x0003C456 File Offset: 0x0003A656
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

		// Token: 0x06000E53 RID: 3667 RVA: 0x0003C473 File Offset: 0x0003A673
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0003C490 File Offset: 0x0003A690
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ExtensionKind={0}", (this.ExtensionKind != null) ? this.ExtensionKind.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ExtensionKind={0}", (this.ExtensionKind != null) ? this.ExtensionKind.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ExtensionKind={0}", (this.ExtensionKind != null) ? this.ExtensionKind.ToString() : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ExtensionVersion={0}", (this.ExtensionVersion != null) ? this.ExtensionVersion.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ExtensionVersion={0}", (this.ExtensionVersion != null) ? this.ExtensionVersion.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ExtensionVersion={0}", (this.ExtensionVersion != null) ? this.ExtensionVersion.ToString() : string.Empty)));
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0003C5F0 File Offset: 0x0003A7F0
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0003C5F9 File Offset: 0x0003A7F9
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0003C602 File Offset: 0x0003A802
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0003C60C File Offset: 0x0003A80C
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

		// Token: 0x06000E59 RID: 3673 RVA: 0x0003C7D0 File Offset: 0x0003A9D0
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0003C7DC File Offset: 0x0003A9DC
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

		// Token: 0x06000E5B RID: 3675 RVA: 0x0003C860 File Offset: 0x0003AA60
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x0400031E RID: 798
		private string creationMessage;

		// Token: 0x0400031F RID: 799
		private string m_extensionKind;

		// Token: 0x04000320 RID: 800
		private string m_extensionVersion;
	}
}
