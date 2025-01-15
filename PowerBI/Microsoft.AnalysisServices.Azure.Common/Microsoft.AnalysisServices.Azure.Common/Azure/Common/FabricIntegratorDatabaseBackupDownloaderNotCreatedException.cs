using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000107 RID: 263
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class FabricIntegratorDatabaseBackupDownloaderNotCreatedException : MonitoredException
	{
		// Token: 0x06000CF5 RID: 3317 RVA: 0x00031D4C File Offset: 0x0002FF4C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x00031D54 File Offset: 0x0002FF54
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x00031D5C File Offset: 0x0002FF5C
		public string DatabaseId
		{
			get
			{
				return this.m_databaseId;
			}
			protected set
			{
				this.m_databaseId = value;
			}
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00031D65 File Offset: 0x0002FF65
		public FabricIntegratorDatabaseBackupDownloaderNotCreatedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00031D79 File Offset: 0x0002FF79
		public FabricIntegratorDatabaseBackupDownloaderNotCreatedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00031D90 File Offset: 0x0002FF90
		public FabricIntegratorDatabaseBackupDownloaderNotCreatedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00031DB0 File Offset: 0x0002FFB0
		protected FabricIntegratorDatabaseBackupDownloaderNotCreatedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("FabricIntegratorDatabaseBackupDownloaderNotCreatedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseId = (string)info.GetValue("FabricIntegratorDatabaseBackupDownloaderNotCreatedException_DatabaseId", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseId = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("FabricIntegratorDatabaseBackupDownloaderNotCreatedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00031E84 File Offset: 0x00030084
		public FabricIntegratorDatabaseBackupDownloaderNotCreatedException(string databaseId, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseId = databaseId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00031EA2 File Offset: 0x000300A2
		public FabricIntegratorDatabaseBackupDownloaderNotCreatedException(string databaseId, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseId = databaseId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00031EC8 File Offset: 0x000300C8
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00031EFF File Offset: 0x000300FF
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00031F08 File Offset: 0x00030108
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(FabricIntegratorDatabaseBackupDownloaderNotCreatedException))
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

		// Token: 0x06000D01 RID: 3329 RVA: 0x00031FD8 File Offset: 0x000301D8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("FabricIntegratorDatabaseBackupDownloaderNotCreatedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("FabricIntegratorDatabaseBackupDownloaderNotCreatedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseId != null)
			{
				info.AddValue("FabricIntegratorDatabaseBackupDownloaderNotCreatedException_DatabaseId", this.DatabaseId, typeof(string));
			}
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00032058 File Offset: 0x00030258
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The Fabric Host failed to create a downloader for database: {0}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseId != null) ? this.DatabaseId.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseId != null) ? this.DatabaseId.MarkIfInternal() : string.Empty) : ((this.DatabaseId != null) ? this.DatabaseId.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x000320D3 File Offset: 0x000302D3
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

		// Token: 0x06000D04 RID: 3332 RVA: 0x000320F0 File Offset: 0x000302F0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseId={0}", (this.DatabaseId != null) ? this.DatabaseId.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseId={0}", (this.DatabaseId != null) ? this.DatabaseId.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseId={0}", (this.DatabaseId != null) ? this.DatabaseId.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x000321B4 File Offset: 0x000303B4
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x000321BD File Offset: 0x000303BD
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x000321C6 File Offset: 0x000303C6
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x000321B4 File Offset: 0x000303B4
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x000321D0 File Offset: 0x000303D0
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

		// Token: 0x0400033D RID: 829
		private string creationMessage;

		// Token: 0x0400033E RID: 830
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400033F RID: 831
		private string m_databaseId;
	}
}
