using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.AdminService
{
	// Token: 0x02000134 RID: 308
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class DeleteOrphanedBlobContainerFailedException : AdminProvisioningServiceException
	{
		// Token: 0x0600108C RID: 4236 RVA: 0x00043268 File Offset: 0x00041468
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x00043270 File Offset: 0x00041470
		// (set) Token: 0x0600108E RID: 4238 RVA: 0x00043278 File Offset: 0x00041478
		public string BlobContainerName
		{
			get
			{
				return this.m_blobContainerName;
			}
			protected set
			{
				this.m_blobContainerName = value;
			}
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00043281 File Offset: 0x00041481
		public DeleteOrphanedBlobContainerFailedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00043295 File Offset: 0x00041495
		public DeleteOrphanedBlobContainerFailedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x000432AC File Offset: 0x000414AC
		public DeleteOrphanedBlobContainerFailedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x000432CC File Offset: 0x000414CC
		protected DeleteOrphanedBlobContainerFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("DeleteOrphanedBlobContainerFailedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.BlobContainerName = (string)info.GetValue("DeleteOrphanedBlobContainerFailedException_BlobContainerName", typeof(string));
			}
			catch (SerializationException)
			{
				this.BlobContainerName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("DeleteOrphanedBlobContainerFailedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x000433A0 File Offset: 0x000415A0
		public DeleteOrphanedBlobContainerFailedException(string blobContainerName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.BlobContainerName = blobContainerName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x000433BE File Offset: 0x000415BE
		public DeleteOrphanedBlobContainerFailedException(string blobContainerName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.BlobContainerName = blobContainerName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x000433E4 File Offset: 0x000415E4
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0004341B File Offset: 0x0004161B
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00043424 File Offset: 0x00041624
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(DeleteOrphanedBlobContainerFailedException))
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

		// Token: 0x06001098 RID: 4248 RVA: 0x000434F4 File Offset: 0x000416F4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("DeleteOrphanedBlobContainerFailedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("DeleteOrphanedBlobContainerFailedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.BlobContainerName != null)
			{
				info.AddValue("DeleteOrphanedBlobContainerFailedException_BlobContainerName", this.BlobContainerName, typeof(string));
			}
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00043574 File Offset: 0x00041774
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Deleting orphaned blob container '{0}' failed", (markupKind == PrivateInformationMarkupKind.None) ? ((this.BlobContainerName != null) ? this.BlobContainerName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.BlobContainerName != null) ? this.BlobContainerName.MarkIfInternal() : string.Empty) : ((this.BlobContainerName != null) ? this.BlobContainerName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x000435EF File Offset: 0x000417EF
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

		// Token: 0x0600109B RID: 4251 RVA: 0x0004360C File Offset: 0x0004180C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "BlobContainerName={0}", (this.BlobContainerName != null) ? this.BlobContainerName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "BlobContainerName={0}", (this.BlobContainerName != null) ? this.BlobContainerName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "BlobContainerName={0}", (this.BlobContainerName != null) ? this.BlobContainerName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x000436D0 File Offset: 0x000418D0
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x000436D9 File Offset: 0x000418D9
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x000436E2 File Offset: 0x000418E2
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x000436D0 File Offset: 0x000418D0
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x000436EC File Offset: 0x000418EC
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

		// Token: 0x040003C3 RID: 963
		private string creationMessage;

		// Token: 0x040003C4 RID: 964
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040003C5 RID: 965
		private string m_blobContainerName;
	}
}
