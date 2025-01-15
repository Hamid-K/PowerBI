using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000F7 RID: 247
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class FabricIntegratorReplicatorNotCreatedException : MonitoredException
	{
		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002B4EC File Offset: 0x000296EC
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0002B4F4 File Offset: 0x000296F4
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x0002B4FC File Offset: 0x000296FC
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

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002B505 File Offset: 0x00029705
		public FabricIntegratorReplicatorNotCreatedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002B519 File Offset: 0x00029719
		public FabricIntegratorReplicatorNotCreatedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002B530 File Offset: 0x00029730
		public FabricIntegratorReplicatorNotCreatedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002B550 File Offset: 0x00029750
		protected FabricIntegratorReplicatorNotCreatedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("FabricIntegratorReplicatorNotCreatedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseId = (string)info.GetValue("FabricIntegratorReplicatorNotCreatedException_DatabaseId", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseId = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("FabricIntegratorReplicatorNotCreatedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002B624 File Offset: 0x00029824
		public FabricIntegratorReplicatorNotCreatedException(string databaseId, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseId = databaseId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002B642 File Offset: 0x00029842
		public FabricIntegratorReplicatorNotCreatedException(string databaseId, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseId = databaseId;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002B668 File Offset: 0x00029868
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002B69F File Offset: 0x0002989F
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002B6A8 File Offset: 0x000298A8
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(FabricIntegratorReplicatorNotCreatedException))
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

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002B778 File Offset: 0x00029978
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("FabricIntegratorReplicatorNotCreatedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("FabricIntegratorReplicatorNotCreatedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseId != null)
			{
				info.AddValue("FabricIntegratorReplicatorNotCreatedException_DatabaseId", this.DatabaseId, typeof(string));
			}
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002B7F8 File Offset: 0x000299F8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "The Fabric Host failed to create a replicator for database: {0}", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseId != null) ? this.DatabaseId.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseId != null) ? this.DatabaseId.MarkIfInternal() : string.Empty) : ((this.DatabaseId != null) ? this.DatabaseId.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0002B873 File Offset: 0x00029A73
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

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002B890 File Offset: 0x00029A90
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseId={0}", (this.DatabaseId != null) ? this.DatabaseId.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseId={0}", (this.DatabaseId != null) ? this.DatabaseId.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseId={0}", (this.DatabaseId != null) ? this.DatabaseId.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002B954 File Offset: 0x00029B54
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002B95D File Offset: 0x00029B5D
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002B966 File Offset: 0x00029B66
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002B954 File Offset: 0x00029B54
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002B970 File Offset: 0x00029B70
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

		// Token: 0x04000308 RID: 776
		private string creationMessage;

		// Token: 0x04000309 RID: 777
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x0400030A RID: 778
		private string m_databaseId;
	}
}
