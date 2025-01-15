using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000056 RID: 86
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class DatabaseStorageModeViolationException : DatabaseException
	{
		// Token: 0x0600020A RID: 522 RVA: 0x00006D98 File Offset: 0x00004F98
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00006DA0 File Offset: 0x00004FA0
		// (set) Token: 0x0600020C RID: 524 RVA: 0x00006DA8 File Offset: 0x00004FA8
		public StorageOperationMode CurrentMode
		{
			get
			{
				return this.m_currentMode;
			}
			protected set
			{
				this.m_currentMode = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00006DB1 File Offset: 0x00004FB1
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00006DB9 File Offset: 0x00004FB9
		public StorageOperationMode RequestedMode
		{
			get
			{
				return this.m_requestedMode;
			}
			protected set
			{
				this.m_requestedMode = value;
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00006DC2 File Offset: 0x00004FC2
		public DatabaseStorageModeViolationException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidValueField<StorageOperationMode>();
			CompileCheck.IsValidValueField<StorageOperationMode>();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00006DDB File Offset: 0x00004FDB
		public DatabaseStorageModeViolationException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00006DF2 File Offset: 0x00004FF2
		public DatabaseStorageModeViolationException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00006E10 File Offset: 0x00005010
		protected DatabaseStorageModeViolationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("DatabaseStorageModeViolationException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.CurrentMode = (StorageOperationMode)info.GetValue("DatabaseStorageModeViolationException_CurrentMode", typeof(StorageOperationMode));
			this.RequestedMode = (StorageOperationMode)info.GetValue("DatabaseStorageModeViolationException_RequestedMode", typeof(StorageOperationMode));
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("DatabaseStorageModeViolationException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00006EEC File Offset: 0x000050EC
		public DatabaseStorageModeViolationException(StorageOperationMode currentMode, StorageOperationMode requestedMode)
		{
			this.CurrentMode = currentMode;
			this.RequestedMode = requestedMode;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00006F09 File Offset: 0x00005109
		public DatabaseStorageModeViolationException(StorageOperationMode currentMode, StorageOperationMode requestedMode, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.CurrentMode = currentMode;
			this.RequestedMode = requestedMode;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00006F2E File Offset: 0x0000512E
		public DatabaseStorageModeViolationException(StorageOperationMode currentMode, StorageOperationMode requestedMode, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.CurrentMode = currentMode;
			this.RequestedMode = requestedMode;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00006F5C File Offset: 0x0000515C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00006F93 File Offset: 0x00005193
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00006F9C File Offset: 0x0000519C
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(DatabaseStorageModeViolationException))
			{
				TraceSourceBase<StorageTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<StorageTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<StorageTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000706C File Offset: 0x0000526C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("DatabaseStorageModeViolationException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("DatabaseStorageModeViolationException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			info.AddValue("DatabaseStorageModeViolationException_CurrentMode", this.CurrentMode, typeof(StorageOperationMode));
			info.AddValue("DatabaseStorageModeViolationException_RequestedMode", this.RequestedMode, typeof(StorageOperationMode));
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007108 File Offset: 0x00005308
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The Database operation failed because the access mode '{0}' is incompatible with the requested mode '{1}'.", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? this.CurrentMode.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.CurrentMode.ToString() : this.CurrentMode.ToString()),
				(markupKind == PrivateInformationMarkupKind.None) ? this.RequestedMode.ToString() : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.RequestedMode.ToString() : this.RequestedMode.ToString())
			});
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600021B RID: 539 RVA: 0x000071BE File Offset: 0x000053BE
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

		// Token: 0x0600021C RID: 540 RVA: 0x000071DC File Offset: 0x000053DC
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "CurrentMode={0}", new object[] { this.CurrentMode.ToString() }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "CurrentMode={0}", new object[] { this.CurrentMode.ToString() }) : string.Format(CultureInfo.CurrentCulture, "CurrentMode={0}", new object[] { this.CurrentMode.ToString() })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "RequestedMode={0}", new object[] { this.RequestedMode.ToString() }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "RequestedMode={0}", new object[] { this.RequestedMode.ToString() }) : string.Format(CultureInfo.CurrentCulture, "RequestedMode={0}", new object[] { this.RequestedMode.ToString() })));
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000734E File Offset: 0x0000554E
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00007357 File Offset: 0x00005557
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007360 File Offset: 0x00005560
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000734E File Offset: 0x0000554E
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000736C File Offset: 0x0000556C
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

		// Token: 0x040000E2 RID: 226
		private string creationMessage;

		// Token: 0x040000E3 RID: 227
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040000E4 RID: 228
		private StorageOperationMode m_currentMode;

		// Token: 0x040000E5 RID: 229
		private StorageOperationMode m_requestedMode;
	}
}
