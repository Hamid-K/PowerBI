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
	// Token: 0x020000B4 RID: 180
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class DataMovementExtensionNotLoadedException : GatewayPipelineException
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x0003B975 File Offset: 0x00039B75
		// (set) Token: 0x06000E2A RID: 3626 RVA: 0x0003B97D File Offset: 0x00039B7D
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

		// Token: 0x06000E2B RID: 3627 RVA: 0x0003B986 File Offset: 0x00039B86
		public DataMovementExtensionNotLoadedException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0003B99A File Offset: 0x00039B9A
		public DataMovementExtensionNotLoadedException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0003B9B0 File Offset: 0x00039BB0
		public DataMovementExtensionNotLoadedException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0003B9D3 File Offset: 0x00039BD3
		public DataMovementExtensionNotLoadedException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0003B9F8 File Offset: 0x00039BF8
		protected DataMovementExtensionNotLoadedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("DataMovementExtensionNotLoadedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ExtensionKind = (string)info.GetValue("DataMovementExtensionNotLoadedException_ExtensionKind", typeof(string));
			}
			catch (SerializationException)
			{
				this.ExtensionKind = null;
			}
			this.ConstructorInternal(true);
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0003BA94 File Offset: 0x00039C94
		public DataMovementExtensionNotLoadedException(string extensionKind, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ExtensionKind = extensionKind;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0003BABE File Offset: 0x00039CBE
		public DataMovementExtensionNotLoadedException(string extensionKind, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ExtensionKind = extensionKind;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0003BAEA File Offset: 0x00039CEA
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_DataMovementExtensionNotLoadedError";
			}
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0003BB01 File Offset: 0x00039D01
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0003BB0C File Offset: 0x00039D0C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(DataMovementExtensionNotLoadedException))
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

		// Token: 0x06000E35 RID: 3637 RVA: 0x0003BBC0 File Offset: 0x00039DC0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("DataMovementExtensionNotLoadedException_creationMessage", this.creationMessage, typeof(string));
			if (this.ExtensionKind != null)
			{
				info.AddValue("DataMovementExtensionNotLoadedException_ExtensionKind", this.ExtensionKind, typeof(string));
			}
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0003BC20 File Offset: 0x00039E20
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Mashup extension {0} does not load into Mashup Engine", (markupKind == PrivateInformationMarkupKind.None) ? ((this.ExtensionKind != null) ? this.ExtensionKind.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ExtensionKind != null) ? this.ExtensionKind.ToString() : string.Empty) : ((this.ExtensionKind != null) ? this.ExtensionKind.ToString() : string.Empty)));
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x0003BC95 File Offset: 0x00039E95
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

		// Token: 0x06000E38 RID: 3640 RVA: 0x0003BCB2 File Offset: 0x00039EB2
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0003BCD0 File Offset: 0x00039ED0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ExtensionKind={0}", (this.ExtensionKind != null) ? this.ExtensionKind.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ExtensionKind={0}", (this.ExtensionKind != null) ? this.ExtensionKind.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ExtensionKind={0}", (this.ExtensionKind != null) ? this.ExtensionKind.ToString() : string.Empty)));
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0003BD8E File Offset: 0x00039F8E
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0003BD97 File Offset: 0x00039F97
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0003BDA0 File Offset: 0x00039FA0
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0003BDAC File Offset: 0x00039FAC
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

		// Token: 0x06000E3E RID: 3646 RVA: 0x0003BF70 File Offset: 0x0003A170
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0003BF7C File Offset: 0x0003A17C
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

		// Token: 0x06000E40 RID: 3648 RVA: 0x0003C000 File Offset: 0x0003A200
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x0400031C RID: 796
		private string creationMessage;

		// Token: 0x0400031D RID: 797
		private string m_extensionKind;
	}
}
