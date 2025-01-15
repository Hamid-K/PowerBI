using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.AdminService
{
	// Token: 0x02000136 RID: 310
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class AdvancedModelFeatureNotSupportedException : AdminProvisioningServiceException
	{
		// Token: 0x060010B2 RID: 4274 RVA: 0x00043D70 File Offset: 0x00041F70
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x00043D78 File Offset: 0x00041F78
		// (set) Token: 0x060010B4 RID: 4276 RVA: 0x00043D80 File Offset: 0x00041F80
		public string FeatureName
		{
			get
			{
				return this.m_featureName;
			}
			protected set
			{
				this.m_featureName = value;
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00043D89 File Offset: 0x00041F89
		public AdvancedModelFeatureNotSupportedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00043D9D File Offset: 0x00041F9D
		public AdvancedModelFeatureNotSupportedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00043DB4 File Offset: 0x00041FB4
		public AdvancedModelFeatureNotSupportedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00043DD4 File Offset: 0x00041FD4
		protected AdvancedModelFeatureNotSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("AdvancedModelFeatureNotSupportedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.FeatureName = (string)info.GetValue("AdvancedModelFeatureNotSupportedException_FeatureName", typeof(string));
			}
			catch (SerializationException)
			{
				this.FeatureName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("AdvancedModelFeatureNotSupportedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00043EA8 File Offset: 0x000420A8
		public AdvancedModelFeatureNotSupportedException(string featureName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.FeatureName = featureName;
			this.ConstructorInternal(false);
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00043EC6 File Offset: 0x000420C6
		public AdvancedModelFeatureNotSupportedException(string featureName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.FeatureName = featureName;
			this.ConstructorInternal(false);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00043EEC File Offset: 0x000420EC
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00043F23 File Offset: 0x00042123
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00043F2C File Offset: 0x0004212C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(AdvancedModelFeatureNotSupportedException))
			{
				TraceSourceBase<ANCommonTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<ANCommonTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<ANCommonTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x00043FFC File Offset: 0x000421FC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("AdvancedModelFeatureNotSupportedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("AdvancedModelFeatureNotSupportedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.FeatureName != null)
			{
				info.AddValue("AdvancedModelFeatureNotSupportedException_FeatureName", this.FeatureName, typeof(string));
			}
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0004407A File Offset: 0x0004227A
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Model operation failed because the user tried to load a model with unsupported model feature.", Array.Empty<object>());
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x00044090 File Offset: 0x00042290
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

		// Token: 0x060010C1 RID: 4289 RVA: 0x000440B0 File Offset: 0x000422B0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "FeatureName={0}", (this.FeatureName != null) ? this.FeatureName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "FeatureName={0}", (this.FeatureName != null) ? this.FeatureName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "FeatureName={0}", (this.FeatureName != null) ? this.FeatureName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00044174 File Offset: 0x00042374
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0004417D File Offset: 0x0004237D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00044186 File Offset: 0x00042386
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00044174 File Offset: 0x00042374
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00044190 File Offset: 0x00042390
		private string ToString(PrivateInformationMarkupKind markupKind)
		{
			string text = "[" + ExceptionsTemplateHelper.MagicLevel.ToString(CultureInfo.CurrentCulture) + "]" + base.GetType().FullName;
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
			text3 = text3 + Environment.NewLine + "ExceptionCulprit=" + this.exceptionCulprit.ToString();
			if (base.InnerException != null)
			{
				try
				{
					ExceptionsTemplateHelper.IncrementMagicLevel();
					IMonitoredError monitoredError = base.InnerException as MonitoredException;
					string text4;
					if (markupKind != PrivateInformationMarkupKind.None)
					{
						if (markupKind != PrivateInformationMarkupKind.Internal)
						{
							text4 = ((monitoredError == null) ? base.InnerException.MarkIfPrivate() : monitoredError.ToPrivateString());
							text4 = text4.ObfuscatePrivateValue(true);
						}
						else
						{
							text4 = ((monitoredError == null) ? base.InnerException.MarkIfInternal() : monitoredError.ToInternalString());
						}
					}
					else
					{
						text4 = ((monitoredError == null) ? base.InnerException.ToString() : monitoredError.ToOriginalString());
					}
					text3 = string.Concat(new string[]
					{
						text3,
						" --->",
						Environment.NewLine,
						text4,
						Environment.NewLine,
						"   --- End of inner exception stack trace ---",
						Environment.NewLine,
						"  (",
						text,
						".StackTrace:)"
					});
				}
				finally
				{
					ExceptionsTemplateHelper.DecrementMagicLevel();
				}
			}
			if (this.StackTrace != null)
			{
				text3 = text3 + Environment.NewLine + this.StackTrace;
			}
			return text3;
		}

		// Token: 0x040003C8 RID: 968
		private string creationMessage;

		// Token: 0x040003C9 RID: 969
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040003CA RID: 970
		private string m_featureName;
	}
}
