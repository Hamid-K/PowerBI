using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200031E RID: 798
	[GeneratedCode("CloudPlatformException", "4.0.0.0")]
	[Serializable]
	public class CorruptedFileException : CompressionException
	{
		// Token: 0x06001705 RID: 5893 RVA: 0x00053CA8 File Offset: 0x00051EA8
		public override ExceptionCulprit GetExceptionCulprit()
		{
			return this.exceptionCulprit;
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x00053CB0 File Offset: 0x00051EB0
		// (set) Token: 0x06001707 RID: 5895 RVA: 0x00053CB8 File Offset: 0x00051EB8
		public string Compressed
		{
			get
			{
				return this.m_compressed;
			}
			protected set
			{
				this.m_compressed = value;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001708 RID: 5896 RVA: 0x00053CC1 File Offset: 0x00051EC1
		// (set) Token: 0x06001709 RID: 5897 RVA: 0x00053CC9 File Offset: 0x00051EC9
		public string Decompressed
		{
			get
			{
				return this.m_decompressed;
			}
			protected set
			{
				this.m_decompressed = value;
			}
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x00053CD2 File Offset: 0x00051ED2
		public CorruptedFileException()
		{
			this.ConstructorInternal(false);
			CompileCheck.IsValidReferenceField<string>();
			CompileCheck.IsValidReferenceField<string>();
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00053CEB File Offset: 0x00051EEB
		public CorruptedFileException(string message)
			: base(message)
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x00053D02 File Offset: 0x00051F02
		public CorruptedFileException(string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00053D20 File Offset: 0x00051F20
		protected CorruptedFileException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			try
			{
				this.creationMessage = (string)info.GetValue("CorruptedFileException_creationMessage", typeof(string));
			}
			catch (SerializationException)
			{
				this.creationMessage = null;
			}
			try
			{
				this.Compressed = (string)info.GetValue("CorruptedFileException_Compressed", typeof(string));
			}
			catch (SerializationException)
			{
				this.Compressed = null;
			}
			try
			{
				this.Decompressed = (string)info.GetValue("CorruptedFileException_Decompressed", typeof(string));
			}
			catch (SerializationException)
			{
				this.Decompressed = null;
			}
			this.ConstructorInternal(true);
			try
			{
				this.exceptionCulprit = (ExceptionCulprit)info.GetValue("CorruptedFileException_exceptionCulprit", typeof(ExceptionCulprit));
			}
			catch (SerializationException)
			{
				this.exceptionCulprit = ExceptionCulprit.System;
			}
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00053E30 File Offset: 0x00052030
		public CorruptedFileException(string compressed, string decompressed)
		{
			this.Compressed = compressed;
			this.Decompressed = decompressed;
			this.ConstructorInternal(false);
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x00053E4D File Offset: 0x0005204D
		public CorruptedFileException(string compressed, string decompressed, string message)
			: base(message)
		{
			this.creationMessage = message;
			this.Compressed = compressed;
			this.Decompressed = decompressed;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x00053E72 File Offset: 0x00052072
		public CorruptedFileException(string compressed, string decompressed, string message, Exception innerException)
			: base(message, InnerExceptionCreator.GetInnerException(innerException))
		{
			this.creationMessage = message;
			this.Compressed = compressed;
			this.Decompressed = decompressed;
			this.ConstructorInternal(false);
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x00053EA0 File Offset: 0x000520A0
		private void ConstructorInternal(bool deserializing)
		{
			this.Constructor(deserializing);
			MonitoredException ex = base.InnerException as MonitoredException;
			if (ex != null && this.exceptionCulprit == ExceptionCulprit.System)
			{
				this.exceptionCulprit = ex.GetExceptionCulprit();
			}
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x00053ED7 File Offset: 0x000520D7
		private void Constructor(bool deserializing)
		{
			this.TraceConstructor();
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x00053EE0 File Offset: 0x000520E0
		private void TraceConstructor()
		{
			Type type = base.GetType();
			if (type == typeof(CorruptedFileException))
			{
				TraceSourceBase<UtilsTrace>.Tracer.TraceError("Exception object created: {0}: {1}{2}", new object[]
				{
					type,
					this.Message,
					(base.InnerException != null) ? "(wrapped: {0}/{1})".FormatWithInvariantCulture(new object[]
					{
						base.InnerException.GetType().Name,
						base.InnerException.Message
					}) : string.Empty
				});
				if (TraceSourceBase<UtilsTrace>.Tracer.ShouldTrace(TraceVerbosity.Error) && (base.InnerException == null || !(base.InnerException is IMonitoredError)))
				{
					TraceSourceBase<UtilsTrace>.Tracer.TraceError("StackTrace: {0}", new object[]
					{
						new StackTrace(0, true)
					});
				}
			}
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x00053FB0 File Offset: 0x000521B0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SerializationInfo>(info, "info");
			base.GetObjectData(info, context);
			info.AddValue("CorruptedFileException_creationMessage", this.creationMessage, typeof(string));
			info.AddValue("CorruptedFileException_exceptionCulprit", this.exceptionCulprit, typeof(ExceptionCulprit));
			if (this.Compressed != null)
			{
				info.AddValue("CorruptedFileException_Compressed", this.Compressed, typeof(string));
			}
			if (this.Decompressed != null)
			{
				info.AddValue("CorruptedFileException_Decompressed", this.Decompressed, typeof(string));
			}
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00054054 File Offset: 0x00052254
		private string CreateMessageFromTemplate(PrivateInformationMarkupKind markupKind)
		{
			return string.Format(CultureInfo.CurrentCulture, "Failed to decompress file '{0}' into '{1}' due to corrupted file", new object[]
			{
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Compressed != null) ? this.Compressed.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Compressed != null) ? this.Compressed.MarkIfInternal() : string.Empty) : ((this.Compressed != null) ? this.Compressed.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty)),
				(markupKind == PrivateInformationMarkupKind.None) ? ((this.Decompressed != null) ? this.Decompressed.ToString() : string.Empty) : ((markupKind == PrivateInformationMarkupKind.Internal) ? ((this.Decompressed != null) ? this.Decompressed.MarkIfInternal() : string.Empty) : ((this.Decompressed != null) ? this.Decompressed.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty))
			});
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001716 RID: 5910 RVA: 0x0005413A File Offset: 0x0005233A
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

		// Token: 0x06001717 RID: 5911 RVA: 0x00054158 File Offset: 0x00052358
		protected override string GetPropertiesString(PrivateInformationMarkupKind markupKind)
		{
			string text = Environment.NewLine;
			text = base.GetPropertiesString(markupKind);
			text = (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Compressed={0}", new object[] { (this.Compressed != null) ? this.Compressed.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Compressed={0}", new object[] { (this.Compressed != null) ? this.Compressed.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Compressed={0}", new object[] { (this.Compressed != null) ? this.Compressed.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
			return (string.IsNullOrEmpty(text) ? text : (text + Environment.NewLine)) + ((markupKind == PrivateInformationMarkupKind.None) ? string.Format(CultureInfo.CurrentCulture, "Decompressed={0}", new object[] { (this.Decompressed != null) ? this.Decompressed.ToString() : string.Empty }) : ((markupKind == PrivateInformationMarkupKind.Internal) ? string.Format(CultureInfo.CurrentCulture, "Decompressed={0}", new object[] { (this.Decompressed != null) ? this.Decompressed.MarkIfInternal() : string.Empty }) : string.Format(CultureInfo.CurrentCulture, "Decompressed={0}", new object[] { (this.Decompressed != null) ? this.Decompressed.MarkIfPrivate().ObfuscatePrivateValue(false) : string.Empty })));
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x000542FA File Offset: 0x000524FA
		public override string ToString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x00054303 File Offset: 0x00052503
		public override string ToPrivateString()
		{
			return this.ToString(PrivateInformationMarkupKind.Private);
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0005430C File Offset: 0x0005250C
		public override string ToInternalString()
		{
			return this.ToString(PrivateInformationMarkupKind.Internal);
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x000542FA File Offset: 0x000524FA
		public override string ToOriginalString()
		{
			return this.ToString(PrivateInformationMarkupKind.None);
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x00054318 File Offset: 0x00052518
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

		// Token: 0x04000815 RID: 2069
		private string creationMessage;

		// Token: 0x04000816 RID: 2070
		private ExceptionCulprit exceptionCulprit;

		// Token: 0x04000817 RID: 2071
		private string m_compressed;

		// Token: 0x04000818 RID: 2072
		private string m_decompressed;
	}
}
