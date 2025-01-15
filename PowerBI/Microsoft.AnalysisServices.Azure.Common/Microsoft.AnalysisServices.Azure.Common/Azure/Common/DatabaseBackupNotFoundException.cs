using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200010B RID: 267
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class DatabaseBackupNotFoundException : MonitoredException
	{
		// Token: 0x06000D49 RID: 3401 RVA: 0x0003370C File Offset: 0x0003190C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x00033714 File Offset: 0x00031914
		// (set) Token: 0x06000D4B RID: 3403 RVA: 0x0003371C File Offset: 0x0003191C
		public string DatabaseName
		{
			get
			{
				return this.m_databaseName;
			}
			protected set
			{
				this.m_databaseName = value;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00033725 File Offset: 0x00031925
		// (set) Token: 0x06000D4D RID: 3405 RVA: 0x0003372D File Offset: 0x0003192D
		public string StorageAccountName
		{
			get
			{
				return this.m_storageAccountName;
			}
			protected set
			{
				this.m_storageAccountName = value;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x00033736 File Offset: 0x00031936
		// (set) Token: 0x06000D4F RID: 3407 RVA: 0x0003373E File Offset: 0x0003193E
		public string ContainerID
		{
			get
			{
				return this.m_containerID;
			}
			protected set
			{
				this.m_containerID = value;
			}
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00033747 File Offset: 0x00031947
		public DatabaseBackupNotFoundException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00033765 File Offset: 0x00031965
		public DatabaseBackupNotFoundException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0003377C File Offset: 0x0003197C
		public DatabaseBackupNotFoundException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0003379C File Offset: 0x0003199C
		protected DatabaseBackupNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("DatabaseBackupNotFoundException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseName = (string)info.GetValue("DatabaseBackupNotFoundException_DatabaseName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseName = null;
			}
			try
			{
				this.StorageAccountName = (string)info.GetValue("DatabaseBackupNotFoundException_StorageAccountName", typeof(string));
			}
			catch (SerializationException)
			{
				this.StorageAccountName = null;
			}
			try
			{
				this.ContainerID = (string)info.GetValue("DatabaseBackupNotFoundException_ContainerID", typeof(string));
			}
			catch (SerializationException)
			{
				this.ContainerID = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("DatabaseBackupNotFoundException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x000338E4 File Offset: 0x00031AE4
		public DatabaseBackupNotFoundException(string databaseName, string storageAccountName, string containerID)
		{
			this.DatabaseName = databaseName;
			this.StorageAccountName = storageAccountName;
			this.ContainerID = containerID;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00033908 File Offset: 0x00031B08
		public DatabaseBackupNotFoundException(string databaseName, string storageAccountName, string containerID, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.StorageAccountName = storageAccountName;
			this.ContainerID = containerID;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00033936 File Offset: 0x00031B36
		public DatabaseBackupNotFoundException(string databaseName, string storageAccountName, string containerID, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.StorageAccountName = storageAccountName;
			this.ContainerID = containerID;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0003396C File Offset: 0x00031B6C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x000339A3 File Offset: 0x00031BA3
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x000339AC File Offset: 0x00031BAC
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(DatabaseBackupNotFoundException))
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

		// Token: 0x06000D5A RID: 3418 RVA: 0x00033A7C File Offset: 0x00031C7C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("DatabaseBackupNotFoundException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("DatabaseBackupNotFoundException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseName != null)
			{
				info.AddValue("DatabaseBackupNotFoundException_DatabaseName", this.DatabaseName, typeof(string));
			}
			if (this.StorageAccountName != null)
			{
				info.AddValue("DatabaseBackupNotFoundException_StorageAccountName", this.StorageAccountName, typeof(string));
			}
			if (this.ContainerID != null)
			{
				info.AddValue("DatabaseBackupNotFoundException_ContainerID", this.ContainerID, typeof(string));
			}
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00033B40 File Offset: 0x00031D40
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Backup for database {0} cannot be found in storage {1} and container ID {2}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : ((this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.StorageAccountName != null) ? this.StorageAccountName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.StorageAccountName != null) ? this.StorageAccountName.MarkIfInternal() : string.Empty) : ((this.StorageAccountName != null) ? this.StorageAccountName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.ContainerID != null) ? this.ContainerID.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.ContainerID != null) ? this.ContainerID.MarkIfInternal() : string.Empty) : ((this.ContainerID != null) ? this.ContainerID.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x00033C79 File Offset: 0x00031E79
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

		// Token: 0x06000D5D RID: 3421 RVA: 0x00033C98 File Offset: 0x00031E98
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "StorageAccountName={0}", (this.StorageAccountName != null) ? this.StorageAccountName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "StorageAccountName={0}", (this.StorageAccountName != null) ? this.StorageAccountName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "StorageAccountName={0}", (this.StorageAccountName != null) ? this.StorageAccountName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "ContainerID={0}", (this.ContainerID != null) ? this.ContainerID.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "ContainerID={0}", (this.ContainerID != null) ? this.ContainerID.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "ContainerID={0}", (this.ContainerID != null) ? this.ContainerID.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00033EAC File Offset: 0x000320AC
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00033EB5 File Offset: 0x000320B5
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00033EBE File Offset: 0x000320BE
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00033EAC File Offset: 0x000320AC
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00033EC8 File Offset: 0x000320C8
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

		// Token: 0x04000349 RID: 841
		private string creationMessage;

		// Token: 0x0400034A RID: 842
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400034B RID: 843
		private string m_databaseName;

		// Token: 0x0400034C RID: 844
		private string m_storageAccountName;

		// Token: 0x0400034D RID: 845
		private string m_containerID;
	}
}
