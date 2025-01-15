using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.AdminService
{
	// Token: 0x02000133 RID: 307
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class UnexpectedPbiRpsContainerFoundException : AdminProvisioningServiceException
	{
		// Token: 0x06001077 RID: 4215 RVA: 0x00042BF8 File Offset: 0x00040DF8
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x00042C00 File Offset: 0x00040E00
		// (set) Token: 0x06001079 RID: 4217 RVA: 0x00042C08 File Offset: 0x00040E08
		public string BlobContainer
		{
			get
			{
				return this.m_blobContainer;
			}
			protected set
			{
				this.m_blobContainer = value;
			}
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00042C11 File Offset: 0x00040E11
		public UnexpectedPbiRpsContainerFoundException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00042C25 File Offset: 0x00040E25
		public UnexpectedPbiRpsContainerFoundException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00042C3C File Offset: 0x00040E3C
		public UnexpectedPbiRpsContainerFoundException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x00042C5C File Offset: 0x00040E5C
		protected UnexpectedPbiRpsContainerFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("UnexpectedPbiRpsContainerFoundException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.BlobContainer = (string)info.GetValue("UnexpectedPbiRpsContainerFoundException_BlobContainer", typeof(string));
			}
			catch (SerializationException)
			{
				this.BlobContainer = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("UnexpectedPbiRpsContainerFoundException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00042D30 File Offset: 0x00040F30
		public UnexpectedPbiRpsContainerFoundException(string blobContainer, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.BlobContainer = blobContainer;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00042D4E File Offset: 0x00040F4E
		public UnexpectedPbiRpsContainerFoundException(string blobContainer, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.BlobContainer = blobContainer;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00042D74 File Offset: 0x00040F74
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00042DAB File Offset: 0x00040FAB
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00042DB4 File Offset: 0x00040FB4
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(UnexpectedPbiRpsContainerFoundException))
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

		// Token: 0x06001083 RID: 4227 RVA: 0x00042E84 File Offset: 0x00041084
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("UnexpectedPbiRpsContainerFoundException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("UnexpectedPbiRpsContainerFoundException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.BlobContainer != null)
			{
				info.AddValue("UnexpectedPbiRpsContainerFoundException_BlobContainer", this.BlobContainer, typeof(string));
			}
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00042F04 File Offset: 0x00041104
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Unusual Container '{0}' encountered during orphaned blob deletion flows", (markupKind == PrivateInformationMarkupKind.None) ? ((this.BlobContainer != null) ? this.BlobContainer.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.BlobContainer != null) ? this.BlobContainer.MarkIfInternal() : string.Empty) : ((this.BlobContainer != null) ? this.BlobContainer.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x00042F7F File Offset: 0x0004117F
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

		// Token: 0x06001086 RID: 4230 RVA: 0x00042F9C File Offset: 0x0004119C
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "BlobContainer={0}", (this.BlobContainer != null) ? this.BlobContainer.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "BlobContainer={0}", (this.BlobContainer != null) ? this.BlobContainer.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "BlobContainer={0}", (this.BlobContainer != null) ? this.BlobContainer.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x00043060 File Offset: 0x00041260
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00043069 File Offset: 0x00041269
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00043072 File Offset: 0x00041272
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00043060 File Offset: 0x00041260
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0004307C File Offset: 0x0004127C
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

		// Token: 0x040003C0 RID: 960
		private string creationMessage;

		// Token: 0x040003C1 RID: 961
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040003C2 RID: 962
		private string m_blobContainer;
	}
}
