using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000F4 RID: 244
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class OperationForPbiDedicatedDisabledOnBackupClusterException : MonitoredException
	{
		// Token: 0x06000B5E RID: 2910 RVA: 0x00029FF4 File Offset: 0x000281F4
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x00029FFC File Offset: 0x000281FC
		// (set) Token: 0x06000B60 RID: 2912 RVA: 0x0002A004 File Offset: 0x00028204
		public string OperationName
		{
			get
			{
				return this.m_operationName;
			}
			protected set
			{
				this.m_operationName = value;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x0002A00D File Offset: 0x0002820D
		// (set) Token: 0x06000B62 RID: 2914 RVA: 0x0002A015 File Offset: 0x00028215
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

		// Token: 0x06000B63 RID: 2915 RVA: 0x0002A01E File Offset: 0x0002821E
		public OperationForPbiDedicatedDisabledOnBackupClusterException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0002A037 File Offset: 0x00028237
		public OperationForPbiDedicatedDisabledOnBackupClusterException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0002A04E File Offset: 0x0002824E
		public OperationForPbiDedicatedDisabledOnBackupClusterException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0002A06C File Offset: 0x0002826C
		protected OperationForPbiDedicatedDisabledOnBackupClusterException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("OperationForPbiDedicatedDisabledOnBackupClusterException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.OperationName = (string)info.GetValue("OperationForPbiDedicatedDisabledOnBackupClusterException_OperationName", typeof(string));
			}
			catch (SerializationException)
			{
				this.OperationName = null;
			}
			try
			{
				this.DatabaseName = (string)info.GetValue("OperationForPbiDedicatedDisabledOnBackupClusterException_DatabaseName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("OperationForPbiDedicatedDisabledOnBackupClusterException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0002A17C File Offset: 0x0002837C
		public OperationForPbiDedicatedDisabledOnBackupClusterException(string operationName, string databaseName)
		{
			this.OperationName = operationName;
			this.DatabaseName = databaseName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0002A199 File Offset: 0x00028399
		public OperationForPbiDedicatedDisabledOnBackupClusterException(string operationName, string databaseName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.OperationName = operationName;
			this.DatabaseName = databaseName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0002A1BE File Offset: 0x000283BE
		public OperationForPbiDedicatedDisabledOnBackupClusterException(string operationName, string databaseName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.OperationName = operationName;
			this.DatabaseName = databaseName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0002A1EC File Offset: 0x000283EC
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0002A224 File Offset: 0x00028424
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("OperationForPbiDedicatedDisabledOnBackupClusterException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("OperationForPbiDedicatedDisabledOnBackupClusterException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.OperationName != null)
			{
				info.AddValue("OperationForPbiDedicatedDisabledOnBackupClusterException_OperationName", this.OperationName, typeof(string));
			}
			if (this.DatabaseName != null)
			{
				info.AddValue("OperationForPbiDedicatedDisabledOnBackupClusterException_DatabaseName", this.DatabaseName, typeof(string));
			}
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0002A2C8 File Offset: 0x000284C8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "'{0}' is disabled for Power BI dedicated database '{1}' on a backup cluster.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.OperationName != null) ? this.OperationName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.OperationName != null) ? this.OperationName.MarkIfInternal() : string.Empty) : ((this.OperationName != null) ? this.OperationName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : ((this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0002A3A2 File Offset: 0x000285A2
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

		// Token: 0x06000B6F RID: 2927 RVA: 0x0002A3C0 File Offset: 0x000285C0
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "OperationName={0}", (this.OperationName != null) ? this.OperationName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "OperationName={0}", (this.OperationName != null) ? this.OperationName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "OperationName={0}", (this.OperationName != null) ? this.OperationName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0002A52C File Offset: 0x0002872C
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002A535 File Offset: 0x00028735
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002A53E File Offset: 0x0002873E
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0002A52C File Offset: 0x0002872C
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002A548 File Offset: 0x00028748
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

		// Token: 0x040002FD RID: 765
		private string creationMessage;

		// Token: 0x040002FE RID: 766
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040002FF RID: 767
		private string m_operationName;

		// Token: 0x04000300 RID: 768
		private string m_databaseName;
	}
}
