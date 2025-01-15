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
	// Token: 0x02000022 RID: 34
	[GeneratedCode("DMGatewayPipelineException", "4.0.0.0")]
	[Serializable]
	public class InvalidObjectArrayArgumentException : GatewayPipelineException
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600013A RID: 314 RVA: 0x000067E1 File Offset: 0x000049E1
		// (set) Token: 0x0600013B RID: 315 RVA: 0x000067E9 File Offset: 0x000049E9
		public string ParamName
		{
			get
			{
				return this.m_paramName;
			}
			protected set
			{
				this.m_paramName = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000067F2 File Offset: 0x000049F2
		// (set) Token: 0x0600013D RID: 317 RVA: 0x000067FA File Offset: 0x000049FA
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

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006803 File Offset: 0x00004A03
		// (set) Token: 0x0600013F RID: 319 RVA: 0x0000680B File Offset: 0x00004A0B
		public int ExpectedSize
		{
			get
			{
				return this.m_expectedSize;
			}
			protected set
			{
				this.m_expectedSize = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006814 File Offset: 0x00004A14
		// (set) Token: 0x06000141 RID: 321 RVA: 0x0000681C File Offset: 0x00004A1C
		public int ActualSize
		{
			get
			{
				return this.m_actualSize;
			}
			protected set
			{
				this.m_actualSize = value;
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006825 File Offset: 0x00004A25
		public InvalidObjectArrayArgumentException()
		{
			this.ConstructorInternal(false);
			GatewayExceptionUtils.CompileCheck.IsValidReferenceField<string>();
			GatewayExceptionUtils.CompileCheck.IsValidValueField<bool>();
			GatewayExceptionUtils.CompileCheck.IsValidValueField<int>();
			GatewayExceptionUtils.CompileCheck.IsValidValueField<int>();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006848 File Offset: 0x00004A48
		public InvalidObjectArrayArgumentException(params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000685E File Offset: 0x00004A5E
		public InvalidObjectArrayArgumentException(string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006881 File Offset: 0x00004A81
		public InvalidObjectArrayArgumentException(string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000068A8 File Offset: 0x00004AA8
		protected InvalidObjectArrayArgumentException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			RuntimeChecks.CheckValue(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("InvalidObjectArrayArgumentException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.ParamName = (string)info.GetValue("InvalidObjectArrayArgumentException_ParamName", typeof(string));
			}
			catch (SerializationException)
			{
				this.ParamName = null;
			}
			this.IsNull = (bool)info.GetValue("InvalidObjectArrayArgumentException_IsNull", typeof(bool));
			this.ExpectedSize = (int)info.GetValue("InvalidObjectArrayArgumentException_ExpectedSize", typeof(int));
			this.ActualSize = (int)info.GetValue("InvalidObjectArrayArgumentException_ActualSize", typeof(int));
			this.ConstructorInternal(true);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000069A4 File Offset: 0x00004BA4
		public InvalidObjectArrayArgumentException(string paramName, bool isNull, int expectedSize, int actualSize, params PowerBIErrorDetail[] errorDetails)
		{
			this.creationErrorDetails = errorDetails;
			this.ParamName = paramName;
			this.IsNull = isNull;
			this.ExpectedSize = expectedSize;
			this.ActualSize = actualSize;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000069D8 File Offset: 0x00004BD8
		public InvalidObjectArrayArgumentException(string paramName, bool isNull, int expectedSize, int actualSize, string message, params PowerBIErrorDetail[] errorDetails)
			: base(message, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ParamName = paramName;
			this.IsNull = isNull;
			this.ExpectedSize = expectedSize;
			this.ActualSize = actualSize;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006A28 File Offset: 0x00004C28
		public InvalidObjectArrayArgumentException(string paramName, bool isNull, int expectedSize, int actualSize, string message, GatewayPipelineException innerException, params PowerBIErrorDetail[] errorDetails)
			: base(message, innerException, Array.Empty<PowerBIErrorDetail>())
		{
			this.creationMessage = message;
			this.creationErrorDetails = errorDetails;
			this.ParamName = paramName;
			this.IsNull = isNull;
			this.ExpectedSize = expectedSize;
			this.ActualSize = actualSize;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006A78 File Offset: 0x00004C78
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			if (!deserializing)
			{
				base.GatewayPipelineErrorCode = "DM_GWPipeline_Gateway_InvalidObjectArrayArgumentError";
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006A8F File Offset: 0x00004C8F
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006A98 File Offset: 0x00004C98
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(InvalidObjectArrayArgumentException))
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

		// Token: 0x0600014D RID: 333 RVA: 0x00006B4C File Offset: 0x00004D4C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			RuntimeChecks.CheckValue(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("InvalidObjectArrayArgumentException_creationMessage", this.creationMessage, typeof(string));
			if (this.ParamName != null)
			{
				info.AddValue("InvalidObjectArrayArgumentException_ParamName", this.ParamName, typeof(string));
			}
			info.AddValue("InvalidObjectArrayArgumentException_IsNull", this.IsNull, typeof(bool));
			info.AddValue("InvalidObjectArrayArgumentException_ExpectedSize", this.ExpectedSize, typeof(int));
			info.AddValue("InvalidObjectArrayArgumentException_ActualSize", this.ActualSize, typeof(int));
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006C0C File Offset: 0x00004E0C
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The method argument {0} is either null or not of the expected size. IsNull: {1}, Expected size: {2}, Actual size: {3}", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.ParamName != null) ? this.ParamName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ParamName != null) ? this.ParamName.ToString() : string.Empty) : ((this.ParamName != null) ? this.ParamName.ToString() : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? this.IsNull.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.IsNull.ToString() : this.IsNull.ToString()),
				(markupKind == PrivateInformationMarkupKind.None) ? this.ExpectedSize.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.ExpectedSize.ToString(CultureInfo.InvariantCulture) : this.ExpectedSize.ToString(CultureInfo.InvariantCulture)),
				(markupKind == PrivateInformationMarkupKind.None) ? this.ActualSize.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.ActualSize.ToString(CultureInfo.InvariantCulture) : this.ActualSize.ToString(CultureInfo.InvariantCulture))
			});
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00006D50 File Offset: 0x00004F50
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

		// Token: 0x06000150 RID: 336 RVA: 0x00006D6D File Offset: 0x00004F6D
		internal override string GetMarkedUpMessage()
		{
			if (!string.IsNullOrEmpty(this.creationMessage))
			{
				return this.creationMessage;
			}
			return this.CreateMessageFromTemplate(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00006D8C File Offset: 0x00004F8C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ParamName={0}", (this.ParamName != null) ? this.ParamName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ParamName={0}", (this.ParamName != null) ? this.ParamName.ToString() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ParamName={0}", (this.ParamName != null) ? this.ParamName.ToString() : string.Empty)));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "IsNull={0}", this.IsNull.ToString()) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "IsNull={0}", this.IsNull.ToString()) : string.Format(CultureInfo.CurrentCulture, "IsNull={0}", this.IsNull.ToString())));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ExpectedSize={0}", this.ExpectedSize.ToString(CultureInfo.InvariantCulture)) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ExpectedSize={0}", this.ExpectedSize.ToString(CultureInfo.InvariantCulture)) : string.Format(CultureInfo.CurrentCulture, "ExpectedSize={0}", this.ExpectedSize.ToString(CultureInfo.InvariantCulture))));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ActualSize={0}", this.ActualSize.ToString(CultureInfo.InvariantCulture)) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ActualSize={0}", this.ActualSize.ToString(CultureInfo.InvariantCulture)) : string.Format(CultureInfo.CurrentCulture, "ActualSize={0}", this.ActualSize.ToString(CultureInfo.InvariantCulture))));
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006FE2 File Offset: 0x000051E2
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006FEB File Offset: 0x000051EB
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006FF4 File Offset: 0x000051F4
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00007000 File Offset: 0x00005200
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

		// Token: 0x06000156 RID: 342 RVA: 0x000071C4 File Offset: 0x000053C4
		public override IDictionary<string, string> GetClientErrorParameters()
		{
			return this.GetClientErrorParameters(false);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000071D0 File Offset: 0x000053D0
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

		// Token: 0x06000158 RID: 344 RVA: 0x00007254 File Offset: 0x00005454
		public override IList<PowerBIErrorDetail> GetErrorDetails()
		{
			List<PowerBIErrorDetail> list = new List<PowerBIErrorDetail>();
			if (this.creationErrorDetails != null && this.creationErrorDetails.Length != 0)
			{
				list.AddRange(this.creationErrorDetails);
			}
			return list;
		}

		// Token: 0x0400023C RID: 572
		private string creationMessage;

		// Token: 0x0400023D RID: 573
		private string m_paramName;

		// Token: 0x0400023E RID: 574
		private bool m_isNull;

		// Token: 0x0400023F RID: 575
		private int m_expectedSize;

		// Token: 0x04000240 RID: 576
		private int m_actualSize;
	}
}
