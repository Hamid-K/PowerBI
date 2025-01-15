using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000E1 RID: 225
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ProcessDatabaseFailedSessionCancelledException : MonitoredException
	{
		// Token: 0x060009BD RID: 2493 RVA: 0x000221E4 File Offset: 0x000203E4
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x000221EC File Offset: 0x000203EC
		// (set) Token: 0x060009BF RID: 2495 RVA: 0x000221F4 File Offset: 0x000203F4
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

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x000221FD File Offset: 0x000203FD
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x00022205 File Offset: 0x00020405
		public string IsOOL
		{
			get
			{
				return this.m_isOOL;
			}
			protected set
			{
				this.m_isOOL = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x0002220E File Offset: 0x0002040E
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x00022216 File Offset: 0x00020416
		public string IsTM
		{
			get
			{
				return this.m_isTM;
			}
			protected set
			{
				this.m_isTM = value;
			}
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0002221F File Offset: 0x0002041F
		public ProcessDatabaseFailedSessionCancelledException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0002223D File Offset: 0x0002043D
		public ProcessDatabaseFailedSessionCancelledException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00022254 File Offset: 0x00020454
		public ProcessDatabaseFailedSessionCancelledException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00022274 File Offset: 0x00020474
		protected ProcessDatabaseFailedSessionCancelledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ProcessDatabaseFailedSessionCancelledException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DatabaseName = (string)info.GetValue("ProcessDatabaseFailedSessionCancelledException_DatabaseName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseName = null;
			}
			try
			{
				this.IsOOL = (string)info.GetValue("ProcessDatabaseFailedSessionCancelledException_IsOOL", typeof(string));
			}
			catch (SerializationException)
			{
				this.IsOOL = null;
			}
			try
			{
				this.IsTM = (string)info.GetValue("ProcessDatabaseFailedSessionCancelledException_IsTM", typeof(string));
			}
			catch (SerializationException)
			{
				this.IsTM = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ProcessDatabaseFailedSessionCancelledException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x000223BC File Offset: 0x000205BC
		public ProcessDatabaseFailedSessionCancelledException(string databaseName, string isOOL, string isTM)
		{
			this.DatabaseName = databaseName;
			this.IsOOL = isOOL;
			this.IsTM = isTM;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x000223E0 File Offset: 0x000205E0
		public ProcessDatabaseFailedSessionCancelledException(string databaseName, string isOOL, string isTM, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.IsOOL = isOOL;
			this.IsTM = isTM;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0002240E File Offset: 0x0002060E
		public ProcessDatabaseFailedSessionCancelledException(string databaseName, string isOOL, string isTM, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DatabaseName = databaseName;
			this.IsOOL = isOOL;
			this.IsTM = isTM;
			this.ConstructorInternal(false);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00022444 File Offset: 0x00020644
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0002247C File Offset: 0x0002067C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ProcessDatabaseFailedSessionCancelledException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ProcessDatabaseFailedSessionCancelledException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DatabaseName != null)
			{
				info.AddValue("ProcessDatabaseFailedSessionCancelledException_DatabaseName", this.DatabaseName, typeof(string));
			}
			if (this.IsOOL != null)
			{
				info.AddValue("ProcessDatabaseFailedSessionCancelledException_IsOOL", this.IsOOL, typeof(string));
			}
			if (this.IsTM != null)
			{
				info.AddValue("ProcessDatabaseFailedSessionCancelledException_IsTM", this.IsTM, typeof(string));
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00022540 File Offset: 0x00020740
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "ProcessDatabase for database {0} failed", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : ((this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x000225BB File Offset: 0x000207BB
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

		// Token: 0x060009D0 RID: 2512 RVA: 0x000225D8 File Offset: 0x000207D8
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "IsOOL={0}", (this.IsOOL != null) ? this.IsOOL.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "IsOOL={0}", (this.IsOOL != null) ? this.IsOOL.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "IsOOL={0}", (this.IsOOL != null) ? this.IsOOL.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "IsTM={0}", (this.IsTM != null) ? this.IsTM.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "IsTM={0}", (this.IsTM != null) ? this.IsTM.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "IsTM={0}", (this.IsTM != null) ? this.IsTM.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x000227EC File Offset: 0x000209EC
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x000227F5 File Offset: 0x000209F5
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x000227FE File Offset: 0x000209FE
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x000227EC File Offset: 0x000209EC
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00022808 File Offset: 0x00020A08
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

		// Token: 0x040002B5 RID: 693
		private string creationMessage;

		// Token: 0x040002B6 RID: 694
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040002B7 RID: 695
		private string m_databaseName;

		// Token: 0x040002B8 RID: 696
		private string m_isOOL;

		// Token: 0x040002B9 RID: 697
		private string m_isTM;
	}
}
