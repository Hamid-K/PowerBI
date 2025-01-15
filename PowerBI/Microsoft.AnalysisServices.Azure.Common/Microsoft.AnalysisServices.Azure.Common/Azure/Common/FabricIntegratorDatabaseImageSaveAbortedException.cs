using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000102 RID: 258
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class FabricIntegratorDatabaseImageSaveAbortedException : MonitoredException
	{
		// Token: 0x06000C97 RID: 3223 RVA: 0x00030364 File Offset: 0x0002E564
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x0003036C File Offset: 0x0002E56C
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x00030374 File Offset: 0x0002E574
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

		// Token: 0x06000C9A RID: 3226 RVA: 0x0003037D File Offset: 0x0002E57D
		public FabricIntegratorDatabaseImageSaveAbortedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00030391 File Offset: 0x0002E591
		public FabricIntegratorDatabaseImageSaveAbortedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x000303A8 File Offset: 0x0002E5A8
		public FabricIntegratorDatabaseImageSaveAbortedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x000303C8 File Offset: 0x0002E5C8
		protected FabricIntegratorDatabaseImageSaveAbortedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("FabricIntegratorDatabaseImageSaveAbortedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseId = (string)info.GetValue("FabricIntegratorDatabaseImageSaveAbortedException_DatabaseId", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseId = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("FabricIntegratorDatabaseImageSaveAbortedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0003049C File Offset: 0x0002E69C
		public FabricIntegratorDatabaseImageSaveAbortedException(string databaseId, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseId = databaseId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x000304BA File Offset: 0x0002E6BA
		public FabricIntegratorDatabaseImageSaveAbortedException(string databaseId, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseId = databaseId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x000304E0 File Offset: 0x0002E6E0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00030517 File Offset: 0x0002E717
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00030520 File Offset: 0x0002E720
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(FabricIntegratorDatabaseImageSaveAbortedException))
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

		// Token: 0x06000CA3 RID: 3235 RVA: 0x000305F0 File Offset: 0x0002E7F0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("FabricIntegratorDatabaseImageSaveAbortedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("FabricIntegratorDatabaseImageSaveAbortedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseId != null)
			{
				info.AddValue("FabricIntegratorDatabaseImageSaveAbortedException_DatabaseId", this.DatabaseId, typeof(string));
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00030670 File Offset: 0x0002E870
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Database {0} image save aborted", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseId != null) ? this.DatabaseId.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseId != null) ? this.DatabaseId.MarkIfInternal() : string.Empty) : ((this.DatabaseId != null) ? this.DatabaseId.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x000306EB File Offset: 0x0002E8EB
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

		// Token: 0x06000CA6 RID: 3238 RVA: 0x00030708 File Offset: 0x0002E908
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseId={0}", (this.DatabaseId != null) ? this.DatabaseId.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseId={0}", (this.DatabaseId != null) ? this.DatabaseId.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseId={0}", (this.DatabaseId != null) ? this.DatabaseId.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x000307CC File Offset: 0x0002E9CC
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x000307D5 File Offset: 0x0002E9D5
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x000307DE File Offset: 0x0002E9DE
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x000307CC File Offset: 0x0002E9CC
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x000307E8 File Offset: 0x0002E9E8
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

		// Token: 0x04000330 RID: 816
		private string creationMessage;

		// Token: 0x04000331 RID: 817
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000332 RID: 818
		private string m_databaseId;
	}
}
