using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.AnalysisServices.Azure.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Resta
{
	// Token: 0x02000018 RID: 24
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class RestaDatasetMigrationException : RestaProcessorException
	{
		// Token: 0x06000166 RID: 358 RVA: 0x00007FF4 File Offset: 0x000061F4
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00007FFC File Offset: 0x000061FC
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00008004 File Offset: 0x00006204
		public bool RollbackRequired
		{
			get
			{
				return this.m_rollbackRequired;
			}
			protected set
			{
				this.m_rollbackRequired = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000800D File Offset: 0x0000620D
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00008015 File Offset: 0x00006215
		public bool IncompatibleDataset
		{
			get
			{
				return this.m_incompatibleDataset;
			}
			protected set
			{
				this.m_incompatibleDataset = value;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000801E File Offset: 0x0000621E
		public RestaDatasetMigrationException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidValueField<bool>();
			CompileCheck.IsValidValueField<bool>();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00008037 File Offset: 0x00006237
		public RestaDatasetMigrationException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000804E File Offset: 0x0000624E
		public RestaDatasetMigrationException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000806C File Offset: 0x0000626C
		protected RestaDatasetMigrationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("RestaDatasetMigrationException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			this.RollbackRequired = (bool)info.GetValue("RestaDatasetMigrationException_RollbackRequired", typeof(bool));
			this.IncompatibleDataset = (bool)info.GetValue("RestaDatasetMigrationException_IncompatibleDataset", typeof(bool));
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("RestaDatasetMigrationException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00008148 File Offset: 0x00006348
		public RestaDatasetMigrationException(bool rollbackRequired, bool incompatibleDataset)
		{
			this.RollbackRequired = rollbackRequired;
			this.IncompatibleDataset = incompatibleDataset;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00008165 File Offset: 0x00006365
		public RestaDatasetMigrationException(bool rollbackRequired, bool incompatibleDataset, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.RollbackRequired = rollbackRequired;
			this.IncompatibleDataset = incompatibleDataset;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000818A File Offset: 0x0000638A
		public RestaDatasetMigrationException(bool rollbackRequired, bool incompatibleDataset, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.RollbackRequired = rollbackRequired;
			this.IncompatibleDataset = incompatibleDataset;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000081B8 File Offset: 0x000063B8
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000081EF File Offset: 0x000063EF
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000081F8 File Offset: 0x000063F8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(RestaDatasetMigrationException))
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

		// Token: 0x06000175 RID: 373 RVA: 0x000082C8 File Offset: 0x000064C8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("RestaDatasetMigrationException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("RestaDatasetMigrationException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			info.AddValue("RestaDatasetMigrationException_RollbackRequired", this.RollbackRequired, typeof(bool));
			info.AddValue("RestaDatasetMigrationException_IncompatibleDataset", this.IncompatibleDataset, typeof(bool));
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00008363 File Offset: 0x00006563
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Dataset V1->V2 migration failed", Array.Empty<object>());
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00008379 File Offset: 0x00006579
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

		// Token: 0x06000178 RID: 376 RVA: 0x00008398 File Offset: 0x00006598
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "RollbackRequired={0}", this.RollbackRequired.ToString()) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "RollbackRequired={0}", this.RollbackRequired.ToString()) : string.Format(CultureInfo.CurrentCulture, "RollbackRequired={0}", this.RollbackRequired.ToString())));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "IncompatibleDataset={0}", this.IncompatibleDataset.ToString()) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "IncompatibleDataset={0}", this.IncompatibleDataset.ToString()) : string.Format(CultureInfo.CurrentCulture, "IncompatibleDataset={0}", this.IncompatibleDataset.ToString())));
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000084B0 File Offset: 0x000066B0
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000084B9 File Offset: 0x000066B9
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000084C2 File Offset: 0x000066C2
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000084B0 File Offset: 0x000066B0
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000084CC File Offset: 0x000066CC
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

		// Token: 0x04000052 RID: 82
		private string creationMessage;

		// Token: 0x04000053 RID: 83
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000054 RID: 84
		private bool m_rollbackRequired;

		// Token: 0x04000055 RID: 85
		private bool m_incompatibleDataset;
	}
}
