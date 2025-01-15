using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000EC RID: 236
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class DataSourceDoesNotExistException : MonitoredException
	{
		// Token: 0x06000AB9 RID: 2745 RVA: 0x00026F7C File Offset: 0x0002517C
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x00026F84 File Offset: 0x00025184
		// (set) Token: 0x06000ABB RID: 2747 RVA: 0x00026F8C File Offset: 0x0002518C
		public string DataSourceName
		{
			get
			{
				return this.m_dataSourceName;
			}
			protected set
			{
				this.m_dataSourceName = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00026F95 File Offset: 0x00025195
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x00026F9D File Offset: 0x0002519D
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

		// Token: 0x06000ABE RID: 2750 RVA: 0x00026FA6 File Offset: 0x000251A6
		public DataSourceDoesNotExistException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00026FBF File Offset: 0x000251BF
		public DataSourceDoesNotExistException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00026FD6 File Offset: 0x000251D6
		public DataSourceDoesNotExistException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00026FF4 File Offset: 0x000251F4
		protected DataSourceDoesNotExistException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("DataSourceDoesNotExistException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.DataSourceName = (string)info.GetValue("DataSourceDoesNotExistException_DataSourceName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DataSourceName = null;
			}
			try
			{
				this.DatabaseName = (string)info.GetValue("DataSourceDoesNotExistException_DatabaseName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("DataSourceDoesNotExistException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00027104 File Offset: 0x00025304
		public DataSourceDoesNotExistException(string dataSourceName, string databaseName)
		{
			this.DataSourceName = dataSourceName;
			this.DatabaseName = databaseName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00027121 File Offset: 0x00025321
		public DataSourceDoesNotExistException(string dataSourceName, string databaseName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.DataSourceName = dataSourceName;
			this.DatabaseName = databaseName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00027146 File Offset: 0x00025346
		public DataSourceDoesNotExistException(string dataSourceName, string databaseName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.DataSourceName = dataSourceName;
			this.DatabaseName = databaseName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00027174 File Offset: 0x00025374
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x000271AC File Offset: 0x000253AC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("DataSourceDoesNotExistException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("DataSourceDoesNotExistException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.DataSourceName != null)
			{
				info.AddValue("DataSourceDoesNotExistException_DataSourceName", this.DataSourceName, typeof(string));
			}
			if (this.DatabaseName != null)
			{
				info.AddValue("DataSourceDoesNotExistException_DatabaseName", this.DatabaseName, typeof(string));
			}
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00027250 File Offset: 0x00025450
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Data source {0} does not exist in database {1}.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.DataSourceName != null) ? this.DataSourceName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DataSourceName != null) ? this.DataSourceName.MarkIfInternal() : string.Empty) : ((this.DataSourceName != null) ? this.DataSourceName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : ((this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0002732A File Offset: 0x0002552A
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

		// Token: 0x06000ACA RID: 2762 RVA: 0x00027348 File Offset: 0x00025548
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DataSourceName={0}", (this.DataSourceName != null) ? this.DataSourceName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DataSourceName={0}", (this.DataSourceName != null) ? this.DataSourceName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DataSourceName={0}", (this.DataSourceName != null) ? this.DataSourceName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x000274B4 File Offset: 0x000256B4
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x000274BD File Offset: 0x000256BD
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x000274C6 File Offset: 0x000256C6
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x000274B4 File Offset: 0x000256B4
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x000274D0 File Offset: 0x000256D0
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

		// Token: 0x040002E3 RID: 739
		private string creationMessage;

		// Token: 0x040002E4 RID: 740
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040002E5 RID: 741
		private string m_dataSourceName;

		// Token: 0x040002E6 RID: 742
		private string m_databaseName;
	}
}
