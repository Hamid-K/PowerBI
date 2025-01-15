using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000F3 RID: 243
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class UnsupportedOperationForPbiSharedException : MonitoredException
	{
		// Token: 0x06000B47 RID: 2887 RVA: 0x000298B4 File Offset: 0x00027AB4
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x000298BC File Offset: 0x00027ABC
		// (set) Token: 0x06000B49 RID: 2889 RVA: 0x000298C4 File Offset: 0x00027AC4
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

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x000298CD File Offset: 0x00027ACD
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x000298D5 File Offset: 0x00027AD5
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

		// Token: 0x06000B4C RID: 2892 RVA: 0x000298DE File Offset: 0x00027ADE
		public UnsupportedOperationForPbiSharedException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x000298F7 File Offset: 0x00027AF7
		public UnsupportedOperationForPbiSharedException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0002990E File Offset: 0x00027B0E
		public UnsupportedOperationForPbiSharedException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0002992C File Offset: 0x00027B2C
		protected UnsupportedOperationForPbiSharedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("UnsupportedOperationForPbiSharedException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.OperationName = (string)info.GetValue("UnsupportedOperationForPbiSharedException_OperationName", typeof(string));
			}
			catch (SerializationException)
			{
				this.OperationName = null;
			}
			try
			{
				this.DatabaseName = (string)info.GetValue("UnsupportedOperationForPbiSharedException_DatabaseName", typeof(string));
			}
			catch (SerializationException)
			{
				this.DatabaseName = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("UnsupportedOperationForPbiSharedException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00029A3C File Offset: 0x00027C3C
		public UnsupportedOperationForPbiSharedException(string operationName, string databaseName)
		{
			this.OperationName = operationName;
			this.DatabaseName = databaseName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00029A59 File Offset: 0x00027C59
		public UnsupportedOperationForPbiSharedException(string operationName, string databaseName, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.OperationName = operationName;
			this.DatabaseName = databaseName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00029A7E File Offset: 0x00027C7E
		public UnsupportedOperationForPbiSharedException(string operationName, string databaseName, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.OperationName = operationName;
			this.DatabaseName = databaseName;
			this.ConstructorInternal(false);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00029AAC File Offset: 0x00027CAC
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0000C243 File Offset: 0x0000A443
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00029AE4 File Offset: 0x00027CE4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("UnsupportedOperationForPbiSharedException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("UnsupportedOperationForPbiSharedException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.OperationName != null)
			{
				info.AddValue("UnsupportedOperationForPbiSharedException_OperationName", this.OperationName, typeof(string));
			}
			if (this.DatabaseName != null)
			{
				info.AddValue("UnsupportedOperationForPbiSharedException_DatabaseName", this.DatabaseName, typeof(string));
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00029B88 File Offset: 0x00027D88
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "{0} is unsupported for PowerBI shared database '{1}'.", (markupKind == PrivateInformationMarkupKind.None) ? ((this.OperationName != null) ? this.OperationName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.OperationName != null) ? this.OperationName.MarkIfInternal() : string.Empty) : ((this.OperationName != null) ? this.OperationName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)), (markupKind == PrivateInformationMarkupKind.None) ? ((this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : ((this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x00029C62 File Offset: 0x00027E62
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

		// Token: 0x06000B58 RID: 2904 RVA: 0x00029C80 File Offset: 0x00027E80
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "OperationName={0}", (this.OperationName != null) ? this.OperationName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "OperationName={0}", (this.OperationName != null) ? this.OperationName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "OperationName={0}", (this.OperationName != null) ? this.OperationName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfInternal() : string.Empty) : string.Format(CultureInfo.CurrentCulture, "DatabaseName={0}", (this.DatabaseName != null) ? this.DatabaseName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)));
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00029DEC File Offset: 0x00027FEC
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00029DF5 File Offset: 0x00027FF5
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00029DFE File Offset: 0x00027FFE
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00029DEC File Offset: 0x00027FEC
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00029E08 File Offset: 0x00028008
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

		// Token: 0x040002F9 RID: 761
		private string creationMessage;

		// Token: 0x040002FA RID: 762
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040002FB RID: 763
		private string m_operationName;

		// Token: 0x040002FC RID: 764
		private string m_databaseName;
	}
}
