using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.AdminService
{
	// Token: 0x02000131 RID: 305
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class InvalidOrphanedBlobContainerSpecificationDeletionRequestException : AdminProvisioningServiceException
	{
		// Token: 0x06001051 RID: 4177 RVA: 0x000420EC File Offset: 0x000402EC
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x000420F4 File Offset: 0x000402F4
		// (set) Token: 0x06001053 RID: 4179 RVA: 0x000420FC File Offset: 0x000402FC
		public string BlobContainerSpecification
		{
			get
			{
				return this.m_blobContainerSpecification;
			}
			protected set
			{
				this.m_blobContainerSpecification = value;
			}
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x00042105 File Offset: 0x00040305
		public InvalidOrphanedBlobContainerSpecificationDeletionRequestException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x00042119 File Offset: 0x00040319
		public InvalidOrphanedBlobContainerSpecificationDeletionRequestException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x00042130 File Offset: 0x00040330
		public InvalidOrphanedBlobContainerSpecificationDeletionRequestException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00042150 File Offset: 0x00040350
		protected InvalidOrphanedBlobContainerSpecificationDeletionRequestException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("InvalidOrphanedBlobContainerSpecificationDeletionRequestException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.BlobContainerSpecification = (string)info.GetValue("InvalidOrphanedBlobContainerSpecificationDeletionRequestException_BlobContainerSpecification", typeof(string));
			}
			catch (SerializationException)
			{
				this.BlobContainerSpecification = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("InvalidOrphanedBlobContainerSpecificationDeletionRequestException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00042224 File Offset: 0x00040424
		public InvalidOrphanedBlobContainerSpecificationDeletionRequestException(string blobContainerSpecification, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.BlobContainerSpecification = blobContainerSpecification;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00042242 File Offset: 0x00040442
		public InvalidOrphanedBlobContainerSpecificationDeletionRequestException(string blobContainerSpecification, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.BlobContainerSpecification = blobContainerSpecification;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00042268 File Offset: 0x00040468
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0004229F File Offset: 0x0004049F
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x000422A8 File Offset: 0x000404A8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(InvalidOrphanedBlobContainerSpecificationDeletionRequestException))
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

		// Token: 0x0600105D RID: 4189 RVA: 0x00042378 File Offset: 0x00040578
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("InvalidOrphanedBlobContainerSpecificationDeletionRequestException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("InvalidOrphanedBlobContainerSpecificationDeletionRequestException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.BlobContainerSpecification != null)
			{
				info.AddValue("InvalidOrphanedBlobContainerSpecificationDeletionRequestException_BlobContainerSpecification", this.BlobContainerSpecification, typeof(string));
			}
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x000423F8 File Offset: 0x000405F8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Deleting orphaned blob container specification '{0}' not supported", (markupKind == PrivateInformationMarkupKind.None) ? ((this.BlobContainerSpecification != null) ? this.BlobContainerSpecification.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.BlobContainerSpecification != null) ? this.BlobContainerSpecification.MarkIfInternal() : string.Empty) : ((this.BlobContainerSpecification != null) ? this.BlobContainerSpecification.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x00042473 File Offset: 0x00040673
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

		// Token: 0x06001060 RID: 4192 RVA: 0x00042490 File Offset: 0x00040690
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "BlobContainerSpecification={0}", (this.BlobContainerSpecification != null) ? this.BlobContainerSpecification.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "BlobContainerSpecification={0}", (this.BlobContainerSpecification != null) ? this.BlobContainerSpecification.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "BlobContainerSpecification={0}", (this.BlobContainerSpecification != null) ? this.BlobContainerSpecification.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00042554 File Offset: 0x00040754
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0004255D File Offset: 0x0004075D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00042566 File Offset: 0x00040766
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x00042554 File Offset: 0x00040754
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x00042570 File Offset: 0x00040770
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

		// Token: 0x040003BB RID: 955
		private string creationMessage;

		// Token: 0x040003BC RID: 956
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040003BD RID: 957
		private string m_blobContainerSpecification;
	}
}
