using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000303 RID: 771
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class ExtendedFileVersionInfoException : Win32Exception
	{
		// Token: 0x060014E9 RID: 5353 RVA: 0x00049BF0 File Offset: 0x00047DF0
		public virtual ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x00049BF8 File Offset: 0x00047DF8
		// (set) Token: 0x060014EB RID: 5355 RVA: 0x00049C00 File Offset: 0x00047E00
		public string FileName
		{
			get
			{
				return this.m_fileName;
			}
			protected set
			{
				this.m_fileName = value;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x00049C09 File Offset: 0x00047E09
		// (set) Token: 0x060014ED RID: 5357 RVA: 0x00049C11 File Offset: 0x00047E11
		public string Function
		{
			get
			{
				return this.m_function;
			}
			protected set
			{
				this.m_function = value;
			}
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00049C1A File Offset: 0x00047E1A
		public ExtendedFileVersionInfoException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x00049C33 File Offset: 0x00047E33
		public ExtendedFileVersionInfoException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x00049C4A File Offset: 0x00047E4A
		public ExtendedFileVersionInfoException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x00049C68 File Offset: 0x00047E68
		protected ExtendedFileVersionInfoException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("ExtendedFileVersionInfoException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.FileName = (string)info.GetValue("ExtendedFileVersionInfoException_FileName", typeof(string));
			}
			catch (SerializationException)
			{
				this.FileName = null;
			}
			try
			{
				this.Function = (string)info.GetValue("ExtendedFileVersionInfoException_Function", typeof(string));
			}
			catch (SerializationException)
			{
				this.Function = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("ExtendedFileVersionInfoException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x00049D78 File Offset: 0x00047F78
		public ExtendedFileVersionInfoException(string fileName, string function)
		{
			this.FileName = fileName;
			this.Function = function;
			this.ConstructorInternal(false);
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x00049D95 File Offset: 0x00047F95
		public ExtendedFileVersionInfoException(string fileName, string function, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.FileName = fileName;
			this.Function = function;
			this.ConstructorInternal(false);
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x00049DBA File Offset: 0x00047FBA
		public ExtendedFileVersionInfoException(string fileName, string function, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.FileName = fileName;
			this.Function = function;
			this.ConstructorInternal(false);
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x00049DE6 File Offset: 0x00047FE6
		public ExtendedFileVersionInfoException(string fileName, string function, int errorCode)
			: base(errorCode)
		{
			this.FileName = fileName;
			this.Function = function;
			this.ConstructorInternal(false);
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x00049E04 File Offset: 0x00048004
		public ExtendedFileVersionInfoException(int errorCode, string message)
			: base(errorCode, message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x00049E1C File Offset: 0x0004801C
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x00009B3B File Offset: 0x00007D3B
		private void Constructor(bool deserializing)
		{
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x00049E54 File Offset: 0x00048054
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("ExtendedFileVersionInfoException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("ExtendedFileVersionInfoException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.FileName != null)
			{
				info.AddValue("ExtendedFileVersionInfoException_FileName", this.FileName, typeof(string));
			}
			if (this.Function != null)
			{
				info.AddValue("ExtendedFileVersionInfoException_Function", this.Function, typeof(string));
			}
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x00049EF8 File Offset: 0x000480F8
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Function '{0}' on file '{1}' produced error '{2}' ({3})", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Function != null) ? this.Function.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Function != null) ? this.Function.MarkIfInternal() : string.Empty) : ((this.Function != null) ? this.Function.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.FileName != null) ? this.FileName.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.FileName != null) ? this.FileName.MarkIfInternal() : string.Empty) : ((this.FileName != null) ? this.FileName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? this.ErrorCode.ToString(CultureInfo.InvariantCulture) : ((markupKind == PrivateInformationMarkupKind.Internal) ? this.ErrorCode.ToString(CultureInfo.InvariantCulture) : this.ErrorCode.ToString(CultureInfo.InvariantCulture)),
				(markupKind == PrivateInformationMarkupKind.None) ? (base.Message ?? string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? (base.Message ?? string.Empty) : (base.Message ?? string.Empty))
			});
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x0004A060 File Offset: 0x00048260
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

		// Token: 0x060014FC RID: 5372 RVA: 0x0004A080 File Offset: 0x00048280
		protected virtual string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "FileName={0}", new object[] { (this.FileName != null) ? this.FileName.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "FileName={0}", new object[] { (this.FileName != null) ? this.FileName.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "FileName={0}", new object[] { (this.FileName != null) ? this.FileName.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Function={0}", new object[] { (this.Function != null) ? this.Function.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Function={0}", new object[] { (this.Function != null) ? this.Function.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Function={0}", new object[] { (this.Function != null) ? this.Function.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0004A21A File Offset: 0x0004841A
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x0004A223 File Offset: 0x00048423
		public virtual string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0004A22C File Offset: 0x0004842C
		public virtual string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x0004A21A File Offset: 0x0004841A
		public virtual string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x0004A238 File Offset: 0x00048438
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

		// Token: 0x040007CA RID: 1994
		private string creationMessage;

		// Token: 0x040007CB RID: 1995
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x040007CC RID: 1996
		private string m_fileName;

		// Token: 0x040007CD RID: 1997
		private string m_function;
	}
}
